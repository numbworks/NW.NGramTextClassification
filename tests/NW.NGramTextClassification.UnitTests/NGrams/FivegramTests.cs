using NW.NGramTextClassification.NGrams;
using NW.NGramTextClassification.NGramTokenization;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests.NGrams
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
                        LabeledExamples.ObjectMother.ShortLabeledExample01_Fivegrams[0].Value
                    );
            Fivegram actual2
                = new Fivegram(
                        LabeledExamples.ObjectMother.ShortLabeledExample01_Fivegrams[0].Value
                    );

            // Assert
            Assert.That(actual1, Is.InstanceOf<Fivegram>());
            Assert.That(actual2, Is.InstanceOf<Fivegram>());

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