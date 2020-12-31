namespace NW.NGrams
{
    public class Monogram : ANGram, INGram
    {

        // Fields
        // Properties
        // Constructors
        public Monogram(ITokenizationStrategy strategy, string value)
            : base(1, strategy, value) { }

        // Methods (public)
        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 30.12.2020

*/