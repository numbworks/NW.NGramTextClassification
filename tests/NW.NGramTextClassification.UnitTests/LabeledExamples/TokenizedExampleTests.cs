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
                                                nGrams: ObjectMother.TokenizedExample01.NGrams
                                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException("labeledExample").Message
                ).SetArgDisplayNames($"{nameof(tokenizedExampleExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate( () => new TokenizedExample(
                                                labeledExample: ObjectMother.LabeledExample01,
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
                => UnitTests.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void ToString_ShouldReturnExpectedString_WhenInvoked()
        {

            // Arrange
            // Act
            string actual
                = ObjectMother.TokenizedExample01.ToString();

            // Assert
            Assert.AreEqual(
                    ObjectMother.TokenizedExample01_AsString,
                    actual);

        }

        [Test]
        public void TokenizedExample_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            TokenizedExample actual
                = new TokenizedExample(
                        labeledExample: ObjectMother.TokenizedExample01.LabeledExample,
                        nGrams: ObjectMother.TokenizedExample01.NGrams
                    );

            // Assert
            Assert.IsInstanceOf<TokenizedExample>(actual);

        }

        #endregion

        #region TearDown
        #endregion

    }

}

/*
    Author: numbworks@gmail.com
    Last Update: 25.09.2022
*/