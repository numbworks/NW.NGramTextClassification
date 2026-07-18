using System;
using NW.Shared.Validation;
using NW.NGramTextClassification.CLI.Shared;
using McMaster.Extensions.CommandLineUtils;

namespace NW.NGramTextClassification.CLI.ApplicationSession
{
    /// <inheritdoc cref="ISessionManager"/>
    public class SessionManager : ISessionManager
    {

        #region Fields

        private ILibraryBroker _libraryBroker;
        private SessionManagerBag _sessionManagerBag;

        #endregion

        #region Properties
        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="SessionManager"/> instance.</summary>
        /// <exception cref="ArgumentNullException"/>
        public SessionManager(ILibraryBroker libraryBroker, SessionManagerBag sessionManagerBag)
        {

            Validator.ValidateObject(libraryBroker, nameof(libraryBroker));
            Validator.ValidateObject(sessionManagerBag, nameof(sessionManagerBag));

            _libraryBroker = libraryBroker;
            _sessionManagerBag = sessionManagerBag;

        }

        #endregion

        #region Methods_public

        public CommandLineApplication Add(CommandLineApplication app)
        {

            Validator.ValidateObject(app, nameof(app));

            app.Command(Shared.MessageCollection.COMMAND_SESSION_NAME, sessionCommand =>
            {

                sessionCommand = AddMain(sessionCommand);
                sessionCommand = AddClassify(sessionCommand);

            });

            return app;

        }

        #endregion

        #region Methods_private

        private CommandLineApplication AddMain(CommandLineApplication command)
        {

            command.Description = Shared.MessageCollection.COMMAND_SESSION_DESCR;
            command.OnExecute(() =>
            {

                int exitCode = _libraryBroker.ShowHeader();
                command.ShowHelp();

                return exitCode;

            });

            return command;

        }
        private CommandLineApplication AddClassify(CommandLineApplication command)
        {

            command.Command(Shared.MessageCollection.SUBCOMMAND_CLASSIFY_NAME, subCommand =>
            {

                subCommand.Description = Shared.MessageCollection.SUBCOMMAND_CLASSIFY_DESCR;

                CommandOption labeledExamplesOption = CreateRequiredLabeledExamplesOption(subCommand);
                CommandOption textSnippetsOption = CreateRequiredTextSnippetsOption(subCommand);
                CommandOption folderPathOption = CreateOptionalFolderPathOption(subCommand);
                CommandOption tokenizerRuleSetOption = CreateOptionalTokenizerRuleSetOption(subCommand);
                CommandOption minAccuracySingleOption = CreateOptionalMinAccuracySingleOption(subCommand);
                CommandOption minAccuracyMultipleOption = CreateOptionalMinAccuracyMultipleOption(subCommand);
                CommandOption saveSessionOption = CreateOptionalSaveSessionOption(subCommand);
                CommandOption cleanLabeledExamplesOption = CreateOptionalCleanLabeledExamplesOption(subCommand);
                CommandOption disableIndexSerializationOption = CreateOptionalDisableIndexSerializationOption(subCommand);

                subCommand.OnExecute(() =>
                {

                    ClassifyData classifyData
                        = new ClassifyData(
                                labeledExamples: labeledExamplesOption.Value(),
                                textSnippets: textSnippetsOption.Value(),
                                folderPath: folderPathOption.Value(),
                                tokenizerRuleSet: tokenizerRuleSetOption.Value(),
                                minAccuracySingle: _sessionManagerBag.DoubleManager.ParseOrDefault(minAccuracySingleOption.Value()),
                                minAccuracyMultiple: _sessionManagerBag.DoubleManager.ParseOrDefault(minAccuracyMultipleOption.Value()),
                                saveSession: saveSessionOption.HasValue(),
                                cleanLabeledExamples: cleanLabeledExamplesOption.HasValue(),
                                disableIndexSerialization: disableIndexSerializationOption.HasValue()
                        );

                    return _libraryBroker.RunSessionClassify(classifyData);

                });

            });

            return command;

        }

