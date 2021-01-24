using System;

namespace NW.NGramTextClassification
{
    public abstract class ANGram
    {

        // Fields
        // Properties
        public ushort N { get; }
        public ITokenizationStrategy Strategy { get; }
        public string Value { get; set; }

        // Constructor(s)
        protected ANGram(ushort n, ITokenizationStrategy strategy, string value)
        {

            Validator.ValidateN(n);
            Validator.ValidateObject(strategy, nameof(strategy));
            Validator.ValidateStringNullOrWhiteSpace(value, nameof(value));

            N = n;
            Strategy = strategy;
            Value = value;

        }

        // Methods
        public override int GetHashCode()
            => (N, Strategy, Value).GetHashCode();
        public override bool Equals(object obj)
        {

            if (ReferenceEquals(this, obj))
                return true;

            if (ReferenceEquals(null, obj))
                return false;

            if (GetType() != obj.GetType())
                return false;

            return (N == ((ANGram)obj).N)
                    && Strategy.Equals(((ANGram)obj).Strategy)
                    && string.Equals(Value, ((ANGram)obj).Value, StringComparison.InvariantCulture);

        }
        public static bool operator ==(ANGram a, ANGram b)
        {

            if (ReferenceEquals(a, b))
                return true;

            if (ReferenceEquals(a, null))
                return false;

            return a.Equals(b) && b.Equals(a);

        }
        public static bool operator !=(ANGram a, ANGram b)
            => !(a == b);

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 30.12.2020

*/