using System;
using System.Collections.Generic;
using NW.NGramTextClassification.LabeledExamples;
using NW.NGramTextClassification.NGramTokenization;
using NW.NGramTextClassification.TextClassifications;
using NW.NGramTextClassification.TextSnippets;
using NW.Shared.Serialization;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests.Serialization
{
    [TestFixture]
    public class SerializerTests
    {

        #region Fields

        private static TestCaseData[] serializeExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new Serializer<LabeledExample>().Serialize(objects: null)),
                typeof(ArgumentNullException),
                new ArgumentNullException("objects").Message
                ).SetArgDisplayNames($"{nameof(serializeExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new Serializer<LabeledExample>().Serialize(obj: null)),
                typeof(ArgumentNullException),
                new ArgumentNullException("obj").Message
                ).SetArgDisplayNames($"{nameof(serializeExceptionTestCases)}_02")

        };
        private static TestCaseData[] deserializeManyOrDefaultWhenUnproperArgumentTestCases =
        {

            new TestCaseData(
                    "Unproper Json content"
                ).SetArgDisplayNames($"{nameof(deserializeManyOrDefaultWhenUnproperArgumentTestCases)}_01"),

            new TestCaseData(
                    string.Empty
                ).SetArgDisplayNames($"{nameof(deserializeManyOrDefaultWhenUnproperArgumentTestCases)}_02"),

            new TestCaseData(
                    null
                ).SetArgDisplayNames($"{nameof(deserializeManyOrDefaultWhenUnproperArgumentTestCases)}_03"),

            new TestCaseData(
                    "[]"
                ).SetArgDisplayNames($"{nameof(deserializeManyOrDefaultWhenUnproperArgumentTestCases)}_04")

        };
        private static TestCaseData[] deserializeOrDefaultWhenUnproperArgumentTestCases =
        {

            new TestCaseData(
                    "Unproper Json content"
                ).SetArgDisplayNames($"{nameof(deserializeOrDefaultWhenUnproperArgumentTestCases)}_01"),

            new TestCaseData(
                    string.Empty
                ).SetArgDisplayNames($"{nameof(deserializeOrDefaultWhenUnproperArgumentTestCases)}_02"),

            new TestCaseData(
                    null
                ).SetArgDisplayNames($"{nameof(deserializeOrDefaultWhenUnproperArgumentTestCases)}_03"),

            new TestCaseData(
                    "[]"
                ).SetArgDisplayNames($"{nameof(deserializeOrDefaultWhenUnproperArgumentTestCases)}_04")

        };

        #endregion

        #region SetUp

        #endregion

        #region Tests

        [TestCaseSource(nameof(serializeExceptionTestCases))]
        public void Serialize_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void Serialize_ShouldReturnExpectedString_WhenArgumentIsCollectionAndTypeIsLabeledExample()
        {

            // Arrange
            List<LabeledExample> objects = LabeledExamples.ObjectMother.ShortLabeledExamples;
            string expected = LabeledExamples.ObjectMother.ShortLabeledExamplesAsJson_Content;

            // Act
            string actual = new Serializer<LabeledExample>().Serialize(objects: objects);

            // Assert
            Assert.That(expected, Is.EqualTo(actual));

        }

        [Test]
        public void Serialize_ShouldReturnExpectedString_WhenArgumentIsCollectionAndTypeIsTextSnippet()
        {

            // Arrange
            List<TextSnippet> objects = TextSnippets.ObjectMother.TextSnippets;
            string expected = TextSnippets.ObjectMother.TextSnippetsAsJson_Content;

            // Act
            string actual = new Serializer<TextSnippet>().Serialize(objects: objects);

            // Assert
            Assert.That(expected, Is.EqualTo(actual));

        }


        [Test]
        public void Serialize_ShouldReturnExpectedString_WhenArgumentIsSingleObjectAndTypeIsTextSnippet()
        {

            // Arrange
            TextSnippet obj = TextSnippets.ObjectMother.TextSnippet;
            string expected = TextSnippets.ObjectMother.TextSnippetAsJson_Content;

            // Act
            string actual = new Serializer<TextSnippet>().Serialize(obj: obj);

            // Assert
            Assert.That(expected, Is.EqualTo(actual));

        }

        [Test]
        public void Serialize_ShouldReturnExpectedString_WhenArgumentIsSingleObjectAndTypeIsTextClassifierSession()
        {

            // Arrange
            TextClassifierSession obj = TextClassifications.ObjectMother.TextClassifierSession_CompleteLabeledExamples00;
            string expected = TextClassifications.ObjectMother.TextClassifierrSessionCLE00AsJson_Content;

            // Act
            string actual = new Serializer<TextClassifierSession>().Serialize(obj: obj);

            // Assert
            Assert.That(expected, Is.EqualTo(actual));

        }


        [TestCaseSource(nameof(deserializeManyOrDefaultWhenUnproperArgumentTestCases))]
        public void DeserializeManyOrDefault_ShouldReturnDefault_WhenTypeIsLabeledExample(string json)
        {

            // Arrange
            // Act
            List<LabeledExample> actual = new Serializer<LabeledExample>().DeserializeManyOrDefault(json: json);

            // Assert
            Assert.That(Serializer<LabeledExample>.Default, Is.EqualTo(actual));

        }

        [Test]
        public void DeserializeManyOrDefault_ShouldReturnExpectedCollectionOfObjects_WhenTypeIsLabeledExample()
        {

            // Arrange
            string json = LabeledExamples.ObjectMother.ShortLabeledExamplesAsJson_Content;
            List<LabeledExample> expected = LabeledExamples.ObjectMother.ShortLabeledExamples;

            // Act
            List<LabeledExample> actual = new Serializer<LabeledExample>().DeserializeManyOrDefault(json: json);

            // Assert
            Assert.That(
                    LabeledExamples.ObjectMother.AreEqual(expected, actual),
                    Is.True
                );

        }

        [Test]
        public void DeserializeManyOrDefault_ShouldReturnExpectedCollectionOfObjects_WhenTypeIsTextSnippet()
        {

            // Arrange
            string json = TextSnippets.ObjectMother.TextSnippetsAsJson_Content;
            List<TextSnippet> expected = TextSnippets.ObjectMother.TextSnippets;

            // Act
            List<TextSnippet> actual = new Serializer<TextSnippet>().DeserializeManyOrDefault(json: json);

            // Assert
            Assert.That(
                    TextSnippets.ObjectMother.AreEqual(expected, actual),
                    Is.True
                );

        }


        [TestCaseSource(nameof(deserializeOrDefaultWhenUnproperArgumentTestCases))]
        public void DeserializeOrDefault_ShouldReturnDefault_WhenTypeIsNGramTokenizerRuleSet(string json)
        {

            // Arrange
            // Act
            NGramTokenizerRuleSet actual = new Serializer<NGramTokenizerRuleSet>().DeserializeOrDefault(json: json);

            // Assert
            Assert.That(default(NGramTokenizerRuleSet), Is.EqualTo(actual));

        }

        [Test]
        public void DeserializeOrDefault_ShouldReturnExpectedObject_WhenTypeIsNGramTokenizerRuleSet()
        {

            // Arrange
            string json = TextClassifications.ObjectMother.TokenizerRuleSetAsJson_Content;
            NGramTokenizerRuleSet expected = TextClassifications.ObjectMother.TokenizerRuleSet;

            // Act
            NGramTokenizerRuleSet actual = new Serializer<NGramTokenizerRuleSet>().DeserializeOrDefault(json: json);

            // Assert
            Assert.That(
                    NGramTokenization.ObjectMother.AreEqual(expected, actual),
                    Is.True
                );

        }

        #endregion

        #region TearDown

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 14.02.2024
*/
