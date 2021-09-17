using NW.NGramTextClassification.NGrams;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests
{
    [TestFixture]
    public class FourgramTests
    {

        // Fields
        // SetUp
        // Tests
        [Test]
        public void Fourgram_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            Fourgram actual1
                = new Fourgram(
                        new TokenizationStrategy(),
                        ObjectMother.LabeledExample_Text1_FourgramValue1
                    );
            Fourgram actual2
                = new Fourgram(
                        ObjectMother.LabeledExample_Text1_FourgramValue1
                    );

            // Assert
            Assert.IsInstanceOf<Fourgram>(actual1);
            Assert.IsInstanceOf<Fourgram>(actual2);

        }

        // TearDown
        // Support methods

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 17.09.2021
*/