using NW.NGramTextClassification.NGrams;
using NW.NGramTextClassification.NGramTokenization;

namespace NW.NGramTextClassification.UnitTests
{
    public class FakeGram : ANGram, INGram
    {

        // Fields
        // Properties
        // Constructors
        public FakeGram(ushort n, ITokenizationStrategy strategy, string value)
            : base(n, strategy, value) { }

        // Methods (public)
        // Methods (private)

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 20.01.2021

*/
