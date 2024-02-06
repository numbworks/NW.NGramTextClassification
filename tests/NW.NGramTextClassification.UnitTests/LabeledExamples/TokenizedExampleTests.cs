using System;
using NW.NGramTextClassification.LabeledExamples;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests.LabeledExamples
{
    [TestFixture]
    public class TokenizedExampleTests
    {

        #region Fields

        private static TestCaseData[] tokenizedExampleExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate( () => new TokenizedExample(
                                                labeledExample: null,
                                                nGrams: ObjectMother.ShortTokenizedExample01.NGrams
                                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException("labeledExample").Message
                ).SetArgDisplayNames($"{nameof(tokenizedExampleExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate( () => new TokenizedExample(
                                                labeledExample: ObjectMother.ShortLabeledExample01,
                                                nGrams: null
                                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException("nGrams").Message
                ).SetArgDisplayNames($"{nameof(tokenizedExampleExceptionTestCases)}_02")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(tokenizedExampleExceptionTestCases))]
        public void TokenizedExample_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void ToString_ShouldReturnExpectedString_WhenInvoked()
        {

            // Arrange
            // Act
            string actual
                = ObjectMother.ShortTokenizedExample01.ToString();

            // Assert
            Assert.That(
                    ObjectMother.ShortTokenizedExample01_AsString,
                    Is.EqualTo(actual));

        }

        [Test]
        public void TokenizedExample_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            TokenizedExample actual
                = new TokenizedExample(
                        labeledExample: ObjectMother.ShortTokenizedExample01.LabeledExample,
                        nGrams: ObjectMother.ShortTokenizedExample01.NGrams
                    );

            // Assert
            Assert.That(actual, Is.InstanceOf<TokenizedExample>());

        }

        #endregion

        #region TearDown
        #endregion

    }

}

/*
    Author: numbworks@gmail.com
    Last Update: 31.01.2024
*/