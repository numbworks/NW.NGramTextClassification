﻿using NW.NGramTextClassification.NGrams;
using NW.NGramTextClassification.NGramTokenization;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests.NGrams
{
    [TestFixture]
    public class MonogramTests
    {

        #region Fields
        #endregion

        #region SetUp
        #endregion

        #region Tests

        [Test]
        public void Monogram_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            Monogram actual1
                = new Monogram(
                        new TokenizationStrategy(),
                        LabeledExamples.ObjectMother.ShortLabeledExample01_Monograms[0].Value
                    );
            Monogram actual2
                = new Monogram(
                        LabeledExamples.ObjectMother.ShortLabeledExample01_Monograms[0].Value
                    );

            // Assert
            Assert.That(actual1, Is.InstanceOf<Monogram>());
            Assert.That(actual2, Is.InstanceOf<Monogram>());

        }

        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 31.09.2021
*/
