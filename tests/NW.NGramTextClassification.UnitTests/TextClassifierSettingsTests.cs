using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests
{
    [TestFixture]
    public class TextClassifierSettingsTests
    {

        // Fields
        // SetUp
        // Tests
        [Test]
        public void TextClassifierSettings_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            TextClassifierSettings actual1 = new TextClassifierSettings();
            TextClassifierSettings actual2 = new TextClassifierSettings(10);

            // Assert
            Assert.IsInstanceOf<TextClassifierSettings>(actual1);
            Assert.IsInstanceOf<TextClassifierSettings>(actual2);

        }

        // TearDown
        // Support methods

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 26.01.2021

*/
