using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests
{
    [TestFixture]
    public class TextClassifierSettingsTests
    {

        #region Fields
        #endregion

        #region SetUp
        #endregion

        #region Tests

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

        #endregion

        #region TearDown
        #endregion

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 26.01.2021

*/
