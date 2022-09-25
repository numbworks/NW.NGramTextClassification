using NW.NGramTextClassification.NGrams;
using NW.NGramTextClassification.NGramTokenization;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests
{
    [TestFixture]
    public class FivegramTests
    {

        #region Fields
        #endregion

        #region SetUp
        #endregion

        #region Tests

        [Test]
        public void Fivegram_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            Fivegram actual1
                = new Fivegram(
                        new TokenizationStrategy(),
                        LabeledExamples.ObjectMother.LabeledExample01_Fivegrams[0].Value
                    );
            Fivegram actual2
                = new Fivegram(
                        LabeledExamples.ObjectMother.LabeledExample01_Fivegrams[0].Value
                    );

            // Assert
            Assert.IsInstanceOf<Fivegram>(actual1);
            Assert.IsInstanceOf<Fivegram>(actual2);

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