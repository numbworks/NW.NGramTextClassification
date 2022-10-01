using System;
using System.Collections.Generic;
using System.Linq;
using NW.NGramTextClassification.AsciiBanner;
using NW.NGramTextClassification.LabeledExamples;
using NW.NGramTextClassification.NGrams;
using NW.NGramTextClassification.NGramTokenization;
using NW.NGramTextClassification.Similarity;
using NW.NGramTextClassification.TextClassifications;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests
{
    [TestFixture]
    public class TextClassifierTests
    {

        #region Fields

        private static TestCaseData[] textClassifierExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier(
                                        null,
                                        new TextClassifierSettings()
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("components").Message
                ).SetArgDisplayNames($"{nameof(textClassifierExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier(
                                        new TextClassifierComponents(),
                                        null
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("settings").Message
                ).SetArgDisplayNames($"{nameof(textClassifierExceptionTestCases)}_02")

        };
        private static TestCaseData[] classifyOrDefaultExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .ClassifyOrDefault(
                                        snippet: null,
                                        tokenizerRuleSet: NGramTokenization.ObjectMother.NGramTokenizerRuleSet_MonoBiTriFourFive,
                                        labeledExamples: LabeledExamples.ObjectMother.ShortLabeledExamples
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("snippet").Message
                ).SetArgDisplayNames($"{nameof(classifyOrDefaultExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .ClassifyOrDefault(
                                        snippet: LabeledExamples.ObjectMother.ShortLabeledExample01.Text,
                                        tokenizerRuleSet: null,
                                        labeledExamples: LabeledExamples.ObjectMother.ShortLabeledExamples
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("tokenizerRuleSet").Message
                ).SetArgDisplayNames($"{nameof(classifyOrDefaultExceptionTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .ClassifyOrDefault(
                                        snippet: LabeledExamples.ObjectMother.ShortLabeledExample01.Text,
                                        tokenizerRuleSet: NGramTokenization.ObjectMother.NGramTokenizerRuleSet_MonoBiTriFourFive,
                                        labeledExamples: null
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("labeledExamples").Message
                ).SetArgDisplayNames($"{nameof(classifyOrDefaultExceptionTestCases)}_03")

        };
        private static TestCaseData[] classifyOrDefaultWhenAllRulesFailedTestCases =
        {

           new TestCaseData(
                    "/",
                    new NGramTokenizerRuleSet(
                            doForMonogram: true,
                            doForBigram: true,
                            doForTrigram: true,
                            doForFourgram: true,
                            doForFivegram: true
                        ),
                    LabeledExamples.ObjectMother.CreateThirtyCompleteLabeledExamples(),
                    new TextClassifierResult(
                            label: null,
                            indexes: new List<SimilarityIndex>(),
                            indexAverages: new List<SimilarityIndexAverage>()
                        )
                ).SetArgDisplayNames($"{nameof(classifyOrDefaultWhenAllRulesFailedTestCases)}_01"),

            new TestCaseData(
                    "hi",
                    new NGramTokenizerRuleSet(
                            doForMonogram: false,
                            doForBigram: false,
                            doForTrigram: false,
                            doForFourgram: false,
                            doForFivegram: true
                        ),
                    LabeledExamples.ObjectMother.CreateThirtyCompleteLabeledExamples(),
                    new TextClassifierResult(
                            label: null,
                            indexes: new List<SimilarityIndex>(),
                            indexAverages: new List<SimilarityIndexAverage>()
                        )
                ).SetArgDisplayNames($"{nameof(classifyOrDefaultWhenAllRulesFailedTestCases)}_02"),


        };
        private static TestCaseData[] classifyOrDefaultWhenUntokenizableExamples =
       {

            new TestCaseData(
                    LabeledExamples.ObjectMother.CreateThirtyCompleteLabeledExamples()[0].Text,
                    NGramTokenization.ObjectMother.NGramTokenizerRuleSet_Five,
                    LabeledExamples.ObjectMother.ShortLabeledExamples_Untokenizable,
                    TextClassifier.DefaultTextClassifierResult
                ).SetArgDisplayNames($"{nameof(classifyOrDefaultWhenUntokenizableExamples)}_01")

        };
        private static TestCaseData[] classifyOrDefaultWhenOneLabeledExampleAndSuccessfulClassification =
        {

            new TestCaseData(
                    LabeledExamples.ObjectMother.CreateThirtyCompleteLabeledExamples()[0].Text,
                    new NGramTokenizerRuleSet(
                            doForMonogram: true,
                            doForBigram: true,
                            doForTrigram: true,
                            doForFourgram: true,
                            doForFivegram: true
                        ),
                    LabeledExamples.ObjectMother.CreateThirtyCompleteLabeledExamples().GetRange(0, 1),
                    LabeledExamples.ObjectMother.CreateThirtyCompleteLabeledExamples()[0].Label
                ).SetArgDisplayNames($"{nameof(classifyOrDefaultWhenOneLabeledExampleAndSuccessfulClassification)}_01")

        };
        private static TestCaseData[] classifyOrDefaultWhenThirtyLabeledExamplesAndSuccessfulClassification =
        {

            new TestCaseData(
                    LabeledExamples.ObjectMother.CreateThirtyCompleteLabeledExamples()[0].Text,
                    new NGramTokenizerRuleSet(
                            doForMonogram: true,
                            doForBigram: true,
                            doForTrigram: true,
                            doForFourgram: true,
                            doForFivegram: true
                        ),
                    LabeledExamples.ObjectMother.CreateThirtyCompleteLabeledExamples(),
                    TextClassifications.ObjectMother.TextClassifierResult_CompleteLabeledExamples00
                ).SetArgDisplayNames($"{nameof(classifyOrDefaultWhenThirtyLabeledExamplesAndSuccessfulClassification)}_01")

        };
        private static TestCaseData[] classifyManyExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .ClassifyMany(
                                        snippets: null,
                                        tokenizerRuleSet: NGramTokenization.ObjectMother.NGramTokenizerRuleSet_MonoBiTriFourFive,
                                        labeledExamples: LabeledExamples.ObjectMother.ShortLabeledExamples
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("snippets").Message
                ).SetArgDisplayNames($"{nameof(classifyManyExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .ClassifyMany(
                                        snippets: new List<string> { string.Empty },
                                        tokenizerRuleSet: NGramTokenization.ObjectMother.NGramTokenizerRuleSet_MonoBiTriFourFive,
                                        labeledExamples: LabeledExamples.ObjectMother.ShortLabeledExamples
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException(NGramTextClassification.TextClassifications.MessageCollection.SnippetsIndex(0)).Message
                ).SetArgDisplayNames($"{nameof(classifyManyExceptionTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .ClassifyMany(
                                        snippets: TextClassifications.ObjectMother.Snippets_CompleteLabeledExamples00,
                                        tokenizerRuleSet: null,
                                        labeledExamples: LabeledExamples.ObjectMother.ShortLabeledExamples
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("tokenizerRuleSet").Message
                ).SetArgDisplayNames($"{nameof(classifyManyExceptionTestCases)}_03"),

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .ClassifyMany(
                                        snippets: TextClassifications.ObjectMother.Snippets_CompleteLabeledExamples00,
                                        tokenizerRuleSet: NGramTokenization.ObjectMother.NGramTokenizerRuleSet_MonoBiTriFourFive,
                                        labeledExamples: null
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("labeledExamples").Message
                ).SetArgDisplayNames($"{nameof(classifyManyExceptionTestCases)}_04")

        };
        private static TestCaseData[] classifyManyTestCases =
        {

            new TestCaseData(
                    TextClassifications.ObjectMother.Snippets_CompleteLabeledExamples00,
                    TextClassifier.DefaultNGramTokenizerRuleSet,
                    LabeledExamples.ObjectMother.CreateThirtyCompleteLabeledExamples(),
                    TextClassifications.ObjectMother.TextClassifierResults_CompleteLabeledExamples00
                ).SetArgDisplayNames($"{nameof(classifyManyTestCases)}_01"),

            new TestCaseData(
                    TextClassifications.ObjectMother.Snippets_Untokenizable,
                    NGramTokenization.ObjectMother.NGramTokenizerRuleSet_Five,
                    LabeledExamples.ObjectMother.CreateThirtyCompleteLabeledExamples(),
                    TextClassifications.ObjectMother.TextClassifierResults_Untokenizable
                ).SetArgDisplayNames($"{nameof(classifyManyTestCases)}_02")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(textClassifierExceptionTestCases))]
        public void TextClassifier_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(classifyOrDefaultExceptionTestCases))]
        public void ClassifyOrDefault_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void TextClassifier_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            TextClassifier actual = new TextClassifier();

            // Assert
            Assert.IsInstanceOf<TextClassifier>(actual);

            Assert.IsInstanceOf<string>(actual.AsciiBanner);
            Assert.IsInstanceOf<string>(actual.Version);

            Assert.IsInstanceOf<TextClassifierComponents>(TextClassifier.DefaultTextClassifierComponents);
            Assert.IsInstanceOf<TextClassifierSettings>(TextClassifier.DefaultTextClassifierSettings);
            Assert.IsInstanceOf<INGramTokenizerRuleSet>(TextClassifier.DefaultNGramTokenizerRuleSet);
            Assert.IsInstanceOf<TextClassifierResult>(TextClassifier.DefaultTextClassifierResult);

        }


        [TestCaseSource(nameof(classifyOrDefaultWhenAllRulesFailedTestCases))]
        public void ClassifyOrDefault_ShouldReturnExpectedTextClassifierResult_WhenAllRulesFailed
            (string text, INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples, TextClassifierResult expected)
        {

            // Arrange
            List<string> actualLogMessages = new List<string>();
            Action<string> fakeLoggingAction = (message) => actualLogMessages.Add(message);
            TextClassifierComponents components
                = new TextClassifierComponents(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: TextClassifierComponents.DefaultRoundingFunction,
                          textTruncatingFunction: TextClassifierComponents.DefaultTextTruncatingFunction,
                          loggingAction: fakeLoggingAction,
                          labeledExampleManager: new LabeledExampleManager(),
                          asciiBannerManager: new AsciiBannerManager(),
                          loggingActionAsciiBanner: TextClassifierComponents.DefaultLoggingActionAsciiBanner);
            TextClassifier textClassifier = new TextClassifier(components, new TextClassifierSettings());

            List<string> expectedLogMessages = CreateWhenAllRulesFailed(text, tokenizerRuleSet, labeledExamples, components);

            // Act
            TextClassifierResult actual = textClassifier.ClassifyOrDefault(text, tokenizerRuleSet, labeledExamples);

            // Assert
            Assert.IsTrue(
                    TextClassifications.ObjectMother.AreEqual(expected, actual)
                );
            Assert.AreEqual(expectedLogMessages, actualLogMessages);

        }

        [TestCaseSource(nameof(classifyOrDefaultWhenUntokenizableExamples))]
        public void ClassifyOrDefault_ShouldReturnDefaultClassifierResult_WhenUntokenizableExamples
            (string text, INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples, TextClassifierResult expected)
        {

            // Arrange
            List<string> actualLogMessages = new List<string>();
            Action<string> fakeLoggingAction = (message) => actualLogMessages.Add(message);
            TextClassifierComponents components
                = new TextClassifierComponents(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: TextClassifierComponents.DefaultRoundingFunction,
                          textTruncatingFunction: TextClassifierComponents.DefaultTextTruncatingFunction,
                          loggingAction: fakeLoggingAction,
                          labeledExampleManager: new LabeledExampleManager(),
                          asciiBannerManager: new AsciiBannerManager(),
                          loggingActionAsciiBanner: TextClassifierComponents.DefaultLoggingActionAsciiBanner);
            TextClassifier textClassifier = new TextClassifier(components, new TextClassifierSettings());

            List<string> initialLogMessages = CreateWhenAllRulesFailed(text, tokenizerRuleSet, labeledExamples, components).GetRange(0, 5);
            // We skip all the messages in the middle, otherwise the test would be too complex.
            List<string> finalLogMessages = new List<string>()
            {

                NGramTextClassification.TextClassifications.MessageCollection.AtLeastOneLabeledExampleFailedTokenized

            };

            // Act
            TextClassifierResult actual = textClassifier.ClassifyOrDefault(text, tokenizerRuleSet, labeledExamples);

            // Assert
            Assert.IsTrue(
                    TextClassifications.ObjectMother.AreEqual(expected, actual)
                );
            Assert.AreEqual(
                    initialLogMessages,
                    actualLogMessages.GetRange(0, 5)
                );
            Assert.AreEqual(
                    finalLogMessages,
                    Enumerable.Reverse(actualLogMessages).Take(finalLogMessages.Count).Reverse().ToList()
                );

        }

        [TestCaseSource(nameof(classifyOrDefaultWhenOneLabeledExampleAndSuccessfulClassification))]
        public void ClassifyOrDefault_ShouldReturnExpectedLabel_WhenOneLabeledExampleAndSuccessfulClassification
            (string text, INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples, string expectedLabel)
        {

            // Arrange
            List<string> actualLogMessages = new List<string>();
            Action<string> fakeLoggingAction = (message) => actualLogMessages.Add(message);
            TextClassifierComponents components
                = new TextClassifierComponents(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: TextClassifierComponents.DefaultRoundingFunction,
                          textTruncatingFunction: TextClassifierComponents.DefaultTextTruncatingFunction,
                          loggingAction: fakeLoggingAction,
                          labeledExampleManager: new LabeledExampleManager(),
                          asciiBannerManager: new AsciiBannerManager(),
                          loggingActionAsciiBanner: TextClassifierComponents.DefaultLoggingActionAsciiBanner);
            TextClassifier textClassifier = new TextClassifier(components, new TextClassifierSettings());

            List<string> initialLogMessages = CreateWhenAllRulesFailed(text, tokenizerRuleSet, labeledExamples, components).GetRange(0, 5);
            // We skip all the messages in the middle, otherwise the test would be too complex.
            List<string> finalLogMessages = new List<string>()
            {

                NGramTextClassification.TextClassifications.MessageCollection.ResultOfClassificationTaskIs(expectedLabel),
                NGramTextClassification.TextClassifications.MessageCollection.ClassificationTaskHasBeenSuccessful

            };

            // Act
            TextClassifierResult actual = textClassifier.ClassifyOrDefault(text, tokenizerRuleSet, labeledExamples);

            // Assert
            Assert.AreEqual(expectedLabel, actual.Label);
            Assert.AreEqual(
                    initialLogMessages, 
                    actualLogMessages.GetRange(0, 5)
                );
            Assert.AreEqual(
                    finalLogMessages, 
                    Enumerable.Reverse(actualLogMessages).Take(finalLogMessages.Count).Reverse().ToList()
                );

        }

        [TestCaseSource(nameof(classifyOrDefaultWhenThirtyLabeledExamplesAndSuccessfulClassification))]
        public void ClassifyOrDefault_ShouldReturnExpectedTextClassifierResult_WhenThirtyLabeledExamplesAndSuccessfulClassification
            (string text, INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples, TextClassifierResult expected)
        {

            // Arrange
            List<string> actualLogMessages = new List<string>();
            Action<string> fakeLoggingAction = (message) => actualLogMessages.Add(message);
            TextClassifierComponents components
                = new TextClassifierComponents(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: TextClassifierComponents.DefaultRoundingFunction,
                          textTruncatingFunction: TextClassifierComponents.DefaultTextTruncatingFunction,
                          loggingAction: fakeLoggingAction,
                          labeledExampleManager: new LabeledExampleManager(),
                          asciiBannerManager: new AsciiBannerManager(),
                          loggingActionAsciiBanner: TextClassifierComponents.DefaultLoggingActionAsciiBanner);
            TextClassifierSettings settings
                = new TextClassifierSettings(
                          truncateTextInLogMessagesAfter: TextClassifierSettings.DefaultTruncateTextInLogMessagesAfter,
                          minimumAccuracySingleLabel: 0.0,
                          minimumAccuracyMultipleLabels: TextClassifierSettings.DefaultMinimumAccuracyMultipleLabels
                    );
            TextClassifier textClassifier = new TextClassifier(components, settings);

            List<string> initialLogMessages = CreateWhenAllRulesFailed(text, tokenizerRuleSet, labeledExamples, components).GetRange(0, 5);
            // We skip all the messages in the middle, otherwise the test would be too complex.
            List<string> finalLogMessages = new List<string>()
            {

                NGramTextClassification.TextClassifications.MessageCollection.ResultOfClassificationTaskIs(expected.Label),
                NGramTextClassification.TextClassifications.MessageCollection.ClassificationTaskHasBeenSuccessful

            };

            // Act
            TextClassifierResult actual = textClassifier.ClassifyOrDefault(text, tokenizerRuleSet, labeledExamples);

            // Assert
            Assert.IsTrue(
                    TextClassifications.ObjectMother.AreEqual(expected, actual)
                );
            Assert.AreEqual(
                    initialLogMessages,
                    actualLogMessages.GetRange(0, 5)
                );
            Assert.AreEqual(
                    finalLogMessages,
                    Enumerable.Reverse(actualLogMessages).Take(finalLogMessages.Count).Reverse().ToList()
                );

        }


        [Test]
        public void GetLabel_ShouldReturnNull_WhenContainsAtLeastOneNonZeroIndexAverageReturnsFalse()
        {

            // Arrange
            List<string> actualLogMessages = new List<string>();
            Action<string> fakeLoggingAction = (message) => actualLogMessages.Add(message);
            TextClassifierComponents components
                = new TextClassifierComponents(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: TextClassifierComponents.DefaultRoundingFunction,
                          textTruncatingFunction: TextClassifierComponents.DefaultTextTruncatingFunction,
                          loggingAction: fakeLoggingAction,
                          labeledExampleManager: new LabeledExampleManager(),
                          asciiBannerManager: new AsciiBannerManager(),
                          loggingActionAsciiBanner: TextClassifierComponents.DefaultLoggingActionAsciiBanner);
            TextClassifier textClassifier = new TextClassifier(components, new TextClassifierSettings());

            List<SimilarityIndexAverage> indexAverages = new List<SimilarityIndexAverage>()
            {

                new SimilarityIndexAverage(label: "en", value: 0),
                new SimilarityIndexAverage(label: "sv", value: 0)

            };

            string expected = null;
            List<string> expectedLogMessages = new List<string>()
            {

               NGramTextClassification.TextClassifications.MessageCollection.FollowingVerificationReturnedFalse("AreAllIndexAveragesEqualToZero")

            };

            // Act
            string actual
                = Utilities.ObjectMother.CallPrivateMethod<TextClassifier, string>(
                        obj: textClassifier,
                        methodName: "GetLabel",
                        args: new object[] { indexAverages }
                    );

            // Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedLogMessages, actualLogMessages);

        }

        [Test]
        public void GetLabel_ShouldReturnNull_ContainsAtLeastOneDifferentIndexAverageReturnsFalse()
        {

            // Arrange
            List<string> actualLogMessages = new List<string>();
            Action<string> fakeLoggingAction = (message) => actualLogMessages.Add(message);
            TextClassifierComponents components
                = new TextClassifierComponents(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: TextClassifierComponents.DefaultRoundingFunction,
                          textTruncatingFunction: TextClassifierComponents.DefaultTextTruncatingFunction,
                          loggingAction: fakeLoggingAction,
                          labeledExampleManager: new LabeledExampleManager(),
                          asciiBannerManager: new AsciiBannerManager(),
                          loggingActionAsciiBanner: TextClassifierComponents.DefaultLoggingActionAsciiBanner);
            TextClassifierSettings settings
                = new TextClassifierSettings(
                          truncateTextInLogMessagesAfter: TextClassifierSettings.DefaultTruncateTextInLogMessagesAfter,
                          minimumAccuracySingleLabel: TextClassifierSettings.DefaultMinimumAccuracySingleLabel,         // 0.98 >= 0.5 
                          minimumAccuracyMultipleLabels: TextClassifierSettings.DefaultMinimumAccuracyMultipleLabels
                    );
            TextClassifier textClassifier = new TextClassifier(components, settings);

            List<SimilarityIndexAverage> indexAverages = new List<SimilarityIndexAverage>()
            {

                new SimilarityIndexAverage(label: "en", value: 0.1),
                new SimilarityIndexAverage(label: "sv", value: 0.1),
                new SimilarityIndexAverage(label: "dk", value: 0.1)

            };

            string expected = null;
            List<string> expectedLogMessages = new List<string>()
            {

                NGramTextClassification.TextClassifications.MessageCollection.FollowingVerificationReturnedTrue("AreAllIndexAveragesEqualToZero"),
                NGramTextClassification.TextClassifications.MessageCollection.FollowingVerificationReturnedFalse("AreAllIndexAveragesSameValue")

            };

            // Act
            string actual
                = Utilities.ObjectMother.CallPrivateMethod<TextClassifier, string>(
                        obj: textClassifier,
                        methodName: "GetLabel",
                        args: new object[] { indexAverages }
                    );

            // Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedLogMessages, actualLogMessages);

        }

        [Test]
        public void GetLabel_ShouldReturnNull_ContainsTwoDifferentHighestIndexAveragesReturnsFalse()
        {

            // Arrange
            List<string> actualLogMessages = new List<string>();
            Action<string> fakeLoggingAction = (message) => actualLogMessages.Add(message);
            TextClassifierComponents components
                = new TextClassifierComponents(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: TextClassifierComponents.DefaultRoundingFunction,
                          textTruncatingFunction: TextClassifierComponents.DefaultTextTruncatingFunction,
                          loggingAction: fakeLoggingAction,
                          labeledExampleManager: new LabeledExampleManager(),
                          asciiBannerManager: new AsciiBannerManager(),
                          loggingActionAsciiBanner: TextClassifierComponents.DefaultLoggingActionAsciiBanner);
            TextClassifier textClassifier = new TextClassifier(components, new TextClassifierSettings());

            List<SimilarityIndexAverage> indexAverages = new List<SimilarityIndexAverage>()
            {

                new SimilarityIndexAverage(label: "sv", value: 0.98),
                new SimilarityIndexAverage(label: "en", value: 0.98),
                new SimilarityIndexAverage(label: "dk", value: 0.32)

            };

            string expected = null;
            List<string> expectedLogMessages = new List<string>()
            {

                NGramTextClassification.TextClassifications.MessageCollection.FollowingVerificationReturnedTrue("AreAllIndexAveragesEqualToZero"),
                NGramTextClassification.TextClassifications.MessageCollection.FollowingVerificationReturnedTrue("AreAllIndexAveragesSameValue"),
                NGramTextClassification.TextClassifications.MessageCollection.FollowingVerificationReturnedFalse("AreTwoHighestIndexAveragesSameValue")

            };

            // Act
            string actual
                = Utilities.ObjectMother.CallPrivateMethod<TextClassifier, string>(
                        obj: textClassifier,
                        methodName: "GetLabel",
                        args: new object[] { indexAverages }
                    );

            // Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedLogMessages, actualLogMessages);

        }

        [Test]
        public void GetLabel_ShouldReturnExpectedLabel_OneSimilarityIndexAverageValue()
        {

            // Arrange
            List<string> actualLogMessages = new List<string>();
            Action<string> fakeLoggingAction = (message) => actualLogMessages.Add(message);
            TextClassifierComponents components
                = new TextClassifierComponents(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: TextClassifierComponents.DefaultRoundingFunction,
                          textTruncatingFunction: TextClassifierComponents.DefaultTextTruncatingFunction,
                          loggingAction: fakeLoggingAction,
                          labeledExampleManager: new LabeledExampleManager(),
                          asciiBannerManager: new AsciiBannerManager(),
                          loggingActionAsciiBanner: TextClassifierComponents.DefaultLoggingActionAsciiBanner);
            TextClassifierSettings settings
                = new TextClassifierSettings(
                          truncateTextInLogMessagesAfter: TextClassifierSettings.DefaultTruncateTextInLogMessagesAfter,
                          minimumAccuracySingleLabel: TextClassifierSettings.DefaultMinimumAccuracySingleLabel,         // 0.98 >= 0.5 
                          minimumAccuracyMultipleLabels: TextClassifierSettings.DefaultMinimumAccuracyMultipleLabels
                    );
            TextClassifier textClassifier = new TextClassifier(components, settings);

            List<SimilarityIndexAverage> indexAverages = new List<SimilarityIndexAverage>()
            {

                new SimilarityIndexAverage(label: "sv", value: 0.98)

            };

            string expected = "sv";
            List<string> expectedLogMessages = new List<string>()
            {

                NGramTextClassification.TextClassifications.MessageCollection.FollowingVerificationReturnedFalse("AreAllIndexAveragesEqualToZero"),
                NGramTextClassification.TextClassifications.MessageCollection.FollowingVerificationReturnedTrue("IsSingleLabelAndHigherEqualThanMinimumAccuracy"),
                NGramTextClassification.TextClassifications.MessageCollection.SimilarityIndexAverageWithTheHighestValueIs(new SimilarityIndexAverage(label: expected, value: 0.98))

            };

            // Act
            string actual
                = Utilities.ObjectMother.CallPrivateMethod<TextClassifier, string>(
                        obj: textClassifier,
                        methodName: "GetLabel",
                        args: new object[] { indexAverages }
                    );

            // Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedLogMessages, actualLogMessages);

        }

        [Test]
        public void GetLabel_ShouldReturnExpectedLabel_ThreeDifferentSimilarityIndexAverageValues()
        {

            // Arrange
            List<string> actualLogMessages = new List<string>();
            Action<string> fakeLoggingAction = (message) => actualLogMessages.Add(message);
            TextClassifierComponents components
                = new TextClassifierComponents(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: TextClassifierComponents.DefaultRoundingFunction,
                          textTruncatingFunction: TextClassifierComponents.DefaultTextTruncatingFunction,
                          loggingAction: fakeLoggingAction,
                          labeledExampleManager: new LabeledExampleManager(),
                          asciiBannerManager: new AsciiBannerManager(),
                          loggingActionAsciiBanner: TextClassifierComponents.DefaultLoggingActionAsciiBanner);
            TextClassifierSettings settings
                = new TextClassifierSettings(
                          truncateTextInLogMessagesAfter: TextClassifierSettings.DefaultTruncateTextInLogMessagesAfter,
                          minimumAccuracySingleLabel: TextClassifierSettings.DefaultMinimumAccuracySingleLabel,         // 0.98 >= 0.5 
                          minimumAccuracyMultipleLabels: TextClassifierSettings.DefaultMinimumAccuracyMultipleLabels
                    );
            TextClassifier textClassifier = new TextClassifier(components, settings);

            List<SimilarityIndexAverage> indexAverages = new List<SimilarityIndexAverage>()
            {

                new SimilarityIndexAverage(label: "sv", value: 0.98),
                new SimilarityIndexAverage(label: "en", value: 0.75),
                new SimilarityIndexAverage(label: "dk", value: 0.32)

            };

            string expected = "sv";
            List<string> expectedLogMessages = new List<string>()
            {

                NGramTextClassification.TextClassifications.MessageCollection.FollowingVerificationReturnedTrue("AreAllIndexAveragesEqualToZero"),
                NGramTextClassification.TextClassifications.MessageCollection.FollowingVerificationReturnedTrue("AreAllIndexAveragesSameValue"),
                NGramTextClassification.TextClassifications.MessageCollection.FollowingVerificationReturnedTrue("AreTwoHighestIndexAveragesSameValue"),
                NGramTextClassification.TextClassifications.MessageCollection.SimilarityIndexAverageWithTheHighestValueIs(new SimilarityIndexAverage(label: expected, value: 0.98))

            };

            // Act
            string actual
                = Utilities.ObjectMother.CallPrivateMethod<TextClassifier, string>(
                        obj: textClassifier,
                        methodName: "GetLabel",
                        args: new object[] { indexAverages }
                    );

            // Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedLogMessages, actualLogMessages);

        }


        [Test]
        public void LogAsciiBanner_ShouldLogAsExpected_WhenInvoked()
        {

            // Arrange
            List<string> actualLogMessages = new List<string>();
            Action<string> fakeLoggerAsciiBanner = (message) => actualLogMessages.Add(message);
            TextClassifierComponents components
                = new TextClassifierComponents(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: TextClassifierComponents.DefaultRoundingFunction,
                          textTruncatingFunction: TextClassifierComponents.DefaultTextTruncatingFunction,
                          loggingAction: TextClassifierComponents.DefaultLoggingAction,
                          labeledExampleManager: new LabeledExampleManager(),
                          asciiBannerManager: new AsciiBannerManager(),
                          loggingActionAsciiBanner: fakeLoggerAsciiBanner);
            TextClassifier textClassifier = new TextClassifier(components, new TextClassifierSettings());

            List<string> expectedMessages = new List<string>()
            {

                new AsciiBannerManager().Create(textClassifier.Version)

            };

            // Act            
            textClassifier.LogAsciiBanner();

            // Assert
            Assert.AreEqual(expectedMessages, actualLogMessages);

        }


        [TestCaseSource(nameof(classifyManyExceptionTestCases))]
        public void ClassifyMany_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(classifyManyTestCases))]
        public void ClassifyMany_ShouldReturnExpectedCollectionOfTextClassifierResults_WhenInvoked
            (List<string> snippets, INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples, List<TextClassifierResult> expected)
        {

            // Arrange
            List<string> actualLogMessages = new List<string>();
            Action<string> fakeLoggingAction = (message) => actualLogMessages.Add(message);
            TextClassifierComponents components
                = new TextClassifierComponents(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: TextClassifierComponents.DefaultRoundingFunction,
                          textTruncatingFunction: TextClassifierComponents.DefaultTextTruncatingFunction,
                          loggingAction: fakeLoggingAction,
                          labeledExampleManager: new LabeledExampleManager(),
                          asciiBannerManager: new AsciiBannerManager(),
                          loggingActionAsciiBanner: TextClassifierComponents.DefaultLoggingActionAsciiBanner);
            TextClassifierSettings settings
                = new TextClassifierSettings(
                          truncateTextInLogMessagesAfter: TextClassifierSettings.DefaultTruncateTextInLogMessagesAfter,
                          minimumAccuracySingleLabel: 0.0,
                          minimumAccuracyMultipleLabels: TextClassifierSettings.DefaultMinimumAccuracyMultipleLabels
                    );
            TextClassifier textClassifier = new TextClassifier(components, settings);

            List<string> expectedLogMessages = new List<string>()
            {

                NGramTextClassification.TextClassifications.MessageCollection.ProvidedSnippetsAre(snippets.Count)

            };

            // Act
            List <TextClassifierResult> actual
                = textClassifier.ClassifyMany(snippets: snippets, tokenizerRuleSet: tokenizerRuleSet, labeledExamples: labeledExamples);

            // Assert
            Assert.IsTrue(
                    TextClassifications.ObjectMother.AreEqual(expected, actual)
                );
            Assert.AreEqual(expectedLogMessages[0], actualLogMessages[0]); // The only messages that it's different from ClassifyOrDefault() is the first one.

        }


        #endregion

        #region TearDown
        #endregion

        #region SupportMethods

        private static List<string> CreateWhenAllRulesFailed
            (string text, INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples, TextClassifierComponents components)
        {

            string expectedText
                = TextClassifierComponents.DefaultTextTruncatingFunction(
                        text,
                        TextClassifierSettings.DefaultTruncateTextInLogMessagesAfter);

            List<INGram> expectedNGrams = components.NGramsTokenizer.DoForRuleSetOrDefault(text, tokenizerRuleSet);

            List<string> expectedMessages = new List<string>()
            {

                NGramTextClassification.TextClassifications.MessageCollection.AttemptingToClassifyProvidedSnippet,
                NGramTextClassification.TextClassifications.MessageCollection.FollowingSnippetHasBeenProvided(expectedText),
                NGramTextClassification.TextClassifications.MessageCollection.FollowingNGramsTokenizerRuleSetWillBeUsed(tokenizerRuleSet),
                NGramTextClassification.TextClassifications.MessageCollection.XLabeledExamplesHaveBeenProvided(labeledExamples),
                NGramTextClassification.TextClassifications.MessageCollection.ProvidedTextHasBeenTokenizedIntoXNGrams(expectedNGrams),

                NGramTextClassification.TextClassifications.MessageCollection.AllRulesInProvidedRulesetFailed(text)

            };

            return expectedMessages;

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 30.09.2022
*/