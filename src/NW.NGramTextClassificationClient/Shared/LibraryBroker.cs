using System;
using System.IO;
using System.Collections.Generic;
using NW.NGramTextClassification;
using NW.NGramTextClassification.Files;
using NW.NGramTextClassification.Validation;
using NW.NGramTextClassification.LabeledExamples;
using NW.NGramTextClassification.TextSnippets;
using NW.NGramTextClassification.Serializations;
using NW.NGramTextClassification.NGramTokenization;
using NW.NGramTextClassification.TextClassifications;
using NW.NGramTextClassification.Bags;

namespace NW.NGramTextClassificationClient.Shared
{
    /// <inheritdoc cref="ILibraryBroker"/>
    public class LibraryBroker : ILibraryBroker
    {

        #region Fields

        private IComponentBagFactory _componentBagFactory { get; }
        private ISettingBagFactory _settingBagFactory { get; }
        private ITextClassifierFactory _textClassifierFactory { get; }

        #endregion

        #region Properties

        public static int Success { get; } = ((int)ExitCodes.Success);
        public static int Failure { get; } = ((int)ExitCodes.Failure);
        public static string SeparatorLine { get; } = string.Empty;
        public static Func<string, string> ErrorMessageFormatter = (message) => $"ERROR: {message}";

        public static NGramTokenizerRuleSet DefaultTokenizerRuleSet { get; } = new NGramTokenizerRuleSet();

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="LibraryBroker"/> instance.</summary>
        /// <exception cref="ArgumentNullException"/>
        public LibraryBroker
            (IComponentBagFactory componentBagFactory, ISettingBagFactory settingBagFactory, ITextClassifierFactory textClassifierFactory)
        {

            Validator.ValidateObject(componentBagFactory, nameof(componentBagFactory));
            Validator.ValidateObject(settingBagFactory, nameof(settingBagFactory));
            Validator.ValidateObject(textClassifierFactory, nameof(textClassifierFactory));

            _componentBagFactory = componentBagFactory;
            _settingBagFactory = settingBagFactory;
            _textClassifierFactory = textClassifierFactory;

        }

        /// <summary>Initializes a <see cref="LibraryBroker"/> instance using default parameters.</summary>
        public LibraryBroker()
            : this(new ComponentBagFactory(), new SettingBagFactory(), new TextClassifierFactory()) { }

        #endregion

        #region Methods_public

        public int ShowHeader()
        {

            ComponentBag componentBag = _componentBagFactory.Create();
            SettingBag settingBag = _settingBagFactory.Create();
            TextClassifier textClassifier = _textClassifierFactory.Create(componentBag, settingBag);

            ShowHeader(componentBag, textClassifier);

            return Success;

        }
        public int RunAboutMain()
        {

            ComponentBag componentBag = _componentBagFactory.Create();
            SettingBag settingBag = _settingBagFactory.Create();
            TextClassifier textClassifier = _textClassifierFactory.Create(componentBag, settingBag);

            ShowHeader(componentBag, textClassifier);

            componentBag.LoggingActionAsciiBanner(Shared.MessageCollection.Application_Description);
            componentBag.LoggingActionAsciiBanner(SeparatorLine);

            componentBag.LoggingActionAsciiBanner(Shared.MessageCollection.About_Information_Author);
            componentBag.LoggingActionAsciiBanner(Shared.MessageCollection.About_Information_Email);
            componentBag.LoggingActionAsciiBanner(Shared.MessageCollection.About_Information_Url);
            componentBag.LoggingActionAsciiBanner(Shared.MessageCollection.About_Information_License);

            ShowFooter(componentBag);

            return Success;

        }
        public int RunSessionClassify(ClassifyData classifyData)
        {

            try
            {

                Validator.ValidateObject(classifyData, nameof(classifyData));

                classifyData = Defaultize(classifyData);

                ComponentBag componentBag = _componentBagFactory.Create();
                SettingBag settingBag = _settingBagFactory.Create(classifyData);
                TextClassifier textClassifier = _textClassifierFactory.Create(componentBag, settingBag);

                ShowHeader(componentBag, textClassifier);

                List<LabeledExample> labeledExamples = LoadLabeledExamplesOrThrow(classifyData, textClassifier);
                List<TextSnippet> textSnippets = LoadTextSnippetsOrThrow(classifyData, textClassifier);

                NGramTokenizerRuleSet tokenizerRuleSet;
                if (string.IsNullOrWhiteSpace(classifyData.TokenizerRuleSet))
                    tokenizerRuleSet = DefaultTokenizerRuleSet;
                else
                    tokenizerRuleSet = LoadTokenizerRuleSetOrThrow(classifyData, textClassifier);

                if (classifyData.CleanLabeledExamples)
                    labeledExamples = textClassifier.CleanLabeledExamples(labeledExamples, tokenizerRuleSet);

                TextClassifierSession session = textClassifier.ClassifyMany(textSnippets, tokenizerRuleSet, labeledExamples);

                if (classifyData.SaveSession)
                    textClassifier.SaveSession(session, classifyData.FolderPath, classifyData.DisableIndexSerialization);

                ShowFooter(componentBag);

                return Success;

            }
            catch (Exception e)
            {

                return LogAndReturnFailure(e);

            }
        
        }

