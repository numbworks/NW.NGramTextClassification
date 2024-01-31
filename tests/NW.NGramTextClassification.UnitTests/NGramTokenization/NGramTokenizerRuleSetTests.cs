using System;
using NW.NGramTextClassification.NGramTokenization;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests.NGramTokenization
{
    [TestFixture]
    public class NGramTokenizerRuleSetTests
    {

        #region Fields

        private static TestCaseData[] nGramTokenizerRuleSetExceptionTestCases =
        {

            new TestCaseData(
                    new TestDelegate( () => new NGramTokenizerRuleSet(false, false, false, false, false) ),
                    typeof(ArgumentException),
                    MessageCollection.AtLeastOneArgumentMustBeTrue
                ).SetArgDisplayNames($"{nameof(nGramTokenizerRuleSetExceptionTestCases)}_01")
        };
        private static TestCaseData[] nGramTokenizerRuleSetTestCases =
        {

            new TestCaseData(true, false, false, false, false)
                .SetArgDisplayNames($"{nameof(nGramTokenizerRuleSetTestCases)}_01"),

            new TestCaseData(true, true, false, false, false)
                .SetArgDisplayNames($"{nameof(nGramTokenizerRuleSetTestCases)}_02"),

            new TestCaseData(true, true, true, false, false)
                .SetArgDisplayNames($"{nameof(nGramTokenizerRuleSetTestCases)}_03"),

            new TestCaseData(true, true, true, true, false)
                .SetArgDisplayNames($"{nameof(nGramTokenizerRuleSetTestCases)}_04"),

            new TestCaseData(true, true, true, true, true)
                .SetArgDisplayNames($"{nameof(nGramTokenizerRuleSetTestCases)}_05"),

            new TestCaseData(false, false, false, false, true)
                .SetArgDisplayNames($"{nameof(nGramTokenizerRuleSetTestCases)}_06"),

            new TestCaseData(false, false, false, true, true)
                .SetArgDisplayNames($"{nameof(nGramTokenizerRuleSetTestCases)}_07"),

            new TestCaseData(false, false, true, true, true)
                .SetArgDisplayNames($"{nameof(nGramTokenizerRuleSetTestCases)}_08"),

            new TestCaseData(false, true, true, true, true)
                .SetArgDisplayNames($"{nameof(nGramTokenizerRuleSetTestCases)}_09")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(nGramTokenizerRuleSetExceptionTestCases))]
        public void NGramTokenizerRuleSet_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(nGramTokenizerRuleSetTestCases))]
        public void NGramTokenizerRuleSet_ShouldCreateAnInstanceOfThisType_WhenProperArgument
                (bool doForMonogram, bool doForBigram, bool doForTrigram, bool doForFourgram, bool doForFivegram)
        {

            // Arrange
            // Act
            NGramTokenizerRuleSet actual 
                = new NGramTokenizerRuleSet(
                        doForMonogram: doForMonogram,
                        doForBigram: doForBigram,
                        doForTrigram: doForTrigram,
                        doForFourgram: doForFourgram,
                        doForFivegram: doForFivegram
                    );

            // Assert
            Assert.That(actual, Is.InstanceOf<NGramTokenizerRuleSet>());

            Assert.That(NGramTokenizerRuleSet.DefaultDoForMonogram, Is.InstanceOf<bool>());
            Assert.That(NGramTokenizerRuleSet.DefaultDoForBigram,Is.InstanceOf<bool>());
            Assert.That(NGramTokenizerRuleSet.DefaultDoForTrigram, Is.InstanceOf<bool>());
            Assert.That(NGramTokenizerRuleSet.DefaultDoForFourgram, Is.InstanceOf<bool>());
            Assert.That(NGramTokenizerRuleSet.DefaultDoForFivegram, Is.InstanceOf<bool>());

        }

        [Test]
        public void NGramTokenizerRuleSet_ShouldCreateAnInstanceOfThisType_WhenDefaultConstructor()
        {

            // Arrange
            // Act
            NGramTokenizerRuleSet actual = new NGramTokenizerRuleSet();

            // Assert
            Assert.That(actual, Is.InstanceOf<NGramTokenizerRuleSet>());

            Assert.That(NGramTokenizerRuleSet.DefaultDoForMonogram, Is.True);
            Assert.That(NGramTokenizerRuleSet.DefaultDoForBigram, Is.True);
            Assert.That(NGramTokenizerRuleSet.DefaultDoForTrigram, Is.True);
            Assert.That(NGramTokenizerRuleSet.DefaultDoForFourgram, Is.True);
            Assert.That(NGramTokenizerRuleSet.DefaultDoForFivegram, Is.True);

        }

        [Test]
        public void ToString_ShouldReturnExpectedString_WhenDefaultConstructor()
        {

            // Arrange
            string expected
                = "[ DoForMonogram: 'True', DoForBigram: 'True', DoForTrigram: 'True', DoForFourgram: 'True', DoForFivegram: 'True' ]";

            // Act
            string actual = new NGramTokenizerRuleSet().ToString();


            // Assert
            Assert.That(expected, Is.EqualTo(actual));

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