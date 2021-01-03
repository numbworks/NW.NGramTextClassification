namespace NW.NGramTextClassification
{
    public class TextClassifierSettings
    {

        // Fields
        // Properties
        public static uint DefaultTruncateTextInLogMessagesAfter { get; } = 20;

        public uint TruncateTextInLogMessagesAfter { get; private set; }

        // Constructors
        public TextClassifierSettings(
            uint truncateTextInLogMessagesAfter)
        {

            TruncateTextInLogMessagesAfter = truncateTextInLogMessagesAfter;

        }
        public TextClassifierSettings()
            : this(DefaultTruncateTextInLogMessagesAfter) { }

        // Methods (public)
        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 04.01.2021

*/
