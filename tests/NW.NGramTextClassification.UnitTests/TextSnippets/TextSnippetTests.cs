using System;
using NW.NGramTextClassification.TextSnippets;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests.TextSnippets
{
    [TestFixture]
    public class TextSnippetTests
    {

        #region Fields

        private static TestCaseData[] textSnippetExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate( () => new TextSnippet(text: null)),
                typeof(ArgumentNullException),
                new ArgumentNullException("text").Message
                ).SetArgDisplayNames($"{nameof(textSnippetExceptionTestCases)}_01")

        };

        #endregion

        #region SetUp

        #endregion

        #region Tests

        [TestCaseSource(nameof(textSnippetExceptionTestCases))]
        public void TextSnippet_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void ToString_ShouldReturnExpectedString_WhenInvoked()
        {

            // Arrange
            string text = "Some text";
            string expected = $"[ {nameof(TextSnippet.Text)}: '{text}' ]";

            // Act
            string actual = new TextSnippet(text: text).ToString();

            // Assert
            Assert.That(expected, Is.EqualTo(actual));

        }

        [Test]
        public void TextSnippet_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            TextSnippet actual = new TextSnippet(text: "Some text");

            // Assert
            Assert.That(actual, Is.InstanceOf<TextSnippet>());
            Assert.That(actual.Text, Is.InstanceOf<string>());

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
