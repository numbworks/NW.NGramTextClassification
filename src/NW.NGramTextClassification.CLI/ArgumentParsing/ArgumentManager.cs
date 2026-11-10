using System;
using NW.NGramTextClassification.Bags;
using NW.NGramTextClassification.TextClassifiers;
using NW.NGramTextClassification.CLI.AsciiBanners;
using NW.NGramTextClassification.CLI.TerminalWindows;
using McMaster.Extensions.CommandLineUtils;
using McMaster.Extensions.CommandLineUtils.Validation;
using McMaster.Extensions.CommandLineUtils.HelpText;
using NW.NGramTextClassification.TextClassifications;

namespace NW.NGramTextClassification.CLI.ArgumentParsing
{

    /// <inheritdoc cref="IArgumentManager"/>
    public class ArgumentManager : IArgumentManager
    {

        #region Fields
        #endregion

        #region Properties

        public static int Success { get; } = ((int)ExitCodes.Success);
        public static int Failure { get; } = ((int)ExitCodes.Failure);
        public static string SeparatorLine { get; } = string.Empty;
        public static Func<string, string> ErrorMessageFormatter = (message) => $"ERROR: {message}";
        public static Action<string> DefaultLoggingActionAsciiBanner { get; }
            = (message) => Console.WriteLine($"{message}");

        public IDoubleManager DoubleManager { get; }
        public IOptionValidator MinimumAccuracyValidator { get; }
        public IAsciiBannerManager AsciiBannerManager { get; }
        public ITerminalWindowManager TerminalWindowManager { get; }
        public ITextClassificationManagerFactory TextClassificationManagerFactory { get; }
        public Action<string> LoggingActionAsciiBanner { get; }

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="ArgumentManager"/> instance.</summary>
        /// <exception cref="ArgumentNullException"/>
        public ArgumentManager(
            IDoubleManager doubleManager = null,
            IAsciiBannerManager asciiBannerManager = null,
            ITerminalWindowManager terminalWindowManager = null,
            ITextClassificationManagerFactory textClassificationManagerFactory = null,
            Action<string> loggingActionAsciiBanner = null
            )
        {
            
            DoubleManager = doubleManager ?? new DoubleManager();
            MinimumAccuracyValidator = new MinimumAccuracyValidator(DoubleManager);
            AsciiBannerManager = asciiBannerManager ?? new AsciiBannerManager();
            TerminalWindowManager = terminalWindowManager ?? new TerminalWindowManager();
            TextClassificationManagerFactory = textClassificationManagerFactory ?? new TextClassificationManagerFactory();
            LoggingActionAsciiBanner = loggingActionAsciiBanner ?? DefaultLoggingActionAsciiBanner;

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
            app = AddSession(app);

            app.HelpOption(inherited: true);

            AddCustomHelpTextGenerator(app);
            AddValidationErrorHandling(app);

            return app;

        }

        #endregion

        #region Methods (private)

        private CommandLineApplication AddRoot(CommandLineApplication app)
        {

            app.OnExecute(() =>
            {

                app.ShowHelp();

                return Success;

            });

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

                command.ShowHelp();

                return Success;

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
                CommandOption enableUTCOption = CreateEnableUTCOption(subCommand);

                subCommand.OnExecute(() =>
                {

                    SettingBag settingBag = new SettingBag(
                        folderPath: folderPathOption.Value(),
                        minimumAccuracySingleLabel: DoubleManager.ParseOrDefault(minAccuracySingleOption.Value()),
                        minimumAccuracyMultipleLabels: DoubleManager.ParseOrDefault(minAccuracyMultipleOption.Value()),
                        saveSession: saveSessionOption.HasValue(),
                        cleanLabeledExamples: cleanLabeledExamplesOption.HasValue(),
                        disableIndexSerialization: disableIndexSerializationOption.HasValue(),
                        enableUTC: enableUTCOption.HasValue()
                    );

                    ClassifyInput classifyInput = new ClassifyInput(
                        labeledExamplesFileName: labeledExamplesOption.Value(),
                        textSnippetsFileName: textSnippetsOption.Value(),
                        tokenizerRuleSetFileName: tokenizerRuleSetOption.Value() ?? string.Empty
                    );

                    return RunSessionClassify(settingBag, classifyInput);

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
        private CommandOption CreateEnableUTCOption(CommandLineApplication subCommand)
        {

            return subCommand
                    .Option(
                        CommandLineString.OPTION_ENABLEUTC_TEMPL,
                        CommandLineString.OPTION_ENABLEUTC_DESCR,
                        CommandOptionType.NoValue);

        }

        private string CreateAsciiBanner()
        {

            string version = ArgumentParsing.TextClassificationManagerFactory.DefaultVersionFunction();
            uint terminalWidth = TerminalWindowManager.GetOrCutoff();
            string asciiBanner = AsciiBannerManager.Create(version, terminalWidth);

            return asciiBanner;
        
        }
        private void LogAsciiBanner()
        {

            string asciiBanner = CreateAsciiBanner();

            LoggingActionAsciiBanner(SeparatorLine);
            LoggingActionAsciiBanner(asciiBanner);
            LoggingActionAsciiBanner(SeparatorLine);

        }

        private void AddCustomHelpTextGenerator(CommandLineApplication command, IHelpTextGenerator helpTextGenerator)
        {

            command.HelpTextGenerator = helpTextGenerator;
            
            foreach (CommandLineApplication subCommand in command.Commands)
                AddCustomHelpTextGenerator(subCommand, helpTextGenerator);

        }
        private void AddCustomHelpTextGenerator(CommandLineApplication app)
        {

            CustomHelpTextGenerator customHelpTextGenerator = new CustomHelpTextGenerator(CreateAsciiBanner);
            app.HelpTextGenerator = customHelpTextGenerator;

            foreach (CommandLineApplication command in app.Commands)
                AddCustomHelpTextGenerator(command, customHelpTextGenerator);
        
        }
        private void AddValidationErrorHandling(CommandLineApplication app)
        {

            app.OnValidationError(validationResult =>
            {

                ShowHeader();
                
                LoggingActionAsciiBanner(ErrorMessageFormatter(validationResult.ErrorMessage));
                
                return Failure;
                
            });

            foreach (CommandLineApplication command in app.Commands)
                AddValidationErrorHandling(command);

        }        

        private int ShowHeader()
        {
            
            LogAsciiBanner();

            return Success;

        }
        private int RunSessionClassify(SettingBag settingBag, ClassifyInput classifyInput)
        {
            
            LogAsciiBanner();

            ITextClassificationManager textClassificationManager 
                = TextClassificationManagerFactory.Create(settingBag: settingBag, out _);

            textClassificationManager.Classify(classifyInput);

            return Success;

        }

        #endregion

    }

}