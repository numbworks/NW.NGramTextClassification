namespace NW.NGramTextClassification
{
    public class Trigram : ANGram, INGram
    {

        // Fields
        // Properties
        // Constructors
        public Trigram(ITokenizationStrategy strategy, string value)
            : base(3, strategy, value) { }
        public Trigram(string value)
            : base(3, new TokenizationStrategy(), value) { }

        // Methods (public)
        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 30.12.2020

*/