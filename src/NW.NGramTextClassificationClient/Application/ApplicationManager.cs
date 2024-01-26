using System;
using NW.NGramTextClassification.Validation;
using McMaster.Extensions.CommandLineUtils;
using NW.NGramTextClassificationClient.Shared;
using NW.NGramTextClassificationClient.ApplicationSession;

namespace NW.NGramTextClassificationClient.Application
{
    /// <inheritdoc cref="IApplicationManager"/>
    public class ApplicationManager : IApplicationManager
    {

        #region Fields

        private ILibraryBroker _libraryBroker;
        private ApplicationSections _sections;

        #endregion

        #region Properties
        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="ApplicationManager"/> instance.</summary>
        /// <exception cref="ArgumentNullException"/>
        public ApplicationManager
            (ILibraryBroker libraryBroker, IApplicationSectionsFactory sectionsFactory, DependencyBag dependencyBag)
        {

            Validator.ValidateObject(libraryBroker, nameof(libraryBroker));
            Validator.ValidateObject(sectionsFactory, nameof(sectionsFactory));
            Validator.ValidateObject(dependencyBag, nameof(dependencyBag));

            _libraryBroker = libraryBroker;
            _sections = sectionsFactory.Create(libraryBroker, dependencyBag);

        }

        /// <summary>Initializes a <see cref="ApplicationManager"/> instance using default parameters.</summary>
        public ApplicationManager()
            : this(new LibraryBroker(), new ApplicationSectionsFactory(), new DependencyBag()) { }

        #endregion

        #region Methods_public

        public int Execute(params string[] args)
        {

            CommandLineApplication app = Create();

            return app.Execute(args);

        }

        #endregion

        #region Methods_private

        private CommandLineApplication Create()
        {

            CommandLineApplication app = new CommandLineApplication
            {

                Name = Shared.MessageCollection.Application_Name,
                Description = Shared.MessageCollection.Application_Description

            };

            app = AddRoot(app);
            app = _sections.AboutManager.Add(app);
            app = _sections.SessionManager.Add(app);

            app.HelpOption(inherited: true);

            return app;

        }
        private CommandLineApplication AddRoot(CommandLineApplication app)
        {

            app.OnExecute(() =>
            {

                int exitCode = _libraryBroker.ShowHeader();
                app.ShowHelp();

                return exitCode;

            });

            return app;

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 26.01.2024
*/