        private CommandOption CreateRequiredLabeledExamplesOption(CommandLineApplication subCommand)
        {

            CommandOption result
                = subCommand
                    .Option(
                        Shared.MessageCollection.OPTION_LABELEDEXAMPLES_TEMPL,
                        Shared.MessageCollection.OPTION_LABELEDEXAMPLES_DESCR,
                        CommandOptionType.SingleValue)
                    .IsRequired(
                        false,
                        Shared.MessageCollection.OPTION_LABELEDEXAMPLES_ERRORMESSAGE);

            return result;

        }
        private CommandOption CreateRequiredTextSnippetsOption(CommandLineApplication subCommand)
        {

            CommandOption result
                = subCommand
                    .Option(
                        Shared.MessageCollection.OPTION_TEXTSNIPPETS_TEMPL,
                        Shared.MessageCollection.OPTION_TEXTSNIPPETS_DESCR,
                        CommandOptionType.SingleValue)
                    .IsRequired(
                        false,
                        Shared.MessageCollection.OPTION_TEXTSNIPPETS_ERRORMESSAGE);

            return result;

        }
        private CommandOption CreateOptionalFolderPathOption(CommandLineApplication subCommand)
        {

            return subCommand
                    .Option(
                        Shared.MessageCollection.OPTION_FOLDERPATH_TEMPL,
                        Shared.MessageCollection.OPTION_FOLDERPATH_DESCR,
                        CommandOptionType.SingleValue)
                    .Accepts(validator => validator.ExistingDirectory());

        }
        private CommandOption CreateOptionalTokenizerRuleSetOption(CommandLineApplication subCommand)
        {

            CommandOption result
                = subCommand
                    .Option(
                        Shared.MessageCollection.OPTION_TOKENIZERRULESET_TEMPL,
                        Shared.MessageCollection.OPTION_TOKENIZERRULESET_DESCR,
                        CommandOptionType.SingleValue);

            return result;

        }
        private CommandOption CreateOptionalMinAccuracySingleOption(CommandLineApplication subCommand)
        {

            return subCommand
                    .Option(
                        Shared.MessageCollection.OPTION_MINACCURACYSINGLE_TEMPL,
                        Shared.MessageCollection.OPTION_MINACCURACYSINGLE_DESCR,
                        CommandOptionType.SingleValue)
                    .Accepts(validator => validator.Use(_sessionManagerBag.MinimumAccuracyValidator));

        }
        private CommandOption CreateOptionalMinAccuracyMultipleOption(CommandLineApplication subCommand)
        {

            return subCommand
                    .Option(
                        Shared.MessageCollection.OPTION_MINACCURACYMULTIPLE_TEMPL,
                        Shared.MessageCollection.OPTION_MINACCURACYMULTIPLE_DESCR,
                        CommandOptionType.SingleValue)
                    .Accepts(validator => validator.Use(_sessionManagerBag.MinimumAccuracyValidator));

        }
        private CommandOption CreateOptionalSaveSessionOption(CommandLineApplication subCommand)
        {

            return subCommand
                    .Option(
                        Shared.MessageCollection.OPTION_SAVESESSION_TEMPL,
                        Shared.MessageCollection.OPTION_SAVESESSION_DESCR,
                        CommandOptionType.NoValue);

        }
        private CommandOption CreateOptionalCleanLabeledExamplesOption(CommandLineApplication subCommand)
        {

            return subCommand
                    .Option(
                        Shared.MessageCollection.OPTION_CLEANLABELEDEXAMPLES_TEMPL,
                        Shared.MessageCollection.OPTION_CLEANLABELEDEXAMPLES_DESCR,
                        CommandOptionType.NoValue);

        }
        private CommandOption CreateOptionalDisableIndexSerializationOption(CommandLineApplication subCommand)
        {

            return subCommand
                    .Option(
                        Shared.MessageCollection.OPTION_DISABLEINDEXSERIALIZATION_TEMPL,
                        Shared.MessageCollection.OPTION_DISABLEINDEXSERIALIZATION_DESCR,
                        CommandOptionType.NoValue);

        }

        #endregion

    }
}