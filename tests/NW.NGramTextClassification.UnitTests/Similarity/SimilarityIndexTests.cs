using System;
using NW.NGramTextClassification.Similarity;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests
{
    [TestFixture]
    public class SimilarityIndexTests
    {

        #region Fields

        private static TestCaseData[] similarityIndexExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new SimilarityIndex(
                                    text: null,
                                    label: ObjectMother.SimilarityIndex01_Label,
                                    value: ObjectMother.SimilarityIndex01_Value
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("text").Message
                ).SetArgDisplayNames($"{nameof(similarityIndexExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new SimilarityIndex(
                                    text: ObjectMother.SimilarityIndex01_Text,
                                    label: null,
                                    value: ObjectMother.SimilarityIndex01_Value
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("label").Message
                ).SetArgDisplayNames($"{nameof(similarityIndexExceptionTestCases)}_02")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [Test]
        public void SimilarityIndex_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            SimilarityIndex actual
                = new SimilarityIndex(
                        ObjectMother.SimilarityIndex01_Text,
                        ObjectMother.SimilarityIndex01_Label,
                        ObjectMother.SimilarityIndex01_Value
                    );

            // Assert
            Assert.IsInstanceOf<SimilarityIndex>(actual);

        }

        [Test]
        public void ToString_ShouldReturnTheExpectedString_WhenInvoked()
        {

            // Arrange
            // Act
            // Assert
            Assert.IsTrue(
                string.Equals(
                    ObjectMother.SimilarityIndex01_AsString,
                    ObjectMother.SimilarityIndex01.ToString(),
                    StringComparison.InvariantCulture));

        }

        [TestCaseSource(nameof(similarityIndexExceptionTestCases))]
        public void SimilarityIndex_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 19.09.2021
*/