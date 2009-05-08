using System;
using System.Linq;
using System.Reflection;
using Keyrox.Scripting.Attributes;
using Keyrox.Scripting.Keywords;
using Keyrox.Shared.Reflection;

namespace Keyrox.Scripting.Parser {
    public class ScriptAction {

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptAction"/> class.
        /// </summary>
        /// <param name="method">The method.</param>
        public ScriptAction(MethodInfo method, Type type) {
            this.Method = method;
            this.ActionClass = type;
            this.Title = method.GetAttribute<ActionTitle>();
            this.Config = method.GetAttribute<ActionConfig>();
            this.Examples = method.GetAttribute<ActionExamples>();
            this.Params = method.GetAttributes<ActionParameter>();
            if (this.Params != null) { this.Params = (from pr in this.Params orderby pr.Index ascending select pr).ToArray(); }
        }

        /// <summary>
        /// Gets or sets the method.
        /// </summary>
        /// <value>The method.</value>
        public MethodInfo Method { get; set; }

        /// <summary>
        /// Gets or sets the action class.
        /// </summary>
        /// <value>The action class.</value>
        public Type ActionClass { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public ActionTitle Title { get; set; }

        /// <summary>
        /// Gets or sets the config.
        /// </summary>
        /// <value>The config.</value>
        public ActionConfig Config { get; set; }

        /// <summary>
        /// Gets or sets the params.
        /// </summary>
        /// <value>The params.</value>
        public ActionParameter[] Params { get; set; }

        /// <summary>
        /// Gets or sets the examples.
        /// </summary>
        /// <value>The examples.</value>
        public ActionExamples Examples { get; set; }

        /// <summary>
        /// Gets the keyword.
        /// </summary>
        /// <returns></returns>
        public Keyword GetKeyword() {
            var key = new Keyword();
            if (Title != null) {
                key.Title = Title.Title;
                key.Description = Title.Description;
            }
            if (Config != null) {
                key.AutoListType = Config.AutoList;
                key.CaretPosition = Config.CarretPosition;
                key.ImageIndex = Config.ImageIndex;
                key.InputText = Config.InputText;
            }
            if (Params != null) {
                var oparams = from param in Params orderby param.Index ascending select param;
                oparams.ToList().ForEach(p => key.Params += string.Format("{0}: {1}{2}", p.Name, p.Description, Environment.NewLine));
            }
            if (Examples != null) {
                key.Examples = Examples.GetFormatedExamples();
            }
            return key;
        }

    }
}
