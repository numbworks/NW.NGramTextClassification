using System;
using NW.NGramTextClassification.Validation;
using NW.NGramTextClassificationClient.ApplicationAbout;
using NW.NGramTextClassificationClient.ApplicationSession;
using NW.NGramTextClassificationClient.Shared;

namespace NW.NGramTextClassificationClient.Application
{
    /// <inheritdoc cref="IApplicationSectionsFactory"/>
    public class ApplicationSectionsFactory : IApplicationSectionsFactory
    {

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="ApplicationSectionsFactory"/> instance.</summary>
        public ApplicationSectionsFactory() { }

        #endregion

        #region Methods_public

        public ApplicationSections Create(ILibraryBroker libraryBroker, SessionManagerBag sessionManagerBag)
        {

            Validator.ValidateObject(libraryBroker, nameof(libraryBroker));
            Validator.ValidateObject(sessionManagerBag, nameof(sessionManagerBag));

            IAboutManager aboutManager = new AboutManager(libraryBroker);
            ISessionManager sessionManager = new SessionManager(libraryBroker, sessionManagerBag);

            ApplicationSections sections
                = new ApplicationSections(
                            aboutManager: aboutManager,
                            sessionManager: sessionManager
                        );

            return sections;

        }

        #endregion

        #region Methods_private
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 27.09.2022
*/