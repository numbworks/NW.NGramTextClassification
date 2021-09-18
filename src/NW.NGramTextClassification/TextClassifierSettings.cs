namespace NW.NGramTextClassification
{
    /// <summary>Collects all the global settings required by this library.</summary>
    public class TextClassifierSettings
    {

        #region Fields
        #endregion

        #region Properties

        public static uint DefaultTruncateTextInLogMessagesAfter { get; } = 20;

        public uint TruncateTextInLogMessagesAfter { get; private set; }

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="TextClassifierSettings"/> instance.</summary>
        public TextClassifierSettings(uint truncateTextInLogMessagesAfter)
        {

            TruncateTextInLogMessagesAfter = truncateTextInLogMessagesAfter;

        }

        /// <summary>Initializes a <see cref="TextClassifierSettings"/> instance using default parameters.</summary>
        public TextClassifierSettings()
            : this(DefaultTruncateTextInLogMessagesAfter) { }

        #endregion

        #region Methods_public
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 18.09.2021
*/