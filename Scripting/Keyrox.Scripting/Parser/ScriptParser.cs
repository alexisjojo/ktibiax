using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using Keyrox.Scripting.Attributes;
using Keyrox.Scripting.Controls;
using Keyrox.Scripting.Events;
using Keyrox.Scripting.Keywords;
using Keyrox.Scripting.Util;
using Keyrox.Shared.Objects;
using Keyrox.Shared.Reflection;
using Keyrox.SourceCode;
using Keyrox.Scripting.Actions;
using Tibia.Client;

namespace Keyrox.Scripting.Parser {
    public class ScriptParser {

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptParser"/> class.
        /// </summary>
        /// <param name="editor">The editor.</param>
        /// <param name="client">The client.</param>
        public ScriptParser(ScriptEditor editor, TibiaClient client) {
            this.Editor = editor;
        }

        #region "[rgn] Properties     "
        public bool SupressWarnings { get; set; }
        public ScriptFile ParsedScript { get; set; }
        public TibiaClient TibiaClient { get; set; }

        public ScriptEditor Editor { get; private set; }
        public SyntaxDocument Document { get { return Editor.Document; } }

        public KeywordCollection PlayerProperties { get; set; }
        public KeywordCollection CustomItems { get; set; }

        public Dictionary<Row, LocationKeyword> ScriptWayPoints { get; set; }
        public Dictionary<string, ScriptAction> ScriptActions { get; set; }
        public List<string> ScriptSections { get; set; }

        public Dictionary<string, string> ScriptParams { get; set; }
        public Dictionary<string, Row> SetParams { get; set; }
        public Dictionary<string, Row> GetParams { get; set; }

        public event EventHandler<LineErrorEventArgs> OnScriptError;
        public event EventHandler<TextEventArgs> OnOutputChange;
        public event EventHandler<NumberEventArgs> OnProgressReport;
        public event EventHandler OnParseComplete;
        #endregion

