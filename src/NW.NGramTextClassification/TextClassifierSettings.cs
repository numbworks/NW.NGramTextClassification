namespace NW.NGramTextClassification
{
    public class TextClassifierSettings
    {

        // Fields
        // Properties
        public uint TruncateTextInLogMessagesAfter { get; private set; }

        // Constructors
        public TextClassifierSettings(
            uint truncateTextInLogMessagesAfter)
        {

            TruncateTextInLogMessagesAfter = truncateTextInLogMessagesAfter;

        }
        public TextClassifierSettings()
            : this(20) { }

        // Methods (public)
        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 04.01.2021

*/
