using System;
using System.Collections.Generic;
using NW.NGramTextClassification.Arrays;
using NW.NGramTextClassification.Messages;
using NW.NGramTextClassification.NGrams;
using NW.NGramTextClassification.NGramTokenization;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests
{
    [TestFixture]
    public class NGramTokenizerTests
    {

        #region Fields

        private static TestCaseData[] nGramTokenizerExceptionTestCases =
        {

            new TestCaseData(
                    new TestDelegate( () => new NGramTokenizer(null, new TokenizationStrategy()) ),
                    typeof(ArgumentNullException),
                    new ArgumentNullException("arrayManager").Message
                ).SetArgDisplayNames($"{nameof(nGramTokenizerExceptionTestCases)}_01"),

            new TestCaseData(
                    new TestDelegate( () => new NGramTokenizer(new ArrayManager(), null) ),
                    typeof(ArgumentNullException),
                    new ArgumentNullException("tokenizationStrategy").Message
                ).SetArgDisplayNames($"{nameof(nGramTokenizerExceptionTestCases)}_02"),

        };
        private static TestCaseData[] validateExceptionTestCases =
        {

            new TestCaseData(
                    new TestDelegate( 
                            () => new NGramTokenizer(
                                            arrayManager: new ArrayManager(),
                                            tokenizationStrategy: ObjectMother.NGramTokenizer_TokenizationStrategy_NonAlphanumerical
                                        )
                                    .DoForRuleSet(
                                        text: ObjectMother.Shared_Text1_TextOnlyFirstWord,
                                        tokenizerRuleSet: new NGramTokenizerRuleSet()
                        )),
                    typeof(ArgumentException),
                    MessageCollection.NGramsTokenizer_ProvidedTokenizationStrategyPatternReturnsZeroMatches.Invoke(ObjectMother.NGramTokenizer_TokenizationStrategy_NonAlphanumerical)
                ).SetArgDisplayNames($"{nameof(validateExceptionTestCases)}_01"),

            new TestCaseData(
                    new TestDelegate(
                            () => new NGramTokenizer()
                                        .DoForRuleSet(
                                            text: null,
                                            tokenizerRuleSet: new NGramTokenizerRuleSet()
                        )),
                    typeof(ArgumentNullException),
                    new ArgumentNullException("text").Message
                ).SetArgDisplayNames($"{nameof(validateExceptionTestCases)}_02")

        };
        private static TestCaseData[] tryDoForRuleSetTestCases = CreateDoForRuleSetTestCases(nameof(tryDoForRuleSetTestCases));
        private static TestCaseData[] doForRuleSetTestCases = CreateDoForRuleSetTestCases(nameof(doForRuleSetTestCases));

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(nGramTokenizerExceptionTestCases))]
        public void NGramTokenizer_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(validateExceptionTestCases))]
        public void Validate_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void NGramTokenizer_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            NGramTokenizer actual1 = new NGramTokenizer(new ArrayManager(), new TokenizationStrategy());
            NGramTokenizer actual2 = new NGramTokenizer();

            // Assert
            Assert.IsInstanceOf<NGramTokenizer>(actual1);
            Assert.IsInstanceOf<NGramTokenizer>(actual2);

        }

        [TestCaseSource(nameof(tryDoForRuleSetTestCases))]
        public void TryDoForRuleSet_ShouldReturnExpectedCollectionOfNGrams_WhenProperParameters
            (string text, INGramTokenizerRuleSet tokenizerRuleSet, List<INGram> expected)
        {


            // Arrange
            NGramTokenizer nGramTokenizer = new NGramTokenizer();

            // Act
            List<INGram> actual = nGramTokenizer.DoForRuleSetOrDefault(text, tokenizerRuleSet);

            // Assert
            Assert.IsTrue(
                    ObjectMother.AreEqual(expected, actual)
                );

        }

        [TestCaseSource(nameof(doForRuleSetTestCases))]
        public void DoForRuleSet_ShouldReturnExpectedCollectionOfNGrams_WhenProperParameters
            (string text, INGramTokenizerRuleSet tokenizerRuleSet, List<INGram> expected)
        {

            // Arrange
            NGramTokenizer nGramTokenizer = new NGramTokenizer();

            // Act
            List<INGram> actual 
                = nGramTokenizer
                    .DoForRuleSetOrDefault(
                        text: text, 
                        tokenizerRuleSet: tokenizerRuleSet
                        );

            // Assert
            Assert.IsTrue(
                    ObjectMother.AreEqual(expected, actual)
                );

        }

        [Test]
        public void TryDoForRuleSet_ShouldReturnNullInsteadOfALabeledExample_WhenUnproperParameters()
        {

            // Arrange
            NGramTokenizer nGramTokenizer = new NGramTokenizer();

            // Act
            List<INGram> actual 
                = nGramTokenizer
                    .DoForRuleSetOrDefault(
                        text: ObjectMother.Shared_Text1_TextOnlyFirstWord, 
                        tokenizerRuleSet: ObjectMother.Shared_RuleSet_Five
                        );

            // Assert
            Assert.IsNull(actual);

        }

        [Test]
        public void DoForMonogram_ShouldReturnExpectedCollectionOfMonograms_WhenProperParameters()
        {

            // Arrange
            NGramTokenizer nGramTokenizer = new NGramTokenizer();

            // Act
            List<Monogram> actual = nGramTokenizer.DoForMonogram(ObjectMother.Shared_Text1_Text);

            // Assert
            Assert.IsTrue(
                    ObjectMother.AreEqual(ObjectMother.Shared_LabeledExample01_Monograms, actual)
                );

        }

        [Test]
        public void DoForBigram_ShouldReturnExpectedCollectionOfBigrams_WhenProperParameters()
        {

            // Arrange
            NGramTokenizer nGramTokenizer = new NGramTokenizer();

            // Act
            List<Bigram> actual = nGramTokenizer.DoForBigram(ObjectMother.Shared_Text1_Text);

            // Assert
            Assert.IsTrue(
                    ObjectMother.AreEqual(ObjectMother.Shared_LabeledExample01_Bigrams, actual)
                );

        }

        [Test]
        public void DoForTrigram_ShouldReturnExpectedCollectionOfTrigrams_WhenProperParameters()
        {

            // Arrange
            NGramTokenizer nGramTokenizer = new NGramTokenizer();

            // Act
            List<Trigram> actual = nGramTokenizer.DoForTrigram(ObjectMother.Shared_Text1_Text);

            // Assert
            Assert.IsTrue(
                    ObjectMother.AreEqual(ObjectMother.Shared_LabeledExample01_Trigrams, actual)
                );

        }

        [Test]
        public void DoForFourgram_ShouldReturnExpectedCollectionOfFourgrams_WhenProperParameters()
        {

            // Arrange
            NGramTokenizer nGramTokenizer = new NGramTokenizer();

            // Act
            List<Fourgram> actual = nGramTokenizer.DoForFourgram(ObjectMother.Shared_Text1_Text);

            // Assert
            Assert.IsTrue(
                    ObjectMother.AreEqual(ObjectMother.Shared_LabeledExample01_Fourgrams, actual)
                );

        }

        [Test]
        public void DoForFivegram_ShouldReturnExpectedCollectionOfFivegrams_WhenProperParameters()
        {

            // Arrange
            NGramTokenizer nGramTokenizer = new NGramTokenizer();

            // Act
            List<Fivegram> actual = nGramTokenizer.DoForFivegram(ObjectMother.Shared_Text1_Text);

            // Assert
            Assert.IsTrue(
                    ObjectMother.AreEqual(ObjectMother.Shared_LabeledExample01_Fivegrams, actual)
                );

        }

        #endregion

        #region TearDown
        #endregion

        #region Support_Methods

        private static TestCaseData[] CreateDoForRuleSetTestCases(string baseName)
        {

            List<TestCaseData> testCases = new List<TestCaseData>()
            {

                new TestCaseData(
                        ObjectMother.Shared_Text1_Text,
                        ObjectMother.Shared_RuleSet_Mono,
                        ObjectMother.CreateNGrams(
                            ObjectMother.Shared_LabeledExample01_Monograms
                            )
                    ).SetArgDisplayNames($"{baseName}_01"),

                new TestCaseData(
                        ObjectMother.Shared_Text1_Text,
                        ObjectMother.Shared_RuleSet_MonoBi,
                        ObjectMother.CreateNGrams(
                            ObjectMother.Shared_LabeledExample01_Monograms,
                            ObjectMother.Shared_LabeledExample01_Bigrams
                            )
                    ).SetArgDisplayNames($"{nameof(baseName)}_02"),

                new TestCaseData(
                        ObjectMother.Shared_Text1_Text,
                        ObjectMother.Shared_RuleSet_MonoBiTri,
                        ObjectMother.CreateNGrams(
                            ObjectMother.Shared_LabeledExample01_Monograms,
                            ObjectMother.Shared_LabeledExample01_Bigrams,
                            ObjectMother.Shared_LabeledExample01_Trigrams
                            )
                    ).SetArgDisplayNames($"{nameof(baseName)}_03"),

                new TestCaseData(
                        ObjectMother.Shared_Text1_Text,
                        ObjectMother.Shared_RuleSet_MonoBiTriFour,
                        ObjectMother.CreateNGrams(
                            ObjectMother.Shared_LabeledExample01_Monograms,
                            ObjectMother.Shared_LabeledExample01_Bigrams,
                            ObjectMother.Shared_LabeledExample01_Trigrams,
                            ObjectMother.Shared_LabeledExample01_Fourgrams
                            )
                    ).SetArgDisplayNames($"{nameof(baseName)}_04"),

                new TestCaseData(
                        ObjectMother.Shared_Text1_Text,
                        ObjectMother.Shared_RuleSet_MonoBiTriFourFive,
                        ObjectMother.CreateNGrams(
                            ObjectMother.Shared_LabeledExample01_Monograms,
                            ObjectMother.Shared_LabeledExample01_Bigrams,
                            ObjectMother.Shared_LabeledExample01_Trigrams,
                            ObjectMother.Shared_LabeledExample01_Fourgrams,
                            ObjectMother.Shared_LabeledExample01_Fivegrams
                            )
                    ).SetArgDisplayNames($"{nameof(baseName)}_05")

            };

            return testCases.ToArray();

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 24.09.2021
*/