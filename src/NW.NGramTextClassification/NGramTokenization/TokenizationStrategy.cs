using System;
using NW.Shared.Validation;

namespace NW.NGramTextClassification.NGramTokenization
{
    /// <inheritdoc cref="ITokenizationStrategy"/>
    public class TokenizationStrategy : ITokenizationStrategy
    {

        #region Fields
        #endregion

        #region Properties

        public static string DefaultPattern { get; } = "[\\w0-9_]{1,}";
        public static string DefaultDelimiter { get; } = " ";
        public static bool DefaultToLowercase { get; } = true;

        public string Pattern { get; }
        public string Delimiter { get; }
        public bool ToLowercase { get; }

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="TokenizationStrategy"/> instance.</summary>
        /// <exception cref="ArgumentNullException"/>
        public TokenizationStrategy(string pattern, string delimiter, bool toLowercase)
        {

            Validator.ValidateStringNullOrWhiteSpace(pattern, nameof(pattern));
            Validator.ValidateStringNullOrEmpty(delimiter, nameof(delimiter)); // Whitespace is a valid delimiter

            Pattern = pattern;
            Delimiter = delimiter;
            ToLowercase = toLowercase;

        }

        /// <summary>Initializes a <see cref="TokenizationStrategy"/> instance using default parameters.</summary>
        public TokenizationStrategy()
            : this(DefaultPattern, DefaultDelimiter, DefaultToLowercase) { }

        #endregion

        #region Methods_public

        public override string ToString()
        {

            string content
                = string.Join(
                    ", ",
                    $"{nameof(Pattern)}: '{Pattern}'",
                    $"{nameof(Delimiter)}: '{Delimiter}'",
                    $"{nameof(ToLowercase)}: '{ToLowercase.ToString()}'"
                    );

            return $"[ {content} ]";

        }
        public override int GetHashCode()
            => (Pattern, Delimiter, ToLowercase).GetHashCode();
        public override bool Equals(object obj)
        {

            if (ReferenceEquals(this, obj))
                return true;

            if (ReferenceEquals(null, obj))
                return false;

            if (GetType() != obj.GetType())
                return false;

            return string.Equals(Delimiter, ((TokenizationStrategy)obj).Delimiter, StringComparison.InvariantCulture)
                    && string.Equals(Pattern, ((TokenizationStrategy)obj).Pattern, StringComparison.InvariantCulture)
                    && (ToLowercase == ((TokenizationStrategy)obj).ToLowercase);

        }
        public static bool operator ==(TokenizationStrategy a, TokenizationStrategy b)
        {

            if (ReferenceEquals(a, b))
                return true;

            if (ReferenceEquals(a, null))
                return false;

            return a.Equals(b) && b.Equals(a);

        }
        public static bool operator !=(TokenizationStrategy a, TokenizationStrategy b)
            => !(a == b);

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 17.09.2021
*/