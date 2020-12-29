using NUnit.Framework;

namespace NW.NGrams.UnitTests
{
    [TestFixture]
    public class LabeledTextJsonTests
    {

        // Fields
        // SetUp
        // Tests
        [TestCase("")]
        [TestCase("Hej du!")]
        public void ToString_ShouldReturnTheExpectedString_WhenInvoked(string strText)
        {

            // Arrange
            string strExpected = "{ \"LabeledTextId\": \"1\", \"Label\": \"sv\", \"Text\": \"" + strText + "\" }";
            LabeledTextJson objLabeledTextJson = new LabeledTextJson()
            {
                LabeledTextId = 1,
                Label = "sv",
                Text = strText

            };

            // Act
            string strActual = objLabeledTextJson.ToString();

            // Assert
            Assert.AreEqual(strExpected, strActual);

        }

        [Test]
        public void ToString_ShouldReturnTheExpectedStringWithShortenedText_WhenTextIsBiggerThanTenCharacters()
        {

            // Arrange
            string strExpected = "{ \"LabeledTextId\": \"1\", \"Label\": \"sv\", \"Text\": \"Är du genu [...]\" }";
            LabeledTextJson objLabeledTextJson = new LabeledTextJson()
            {
                LabeledTextId = 1,
                Label = "sv",
                Text = "Är du genuint intresserad av e-handel och förståelse av kundresan?"

            };

            // Act
            string strActual = objLabeledTextJson.ToString();

            // Assert
            Assert.AreEqual(strExpected, strActual);

        }

        // TearDown

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 18.01.2018

*/
