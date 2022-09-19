using System;
using System.Collections.Generic;
using NW.NGramTextClassification.LabeledExamples;
using NW.NGramTextClassification.NGrams;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests
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
                                                nGrams: ObjectMother.Shared_TokenizedExample01.NGrams
                                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException("labeledExample").Message
                ).SetArgDisplayNames($"{nameof(tokenizedExampleExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate( () => new TokenizedExample(
                                                labeledExample: ObjectMother.Shared_LabeledExample01,
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
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void ToString_ShouldReturnExpectedString_WhenInvoked()
        {

            // Arrange
            // Act
            string actual
                = ObjectMother.Shared_TokenizedExample01.ToString();

            // Assert
            Assert.AreEqual(
                    ObjectMother.Shared_TokenizedExample01_AsString,
                    actual);

        }

        [Test]
        public void TokenizedExample_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            TokenizedExample actual
                = new TokenizedExample(
                        labeledExample: ObjectMother.Shared_TokenizedExample01.LabeledExample,
                        nGrams: ObjectMother.Shared_TokenizedExample01.NGrams
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
    Last Update: 19.09.2022
*/