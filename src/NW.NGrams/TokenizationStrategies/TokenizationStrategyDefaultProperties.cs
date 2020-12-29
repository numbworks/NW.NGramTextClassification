namespace NW.NGrams
{
    public static class TokenizationStrategyDefaultProperties
    {

        // Fields
        // Properties

        /// <summary>
        /// "[\\w0-9_]{1,}" is the default pattern because in a Unicode-aware environment such as .NET
        /// \w will match any letter of any alphabet - incl. the scandinavian vowels (åäöÅÄÖ) for example.
        /// </summary>
        public static string Pattern { get; } = "[\\w0-9_]{1,}";

        public static string Delimiter { get; } = " ";
        public static bool ConvertAllToLowercase { get; } = true;

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 29.12.2020

*/