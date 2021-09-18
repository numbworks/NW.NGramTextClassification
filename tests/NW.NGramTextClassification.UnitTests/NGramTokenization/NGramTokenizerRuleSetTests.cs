using System;
using NW.NGramTextClassification.NGramTokenization;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests
{
    [TestFixture]
    public class NGramTokenizerRuleSetTests
    {

        #region Fields
        #endregion

        #region SetUp
        #endregion

        #region Tests

        [Test]
        public void NGramTokenizerRuleSet_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            NGramTokenizerRuleSet actual1 = new NGramTokenizerRuleSet(true, false, true, false, false);
            NGramTokenizerRuleSet actual2 = new NGramTokenizerRuleSet();

            // Assert
            Assert.IsInstanceOf<NGramTokenizerRuleSet>(actual1);
            Assert.IsInstanceOf<NGramTokenizerRuleSet>(actual2);

        }

        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 18.09.2021
*/