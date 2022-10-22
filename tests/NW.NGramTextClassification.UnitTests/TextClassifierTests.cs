﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using NW.NGramTextClassification.AsciiBanner;
using NW.NGramTextClassification.Filenames;
using NW.NGramTextClassification.Files;
using NW.NGramTextClassification.LabeledExamples;
using NW.NGramTextClassification.NGrams;
using NW.NGramTextClassification.NGramTokenization;
using NW.NGramTextClassification.Serializations;
using NW.NGramTextClassification.Similarity;
using NW.NGramTextClassification.TextClassifications;
using NW.NGramTextClassification.TextSnippets;
using NW.NGramTextClassification.UnitTests.Utilities;
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
                                        textSnippet: null,
                                        tokenizerRuleSet: NGramTokenization.ObjectMother.NGramTokenizerRuleSet_MonoBiTriFourFive,
                                        labeledExamples: LabeledExamples.ObjectMother.ShortLabeledExamples
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("textSnippet").Message
                ).SetArgDisplayNames($"{nameof(classifyOrDefaultExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .ClassifyOrDefault(
                                        textSnippet: new TextSnippet(text: LabeledExamples.ObjectMother.ShortLabeledExample01.Text),
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
                                        textSnippet: new TextSnippet(text: LabeledExamples.ObjectMother.ShortLabeledExample01.Text),
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
                    new TextSnippet(text: "/"),
                    new NGramTokenizerRuleSet(
                            doForMonogram: true,
                            doForBigram: true,
                            doForTrigram: true,
                            doForFourgram: true,
                            doForFivegram: true
                        ),
                    LabeledExamples.ObjectMother.CreateThirtyCompleteLabeledExamples(),
                    new TextClassifierSettings(),
                    TextClassifications.ObjectMother.TextClassifierSession_Default
                ).SetArgDisplayNames($"{nameof(classifyOrDefaultWhenAllRulesFailedTestCases)}_01"),

            new TestCaseData(
                    new TextSnippet(text: "hi"),
                    new NGramTokenizerRuleSet(
                            doForMonogram: false,
                            doForBigram: false,
                            doForTrigram: false,
                            doForFourgram: false,
                            doForFivegram: true
                        ),
                    LabeledExamples.ObjectMother.CreateThirtyCompleteLabeledExamples(),
                    new TextClassifierSettings(),
                    TextClassifications.ObjectMother.TextClassifierSession_Default
                ).SetArgDisplayNames($"{nameof(classifyOrDefaultWhenAllRulesFailedTestCases)}_02"),


        };
        private static TestCaseData[] classifyOrDefaultWhenUntokenizableExamples =
       {

            new TestCaseData(
                    new TextSnippet(
                            text: LabeledExamples.ObjectMother.CreateThirtyCompleteLabeledExamples()[0].Text),
                    NGramTokenization.ObjectMother.NGramTokenizerRuleSet_Five,
                    LabeledExamples.ObjectMother.ShortLabeledExamples_Untokenizable,
                    new TextClassifierSettings(),
                    TextClassifications.ObjectMother.TextClassifierSession_Default
                ).SetArgDisplayNames($"{nameof(classifyOrDefaultWhenUntokenizableExamples)}_01")

        };
        private static TestCaseData[] classifyOrDefaultWhenOneLabeledExampleAndSuccessfulClassification =
        {

            new TestCaseData(
                    new TextSnippet(
                            text: LabeledExamples.ObjectMother.CreateThirtyCompleteLabeledExamples()[0].Text),
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
                    new TextSnippet(
                            text: LabeledExamples.ObjectMother.CreateThirtyCompleteLabeledExamples()[0].Text),
                    new NGramTokenizerRuleSet(
                            doForMonogram: true,
                            doForBigram: true,
                            doForTrigram: true,
                            doForFourgram: true,
                            doForFivegram: true
                        ),
                    LabeledExamples.ObjectMother.CreateThirtyCompleteLabeledExamples(),
                    new TextClassifierSettings(
                          truncateTextInLogMessagesAfter: TextClassifierSettings.DefaultTruncateTextInLogMessagesAfter,
                          minimumAccuracySingleLabel: 0.0,
                          minimumAccuracyMultipleLabels: TextClassifierSettings.DefaultMinimumAccuracyMultipleLabels,
                          folderPath: TextClassifierSettings.DefaultFolderPath
                    ),
                    new TextClassifierSession(
                            settings: new TextClassifierSettings(
                                            truncateTextInLogMessagesAfter: TextClassifierSettings.DefaultTruncateTextInLogMessagesAfter,
                                            minimumAccuracySingleLabel: 0.0,
                                            minimumAccuracyMultipleLabels: TextClassifierSettings.DefaultMinimumAccuracyMultipleLabels,
                                            folderPath: TextClassifierSettings.DefaultFolderPath
                                        ),
                            results: TextClassifications.ObjectMother.TextClassifierResults_CompleteLabeledExamples00,
                            version: new TextClassifier().Version
                        )
                ).SetArgDisplayNames($"{nameof(classifyOrDefaultWhenThirtyLabeledExamplesAndSuccessfulClassification)}_01")

        };
        private static TestCaseData[] classifyManyExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .ClassifyMany(
                                        textSnippets: null,
                                        tokenizerRuleSet: NGramTokenization.ObjectMother.NGramTokenizerRuleSet_MonoBiTriFourFive,
                                        labeledExamples: LabeledExamples.ObjectMother.ShortLabeledExamples
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("textSnippets").Message
                ).SetArgDisplayNames($"{nameof(classifyManyExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .ClassifyMany(
                                        textSnippets: TextClassifications.ObjectMother.Snippets_CompleteLabeledExamples00.Select(text => new TextSnippet(text: text)).ToList(),
                                        tokenizerRuleSet: null,
                                        labeledExamples: LabeledExamples.ObjectMother.ShortLabeledExamples
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("tokenizerRuleSet").Message
                ).SetArgDisplayNames($"{nameof(classifyManyExceptionTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .ClassifyMany(
                                        textSnippets: TextClassifications.ObjectMother.Snippets_CompleteLabeledExamples00.Select(text => new TextSnippet(text: text)).ToList(),
                                        tokenizerRuleSet: NGramTokenization.ObjectMother.NGramTokenizerRuleSet_MonoBiTriFourFive,
                                        labeledExamples: null
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("labeledExamples").Message
                ).SetArgDisplayNames($"{nameof(classifyManyExceptionTestCases)}_03")

        };
        private static TestCaseData[] classifyManyTestCases =
        {

            new TestCaseData(
                    TextClassifications.ObjectMother.Snippets_CompleteLabeledExamples00.Select(text => new TextSnippet(text: text)).ToList(),
                    TextClassifier.DefaultNGramTokenizerRuleSet,
                    LabeledExamples.ObjectMother.CreateThirtyCompleteLabeledExamples(),
                    new TextClassifierSettings(
                          truncateTextInLogMessagesAfter: TextClassifierSettings.DefaultTruncateTextInLogMessagesAfter,
                          minimumAccuracySingleLabel: 0.0,
                          minimumAccuracyMultipleLabels: TextClassifierSettings.DefaultMinimumAccuracyMultipleLabels,
                          folderPath: TextClassifierSettings.DefaultFolderPath
                    ),
                    new TextClassifierSession(
                            settings: new TextClassifierSettings(
                                            truncateTextInLogMessagesAfter: TextClassifierSettings.DefaultTruncateTextInLogMessagesAfter,
                                            minimumAccuracySingleLabel: 0.0,
                                            minimumAccuracyMultipleLabels: TextClassifierSettings.DefaultMinimumAccuracyMultipleLabels,
                                            folderPath: TextClassifierSettings.DefaultFolderPath
                                        ),
                            results: TextClassifications.ObjectMother.TextClassifierResults_CompleteLabeledExamples00,
                            version: new TextClassifier().Version
                        )
                ).SetArgDisplayNames($"{nameof(classifyManyTestCases)}_01"),

            new TestCaseData(
                    TextClassifications.ObjectMother.Snippets_Untokenizable.Select(text => new TextSnippet(text: text)).ToList(),
                    NGramTokenization.ObjectMother.NGramTokenizerRuleSet_Five,
                    LabeledExamples.ObjectMother.CreateThirtyCompleteLabeledExamples(),
                    new TextClassifierSettings(
                          truncateTextInLogMessagesAfter: TextClassifierSettings.DefaultTruncateTextInLogMessagesAfter,
                          minimumAccuracySingleLabel: 0.0,
                          minimumAccuracyMultipleLabels: TextClassifierSettings.DefaultMinimumAccuracyMultipleLabels,
                          folderPath: TextClassifierSettings.DefaultFolderPath
                    ),
                    new TextClassifierSession(
                            settings: new TextClassifierSettings(
                                            truncateTextInLogMessagesAfter: TextClassifierSettings.DefaultTruncateTextInLogMessagesAfter,
                                            minimumAccuracySingleLabel: 0.0,
                                            minimumAccuracyMultipleLabels: TextClassifierSettings.DefaultMinimumAccuracyMultipleLabels,
                                            folderPath: TextClassifierSettings.DefaultFolderPath
                                        ),
                            results: TextClassifications.ObjectMother.TextClassifierResults_Untokenizable,
                            version: new TextClassifier().Version
                        )
                ).SetArgDisplayNames($"{nameof(classifyManyTestCases)}_02")

        };
        private static TestCaseData[] loadLabeledExamplesOrDefaultExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier().LoadLabeledExamplesOrDefault(jsonFile: (IFileInfoAdapter)null)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException("jsonFile").Message
            ).SetArgDisplayNames($"{nameof(loadLabeledExamplesOrDefaultExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier().LoadLabeledExamplesOrDefault(jsonFile: Files.ObjectMother.FileInfoAdapterDoesntExist)
                    ),
                typeof(ArgumentException),
                NGramTextClassification.Validation.MessageCollection.ProvidedPathDoesntExist(Files.ObjectMother.FileInfoAdapterDoesntExist)
            ).SetArgDisplayNames($"{nameof(loadLabeledExamplesOrDefaultExceptionTestCases)}_02")

        };
        private static TestCaseData[] loadTextSnippetsOrDefaultExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier().LoadTextSnippetsOrDefault(jsonFile: (IFileInfoAdapter)null)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException("jsonFile").Message
            ).SetArgDisplayNames($"{nameof(loadTextSnippetsOrDefaultExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier().LoadTextSnippetsOrDefault(jsonFile: Files.ObjectMother.FileInfoAdapterDoesntExist)
                    ),
                typeof(ArgumentException),
                NGramTextClassification.Validation.MessageCollection.ProvidedPathDoesntExist(Files.ObjectMother.FileInfoAdapterDoesntExist)
            ).SetArgDisplayNames($"{nameof(loadTextSnippetsOrDefaultExceptionTestCases)}_02")

        };
        private static TestCaseData[] loadTokenizerRuleSetOrDefaultExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier().LoadTokenizerRuleSetOrDefault(jsonFile: (IFileInfoAdapter)null)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException("jsonFile").Message
            ).SetArgDisplayNames($"{nameof(loadTokenizerRuleSetOrDefaultExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier().LoadTokenizerRuleSetOrDefault(jsonFile: Files.ObjectMother.FileInfoAdapterDoesntExist)
                    ),
                typeof(ArgumentException),
                NGramTextClassification.Validation.MessageCollection.ProvidedPathDoesntExist(Files.ObjectMother.FileInfoAdapterDoesntExist)
            ).SetArgDisplayNames($"{nameof(loadTokenizerRuleSetOrDefaultExceptionTestCases)}_02")

        };
        private static TestCaseData[] convertExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier().Convert(filePath: null)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException("filePath").Message
            ).SetArgDisplayNames($"{nameof(convertExceptionTestCases)}_01")

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
        public void ClassifyOrDefault_ShouldReturnExpectedTextClassifierSession_WhenAllRulesFailed
            (TextSnippet textSnippet, INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples, TextClassifierSettings settings, TextClassifierSession expected)
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
                          loggingActionAsciiBanner: TextClassifierComponents.DefaultLoggingActionAsciiBanner,
                          fileManager: new FileManager(),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: TextClassifierComponents.DefaultNowFunction);
            TextClassifier textClassifier = new TextClassifier(components, settings);

            List<string> expectedLogMessages = CreateWhenAllRulesFailed(textSnippet, tokenizerRuleSet, labeledExamples, components);

            // Act
            TextClassifierSession actual = textClassifier.ClassifyOrDefault(textSnippet, tokenizerRuleSet, labeledExamples);

            // Assert
            Assert.IsTrue(
                    TextClassifications.ObjectMother.AreEqual(expected, actual)
                );
            Assert.AreEqual(expectedLogMessages, actualLogMessages);

        }

        [TestCaseSource(nameof(classifyOrDefaultWhenUntokenizableExamples))]
        public void ClassifyOrDefault_ShouldReturnSessionWithDefaultClassifierResult_WhenUntokenizableExamples
            (TextSnippet textSnippet, INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples, TextClassifierSettings settings, TextClassifierSession expected)
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
                          loggingActionAsciiBanner: TextClassifierComponents.DefaultLoggingActionAsciiBanner,
                          fileManager: new FileManager(),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: TextClassifierComponents.DefaultNowFunction);
            TextClassifier textClassifier = new TextClassifier(components, settings);

            List<string> initialLogMessages = CreateWhenAllRulesFailed(textSnippet, tokenizerRuleSet, labeledExamples, components).GetRange(0, 5);
            // We skip all the messages in the middle, otherwise the test would be too complex.
            List<string> finalLogMessages = new List<string>()
            {

                NGramTextClassification.TextClassifications.MessageCollection.AtLeastOneLabeledExampleFailedTokenized

            };

            // Act
            TextClassifierSession actual = textClassifier.ClassifyOrDefault(textSnippet, tokenizerRuleSet, labeledExamples);

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
            (TextSnippet textSnippet, INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples, string expectedLabel)
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
                          loggingActionAsciiBanner: TextClassifierComponents.DefaultLoggingActionAsciiBanner, 
                          fileManager: new FileManager(),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: TextClassifierComponents.DefaultNowFunction);
            TextClassifier textClassifier = new TextClassifier(components, new TextClassifierSettings());

            List<string> initialLogMessages = CreateWhenAllRulesFailed(textSnippet, tokenizerRuleSet, labeledExamples, components).GetRange(0, 5);
            // We skip all the messages in the middle, otherwise the test would be too complex.
            List<string> finalLogMessages = new List<string>()
            {

                NGramTextClassification.TextClassifications.MessageCollection.ResultOfClassificationTaskIs(expectedLabel),
                NGramTextClassification.TextClassifications.MessageCollection.ClassificationTaskHasBeenSuccessful

            };

            // Act
            TextClassifierSession actual = textClassifier.ClassifyOrDefault(textSnippet, tokenizerRuleSet, labeledExamples);

            // Assert
            Assert.AreEqual(expectedLabel, actual.Results[0].Label);
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
        public void ClassifyOrDefault_ShouldReturnExpectedTextClassifierSession_WhenThirtyLabeledExamplesAndSuccessfulClassification
            (TextSnippet textSnippet, INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples, TextClassifierSettings settings, TextClassifierSession expected)
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
                          loggingActionAsciiBanner: TextClassifierComponents.DefaultLoggingActionAsciiBanner,
                          fileManager: new FileManager(),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: TextClassifierComponents.DefaultNowFunction);
            TextClassifier textClassifier = new TextClassifier(components, settings);

            List<string> initialLogMessages = CreateWhenAllRulesFailed(textSnippet, tokenizerRuleSet, labeledExamples, components).GetRange(0, 5);
            // We skip all the messages in the middle, otherwise the test would be too complex.
            List<string> finalLogMessages = new List<string>()
            {

                NGramTextClassification.TextClassifications.MessageCollection.ResultOfClassificationTaskIs(expected.Results[0].Label),
                NGramTextClassification.TextClassifications.MessageCollection.ClassificationTaskHasBeenSuccessful

            };

            // Act
            TextClassifierSession actual = textClassifier.ClassifyOrDefault(textSnippet, tokenizerRuleSet, labeledExamples);

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
        public void GetLabel_ShouldReturnNull_WhenAreAllIndexAveragesEqualToZeroReturnsTrue()
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
                          loggingActionAsciiBanner: TextClassifierComponents.DefaultLoggingActionAsciiBanner,
                          fileManager: new FileManager(),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: TextClassifierComponents.DefaultNowFunction);
            TextClassifierSettings settings
                = new TextClassifierSettings(
                          truncateTextInLogMessagesAfter: TextClassifierSettings.DefaultTruncateTextInLogMessagesAfter,
                          minimumAccuracySingleLabel: TextClassifierSettings.DefaultMinimumAccuracySingleLabel,
                          minimumAccuracyMultipleLabels: TextClassifierSettings.DefaultMinimumAccuracyMultipleLabels,
                          folderPath: TextClassifierSettings.DefaultFolderPath
                    );
            TextClassifier textClassifier = new TextClassifier(components, settings);

            List<SimilarityIndexAverage> indexAverages = new List<SimilarityIndexAverage>()
            {

                new SimilarityIndexAverage(label: "en", value: 0),
                new SimilarityIndexAverage(label: "sv", value: 0)

            };

            string expected = null;
            List<string> expectedLogMessages = new List<string>()
            {

               NGramTextClassification.TextClassifications.MessageCollection.FollowingVerificationReturnedTrue("AreAllIndexAveragesEqualToZero")

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
        public void GetLabel_ShouldReturnExpectedLabel_WhenIsSingleLabelAndHigherEqualThanMinimumAccuracyReturnsTrue()
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
                          loggingActionAsciiBanner: TextClassifierComponents.DefaultLoggingActionAsciiBanner,
                          fileManager: new FileManager(),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: TextClassifierComponents.DefaultNowFunction);
            TextClassifierSettings settings
                = new TextClassifierSettings(
                          truncateTextInLogMessagesAfter: TextClassifierSettings.DefaultTruncateTextInLogMessagesAfter,
                          minimumAccuracySingleLabel: TextClassifierSettings.DefaultMinimumAccuracySingleLabel,         // 0.98 >= 0.5 
                          minimumAccuracyMultipleLabels: TextClassifierSettings.DefaultMinimumAccuracyMultipleLabels,
                          folderPath: TextClassifierSettings.DefaultFolderPath
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
        public void GetLabel_ShouldReturnNull_WhenIsSingleLabelAndLessThanMinimumAccuracyReturnsTrue()
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
                          loggingActionAsciiBanner: TextClassifierComponents.DefaultLoggingActionAsciiBanner,
                          fileManager: new FileManager(),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: TextClassifierComponents.DefaultNowFunction);
            TextClassifierSettings settings
                = new TextClassifierSettings(
                          truncateTextInLogMessagesAfter: TextClassifierSettings.DefaultTruncateTextInLogMessagesAfter,
                          minimumAccuracySingleLabel: 1.0,                                                                 // 0.98 < 1.0 
                          minimumAccuracyMultipleLabels: TextClassifierSettings.DefaultMinimumAccuracyMultipleLabels,
                          folderPath: TextClassifierSettings.DefaultFolderPath
                    );
            TextClassifier textClassifier = new TextClassifier(components, settings);

            List<SimilarityIndexAverage> indexAverages = new List<SimilarityIndexAverage>()
            {

                new SimilarityIndexAverage(label: "sv", value: 0.98)

            };

            string expected = null;
            List<string> expectedLogMessages = new List<string>()
            {

                NGramTextClassification.TextClassifications.MessageCollection.FollowingVerificationReturnedFalse("AreAllIndexAveragesEqualToZero"),
                NGramTextClassification.TextClassifications.MessageCollection.FollowingVerificationReturnedFalse("IsSingleLabelAndHigherEqualThanMinimumAccuracy"),
                NGramTextClassification.TextClassifications.MessageCollection.FollowingVerificationReturnedTrue("IsSingleLabelAndLessThanMinimumAccuracy")

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
        public void GetLabel_ShouldReturnNull_WhenAreAllIndexAveragesSameValueReturnsTrue()
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
                          loggingActionAsciiBanner: TextClassifierComponents.DefaultLoggingActionAsciiBanner,
                          fileManager: new FileManager(),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: TextClassifierComponents.DefaultNowFunction);
            TextClassifierSettings settings
                = new TextClassifierSettings(
                          truncateTextInLogMessagesAfter: TextClassifierSettings.DefaultTruncateTextInLogMessagesAfter,
                          minimumAccuracySingleLabel: TextClassifierSettings.DefaultMinimumAccuracySingleLabel,
                          minimumAccuracyMultipleLabels: TextClassifierSettings.DefaultMinimumAccuracyMultipleLabels,
                          folderPath: TextClassifierSettings.DefaultFolderPath
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

                NGramTextClassification.TextClassifications.MessageCollection.FollowingVerificationReturnedFalse("AreAllIndexAveragesEqualToZero"),
                NGramTextClassification.TextClassifications.MessageCollection.FollowingVerificationReturnedFalse("IsSingleLabelAndHigherEqualThanMinimumAccuracy"),
                NGramTextClassification.TextClassifications.MessageCollection.FollowingVerificationReturnedFalse("IsSingleLabelAndLessThanMinimumAccuracy"),
                NGramTextClassification.TextClassifications.MessageCollection.FollowingVerificationReturnedTrue("AreAllIndexAveragesSameValue")

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
        public void GetLabel_ShouldReturnNull_WhenAreTwoHighestIndexAveragesSameValueReturnsTrue()
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
                          loggingActionAsciiBanner: TextClassifierComponents.DefaultLoggingActionAsciiBanner,
                          fileManager: new FileManager(),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: TextClassifierComponents.DefaultNowFunction);
            TextClassifierSettings settings
                = new TextClassifierSettings(
                          truncateTextInLogMessagesAfter: TextClassifierSettings.DefaultTruncateTextInLogMessagesAfter,
                          minimumAccuracySingleLabel: TextClassifierSettings.DefaultMinimumAccuracySingleLabel,         // 0.98 >= 0.5
                          minimumAccuracyMultipleLabels: TextClassifierSettings.DefaultMinimumAccuracyMultipleLabels,
                          folderPath: TextClassifierSettings.DefaultFolderPath
                    );
            TextClassifier textClassifier = new TextClassifier(components, settings);

            List<SimilarityIndexAverage> indexAverages = new List<SimilarityIndexAverage>()
            {

                new SimilarityIndexAverage(label: "sv", value: 0.98),
                new SimilarityIndexAverage(label: "en", value: 0.98),
                new SimilarityIndexAverage(label: "dk", value: 0.32)

            };

            string expected = null;
            List<string> expectedLogMessages = new List<string>()
            {

                NGramTextClassification.TextClassifications.MessageCollection.FollowingVerificationReturnedFalse("AreAllIndexAveragesEqualToZero"),
                NGramTextClassification.TextClassifications.MessageCollection.FollowingVerificationReturnedFalse("IsSingleLabelAndHigherEqualThanMinimumAccuracy"),
                NGramTextClassification.TextClassifications.MessageCollection.FollowingVerificationReturnedFalse("IsSingleLabelAndLessThanMinimumAccuracy"),
                NGramTextClassification.TextClassifications.MessageCollection.FollowingVerificationReturnedFalse("AreAllIndexAveragesSameValue"),
                NGramTextClassification.TextClassifications.MessageCollection.FollowingVerificationReturnedTrue("AreTwoHighestIndexAveragesSameValue")

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
        public void GetLabel_ShouldReturnNull_WhenIsLessThanMinimumAccuracyMultipleLabelsReturnsTrue()
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
                          loggingActionAsciiBanner: TextClassifierComponents.DefaultLoggingActionAsciiBanner,
                          fileManager: new FileManager(),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: TextClassifierComponents.DefaultNowFunction);
            TextClassifierSettings settings
                = new TextClassifierSettings(
                            truncateTextInLogMessagesAfter: TextClassifierSettings.DefaultTruncateTextInLogMessagesAfter,
                            minimumAccuracySingleLabel: TextClassifierSettings.DefaultMinimumAccuracySingleLabel,
                            minimumAccuracyMultipleLabels: 1.0,                                                            // 0.98 <= 1.0
                            folderPath: TextClassifierSettings.DefaultFolderPath
                        );
            TextClassifier textClassifier = new TextClassifier(components, settings);

            List<SimilarityIndexAverage> indexAverages = new List<SimilarityIndexAverage>()
            {

                new SimilarityIndexAverage(label: "sv", value: 0.98),
                new SimilarityIndexAverage(label: "en", value: 0.75),
                new SimilarityIndexAverage(label: "dk", value: 0.32)

            };

            string expected = null;
            List<string> expectedLogMessages = new List<string>()
            {

                NGramTextClassification.TextClassifications.MessageCollection.FollowingVerificationReturnedFalse("AreAllIndexAveragesEqualToZero"),
                NGramTextClassification.TextClassifications.MessageCollection.FollowingVerificationReturnedFalse("IsSingleLabelAndHigherEqualThanMinimumAccuracy"),
                NGramTextClassification.TextClassifications.MessageCollection.FollowingVerificationReturnedFalse("IsSingleLabelAndLessThanMinimumAccuracy"),
                NGramTextClassification.TextClassifications.MessageCollection.FollowingVerificationReturnedFalse("AreAllIndexAveragesSameValue"),
                NGramTextClassification.TextClassifications.MessageCollection.FollowingVerificationReturnedFalse("AreTwoHighestIndexAveragesSameValue"),
                NGramTextClassification.TextClassifications.MessageCollection.FollowingVerificationReturnedTrue("IsLessThanMinimumAccuracyMultipleLabels"),

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
        public void GetLabel_ShouldReturnExpectedLabel_WhenIsLessThanMinimumAccuracyMultipleLabelsReturnsFalse()
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
                          loggingActionAsciiBanner: TextClassifierComponents.DefaultLoggingActionAsciiBanner,
                          fileManager: new FileManager(),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: TextClassifierComponents.DefaultNowFunction);
            TextClassifierSettings settings
                = new TextClassifierSettings(
                          truncateTextInLogMessagesAfter: TextClassifierSettings.DefaultTruncateTextInLogMessagesAfter,
                          minimumAccuracySingleLabel: TextClassifierSettings.DefaultMinimumAccuracySingleLabel,
                          minimumAccuracyMultipleLabels: TextClassifierSettings.DefaultMinimumAccuracyMultipleLabels,        // 0.98 > 0.5
                          folderPath: TextClassifierSettings.DefaultFolderPath
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

                NGramTextClassification.TextClassifications.MessageCollection.FollowingVerificationReturnedFalse("AreAllIndexAveragesEqualToZero"),
                NGramTextClassification.TextClassifications.MessageCollection.FollowingVerificationReturnedFalse("IsSingleLabelAndHigherEqualThanMinimumAccuracy"),
                NGramTextClassification.TextClassifications.MessageCollection.FollowingVerificationReturnedFalse("IsSingleLabelAndLessThanMinimumAccuracy"),
                NGramTextClassification.TextClassifications.MessageCollection.FollowingVerificationReturnedFalse("AreAllIndexAveragesSameValue"),
                NGramTextClassification.TextClassifications.MessageCollection.FollowingVerificationReturnedFalse("AreTwoHighestIndexAveragesSameValue"),
                NGramTextClassification.TextClassifications.MessageCollection.FollowingVerificationReturnedFalse("IsLessThanMinimumAccuracyMultipleLabels"),
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
                          loggingActionAsciiBanner: fakeLoggerAsciiBanner,
                          fileManager: new FileManager(),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: TextClassifierComponents.DefaultNowFunction);
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
        public void ClassifyMany_ShouldReturnExpectedTextClassifierSession_WhenInvoked
            (List<TextSnippet> textSnippets, INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples, TextClassifierSettings settings, TextClassifierSession expected)
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
                          loggingActionAsciiBanner: TextClassifierComponents.DefaultLoggingActionAsciiBanner,
                          fileManager: new FileManager(),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: TextClassifierComponents.DefaultNowFunction);
            TextClassifier textClassifier = new TextClassifier(components, settings);

            List<string> expectedLogMessages = new List<string>()
            {

                NGramTextClassification.TextClassifications.MessageCollection.ProvidedSnippetsAre(textSnippets.Count)

            };

            // Act
            TextClassifierSession actual
                = textClassifier.ClassifyMany(textSnippets: textSnippets, tokenizerRuleSet: tokenizerRuleSet, labeledExamples: labeledExamples);

            // Assert
            Assert.IsTrue(
                    TextClassifications.ObjectMother.AreEqual(expected, actual)
                );
            Assert.AreEqual(expectedLogMessages[0], actualLogMessages[0]); // The only messages that it's different from ClassifyOrDefault() is the first one.

        }


        [TestCaseSource(nameof(loadLabeledExamplesOrDefaultExceptionTestCases))]
        public void LoadLabeledExamplesOrDefault_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void LoadLabeledExamplesOrDefault_ShouldReturnExpectedCollectionOfLabeledExamples_WhenProperJsonFileContent()
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
                          loggingActionAsciiBanner: TextClassifierComponents.DefaultLoggingActionAsciiBanner,
                          fileManager: new FakeFileManager(LabeledExamples.ObjectMother.ShortLabeledExamplesAsJson_Content),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: TextClassifierComponents.DefaultNowFunction);
            TextClassifier textClassifier = new TextClassifier(components, new TextClassifierSettings());

            IFileInfoAdapter fakeJsonFile = new FakeFileInfoAdapter(true, @"C:\LabeledExamples.json");
            List<string> expectedLogMessages = new List<string>()
            {

                NGramTextClassification.TextClassifications.MessageCollection.AttemptingToLoadObjectsFrom(typeof(LabeledExample), fakeJsonFile),
                NGramTextClassification.TextClassifications.MessageCollection.ObjectsSuccessfullyLoaded(typeof(LabeledExample))

            };

            // Act
            List<LabeledExample> actual = textClassifier.LoadLabeledExamplesOrDefault(fakeJsonFile);

            // Assert
            Assert.IsTrue(
                    LabeledExamples.ObjectMother.AreEqual(LabeledExamples.ObjectMother.ShortLabeledExamples, actual)
                );
            Assert.AreEqual(expectedLogMessages, actualLogMessages);

        }

        [Test]
        public void LoadLabeledExamplesOrDefault_ShouldReturnDefault_WhenUnproperJsonFileContent()
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
                          loggingActionAsciiBanner: TextClassifierComponents.DefaultLoggingActionAsciiBanner,
                          fileManager: new FakeFileManager("Unproper Json content"),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: TextClassifierComponents.DefaultNowFunction);
            TextClassifier textClassifier = new TextClassifier(components, new TextClassifierSettings());

            IFileInfoAdapter fakeJsonFile = new FakeFileInfoAdapter(true, @"C:\LabeledExamples.json");
            List<string> expectedLogMessages = new List<string>()
            {

                NGramTextClassification.TextClassifications.MessageCollection.AttemptingToLoadObjectsFrom(typeof(LabeledExample), fakeJsonFile),
                NGramTextClassification.TextClassifications.MessageCollection.ObjectsFailedToLoad(typeof(LabeledExample))

            };

            // Act
            List<LabeledExample> actual = textClassifier.LoadLabeledExamplesOrDefault(fakeJsonFile);

            // Assert
            Assert.AreEqual(Serializer<LabeledExample>.Default, actual);
            Assert.AreEqual(expectedLogMessages, actualLogMessages);

        }


        [TestCaseSource(nameof(loadTextSnippetsOrDefaultExceptionTestCases))]
        public void LoadTextSnippetsOrDefault_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void LoadTextSnippetsOrDefault_ShouldReturnExpectedCollectionOfTextSnippets_WhenProperJsonFileContent()
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
                          loggingActionAsciiBanner: TextClassifierComponents.DefaultLoggingActionAsciiBanner,
                          fileManager: new FakeFileManager(LabeledExamples.ObjectMother.ShortLabeledExamplesAsJson_Content),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: TextClassifierComponents.DefaultNowFunction);
            TextClassifier textClassifier = new TextClassifier(components, new TextClassifierSettings());

            IFileInfoAdapter fakeJsonFile = new FakeFileInfoAdapter(true, @"C:\TextSnippets.json");
            List<string> expectedLogMessages = new List<string>()
            {

                NGramTextClassification.TextClassifications.MessageCollection.AttemptingToLoadObjectsFrom(typeof(TextSnippet), fakeJsonFile),
                NGramTextClassification.TextClassifications.MessageCollection.ObjectsSuccessfullyLoaded(typeof(TextSnippet))

            };

            // Act
            List<TextSnippet> actual = textClassifier.LoadTextSnippetsOrDefault(fakeJsonFile);

            // Assert
            Assert.IsTrue(
                    TextSnippets.ObjectMother.AreEqual(TextSnippets.ObjectMother.TextSnippets, actual)
                );
            Assert.AreEqual(expectedLogMessages, actualLogMessages);

        }

        [Test]
        public void LoadTextSnippetsOrDefault_ShouldReturnDefault_WhenUnproperJsonFileContent()
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
                          loggingActionAsciiBanner: TextClassifierComponents.DefaultLoggingActionAsciiBanner,
                          fileManager: new FakeFileManager("Unproper Json content"),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: TextClassifierComponents.DefaultNowFunction);
            TextClassifier textClassifier = new TextClassifier(components, new TextClassifierSettings());

            IFileInfoAdapter fakeJsonFile = new FakeFileInfoAdapter(true, @"C:\TextSnippets.json");
            List<string> expectedLogMessages = new List<string>()
            {

                NGramTextClassification.TextClassifications.MessageCollection.AttemptingToLoadObjectsFrom(typeof(TextSnippet), fakeJsonFile),
                NGramTextClassification.TextClassifications.MessageCollection.ObjectsFailedToLoad(typeof(TextSnippet))

            };

            // Act
            List<TextSnippet> actual = textClassifier.LoadTextSnippetsOrDefault(fakeJsonFile);

            // Assert
            Assert.AreEqual(Serializer<TextSnippet>.Default, actual);
            Assert.AreEqual(expectedLogMessages, actualLogMessages);

        }


        [TestCaseSource(nameof(loadTokenizerRuleSetOrDefaultExceptionTestCases))]
        public void LoadTokenizerRuleSetOrDefault_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void LoadTokenizerRuleSetOrDefault_ShouldReturnExpectedTokenizerRuleSet_WhenProperJsonFileContent()
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
                          loggingActionAsciiBanner: TextClassifierComponents.DefaultLoggingActionAsciiBanner,
                          fileManager: new FakeFileManager(TextClassifications.ObjectMother.TokenizerRuleSetAsJson_Content),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: TextClassifierComponents.DefaultNowFunction);
            TextClassifier textClassifier = new TextClassifier(components, new TextClassifierSettings());

            IFileInfoAdapter fakeJsonFile = new FakeFileInfoAdapter(true, @"C:\TokenizerRuleSet.json");
            List<string> expectedLogMessages = new List<string>()
            {

                NGramTextClassification.TextClassifications.MessageCollection.AttemptingToLoadObjectFrom(typeof(NGramTokenizerRuleSet), fakeJsonFile),
                NGramTextClassification.TextClassifications.MessageCollection.ObjectSuccessfullyLoaded(typeof(NGramTokenizerRuleSet))

            };

            // Act
            NGramTokenizerRuleSet actual = textClassifier.LoadTokenizerRuleSetOrDefault(fakeJsonFile);

            // Assert
            Assert.IsTrue(
                    NGramTokenization.ObjectMother.AreEqual(TextClassifications.ObjectMother.TokenizerRuleSet, actual)
                );
            Assert.AreEqual(expectedLogMessages, actualLogMessages);

        }

        [Test]
        public void LoadTokenizerRuleSetOrDefault_ShouldReturnDefault_WhenUnproperJsonFileContent()
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
                          loggingActionAsciiBanner: TextClassifierComponents.DefaultLoggingActionAsciiBanner,
                          fileManager: new FakeFileManager("Unproper Json content"),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: TextClassifierComponents.DefaultNowFunction);
            TextClassifier textClassifier = new TextClassifier(components, new TextClassifierSettings());

            IFileInfoAdapter fakeJsonFile = new FakeFileInfoAdapter(true, @"C:\TokenizerRuleSet.json");
            List<string> expectedLogMessages = new List<string>()
            {

                NGramTextClassification.TextClassifications.MessageCollection.AttemptingToLoadObjectFrom(typeof(NGramTokenizerRuleSet), fakeJsonFile),
                NGramTextClassification.TextClassifications.MessageCollection.ObjectFailedToLoad(typeof(NGramTokenizerRuleSet))

            };

            // Act
            NGramTokenizerRuleSet actual = textClassifier.LoadTokenizerRuleSetOrDefault(fakeJsonFile);

            // Assert
            Assert.AreEqual(default(NGramTokenizerRuleSet), actual);
            Assert.AreEqual(expectedLogMessages, actualLogMessages);

        }


        [Test]
        public void SaveLabeledExamples_ShouldSaveCollectionOfLabeledExamples_WhenProperArguments()
        {

            // Arrange
            List<string> actualLogMessages = new List<string>();
            Action<string> fakeLoggingAction = (message) => actualLogMessages.Add(message);

            Func<DateTime> FakeNowFunction = () => Filenames.ObjectMother.FakeNow;

            TextClassifierComponents components
                = new TextClassifierComponents(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: TextClassifierComponents.DefaultRoundingFunction,
                          textTruncatingFunction: TextClassifierComponents.DefaultTextTruncatingFunction,
                          loggingAction: fakeLoggingAction,
                          labeledExampleManager: new LabeledExampleManager(),
                          asciiBannerManager: new AsciiBannerManager(),
                          loggingActionAsciiBanner: TextClassifierComponents.DefaultLoggingActionAsciiBanner,
                          fileManager: new FakeFileManager(LabeledExamples.ObjectMother.ShortLabeledExamplesAsJson_Content),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: FakeNowFunction);
            TextClassifier textClassifier = new TextClassifier(components, new TextClassifierSettings());

            string folderPath = Filenames.ObjectMother.FakeFilePath;
            string fileName = $"ngramtc_labeledexamples_{Filenames.ObjectMother.FakeNowString}.json";
            string filePath = Path.Combine(folderPath, fileName);
            IFileInfoAdapter fakeJsonFile = new FakeFileInfoAdapter(true, filePath);

            List<string> expectedLogMessages = new List<string>()
            {

                NGramTextClassification.TextClassifications.MessageCollection.AttemptingToSaveObjectsAs(typeof(LabeledExample), fakeJsonFile),
                NGramTextClassification.TextClassifications.MessageCollection.ObjectsSuccessfullySaved(typeof(LabeledExample))

            };

            // Act
            textClassifier.SaveLabeledExamples(LabeledExamples.ObjectMother.ShortLabeledExamples, folderPath);

            // Assert
            Assert.AreEqual(expectedLogMessages, actualLogMessages);

        }

        [Test]
        public void SaveTextSnippets_ShouldSaveCollectionOfTextSnippets_WhenProperArguments()
        {

            // Arrange
            List<string> actualLogMessages = new List<string>();
            Action<string> fakeLoggingAction = (message) => actualLogMessages.Add(message);

            Func<DateTime> FakeNowFunction = () => Filenames.ObjectMother.FakeNow;

            TextClassifierComponents components
                = new TextClassifierComponents(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: TextClassifierComponents.DefaultRoundingFunction,
                          textTruncatingFunction: TextClassifierComponents.DefaultTextTruncatingFunction,
                          loggingAction: fakeLoggingAction,
                          labeledExampleManager: new LabeledExampleManager(),
                          asciiBannerManager: new AsciiBannerManager(),
                          loggingActionAsciiBanner: TextClassifierComponents.DefaultLoggingActionAsciiBanner,
                          fileManager: new FakeFileManager(LabeledExamples.ObjectMother.ShortLabeledExamplesAsJson_Content),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: FakeNowFunction);
            TextClassifier textClassifier = new TextClassifier(components, new TextClassifierSettings());

            string folderPath = Filenames.ObjectMother.FakeFilePath;
            string fileName = $"ngramtc_textsnippets_{Filenames.ObjectMother.FakeNowString}.json";
            string filePath = Path.Combine(folderPath, fileName);
            IFileInfoAdapter fakeJsonFile = new FakeFileInfoAdapter(true, filePath);

            List<string> expectedLogMessages = new List<string>()
            {

                NGramTextClassification.TextClassifications.MessageCollection.AttemptingToSaveObjectsAs(typeof(TextSnippet), fakeJsonFile),
                NGramTextClassification.TextClassifications.MessageCollection.ObjectsSuccessfullySaved(typeof(TextSnippet))

            };

            // Act
            textClassifier.SaveTextSnippets(TextSnippets.ObjectMother.TextSnippets, folderPath);

            // Assert
            Assert.AreEqual(expectedLogMessages, actualLogMessages);

        }

        [Test]
        public void SaveSession_ShouldSaveSession_WhenProperArguments()
        {

            // Arrange
            List<string> actualLogMessages = new List<string>();
            Action<string> fakeLoggingAction = (message) => actualLogMessages.Add(message);

            Func<DateTime> FakeNowFunction = () => Filenames.ObjectMother.FakeNow;

            TextClassifierComponents components
                = new TextClassifierComponents(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: TextClassifierComponents.DefaultRoundingFunction,
                          textTruncatingFunction: TextClassifierComponents.DefaultTextTruncatingFunction,
                          loggingAction: fakeLoggingAction,
                          labeledExampleManager: new LabeledExampleManager(),
                          asciiBannerManager: new AsciiBannerManager(),
                          loggingActionAsciiBanner: TextClassifierComponents.DefaultLoggingActionAsciiBanner,
                          fileManager: new FakeFileManager(TextClassifications.ObjectMother.TextClassifierrSessionCLE00AsJson_Content),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: FakeNowFunction);
            TextClassifier textClassifier = new TextClassifier(components, new TextClassifierSettings());

            string folderPath = Filenames.ObjectMother.FakeFilePath;
            string fileName = $"ngramtc_session_{Filenames.ObjectMother.FakeNowString}.json";
            string filePath = Path.Combine(folderPath, fileName);
            IFileInfoAdapter fakeJsonFile = new FakeFileInfoAdapter(true, filePath);

            List<string> expectedLogMessages = new List<string>()
            {

                NGramTextClassification.TextClassifications.MessageCollection.AttemptingToSaveObjectAs(typeof(TextClassifierSession), fakeJsonFile),
                NGramTextClassification.TextClassifications.MessageCollection.ObjectSuccessfullySaved(typeof(TextClassifierSession))

            };

            // Act
            textClassifier.SaveSession(TextClassifications.ObjectMother.TextClassifierSession_CompleteLabeledExamples00, folderPath);

            // Assert
            Assert.AreEqual(expectedLogMessages, actualLogMessages);

        }


        [Test]
        public void SaveLabeledExamples_ShouldLogExpectedMessage_WhenWriteAllTextThrowsException()
        {

            // Arrange
            List<string> actualLogMessages = new List<string>();
            Action<string> fakeLoggingAction = (message) => actualLogMessages.Add(message);

            Func<DateTime> FakeNowFunction = () => Filenames.ObjectMother.FakeNow;

            TextClassifierComponents components
                = new TextClassifierComponents(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: TextClassifierComponents.DefaultRoundingFunction,
                          textTruncatingFunction: TextClassifierComponents.DefaultTextTruncatingFunction,
                          loggingAction: fakeLoggingAction,
                          labeledExampleManager: new LabeledExampleManager(),
                          asciiBannerManager: new AsciiBannerManager(),
                          loggingActionAsciiBanner: TextClassifierComponents.DefaultLoggingActionAsciiBanner,
                          fileManager: new FakeFileManagerThrowingWriteExceptions(
                                                content: LabeledExamples.ObjectMother.ShortLabeledExamplesAsJson_Content,
                                                writeExceptionMessage: "A random write-to-disk issue."),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: FakeNowFunction);
            TextClassifier textClassifier = new TextClassifier(components, new TextClassifierSettings());

            string folderPath = Filenames.ObjectMother.FakeFilePath;
            string fileName = $"ngramtc_labeledexamples_{Filenames.ObjectMother.FakeNowString}.json";
            string filePath = Path.Combine(folderPath, fileName);
            IFileInfoAdapter fakeJsonFile = new FakeFileInfoAdapter(true, filePath);

            List<string> expectedLogMessages = new List<string>()
            {

                NGramTextClassification.TextClassifications.MessageCollection.AttemptingToSaveObjectsAs(typeof(LabeledExample), fakeJsonFile),
                NGramTextClassification.TextClassifications.MessageCollection.ObjectsFailedToSave(typeof(LabeledExample))

            };

            // Act
            textClassifier.SaveLabeledExamples(LabeledExamples.ObjectMother.ShortLabeledExamples, folderPath);

            // Assert
            Assert.AreEqual(expectedLogMessages, actualLogMessages);

        }

        [Test]
        public void SaveTextSnippets_ShouldLogExpectedMessage_WhenWriteAllTextThrowsException()
        {

            // Arrange
            List<string> actualLogMessages = new List<string>();
            Action<string> fakeLoggingAction = (message) => actualLogMessages.Add(message);

            Func<DateTime> FakeNowFunction = () => Filenames.ObjectMother.FakeNow;

            TextClassifierComponents components
                = new TextClassifierComponents(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: TextClassifierComponents.DefaultRoundingFunction,
                          textTruncatingFunction: TextClassifierComponents.DefaultTextTruncatingFunction,
                          loggingAction: fakeLoggingAction,
                          labeledExampleManager: new LabeledExampleManager(),
                          asciiBannerManager: new AsciiBannerManager(),
                          loggingActionAsciiBanner: TextClassifierComponents.DefaultLoggingActionAsciiBanner,
                          fileManager: new FakeFileManagerThrowingWriteExceptions(
                                                content: LabeledExamples.ObjectMother.ShortLabeledExamplesAsJson_Content,
                                                writeExceptionMessage: "A random write-to-disk issue."),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: FakeNowFunction);
            TextClassifier textClassifier = new TextClassifier(components, new TextClassifierSettings());

            string folderPath = Filenames.ObjectMother.FakeFilePath;
            string fileName = $"ngramtc_textsnippets_{Filenames.ObjectMother.FakeNowString}.json";
            string filePath = Path.Combine(folderPath, fileName);
            IFileInfoAdapter fakeJsonFile = new FakeFileInfoAdapter(true, filePath);

            List<string> expectedLogMessages = new List<string>()
            {

                NGramTextClassification.TextClassifications.MessageCollection.AttemptingToSaveObjectsAs(typeof(TextSnippet), fakeJsonFile),
                NGramTextClassification.TextClassifications.MessageCollection.ObjectsFailedToSave(typeof(TextSnippet))

            };

            // Act
            textClassifier.SaveTextSnippets(TextSnippets.ObjectMother.TextSnippets, folderPath);

            // Assert
            Assert.AreEqual(expectedLogMessages, actualLogMessages);

        }

        [Test]
        public void SaveSession_ShouldLogExpectedMessage_WhenWriteAllTextThrowsException()
        {

            // Arrange
            List<string> actualLogMessages = new List<string>();
            Action<string> fakeLoggingAction = (message) => actualLogMessages.Add(message);

            Func<DateTime> FakeNowFunction = () => Filenames.ObjectMother.FakeNow;

            TextClassifierComponents components
                = new TextClassifierComponents(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: TextClassifierComponents.DefaultRoundingFunction,
                          textTruncatingFunction: TextClassifierComponents.DefaultTextTruncatingFunction,
                          loggingAction: fakeLoggingAction,
                          labeledExampleManager: new LabeledExampleManager(),
                          asciiBannerManager: new AsciiBannerManager(),
                          loggingActionAsciiBanner: TextClassifierComponents.DefaultLoggingActionAsciiBanner,
                          fileManager: new FakeFileManagerThrowingWriteExceptions(
                                                content: TextClassifications.ObjectMother.TextClassifierrSessionCLE00AsJson_Content,
                                                writeExceptionMessage: "A random write-to-disk issue."),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: FakeNowFunction);
            TextClassifier textClassifier = new TextClassifier(components, new TextClassifierSettings());

            string folderPath = Filenames.ObjectMother.FakeFilePath;
            string fileName = $"ngramtc_session_{Filenames.ObjectMother.FakeNowString}.json";
            string filePath = Path.Combine(folderPath, fileName);
            IFileInfoAdapter fakeJsonFile = new FakeFileInfoAdapter(true, filePath);

            List<string> expectedLogMessages = new List<string>()
            {

                NGramTextClassification.TextClassifications.MessageCollection.AttemptingToSaveObjectAs(typeof(TextClassifierSession), fakeJsonFile),
                NGramTextClassification.TextClassifications.MessageCollection.ObjectFailedToSave(typeof(TextClassifierSession))

            };

            // Act
            textClassifier.SaveSession(TextClassifications.ObjectMother.TextClassifierSession_CompleteLabeledExamples00, folderPath);

            // Assert
            Assert.AreEqual(expectedLogMessages, actualLogMessages);

        }


        [Test]
        public void Create_ShouldThrowExpectedException_WhenProvidedTypeIsNotSupported()
        {

            // Arrange
            TextClassifier textClassifier = new TextClassifier();

            try
            {

                // Act
                IFileInfoAdapter actual
                    = ObjectMother.CallPrivateGenericMethod<TextClassifier, IFileInfoAdapter>(
                            obj: textClassifier,
                            methodName: "Create",
                            args: new object[] { @"C:\", DateTime.Now },
                            methodType: typeof(Monogram) // "Monogram" is a not supported type.
                        );

            }
            catch(TargetInvocationException e)
            {

                // Assert
                Assert.IsInstanceOf<Exception>(e.InnerException);
                Assert.AreEqual(
                    NGramTextClassification.TextClassifications.MessageCollection.ThereIsNoStrategyOutOfType(typeof(Monogram)),
                    e.InnerException.Message);

            }

        }

        [TestCaseSource(nameof(convertExceptionTestCases))]
        public void Convert_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        #endregion

        #region TearDown
        #endregion

        #region SupportMethods

        private static List<string> CreateWhenAllRulesFailed
            (TextSnippet textSnippet, INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples, TextClassifierComponents components)
        {

            string expectedText
                = TextClassifierComponents.DefaultTextTruncatingFunction(
                        textSnippet.Text,
                        TextClassifierSettings.DefaultTruncateTextInLogMessagesAfter);

            List<INGram> expectedNGrams = components.NGramsTokenizer.DoForRuleSetOrDefault(textSnippet.Text, tokenizerRuleSet);

            List<string> expectedMessages = new List<string>()
            {

                NGramTextClassification.TextClassifications.MessageCollection.AttemptingToClassifyProvidedSnippet,
                NGramTextClassification.TextClassifications.MessageCollection.FollowingSnippetHasBeenProvided(expectedText),
                NGramTextClassification.TextClassifications.MessageCollection.FollowingNGramsTokenizerRuleSetWillBeUsed(tokenizerRuleSet),
                NGramTextClassification.TextClassifications.MessageCollection.XLabeledExamplesHaveBeenProvided(labeledExamples),
                NGramTextClassification.TextClassifications.MessageCollection.ProvidedTextHasBeenTokenizedIntoXNGrams(expectedNGrams),

                NGramTextClassification.TextClassifications.MessageCollection.AllRulesInProvidedRulesetFailed(expectedText)

            };

            return expectedMessages;

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 18.10.2022
*/