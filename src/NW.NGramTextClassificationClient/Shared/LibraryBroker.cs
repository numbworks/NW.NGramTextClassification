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

namespace NW.NGramTextClassificationClient.Shared
{
    /// <inheritdoc cref="ILibraryBroker"/>
    public class LibraryBroker : ILibraryBroker
    {

        #region Fields

        private ITextClassifierComponentsFactory _componentsFactory { get; }
        private ITextClassifierSettingsFactory _settingsFactory { get; }
        private ITextClassifierFactory _textClassifierFactory { get; }

        #endregion

        #region Properties

        public static int Success { get; } = ((int)ExitCodes.Success);
        public static int Failure { get; } = ((int)ExitCodes.Failure);
        public static string SeparatorLine { get; } = string.Empty;
        public static Func<string, string> ErrorMessageFormatter = (message) => $"ERROR: {message}";

        public NGramTokenizerRuleSet DefaultTokenizerRuleSet { get; } = new NGramTokenizerRuleSet();

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="LibraryBroker"/> instance.</summary>
        /// <exception cref="ArgumentNullException"/>
        public LibraryBroker
            (ITextClassifierComponentsFactory componentsFactory, ITextClassifierSettingsFactory settingsFactory, ITextClassifierFactory textClassifierFactory)
        {

            Validator.ValidateObject(componentsFactory, nameof(componentsFactory));
            Validator.ValidateObject(settingsFactory, nameof(settingsFactory));
            Validator.ValidateObject(textClassifierFactory, nameof(textClassifierFactory));

            _componentsFactory = componentsFactory;
            _settingsFactory = settingsFactory;
            _textClassifierFactory = textClassifierFactory;

        }

        /// <summary>Initializes a <see cref="LibraryBroker"/> instance using default parameters.</summary>
        public LibraryBroker()
            : this(new TextClassifierComponentsFactory(), new TextClassifierSettingsFactory(), new TextClassifierFactory()) { }

        #endregion

        #region Methods_public

        public int ShowHeader()
        {

            TextClassifierComponents components = _componentsFactory.Create();
            TextClassifierSettings settings = _settingsFactory.Create();
            TextClassifier textClassifier = _textClassifierFactory.Create(components, settings);

            ShowHeader(components, textClassifier);

            return Success;

        }
        public int RunAboutMain()
        {

            TextClassifierComponents components = _componentsFactory.Create();
            TextClassifierSettings settings = _settingsFactory.Create();
            TextClassifier textClassifier = _textClassifierFactory.Create(components, settings);

            ShowHeader(components, textClassifier);

            components.LoggingActionAsciiBanner(Shared.MessageCollection.Application_Description);
            components.LoggingActionAsciiBanner(SeparatorLine);

            components.LoggingActionAsciiBanner(Shared.MessageCollection.About_Information_Author);
            components.LoggingActionAsciiBanner(Shared.MessageCollection.About_Information_Email);
            components.LoggingActionAsciiBanner(Shared.MessageCollection.About_Information_Url);
            components.LoggingActionAsciiBanner(Shared.MessageCollection.About_Information_License);

            ShowFooter(components);

            return Success;

        }
        public int RunSessionClassify(ClassifyData classifyData)
        {

            try
            {

                Validator.ValidateObject(classifyData, nameof(classifyData));

                classifyData = Defaultize(classifyData);

                TextClassifierComponents components = new TextClassifierComponents();
                TextClassifierSettings settings = Create(classifyData);
                TextClassifier textClassifier = Create(components, settings);

                ShowHeader(components, textClassifier);

                List<LabeledExample> labeledExamples = LoadLabeledExamplesOrThrow(classifyData, textClassifier);
                List<TextSnippet> textSnippets = LoadTextSnippetsOrThrow(classifyData, textClassifier);

                NGramTokenizerRuleSet tokenizerRuleSet;
                if (string.IsNullOrWhiteSpace(classifyData.TokenizerRuleSet))
                    tokenizerRuleSet = DefaultTokenizerRuleSet;
                else
                    tokenizerRuleSet = LoadTokenizerRuleSetOrThrow(classifyData, textClassifier);

                TextClassifierSession session = textClassifier.ClassifyMany(textSnippets, tokenizerRuleSet, labeledExamples);

                if (classifyData.SaveSession)
                    textClassifier.SaveSession(session, classifyData.FolderPath);

                ShowFooter(components);

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

            TextClassifierComponents components = _componentsFactory.Create();

            components.LoggingAction(ErrorMessageFormatter(e.Message));
            if (e.InnerException != null)
                components.LoggingAction(ErrorMessageFormatter(e.InnerException.Message));

            ShowFooter(components);

            return Failure;

        }
        private void ShowHeader(TextClassifierComponents components, TextClassifier textClassifier)
        {

            components.LoggingActionAsciiBanner(SeparatorLine);
            components.LoggingActionAsciiBanner(textClassifier.AsciiBanner);
            components.LoggingActionAsciiBanner(SeparatorLine);

        }
        private void ShowFooter(TextClassifierComponents components)
        {

            components.LoggingActionAsciiBanner(SeparatorLine);

        }

        private ClassifyData Defaultize(ClassifyData classifyData)
        {

            ClassifyData updated = new ClassifyData(
                    labeledExamples: classifyData.LabeledExamples,
                    textSnippets: classifyData.TextSnippets,
                    folderPath: classifyData.FolderPath ?? TextClassifierSettings.DefaultFolderPath,
                    tokenizerRuleSet: classifyData.TokenizerRuleSet,
                    minAccuracySingle: classifyData.MinAccuracySingle ?? TextClassifierSettings.DefaultMinimumAccuracySingleLabel,
                    minAccuracyMultiple: classifyData.MinAccuracyMultiple ?? TextClassifierSettings.DefaultMinimumAccuracyMultipleLabels,
                    saveSession: classifyData.SaveSession
                );

            return updated;

        }

        private TextClassifierSettings Create(ClassifyData classifyData)
        {

            TextClassifierSettings settings = new TextClassifierSettings(

                  truncateTextInLogMessagesAfter: TextClassifierSettings.DefaultTruncateTextInLogMessagesAfter,
                  minimumAccuracySingleLabel: classifyData.MinAccuracySingle ?? TextClassifierSettings.DefaultMinimumAccuracySingleLabel,
                  minimumAccuracyMultipleLabels: classifyData.MinAccuracyMultiple ?? TextClassifierSettings.DefaultMinimumAccuracyMultipleLabels,
                  folderPath: classifyData.FolderPath ?? TextClassifierSettings.DefaultFolderPath

                );

            return settings;

        }
        private TextClassifier Create(TextClassifierComponents components, TextClassifierSettings settings)
        {

            TextClassifier textClassifier = new TextClassifier(components, settings);

            return textClassifier;

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
    Last Update: 22.10.2022
*/