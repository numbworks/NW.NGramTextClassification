using System;
using NW.NGramTextClassificationClient.ApplicationAbout;
using NW.NGramTextClassificationClient.ApplicationSession;
using NW.NGramTextClassification.Validation;

namespace NW.NGramTextClassificationClient.Application
{
    /// <summary>Collects all the dependencies required by <see cref="ApplicationManager"/>.</summary>
    public class ApplicationSections
    {

        #region Fields
        #endregion

        #region Properties

        public IAboutManager AboutManager { get; }
        public ISessionManager SessionManager { get; }

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="ApplicationSections"/> instance.</summary>
        /// <exception cref="ArgumentNullException"/>
        public ApplicationSections(IAboutManager aboutManager, ISessionManager sessionManager)
        {

            Validator.ValidateObject(aboutManager, nameof(aboutManager));
            Validator.ValidateObject(sessionManager, nameof(sessionManager));

            AboutManager = aboutManager;
            SessionManager = sessionManager;

        }

        #endregion

        #region Methods_public
        #endregion

        #region Methods_private
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 27.09.2022
*/