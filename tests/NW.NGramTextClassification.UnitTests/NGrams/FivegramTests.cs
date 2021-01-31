using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests
{
    [TestFixture]
    public class FivegramTests
    {

        // Fields
        // SetUp
        // Tests
        [Test]
        public void Fivegram_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            Fivegram actual1
                = new Fivegram(
                        new TokenizationStrategy(),
                        ObjectMother.LabeledExample_Text1_FivegramValue1
                    );
            Fivegram actual2
                = new Fivegram(
                        ObjectMother.LabeledExample_Text1_FivegramValue1
                    );

            // Assert
            Assert.IsInstanceOf<Fivegram>(actual1);
            Assert.IsInstanceOf<Fivegram>(actual2);

        }

        // TearDown
        // Support methods

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 30.01.2021

*/