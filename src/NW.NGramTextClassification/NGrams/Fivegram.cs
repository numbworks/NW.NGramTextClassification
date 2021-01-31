namespace NW.NGramTextClassification
{
    public class Fivegram : ANGram, INGram
    {

        // Fields
        // Properties
        // Constructors
        public Fivegram(ITokenizationStrategy strategy, string value)
            : base(5, strategy, value) { }
        public Fivegram(string value)
            : base(5, new TokenizationStrategy(), value) { }

        // Methods (public)
        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 30.01.2021

*/