using System;
using McMaster.Extensions.CommandLineUtils.Validation;
using NW.Shared.Validation;

namespace NW.NGramTextClassificationClient.ApplicationSession
{
    /// <summary>Collects all the dependencies required by <see cref="SessionManager"/>.</summary>
    public class SessionManagerBag
    {

        #region Fields
        #endregion

        #region Properties

        public IDoubleManager DoubleManager { get; }
        public IOptionValidator MinimumAccuracyValidator { get; }

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="SessionManagerBag"/> instance.</summary>
        /// <exception cref="ArgumentNullException"/>
        public SessionManagerBag(IDoubleManager doubleManager) 
        {

            Validator.ValidateObject(doubleManager, nameof(doubleManager));

            DoubleManager = doubleManager;

            MinimumAccuracyValidator = new MinimumAccuracyValidator(doubleManager);

        }

        /// <summary>Initializes a <see cref="SessionManagerBag"/> instance using default parameters.</summary>
        public SessionManagerBag()
            : this(new DoubleManager()) { }

        #endregion

        #region Methods_public
        #endregion

        #region Methods_private
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 26.01.2024
*/