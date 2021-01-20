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
                => Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);


        // TearDown
        // Support methods
        private void Method_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
        {

            // Arrange
            // Act
            // Assert
            Exception actual = Assert.Throws(expectedType, del);
            Assert.AreEqual(expectedMessage, actual.Message);

        }

    }
}

/*

    Author: rua@sitecore.net
    Last Update: xx.xx.2019

*/
