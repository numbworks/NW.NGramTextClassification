using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using NW.NGramTextClassification.Filenames;
using NW.NGramTextClassification.Bags;
using NW.NGramTextClassification.LabeledExamples;
using NW.NGramTextClassification.NGrams;
using NW.NGramTextClassification.NGramTokenization;
using NW.NGramTextClassification.Similarity;
using NW.NGramTextClassification.TextClassifications;
using NW.NGramTextClassification.TextSnippets;
using NW.NGramTextClassification.UnitTests.Utilities;
using NW.Shared.Files;
using NW.Shared.Serialization;
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
                                        new SettingBag()
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("componentBag").Message
                ).SetArgDisplayNames($"{nameof(textClassifierExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier(
                                        new ComponentBag(),
                                        null
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("settingBag").Message
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
                    new SettingBag(),
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
                    new SettingBag(),
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
                    new SettingBag(),
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
                    new SettingBag(
                          truncateTextInLogMessagesAfter: SettingBag.DefaultTruncateTextInLogMessagesAfter,
                          minimumAccuracySingleLabel: 0.0,
                          minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels,
                          folderPath: SettingBag.DefaultFolderPath
                    ),
                    new TextClassifierSession(
                            settingBag: new SettingBag(
                                            truncateTextInLogMessagesAfter: SettingBag.DefaultTruncateTextInLogMessagesAfter,
                                            minimumAccuracySingleLabel: 0.0,
                                            minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels,
                                            folderPath: SettingBag.DefaultFolderPath
                                        ),
                            results: TextClassifications.ObjectMother.TextClassifierResults_CompleteLabeledExamples00,
                            version: ComponentBag.DefaultVersionFunction()
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
                    new SettingBag(
                          truncateTextInLogMessagesAfter: SettingBag.DefaultTruncateTextInLogMessagesAfter,
                          minimumAccuracySingleLabel: 0.0,
                          minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels,
                          folderPath: SettingBag.DefaultFolderPath
                    ),
                    new TextClassifierSession(
                            settingBag: new SettingBag(
                                            truncateTextInLogMessagesAfter: SettingBag.DefaultTruncateTextInLogMessagesAfter,
                                            minimumAccuracySingleLabel: 0.0,
                                            minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels,
                                            folderPath: SettingBag.DefaultFolderPath
                                        ),
                            results: TextClassifications.ObjectMother.TextClassifierResults_CompleteLabeledExamples00,
                            version: ComponentBag.DefaultVersionFunction()
                        )
                ).SetArgDisplayNames($"{nameof(classifyManyTestCases)}_01"),

            new TestCaseData(
                    TextClassifications.ObjectMother.Snippets_Untokenizable.Select(text => new TextSnippet(text: text)).ToList(),
                    NGramTokenization.ObjectMother.NGramTokenizerRuleSet_Five,
                    LabeledExamples.ObjectMother.CreateThirtyCompleteLabeledExamples(),
                    new SettingBag(
                          truncateTextInLogMessagesAfter: SettingBag.DefaultTruncateTextInLogMessagesAfter,
                          minimumAccuracySingleLabel: 0.0,
                          minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels,
                          folderPath: SettingBag.DefaultFolderPath
                    ),
                    new TextClassifierSession(
                            settingBag: new SettingBag(
                                            truncateTextInLogMessagesAfter: SettingBag.DefaultTruncateTextInLogMessagesAfter,
                                            minimumAccuracySingleLabel: 0.0,
                                            minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels,
                                            folderPath: SettingBag.DefaultFolderPath
                                        ),
                            results: TextClassifications.ObjectMother.TextClassifierResults_Untokenizable,
                            version: ComponentBag.DefaultVersionFunction()
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
                        () => new TextClassifier().LoadLabeledExamplesOrDefault(jsonFile: ObjectMother.FileInfoAdapterDoesntExist)
                    ),
            typeof(ArgumentException),
                Shared.Files.Validation.MessageCollection.ProvidedPathDoesntExist(ObjectMother.FileInfoAdapterDoesntExist)
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
                        () => new TextClassifier().LoadTextSnippetsOrDefault(jsonFile: ObjectMother.FileInfoAdapterDoesntExist)
                    ),
                typeof(ArgumentException),
                Shared.Files.Validation.MessageCollection.ProvidedPathDoesntExist(ObjectMother.FileInfoAdapterDoesntExist)
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
                        () => new TextClassifier().LoadTokenizerRuleSetOrDefault(jsonFile: ObjectMother.FileInfoAdapterDoesntExist)
                    ),
                typeof(ArgumentException),
                Shared.Files.Validation.MessageCollection.ProvidedPathDoesntExist(ObjectMother.FileInfoAdapterDoesntExist)
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
        private static TestCaseData[] cleanLabeledExamplesExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .CleanLabeledExamples(
                                        labeledExamples: null,
                                        tokenizerRuleSet: new NGramTokenizerRuleSet()
                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException("labeledExamples").Message
                ).SetArgDisplayNames($"{nameof(cleanLabeledExamplesExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .CleanLabeledExamples(
                                        labeledExamples: LabeledExamples.ObjectMother.ShortLabeledExamples,
                                        tokenizerRuleSet: null
                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException("tokenizerRuleSet").Message
                ).SetArgDisplayNames($"{nameof(cleanLabeledExamplesExceptionTestCases)}_02")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(textClassifierExceptionTestCases))]
        public void TextClassifier_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(classifyOrDefaultExceptionTestCases))]
        public void ClassifyOrDefault_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void TextClassifier_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            TextClassifier actual = new TextClassifier();

            // Assert
            Assert.That(actual, Is.InstanceOf<TextClassifier>());

            Assert.That(TextClassifier.DefaultComponentBag, Is.InstanceOf<ComponentBag>());
            Assert.That(TextClassifier.DefaultSettingBag, Is.InstanceOf<SettingBag>());
            Assert.That(TextClassifier.DefaultNGramTokenizerRuleSet, Is.InstanceOf<INGramTokenizerRuleSet>());
            Assert.That(TextClassifier.DefaultTextClassifierResult, Is.InstanceOf<TextClassifierResult>());
            Assert.That(TextClassifier.SimilarityIndexDisabler, Is.InstanceOf<Func<TextClassifierSession, dynamic>>());

        }


        [TestCaseSource(nameof(classifyOrDefaultWhenAllRulesFailedTestCases))]
        public void ClassifyOrDefault_ShouldReturnExpectedTextClassifierSession_WhenAllRulesFailed
            (TextSnippet textSnippet, INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples, SettingBag settingBag, TextClassifierSession expected)
        {

            // Arrange
            ComponentBag componentBag
                = new ComponentBag(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: ComponentBag.DefaultRoundingFunction,
                          textTruncatingFunction: ComponentBag.DefaultTextTruncatingFunction,
                          loggingAction: ComponentBag.DefaultLoggingAction,
                          labeledExampleManager: new LabeledExampleManager(),
                          loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                          fileManager: new FileManager(),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: ComponentBag.DefaultNowFunction,
                          versionFunction: ComponentBag.DefaultVersionFunction);
            TextClassifier textClassifier = new TextClassifier(componentBag, settingBag);

            // Act
            TextClassifierSession actual = textClassifier.ClassifyOrDefault(textSnippet, tokenizerRuleSet, labeledExamples);

            // Assert
            Assert.That(
                    TextClassifications.ObjectMother.AreEqual(expected, actual),
                    Is.True
                );

        }

        [TestCaseSource(nameof(classifyOrDefaultWhenUntokenizableExamples))]
        public void ClassifyOrDefault_ShouldReturnSessionWithDefaultClassifierResult_WhenUntokenizableExamples
            (TextSnippet textSnippet, INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples, SettingBag settingBag, TextClassifierSession expected)
        {

            // Arrange
            ComponentBag componentBag
                = new ComponentBag(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: ComponentBag.DefaultRoundingFunction,
                          textTruncatingFunction: ComponentBag.DefaultTextTruncatingFunction,
                          loggingAction: ComponentBag.DefaultLoggingAction,
                          labeledExampleManager: new LabeledExampleManager(),
                          loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                          fileManager: new FileManager(),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: ComponentBag.DefaultNowFunction,
                          versionFunction: ComponentBag.DefaultVersionFunction);
            TextClassifier textClassifier = new TextClassifier(componentBag, settingBag);

            // Act
            TextClassifierSession actual = textClassifier.ClassifyOrDefault(textSnippet, tokenizerRuleSet, labeledExamples);

            // Assert
            Assert.That(
                    TextClassifications.ObjectMother.AreEqual(expected, actual),
                    Is.True
                );

        }

        [TestCaseSource(nameof(classifyOrDefaultWhenOneLabeledExampleAndSuccessfulClassification))]
        public void ClassifyOrDefault_ShouldReturnExpectedLabel_WhenOneLabeledExampleAndSuccessfulClassification
            (TextSnippet textSnippet, INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples, string expectedLabel)
        {

            // Arrange
            ComponentBag componentBag
                = new ComponentBag(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: ComponentBag.DefaultRoundingFunction,
                          textTruncatingFunction: ComponentBag.DefaultTextTruncatingFunction,
                          loggingAction: ComponentBag.DefaultLoggingAction,
                          labeledExampleManager: new LabeledExampleManager(),
                          loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner, 
                          fileManager: new FileManager(),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: ComponentBag.DefaultNowFunction,
                          versionFunction: ComponentBag.DefaultVersionFunction);
            TextClassifier textClassifier = new TextClassifier(componentBag, new SettingBag());

            // Act
            TextClassifierSession actual = textClassifier.ClassifyOrDefault(textSnippet, tokenizerRuleSet, labeledExamples);

            // Assert
            Assert.That(expectedLabel, Is.EqualTo(actual.Results[0].Label));

        }

        [TestCaseSource(nameof(classifyOrDefaultWhenThirtyLabeledExamplesAndSuccessfulClassification))]
        public void ClassifyOrDefault_ShouldReturnExpectedTextClassifierSession_WhenThirtyLabeledExamplesAndSuccessfulClassification
            (TextSnippet textSnippet, INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples, SettingBag settingBag, TextClassifierSession expected)
        {

            // Arrange
            ComponentBag componentBag
                = new ComponentBag(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: ComponentBag.DefaultRoundingFunction,
                          textTruncatingFunction: ComponentBag.DefaultTextTruncatingFunction,
                          loggingAction: ComponentBag.DefaultLoggingAction,
                          labeledExampleManager: new LabeledExampleManager(),
                          loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                          fileManager: new FileManager(),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: ComponentBag.DefaultNowFunction,
                          versionFunction: ComponentBag.DefaultVersionFunction);
            TextClassifier textClassifier = new TextClassifier(componentBag, settingBag);

            // Act
            TextClassifierSession actual = textClassifier.ClassifyOrDefault(textSnippet, tokenizerRuleSet, labeledExamples);

            // Assert
            Assert.That(
                    TextClassifications.ObjectMother.AreEqual(expected, actual),
                    Is.True
                );

        }


        [Test]
        public void GetLabel_ShouldReturnNull_WhenAreAllIndexAveragesEqualToZeroReturnsTrue()
        {

            // Arrange
            ComponentBag componentBag
                = new ComponentBag(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: ComponentBag.DefaultRoundingFunction,
                          textTruncatingFunction: ComponentBag.DefaultTextTruncatingFunction,
                          loggingAction: ComponentBag.DefaultLoggingAction,
                          labeledExampleManager: new LabeledExampleManager(),
                          loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                          fileManager: new FileManager(),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: ComponentBag.DefaultNowFunction,
                          versionFunction: ComponentBag.DefaultVersionFunction);
            SettingBag settingBag
                = new SettingBag(
                          truncateTextInLogMessagesAfter: SettingBag.DefaultTruncateTextInLogMessagesAfter,
                          minimumAccuracySingleLabel: SettingBag.DefaultMinimumAccuracySingleLabel,
                          minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels,
                          folderPath: SettingBag.DefaultFolderPath
                    );
            TextClassifier textClassifier = new TextClassifier(componentBag, settingBag);

            List<SimilarityIndexAverage> indexAverages = new List<SimilarityIndexAverage>()
            {

                new SimilarityIndexAverage(label: "en", value: 0),
                new SimilarityIndexAverage(label: "sv", value: 0)

            };

            string expected = null;

            // Act
            string actual
                = ObjectMother.CallPrivateMethod<TextClassifier, string>(
                        obj: textClassifier,
                        methodName: "GetLabel",
                        args: [indexAverages]
                    );

            // Assert
            Assert.That(expected, Is.EqualTo(actual));

        }

        [Test]
        public void GetLabel_ShouldReturnExpectedLabel_WhenIsSingleLabelAndHigherEqualThanMinimumAccuracyReturnsTrue()
        {

            // Arrange
            ComponentBag componentBag
                = new ComponentBag(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: ComponentBag.DefaultRoundingFunction,
                          textTruncatingFunction: ComponentBag.DefaultTextTruncatingFunction,
                          loggingAction: ComponentBag.DefaultLoggingAction,
                          labeledExampleManager: new LabeledExampleManager(),
                          loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                          fileManager: new FileManager(),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: ComponentBag.DefaultNowFunction,
                          versionFunction: ComponentBag.DefaultVersionFunction);
            SettingBag settingBag
                = new SettingBag(
                          truncateTextInLogMessagesAfter: SettingBag.DefaultTruncateTextInLogMessagesAfter,
                          minimumAccuracySingleLabel: SettingBag.DefaultMinimumAccuracySingleLabel,         // 0.98 >= 0.5 
                          minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels,
                          folderPath: SettingBag.DefaultFolderPath
                    );
            TextClassifier textClassifier = new TextClassifier(componentBag, settingBag);

            List<SimilarityIndexAverage> indexAverages = new List<SimilarityIndexAverage>()
            {

                new SimilarityIndexAverage(label: "sv", value: 0.98)

            };

            string expected = "sv";

            // Act
            string actual
                = ObjectMother.CallPrivateMethod<TextClassifier, string>(
                        obj: textClassifier,
                        methodName: "GetLabel",
                        args: [indexAverages]
                    );

            // Assert
            Assert.That(expected, Is.EqualTo(actual));

        }

        [Test]
        public void GetLabel_ShouldReturnNull_WhenIsSingleLabelAndLessThanMinimumAccuracyReturnsTrue()
        {

            // Arrange
            ComponentBag componentBag
                = new ComponentBag(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: ComponentBag.DefaultRoundingFunction,
                          textTruncatingFunction: ComponentBag.DefaultTextTruncatingFunction,
                          loggingAction: ComponentBag.DefaultLoggingAction,
                          labeledExampleManager: new LabeledExampleManager(),
                          loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                          fileManager: new FileManager(),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: ComponentBag.DefaultNowFunction,
                          versionFunction: ComponentBag.DefaultVersionFunction);
            SettingBag settingBag
                = new SettingBag(
                          truncateTextInLogMessagesAfter: SettingBag.DefaultTruncateTextInLogMessagesAfter,
                          minimumAccuracySingleLabel: 1.0,                                                                 // 0.98 < 1.0 
                          minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels,
                          folderPath: SettingBag.DefaultFolderPath
                    );
            TextClassifier textClassifier = new TextClassifier(componentBag, settingBag);

            List<SimilarityIndexAverage> indexAverages = new List<SimilarityIndexAverage>()
            {

                new SimilarityIndexAverage(label: "sv", value: 0.98)

            };

            string expected = null;

            // Act
            string actual
                = ObjectMother.CallPrivateMethod<TextClassifier, string>(
                        obj: textClassifier,
                        methodName: "GetLabel",
                        args: [indexAverages]
                    );

            // Assert
            Assert.That(expected, Is.EqualTo(actual));

        }

        [Test]
        public void GetLabel_ShouldReturnNull_WhenAreAllIndexAveragesSameValueReturnsTrue()
        {

            // Arrange
            ComponentBag componentBag
                = new ComponentBag(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: ComponentBag.DefaultRoundingFunction,
                          textTruncatingFunction: ComponentBag.DefaultTextTruncatingFunction,
                          loggingAction: ComponentBag.DefaultLoggingAction,
                          labeledExampleManager: new LabeledExampleManager(),
                          loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                          fileManager: new FileManager(),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: ComponentBag.DefaultNowFunction,
                          versionFunction: ComponentBag.DefaultVersionFunction);
            SettingBag settingBag
                = new SettingBag(
                          truncateTextInLogMessagesAfter: SettingBag.DefaultTruncateTextInLogMessagesAfter,
                          minimumAccuracySingleLabel: SettingBag.DefaultMinimumAccuracySingleLabel,
                          minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels,
                          folderPath: SettingBag.DefaultFolderPath
                    );
            TextClassifier textClassifier = new TextClassifier(componentBag, settingBag);

            List<SimilarityIndexAverage> indexAverages = new List<SimilarityIndexAverage>()
            {

                new SimilarityIndexAverage(label: "en", value: 0.1),
                new SimilarityIndexAverage(label: "sv", value: 0.1),
                new SimilarityIndexAverage(label: "dk", value: 0.1)

            };

            string expected = null;

            // Act
            string actual
                = ObjectMother.CallPrivateMethod<TextClassifier, string>(
                        obj: textClassifier,
                        methodName: "GetLabel",
                        args: [indexAverages]
                    );

            // Assert
            Assert.That(expected, Is.EqualTo(actual));

        }

        [Test]
        public void GetLabel_ShouldReturnNull_WhenAreTwoHighestIndexAveragesSameValueReturnsTrue()
        {

            // Arrange
            ComponentBag componentBag
                = new ComponentBag(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: ComponentBag.DefaultRoundingFunction,
                          textTruncatingFunction: ComponentBag.DefaultTextTruncatingFunction,
                          loggingAction: ComponentBag.DefaultLoggingAction,
                          labeledExampleManager: new LabeledExampleManager(),
                          loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                          fileManager: new FileManager(),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: ComponentBag.DefaultNowFunction,
                          versionFunction: ComponentBag.DefaultVersionFunction);
            SettingBag settingBag
                = new SettingBag(
                          truncateTextInLogMessagesAfter: SettingBag.DefaultTruncateTextInLogMessagesAfter,
                          minimumAccuracySingleLabel: SettingBag.DefaultMinimumAccuracySingleLabel,         // 0.98 >= 0.5
                          minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels,
                          folderPath: SettingBag.DefaultFolderPath
                    );
            TextClassifier textClassifier = new TextClassifier(componentBag, settingBag);

            List<SimilarityIndexAverage> indexAverages = new List<SimilarityIndexAverage>()
            {

                new SimilarityIndexAverage(label: "sv", value: 0.98),
                new SimilarityIndexAverage(label: "en", value: 0.98),
                new SimilarityIndexAverage(label: "dk", value: 0.32)

            };

            string expected = null;

            // Act
            string actual
                = ObjectMother.CallPrivateMethod<TextClassifier, string>(
                        obj: textClassifier,
                        methodName: "GetLabel",
                        args: [indexAverages]
                    );

            // Assert
            Assert.That(expected, Is.EqualTo(actual));

        }

        [Test]
        public void GetLabel_ShouldReturnNull_WhenIsLessThanMinimumAccuracyMultipleLabelsReturnsTrue()
        {

            // Arrange
            ComponentBag componentBag
                = new ComponentBag(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: ComponentBag.DefaultRoundingFunction,
                          textTruncatingFunction: ComponentBag.DefaultTextTruncatingFunction,
                          loggingAction: ComponentBag.DefaultLoggingAction,
                          labeledExampleManager: new LabeledExampleManager(),
                          loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                          fileManager: new FileManager(),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: ComponentBag.DefaultNowFunction,
                          versionFunction: ComponentBag.DefaultVersionFunction);
            SettingBag settingBag
                = new SettingBag(
                            truncateTextInLogMessagesAfter: SettingBag.DefaultTruncateTextInLogMessagesAfter,
                            minimumAccuracySingleLabel: SettingBag.DefaultMinimumAccuracySingleLabel,
                            minimumAccuracyMultipleLabels: 1.0,                                                            // 0.98 <= 1.0
                            folderPath: SettingBag.DefaultFolderPath
                        );
            TextClassifier textClassifier = new TextClassifier(componentBag, settingBag);

            List<SimilarityIndexAverage> indexAverages = new List<SimilarityIndexAverage>()
            {

                new SimilarityIndexAverage(label: "sv", value: 0.98),
                new SimilarityIndexAverage(label: "en", value: 0.75),
                new SimilarityIndexAverage(label: "dk", value: 0.32)

            };

            string expected = null;

            // Act
            string actual
                = ObjectMother.CallPrivateMethod<TextClassifier, string>(
                        obj: textClassifier,
                        methodName: "GetLabel",
                        args: [indexAverages]
                    );

            // Assert
            Assert.That(expected, Is.EqualTo(actual));

        }

        [Test]
        public void GetLabel_ShouldReturnExpectedLabel_WhenIsLessThanMinimumAccuracyMultipleLabelsReturnsFalse()
        {

            // Arrange
            ComponentBag componentBag
                = new ComponentBag(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: ComponentBag.DefaultRoundingFunction,
                          textTruncatingFunction: ComponentBag.DefaultTextTruncatingFunction,
                          loggingAction: ComponentBag.DefaultLoggingAction,
                          labeledExampleManager: new LabeledExampleManager(),
                          loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                          fileManager: new FileManager(),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: ComponentBag.DefaultNowFunction,
                          versionFunction: ComponentBag.DefaultVersionFunction);
            SettingBag settingBag
                = new SettingBag(
                          truncateTextInLogMessagesAfter: SettingBag.DefaultTruncateTextInLogMessagesAfter,
                          minimumAccuracySingleLabel: SettingBag.DefaultMinimumAccuracySingleLabel,
                          minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels,        // 0.98 > 0.5
                          folderPath: SettingBag.DefaultFolderPath
                    );
            TextClassifier textClassifier = new TextClassifier(componentBag, settingBag);

            List<SimilarityIndexAverage> indexAverages = new List<SimilarityIndexAverage>()
            {

                new SimilarityIndexAverage(label: "sv", value: 0.98),
                new SimilarityIndexAverage(label: "en", value: 0.75),
                new SimilarityIndexAverage(label: "dk", value: 0.32)

            };

            string expected = "sv";

            // Act
            string actual
                = ObjectMother.CallPrivateMethod<TextClassifier, string>(
                        obj: textClassifier,
                        methodName: "GetLabel",
                        args: [indexAverages]
                    );

            // Assert
            Assert.That(expected, Is.EqualTo(actual));

        }

        [TestCaseSource(nameof(classifyManyExceptionTestCases))]
        public void ClassifyMany_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(classifyManyTestCases))]
        public void ClassifyMany_ShouldReturnExpectedTextClassifierSession_WhenInvoked
            (List<TextSnippet> textSnippets, INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples, SettingBag settingBag, TextClassifierSession expected)
        {

            // Arrange
            ComponentBag componentBag
                = new ComponentBag(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: ComponentBag.DefaultRoundingFunction,
                          textTruncatingFunction: ComponentBag.DefaultTextTruncatingFunction,
                          loggingAction: ComponentBag.DefaultLoggingAction,
                          labeledExampleManager: new LabeledExampleManager(),
                          loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                          fileManager: new FileManager(),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: ComponentBag.DefaultNowFunction,
                          versionFunction: ComponentBag.DefaultVersionFunction);
            TextClassifier textClassifier = new TextClassifier(componentBag, settingBag);

            // Act
            TextClassifierSession actual
                = textClassifier.ClassifyMany(textSnippets: textSnippets, tokenizerRuleSet: tokenizerRuleSet, labeledExamples: labeledExamples);

            // Assert
            Assert.That(
                    TextClassifications.ObjectMother.AreEqual(expected, actual),
                    Is.True
                );

        }


        [TestCaseSource(nameof(loadLabeledExamplesOrDefaultExceptionTestCases))]
        public void LoadLabeledExamplesOrDefault_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void LoadLabeledExamplesOrDefault_ShouldReturnExpectedCollectionOfLabeledExamples_WhenProperJsonFileContent()
        {

            // Arrange
            ComponentBag componentBag
                = new ComponentBag(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: ComponentBag.DefaultRoundingFunction,
                          textTruncatingFunction: ComponentBag.DefaultTextTruncatingFunction,
                          loggingAction: ComponentBag.DefaultLoggingAction,
                          labeledExampleManager: new LabeledExampleManager(),
                          loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                          fileManager: new FakeFileManager(LabeledExamples.ObjectMother.ShortLabeledExamplesAsJson_Content),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: ComponentBag.DefaultNowFunction,
                          versionFunction: ComponentBag.DefaultVersionFunction);
            TextClassifier textClassifier = new TextClassifier(componentBag, new SettingBag());

            IFileInfoAdapter fakeJsonFile = new FakeFileInfoAdapter(true, @"C:\LabeledExamples.json");

            // Act
            List<LabeledExample> actual = textClassifier.LoadLabeledExamplesOrDefault(fakeJsonFile);

            // Assert
            Assert.That(
                    LabeledExamples.ObjectMother.AreEqual(LabeledExamples.ObjectMother.ShortLabeledExamples, actual),
                    Is.True
                );

        }

        [Test]
        public void LoadLabeledExamplesOrDefault_ShouldReturnDefault_WhenUnproperJsonFileContent()
        {

            // Arrange
            ComponentBag componentBag
                = new ComponentBag(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: ComponentBag.DefaultRoundingFunction,
                          textTruncatingFunction: ComponentBag.DefaultTextTruncatingFunction,
                          loggingAction: ComponentBag.DefaultLoggingAction,
                          labeledExampleManager: new LabeledExampleManager(),
                          loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                          fileManager: new FakeFileManager("Unproper Json content"),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: ComponentBag.DefaultNowFunction,
                          versionFunction: ComponentBag.DefaultVersionFunction);
            TextClassifier textClassifier = new TextClassifier(componentBag, new SettingBag());

            IFileInfoAdapter fakeJsonFile = new FakeFileInfoAdapter(true, @"C:\LabeledExamples.json");

            // Act
            List<LabeledExample> actual = textClassifier.LoadLabeledExamplesOrDefault(fakeJsonFile);

            // Assert
            Assert.That(Serializer<LabeledExample>.Default, Is.EqualTo(actual));

        }


        [TestCaseSource(nameof(loadTextSnippetsOrDefaultExceptionTestCases))]
        public void LoadTextSnippetsOrDefault_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void LoadTextSnippetsOrDefault_ShouldReturnExpectedCollectionOfTextSnippets_WhenProperJsonFileContent()
        {

            // Arrange
            ComponentBag componentBag
                = new ComponentBag(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: ComponentBag.DefaultRoundingFunction,
                          textTruncatingFunction: ComponentBag.DefaultTextTruncatingFunction,
                          loggingAction: ComponentBag.DefaultLoggingAction,
                          labeledExampleManager: new LabeledExampleManager(),
                          loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                          fileManager: new FakeFileManager(LabeledExamples.ObjectMother.ShortLabeledExamplesAsJson_Content),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: ComponentBag.DefaultNowFunction,
                          versionFunction: ComponentBag.DefaultVersionFunction);
            TextClassifier textClassifier = new TextClassifier(componentBag, new SettingBag());

            IFileInfoAdapter fakeJsonFile = new FakeFileInfoAdapter(true, @"C:\TextSnippets.json");

            // Act
            List<TextSnippet> actual = textClassifier.LoadTextSnippetsOrDefault(fakeJsonFile);

            // Assert
            Assert.That(
                    TextSnippets.ObjectMother.AreEqual(TextSnippets.ObjectMother.TextSnippets, actual),
                    Is.True
                );

        }

        [Test]
        public void LoadTextSnippetsOrDefault_ShouldReturnDefault_WhenUnproperJsonFileContent()
        {

            // Arrange
            ComponentBag componentBag
                = new ComponentBag(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: ComponentBag.DefaultRoundingFunction,
                          textTruncatingFunction: ComponentBag.DefaultTextTruncatingFunction,
                          loggingAction: ComponentBag.DefaultLoggingAction,
                          labeledExampleManager: new LabeledExampleManager(),
                          loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                          fileManager: new FakeFileManager("Unproper Json content"),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: ComponentBag.DefaultNowFunction,
                          versionFunction: ComponentBag.DefaultVersionFunction);
            TextClassifier textClassifier = new TextClassifier(componentBag, new SettingBag());

            IFileInfoAdapter fakeJsonFile = new FakeFileInfoAdapter(true, @"C:\TextSnippets.json");

            // Act
            List<TextSnippet> actual = textClassifier.LoadTextSnippetsOrDefault(fakeJsonFile);

            // Assert
            Assert.That(Serializer<TextSnippet>.Default, Is.EqualTo(actual));

        }


        [TestCaseSource(nameof(loadTokenizerRuleSetOrDefaultExceptionTestCases))]
        public void LoadTokenizerRuleSetOrDefault_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void LoadTokenizerRuleSetOrDefault_ShouldReturnExpectedTokenizerRuleSet_WhenProperJsonFileContent()
        {

            // Arrange
            ComponentBag componentBag
                = new ComponentBag(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: ComponentBag.DefaultRoundingFunction,
                          textTruncatingFunction: ComponentBag.DefaultTextTruncatingFunction,
                          loggingAction: ComponentBag.DefaultLoggingAction,
                          labeledExampleManager: new LabeledExampleManager(),
                          loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                          fileManager: new FakeFileManager(TextClassifications.ObjectMother.TokenizerRuleSetAsJson_Content),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: ComponentBag.DefaultNowFunction,
                          versionFunction: ComponentBag.DefaultVersionFunction);
            TextClassifier textClassifier = new TextClassifier(componentBag, new SettingBag());

            IFileInfoAdapter fakeJsonFile = new FakeFileInfoAdapter(true, @"C:\TokenizerRuleSet.json");

            // Act
            NGramTokenizerRuleSet actual = textClassifier.LoadTokenizerRuleSetOrDefault(fakeJsonFile);

            // Assert
            Assert.That(
                    NGramTokenization.ObjectMother.AreEqual(TextClassifications.ObjectMother.TokenizerRuleSet, actual),
                    Is.True
                );

        }

        [Test]
        public void LoadTokenizerRuleSetOrDefault_ShouldReturnDefault_WhenUnproperJsonFileContent()
        {

            // Arrange
            ComponentBag componentBag
                = new ComponentBag(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: ComponentBag.DefaultRoundingFunction,
                          textTruncatingFunction: ComponentBag.DefaultTextTruncatingFunction,
                          loggingAction: ComponentBag.DefaultLoggingAction,
                          labeledExampleManager: new LabeledExampleManager(),
                          loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                          fileManager: new FakeFileManager("Unproper Json content"),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: ComponentBag.DefaultNowFunction,
                          versionFunction: ComponentBag.DefaultVersionFunction);
            TextClassifier textClassifier = new TextClassifier(componentBag, new SettingBag());

            IFileInfoAdapter fakeJsonFile = new FakeFileInfoAdapter(true, @"C:\TokenizerRuleSet.json");

            // Act
            NGramTokenizerRuleSet actual = textClassifier.LoadTokenizerRuleSetOrDefault(fakeJsonFile);

            // Assert
            Assert.That(actual, Is.EqualTo(default(NGramTokenizerRuleSet)));

        }


        [Test]
        public void SaveLabeledExamples_ShouldSaveCollectionOfLabeledExamplesAndNotThrowException_WhenProperArguments()
        {

            // Arrange
            Func<DateTime> fakeNowFunction = () => Filenames.ObjectMother.FakeNow;
            Func<string> fakeVersionFunction = () => "1.0.0";

            ComponentBag componentBag
                = new ComponentBag(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: ComponentBag.DefaultRoundingFunction,
                          textTruncatingFunction: ComponentBag.DefaultTextTruncatingFunction,
                          loggingAction: ComponentBag.DefaultLoggingAction,
                          labeledExampleManager: new LabeledExampleManager(),
                          loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                          fileManager: new FakeFileManager(LabeledExamples.ObjectMother.ShortLabeledExamplesAsJson_Content),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: fakeNowFunction,
                          versionFunction: fakeVersionFunction);

            string folderPath = Path.GetFullPath(Filenames.ObjectMother.FakeFilePath);
            string fileName = $"nwngram_labeledexamples_{Filenames.ObjectMother.FakeNowString}.json";
            string filePath = Path.Combine(folderPath, fileName);
            IFileInfoAdapter fakeJsonFile = new FakeFileInfoAdapter(true, filePath);

            SettingBag settingBag = new SettingBag(
                    truncateTextInLogMessagesAfter: SettingBag.DefaultTruncateTextInLogMessagesAfter,
                    minimumAccuracySingleLabel: SettingBag.DefaultMinimumAccuracySingleLabel,
                    minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels, 
                    folderPath: folderPath
            );

            TextClassifier textClassifier = new TextClassifier(componentBag, settingBag);

            // Act
            // Assert
            Assert.DoesNotThrow(() => 
                textClassifier.SaveLabeledExamples(LabeledExamples.ObjectMother.ShortLabeledExamples, folderPath));

        }

        [Test]
        public void SaveTextSnippets_ShouldSaveCollectionOfTextSnippetsAndNotThrowException_WhenProperArguments()
        {

            // Arrange
            Func<DateTime> fakeNowFunction = () => Filenames.ObjectMother.FakeNow;
            Func<string> fakeVersionFunction = () => "1.0.0";

            ComponentBag componentBag
                = new ComponentBag(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: ComponentBag.DefaultRoundingFunction,
                          textTruncatingFunction: ComponentBag.DefaultTextTruncatingFunction,
                          loggingAction: ComponentBag.DefaultLoggingAction,
                          labeledExampleManager: new LabeledExampleManager(),
                          loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                          fileManager: new FakeFileManager(LabeledExamples.ObjectMother.ShortLabeledExamplesAsJson_Content),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: fakeNowFunction,
                          versionFunction: fakeVersionFunction);
            TextClassifier textClassifier = new TextClassifier(componentBag, new SettingBag());

            string folderPath = Path.GetFullPath(Filenames.ObjectMother.FakeFilePath);
            string fileName = $"nwngram_textsnippets_{Filenames.ObjectMother.FakeNowString}.json";
            string filePath = Path.Combine(folderPath, fileName);
            IFileInfoAdapter fakeJsonFile = new FakeFileInfoAdapter(true, filePath);

            // Act
            // Assert
            Assert.DoesNotThrow(() => 
                textClassifier.SaveTextSnippets(TextSnippets.ObjectMother.TextSnippets, folderPath));

        }

        [TestCase(false)]
        [TestCase(true)]
        public void SaveSession_ShouldSaveSessionAndNotThrowException_WhenInvoked(bool disableIndexSerialization)
        {

            // Arrange
            Func<DateTime> fakeNowFunction = () => Filenames.ObjectMother.FakeNow;
            Func<string> fakeVersionFunction = () => "1.0.0";

            ComponentBag componentBag
                = new ComponentBag(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: ComponentBag.DefaultRoundingFunction,
                          textTruncatingFunction: ComponentBag.DefaultTextTruncatingFunction,
                          loggingAction: ComponentBag.DefaultLoggingAction,
                          labeledExampleManager: new LabeledExampleManager(),
                          loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                          fileManager: new FakeFileManager(TextClassifications.ObjectMother.TextClassifierrSessionCLE00AsJson_Content),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: fakeNowFunction,
                          versionFunction: fakeVersionFunction);
            TextClassifier textClassifier = new TextClassifier(componentBag, new SettingBag());

            string folderPath = Path.GetFullPath(Filenames.ObjectMother.FakeFilePath);
            string fileName = $"nwngram_session_{Filenames.ObjectMother.FakeNowString}.json";
            string filePath = Path.Combine(folderPath, fileName);
            IFileInfoAdapter fakeJsonFile = new FakeFileInfoAdapter(true, filePath);

            // Act
            // Assert
            Assert.DoesNotThrow(() => 
                textClassifier.SaveSession(TextClassifications.ObjectMother.TextClassifierSession_CompleteLabeledExamples00, folderPath, disableIndexSerialization));

        }


        [Test]
        public void SaveLabeledExamples_ShouldThrowExpectedException_WhenWriteAllTextFails()
        {

            // Arrange
            Func<DateTime> fakeNowFunction = () => Filenames.ObjectMother.FakeNow;
            Func<string> fakeVersionFunction = () => "1.0.0";

            string writeExceptionMessage = "A random write-to-disk issue.";

            ComponentBag componentBag
                = new ComponentBag(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: ComponentBag.DefaultRoundingFunction,
                          textTruncatingFunction: ComponentBag.DefaultTextTruncatingFunction,
                          loggingAction: ComponentBag.DefaultLoggingAction,
                          labeledExampleManager: new LabeledExampleManager(),
                          loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                          fileManager: new FakeFileManagerThrowingWriteExceptions(
                                                content: LabeledExamples.ObjectMother.ShortLabeledExamplesAsJson_Content,
                                                writeExceptionMessage: writeExceptionMessage),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: fakeNowFunction,
                          versionFunction: fakeVersionFunction);
            TextClassifier textClassifier = new TextClassifier(componentBag, new SettingBag());

            string folderPath = Path.GetFullPath(Filenames.ObjectMother.FakeFilePath);
            string fileName = $"nwngram_labeledexamples_{Filenames.ObjectMother.FakeNowString}.json";
            string filePath = Path.Combine(folderPath, fileName);
            IFileInfoAdapter fakeJsonFile = new FakeFileInfoAdapter(true, filePath);

            // Act
            // Assert
            Exception e = Assert.Throws<Exception>(() => 
                textClassifier.SaveLabeledExamples(LabeledExamples.ObjectMother.ShortLabeledExamples, folderPath));

            Assert.That(e.Message, Is.EqualTo(writeExceptionMessage));

        }

        [Test]
        public void SaveTextSnippets_ShouldThrowExpectedException_WhenWriteAllTextFails()
        {

            // Arrange
            Func<DateTime> fakeNowFunction = () => Filenames.ObjectMother.FakeNow;
            Func<string> fakeVersionFunction = () => "1.0.0";

            string writeExceptionMessage = "A random write-to-disk issue.";

            ComponentBag componentBag
                = new ComponentBag(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: ComponentBag.DefaultRoundingFunction,
                          textTruncatingFunction: ComponentBag.DefaultTextTruncatingFunction,
                          loggingAction: ComponentBag.DefaultLoggingAction,
                          labeledExampleManager: new LabeledExampleManager(),
                          loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                          fileManager: new FakeFileManagerThrowingWriteExceptions(
                                                content: LabeledExamples.ObjectMother.ShortLabeledExamplesAsJson_Content,
                                                writeExceptionMessage: writeExceptionMessage),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: fakeNowFunction,
                          versionFunction: fakeVersionFunction);
            TextClassifier textClassifier = new TextClassifier(componentBag, new SettingBag());

            string folderPath = Path.GetFullPath(Filenames.ObjectMother.FakeFilePath);
            string fileName = $"nwngram_textsnippets_{Filenames.ObjectMother.FakeNowString}.json";
            string filePath = Path.Combine(folderPath, fileName);
            IFileInfoAdapter fakeJsonFile = new FakeFileInfoAdapter(true, filePath);

            // Act
            // Assert
            Exception e = Assert.Throws<Exception>(() => 
                textClassifier.SaveTextSnippets(TextSnippets.ObjectMother.TextSnippets, folderPath));

            Assert.That(e.Message, Is.EqualTo(writeExceptionMessage));

        }

        [Test]
        public void SaveSession_ShouldThrowExpectedException_WhenWriteAllTextFails()
        {

            // Arrange
            List<string> actualLogMessages = new List<string>();
            Action<string> fakeLoggingAction = (message) => actualLogMessages.Add(message);

            Func<DateTime> fakeNowFunction = () => Filenames.ObjectMother.FakeNow;
            Func<string> fakeVersionFunction = () => "1.0.0";

            string writeExceptionMessage = "A random write-to-disk issue.";

            ComponentBag componentBag
                = new ComponentBag(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: ComponentBag.DefaultRoundingFunction,
                          textTruncatingFunction: ComponentBag.DefaultTextTruncatingFunction,
                          loggingAction: fakeLoggingAction,
                          labeledExampleManager: new LabeledExampleManager(),
                          loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                          fileManager: new FakeFileManagerThrowingWriteExceptions(
                                                content: TextClassifications.ObjectMother.TextClassifierrSessionCLE00AsJson_Content,
                                                writeExceptionMessage: writeExceptionMessage),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: fakeNowFunction,
                          versionFunction: fakeVersionFunction);
            TextClassifier textClassifier = new TextClassifier(componentBag, new SettingBag());

            string folderPath = Path.GetFullPath(Filenames.ObjectMother.FakeFilePath);
            string fileName = $"nwngram_session_{Filenames.ObjectMother.FakeNowString}.json";
            string filePath = Path.Combine(folderPath, fileName);
            IFileInfoAdapter fakeJsonFile = new FakeFileInfoAdapter(true, filePath);

            // Act
            // Assert
            Exception e = Assert.Throws<Exception>(() => 
                textClassifier.SaveSession(TextClassifications.ObjectMother.TextClassifierSession_CompleteLabeledExamples00, folderPath, false));

            Assert.That(e.Message, Is.EqualTo(writeExceptionMessage));

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
                            args: [@"C:\", DateTime.Now],
                            methodType: typeof(Monogram) // "Monogram" is a not supported type.
                        );

            }
            catch(TargetInvocationException e)
            {

                // Assert
                Assert.That(e.InnerException, Is.InstanceOf<Exception>());
                Assert.That(
                    NGramTextClassification.TextClassifications.MessageCollection.ThereIsNoStrategyOutOfType(typeof(Monogram)),
                    Is.EqualTo(e.InnerException.Message));

            }

        }

        [TestCaseSource(nameof(convertExceptionTestCases))]
        public void Convert_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(cleanLabeledExamplesExceptionTestCases))]
        public void CleanLabeledExamples_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void CleanLabeledExamples_ShouldReturnExpectedList_WhenAllLabeledExamplesAreRemoved()
        {

            // Arrange
            Func<DateTime> fakeNowFunction = () => Filenames.ObjectMother.FakeNow;
            Func<string> fakeVersionFunction = () => "1.0.0";

            ComponentBag componentBag
                = new ComponentBag(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: ComponentBag.DefaultRoundingFunction,
                          textTruncatingFunction: ComponentBag.DefaultTextTruncatingFunction,
                          loggingAction: ComponentBag.DefaultLoggingAction,
                          labeledExampleManager: new LabeledExampleManager(),
                          loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                          fileManager: new FileManager(),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: fakeNowFunction,
                          versionFunction: fakeVersionFunction);
            TextClassifier textClassifier = new TextClassifier(componentBag, new SettingBag());

            // Act
            List<LabeledExample> actual = 
                textClassifier.CleanLabeledExamples(
                    labeledExamples: LabeledExamples.ObjectMother.UntokenizableLabeledExamples,
                    tokenizerRuleSet: new NGramTokenizerRuleSet()
                );

            // Assert
            Assert.That(actual.Count, Is.EqualTo(0));

        }
        
        [Test]
        public void CleanLabeledExamples_ShouldReturnExpectedList_WhenLabeledExamplesAreNotRemoved()
        {

            // Arrange
            Func<DateTime> fakeNowFunction = () => Filenames.ObjectMother.FakeNow;
            Func<string> fakeVersionFunction = () => "1.0.0";

            ComponentBag componentBag
                = new ComponentBag(
                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: ComponentBag.DefaultRoundingFunction,
                          textTruncatingFunction: ComponentBag.DefaultTextTruncatingFunction,
                          loggingAction: ComponentBag.DefaultLoggingAction,
                          labeledExampleManager: new LabeledExampleManager(),
                          loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                          fileManager: new FileManager(),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: fakeNowFunction,
                          versionFunction: fakeVersionFunction);
            TextClassifier textClassifier = new TextClassifier(componentBag, new SettingBag());

            // Act
            List<LabeledExample> actual = 
                textClassifier.CleanLabeledExamples(
                    labeledExamples: LabeledExamples.ObjectMother.ShortLabeledExamples,
                    tokenizerRuleSet: new NGramTokenizerRuleSet()
                );

            // Assert
            Assert.That(actual, Is.EqualTo(LabeledExamples.ObjectMother.ShortLabeledExamples));

        }

        [Test]
        public void SimilarityIndexDisabler_ShouldReplaceEverySimilarityIndexesWithAnEmptyCollection_WhenInvoked()
        {

            // Arrange
            // Act
            dynamic actual = TextClassifier.SimilarityIndexDisabler(TextClassifications.ObjectMother.TextClassifierSession_CompleteLabeledExamples00);

            // Assert
            foreach (ExpandoObject result in actual.Results)
            {

                var similarityIndexes = ((ExpandoObject)actual.Results[0]).FirstOrDefault(x => x.Key == "SimilarityIndexes").Value;
                Assert.That(((List<SimilarityIndex>)similarityIndexes).Count, Is.EqualTo(0));

            }

        }

        #endregion

        #region TearDown
        #endregion

        #region SupportMethods
        #endregion

    }
}