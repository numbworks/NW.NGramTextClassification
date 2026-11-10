using System.Collections.Generic;
using NW.NGramTextClassification.TextClassifiers;
using NW.Shared.Validation;

namespace NW.NGramTextClassification.TextClassifications
{
    /// <summary>The outcome of a text classification session.</summary>
    public class ClassifyOutput
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
        /// Initializes a <see cref="ClassifyOutput"/> instance.
        /// </summary>
        public ClassifyOutput(
            double minimumAccuracySingleLabel, 
            double minimumAccuracyMultipleLabels, 
            List<TextClassifierResult> results, 
            string version)
        {

            Validator.ValidateList(results, nameof(results));
            Validator.ValidateStringNullOrWhiteSpace(version, nameof(version));

            MinimumAccuracySingleLabel = minimumAccuracySingleLabel;
            MinimumAccuracyMultipleLabels = minimumAccuracyMultipleLabels;
            Results = results;
            Version = version;

        }

        #endregion

        #region Methods (public)
        #endregion

    }
}