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

            // ValidateStringNullOrWhiteSpace
            new TestCaseData(
                new TestDelegate(
                        () => new SimilarityIndex(
                                    ObjectMother.SimilarityIndex_Id1,
                                    null,
                                    ObjectMother.SimilarityIndex_Value1
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.SimilarityIndex_VariableName_Label).Message
                ).SetArgDisplayNames($"{nameof(similarityIndexExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new SimilarityIndex(
                                    ObjectMother.SimilarityIndex_Id1,
                                    ObjectMother.SimilarityIndex_LabelOnlyWhiteSpaces,
                                    ObjectMother.SimilarityIndex_Value1
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.SimilarityIndex_VariableName_Label).Message
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
                        ObjectMother.SimilarityIndex_Id1,
                        ObjectMother.SimilarityIndex_Label1,
                        ObjectMother.SimilarityIndex_Value1
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
                    ObjectMother.SimilarityIndex_ToString1,
                    ObjectMother.SimilarityIndex1.ToString(),
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
    Last Update: 18.09.2021
*/