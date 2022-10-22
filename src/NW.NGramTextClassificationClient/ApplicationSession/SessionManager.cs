using System;
using NW.NGramTextClassification.Validation;
using NW.NGramTextClassificationClient.Shared;
using McMaster.Extensions.CommandLineUtils;

namespace NW.NGramTextClassificationClient.ApplicationSession
{
    /// <inheritdoc cref="ISessionManager"/>
    public class SessionManager : ISessionManager
    {

        #region Fields

        private ILibraryBroker _libraryBroker;
        private SessionManagerComponents _sessionManagerComponents;

        #endregion

        #region Properties
        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="SessionManager"/> instance.</summary>
        /// <exception cref="ArgumentNullException"/>
        public SessionManager(ILibraryBroker libraryBroker, SessionManagerComponents sessionManagerComponents)
        {

            Validator.ValidateObject(libraryBroker, nameof(libraryBroker));
            Validator.ValidateObject(sessionManagerComponents, nameof(sessionManagerComponents));

            _libraryBroker = libraryBroker;
            _sessionManagerComponents = sessionManagerComponents;

        }

        #endregion

        #region Methods_public

        public CommandLineApplication Add(CommandLineApplication app)
        {

            Validator.ValidateObject(app, nameof(app));

            app.Command(Shared.MessageCollection.Session_Name, sessionCommand =>
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

            command.Description = Shared.MessageCollection.Session_Description;
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

            command.Command(Shared.MessageCollection.Session_Classify_Name, subCommand =>
            {

                subCommand.Description = Shared.MessageCollection.Session_Classify_Description;

                CommandOption labeledExamplesOption = CreateRequiredLabeledExamplesOption(subCommand);
                CommandOption textSnippetsOption = CreateRequiredTextSnippetsOption(subCommand);
                CommandOption folderPathOption = CreateOptionalFolderPathOption(subCommand);
                CommandOption tokenizerRuleSetOption = CreateOptionalTokenizerRuleSetOption(subCommand);
                CommandOption minAccuracySingleOption = CreateOptionalMinAccuracySingleOption(subCommand);
                CommandOption minAccuracyMultipleOption = CreateOptionalMinAccuracyMultipleOption(subCommand);
                CommandOption saveSessionOption = CreateOptionalSaveSessionOption(subCommand);

                subCommand.OnExecute(() =>
                {

                    ClassifyData classifyData
                        = new ClassifyData(
                                labeledExamples: labeledExamplesOption.Value(),
                                textSnippets: textSnippetsOption.Value(),
                                folderPath: folderPathOption.Value(),
                                tokenizerRuleSet: tokenizerRuleSetOption.Value(),
                                minAccuracySingle: _sessionManagerComponents.DoubleManager.Parse(minAccuracySingleOption.Value()),
                                minAccuracyMultiple: _sessionManagerComponents.DoubleManager.Parse(minAccuracyMultipleOption.Value()),
                                saveSession: saveSessionOption.HasValue()
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
                        Shared.MessageCollection.Session_Option_LabeledExamples_Template,
                        Shared.MessageCollection.Session_Option_LabeledExamples_Description,
                        CommandOptionType.SingleValue)
                    .IsRequired(
                        false,
                        Shared.MessageCollection.Session_Option_LabeledExamples_ErrorMessage);

            return result;

        }
        private CommandOption CreateRequiredTextSnippetsOption(CommandLineApplication subCommand)
        {

            CommandOption result
                = subCommand
                    .Option(
                        Shared.MessageCollection.Session_Option_TextSnippets_Template,
                        Shared.MessageCollection.Session_Option_TextSnippets_Description,
                        CommandOptionType.SingleValue)
                    .IsRequired(
                        false,
                        Shared.MessageCollection.Session_Option_TextSnippets_ErrorMessage);

            return result;

        }
        private CommandOption CreateOptionalFolderPathOption(CommandLineApplication subCommand)
        {

            return subCommand
                    .Option(
                        Shared.MessageCollection.Session_Option_FolderPath_Template,
                        Shared.MessageCollection.Session_Option_FolderPath_Description,
                        CommandOptionType.SingleValue)
                    .Accepts(validator => validator.ExistingDirectory());

        }
        private CommandOption CreateOptionalTokenizerRuleSetOption(CommandLineApplication subCommand)
        {

            CommandOption result
                = subCommand
                    .Option(
                        Shared.MessageCollection.Session_Option_TokenizerRuleSet_Template,
                        Shared.MessageCollection.Session_Option_TokenizerRuleSet_Description,
                        CommandOptionType.SingleValue);

            return result;

        }
        private CommandOption CreateOptionalSaveSessionOption(CommandLineApplication subCommand)
        {

            return subCommand
                    .Option(
                        Shared.MessageCollection.Session_Option_SaveSession_Template,
                        Shared.MessageCollection.Session_Option_SaveSession_Description,
                        CommandOptionType.NoValue);

        }
        private CommandOption CreateOptionalMinAccuracySingleOption(CommandLineApplication subCommand)
        {

            return subCommand
                    .Option(
                        Shared.MessageCollection.Session_Option_MinAccuracySingle_Template,
                        Shared.MessageCollection.Session_Option_MinAccuracySingle_Description,
                        CommandOptionType.SingleValue)
                    .Accepts(validator => validator.Use(_sessionManagerComponents.MinimumAccuracyValidator));

        }
        private CommandOption CreateOptionalMinAccuracyMultipleOption(CommandLineApplication subCommand)
        {

            return subCommand
                    .Option(
                        Shared.MessageCollection.Session_Option_MinAccuracyMultiple_Template,
                        Shared.MessageCollection.Session_Option_MinAccuracyMultiple_Description,
                        CommandOptionType.SingleValue)
                    .Accepts(validator => validator.Use(_sessionManagerComponents.MinimumAccuracyValidator));

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 22.10.2022
*/