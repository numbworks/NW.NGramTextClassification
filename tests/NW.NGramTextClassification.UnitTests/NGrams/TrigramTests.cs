using NUnit.Framework;
using NW.NGramTextClassification.NGrams;
using NW.NGramTextClassification.NGramTokenization;

namespace NW.NGramTextClassification.UnitTests.NGrams
{
    [TestFixture]
    public class TrigramTests
    {

        #region Fields
        #endregion

        #region SetUp
        #endregion

        #region Tests

        [Test]
        public void Trigram_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            Trigram actual1
                = new Trigram(
                        new TokenizationStrategy(),
                        LabeledExamples.ObjectMother.ShortLabeledExample01_Trigrams[0].Value
                    );
            Trigram actual2
                = new Trigram(
                        LabeledExamples.ObjectMother.ShortLabeledExample01_Trigrams[0].Value
                    );

            // Assert
            Assert.That(actual1, Is.InstanceOf<Trigram>());
            Assert.That(actual2, Is.InstanceOf<Trigram>());

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