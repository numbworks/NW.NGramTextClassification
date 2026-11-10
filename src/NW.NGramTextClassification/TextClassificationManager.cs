using System;
using System.Collections.Generic;
using System.IO;
using NW.NGramTextClassification.LabeledExamples;
using NW.NGramTextClassification.NGramTokenization;
using NW.NGramTextClassification.TextClassifications;
using NW.NGramTextClassification.TextClassifiers;
using NW.NGramTextClassification.TextSnippets;
using NW.Shared.Files;
using NW.Shared.Serialization;
using NW.Shared.Validation;

namespace NW.NGramTextClassification
{

    public class TextClassificationManager
    {
	
		#region Fields

        private IFileManager _fileManager;
		private ITextClassifier _textClassifier;

		#endregion
		
		#region Properties

		public static NGramTokenizerRuleSet DefaultTokenizerRuleSet { get; } = new NGramTokenizerRuleSet();

		#endregion
		
		#region Constructors
		
        /// <summary>Initializes a <see cref="TextClassificationManager"/> instance.</summary>
        public TextClassificationManager(IFileManager fileManager = null, ITextClassifier textClassifier = null)
        {

            _fileManager = fileManager ?? new FileManager();
			_textClassifier = textClassifier ?? new TextClassifier();

        }
		
		#endregion
		
		#region Methods (public)

		public ClassifyOutput Classify(ClassifyInput classifyInput)
        {

            Validator.ValidateObject(classifyInput, nameof(classifyInput));

            IFileInfoAdapter labeledExamplesFile = CreateFile(classifyInput.FolderPath, classifyInput.LabeledExamplesFileName);
            List<LabeledExample> labeledExamples = LoadLabeledExamplesOrThrow(labeledExamplesFile);

            IFileInfoAdapter textSnippetsFile = CreateFile(classifyInput.FolderPath, classifyInput.TextSnippetsFileName);
            List<TextSnippet> textSnippets = LoadTextSnippetsOrThrow(textSnippetsFile);

            NGramTokenizerRuleSet tokenizerRuleSet = EstablishTokenizerRuleSet(classifyInput);

            throw new NotImplementedException();

        }



        #endregion

        #region Methods (private)

        private IFileInfoAdapter CreateFile(string folderPath, string fileName)
        {
            
			string filePath = Path.Combine(folderPath, fileName);
            IFileInfoAdapter file = _fileManager.Create(filePath);

			return file;

        }
        private List<LabeledExample> LoadLabeledExamplesOrThrow(IFileInfoAdapter labeledExamplesFile)
        {

            List<LabeledExample> labeledExamples = _textClassifier.LoadLabeledExamplesOrDefault(labeledExamplesFile);
			
            if (labeledExamples == Serializer<LabeledExample>.Default)
                throw new Exception(TextClassifications.MessageCollection.LoadingFileNameReturnedDefault(labeledExamplesFile.FullName));

            return labeledExamples;

        }
        private List<TextSnippet> LoadTextSnippetsOrThrow(IFileInfoAdapter textSnippetsFile)
        {

            List<TextSnippet> textSnippets = _textClassifier.LoadTextSnippetsOrDefault(textSnippetsFile);

            if (textSnippets == Serializer<TextSnippet>.Default)
                throw new Exception(TextClassifications.MessageCollection.LoadingFileNameReturnedDefault(textSnippetsFile.FullName));

            return textSnippets;

        }
        
        private NGramTokenizerRuleSet EstablishTokenizerRuleSet(ClassifyInput classifyInput)
        {

            NGramTokenizerRuleSet tokenizerRuleSet;

            if (string.IsNullOrWhiteSpace(classifyInput.TokenizerRuleSetFileName))
                tokenizerRuleSet = DefaultTokenizerRuleSet;
            else
            {
                IFileInfoAdapter tokenizerRuleSetFile = CreateFile(classifyInput.FolderPath, classifyInput.TokenizerRuleSetFileName);
                tokenizerRuleSet = LoadTokenizerRuleSetOrThrow(tokenizerRuleSetFile);
            }

			return tokenizerRuleSet;
			
        }		
		private NGramTokenizerRuleSet LoadTokenizerRuleSetOrThrow(IFileInfoAdapter tokenizerRuleSetFile)
        {

            NGramTokenizerRuleSet tokenizerRuleset = _textClassifier.LoadTokenizerRuleSetOrDefault(tokenizerRuleSetFile);

            if (tokenizerRuleset == default(NGramTokenizerRuleSet))
                throw new Exception(TextClassifications.MessageCollection.LoadingFileNameReturnedDefault(tokenizerRuleSetFile.FullName));

            return tokenizerRuleset;

        }

		#endregion
	
	}
}