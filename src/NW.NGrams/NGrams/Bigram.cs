namespace NW.NGrams
{
    public class Bigram : ANGram, INGram
    {

        // Fields
        // Properties
        // Constructors
        public Bigram(ITokenizationStrategy strategy, string value)
            : base(2, strategy, value) { }

        // Methods (public)
        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 30.12.2020

*/