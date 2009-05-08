namespace Keyrox.SourceCode.SyntaxDocumentParsers
{
    public abstract class ParserBase : IParser
    {
        #region IParser Members

        public SyntaxDocument Document { get; set; }
        public SyntaxDefinition SyntaxDefinition { get; set; }
        public string Separators { get; set; }

        public void Init(string syntaxDefinitionPath)
        {
            if (!syntaxDefinitionPath.ToLowerInvariant().EndsWith(".syn"))
                syntaxDefinitionPath += ".syn";

            var loader = new SyntaxDefinitionLoader();
            SyntaxDefinition syntax = loader.Load(syntaxDefinitionPath);
            Init(syntax);
        }

        public void Init(string syntaxDefinitionPath, string separators)
        {
            Separators = separators;
            Init(syntaxDefinitionPath);
        }

        public void Init(SyntaxDefinition syntaxDefinition)
        {
            SyntaxDefinition = syntaxDefinition;
        }

        public abstract void ParseRow(int rowIndex, bool parseKeywords);


        public void ParsePreviewLine(int rowIndex)
        {
            Row Row = Document[rowIndex];
            Row.Clear();
            Row.Add(Row.Text);
            Row.RowState = RowState.NotParsed;
        }

        #endregion
    }
}