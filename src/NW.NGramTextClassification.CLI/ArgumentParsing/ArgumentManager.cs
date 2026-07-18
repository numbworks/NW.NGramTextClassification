using System;
using System.Collections.Generic;
using System.IO;
using NW.NGramTextClassification.Bags;
using NW.NGramTextClassification.LabeledExamples;
using NW.NGramTextClassification.NGramTokenization;
using NW.NGramTextClassification.TextSnippets;
using NW.NGramTextClassification.TextClassifications;
using NW.NGramTextClassification.CLI.AsciiBanners;
using NW.NGramTextClassification.CLI.TerminalWindows;
using NW.Shared.Files;
using NW.Shared.Serialization;
using McMaster.Extensions.CommandLineUtils;
using McMaster.Extensions.CommandLineUtils.Validation;

namespace NW.NGramTextClassification.CLI.ArgumentParsing
{
    /// <inheritdoc cref="IArgumentManager"/>
    public class ArgumentManager : IArgumentManager
    {

        #region Fields

        private ComponentBag _componentBag { get; }

        #endregion

        #region Properties

        public static int Success { get; } = ((int)ExitCodes.Success);
        public static int Failure { get; } = ((int)ExitCodes.Failure);
        public static string SeparatorLine { get; } = string.Empty;
        public static Func<string, string> ErrorMessageFormatter = (message) => $"ERROR: {message}";
        public static NGramTokenizerRuleSet DefaultTokenizerRuleSet { get; } = new NGramTokenizerRuleSet();

        public IDoubleManager DoubleManager { get; }
        public IOptionValidator MinimumAccuracyValidator { get; }
        public IAsciiBannerManager AsciiBannerManager { get; }
        public ITerminalWindowManager TerminalWindowManager { get; }
        public ITextClassifierFactory TextClassifierFactory { get; }
        public IComponentBagFactory ComponentBagFactory { get; }
        public ISettingBagFactory SettingBagFactory { get; }

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="ArgumentManager"/> instance.</summary>
        /// <exception cref="ArgumentNullException"/>
        public ArgumentManager(
            IDoubleManager doubleManager = null,
            IAsciiBannerManager asciiBannerManager = null,
            ITerminalWindowManager terminalWindowManager = null,
            ITextClassifierFactory textClassifierFactory = null,
            IComponentBagFactory componentBagFactory = null,
            ISettingBagFactory settingBagFactory = null)
        {
            
            DoubleManager = doubleManager ?? new DoubleManager();
            MinimumAccuracyValidator = new MinimumAccuracyValidator(DoubleManager);
            AsciiBannerManager = asciiBannerManager ?? new AsciiBannerManager();
            TerminalWindowManager = terminalWindowManager ?? new TerminalWindowManager();
            TextClassifierFactory = textClassifierFactory ?? new TextClassifierFactory();
            ComponentBagFactory = componentBagFactory ?? new ComponentBagFactory();
            SettingBagFactory = settingBagFactory ?? new SettingBagFactory();

            _componentBag = ComponentBagFactory.Create();

        }
        #endregion

        #region Methods (public)

        public CommandLineApplication Create()
        {

            CommandLineApplication app = new CommandLineApplication
            {

                Name = CommandLineString.APPLICATION_NAME,
                Description = CommandLineString.APPLICATION_DESCRIPTION

            };

            app = AddRoot(app);
            app = AddAbout(app);
            app = AddSession(app);

            app.HelpOption(inherited: true);

            return app;

        }

        #endregion

        #region Methods (private)

        private CommandLineApplication AddRoot(CommandLineApplication app)
        {

            app.OnExecute(() =>
            {

                int exitCode = ShowHeader();
                app.ShowHelp();

                return exitCode;

            });

            return app;

        }

        private CommandLineApplication AddAbout(CommandLineApplication app)
        {

            app.Command(CommandLineString.COMMAND_ABOUT_NAME, command =>
            {

                command = AddAboutMain(command);

            });

            return app;

        }
        private CommandLineApplication AddAboutMain(CommandLineApplication app)
        {

            app.Description = CommandLineString.COMMAND_ABOUT_DESCR;
            app.OnExecute(RunAboutMain);

            return app;

        }

