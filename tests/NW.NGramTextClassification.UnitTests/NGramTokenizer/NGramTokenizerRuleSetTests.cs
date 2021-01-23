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

        [Test]
        public void NGramTokenizerRuleSet_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            NGramTokenizerRuleSet actual1 = new NGramTokenizerRuleSet(true, false, true);
            NGramTokenizerRuleSet actual2 = new NGramTokenizerRuleSet();

            // Assert
            Assert.IsInstanceOf<NGramTokenizerRuleSet>(actual1);
            Assert.IsInstanceOf<NGramTokenizerRuleSet>(actual2);

        }

        // TearDown
        // Support methods

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 22.01.2021

*/