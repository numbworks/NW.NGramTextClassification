namespace NW.NGramTextClassification
{
    public class Fourgram : ANGram, INGram
    {

        // Fields
        // Properties
        // Constructors
        public Fourgram(ITokenizationStrategy strategy, string value)
            : base(4, strategy, value) { }
        public Fourgram(string value)
            : base(4, new TokenizationStrategy(), value) { }

        // Methods (public)
        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 30.01.2021

*/