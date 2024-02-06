using NW.NGramTextClassification.NGrams;
using NW.NGramTextClassification.NGramTokenization;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests.NGrams
{
    [TestFixture]
    public class FourgramTests
    {

        #region Fields
        #endregion

        #region SetUp
        #endregion

        #region Tests

        [Test]
        public void Fourgram_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            Fourgram actual1
                = new Fourgram(
                        new TokenizationStrategy(),
                        LabeledExamples.ObjectMother.ShortLabeledExample01_Fourgrams[0].Value
                    );
            Fourgram actual2
                = new Fourgram(
                        LabeledExamples.ObjectMother.ShortLabeledExample01_Fourgrams[0].Value
                    );

            // Assert
            Assert.That(actual1,Is.InstanceOf<Fourgram>());
            Assert.That(actual2, Is.InstanceOf<Fourgram>());

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