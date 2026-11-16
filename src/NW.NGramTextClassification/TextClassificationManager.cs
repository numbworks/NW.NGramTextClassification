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

    /// <inheritdoc cref="ITextClassifier"/>
    public class TextClassificationManager : ITextClassificationManager
    {
	
		#region Fields

		private ComponentBag _componentBag;
		private SettingBag _settingBag;

		#endregion
		
		#region Properties

        public static List<LabeledExample> DefaultLabeledExamples { get; } = new List<LabeledExample>()
            {
                new LabeledExample(label: "en", text: "We are looking for several skilled and driven developers to join our team."),
                new LabeledExample(label: "sv", text: "Vår kund erbjuder trivsel, arbetsglädje och en trygg arbetsmiljö.")
            };
        public static List<TextSnippet> DefaultTextSnippets { get; } = new List<TextSnippet>()
            {
                new TextSnippet(text: "We are looking for several skilled and driven developers to join our team."),
                new TextSnippet(text: "Vår kund erbjuder trivsel, arbetsglädje och en trygg arbetsmiljö.")
            };
        public static NGramTokenizerRuleSet DefaultTokenizerRuleSet { get; } = new NGramTokenizerRuleSet();

        public static string DefaultLabeledExamplesFileName { get; } = "LabeledExamples.json";
        public static string DefaultTextSnippetsFileName { get; } = "TextSnippets.json";
        public static string DefaultTokenizerRuleSetFileName { get; } = "TokenizerRuleSet.json";

		#endregion
		
		#region Constructors
		
        /// <summary>Initializes a <see cref="TextClassificationManager"/> instance.</summary>
        public TextClassificationManager(ComponentBag componentBag = null, SettingBag settingBag = null)
        {

			_componentBag = componentBag ?? new ComponentBag();
			_settingBag = settingBag ?? new SettingBag();

        }
		
		#endregion
		
		#region Methods (public)

        public ClassifyOutput Classify(ClassifyInput classifyInput)
        {

            Validator.ValidateObject(classifyInput, nameof(classifyInput));

            LogStartup(classifyInput);

            IFileInfoAdapter labeledExamplesFile = CreateFile(_settingBag.FolderPath, classifyInput.LabeledExamplesFileName);
            List<LabeledExample> labeledExamples = LoadLabeledExamplesOrThrow(labeledExamplesFile);

            _componentBag.LoggingAction(TextClassifications.MessageCollection.XLabeledExamplesHaveBeenLoaded(labeledExamples));

            IFileInfoAdapter textSnippetsFile = CreateFile(_settingBag.FolderPath, classifyInput.TextSnippetsFileName);
            List<TextSnippet> textSnippets = LoadTextSnippetsOrThrow(textSnippetsFile);

            _componentBag.LoggingAction(TextClassifications.MessageCollection.XTextSnippetsHaveBeenLoaded(textSnippets));

            NGramTokenizerRuleSet tokenizerRuleSet = EstablishTokenizerRuleSet(classifyInput);

            _componentBag.LoggingAction(TextClassifications.MessageCollection.TheProvidedIs(nameof(NGramTokenizerRuleSet), tokenizerRuleSet.ToString()));

            if (_settingBag.CleanLabeledExamples)
            {
                List<LabeledExample> removedExamples;
                labeledExamples = _componentBag.TextClassifier.CleanLabeledExamples(labeledExamples, tokenizerRuleSet, out removedExamples);
                LogRemovedLabeledExamples(cleanExamples: labeledExamples, removedExamples: removedExamples);
            }

            TextClassifierContext textClassifierContext = new TextClassifierContext(
                tokenizerRuleSet: tokenizerRuleSet,
                labeledExamples: labeledExamples,
                minimumAccuracySingleLabel: _settingBag.MinimumAccuracySingleLabel,
                minimumAccuracyMultipleLabels: _settingBag.MinimumAccuracyMultipleLabels
            );

            ClassifyOutput classifyOutput = _componentBag.TextClassifier.ClassifyMany(
                textSnippets: textSnippets, 
                textClassifierContext: textClassifierContext);

            LogTextClassificationResults(classifyOutput);

            if (_settingBag.SaveOutput)
            {
                _componentBag.TextClassifier.SaveClassifyOutput(classifyOutput, _settingBag.FolderPath, _settingBag.DisableIndexSerialization);
                _componentBag.LoggingAction(TextClassifications.MessageCollection.ClassifyOutputSuccessfullySaved(_settingBag.FolderPath));
            }

            _componentBag.LoggingAction(TextClassifications.MessageCollection.TextClassificationProcessCompleted);

            return classifyOutput;

        }
        public ClassifyOutput ClassifyOrNull(ClassifyInput classifyInput)
        {

            try
            {

                return Classify(classifyInput);

            }
            catch (Exception e)
            {
                
                LogException(e);
                return null;

            }

        }
        
        public void Scaffold()
        {
            
            _componentBag.LoggingAction(TextClassifications.MessageCollection.ScaffoldingProcessStarted);

            IFileInfoAdapter labeledExamplesFile = new FileInfoAdapter(Path.Combine(_settingBag.FolderPath, DefaultLabeledExamplesFileName));
            IFileInfoAdapter textSnippetsFile = new FileInfoAdapter(Path.Combine(_settingBag.FolderPath, DefaultTextSnippetsFileName));
            IFileInfoAdapter tokenizerRuleSetFile = new FileInfoAdapter(Path.Combine(_settingBag.FolderPath, DefaultTokenizerRuleSetFileName));

            _componentBag.TextClassifier.Save(DefaultLabeledExamples, labeledExamplesFile);
            // Log json content (?)
            _componentBag.LoggingAction(TextClassifications.MessageCollection.FollowingExampleJSONFileSaved(labeledExamplesFile));

            _componentBag.TextClassifier.Save(DefaultTextSnippets, textSnippetsFile);
            // Log json content (?)
            _componentBag.LoggingAction(TextClassifications.MessageCollection.FollowingExampleJSONFileSaved(textSnippetsFile));

            _componentBag.TextClassifier.Save(DefaultTokenizerRuleSet, tokenizerRuleSetFile);
            // Log json content (?)
            _componentBag.LoggingAction(TextClassifications.MessageCollection.FollowingExampleJSONFileSaved(tokenizerRuleSetFile));

            _componentBag.LoggingAction(TextClassifications.MessageCollection.ScaffoldingProcessCompleted);

        }
        public void Scaffold(out bool success)
        {

            try
            {

                Scaffold();
                success = true;

            }
            catch (Exception e)
            {
                
                LogException(e);
                success = false;

            }

        }

        #endregion

        #region Methods (private)

        private IFileInfoAdapter CreateFile(string folderPath, string fileName)
        {
            
			string filePath = Path.Combine(folderPath, fileName);
            IFileInfoAdapter file = _componentBag.FileManager.Create(filePath);

			return file;

        }
        private List<LabeledExample> LoadLabeledExamplesOrThrow(IFileInfoAdapter labeledExamplesFile)
        {

            List<LabeledExample> labeledExamples = _componentBag.TextClassifier.LoadLabeledExamplesOrDefault(labeledExamplesFile);
			
            if (labeledExamples == Serializer<LabeledExample>.Default)
                throw new Exception(TextClassifications.MessageCollection.LoadingFileNameReturnedDefault(labeledExamplesFile.FullName));

            return labeledExamples;

        }
        private List<TextSnippet> LoadTextSnippetsOrThrow(IFileInfoAdapter textSnippetsFile)
        {

            List<TextSnippet> textSnippets = _componentBag.TextClassifier.LoadTextSnippetsOrDefault(textSnippetsFile);

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

            NGramTokenizerRuleSet tokenizerRuleset = _componentBag.TextClassifier.LoadTokenizerRuleSetOrDefault(tokenizerRuleSetFile);

            if (tokenizerRuleset == default(NGramTokenizerRuleSet))
                throw new Exception(TextClassifications.MessageCollection.LoadingFileNameReturnedDefault(tokenizerRuleSetFile.FullName));

            return tokenizerRuleset;

        }

        private void LogStartup(ClassifyInput classifyInput)
        {

            _componentBag.LoggingAction(TextClassifications.MessageCollection.TextClassificationProcessStarted);

            _componentBag.LoggingAction(TextClassifications.MessageCollection.TheProvidedIs(nameof(SettingBag.FolderPath), _settingBag.FolderPath));
            _componentBag.LoggingAction(TextClassifications.MessageCollection.TheProvidedIs(nameof(SettingBag.MinimumAccuracySingleLabel), _settingBag.MinimumAccuracySingleLabel.ToString()));
            _componentBag.LoggingAction(TextClassifications.MessageCollection.TheProvidedIs(nameof(SettingBag.MinimumAccuracyMultipleLabels), _settingBag.MinimumAccuracyMultipleLabels.ToString()));
            _componentBag.LoggingAction(TextClassifications.MessageCollection.TheProvidedIs(nameof(SettingBag.SaveOutput), _settingBag.SaveOutput.ToString()));
            _componentBag.LoggingAction(TextClassifications.MessageCollection.TheProvidedIs(nameof(SettingBag.CleanLabeledExamples), _settingBag.CleanLabeledExamples.ToString()));
            _componentBag.LoggingAction(TextClassifications.MessageCollection.TheProvidedIs(nameof(SettingBag.DisableIndexSerialization), _settingBag.DisableIndexSerialization.ToString()));
            _componentBag.LoggingAction(TextClassifications.MessageCollection.TheProvidedIs(nameof(SettingBag.EnableUTC), _settingBag.EnableUTC.ToString()));

            _componentBag.LoggingAction(TextClassifications.MessageCollection.TheProvidedIs(nameof(ClassifyInput.LabeledExamplesFileName), classifyInput.LabeledExamplesFileName));
            _componentBag.LoggingAction(TextClassifications.MessageCollection.TheProvidedIs(nameof(ClassifyInput.TextSnippetsFileName), classifyInput.TextSnippetsFileName));

            if (!string.IsNullOrEmpty(classifyInput.TokenizerRuleSetFileName))
                _componentBag.LoggingAction(TextClassifications.MessageCollection.TheProvidedIs(nameof(ClassifyInput.TokenizerRuleSetFileName), classifyInput.TokenizerRuleSetFileName));
        
        }
        private void LogRemovedLabeledExamples(List<LabeledExample> cleanExamples, List<LabeledExample> removedExamples)
        {

            _componentBag.LoggingAction(TextClassifications.MessageCollection.LabeledExamplesHaveBeenThruCleaningProcess(cleanExamples));
            _componentBag.LoggingAction(TextClassifications.MessageCollection.LabeledExamplesHaveBeenRemoved(removedExamples));

            foreach (LabeledExample removedExample in removedExamples)
                _componentBag.LoggingAction(TextClassifications.MessageCollection.RemovedLabeledExample(removedExample));

        }
        private void LogTextClassificationResults(ClassifyOutput classifyOutput)
        {

            _componentBag.LoggingAction(TextClassifications.MessageCollection.TextClassificationOutputSuccessfullyCreated);
            _componentBag.LoggingAction(TextClassifications.MessageCollection.TextClassificationOutputContainsResults);

            foreach (TextClassifierResult result in classifyOutput.Results)
                _componentBag.LoggingAction(result.ToString());

        }        
        private void LogException(Exception e)
        {

            _componentBag.LoggingAction(_componentBag.ErrorFormattingFunction(e.Message));

            if (e.InnerException != null)
                _componentBag.LoggingAction(_componentBag.ErrorFormattingFunction(e.InnerException.Message));

        }

		#endregion
	
	}
}