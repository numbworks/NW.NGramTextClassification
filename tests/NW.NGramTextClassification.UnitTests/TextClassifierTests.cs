using System;
using System.Collections.Generic;
using NW.NGramTextClassification.LabeledExamples;
using NW.NGramTextClassification.Messages;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests
{
    [TestFixture]
    public class TextClassifierTests
    {

        // Fields
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
                new ArgumentNullException(ObjectMother.TextClassifier_VariableName_Components).Message
                ).SetArgDisplayNames($"{nameof(textClassifierExceptionTestCases)}_01"),

            // ValidateObject
            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier(
                                        new TextClassifierComponents(),
                                        null
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.TextClassifier_VariableName_Settings).Message
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
                                        ObjectMother.TextClassifier_LabeledExamples
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.TextClassifier_VariableName_Text).Message
                ).SetArgDisplayNames($"{nameof(predictLabelExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .PredictLabel(
                                        ObjectMother.TextClassifier_TextOnlyWhiteSpaces,
                                        ObjectMother.TextClassifier_LabeledExamples
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.TextClassifier_VariableName_Text).Message
                ).SetArgDisplayNames($"{nameof(predictLabelExceptionTestCases)}_02"),


            // ValidateObject
            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .PredictLabel(
                                        ObjectMother.TextClassifier_Text1,
                                        null,
                                        new NGramTokenizerRuleSet(),
                                        ObjectMother.TextClassifier_LabeledExamples
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.TextClassifier_VariableName_Strategy).Message
                ).SetArgDisplayNames($"{nameof(predictLabelExceptionTestCases)}_03"),

            // ValidateObject
            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .PredictLabel(
                                        ObjectMother.TextClassifier_Text1,
                                        new TokenizationStrategy(),
                                        null,
                                        ObjectMother.TextClassifier_LabeledExamples
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.TextClassifier_VariableName_RuleSet).Message
                ).SetArgDisplayNames($"{nameof(predictLabelExceptionTestCases)}_04"),

            // ValidateList
            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .PredictLabel(
                                        ObjectMother.TextClassifier_Text1,
                                        null
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.TextClassifier_VariableName_LabeledExamples).Message
                ).SetArgDisplayNames($"{nameof(predictLabelExceptionTestCases)}_05"),

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .PredictLabel(
                                        ObjectMother.TextClassifier_Text1,
                                        new List<LabeledExample>()
                                )),
                typeof(ArgumentException),
                MessageCollection.Validator_VariableContainsZeroItems.Invoke(ObjectMother.TextClassifier_VariableName_LabeledExamples)
                ).SetArgDisplayNames($"{nameof(predictLabelExceptionTestCases)}_06")

        };

        // SetUp
        // Tests
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
                        ObjectMother.TextClassifier_Text1, 
                        TextClassifierSettings.DefaultTruncateTextInLogMessagesAfter);
            List<string> expectedLogMessages = new List<string>()
            {

                MessageCollection.TextClassifier_AttemptingToPredictLabel,
                MessageCollection.TextClassifier_FollowingTextHasBeenProvided.Invoke(truncatedText),
                MessageCollection.TextClassifier_FollowingTokenizationStrategyWillBeUsed.Invoke(new TokenizationStrategy()),
                MessageCollection.TextClassifier_FollowingNGramsTokenizerRuleSetWillBeUsed.Invoke(new NGramTokenizerRuleSet()),
                MessageCollection.TextClassifier_XLabeledExamplesHaveBeenProvided.Invoke(ObjectMother.TextClassifier_LabeledExamples),
                MessageCollection.TextClassifier_ProvidedTextHasBeenTokenizedIntoXNGrams.Invoke(ObjectMother.TextClassifier_Text1_NGrams),

                MessageCollection.TextClassifier_ComparingProvidedTextAgainstFollowingLabeledExample.Invoke(ObjectMother.TextClassifier_LabeledExamples[0]),
                MessageCollection.TextClassifier_CalculatedSimilarityIndexValueIs.Invoke(ObjectMother.TextClassifier_Text1_SimilarityIndexes[0].Value),
                MessageCollection.TextClassifier_RoundedSimilarityIndexValueIs.Invoke(ObjectMother.TextClassifier_Text1_SimilarityIndexes[0].Value),
                MessageCollection.TextClassifier_FollowingSimilarityIndexObjectHasBeenAddedToTheList.Invoke(ObjectMother.TextClassifier_Text1_SimilarityIndexes[0]),

                MessageCollection.TextClassifier_ComparingProvidedTextAgainstFollowingLabeledExample.Invoke(ObjectMother.TextClassifier_LabeledExamples[1]),
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

                MessageCollection.TextClassifier_PredictedLabelIs.Invoke(ObjectMother.TextClassifier_Text1_Label),
                MessageCollection.TextClassifier_PredictionHasBeenSuccessful

            };

            // Act
            TextClassifierResult actual 
                = textClassifier.PredictLabel(
                                    ObjectMother.TextClassifier_Text1,
                                    ObjectMother.TextClassifier_LabeledExamples);

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
                        ObjectMother.TextClassifier_Text3,
                        TextClassifierSettings.DefaultTruncateTextInLogMessagesAfter);
            List<string> expectedLogMessages = new List<string>()
            {

                MessageCollection.TextClassifier_AttemptingToPredictLabel,
                MessageCollection.TextClassifier_FollowingTextHasBeenProvided.Invoke(truncatedText),
                MessageCollection.TextClassifier_FollowingTokenizationStrategyWillBeUsed.Invoke(new TokenizationStrategy()),
                MessageCollection.TextClassifier_FollowingNGramsTokenizerRuleSetWillBeUsed.Invoke(new NGramTokenizerRuleSet()),
                MessageCollection.TextClassifier_XLabeledExamplesHaveBeenProvided.Invoke(ObjectMother.TextClassifier_LabeledExamples),
                MessageCollection.TextClassifier_ProvidedTextHasBeenTokenizedIntoXNGrams.Invoke(ObjectMother.TextClassifier_Text3_NGrams),

                MessageCollection.TextClassifier_ComparingProvidedTextAgainstFollowingLabeledExample.Invoke(ObjectMother.TextClassifier_LabeledExamples[0]),
                MessageCollection.TextClassifier_CalculatedSimilarityIndexValueIs.Invoke(ObjectMother.TextClassifier_Text3_SimilarityIndexes[0].Value),
                MessageCollection.TextClassifier_RoundedSimilarityIndexValueIs.Invoke(ObjectMother.TextClassifier_Text3_SimilarityIndexes[0].Value),
                MessageCollection.TextClassifier_FollowingSimilarityIndexObjectHasBeenAddedToTheList.Invoke(ObjectMother.TextClassifier_Text3_SimilarityIndexes[0]),

                MessageCollection.TextClassifier_ComparingProvidedTextAgainstFollowingLabeledExample.Invoke(ObjectMother.TextClassifier_LabeledExamples[1]),
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

                MessageCollection.TextClassifier_PredictedLabelIs.Invoke(ObjectMother.TextClassifier_Text3_Label),
                MessageCollection.TextClassifier_PredictionHasFailedTryIncreasingTheAmountOfProvidedLabeledExamples

            };

            // Act
            TextClassifierResult actual
                = textClassifier.PredictLabel(
                                    ObjectMother.TextClassifier_Text3,
                                    ObjectMother.TextClassifier_LabeledExamples);

            // Assert
            Assert.IsTrue(
                    ObjectMother.AreEqual(
                        ObjectMother.TextClassifier_Text3_TextClassifierResult,
                        actual
                        ));
            Assert.AreEqual(expectedLogMessages, actualLogMessages);

        }

        // TearDown
        // Support methods

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 17.09.2021
*/