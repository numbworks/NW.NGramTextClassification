using NW.NGramTextClassification.NGrams;
using NW.NGramTextClassification.NGramTokenization;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests.NGrams
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
                        LabeledExamples.ObjectMother.ShortLabeledExample01_Bigrams[0].Value
                    );
            Bigram actual2
                = new Bigram(
                        LabeledExamples.ObjectMother.ShortLabeledExample01_Bigrams[0].Value
                    );

            // Assert
            Assert.That(actual1, Is.InstanceOf<Bigram>());
            Assert.That(actual2, Is.InstanceOf<Bigram>());

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
