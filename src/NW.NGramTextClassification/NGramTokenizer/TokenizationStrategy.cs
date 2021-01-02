namespace NW.NGramTextClassification
{
    public class TokenizationStrategy : ITokenizationStrategy
    {

        // Fields
        // Properties
        public static string DefaultPattern { get; } = "[\\w0-9_]{1,}";
        public static string DefaultDelimiter { get; } = " ";
        public static bool DefaultToLowercase { get; } = true;
        public string Pattern { get; }
        public string Delimiter { get; }
        public bool ToLowercase { get; }

        // Constructors
        public TokenizationStrategy(string pattern, string delimiter, bool toLowercase)
        {

            Validator.ValidateStringNullOrWhiteSpace(pattern, nameof(pattern));
            Validator.ValidateStringNullOrEmpty(delimiter, nameof(delimiter)); // Whitespace is a valid delimiter

            Pattern = pattern;
            Delimiter = delimiter;
            ToLowercase = toLowercase;

        }
        public TokenizationStrategy()
            : this(DefaultPattern, DefaultDelimiter, DefaultToLowercase) { }

        // Methods (public)
        public override string ToString()
        {

            string content
                = string.Join(
                    ", ",
                    $"{nameof(Pattern)}: '{Pattern}'",
                    $"{nameof(Delimiter)}: '{Delimiter}'",
                    $"{nameof(ToLowercase)}: '{ToLowercase.ToString()}...'"
                    );

            return $"[ {content} ]";

        }

        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 30.12.2020

*/
