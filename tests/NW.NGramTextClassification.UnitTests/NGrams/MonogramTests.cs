using NW.NGramTextClassification.NGrams;
using NW.NGramTextClassification.NGramTokenization;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests
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
                        ObjectMother.Shared_LabeledExample01_Monograms[0].Value
                    );
            Monogram actual2
                = new Monogram(
                        ObjectMother.Shared_LabeledExample01_Monograms[0].Value
                    );

            // Assert
            Assert.IsInstanceOf<Monogram>(actual1);
            Assert.IsInstanceOf<Monogram>(actual2);

        }

        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 21.09.2021
*/
