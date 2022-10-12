using System;
using System.Collections.Generic;
using NW.NGramTextClassification.TextSnippets;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests.TextSnippets
{
    [TestFixture]
    public class TextSnippetSerializerTests
    {

        #region Fields

        private static TestCaseData[] serializeToJsonExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new TextSnippetSerializer().SerializeToJson(textSnippets: null)
                            ),
                typeof(ArgumentNullException),
                new ArgumentNullException("textSnippets").Message
                ).SetArgDisplayNames($"{nameof(serializeToJsonExceptionTestCases)}_01")

        };
        private static TestCaseData[] serializeToJsonTestCases =
        {

            new TestCaseData(
                    ObjectMother.TextSnippets,
                    ObjectMother.TextSnippetsAsJson_Content
                ).SetArgDisplayNames($"{nameof(serializeToJsonTestCases)}_01"),

        };
        private static TestCaseData[] deserializeFromJsonOrDefaultTestCases =
        {

            new TestCaseData(
                    ObjectMother.TextSnippetsAsJson_Content,
                    ObjectMother.TextSnippets
                ).SetArgDisplayNames($"{nameof(deserializeFromJsonOrDefaultTestCases)}_01")

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

        [Test]
        public void TextSnippetSerializer_ShouldCreateAnInstanceOfThisType_WhenInvoked()
        {

            // Arrange
            // Act
            TextSnippetSerializer actual = new TextSnippetSerializer();

            // Assert
            Assert.IsInstanceOf<TextSnippetSerializer>(actual);

        }

        [TestCaseSource(nameof(serializeToJsonExceptionTestCases))]
        public void SerializeToJson_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(serializeToJsonTestCases))]
        public void SerializeToJson_ShouldReturnExpectedString_WhenProperParameters
            (List<TextSnippet> textSnippets, string expected)
        {

            // Arrange
            // Act
            string actual = new TextSnippetSerializer().SerializeToJson(textSnippets: textSnippets);

            // Assert
            Assert.AreEqual(expected, actual);

        }

        [TestCaseSource(nameof(deserializeFromJsonOrDefaultTestCases))]
        public void DeserializeFromJsonOrDefault_ShouldReturnExpectedCollectionOfTextSnippets_WhenProperParameters
            (string json, List<TextSnippet> expected)
        {

            // Arrange
            // Act
            List<TextSnippet> actual = new TextSnippetSerializer().DeserializeFromJsonOrDefault(json: json);

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
            List<TextSnippet> actual = new TextSnippetSerializer().DeserializeFromJsonOrDefault(json: json);

            // Assert
            Assert.AreEqual(TextSnippetSerializer.Default, actual);

        }

        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 12.10.2022
*/