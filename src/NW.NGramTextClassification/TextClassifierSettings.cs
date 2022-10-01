namespace NW.NGramTextClassification
{
    /// <summary>Collects all the global settings required by this library.</summary>
    public class TextClassifierSettings
    {

        #region Fields
        #endregion

        #region Properties

        public static uint DefaultTruncateTextInLogMessagesAfter { get; } = 20;
        public static double DefaultMinimumAccuracySingleLabel { get; } = 0.5;
        public static double DefaultMinimumAccuracyMultipleLabels { get; } = 0.0;

        public uint TruncateTextInLogMessagesAfter { get; }
        public double MinimumAccuracySingleLabel { get; }
        public double MinimumAccuracyMultipleLabels { get; }

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="TextClassifierSettings"/> instance.</summary>
        public TextClassifierSettings(
                    uint truncateTextInLogMessagesAfter,
                    double minimumAccuracySingleLabel,
                    double minimumAccuracyMultipleLabels
            )
        {

            TruncateTextInLogMessagesAfter = truncateTextInLogMessagesAfter;
            MinimumAccuracySingleLabel = minimumAccuracySingleLabel;
            MinimumAccuracyMultipleLabels = minimumAccuracyMultipleLabels;

        }

        /// <summary>Initializes a <see cref="TextClassifierSettings"/> instance using default parameters.</summary>
        public TextClassifierSettings()
            : this(
                  truncateTextInLogMessagesAfter: DefaultTruncateTextInLogMessagesAfter,
                  minimumAccuracySingleLabel: DefaultMinimumAccuracySingleLabel,
                  minimumAccuracyMultipleLabels: DefaultMinimumAccuracyMultipleLabels
                  ) { }

        #endregion

        #region Methods_public
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 30.09.2021
*/