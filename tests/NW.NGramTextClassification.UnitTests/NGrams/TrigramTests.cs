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
                        LabeledExamples.ObjectMother.LabeledExample01_Trigrams[0].Value
                    );
            Trigram actual2
                = new Trigram(
                        LabeledExamples.ObjectMother.LabeledExample01_Trigrams[0].Value
                    );

            // Assert
            Assert.IsInstanceOf<Trigram>(actual1);
            Assert.IsInstanceOf<Trigram>(actual2);

        }

        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 25.09.2021
*/