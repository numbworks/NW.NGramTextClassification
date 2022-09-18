using System;
using System.Collections.Generic;
using NW.NGramTextClassification.LabeledExamples;
using NW.NGramTextClassification.Messages;
using NW.NGramTextClassification.NGramTokenization;
using NW.NGramTextClassification.Similarity;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests
{
    [TestFixture]
    public class TextClassifierTests
    {

        #region Fields

        private static TestCaseData[] textClassifierExceptionTestCases =
        {

            // ValidateObject
            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier(
                                        null,
                                        new TextClassifierSettings()
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("components").Message
                ).SetArgDisplayNames($"{nameof(textClassifierExceptionTestCases)}_01"),

            // ValidateObject
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
        private static TestCaseData[] predictLabelExceptionTestCases =
        {

            // ValidateStringNullOrWhiteSpace
            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .PredictLabel(
                                        null,
                                        ObjectMother.Shared_LabeledExamples_Text1Text2_MonoBiTriFourFive
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("text").Message
                ).SetArgDisplayNames($"{nameof(predictLabelExceptionTestCases)}_01"),

            // ValidateObject
            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .PredictLabel(
                                        ObjectMother.Shared_Text1_Text,
                                        null,
                                        ObjectMother.Shared_LabeledExamples_Text1Text2_MonoBiTriFourFive
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("tokenizerRuleSet").Message
                ).SetArgDisplayNames($"{nameof(predictLabelExceptionTestCases)}_02"),

            // ValidateList
            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .PredictLabel(
                                        ObjectMother.Shared_Text1_Text,
                                        null
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("labeledExamples").Message
                ).SetArgDisplayNames($"{nameof(predictLabelExceptionTestCases)}_03"),

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .PredictLabel(
                                        ObjectMother.Shared_Text1_Text,
                                        new List<LabeledExample>()
                                )),
                typeof(ArgumentException),
                MessageCollection.Validator_VariableContainsZeroItems.Invoke("labeledExamples")
                ).SetArgDisplayNames($"{nameof(predictLabelExceptionTestCases)}_04")

        };
        private static TestCaseData[] tryPredictLabelTestCases =
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
                    ObjectMother.CreateLabeledExamples(),
                    new TextClassifierResult(
                            label: null,
                            indexes: new List<SimilarityIndex>(),
                            indexAverages: new List<SimilarityIndexAverage>()
                        )
                ).SetArgDisplayNames($"{nameof(tryPredictLabelTestCases)}_01"),

            new TestCaseData(
                    "hi",
                    new NGramTokenizerRuleSet(
                            doForMonogram: false,
                            doForBigram: false,
                            doForTrigram: false,
                            doForFourgram: false,
                            doForFivegram: true     
                        ),
                    ObjectMother.CreateLabeledExamples(),
                    new TextClassifierResult(
                            label: null,
                            indexes: new List<SimilarityIndex>(),
                            indexAverages: new List<SimilarityIndexAverage>()
                        )
                ).SetArgDisplayNames($"{nameof(tryPredictLabelTestCases)}_02"),

            new TestCaseData(
                    ObjectMother.CreateLabeledExamples()[0].Text,
                    new NGramTokenizerRuleSet(
                            doForMonogram: true,
                            doForBigram: true,
                            doForTrigram: true,
                            doForFourgram: true,
                            doForFivegram: true
                        ),
                    ObjectMother.CreateLabeledExamples(),
                    ObjectMother.TextClassifier_TextClassifierResult_LabeledExamples00
                ).SetArgDisplayNames($"{nameof(tryPredictLabelTestCases)}_03")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(textClassifierExceptionTestCases))]
        public void TextClassifier_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(predictLabelExceptionTestCases))]
        public void PredictLabel_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void TextClassifier_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            TextClassifier actual = new TextClassifier();

            // Assert
            Assert.IsInstanceOf<TextClassifier>(actual);

        }

        [Test]
        public void PredictLabel_ShouldReturnTheExpectedTextClassifierResultAndLogTheExpectedMessages_WhenProperArgument()
        {

            // Arrange
            List<string> actualLogMessages = new List<string>();
            Action<string> fakeLoggingAction
                = (message) => actualLogMessages.Add(message);
            TextClassifierComponents components
                = new TextClassifierComponents(
                          new NGramTokenizer(),
                          new SimilarityIndexCalculatorJaccard(),
                          TextClassifierComponents.DefaultRoundingFunction,
                          TextClassifierComponents.DefaultTextTruncatingFunction,
                          fakeLoggingAction);
            TextClassifier textClassifier = new TextClassifier(components, new TextClassifierSettings());
            string truncatedText
                = TextClassifierComponents.DefaultTextTruncatingFunction.Invoke(
                        ObjectMother.Shared_Text1_Text,
                        TextClassifierSettings.DefaultTruncateTextInLogMessagesAfter);
            List<string> expectedLogMessages = new List<string>()
            {

                MessageCollection.TextClassifier_AttemptingToPredictLabel,
                MessageCollection.TextClassifier_FollowingTextHasBeenProvided.Invoke(truncatedText),
                MessageCollection.TextClassifier_FollowingNGramsTokenizerRuleSetWillBeUsed.Invoke(new NGramTokenizerRuleSet()),
                MessageCollection.TextClassifier_XLabeledExamplesHaveBeenProvided.Invoke(ObjectMother.Shared_LabeledExamples_Text1Text2_MonoBiTriFourFive),
                MessageCollection.TextClassifier_ProvidedTextHasBeenTokenizedIntoXNGrams.Invoke(ObjectMother.TextClassifier_Text1_NGrams),

                MessageCollection.TextClassifier_ComparingProvidedTextAgainstFollowingLabeledExample.Invoke(ObjectMother.Shared_LabeledExamples_Text1Text2_MonoBiTriFourFive[0]),
                MessageCollection.TextClassifier_CalculatedSimilarityIndexValueIs.Invoke(ObjectMother.TextClassifier_Text1_SimilarityIndexes[0].Value),
                MessageCollection.TextClassifier_RoundedSimilarityIndexValueIs.Invoke(ObjectMother.TextClassifier_Text1_SimilarityIndexes[0].Value),
                MessageCollection.TextClassifier_FollowingSimilarityIndexObjectHasBeenAddedToTheList.Invoke(ObjectMother.TextClassifier_Text1_SimilarityIndexes[0]),

                MessageCollection.TextClassifier_ComparingProvidedTextAgainstFollowingLabeledExample.Invoke(ObjectMother.Shared_LabeledExamples_Text1Text2_MonoBiTriFourFive[1]),
                MessageCollection.TextClassifier_CalculatedSimilarityIndexValueIs.Invoke(ObjectMother.TextClassifier_Text1_SimilarityIndexes[1].Value),
                MessageCollection.TextClassifier_RoundedSimilarityIndexValueIs.Invoke(ObjectMother.TextClassifier_Text1_SimilarityIndexes[1].Value),
                MessageCollection.TextClassifier_FollowingSimilarityIndexObjectHasBeenAddedToTheList.Invoke(ObjectMother.TextClassifier_Text1_SimilarityIndexes[1]),

                MessageCollection.TextClassifier_TokenizedTextHasBeenComparedAgainstTheProvidedLabeledExamples,
                MessageCollection.TextClassifier_XSimilarityIndexObjectsHaveBeenComputed.Invoke(ObjectMother.TextClassifier_Text1_SimilarityIndexes),

                MessageCollection.TextClassifier_FollowingUniqueLabelsHaveBeenFound.Invoke(ObjectMother.TextClassifier_Text1_UniqueLabels),

                MessageCollection.TextClassifier_CalculatingIndexAverageForTheFollowingLabel.Invoke(ObjectMother.TextClassifier_Text1_UniqueLabels[0]),
                MessageCollection.TextClassifier_CalculatedSimilarityIndexAverageValueIs.Invoke(ObjectMother.TextClassifier_Text1_SimilarityIndexAverages[0].Value),
                MessageCollection.TextClassifier_RoundedSimilarityIndexAverageValueIs.Invoke(ObjectMother.TextClassifier_Text1_SimilarityIndexAverages[0].Value),
                MessageCollection.TextClassifier_FollowingSimilarityIndexAverageObjectHasBeenAddedToTheList.Invoke(ObjectMother.TextClassifier_Text1_SimilarityIndexAverages[0]),

                MessageCollection.TextClassifier_CalculatingIndexAverageForTheFollowingLabel.Invoke(ObjectMother.TextClassifier_Text1_UniqueLabels[1]),
                MessageCollection.TextClassifier_CalculatedSimilarityIndexAverageValueIs.Invoke(ObjectMother.TextClassifier_Text1_SimilarityIndexAverages[1].Value),
                MessageCollection.TextClassifier_RoundedSimilarityIndexAverageValueIs.Invoke(ObjectMother.TextClassifier_Text1_SimilarityIndexAverages[1].Value),
                MessageCollection.TextClassifier_FollowingSimilarityIndexAverageObjectHasBeenAddedToTheList.Invoke(ObjectMother.TextClassifier_Text1_SimilarityIndexAverages[1]),

                MessageCollection.TextClassifier_XSimilarityIndexAverageObjectsHaveBeenComputed(ObjectMother.TextClassifier_Text1_SimilarityIndexAverages),

                MessageCollection.TextClassifier_FollowingVerificationHasBeenSuccessful.Invoke("ContainsAtLeastOneIndexAverageThatIsNotZero"),
                MessageCollection.TextClassifier_FollowingVerificationHasBeenSuccessful.Invoke("ContainsAtLeastOneIndexAverageThatIsntEqualToTheOthers"),
                MessageCollection.TextClassifier_FollowingVerificationHasBeenSuccessful.Invoke("ContainsTwoHighestIndexAveragesThatArentEqual"),
                MessageCollection.TextClassifier_SimilarityIndexAverageWithTheHighestValueIs.Invoke(ObjectMother.TextClassifier_Text1_SimilarityIndexAverage1),

                MessageCollection.TextClassifier_PredictedLabelIs.Invoke(ObjectMother.Shared_Text1_Label),
                MessageCollection.TextClassifier_PredictionHasBeenSuccessful

            };

            // Act
            TextClassifierResult actual
                = textClassifier.PredictLabel(
                                    ObjectMother.Shared_Text1_Text,
                                    ObjectMother.Shared_LabeledExamples_Text1Text2_MonoBiTriFourFive);

            // Assert
            Assert.IsTrue(
                    ObjectMother.AreEqual(
                        ObjectMother.TextClassifier_Text1_TextClassifierResult,
                        actual
                        ));
            Assert.AreEqual(expectedLogMessages, actualLogMessages);

        }

        [Test]
        public void PredictLabel_ShouldReturnANullLabelAndLogTheExpectedMessages_WhenContainsAllIndexAverageValuesEqualToZero()
        {

            // Arrange
            List<string> actualLogMessages = new List<string>();
            Action<string> fakeLoggingAction
                = (message) => actualLogMessages.Add(message);
            TextClassifierComponents components
                = new TextClassifierComponents(
                          new NGramTokenizer(),
                          new SimilarityIndexCalculatorJaccard(),
                          TextClassifierComponents.DefaultRoundingFunction,
                          TextClassifierComponents.DefaultTextTruncatingFunction,
                          fakeLoggingAction);
            TextClassifier textClassifier = new TextClassifier(components, new TextClassifierSettings());
            string truncatedText
                = TextClassifierComponents.DefaultTextTruncatingFunction.Invoke(
                        ObjectMother.Shared_Text3_Text,
                        TextClassifierSettings.DefaultTruncateTextInLogMessagesAfter);
            List<string> expectedLogMessages = new List<string>()
            {

                MessageCollection.TextClassifier_AttemptingToPredictLabel,
                MessageCollection.TextClassifier_FollowingTextHasBeenProvided.Invoke(truncatedText),
                MessageCollection.TextClassifier_FollowingNGramsTokenizerRuleSetWillBeUsed.Invoke(new NGramTokenizerRuleSet()),
                MessageCollection.TextClassifier_XLabeledExamplesHaveBeenProvided.Invoke(ObjectMother.Shared_LabeledExamples_Text1Text2_MonoBiTriFourFive),
                MessageCollection.TextClassifier_ProvidedTextHasBeenTokenizedIntoXNGrams.Invoke(ObjectMother.Shared_Text3_TextAsNGrams),

                MessageCollection.TextClassifier_ComparingProvidedTextAgainstFollowingLabeledExample.Invoke(ObjectMother.Shared_LabeledExamples_Text1Text2_MonoBiTriFourFive[0]),
                MessageCollection.TextClassifier_CalculatedSimilarityIndexValueIs.Invoke(ObjectMother.TextClassifier_Text3_SimilarityIndexes[0].Value),
                MessageCollection.TextClassifier_RoundedSimilarityIndexValueIs.Invoke(ObjectMother.TextClassifier_Text3_SimilarityIndexes[0].Value),
                MessageCollection.TextClassifier_FollowingSimilarityIndexObjectHasBeenAddedToTheList.Invoke(ObjectMother.TextClassifier_Text3_SimilarityIndexes[0]),

                MessageCollection.TextClassifier_ComparingProvidedTextAgainstFollowingLabeledExample.Invoke(ObjectMother.Shared_LabeledExamples_Text1Text2_MonoBiTriFourFive[1]),
                MessageCollection.TextClassifier_CalculatedSimilarityIndexValueIs.Invoke(ObjectMother.TextClassifier_Text3_SimilarityIndexes[1].Value),
                MessageCollection.TextClassifier_RoundedSimilarityIndexValueIs.Invoke(ObjectMother.TextClassifier_Text3_SimilarityIndexes[1].Value),
                MessageCollection.TextClassifier_FollowingSimilarityIndexObjectHasBeenAddedToTheList.Invoke(ObjectMother.TextClassifier_Text3_SimilarityIndexes[1]),

                MessageCollection.TextClassifier_TokenizedTextHasBeenComparedAgainstTheProvidedLabeledExamples,
                MessageCollection.TextClassifier_XSimilarityIndexObjectsHaveBeenComputed.Invoke(ObjectMother.TextClassifier_Text3_SimilarityIndexes),

                MessageCollection.TextClassifier_FollowingUniqueLabelsHaveBeenFound.Invoke(ObjectMother.TextClassifier_Text3_UniqueLabels),

                MessageCollection.TextClassifier_CalculatingIndexAverageForTheFollowingLabel.Invoke(ObjectMother.TextClassifier_Text3_UniqueLabels[0]),
                MessageCollection.TextClassifier_CalculatedSimilarityIndexAverageValueIs.Invoke(ObjectMother.TextClassifier_Text3_SimilarityIndexAverages[0].Value),
                MessageCollection.TextClassifier_RoundedSimilarityIndexAverageValueIs.Invoke(ObjectMother.TextClassifier_Text3_SimilarityIndexAverages[0].Value),
                MessageCollection.TextClassifier_FollowingSimilarityIndexAverageObjectHasBeenAddedToTheList.Invoke(ObjectMother.TextClassifier_Text3_SimilarityIndexAverages[0]),

                MessageCollection.TextClassifier_CalculatingIndexAverageForTheFollowingLabel.Invoke(ObjectMother.TextClassifier_Text3_UniqueLabels[1]),
                MessageCollection.TextClassifier_CalculatedSimilarityIndexAverageValueIs.Invoke(ObjectMother.TextClassifier_Text3_SimilarityIndexAverages[1].Value),
                MessageCollection.TextClassifier_RoundedSimilarityIndexAverageValueIs.Invoke(ObjectMother.TextClassifier_Text3_SimilarityIndexAverages[1].Value),
                MessageCollection.TextClassifier_FollowingSimilarityIndexAverageObjectHasBeenAddedToTheList.Invoke(ObjectMother.TextClassifier_Text3_SimilarityIndexAverages[1]),

                MessageCollection.TextClassifier_XSimilarityIndexAverageObjectsHaveBeenComputed(ObjectMother.TextClassifier_Text3_SimilarityIndexAverages),

                MessageCollection.TextClassifier_FollowingVerificationHasFailed.Invoke("ContainsAtLeastOneIndexAverageThatIsNotZero"),

                MessageCollection.TextClassifier_PredictedLabelIs.Invoke(ObjectMother.Shared_Text3_Label),
                MessageCollection.TextClassifier_PredictionHasFailedTryIncreasingTheAmountOfProvidedLabeledExamples

            };

            // Act
            TextClassifierResult actual
                = textClassifier.PredictLabel(
                                    ObjectMother.Shared_Text3_Text,
                                    ObjectMother.Shared_LabeledExamples_Text1Text2_MonoBiTriFourFive);

            // Assert
            Assert.IsTrue(
                    ObjectMother.AreEqual(
                        ObjectMother.TextClassifier_Text3_TextClassifierResult,
                        actual
                        ));
            Assert.AreEqual(expectedLogMessages, actualLogMessages);

        }

        [TestCaseSource(nameof(tryPredictLabelTestCases))]
        public void TryPredictLabel_ShouldReturnTheExpectedTextClassifierResult_WhenProperArgument
            (string text, INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples, TextClassifierResult expected)
        {

            // Arrange
            Action<string> fakeLoggingAction = (message) => { };
            TextClassifierComponents components
                = new TextClassifierComponents(
                          new NGramTokenizer(),
                          new SimilarityIndexCalculatorJaccard(),
                          TextClassifierComponents.DefaultRoundingFunction,
                          TextClassifierComponents.DefaultTextTruncatingFunction,
                          fakeLoggingAction);
            TextClassifier textClassifier = new TextClassifier(components, new TextClassifierSettings());
            string truncatedText
                = TextClassifierComponents.DefaultTextTruncatingFunction.Invoke(
                        text,
                        TextClassifierSettings.DefaultTruncateTextInLogMessagesAfter);

            // Act
            TextClassifierResult actual = textClassifier.PredictLabelOrDefault(text, tokenizerRuleSet, labeledExamples);

            // Assert
            Assert.IsTrue(
                    ObjectMother.AreEqual(expected,actual)
                );

        }

        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 27.09.2021
*/