        #region "[rgn] Helper Methods "
        protected ScriptFile GetFileInfo() {
            var res = new ScriptFile();
            var text = Editor.Document.Text;

            res.Title = GetTextInsidePatern(text, "<Title>", "</Title>");
            res.Author = GetTextInsidePatern(text, "<Author>", "</Author>");
            res.About = GetTextInsidePatern(text, "<About>", "</About>");
            res.TibiaVersion = GetTextInsidePatern(text, "<Tibia>", "</Tibia>");
            res.ReleaseDate = GetTextInsidePatern(text, "<Date>", "</Date>");
            res.Notes = GetTextInsidePatern(text, "<Notes>", "</Notes>");
            res.Rows = new List<ScriptLine>();

            return res;
        }
        protected void AddOutputSeparatorLine() {
            if (OnOutputChange != null) {
                OnOutputChange(this, new TextEventArgs("----------------------------------------"));
            }
        }
        protected void AddOutput(string text) {
            if (OnOutputChange != null) {
                OnOutputChange(this, new TextEventArgs(text));
            }
        }
        protected int LastValidWord(Row row) {
            var comments = row.ToList().Where(w => w.Text.StartsWith("//"));
            if (comments.Count() > 0) {
                if (comments.Last().Index > 0) {
                    for (int i = (comments.Last().Index - 1); i > 0; i--) {
                        if (row[i].Text != " " && row[i].Text != "\t") { return i + 1; }
                    }
                }
                return comments.Last().Index;
            }
            return row.Count;
        }
        private bool IsEmptyRow(Row row) {
            foreach (Word word in row) {
                if (word.Type == WordType.Word) {
                    return false;
                }
            }
            return true; ;
        }
        protected void SetError(Row row, string text) {
            if (!row.Images.Contains(16)) { row.Images.Add(16); }
            foreach (Word word in row) {
                word.HasError = true;
                word.ErrorColor = Color.Red;
                word.ErrorTip = text;
                if (!word.Row.Images.Contains(16)) { word.Row.Images.Add(16); }
            }
            if (OnScriptError != null) { OnScriptError(this, new LineErrorEventArgs(new ScriptLineError(text, row, ScriptLineErrorType.Error))); }
        }
        protected void SetError(Word word, string text) {
            word.HasError = true;
            word.ErrorColor = Color.Red;
            word.ErrorTip = text;
            if (!word.Row.Images.Contains(16)) { word.Row.Images.Add(16); }
            if (OnScriptError != null) { OnScriptError(this, new LineErrorEventArgs(new ScriptLineError(text, word, ScriptLineErrorType.Error))); }
        }
        protected void SetWarning(Word word, string text) {
            if (SupressWarnings) { return; }
            if (!word.Row.Images.Contains(15)) { word.Row.Images.Add(15); }
            word.HasError = true;
            word.ErrorColor = Color.Orange;
            word.WarningTip = text;
            if (OnScriptError != null) { OnScriptError(this, new LineErrorEventArgs(new ScriptLineError(text, word, ScriptLineErrorType.Warning))); }
        }
        protected void SetWarning(Row row, string text) {
            if (SupressWarnings) { return; }
            if (!row.Images.Contains(15)) { row.Images.Add(15); }
            foreach (Word word in row) {
                word.HasError = true;
                word.ErrorColor = Color.Orange;
                word.WarningTip = text;
            }
            if (OnScriptError != null) { OnScriptError(this, new LineErrorEventArgs(new ScriptLineError(text, row, ScriptLineErrorType.Warning))); }
        }
        protected void SetWarning(string text) {
            if (SupressWarnings) { return; }
            if (OnScriptError != null) { OnScriptError(this, new LineErrorEventArgs(new ScriptLineError() { Message = text, ErrorType = ScriptLineErrorType.Warning })); }
        }
        public void ClearErrors() {
            foreach (Row row in Document) {
                foreach (Word word in row) {
                    word.HasError = false;
                    word.InfoTip = "";
                }
            }
        }
        public static int CountChars(string source, char args) {
            var total = 0;
            foreach (var ch in source) { if (ch == args) { total += 1; } }
            return total;
        }
        public static string GetTextInsidePatern(string source, string start, string end) {
            var startindex = source.IndexOf(start);
            if (startindex > -1) {
                var endindex = source.IndexOf(end, startindex);
                if (endindex > -1) {
                    return source.Substring(startindex + start.Length, endindex - startindex);
                }
            }
            return string.Empty;
        }
        public static string GetFunctionArgument(string row) {
            var first = row.IndexOf("(");
            if (first > -1) {
                var last = -1;
                for (int i = (row.Length - 1); i > 0; i--) {
                    if (row[i] == ')') { last = i; break; }
                }
                if (last > -1) {
                    return row.Substring(first + 1, (last - first) - 1);
                }
            }
            return string.Empty;
        }
        public static string[] GetFunctionArguments(string function) {
            var content = GetFunctionArgument(function);
            var args = function.Split(new[] { ' ' }, StringSplitOptions.None);
            return RemoveArgumentSpaces(args);
        }
        public static string RemoveSpacesAround(string text) {
            return text.TrimStart(' ').TrimEnd(' ');
        }
        public static string[] RemoveArgumentSpaces(string[] args) {
            var res = new List<string>();
            foreach (var s in args) { res.Add(RemoveSpacesAround(s)); }
            return res.ToArray();
        }
        public void GetScriptActions() {
            var types = Assembly.GetExecutingAssembly().GetTypesWithAttribute(typeof(ActionClass));
            ScriptActions = new Dictionary<string, ScriptAction>();
            foreach (var type in types) {
                if (type.GetInterfaces().Contains(typeof(ITibiaAction))) {
                    var actions = type.GetMethodsWithAttribute(typeof(ActionTitle));
                    actions.ToList().ForEach(act => ScriptActions.Add(act.Name.ToLower(), new ScriptAction(act, type)));
                }
            }
        }
        #endregion

