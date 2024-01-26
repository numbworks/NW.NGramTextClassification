using System.Collections.Generic;
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
        public TextClassifierSession(SettingCollection settingCollection, List<TextClassifierResult> results, string version)
        {

            Validator.ValidateObject(settingCollection, nameof(settingCollection));
            Validator.ValidateList(results, nameof(results));
            Validator.ValidateStringNullOrWhiteSpace(version, nameof(version));

            MinimumAccuracySingleLabel = settingCollection.MinimumAccuracySingleLabel;
            MinimumAccuracyMultipleLabels = settingCollection.MinimumAccuracyMultipleLabels;

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