        #endregion

        #region Methods_private

        private int LogAndReturnFailure(Exception e)
        {

            ComponentBag componentBag = _componentBagFactory.Create();

            componentBag.LoggingAction(ErrorMessageFormatter(e.Message));
            if (e.InnerException != null)
                componentBag.LoggingAction(ErrorMessageFormatter(e.InnerException.Message));

            ShowFooter(componentBag);

            return Failure;

        }
        private void ShowHeader(ComponentBag componentBag, TextClassifier textClassifier)
        {

            componentBag.LoggingActionAsciiBanner(SeparatorLine);
            componentBag.LoggingActionAsciiBanner(textClassifier.AsciiBanner);
            componentBag.LoggingActionAsciiBanner(SeparatorLine);

        }
        private void ShowFooter(ComponentBag componentBag)
        {

            componentBag.LoggingActionAsciiBanner(SeparatorLine);

        }

        private ClassifyData Defaultize(ClassifyData classifyData)
        {

            ClassifyData updated = new ClassifyData(
                    labeledExamples: classifyData.LabeledExamples,
                    textSnippets: classifyData.TextSnippets,
                    folderPath: classifyData.FolderPath ?? SettingBag.DefaultFolderPath,
                    tokenizerRuleSet: classifyData.TokenizerRuleSet,
                    minAccuracySingle: classifyData.MinAccuracySingle ?? SettingBag.DefaultMinimumAccuracySingleLabel,
                    minAccuracyMultiple: classifyData.MinAccuracyMultiple ?? SettingBag.DefaultMinimumAccuracyMultipleLabels,
                    saveSession: classifyData.SaveSession,
                    cleanLabeledExamples: classifyData.CleanLabeledExamples,
                    disableIndexSerialization: classifyData.DisableIndexSerialization
                );

            return updated;

        }
        private List<LabeledExample> LoadLabeledExamplesOrThrow(ClassifyData classifyData, TextClassifier textClassifier)
        {

            string filePath = Path.Combine(classifyData.FolderPath, classifyData.LabeledExamples);
            IFileInfoAdapter file = textClassifier.Convert(filePath);

            List<LabeledExample> labeledExamples = textClassifier.LoadLabeledExamplesOrDefault(file);
            if (labeledExamples == Serializer<LabeledExample>.Default)
                throw new Exception(MessageCollection.LoadingFileNameReturnedDefault(classifyData.LabeledExamples));

            return labeledExamples;

        }
        private List<TextSnippet> LoadTextSnippetsOrThrow(ClassifyData classifyData, TextClassifier textClassifier)
        {

            string filePath = Path.Combine(classifyData.FolderPath, classifyData.TextSnippets);
            IFileInfoAdapter file = textClassifier.Convert(filePath);

            List<TextSnippet> textSnippets = textClassifier.LoadTextSnippetsOrDefault(file);
            if (textSnippets == Serializer<TextSnippet>.Default)
                throw new Exception(MessageCollection.LoadingFileNameReturnedDefault(classifyData.TextSnippets));

            return textSnippets;

        }
        private NGramTokenizerRuleSet LoadTokenizerRuleSetOrThrow(ClassifyData classifyData, TextClassifier textClassifier)
        {

            string filePath = Path.Combine(classifyData.FolderPath, classifyData.TokenizerRuleSet);
            IFileInfoAdapter file = textClassifier.Convert(filePath);

            NGramTokenizerRuleSet tokenizerRuleset = textClassifier.LoadTokenizerRuleSetOrDefault(file);
            if (tokenizerRuleset == default(NGramTokenizerRuleSet))
                throw new Exception(MessageCollection.LoadingFileNameReturnedDefault(classifyData.TokenizerRuleSet));

            return tokenizerRuleset;

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 26.01.2024
*/