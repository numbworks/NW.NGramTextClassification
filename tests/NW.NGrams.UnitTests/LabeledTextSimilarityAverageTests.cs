using NUnit.Framework;

namespace NW.NGrams.UnitTests
{

    [TestFixture]
    public class LabeledTextSimilarityAverageTests
    {

        // Fields
        // SetUp
        // Tests
        [Test]
        public void ToHeader_ShouldReturnTheExpectedHeader_WhenInvoked()
        {

            // Arrange
            string strExpected = "Label\tAverage";

            // Act
            string strActual = new LabeledTextSimilarityAverage("sv", 0.3).ToHeader();

            // Assert
            Assert.AreEqual(strExpected, strActual);

        }

        [Test]
        public void ToString_ShouldReturnTheExpectedString_WhenInvoked()
        {

            // Arrange
            string strExpected = string.Concat("sv\t", 0.3.ToString());

            // Act
            string strActual = new LabeledTextSimilarityAverage("sv", 0.3).ToString();

            // Assert
            Assert.AreEqual(strExpected, strActual);

        }

        // TearDown

    }

}

/*
 *
 *  Author: numbworks@gmail.com
 *  Last Update: 23.08.2018
 * 
 */
