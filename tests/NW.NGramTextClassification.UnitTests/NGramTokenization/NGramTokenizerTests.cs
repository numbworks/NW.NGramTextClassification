using System;
using System.Collections.Generic;
using NW.NGramTextClassification.Arrays;
using NW.NGramTextClassification.NGrams;
using NW.NGramTextClassification.NGramTokenization;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests.NGramTokenization
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
                                            tokenizationStrategy: ObjectMother.TokenizationStrategy_NonAlphanumerical
                                        )
                                    .DoForRuleSet(
                                        text: LabeledExamples.ObjectMother.ShortLabeledExample03_Untokenizable.Text,
                                        tokenizerRuleSet: new NGramTokenizerRuleSet()
                        )),
                    typeof(ArgumentException),
                    MessageCollection.ProvidedTokenizationStrategyPatternReturnsZeroMatches(ObjectMother.TokenizationStrategy_NonAlphanumerical)
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
        private static TestCaseData[] doForRuleSetOrDefaultTestCases = CreateTestCases(nameof(doForRuleSetOrDefaultTestCases));
        private static TestCaseData[] doForRuleSetTestCases = CreateTestCases(nameof(doForRuleSetTestCases));

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(nGramTokenizerExceptionTestCases))]
        public void NGramTokenizer_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

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

        [TestCaseSource(nameof(validateExceptionTestCases))]
        public void Validate_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(doForRuleSetOrDefaultTestCases))]
        public void DoForRuleSetOrDefault_ShouldReturnExpectedCollectionOfNGrams_WhenProperParameters
            (string text, INGramTokenizerRuleSet tokenizerRuleSet, List<INGram> expected)
        {


            // Arrange
            NGramTokenizer nGramTokenizer = new NGramTokenizer();

            // Act
            List<INGram> actual = nGramTokenizer.DoForRuleSetOrDefault(text, tokenizerRuleSet);

            // Assert
            Assert.IsTrue(
                    NGrams.ObjectMother.AreEqual(expected, actual)
                );

        }

        [Test]
        public void DoForRuleSetOrDefault_ShouldReturnNullInsteadOfALabeledExample_WhenUnproperParameters()
        {

            // Arrange
            NGramTokenizer nGramTokenizer = new NGramTokenizer();

            // Act
            List<INGram> actual 
                = nGramTokenizer
                    .DoForRuleSetOrDefault(
                        text: LabeledExamples.ObjectMother.ShortLabeledExample03_Untokenizable.Text, 
                        tokenizerRuleSet: ObjectMother.NGramTokenizerRuleSet_Five
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
            List<Monogram> actual = nGramTokenizer.DoForMonogram(LabeledExamples.ObjectMother.ShortLabeledExample01.Text);

            // Assert
            Assert.IsTrue(
                    NGrams.ObjectMother.AreEqual(LabeledExamples.ObjectMother.ShortLabeledExample01_Monograms, actual)
                );

        }

        [Test]
        public void DoForBigram_ShouldReturnExpectedCollectionOfBigrams_WhenProperParameters()
        {

            // Arrange
            NGramTokenizer nGramTokenizer = new NGramTokenizer();

            // Act
            List<Bigram> actual = nGramTokenizer.DoForBigram(LabeledExamples.ObjectMother.ShortLabeledExample01.Text);

            // Assert
            Assert.IsTrue(
                    NGrams.ObjectMother.AreEqual(LabeledExamples.ObjectMother.ShortLabeledExample01_Bigrams, actual)
                );

        }

        [Test]
        public void DoForTrigram_ShouldReturnExpectedCollectionOfTrigrams_WhenProperParameters()
        {

            // Arrange
            NGramTokenizer nGramTokenizer = new NGramTokenizer();

            // Act
            List<Trigram> actual = nGramTokenizer.DoForTrigram(LabeledExamples.ObjectMother.ShortLabeledExample01.Text);

            // Assert
            Assert.IsTrue(
                    NGrams.ObjectMother.AreEqual(LabeledExamples.ObjectMother.ShortLabeledExample01_Trigrams, actual)
                );

        }

        [Test]
        public void DoForFourgram_ShouldReturnExpectedCollectionOfFourgrams_WhenProperParameters()
        {

            // Arrange
            NGramTokenizer nGramTokenizer = new NGramTokenizer();

            // Act
            List<Fourgram> actual = nGramTokenizer.DoForFourgram(LabeledExamples.ObjectMother.ShortLabeledExample01.Text);

            // Assert
            Assert.IsTrue(
                    NGrams.ObjectMother.AreEqual(LabeledExamples.ObjectMother.ShortLabeledExample01_Fourgrams, actual)
                );

        }

        [Test]
        public void DoForFivegram_ShouldReturnExpectedCollectionOfFivegrams_WhenProperParameters()
        {

            // Arrange
            NGramTokenizer nGramTokenizer = new NGramTokenizer();

            // Act
            List<Fivegram> actual = nGramTokenizer.DoForFivegram(LabeledExamples.ObjectMother.ShortLabeledExample01.Text);

            // Assert
            Assert.IsTrue(
                    NGrams.ObjectMother.AreEqual(LabeledExamples.ObjectMother.ShortLabeledExample01_Fivegrams, actual)
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
                    .DoForRuleSet(
                        text: text,
                        tokenizerRuleSet: tokenizerRuleSet
                        );

            // Assert
            Assert.IsTrue(
                    NGrams.ObjectMother.AreEqual(expected, actual)
                );

        }

        #endregion

        #region TearDown
        #endregion

        #region Support_Methods

        private static TestCaseData[] CreateTestCases(string baseName)
        {

            List<TestCaseData> testCases = new List<TestCaseData>()
            {

                new TestCaseData(
                        LabeledExamples.ObjectMother.ShortLabeledExample01.Text,
                        ObjectMother.NGramTokenizerRuleSet_Mono,
                        LabeledExamples.ObjectMother.CreateNGrams(
                            LabeledExamples.ObjectMother.ShortLabeledExample01_Monograms
                            )
                    ).SetArgDisplayNames($"{baseName}_01"),

                new TestCaseData(
                        LabeledExamples.ObjectMother.ShortLabeledExample01.Text,
                        ObjectMother.NGramTokenizerRuleSet_MonoBi,
                        LabeledExamples.ObjectMother.CreateNGrams(
                            LabeledExamples.ObjectMother.ShortLabeledExample01_Monograms,
                            LabeledExamples.ObjectMother.ShortLabeledExample01_Bigrams
                            )
                    ).SetArgDisplayNames($"{nameof(baseName)}_02"),

                new TestCaseData(
                        LabeledExamples.ObjectMother.ShortLabeledExample01.Text,
                        ObjectMother.NGramTokenizerRuleSet_MonoBiTri,
                        LabeledExamples.ObjectMother.CreateNGrams(
                            LabeledExamples.ObjectMother.ShortLabeledExample01_Monograms,
                            LabeledExamples.ObjectMother.ShortLabeledExample01_Bigrams,
                            LabeledExamples.ObjectMother.ShortLabeledExample01_Trigrams
                            )
                    ).SetArgDisplayNames($"{nameof(baseName)}_03"),

                new TestCaseData(
                        LabeledExamples.ObjectMother.ShortLabeledExample01.Text,
                        ObjectMother.NGramTokenizerRuleSet_MonoBiTriFour,
                        LabeledExamples.ObjectMother.CreateNGrams(
                            LabeledExamples.ObjectMother.ShortLabeledExample01_Monograms,
                            LabeledExamples.ObjectMother.ShortLabeledExample01_Bigrams,
                            LabeledExamples.ObjectMother.ShortLabeledExample01_Trigrams,
                            LabeledExamples.ObjectMother.ShortLabeledExample01_Fourgrams
                            )
                    ).SetArgDisplayNames($"{nameof(baseName)}_04"),

                new TestCaseData(
                        LabeledExamples.ObjectMother.ShortLabeledExample01.Text,
                        ObjectMother.NGramTokenizerRuleSet_MonoBiTriFourFive,
                        LabeledExamples.ObjectMother.CreateNGrams(
                            LabeledExamples.ObjectMother.ShortLabeledExample01_Monograms,
                            LabeledExamples.ObjectMother.ShortLabeledExample01_Bigrams,
                            LabeledExamples.ObjectMother.ShortLabeledExample01_Trigrams,
                            LabeledExamples.ObjectMother.ShortLabeledExample01_Fourgrams,
                            LabeledExamples.ObjectMother.ShortLabeledExample01_Fivegrams
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
    Last Update: 25.09.2022
*/