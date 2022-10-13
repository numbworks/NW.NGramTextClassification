using System;
using System.Collections.Generic;
using NW.NGramTextClassification.LabeledExamples;
using NW.NGramTextClassification.Serializations;
using NW.NGramTextClassification.TextSnippets;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests.Serializations
{
    [TestFixture]
    public class SerializerTests
    {

        #region Fields

        private static TestCaseData[] serializeToJsonExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new Serializer<LabeledExample>().SerializeToJson(objects: null)),
                typeof(ArgumentNullException),
                new ArgumentNullException("objects").Message
                ).SetArgDisplayNames($"{nameof(serializeToJsonExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new Serializer<LabeledExample>().SerializeToJson(obj: null)),
                typeof(ArgumentNullException),
                new ArgumentNullException("obj").Message
                ).SetArgDisplayNames($"{nameof(serializeToJsonExceptionTestCases)}_02")

        };
        private static TestCaseData[] deserializeFromJsonOrDefaultWhenUnproperArgumentTestCases =
        {

            new TestCaseData(
                    "Unproper Json content"
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

        [TestCaseSource(nameof(serializeToJsonExceptionTestCases))]
        public void SerializeToJson_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void SerializeToJson_ShouldReturnExpectedString_WhenArgumentIsCollectionAndTypeIsLabeledExample()
        {

            // Arrange
            List<LabeledExample> objects = LabeledExamples.ObjectMother.ShortLabeledExamples;
            string expected = LabeledExamples.ObjectMother.ShortLabeledExamplesAsJson_Content;

            // Act
            string actual = new Serializer<LabeledExample>().SerializeToJson(objects: objects);

            // Assert
            Assert.AreEqual(expected, actual);

        }

        [Test]
        public void SerializeToJson_ShouldReturnExpectedString_WhenArgumentIsCollectionAndTypeIsTextSnippet()
        {

            // Arrange
            List<TextSnippet> objects = TextSnippets.ObjectMother.TextSnippets;
            string expected = TextSnippets.ObjectMother.TextSnippetsAsJson_Content;

            // Act
            string actual = new Serializer<TextSnippet>().SerializeToJson(objects: objects);

            // Assert
            Assert.AreEqual(expected, actual);

        }


        [Test]
        public void SerializeToJson_ShouldReturnExpectedString_WhenArgumentIsSingleObjectAndTypeIsTextSnippet()
        {

            // Arrange
            TextSnippet obj = TextSnippets.ObjectMother.TextSnippet;
            string expected = TextSnippets.ObjectMother.TextSnippetAsJson_Content;

            // Act
            string actual = new Serializer<TextSnippet>().SerializeToJson(obj: obj);

            // Assert
            Assert.AreEqual(expected, actual);

        }


        [TestCaseSource(nameof(deserializeFromJsonOrDefaultWhenUnproperArgumentTestCases))]
        public void DeserializeFromJsonOrDefault_ShouldReturnDefault_WhenTypeIsLabeledExample(string json)
        {

            // Arrange
            // Act
            List<LabeledExample> actual = new Serializer<LabeledExample>().DeserializeFromJsonOrDefault(json: json);

            // Assert
            Assert.AreEqual(Serializer<LabeledExample>.Default, actual);

        }

        [Test]
        public void DeserializeFromJsonOrDefault_ShouldReturnExpectedCollectionOfObjects_WhenTypeIsLabeledExample()
        {

            // Arrange
            string json = LabeledExamples.ObjectMother.ShortLabeledExamplesAsJson_Content;
            List<LabeledExample> expected = LabeledExamples.ObjectMother.ShortLabeledExamples;

            // Act
            List<LabeledExample> actual = new Serializer<LabeledExample>().DeserializeFromJsonOrDefault(json: json);

            // Assert
            Assert.IsTrue(
                    LabeledExamples.ObjectMother.AreEqual(expected, actual)
                );

        }

        [Test]
        public void DeserializeFromJsonOrDefault_ShouldReturnExpectedCollectionOfObjects_WhenTypeIsTextSnippet()
        {

            // Arrange
            string json = TextSnippets.ObjectMother.TextSnippetsAsJson_Content;
            List<TextSnippet> expected = TextSnippets.ObjectMother.TextSnippets;

            // Act
            List<TextSnippet> actual = new Serializer<TextSnippet>().DeserializeFromJsonOrDefault(json: json);

            // Assert
            Assert.IsTrue(
                    TextSnippets.ObjectMother.AreEqual(expected, actual)
                );

        }

        #endregion

        #region TearDown

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 13.10.2022
*/
