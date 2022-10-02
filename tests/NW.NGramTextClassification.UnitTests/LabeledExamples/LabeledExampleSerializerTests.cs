using System;
using System.Collections.Generic;
using NW.NGramTextClassification.LabeledExamples;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests.LabeledExamples
{
    [TestFixture]
    public class LabeledExampleSerializerTests
    {

        #region Fields

        private static TestCaseData[] serializeToJsonExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new LabeledExampleSerializer().SerializeToJson(labeledExamples: null)
                            ),
                typeof(ArgumentNullException),
                new ArgumentNullException("labeledExamples").Message
                ).SetArgDisplayNames($"{nameof(serializeToJsonExceptionTestCases)}_01")

        };
        private static TestCaseData[] serializeToJsonTestCases =
        {

            new TestCaseData(
                    ObjectMother.ShortLabeledExamples,
                    ObjectMother.ShortLabeledExamplesAsJson_Content
                ).SetArgDisplayNames($"{nameof(serializeToJsonTestCases)}_01"),

        };
        private static TestCaseData[] deserializeFromJsonOrDefaultTestCases =
        {

            new TestCaseData(
                    ObjectMother.ShortLabeledExamplesAsJson_Content,
                    ObjectMother.ShortLabeledExamples
                ).SetArgDisplayNames($"{nameof(deserializeFromJsonOrDefaultTestCases)}_01")

        };
        private static TestCaseData[] deserializeFromJsonOrDefaultWhenUnproperArgumentTestCases =
        {

            new TestCaseData(
                    "2396329869326"
                ).SetArgDisplayNames($"{nameof(deserializeFromJsonOrDefaultWhenUnproperArgumentTestCases)}_01"),

            new TestCaseData(
                    string.Empty
                ).SetArgDisplayNames($"{nameof(deserializeFromJsonOrDefaultWhenUnproperArgumentTestCases)}_02"),

            new TestCaseData(
                    null
                ).SetArgDisplayNames($"{nameof(deserializeFromJsonOrDefaultWhenUnproperArgumentTestCases)}_03"),

            new TestCaseData(
                    "[]"
                ).SetArgDisplayNames($"{nameof(deserializeFromJsonOrDefaultWhenUnproperArgumentTestCases)}_04")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [Test]
        public void LabeledExampleSerializer_ShouldCreateAnInstanceOfThisType_WhenInvoked()
        {

            // Arrange
            // Act
            LabeledExampleSerializer actual = new LabeledExampleSerializer();

            // Assert
            Assert.IsInstanceOf<LabeledExampleSerializer>(actual);

        }

        [TestCaseSource(nameof(serializeToJsonExceptionTestCases))]
        public void SerializeToJson_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(serializeToJsonTestCases))]
        public void SerializeToJson_ShouldReturnExpectedString_WhenProperParameters
            (List<LabeledExample> labeledExamples, string expected)
        {

            // Arrange
            // Act
            string actual = new LabeledExampleSerializer().SerializeToJson(labeledExamples: labeledExamples);

            // Assert
            Assert.AreEqual(expected, actual);

        }

        [TestCaseSource(nameof(deserializeFromJsonOrDefaultTestCases))]
        public void DeserializeFromJsonOrDefault_ShouldReturnExpectedCollectionOfLabeledExamples_WhenProperParameters
            (string json, List<LabeledExample> expected)
        {

            // Arrange
            // Act
            List<LabeledExample> actual = new LabeledExampleSerializer().DeserializeFromJsonOrDefault(json: json);

            // Assert
            Assert.IsTrue(
                    ObjectMother.AreEqual(expected, actual)
                );

        }


        [TestCaseSource(nameof(deserializeFromJsonOrDefaultWhenUnproperArgumentTestCases))]
        public void DeserializeFromJsonOrDefault_ShouldReturnDefault_WhenProperParameters(string json)
        {

            // Arrange
            // Act
            List<LabeledExample> actual = new LabeledExampleSerializer().DeserializeFromJsonOrDefault(json: json);

            // Assert
            Assert.AreEqual(LabeledExampleSerializer.Default, actual);

        }

        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 02.10.2022
*/