        /// <summary>
        /// Parses this instance.
        /// </summary>
        /// <returns></returns>
        public void Parse() {
            Callback parse = Parsing;
            parse.BeginInvoke(ParseComplete, parse);
        }

        /// <summary>
        /// Parsings this instance.
        /// </summary>
        private void Parsing() {
            GetScriptActions();
            
            ScriptSections = new List<string>();
            ScriptParams = new Dictionary<string, string>();
            PlayerProperties = ScriptKeywords.GetPlayer();
            CustomItems = ScriptKeywords.GetItems();

            ScriptWayPoints = new Dictionary<Row, LocationKeyword>();
            SetParams = new Dictionary<string, Row>();
            GetParams = new Dictionary<string, Row>();

            AddOutputSeparatorLine();
            AddOutput("Script build start...");
            AddOutputSeparatorLine();

            AddOutput("Obtaining script information...");
            var file = GetFileInfo();
            var haserror = false;
            ClearErrors();

            AddOutput("Start row parsing");
            foreach (Row line in Document) {
                var parsedrow = ParseRow(line, file);
                if (parsedrow != null) {
                    file.Rows.Add(parsedrow);
                    AddOutput(string.Format("Row number ({0})... Ok", line.Index));
                }
                else if (parsedrow == null) {
                    AddOutput(string.Format("Row number ({0})... Has Error(s)!", line.Index));
                    haserror = true;
                }
                if (OnProgressReport != null) { OnProgressReport(this, new NumberEventArgs(line.Index)); }
            }
            CheckScriptWarnings(file);

            AddOutputSeparatorLine();
            AddOutput("Compilation complete!");
            if (haserror) { AddOutput("This script contains error(s)..."); }

            if (!haserror) {
                file.ScriptInfo = new ScriptInfo(TibiaClient, file);
                file.Text = Document.Text;
                ParsedScript = file;
            }
            else { ParsedScript = null; }
        }

        /// <summary>
        /// Parses the complete.
        /// </summary>
        /// <param name="resul">The resul.</param>
        private void ParseComplete(IAsyncResult resul) {
            if (OnParseComplete != null) { OnParseComplete(this, EventArgs.Empty); }
        }

