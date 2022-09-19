using NW.NGramTextClassification.NGrams;
using NW.NGramTextClassification.NGramTokenization;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests
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
                        ObjectMother.Shared_LabeledExample01_Fourgrams[0].Value
                    );
            Fourgram actual2
                = new Fourgram(
                        ObjectMother.Shared_LabeledExample01_Fourgrams[0].Value
                    );

            // Assert
            Assert.IsInstanceOf<Fourgram>(actual1);
            Assert.IsInstanceOf<Fourgram>(actual2);

        }

        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 20.09.2021
*/