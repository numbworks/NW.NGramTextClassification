namespace NW.NGrams
{
    public class Trigram : ANGram, INGram
    {

        // Fields
        // Properties
        // Constructors
        public Trigram(ITokenizationStrategy strategy, string value)
            : base(3, strategy, value) { }

        // Methods (public)
        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 30.12.2020

*/