        /// <summary>
        /// Parses the row.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <returns></returns>
        public ScriptLine ParseRow(Row row, ScriptFile file) {
            try {
                var lastindex = LastValidWord(row);
                var words = row.ToList().Where(w => w.Index < lastindex).ToList();

                //Primary Validation
                if (words.Count == 0) { return new ScriptLine(null, row.Index, row.Text, file); }
                if (words.First().Text.StartsWith("/")) { return new ScriptLine(null, row.Index, row.Text, file); }
                if (IsEmptyRow(row)) { return new ScriptLine(null, row.Index, row.Text, file); }
                if (row.Text.StartsWith("#endsection")) { return new ScriptLine(null, row.Index, row.Text, file); }

                //Section Validation
                if (row.Text.StartsWith("#section")) {
                    var sectionName = row.Text.Replace("#section", "").TrimStart(' ').TrimEnd(' ');
                    if (!ScriptSections.Contains(sectionName)) {
                        ScriptSections.Add(sectionName);
                        return new ScriptLine(null, row.Index, row.Text, file);
                    }
                    else { SetError(row, "Duplicated section name."); return null; }
                }

                //Syntax Validation
                if (words.Count < 3) { SetError(row, "Invalid expression."); return null; }
                if (words[1].Text != "(") { SetError(row, "Invalid expression."); return null; }
                if (words.Last().Text != ")") { SetError(row, "Invalid expression."); return null; }

                //Brackets Validation.
                if (CountChars(row.Text, '(') != CountChars(row.Text, ')')) { SetError(row, "You forgot to use the char ')' to close a function. Or otherwise used unnecessarily."); return null; }
                if (CountChars(row.Text, '[') != CountChars(row.Text, ']')) { SetError(row, "You forgot to use the char ']' to close a player property. Or otherwise used unnecessarily."); return null; }
                if (CountChars(row.Text, '{') != CountChars(row.Text, '}')) { SetError(row, "You forgot to use the char '}' to close a custom item. Or otherwise used unnecessarily."); return null; }
                if (CountChars(row.Text, '\"') % 2 != 0) { SetError(row, "You forgot to use the char '\"' to close a text expression. Or otherwise used unnecessarily."); return null; }

                //Null argument validation.
                if (row.Text.IndexOf("((") > -1) { SetError(row, "Invalid expression '(('."); return null; }
                if (row.Text.IndexOf("[[") > -1) { SetError(row, "Invalid expression '[['."); return null; }
                if (row.Text.IndexOf("{{") > -1) { SetError(row, "Invalid expression '{{'."); return null; }
                if (row.Text.IndexOf("[]") > -1) { SetError(row, "Invalid expression '[['."); return null; }
                if (row.Text.IndexOf("{}") > -1) { SetError(row, "Invalid expression '{{'."); return null; }
                if (row.Text.IndexOf(",,") > -1) { SetError(row, "Invalid expression ',,'."); return null; }

                //Function Validation
                if (ScriptActions.ContainsKey(words.First().Text)) {
                    var action = ScriptActions[words.First().Text];
                    var actionline = new ScriptLine(action, row.Index + 1, row.Text, file);
                    var args = RemoveArgumentSpaces(GetFunctionArgument(row.Text).Split(new[] { ',' }));

                    //Argument Validation.
                    if ((action.Params == null || action.Params.Length > 0) && args.Length > 0) {
                        if (args.Length == 0) { SetError(row, "This function does not require arguments."); return null; }
                    }
                    if (action.Params != null && action.Params.Length > 0) {
                        if (args.Length == 0) { SetError(row, "This function requires one or more arguments."); return null; }
                        if (args.Length != action.Params.Length) { SetError(row, "Invalid number of arguments."); return null; }
                        actionline.Args = new string[args.Length];
                        for (int i = 0; i < args.Length; i++) {

                            #region "[rgn] String Argument Valdation "
                            if (action.Params[i].NeedQuotes && args[i].Contains("+")) {
                                var subparams = args[i].Split(new[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
                                foreach (var sp in subparams) {
                                    if (sp.StartsWith("getparam")) {
                                        var spargs = GetFunctionArguments(sp);
                                        if (spargs.Length != 2) {
                                            SetError(row, "Invalid number of arguments to funcion getparam.");
                                            return null;
                                        }
                                        if (!GetParams.ContainsKey(spargs[0])) { GetParams.Add(spargs[0], row); }
                                    }
                                }
                            }
                            else if (!args[i].StartsWith("\"") && action.Params[i].NeedQuotes) {
                                if (!args[i].StartsWith("getparam") && !args[i].StartsWith("getvalue")) {
                                    SetError(row, string.Format("The argument \"{0}\" of this function must be inside quotes. Ex. \"...\"", i.ToString()));
                                    return null;
                                }
                            }
                            actionline.Args[i] = args[i];
                            #endregion
                        }
                    }

                    //Waypoint Validation.
                    if (actionline.FunctionText.ToLower() == "goto") {
                        var wp = new LocationKeyword();
                        if (wp.Parse(GetFunctionArgument(row.Text))) {
                            ScriptWayPoints.Add(row, wp);
                        }
                        else { SetError(row, "Invalid waypoint declaration."); }
                    }

                    //Parameter Validation
                    if (actionline.FunctionText.ToLower() == "setparam") {
                        ScriptParams.Add(actionline.Args[0].ToString().Replace("\"", ""), actionline.Args[1].ToString());
                        if (!SetParams.ContainsKey(actionline.Args[0].ToString().Replace("\"", ""))) {
                            SetParams.Add(actionline.Args[0].ToString().Replace("\"", ""), row);
                        }
                    }
                    foreach (var word in words) {
                        if (word.Text == "getparam") {
                            if (!ScriptParams.ContainsKey(row[word.Index + 3].Text)) {
                                SetError(row[word.Index + 3], string.Format("The parameter \"{0}\" was not found. You must call setparam(\"{0}\", value) before use it.", row[word.Index + 3].Text));
                                return null;
                            }
                            else { if (!GetParams.ContainsKey(row[word.Index + 3].Text)) { GetParams.Add(row[word.Index + 3].Text, row); } }
                        }
                        else if (word.Text == "gotoline") {
                            if (row[word.Index + 2].Text.ToInt32() >= Document.Lines.Length) {
                                SetError(word, string.Format("This script does not have the line \"{0}\".", row[word.Index + 2].Text));
                                return null;
                            }
                        }
                        else if (word.Text == "gotosection") {
                            if (!row.Document.Text.Contains(string.Format("#section {0}", row[word.Index + 2].Text))) {
                                SetError(row[word.Index + 2], string.Format("This script does not contains a section named \"{0}\".", row[word.Index + 2].Text));
                                return null;
                            }
                        }
                        else if (word.Text == "[") {
                            if (PlayerProperties.FindByInputText(row[word.Index + 1].Text.Trim().ToUpper()) == null) {
                                SetError(row[word.Index + 1], string.Format("The player property \"{0}\" does not exist.", row[word.Index + 1].Text));
                                return null;
                            }
                        }
                        else if (word.Text == "{") {
                            if (CustomItems.FindByInputText(row[word.Index + 1].Text.Trim().ToUpper()) == null) {
                                SetError(row[word.Index + 1], string.Format("The custom item \"{0}\" does not exist.", row[word.Index + 1].Text));
                                return null;
                            }
                        }
                    }
                    return actionline;
                }
                SetError(words.First(), string.Format("The function \"{0}\" does not exist.", words.First().Text)); return null;
            }
            catch (Exception) { SetWarning(row, string.Format("Could not parse the row number \"{0}\"", row.Index + 1)); return null; }
        }

        /// <summary>
        /// Checks the script warnings.
        /// </summary>
        public void CheckScriptWarnings(ScriptFile file) {

            //File Verificaton
            AddOutput("Checking script information...");
            if (file.Title == string.Empty) { SetWarning("This script does not have a Title."); }
            if (file.TibiaVersion == string.Empty) { SetWarning("This script does not specifies a tibia version."); }
            if (file.Author == string.Empty) { SetWarning("This script does not have a Author."); }

            //Section Verification
            AddOutput("Checking script sections...");
            if (ScriptSections.Count == 0) { SetWarning("This script contains no sections."); }

            //Params Verification
            AddOutput("Checking script parameters...");
            foreach (var param in SetParams) {
                if (!GetParams.ContainsKey(param.Key)) {
                    //check for getparam in string constants.
                    SetWarning(param.Value, string.Format("The parameter {0} is never used.", param.Key));
                }
            }

            //Waypoint Validation
            AddOutput("Checking script waypoints...");
            if (ScriptWayPoints.Count > 0) {
                var lastwp = ScriptWayPoints.First();
                foreach (var waypoint in ScriptWayPoints) {
                    var wp = waypoint;
                    if (lastwp.Key.Index != wp.Key.Index) {
                        var distx = wp.Value.X - lastwp.Value.X; if (distx < 0) { distx = distx * -1; }
                        var disty = wp.Value.Y - lastwp.Value.Y; if (disty < 0) { disty = disty * -1; }

                        if (distx > 25 || disty > 25) {
                            SetWarning(waypoint.Key, "The distance from the last waypoint is to large.");
                            SetWarning("Try to use waypoints closest to themselves.");
                        }
                        else if (distx == 0 && disty == 0) {
                            if (wp.Value.Z == lastwp.Value.Z && (wp.Key.Index - lastwp.Key.Index) == 1) {
                                SetWarning(waypoint.Key, "This waypoint is duplicated.");
                            }
                        }
                    }
                    lastwp = wp;
                }
            }
            else { SetWarning("This script does not contains waypoints."); }
        }
    }
}
