using System.Collections.Generic;
using NW.NGramTextClassification.Bags;
using NW.NGramTextClassification.Validation;

namespace NW.NGramTextClassification.TextClassifications
{
    /// <summary>The outcome of a text classification session.</summary>
    public class TextClassifierSession
    {

        #region Fields
        #endregion

        #region Properties

        public double MinimumAccuracySingleLabel { get; }
        public double MinimumAccuracyMultipleLabels { get; }
        public List<TextClassifierResult> Results { get; }
        public string Version { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a <see cref="TextClassifierSession"/> instance.
        /// </summary>
        public TextClassifierSession(SettingBag settingBag, List<TextClassifierResult> results, string version)
        {

            Validator.ValidateObject(settingBag, nameof(settingBag));
            Validator.ValidateList(results, nameof(results));
            Validator.ValidateStringNullOrWhiteSpace(version, nameof(version));

            MinimumAccuracySingleLabel = settingBag.MinimumAccuracySingleLabel;
            MinimumAccuracyMultipleLabels = settingBag.MinimumAccuracyMultipleLabels;

            Results = results;
            Version = version;

        }

        #endregion

        #region Methods_public
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 26.01.2024
*/