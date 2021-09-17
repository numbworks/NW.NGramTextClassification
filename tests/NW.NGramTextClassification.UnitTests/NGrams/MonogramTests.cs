using NW.NGramTextClassification.NGrams;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests
{
    [TestFixture]
    public class MonogramTests
    {

        // Fields
        // SetUp
        // Tests
        [Test]
        public void Monogram_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            Monogram actual1
                = new Monogram(
                        new TokenizationStrategy(),
                        ObjectMother.LabeledExample_Text1_MonogramValue1
                    );
            Monogram actual2
                = new Monogram(
                        ObjectMother.LabeledExample_Text1_MonogramValue1
                    );

            // Assert
            Assert.IsInstanceOf<Monogram>(actual1);
            Assert.IsInstanceOf<Monogram>(actual2);

        }

        // TearDown
        // Support methods

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 17.09.2021
*/