        private CommandLineApplication AddSession(CommandLineApplication app)
        {

            app.Command(CommandLineString.COMMAND_SESSION_NAME, sessionCommand =>
            {

                sessionCommand = AddSessionMain(sessionCommand);
                sessionCommand = AddSessionClassify(sessionCommand);

            });

            return app;

        }
        private CommandLineApplication AddSessionMain(CommandLineApplication command)
        {

            command.Description = CommandLineString.COMMAND_SESSION_DESCR;
            command.OnExecute(() =>
            {

                int exitCode = ShowHeader();
                command.ShowHelp();

                return exitCode;

            });

            return command;

        }
        private CommandLineApplication AddSessionClassify(CommandLineApplication command)
        {

            command.Command(CommandLineString.SUBCOMMAND_CLASSIFY_NAME, subCommand =>
            {

                subCommand.Description = CommandLineString.SUBCOMMAND_CLASSIFY_DESCR;

                CommandOption labeledExamplesOption = CreateLabeledExamplesOption(subCommand);
                CommandOption textSnippetsOption = CreateTextSnippetsOption(subCommand);
                CommandOption folderPathOption = CreateFolderPathOption(subCommand);
                CommandOption tokenizerRuleSetOption = CreateTokenizerRuleSetOption(subCommand);
                CommandOption minAccuracySingleOption = CreateMinAccuracySingleOption(subCommand);
                CommandOption minAccuracyMultipleOption = CreateMinAccuracyMultipleOption(subCommand);
                CommandOption saveSessionOption = CreateSaveSessionOption(subCommand);
                CommandOption cleanLabeledExamplesOption = CreateCleanLabeledExamplesOption(subCommand);
                CommandOption disableIndexSerializationOption = CreateDisableIndexSerializationOption(subCommand);

                subCommand.OnExecute(() =>
                {

                    ClassifyData classifyData
                        = new ClassifyData(
                                labeledExamples: labeledExamplesOption.Value(),
                                textSnippets: textSnippetsOption.Value(),
                                folderPath: folderPathOption.Value(),
                                tokenizerRuleSet: tokenizerRuleSetOption.Value(),
                                minAccuracySingle: DoubleManager.ParseOrDefault(minAccuracySingleOption.Value()),
                                minAccuracyMultiple: DoubleManager.ParseOrDefault(minAccuracyMultipleOption.Value()),
                                saveSession: saveSessionOption.HasValue(),
                                cleanLabeledExamples: cleanLabeledExamplesOption.HasValue(),
                                disableIndexSerialization: disableIndexSerializationOption.HasValue()
                        );

                    return RunSessionClassify(classifyData);

                });

            });

            return command;

        }

        private CommandOption CreateLabeledExamplesOption(CommandLineApplication subCommand)
        {

            CommandOption result
                = subCommand
                    .Option(
                        CommandLineString.OPTION_LABELEDEXAMPLES_TEMPL,
                        CommandLineString.OPTION_LABELEDEXAMPLES_DESCR,
                        CommandOptionType.SingleValue)
                    .IsRequired(
                        false,
                        CommandLineString.OPTION_LABELEDEXAMPLES_ERRORMESSAGE);

            return result;

        }
        private CommandOption CreateTextSnippetsOption(CommandLineApplication subCommand)
        {

            CommandOption result
                = subCommand
                    .Option(
                        CommandLineString.OPTION_TEXTSNIPPETS_TEMPL,
                        CommandLineString.OPTION_TEXTSNIPPETS_DESCR,
                        CommandOptionType.SingleValue)
                    .IsRequired(
                        false,
                        CommandLineString.OPTION_TEXTSNIPPETS_ERRORMESSAGE);

            return result;

        }
        private CommandOption CreateFolderPathOption(CommandLineApplication subCommand)
        {

            return subCommand
                    .Option(
                        CommandLineString.OPTION_FOLDERPATH_TEMPL,
                        CommandLineString.OPTION_FOLDERPATH_DESCR,
                        CommandOptionType.SingleValue)
                    .Accepts(validator => validator.ExistingDirectory());

        }
        private CommandOption CreateTokenizerRuleSetOption(CommandLineApplication subCommand)
        {

            CommandOption result
                = subCommand
                    .Option(
                        CommandLineString.OPTION_TOKENIZERRULESET_TEMPL,
                        CommandLineString.OPTION_TOKENIZERRULESET_DESCR,
                        CommandOptionType.SingleValue);

            return result;

        }
        private CommandOption CreateMinAccuracySingleOption(CommandLineApplication subCommand)
        {

            return subCommand
                    .Option(
                        CommandLineString.OPTION_MINACCURACYSINGLE_TEMPL,
                        CommandLineString.OPTION_MINACCURACYSINGLE_DESCR,
                        CommandOptionType.SingleValue)
                    .Accepts(validator => validator.Use(MinimumAccuracyValidator));

        }
        private CommandOption CreateMinAccuracyMultipleOption(CommandLineApplication subCommand)
        {

            return subCommand
                    .Option(
                        CommandLineString.OPTION_MINACCURACYMULTIPLE_TEMPL,
                        CommandLineString.OPTION_MINACCURACYMULTIPLE_DESCR,
                        CommandOptionType.SingleValue)
                    .Accepts(validator => validator.Use(MinimumAccuracyValidator));

        }
        private CommandOption CreateSaveSessionOption(CommandLineApplication subCommand)
        {

            return subCommand
                    .Option(
                        CommandLineString.OPTION_SAVESESSION_TEMPL,
                        CommandLineString.OPTION_SAVESESSION_DESCR,
                        CommandOptionType.NoValue);

        }
        private CommandOption CreateCleanLabeledExamplesOption(CommandLineApplication subCommand)
        {

            return subCommand
                    .Option(
                        CommandLineString.OPTION_CLEANLABELEDEXAMPLES_TEMPL,
                        CommandLineString.OPTION_CLEANLABELEDEXAMPLES_DESCR,
                        CommandOptionType.NoValue);

        }
        private CommandOption CreateDisableIndexSerializationOption(CommandLineApplication subCommand)
        {

            return subCommand
                    .Option(
                        CommandLineString.OPTION_DISABLEINDEXSERIALIZATION_TEMPL,
                        CommandLineString.OPTION_DISABLEINDEXSERIALIZATION_DESCR,
                        CommandOptionType.NoValue);

        }

