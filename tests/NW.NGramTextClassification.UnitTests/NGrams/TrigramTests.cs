using NUnit.Framework;
using NW.NGramTextClassification.NGrams;
using NW.NGramTextClassification.NGramTokenization;

namespace NW.NGramTextClassification.UnitTests
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
                        ObjectMother.LabeledExample_Text1_TrigramValue1
                    );
            Trigram actual2
                = new Trigram(
                        ObjectMother.LabeledExample_Text1_TrigramValue1
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
    Last Update: 17.09.2021
*/