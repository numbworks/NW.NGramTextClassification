namespace NW.NGramTextClassification.TextClassifications
{

	/// <summary>Collects all the data required by the <c>classify</c> sub-command.</summary>
    public class ClassifyInput
    {
	
		#region Fields
		#endregion
		
		#region Properties

		public string FolderPath { get; }
        public string LabeledExamplesFileName { get; }
        public string TextSnippetsFileName { get; }
		public string TokenizerRuleSetFileName { get; }

		public const string DefaultTokenizerRuleSetFileName = "";

		#endregion
		
		#region Constructors

		/// <summary>Initializes a <see cref="ClassifyInput"/> instance.</summary>
		public ClassifyInput(
			string folderPath, 
			string labeledExamplesFileName, 
			string textSnippetsFileName, 
			string tokenizerRuleSetFileName = DefaultTokenizerRuleSetFileName)
        {
            
			FolderPath = folderPath;
			LabeledExamplesFileName = labeledExamplesFileName;
			TextSnippetsFileName = textSnippetsFileName;
			TokenizerRuleSetFileName = tokenizerRuleSetFileName;

        }

		#endregion
		
		#region Methods (public)
		#endregion
		
		#region Methods (private)
		#endregion
	
	}
}