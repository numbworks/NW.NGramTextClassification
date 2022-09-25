using NW.NGramTextClassification.NGrams;
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
                        LabeledExamples.ObjectMother.LabeledExample01_Bigrams[0].Value
                    );
            Bigram actual2
                = new Bigram(
                        LabeledExamples.ObjectMother.LabeledExample01_Bigrams[0].Value
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
    Last Update: 25.09.2021
*/
