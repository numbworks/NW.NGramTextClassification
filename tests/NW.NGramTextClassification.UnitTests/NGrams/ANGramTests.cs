using System;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests
{
    [TestFixture]
    public class ANGramTests
    {

        // Fields
        private static TestCaseData[] aNGramExceptionTestCases =
        {

            // ValidateN
            new TestCaseData(
                new TestDelegate(
                        () => new FakeGram(
                                    0,
                                    new TokenizationStrategy(),
                                    ObjectMother.ANGram_FakeGramValue1
                            )),
                typeof(ArgumentException),
                MessageCollection.VariableCantBeLessThanOne.Invoke(ObjectMother.ANGram_VariableName_N)
                ).SetArgDisplayNames($"{nameof(aNGramExceptionTestCases)}_01"),

            // ValidateObject
            new TestCaseData(
                new TestDelegate(
                        () => new FakeGram(
                                    1,
                                    null,
                                    ObjectMother.ANGram_FakeGramValue1
                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.ANGram_VariableName_Strategy).Message
                ).SetArgDisplayNames($"{nameof(aNGramExceptionTestCases)}_02"),

            // Validator.ValidateStringNullOrWhiteSpace
            new TestCaseData(
                new TestDelegate(
                        () => new FakeGram(
                                    1,
                                    new TokenizationStrategy(),
                                    ObjectMother.ANGram_FakeGramValueOnlyWhiteSpaces
                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.ANGram_VariableName_Value).Message
                ).SetArgDisplayNames($"{nameof(aNGramExceptionTestCases)}_03")

        };

        // SetUp
        // Tests
        [TestCaseSource(nameof(aNGramExceptionTestCases))]
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
