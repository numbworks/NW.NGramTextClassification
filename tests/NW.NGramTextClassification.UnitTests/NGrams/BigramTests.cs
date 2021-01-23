using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests
{
    [TestFixture]
    public class BigramTests
    {

        // Fields
        // SetUp
        // Tests
        [Test]
        public void Bigram_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            Bigram actual1
                = new Bigram(
                        new TokenizationStrategy(),
                        ObjectMother.LabeledExample_Text1_BigramValue1
                    );
            Bigram actual2
                = new Bigram(
                        ObjectMother.LabeledExample_Text1_BigramValue1
                    );

            // Assert
            Assert.IsInstanceOf<Bigram>(actual1);
            Assert.IsInstanceOf<Bigram>(actual2);

        }

        // TearDown
        // Support methods

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 22.01.2021

*/