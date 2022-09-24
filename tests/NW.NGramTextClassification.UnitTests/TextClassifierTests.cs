using System;
using System.Collections.Generic;
using NW.NGramTextClassification.LabeledExamples;
using NW.NGramTextClassification.Messages;
using NW.NGramTextClassification.NGramTokenization;
using NW.NGramTextClassification.Similarity;
using NUnit.Framework;
using NW.NGramTextClassification.NGrams;

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
        private static TestCaseData[] predictLabelOrDefaultExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .PredictLabelOrDefault(
                                        text: null,
                                        tokenizerRuleSet: ObjectMother.Shared_RuleSet_MonoBiTriFourFive,
                                        labeledExamples: ObjectMother.Shared_LabeledExamples
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("text").Message
                ).SetArgDisplayNames($"{nameof(predictLabelOrDefaultExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .PredictLabelOrDefault(
                                        text: ObjectMother.Shared_LabeledExample01.Text,
                                        tokenizerRuleSet: null,
                                        labeledExamples: ObjectMother.Shared_LabeledExamples
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("tokenizerRuleSet").Message
                ).SetArgDisplayNames($"{nameof(predictLabelOrDefaultExceptionTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .PredictLabelOrDefault(
                                        text: ObjectMother.Shared_LabeledExample01.Text,
                                        tokenizerRuleSet: ObjectMother.Shared_RuleSet_MonoBiTriFourFive,
                                        labeledExamples: null
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("labeledExamples").Message
                ).SetArgDisplayNames($"{nameof(predictLabelOrDefaultExceptionTestCases)}_03")

        };
        private static TestCaseData[] predictLabelOrDefaultWhenAllRulesFailedTestCases =
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
                    ObjectMother.CreateThirtyLabeledExamples(),
                    new TextClassifierResult(
                            label: null,
                            indexes: new List<SimilarityIndex>(),
                            indexAverages: new List<SimilarityIndexAverage>()
                        )
                ).SetArgDisplayNames($"{nameof(predictLabelOrDefaultWhenAllRulesFailedTestCases)}_01"),

            new TestCaseData(
                    "hi",
                    new NGramTokenizerRuleSet(
                            doForMonogram: false,
                            doForBigram: false,
                            doForTrigram: false,
                            doForFourgram: false,
                            doForFivegram: true
                        ),
                    ObjectMother.CreateThirtyLabeledExamples(),
                    new TextClassifierResult(
                            label: null,
                            indexes: new List<SimilarityIndex>(),
                            indexAverages: new List<SimilarityIndexAverage>()
                        )
                ).SetArgDisplayNames($"{nameof(predictLabelOrDefaultWhenAllRulesFailedTestCases)}_02"),


        };

        private static TestCaseData[] predictLabelOrDefaultTestCases =
        {

            new TestCaseData(
                    ObjectMother.CreateThirtyLabeledExamples()[0].Text,
                    new NGramTokenizerRuleSet(
                            doForMonogram: true,
                            doForBigram: true,
                            doForTrigram: true,
                            doForFourgram: true,
                            doForFivegram: true
                        ),
                    ObjectMother.CreateThirtyLabeledExamples().GetRange(0, 1),
                    ObjectMother.TextClassifier_TextClassifierResult_LabeledExamples00
                ).SetArgDisplayNames($"{nameof(predictLabelOrDefaultTestCases)}_01")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(textClassifierExceptionTestCases))]
        public void TextClassifier_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(predictLabelOrDefaultExceptionTestCases))]
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

            Assert.IsInstanceOf<TextClassifierComponents>(TextClassifier.DefaultTextClassifierComponents);
            Assert.IsInstanceOf<TextClassifierSettings>(TextClassifier.DefaultTextClassifierSettings);
            Assert.IsInstanceOf<INGramTokenizerRuleSet>(TextClassifier.DefaultNGramTokenizerRuleSet);
            Assert.IsInstanceOf<TextClassifierResult>(TextClassifier.DefaultTextClassifierResult);

        }

        [TestCaseSource(nameof(predictLabelOrDefaultWhenAllRulesFailedTestCases))]
        public void PredictLabelOrDefault_ShouldReturnExpectedTextClassifierResult_WhenAllRulesFailed
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
                          labeledExampleManager: new LabeledExampleManager());
            TextClassifier textClassifier = new TextClassifier(components, new TextClassifierSettings());

            List<string> expectedLogMessages  = CreateWhenAllRulesFailed(text, tokenizerRuleSet, labeledExamples, components);

            // Act
            TextClassifierResult actual = textClassifier.PredictLabelOrDefault(text, tokenizerRuleSet, labeledExamples);

            // Assert
            Assert.IsTrue(
                    ObjectMother.AreEqual(expected, actual)
                );
            Assert.AreEqual(expectedLogMessages, actualLogMessages);

        }

        [TestCaseSource(nameof(predictLabelOrDefaultTestCases))]
        public void PredictLabelOrDefault_ShouldReturnExpectedTextClassifierResult_WhenProperArgument
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
                          labeledExampleManager: new LabeledExampleManager());
            TextClassifier textClassifier = new TextClassifier(components, new TextClassifierSettings());
            List<string> expectedLogMessages = CreateWhen(text, tokenizerRuleSet, labeledExamples, components);

            // Act
            TextClassifierResult actual = textClassifier.PredictLabelOrDefault(text, tokenizerRuleSet, labeledExamples);

            System.IO.File.WriteAllText(
                @"C:\Users\Rubèn\Desktop\actualLogMessages.json",
                System.Text.Json.JsonSerializer.Serialize(
                    actualLogMessages,
                    new System.Text.Json.JsonSerializerOptions()
                    {
                        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                        WriteIndented = true
                    }
                ));

            System.IO.File.WriteAllText(
                @"C:\Users\Rubèn\Desktop\expectedLogMessages.json",
                System.Text.Json.JsonSerializer.Serialize(
                    expectedLogMessages,
                    new System.Text.Json.JsonSerializerOptions()
                    {
                        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                        WriteIndented = true
                    }
                ));

            // Assert
            Assert.IsTrue(
                    ObjectMother.AreEqual(expected, actual)
                );
            Assert.AreEqual(expectedLogMessages, actualLogMessages);

        }

        [Test]
        public void PredictLabelOrDefault_ShouldReturnDefaultTextClassifierResult_WhenUnproperLabeledExamples()
        {

            // Arrange
            // Act
            // Assert

        }

        #endregion

        #region TearDown
        #endregion

        #region SupportMethods
        private static List<string> CreateWhenAllRulesFailed
            (string text, INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples, TextClassifierComponents components)
        {

            string expectedText
                = TextClassifierComponents.DefaultTextTruncatingFunction.Invoke(
                        text,
                        TextClassifierSettings.DefaultTruncateTextInLogMessagesAfter);

            List<INGram> expectedNGrams = components.NGramsTokenizer.DoForRuleSetOrDefault(text, tokenizerRuleSet);

            List<string> expectedLogMessages = new List<string>()
            {

                MessageCollection.TextClassifier_AttemptingToPredictLabel,
                MessageCollection.TextClassifier_FollowingTextHasBeenProvided(expectedText),
                MessageCollection.TextClassifier_FollowingNGramsTokenizerRuleSetWillBeUsed(tokenizerRuleSet),
                MessageCollection.TextClassifier_XLabeledExamplesHaveBeenProvided(labeledExamples),
                MessageCollection.TextClassifier_ProvidedTextHasBeenTokenizedIntoXNGrams(expectedNGrams),
                MessageCollection.TextClassifier_AllRulesInProvidedRulesetFailed(text)

            };

            return expectedLogMessages;

        }
        private static List<string> CreateWhen
            (string text, INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples, TextClassifierComponents components)
        {

            string expectedText
                = TextClassifierComponents.DefaultTextTruncatingFunction.Invoke(
                        text,
                        TextClassifierSettings.DefaultTruncateTextInLogMessagesAfter);

            List<INGram> expectedNGrams = components.NGramsTokenizer.DoForRuleSetOrDefault(text, tokenizerRuleSet);
            
            List<string> expectedLogMessages = new List<string>()
            {

                MessageCollection.TextClassifier_AttemptingToPredictLabel,
                MessageCollection.TextClassifier_FollowingTextHasBeenProvided(expectedText),
                MessageCollection.TextClassifier_FollowingNGramsTokenizerRuleSetWillBeUsed(tokenizerRuleSet),
                MessageCollection.TextClassifier_XLabeledExamplesHaveBeenProvided(labeledExamples),
                MessageCollection.TextClassifier_ProvidedTextHasBeenTokenizedIntoXNGrams(expectedNGrams),
                MessageCollection.TextClassifier_ProvidedLabeledExamplesThruTokenizationProcess,
                // MessageCollection.TextClassifier_ComparingProvidedTextAgainstFollowingTokenizedExample(expectedTokenizedExamples[0]),

                "The calculated 'SimilarityIndex' value is '1'.",
                "The rounded 'SimilarityIndex' value is '1'.",
                "The following SimilarityIndex object has been added to the list: '[ Text: 'VerksamhetsbeskrivningGoGift is a company which focuses on innovative gifts and gift cards solutions. GoGift has activities in every Nordic country and addresses both private as well as corporate customers. GoGift distributes gift cards for thousands of shops, brands and experiences delivered with post, email or SMS. The Super Gift Card, one of the most popular gifts, makes it possible for the gift recipient to choose their own gift at GoGift.com. With more than 7000 business to business customers worldwide GoGift is also a preferred supplier of corporate gifts.ArbetsuppgifterYou will play a very important role developing and maintaining our core platform as well as numerous APIs, applications and websites. You will be a key player in our dev team with highly skilled and experienced software developers (public and external), UX/UI designers, dev ops specialists and online content manager.', Label: 'en', Value: '1' ]'.",
                "The tokenized text has been successfully compared against the provided list of TokenizedExample objects.",
                "'1' SimilarityIndex objects have been computed.",
                "The following unique labels have been found in the provided SimilarityIndex list: '[en]'.",
                "Calculating 'SimilarityIndexAverage' for the following label: 'en'...",

                "The calculated 'SimilarityIndexAverage' value is '1'.",
                "The rounded 'SimilarityIndexAverage' value is '1'.",
                "The following SimilarityIndexAverage object has been added to the list: '[ Label: 'en', Value: '1' ]'.",
                "'1' SimilarityIndexAverage objects have been computed.",
                "The following verification has been successful: 'ContainsAtLeastOneIndexAverageThatIsNotZero'.",
                "The following verification has failed: 'ContainsAtLeastOneIndexAverageThatIsntEqualToTheOthers'.",

                "The predicted label is: ''.",
                "The prediction has failed. Try increasing the amount of provided LabeledExample objects.",

            };

            return expectedLogMessages;

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 24.09.2022
*/