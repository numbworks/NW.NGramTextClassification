using System;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests
{
    [TestFixture]
    public class TextClassifierTests
    {

        // Fields
        private static TestCaseData[] textClassifierExceptionTestCases =
        {

            // ValidateObject
            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier(
                                        null,
                                        new TextClassifierSettings()
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.TextClassifier_VariableName_Components).Message
                ).SetArgDisplayNames($"{nameof(textClassifierExceptionTestCases)}_01"),

            // ValidateObject
            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier(
                                        new TextClassifierComponents(),
                                        null
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.TextClassifier_VariableName_Settings).Message
                ).SetArgDisplayNames($"{nameof(textClassifierExceptionTestCases)}_02")

        };

        // SetUp
        // Tests
        [TestCaseSource(nameof(textClassifierExceptionTestCases))]
        public void TextClassifier_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        // TearDown
        // Support methods


    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 31.12.2020

*/