        private void LogAsciiBanner()
        {

            string version = _componentBag.VersionFunction();
            uint terminalWidth = TerminalWindowManager.GetOrCutoff();
            string asciiBanner = AsciiBannerManager.Create(version, terminalWidth);

            _componentBag.LoggingActionAsciiBanner(SeparatorLine);
            _componentBag.LoggingActionAsciiBanner(asciiBanner);
            _componentBag.LoggingActionAsciiBanner(SeparatorLine);

        }
        private void LogFooter()
            => _componentBag.LoggingActionAsciiBanner(SeparatorLine);
        private void LogAbout()
        {
            
            _componentBag.LoggingActionAsciiBanner(CommandLineString.APPLICATION_DESCRIPTION);
            _componentBag.LoggingActionAsciiBanner(SeparatorLine);
            _componentBag.LoggingActionAsciiBanner(CommandLineString.COMMAND_ABOUT_INFO_AUTHOR);
            _componentBag.LoggingActionAsciiBanner(CommandLineString.COMMAND_ABOUT_INFO_EMAIL);
            _componentBag.LoggingActionAsciiBanner(CommandLineString.COMMAND_ABOUT_INFO_URL);
            _componentBag.LoggingActionAsciiBanner(CommandLineString.COMMAND_ABOUT_INFO_LICENSE);

        }     
        private int LogAndReturnFailure(Exception e)
        {

            _componentBag.LoggingAction(ErrorMessageFormatter(e.Message));

            if (e.InnerException != null)
                _componentBag.LoggingAction(ErrorMessageFormatter(e.InnerException.Message));

            LogFooter();

            return Failure;

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
                throw new Exception(Messages.MessageCollection.LoadingFileNameReturnedDefault(classifyData.LabeledExamples));

            return labeledExamples;

        }
        private List<TextSnippet> LoadTextSnippetsOrThrow(ClassifyData classifyData, TextClassifier textClassifier)
        {

            string filePath = Path.Combine(classifyData.FolderPath, classifyData.TextSnippets);
            IFileInfoAdapter file = textClassifier.Convert(filePath);

            List<TextSnippet> textSnippets = textClassifier.LoadTextSnippetsOrDefault(file);
            if (textSnippets == Serializer<TextSnippet>.Default)
                throw new Exception(Messages.MessageCollection.LoadingFileNameReturnedDefault(classifyData.TextSnippets));

            return textSnippets;

        }
        private NGramTokenizerRuleSet LoadTokenizerRuleSetOrThrow(ClassifyData classifyData, TextClassifier textClassifier)
        {

            string filePath = Path.Combine(classifyData.FolderPath, classifyData.TokenizerRuleSet);
            IFileInfoAdapter file = textClassifier.Convert(filePath);

            NGramTokenizerRuleSet tokenizerRuleset = textClassifier.LoadTokenizerRuleSetOrDefault(file);
            if (tokenizerRuleset == default(NGramTokenizerRuleSet))
                throw new Exception(Messages.MessageCollection.LoadingFileNameReturnedDefault(classifyData.TokenizerRuleSet));

            return tokenizerRuleset;

        }

        private int ShowHeader()
        {
            
            LogAsciiBanner();

            return Success;

        }
        private int RunAboutMain()
        {
            
            LogAsciiBanner();
            LogAbout();
            LogFooter();

            return Success;

        }
        private int RunSessionClassify(ClassifyData classifyData)
        {
            
            try
            {

                LogAsciiBanner();

                classifyData = Defaultize(classifyData);

                SettingBag settingBag = SettingBagFactory.Create(classifyData);
                TextClassifier textClassifier = TextClassifierFactory.Create(_componentBag, settingBag);

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

                LogFooter();

                return Success;

            }
            catch (Exception e)
            {

                return LogAndReturnFailure(e);

            }

        }

        #endregion

    }
}