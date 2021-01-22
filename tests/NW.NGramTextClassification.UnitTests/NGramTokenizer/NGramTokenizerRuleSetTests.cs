using System;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests
{
    [TestFixture]
    public class NGramTokenizerRuleSetTests
    {

        // Fields
        // SetUp
        // Tests
        [Test]
        public void ToString_ShouldReturnTheExpectedString_WhenInvoked()
        {

            // Arrange
            // Act
            string actual = new NGramTokenizerRuleSet().ToString();

            // Assert
            Assert.IsTrue(
                string.Equals(
                    ObjectMother.NGramTokenizerRuleSet_ToString, 
                    actual, 
                    StringComparison.InvariantCulture));

        }

        // TearDown
        // Support methods

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 22.01.2021

*/