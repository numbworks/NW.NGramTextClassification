using System;
using System.ComponentModel.DataAnnotations;
using McMaster.Extensions.CommandLineUtils;
using McMaster.Extensions.CommandLineUtils.Validation;
using NW.Shared.Validation;

namespace NW.NGramTextClassificationClient.ApplicationSession
{
    /// <inheritdoc cref="IOptionValidator"/>
    public class MinimumAccuracyValidator : IOptionValidator
    {

        #region Fields

        private IDoubleManager _doubleManager;
        private string _valueName;

        #endregion

        #region Properties
        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="MinimumAccuracyValidator"/> instance.</summary>
        /// <exception cref="ArgumentNullException"/>
        public MinimumAccuracyValidator(IDoubleManager doubleManager)
        {

            NW.Shared.Validation.Validator.ValidateObject(doubleManager, nameof(doubleManager));

            _doubleManager = doubleManager;
            _valueName = nameof(MinimumAccuracyValidator).Replace("Validator", string.Empty);

        }

        #endregion

        #region Methods_public

        public ValidationResult GetValidationResult(CommandOption option, ValidationContext context)
        {

            // We need to accept also nulls because the minimum accuracy options are optional

            if (string.IsNullOrWhiteSpace(option.Value()))
                return ValidationResult.Success;

            if (_doubleManager.IsValid(option.Value()))
                return ValidationResult.Success;

            return new ValidationResult(Shared.MessageCollection.ValueIsInvalidOrNotWithinRange(_valueName, option.Value()));

        }

        #endregion

        #region Methods_private
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 12.02.2024
*/