using System;
using NW.Shared.Validation;
using NW.NGramTextClassification.CLI.Shared;
using McMaster.Extensions.CommandLineUtils;

namespace NW.NGramTextClassification.CLI.ApplicationAbout
{
    /// <inheritdoc cref="IAboutManager"/>
    public class AboutManager : IAboutManager
    {

        #region Fields

        private ILibraryBroker _libraryBroker;

        #endregion

        #region Properties
        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="AboutManager"/> instance.</summary>
        /// <exception cref="ArgumentNullException"/>
        public AboutManager(ILibraryBroker libraryBroker)
        {

            Validator.ValidateObject(libraryBroker, nameof(libraryBroker));

            _libraryBroker = libraryBroker;

        }

        #endregion

        #region Methods_public

        public CommandLineApplication Add(CommandLineApplication app)
        {

            Validator.ValidateObject(app, nameof(app));

            app.Command(Shared.MessageCollection.COMMAND_ABOUT_NAME, command =>
            {

                command = AddMain(command);

            });

            return app;

        }

        #endregion

        #region Methods_private

        private CommandLineApplication AddMain(CommandLineApplication app)
        {

            app.Description = Shared.MessageCollection.COMMAND_ABOUT_DESCR;
            app.OnExecute(() =>
            {

                return _libraryBroker.RunAboutMain();

            });

            return app;

        }

        #endregion

    }
}