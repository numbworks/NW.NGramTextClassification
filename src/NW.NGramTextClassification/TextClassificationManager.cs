using System;
using System.Collections.Generic;
using System.IO;
using NW.NGramTextClassification.Bags;
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
		private SettingBag _settingBag;

		#endregion
		
		#region Properties

		public static NGramTokenizerRuleSet DefaultTokenizerRuleSet { get; } = new NGramTokenizerRuleSet();

		#endregion
		
		#region Constructors
		
        /// <summary>Initializes a <see cref="TextClassificationManager"/> instance.</summary>
        public TextClassificationManager(IFileManager fileManager = null, ITextClassifier textClassifier = null, SettingBag settingBag = null)
        {

            _fileManager = fileManager ?? new FileManager();
			_textClassifier = textClassifier ?? new TextClassifier();
			_settingBag = settingBag;

        }
		
		#endregion
		
		#region Methods (public)

		public TextClassifierSession Classify(ClassifyInput classifyInput)
        {

            Validator.ValidateObject(classifyInput, nameof(classifyInput));

            IFileInfoAdapter labeledExamplesFile = CreateFile(_settingBag.FolderPath, classifyInput.LabeledExamplesFileName);
            List<LabeledExample> labeledExamples = LoadLabeledExamplesOrThrow(labeledExamplesFile);

            IFileInfoAdapter textSnippetsFile = CreateFile(_settingBag.FolderPath, classifyInput.TextSnippetsFileName);
            List<TextSnippet> textSnippets = LoadTextSnippetsOrThrow(textSnippetsFile);

            NGramTokenizerRuleSet tokenizerRuleSet = EstablishTokenizerRuleSet(classifyInput);

			if (_settingBag.CleanLabeledExamples)
				labeledExamples = _textClassifier.CleanLabeledExamples(labeledExamples, tokenizerRuleSet);

			TextClassifierSession session 
				= _textClassifier.ClassifyMany(
					textSnippets: textSnippets, 
					tokenizerRuleSet: tokenizerRuleSet, 
					labeledExamples: labeledExamples, 
					minimumAccuracySingleLabel: _settingBag.MinimumAccuracySingleLabel, 
					minimumAccuracyMultipleLabels: _settingBag.MinimumAccuracyMultipleLabels);

			if (_settingBag.SaveSession)
				_textClassifier.SaveSession(session, _settingBag.FolderPath, _settingBag.DisableIndexSerialization);

			return session;

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
                IFileInfoAdapter tokenizerRuleSetFile = CreateFile(_settingBag.FolderPath, classifyInput.TokenizerRuleSetFileName);
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