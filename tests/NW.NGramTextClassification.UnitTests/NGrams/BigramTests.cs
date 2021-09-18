﻿using NW.NGramTextClassification.NGrams;
using NW.NGramTextClassification.NGramTokenization;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests
{
    [TestFixture]
    public class BigramTests
    {

        #region Fields
        #endregion

        #region SetUp
        #endregion

        #region Tests

        [Test]
        public void Bigram_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            Bigram actual1
                = new Bigram(
                        new TokenizationStrategy(),
                        ObjectMother.LabeledExample_Text1_BigramValue1
                    );
            Bigram actual2
                = new Bigram(
                        ObjectMother.LabeledExample_Text1_BigramValue1
                    );

            // Assert
            Assert.IsInstanceOf<Bigram>(actual1);
            Assert.IsInstanceOf<Bigram>(actual2);

        }

        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 17.09.2021
*/
