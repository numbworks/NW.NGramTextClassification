namespace NW.NGrams
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

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 30.12.2020

*/