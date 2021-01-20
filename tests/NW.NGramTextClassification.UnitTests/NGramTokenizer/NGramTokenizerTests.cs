using System;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests
{
    [TestFixture]
    public class NGramTokenizerTests
    {

        // Fields
        private static TestCaseData[] nGramTokenizerExceptionTestCases =
        {

            // ValidateObject
            new TestCaseData(
                new TestDelegate( () => new NGramTokenizer(null) ),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.NGramTokenizer_VariableName_ArrayManager).Message
                ).SetArgDisplayNames($"{nameof(nGramTokenizerExceptionTestCases)}_01")

        };

        // SetUp
        // Tests
        [TestCaseSource(nameof(nGramTokenizerExceptionTestCases))]
        public void ANGram_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        // TearDown
        // Support methods

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 20.01.2021

*/
