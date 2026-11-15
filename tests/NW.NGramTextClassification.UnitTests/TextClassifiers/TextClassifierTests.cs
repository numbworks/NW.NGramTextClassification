using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using NW.NGramTextClassification.Bags;
using NW.NGramTextClassification.LabeledExamples;
using NW.NGramTextClassification.NGrams;
using NW.NGramTextClassification.NGramTokenization;
using NW.NGramTextClassification.Similarity;
using NW.NGramTextClassification.TextClassifiers;
using NW.NGramTextClassification.TextClassifications;
using NW.NGramTextClassification.TextSnippets;
using NW.NGramTextClassification.UnitTests.Utilities;
using NW.Shared.Files;
using NW.Shared.Serialization;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests.TextClassifiers
{
    [TestFixture]
    public class TextClassifierTests
    {

        #region Fields

        private static TestCaseData[] classifyExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .Classify(
                                        textSnippet: null,
                                        textClassifierContext: new TextClassifierContext(
                                            tokenizerRuleSet: NGramTokenization.ObjectMother.NGramTokenizerRuleSet_MonoBiTriFourFive,
                                            labeledExamples: LabeledExamples.ObjectMother.ShortLabeledExamples,
                                            minimumAccuracySingleLabel: SettingBag.DefaultMinimumAccuracySingleLabel,
                                            minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels
                                        )
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("textSnippet").Message
                ).SetArgDisplayNames($"{nameof(classifyExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .Classify(
                                        textSnippet: new TextSnippet(text: LabeledExamples.ObjectMother.ShortLabeledExample01.Text),
                                        textClassifierContext: null
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("textClassifierContext").Message
                ).SetArgDisplayNames($"{nameof(classifyExceptionTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .Classify(
                                        textSnippet: new TextSnippet(text: LabeledExamples.ObjectMother.ShortLabeledExample01.Text),
                                        textClassifierContext: new TextClassifierContext(
                                            tokenizerRuleSet: NGramTokenization.ObjectMother.NGramTokenizerRuleSet_MonoBiTriFourFive,
                                            labeledExamples: null,
                                            minimumAccuracySingleLabel: SettingBag.DefaultMinimumAccuracySingleLabel,
                                            minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels
                                        )
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("LabeledExamples").Message
                ).SetArgDisplayNames($"{nameof(classifyExceptionTestCases)}_03")

        };
        private static TestCaseData[] classifyTestCases =
        {

            new TestCaseData(
                    new TextSnippet(
                            text: LabeledExamples.ObjectMother.CreateThirtyCompleteLabeledExamples()[0].Text),
                    new TextClassifierContext(
                        tokenizerRuleSet: new NGramTokenizerRuleSet(
                                doForMonogram: true,
                                doForBigram: true,
                                doForTrigram: true,
                                doForFourgram: true,
                                doForFivegram: true
                            ),
                        labeledExamples: LabeledExamples.ObjectMother.CreateThirtyCompleteLabeledExamples(),
                        minimumAccuracySingleLabel: 0.0,
                        minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels
                    ),
                    new ClassifyOutput(
                            minimumAccuracySingleLabel: 0.0,
                            minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels,
                            results: ObjectMother.TextClassifierResults_CompleteLabeledExamples00,
                            version: TextClassifier.DefaultVersionFunction()
                        )
                ).SetArgDisplayNames($"{nameof(classifyTestCases)}_01")

        };

        private static TestCaseData[] classifyOrDefaultExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .ClassifyOrDefault(
                                        textSnippet: null,
                                        textClassifierContext: new TextClassifierContext(
                                            tokenizerRuleSet: NGramTokenization.ObjectMother.NGramTokenizerRuleSet_MonoBiTriFourFive,
                                            labeledExamples: LabeledExamples.ObjectMother.ShortLabeledExamples,
                                            minimumAccuracySingleLabel: SettingBag.DefaultMinimumAccuracySingleLabel,
                                            minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels
                                        )
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("textSnippet").Message
                ).SetArgDisplayNames($"{nameof(classifyOrDefaultExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .ClassifyOrDefault(
                                        textSnippet: new TextSnippet(text: LabeledExamples.ObjectMother.ShortLabeledExample01.Text),
                                        textClassifierContext: null
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("textClassifierContext").Message
                ).SetArgDisplayNames($"{nameof(classifyOrDefaultExceptionTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .ClassifyOrDefault(
                                        textSnippet: new TextSnippet(text: LabeledExamples.ObjectMother.ShortLabeledExample01.Text),
                                        textClassifierContext: new TextClassifierContext(
                                            tokenizerRuleSet: NGramTokenization.ObjectMother.NGramTokenizerRuleSet_MonoBiTriFourFive,
                                            labeledExamples: null,
                                            minimumAccuracySingleLabel: SettingBag.DefaultMinimumAccuracySingleLabel,
                                            minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels
                                        )
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("LabeledExamples").Message
                ).SetArgDisplayNames($"{nameof(classifyOrDefaultExceptionTestCases)}_03")

        };
        private static TestCaseData[] classifyOrDefaultWhenAllRulesFailedTestCases =
        {

           new TestCaseData(
                    new TextSnippet(text: "/"),
                    new TextClassifierContext(
                        tokenizerRuleSet: new NGramTokenizerRuleSet(
                                doForMonogram: true,
                                doForBigram: true,
                                doForTrigram: true,
                                doForFourgram: true,
                                doForFivegram: true
                            ),
                        labeledExamples: LabeledExamples.ObjectMother.CreateThirtyCompleteLabeledExamples(),
                        minimumAccuracySingleLabel: SettingBag.DefaultMinimumAccuracySingleLabel,
                        minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels
                    ),
                    ObjectMother.ClassifyOutput_Default
                ).SetArgDisplayNames($"{nameof(classifyOrDefaultWhenAllRulesFailedTestCases)}_01"),

            new TestCaseData(
                    new TextSnippet(text: "hi"),
                    new TextClassifierContext(
                        tokenizerRuleSet: new NGramTokenizerRuleSet(
                                doForMonogram: false,
                                doForBigram: false,
                                doForTrigram: false,
                                doForFourgram: false,
                                doForFivegram: true
                            ),
                        labeledExamples: LabeledExamples.ObjectMother.CreateThirtyCompleteLabeledExamples(),
                        minimumAccuracySingleLabel: SettingBag.DefaultMinimumAccuracySingleLabel,
                        minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels
                    ),
                    ObjectMother.ClassifyOutput_Default
                ).SetArgDisplayNames($"{nameof(classifyOrDefaultWhenAllRulesFailedTestCases)}_02"),


        };
        private static TestCaseData[] classifyOrDefaultWhenUntokenizableTestCases =
        {

            new TestCaseData(
                    new TextSnippet(
                            text: LabeledExamples.ObjectMother.CreateThirtyCompleteLabeledExamples()[0].Text),
                    new TextClassifierContext(
                        tokenizerRuleSet: NGramTokenization.ObjectMother.NGramTokenizerRuleSet_Five,
                        labeledExamples: LabeledExamples.ObjectMother.ShortLabeledExamples_Untokenizable,
                        minimumAccuracySingleLabel: SettingBag.DefaultMinimumAccuracySingleLabel,
                        minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels
                    ),
                    ObjectMother.ClassifyOutput_Default
                ).SetArgDisplayNames($"{nameof(classifyOrDefaultWhenUntokenizableTestCases)}_01")

        };
        private static TestCaseData[] classifyOrDefaultWhenOneLabeledTestCases =
        {

            new TestCaseData(
                    new TextSnippet(
                            text: LabeledExamples.ObjectMother.CreateThirtyCompleteLabeledExamples()[0].Text),
                    new TextClassifierContext(
                        tokenizerRuleSet: new NGramTokenizerRuleSet(
                                doForMonogram: true,
                                doForBigram: true,
                                doForTrigram: true,
                                doForFourgram: true,
                                doForFivegram: true
                            ),
                        labeledExamples: LabeledExamples.ObjectMother.CreateThirtyCompleteLabeledExamples().GetRange(0, 1),
                        minimumAccuracySingleLabel: SettingBag.DefaultMinimumAccuracySingleLabel,
                        minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels
                    ),
                    LabeledExamples.ObjectMother.CreateThirtyCompleteLabeledExamples()[0].Label
                ).SetArgDisplayNames($"{nameof(classifyOrDefaultWhenOneLabeledTestCases)}_01")

        };
        private static TestCaseData[] classifyOrDefaultWhenThirtyLabeledTestCases =
        {

            new TestCaseData(
                    new TextSnippet(
                            text: LabeledExamples.ObjectMother.CreateThirtyCompleteLabeledExamples()[0].Text),
                    new TextClassifierContext(
                        tokenizerRuleSet: new NGramTokenizerRuleSet(
                                doForMonogram: true,
                                doForBigram: true,
                                doForTrigram: true,
                                doForFourgram: true,
                                doForFivegram: true
                            ),
                        labeledExamples: LabeledExamples.ObjectMother.CreateThirtyCompleteLabeledExamples(),
                        minimumAccuracySingleLabel: 0.0,
                        minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels
                    ),
                    new ClassifyOutput(
                            minimumAccuracySingleLabel: 0.0,
                            minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels,
                            results: ObjectMother.TextClassifierResults_CompleteLabeledExamples00,
                            version: TextClassifier.DefaultVersionFunction()
                        )
                ).SetArgDisplayNames($"{nameof(classifyOrDefaultWhenThirtyLabeledTestCases)}_01")

        };
        
        private static TestCaseData[] classifyManyExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .ClassifyMany(
                                        textSnippets: null,
                                        textClassifierContext: new TextClassifierContext(
                                            tokenizerRuleSet: NGramTokenization.ObjectMother.NGramTokenizerRuleSet_MonoBiTriFourFive,
                                            labeledExamples: LabeledExamples.ObjectMother.ShortLabeledExamples,
                                            minimumAccuracySingleLabel: SettingBag.DefaultMinimumAccuracySingleLabel,
                                            minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels
                                        )
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("textSnippets").Message
                ).SetArgDisplayNames($"{nameof(classifyManyExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .ClassifyMany(
                                        textSnippets: ObjectMother.Snippets_CompleteLabeledExamples00.Select(text => new TextSnippet(text: text)).ToList(),
                                        textClassifierContext: null
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("textClassifierContext").Message
                ).SetArgDisplayNames($"{nameof(classifyManyExceptionTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .ClassifyMany(
                                        textSnippets: ObjectMother.Snippets_CompleteLabeledExamples00.Select(text => new TextSnippet(text: text)).ToList(),
                                        textClassifierContext: new TextClassifierContext(
                                            tokenizerRuleSet: NGramTokenization.ObjectMother.NGramTokenizerRuleSet_MonoBiTriFourFive,
                                            labeledExamples: null,
                                            minimumAccuracySingleLabel: SettingBag.DefaultMinimumAccuracySingleLabel,
                                            minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels
                                        )
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("LabeledExamples").Message
                ).SetArgDisplayNames($"{nameof(classifyManyExceptionTestCases)}_03")

        };
        private static TestCaseData[] classifyManyTestCases =
        {

            new TestCaseData(
                    ObjectMother.Snippets_CompleteLabeledExamples00.Select(text => new TextSnippet(text: text)).ToList(),
                    new TextClassifierContext(
                        tokenizerRuleSet: TextClassifier.DefaultNGramTokenizerRuleSet,
                        labeledExamples: LabeledExamples.ObjectMother.CreateThirtyCompleteLabeledExamples(),
                        minimumAccuracySingleLabel: 0.0,
                        minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels
                    ),
                    new ClassifyOutput(
                            minimumAccuracySingleLabel: 0.0,
                            minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels,
                            results: ObjectMother.TextClassifierResults_CompleteLabeledExamples00,
                            version: TextClassifier.DefaultVersionFunction()
                        )
                ).SetArgDisplayNames($"{nameof(classifyManyTestCases)}_01")

        };

        private static TestCaseData[] classifyManyOrDefaultExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .ClassifyManyOrDefault(
                                        textSnippets: null,
                                        textClassifierContext: new TextClassifierContext(
                                            tokenizerRuleSet: NGramTokenization.ObjectMother.NGramTokenizerRuleSet_MonoBiTriFourFive,
                                            labeledExamples: LabeledExamples.ObjectMother.ShortLabeledExamples,
                                            minimumAccuracySingleLabel: SettingBag.DefaultMinimumAccuracySingleLabel,
                                            minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels
                                        )
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("textSnippets").Message
                ).SetArgDisplayNames($"{nameof(classifyManyOrDefaultExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .ClassifyManyOrDefault(
                                        textSnippets: ObjectMother.Snippets_CompleteLabeledExamples00.Select(text => new TextSnippet(text: text)).ToList(),
                                        textClassifierContext: null
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("textClassifierContext").Message
                ).SetArgDisplayNames($"{nameof(classifyManyOrDefaultExceptionTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .ClassifyManyOrDefault(
                                        textSnippets: ObjectMother.Snippets_CompleteLabeledExamples00.Select(text => new TextSnippet(text: text)).ToList(),
                                        textClassifierContext: new TextClassifierContext(
                                            tokenizerRuleSet: NGramTokenization.ObjectMother.NGramTokenizerRuleSet_MonoBiTriFourFive,
                                            labeledExamples: null,
                                            minimumAccuracySingleLabel: SettingBag.DefaultMinimumAccuracySingleLabel,
                                            minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels
                                        )
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("LabeledExamples").Message
                ).SetArgDisplayNames($"{nameof(classifyManyOrDefaultExceptionTestCases)}_03")

        };
        private static TestCaseData[] classifyManyOrDefaultTestCases =
        {

            new TestCaseData(
                    ObjectMother.Snippets_CompleteLabeledExamples00.Select(text => new TextSnippet(text: text)).ToList(),
                    new TextClassifierContext(
                        tokenizerRuleSet: TextClassifier.DefaultNGramTokenizerRuleSet,
                        labeledExamples: LabeledExamples.ObjectMother.CreateThirtyCompleteLabeledExamples(),
                        minimumAccuracySingleLabel: 0.0,
                        minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels
                    ),
                    new ClassifyOutput(
                            minimumAccuracySingleLabel: 0.0,
                            minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels,
                            results: ObjectMother.TextClassifierResults_CompleteLabeledExamples00,
                            version: TextClassifier.DefaultVersionFunction()
                        )
                ).SetArgDisplayNames($"{nameof(classifyManyOrDefaultTestCases)}_01"),

            new TestCaseData(
                    ObjectMother.Snippets_Untokenizable.Select(text => new TextSnippet(text: text)).ToList(),
                    new TextClassifierContext(
                        tokenizerRuleSet: NGramTokenization.ObjectMother.NGramTokenizerRuleSet_Five,
                        labeledExamples: LabeledExamples.ObjectMother.CreateThirtyCompleteLabeledExamples(),
                        minimumAccuracySingleLabel: 0.0,
                        minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels
                    ),
                    new ClassifyOutput(
                            minimumAccuracySingleLabel: 0.0,
                            minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels,
                            results: ObjectMother.TextClassifierResults_Untokenizable,
                            version: TextClassifier.DefaultVersionFunction()
                        )
                ).SetArgDisplayNames($"{nameof(classifyManyOrDefaultTestCases)}_02")

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
                        () => new TextClassifier().LoadLabeledExamplesOrDefault(jsonFile: Utilities.ObjectMother.FileInfoAdapterDoesntExist)
                    ),
            typeof(ArgumentException),
                Shared.Files.Validation.MessageCollection.ProvidedPathDoesntExist(Utilities.ObjectMother.FileInfoAdapterDoesntExist)
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
                        () => new TextClassifier().LoadTextSnippetsOrDefault(jsonFile: Utilities.ObjectMother.FileInfoAdapterDoesntExist)
                    ),
                typeof(ArgumentException),
                Shared.Files.Validation.MessageCollection.ProvidedPathDoesntExist(Utilities.ObjectMother.FileInfoAdapterDoesntExist)
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
                        () => new TextClassifier().LoadTokenizerRuleSetOrDefault(jsonFile: Utilities.ObjectMother.FileInfoAdapterDoesntExist)
                    ),
                typeof(ArgumentException),
                Shared.Files.Validation.MessageCollection.ProvidedPathDoesntExist(Utilities.ObjectMother.FileInfoAdapterDoesntExist)
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
                                        tokenizerRuleSet: new NGramTokenizerRuleSet(),
                                        removed: out _
                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException("labeledExamples").Message
                ).SetArgDisplayNames($"{nameof(cleanLabeledExamplesExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .CleanLabeledExamples(
                                        labeledExamples: LabeledExamples.ObjectMother.ShortLabeledExamples,
                                        tokenizerRuleSet: null,
                                        removed: out _
                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException("tokenizerRuleSet").Message
                ).SetArgDisplayNames($"{nameof(cleanLabeledExamplesExceptionTestCases)}_02")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests (TextClassifier)

        [Test]
        public void TextClassifier_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            TextClassifier actual = new TextClassifier();

            // Assert
            Assert.That(actual, Is.InstanceOf<TextClassifier>());

            Assert.That(TextClassifier.DefaultNGramTokenizerRuleSet, Is.InstanceOf<INGramTokenizerRuleSet>());
            Assert.That(TextClassifier.DefaultTextClassifierResult, Is.InstanceOf<TextClassifierResult>());
            Assert.That(TextClassifier.SimilarityIndexDisabler, Is.InstanceOf<Func<ClassifyOutput, dynamic>>());
            Assert.That(TextClassifier.DefaultNowFunction, Is.InstanceOf<Func<DateTime>>());
            Assert.That(TextClassifier.DefaultUTCNowFunction, Is.InstanceOf<Func<DateTime>>());
            Assert.That(TextClassifier.DefaultVersionFunction, Is.InstanceOf<Func<string>>());
            Assert.That(TextClassifier.DefaultRoundingFunction, Is.InstanceOf<Func<double, double>>());

        }

        #endregion

        #region Tests (Classify)

        [TestCaseSource(nameof(classifyExceptionTestCases))]
        public void Classify_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(classifyTestCases))]
        public void Classify_ShouldReturnExpectedClassifyOutput_WhenSuccessfulClassification
            (TextSnippet textSnippet, TextClassifierContext textClassifierContext, ClassifyOutput expected)
        {

            // Arrange
            // Act
            ClassifyOutput actual 
                = new TextClassifier().Classify(
                    textSnippet, 
                    textClassifierContext);

            // Assert
            Assert.That(
                    ObjectMother.AreEqual(expected, actual),
                    Is.True
                );

        }

        #endregion

        #region Tests (ClassifyOrDefault)

        [TestCaseSource(nameof(classifyOrDefaultExceptionTestCases))]
        public void ClassifyOrDefault_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(classifyOrDefaultWhenAllRulesFailedTestCases))]
        public void ClassifyOrDefault_ShouldReturnExpectedClassifyOutput_WhenAllRulesFailed
            (TextSnippet textSnippet, TextClassifierContext textClassifierContext, ClassifyOutput expected)
        {

            // Arrange
            // Act
            ClassifyOutput actual 
                = new TextClassifier().ClassifyOrDefault(
                    textSnippet, 
                    textClassifierContext);

            // Assert
            Assert.That(
                    ObjectMother.AreEqual(expected, actual),
                    Is.True
                );

        }

        [TestCaseSource(nameof(classifyOrDefaultWhenUntokenizableTestCases))]
        public void ClassifyOrDefault_ShouldReturnOutputWithDefaultClassifierResult_WhenUntokenizableExamples
            (TextSnippet textSnippet, TextClassifierContext textClassifierContext, ClassifyOutput expected)
        {

            // Arrange
            // Act
            ClassifyOutput actual 
                = new TextClassifier().ClassifyOrDefault(
                    textSnippet, 
                    textClassifierContext);

            // Assert
            Assert.That(
                    ObjectMother.AreEqual(expected, actual),
                    Is.True
                );

        }

        [TestCaseSource(nameof(classifyOrDefaultWhenOneLabeledTestCases))]
        public void ClassifyOrDefault_ShouldReturnExpectedLabel_WhenOneLabeledExampleAndSuccessfulClassification
            (TextSnippet textSnippet, TextClassifierContext textClassifierContext, string expectedLabel)
        {

            // Arrange
            // Act
            ClassifyOutput actual 
                = new TextClassifier().ClassifyOrDefault(
                    textSnippet, 
                    textClassifierContext);

            // Assert
            Assert.That(expectedLabel, Is.EqualTo(actual.Results[0].Label));

        }

        [TestCaseSource(nameof(classifyOrDefaultWhenThirtyLabeledTestCases))]
        public void ClassifyOrDefault_ShouldReturnExpectedClassifyOutput_WhenThirtyLabeledExamplesAndSuccessfulClassification
            (TextSnippet textSnippet, TextClassifierContext textClassifierContext, ClassifyOutput expected)
        {

            // Arrange
            // Act
            ClassifyOutput actual 
                = new TextClassifier().ClassifyOrDefault(
                    textSnippet, 
                    textClassifierContext);

            // Assert
            Assert.That(
                    ObjectMother.AreEqual(expected, actual),
                    Is.True
                );

        }

        #endregion

        #region Tests (ClassifyMany)

        [TestCaseSource(nameof(classifyManyExceptionTestCases))]
        public void ClassifyMany_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(classifyManyTestCases))]
        public void ClassifyMany_ShouldReturnExpectedClassifyOutput_WhenInvoked
            (List<TextSnippet> textSnippets, TextClassifierContext context, ClassifyOutput expected)
        {

            // Arrange
            // Act
            ClassifyOutput actual
                = new TextClassifier().ClassifyMany(
                    textSnippets: textSnippets, 
                    textClassifierContext: context);

            // Assert
            Assert.That(ObjectMother.AreEqual(expected, actual), Is.True);

        }

        #endregion

        #region Tests (ClassifyManyOrDefault)

        [TestCaseSource(nameof(classifyManyOrDefaultExceptionTestCases))]
        public void ClassifyManyOrDefault_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(classifyManyOrDefaultTestCases))]
        public void ClassifyManyOrDefault_ShouldReturnExpectedClassifyOutput_WhenInvoked
            (List<TextSnippet> textSnippets, TextClassifierContext context, ClassifyOutput expected)
        {

            // Arrange
            // Act
            ClassifyOutput actual
                = new TextClassifier().ClassifyManyOrDefault(
                    textSnippets: textSnippets, 
                    textClassifierContext: context);

            // Assert
            Assert.That(ObjectMother.AreEqual(expected, actual), Is.True);

        }

        #endregion

        #region Tests (GetLabel)

        [Test]
        public void GetLabel_ShouldReturnNull_WhenAreAllIndexAveragesEqualToZeroReturnsTrue()
        {

            // Arrange
            TextClassifier textClassifier = new TextClassifier();

            List<SimilarityIndexAverage> indexAverages = new List<SimilarityIndexAverage>()
            {

                new SimilarityIndexAverage(label: "en", value: 0),
                new SimilarityIndexAverage(label: "sv", value: 0)

            };

            string expected = null;

            // Act
            string actual
                = Utilities.ObjectMother.CallPrivateMethod<TextClassifier, string>(
                        obj: textClassifier,
                        methodName: "GetLabel",
                        args: [indexAverages, SettingBag.DefaultMinimumAccuracySingleLabel, SettingBag.DefaultMinimumAccuracyMultipleLabels]
                    );

            // Assert
            Assert.That(expected, Is.EqualTo(actual));

        }

        [Test]
        public void GetLabel_ShouldReturnExpectedLabel_WhenIsSingleLabelAndHigherEqualThanMinimumAccuracyReturnsTrue()
        {

            // Arrange
            TextClassifier textClassifier = new TextClassifier();

            List<SimilarityIndexAverage> indexAverages = new List<SimilarityIndexAverage>()
            {

                new SimilarityIndexAverage(label: "sv", value: 0.98) // 0.98 >= 0.5                

            };

            string expected = "sv";

            // Act
            string actual
                = Utilities.ObjectMother.CallPrivateMethod<TextClassifier, string>(
                        obj: textClassifier,
                        methodName: "GetLabel",
                        args: [indexAverages, SettingBag.DefaultMinimumAccuracySingleLabel, SettingBag.DefaultMinimumAccuracyMultipleLabels]
                    );

            // Assert
            Assert.That(expected, Is.EqualTo(actual));

        }

        [Test]
        public void GetLabel_ShouldReturnNull_WhenIsSingleLabelAndLessThanMinimumAccuracyReturnsTrue()
        {

            // Arrange
            TextClassifier textClassifier = new TextClassifier();

            List<SimilarityIndexAverage> indexAverages = new List<SimilarityIndexAverage>()
            {

                new SimilarityIndexAverage(label: "sv", value: 0.98) // 0.98 < 1.0

            };

            string expected = null;

            // Act
            string actual
                = Utilities.ObjectMother.CallPrivateMethod<TextClassifier, string>(
                        obj: textClassifier,
                        methodName: "GetLabel",
                        args: [indexAverages, 1.0, SettingBag.DefaultMinimumAccuracyMultipleLabels]
                    );

            // Assert
            Assert.That(expected, Is.EqualTo(actual));

        }

        [Test]
        public void GetLabel_ShouldReturnNull_WhenAreAllIndexAveragesSameValueReturnsTrue()
        {

            // Arrange
            TextClassifier textClassifier = new TextClassifier();

            List<SimilarityIndexAverage> indexAverages = new List<SimilarityIndexAverage>()
            {

                new SimilarityIndexAverage(label: "en", value: 0.1),
                new SimilarityIndexAverage(label: "sv", value: 0.1),
                new SimilarityIndexAverage(label: "dk", value: 0.1)

            };

            string expected = null;

            // Act
            string actual
                = Utilities.ObjectMother.CallPrivateMethod<TextClassifier, string>(
                        obj: textClassifier,
                        methodName: "GetLabel",
                        args: [indexAverages, SettingBag.DefaultMinimumAccuracySingleLabel, SettingBag.DefaultMinimumAccuracyMultipleLabels]
                    );

            // Assert
            Assert.That(expected, Is.EqualTo(actual));

        }

        [Test]
        public void GetLabel_ShouldReturnNull_WhenAreTwoHighestIndexAveragesSameValueReturnsTrue()
        {

            // Arrange
            TextClassifier textClassifier = new TextClassifier();

            List<SimilarityIndexAverage> indexAverages = new List<SimilarityIndexAverage>()
            {

                new SimilarityIndexAverage(label: "sv", value: 0.98),   // 0.98 >= 0.5
                new SimilarityIndexAverage(label: "en", value: 0.98),
                new SimilarityIndexAverage(label: "dk", value: 0.32)

            };

            string expected = null;

            // Act
            string actual
                = Utilities.ObjectMother.CallPrivateMethod<TextClassifier, string>(
                        obj: textClassifier,
                        methodName: "GetLabel",
                        args: [indexAverages, SettingBag.DefaultMinimumAccuracySingleLabel, SettingBag.DefaultMinimumAccuracyMultipleLabels]
                    );

            // Assert
            Assert.That(expected, Is.EqualTo(actual));

        }

        [Test]
        public void GetLabel_ShouldReturnNull_WhenIsLessThanMinimumAccuracyMultipleLabelsReturnsTrue()
        {

            // Arrange
            TextClassifier textClassifier = new TextClassifier();

            List<SimilarityIndexAverage> indexAverages = new List<SimilarityIndexAverage>()
            {

                new SimilarityIndexAverage(label: "sv", value: 0.98),   // 0.98 <= 1.0
                new SimilarityIndexAverage(label: "en", value: 0.75),
                new SimilarityIndexAverage(label: "dk", value: 0.32)

            };

            string expected = null;

            // Act
            string actual
                = Utilities.ObjectMother.CallPrivateMethod<TextClassifier, string>(
                        obj: textClassifier,
                        methodName: "GetLabel",
                        args: [indexAverages, SettingBag.DefaultMinimumAccuracySingleLabel, 1.0]
                    );

            // Assert
            Assert.That(expected, Is.EqualTo(actual));

        }

        [Test]
        public void GetLabel_ShouldReturnExpectedLabel_WhenIsLessThanMinimumAccuracyMultipleLabelsReturnsFalse()
        {

            // Arrange
            TextClassifier textClassifier = new TextClassifier();

            List<SimilarityIndexAverage> indexAverages = new List<SimilarityIndexAverage>()
            {

                new SimilarityIndexAverage(label: "sv", value: 0.98), // 0.98 > 0.5
                new SimilarityIndexAverage(label: "en", value: 0.75),
                new SimilarityIndexAverage(label: "dk", value: 0.32)

            };

            string expected = "sv";

            // Act
            string actual
                = Utilities.ObjectMother.CallPrivateMethod<TextClassifier, string>(
                        obj: textClassifier,
                        methodName: "GetLabel",
                        args: [indexAverages, SettingBag.DefaultMinimumAccuracySingleLabel, SettingBag.DefaultMinimumAccuracyMultipleLabels]
                    );

            // Assert
            Assert.That(expected, Is.EqualTo(actual));

        }

        #endregion

        #region Tests (LoadLabeledExamplesOrDefault)

        [TestCaseSource(nameof(loadLabeledExamplesOrDefaultExceptionTestCases))]
        public void LoadLabeledExamplesOrDefault_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void LoadLabeledExamplesOrDefault_ShouldReturnExpectedCollectionOfLabeledExamples_WhenProperJsonFileContent()
        {

            // Arrange
            TextClassifier textClassifier 
                = new TextClassifier(
                    fileManager: new FakeFileManager(LabeledExamples.ObjectMother.LabeledExamplesAsJson_Content));

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
            TextClassifier textClassifier = new TextClassifier(fileManager: new FakeFileManager("Unproper Json content"));

            IFileInfoAdapter fakeJsonFile = new FakeFileInfoAdapter(true, @"C:\LabeledExamples.json");

            // Act
            List<LabeledExample> actual = textClassifier.LoadLabeledExamplesOrDefault(fakeJsonFile);

            // Assert
            Assert.That(Serializer<LabeledExample>.Default, Is.EqualTo(actual));

        }

        #endregion

        #region Tests (LoadTextSnippetsOrDefault)

        [TestCaseSource(nameof(loadTextSnippetsOrDefaultExceptionTestCases))]
        public void LoadTextSnippetsOrDefault_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void LoadTextSnippetsOrDefault_ShouldReturnExpectedCollectionOfTextSnippets_WhenProperJsonFileContent()
        {

            // Arrange
            TextClassifier textClassifier 
                = new TextClassifier(fileManager: new FakeFileManager(LabeledExamples.ObjectMother.LabeledExamplesAsJson_Content));

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
            TextClassifier textClassifier = new TextClassifier(fileManager: new FakeFileManager("Unproper Json content"));
            IFileInfoAdapter fakeJsonFile = new FakeFileInfoAdapter(true, @"C:\TextSnippets.json");

            // Act
            List<TextSnippet> actual = textClassifier.LoadTextSnippetsOrDefault(fakeJsonFile);

            // Assert
            Assert.That(Serializer<TextSnippet>.Default, Is.EqualTo(actual));

        }

        #endregion

        #region Tests (LoadTokenizerRuleSetOrDefault)

        [TestCaseSource(nameof(loadTokenizerRuleSetOrDefaultExceptionTestCases))]
        public void LoadTokenizerRuleSetOrDefault_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void LoadTokenizerRuleSetOrDefault_ShouldReturnExpectedTokenizerRuleSet_WhenProperJsonFileContent()
        {

            // Arrange
            TextClassifier textClassifier 
                = new TextClassifier(fileManager: new FakeFileManager(ObjectMother.TokenizerRuleSetAsJson_Content));

            IFileInfoAdapter fakeJsonFile = new FakeFileInfoAdapter(true, @"C:\TokenizerRuleSet.json");

            // Act
            NGramTokenizerRuleSet actual = textClassifier.LoadTokenizerRuleSetOrDefault(fakeJsonFile);

            // Assert
            Assert.That(
                    NGramTokenization.ObjectMother.AreEqual(ObjectMother.TokenizerRuleSet, actual),
                    Is.True
                );

        }

        [Test]
        public void LoadTokenizerRuleSetOrDefault_ShouldReturnDefault_WhenUnproperJsonFileContent()
        {

            // Arrange
            TextClassifier textClassifier = new TextClassifier(fileManager: new FakeFileManager("Unproper Json content"));
            IFileInfoAdapter fakeJsonFile = new FakeFileInfoAdapter(true, @"C:\TokenizerRuleSet.json");

            // Act
            NGramTokenizerRuleSet actual = textClassifier.LoadTokenizerRuleSetOrDefault(fakeJsonFile);

            // Assert
            Assert.That(actual, Is.EqualTo(default(NGramTokenizerRuleSet)));

        }

        #endregion

        #region Tests (SaveLabeledExamples)

        [Test]
        public void SaveLabeledExamples_ShouldSaveCollectionOfLabeledExamplesAndNotThrowException_WhenProperArguments()
        {

            // Arrange
            Func<DateTime> fakeNowFunction = () => Filenames.ObjectMother.FakeNow;
            Func<string> fakeVersionFunction = () => "1.0.0";

            TextClassifier textClassifier 
                = new TextClassifier(
                    fileManager: new FakeFileManager(LabeledExamples.ObjectMother.LabeledExamplesAsJson_Content),
                    nowFunction: fakeNowFunction,
                    versionFunction: fakeVersionFunction);

            string folderPath = Path.GetFullPath(Filenames.ObjectMother.FakeFilePath);
            string fileName = $"nwngram_labeledexamples_{Filenames.ObjectMother.FakeNowString}.json";
            string filePath = Path.Combine(folderPath, fileName);
            IFileInfoAdapter fakeJsonFile = new FakeFileInfoAdapter(true, filePath);

            // Act
            // Assert
            Assert.DoesNotThrow(() => 
                textClassifier.SaveLabeledExamples(LabeledExamples.ObjectMother.ShortLabeledExamples, folderPath));

        }

        [Test]
        public void SaveLabeledExamples_ShouldThrowExpectedException_WhenWriteAllTextFails()
        {

            // Arrange
            Func<DateTime> fakeNowFunction = () => Filenames.ObjectMother.FakeNow;
            Func<string> fakeVersionFunction = () => "1.0.0";

            string writeExceptionMessage = "A random write-to-disk issue.";

            TextClassifier textClassifier 
                = new TextClassifier(
                        fileManager: new FakeFileManagerThrowingWriteExceptions(
                                            content: LabeledExamples.ObjectMother.LabeledExamplesAsJson_Content,
                                            writeExceptionMessage: writeExceptionMessage),
                        nowFunction: fakeNowFunction,
                        versionFunction: fakeVersionFunction);

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

        #endregion

        #region Tests (SaveTextSnippets)

        [Test]
        public void SaveTextSnippets_ShouldSaveCollectionOfTextSnippetsAndNotThrowException_WhenProperArguments()
        {

            // Arrange
            Func<DateTime> fakeNowFunction = () => Filenames.ObjectMother.FakeNow;
            Func<string> fakeVersionFunction = () => "1.0.0";

            TextClassifier textClassifier 
                = new TextClassifier(
                    fileManager: new FakeFileManager(LabeledExamples.ObjectMother.LabeledExamplesAsJson_Content),
                    nowFunction: fakeNowFunction,
                    versionFunction: fakeVersionFunction);

            string folderPath = Path.GetFullPath(Filenames.ObjectMother.FakeFilePath);
            string fileName = $"nwngram_textsnippets_{Filenames.ObjectMother.FakeNowString}.json";
            string filePath = Path.Combine(folderPath, fileName);
            IFileInfoAdapter fakeJsonFile = new FakeFileInfoAdapter(true, filePath);

            // Act
            // Assert
            Assert.DoesNotThrow(() => 
                textClassifier.SaveTextSnippets(TextSnippets.ObjectMother.TextSnippets, folderPath));

        }

        [Test]
        public void SaveTextSnippets_ShouldThrowExpectedException_WhenWriteAllTextFails()
        {

            // Arrange
            Func<DateTime> fakeNowFunction = () => Filenames.ObjectMother.FakeNow;
            Func<string> fakeVersionFunction = () => "1.0.0";

            string writeExceptionMessage = "A random write-to-disk issue.";

            TextClassifier textClassifier 
                = new TextClassifier(
                    fileManager: new FakeFileManagerThrowingWriteExceptions(
                                    content: LabeledExamples.ObjectMother.LabeledExamplesAsJson_Content,
                                    writeExceptionMessage: writeExceptionMessage),
                    nowFunction: fakeNowFunction,
                    versionFunction: fakeVersionFunction);

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

        #endregion

        #region Tests (SaveOutput)

        [TestCase(false)]
        [TestCase(true)]
        public void SaveOutput_ShouldSaveOutputAndNotThrowException_WhenInvoked(bool disableIndexSerialization)
        {

            // Arrange
            Func<DateTime> fakeNowFunction = () => Filenames.ObjectMother.FakeNow;
            Func<string> fakeVersionFunction = () => "1.0.0";

            TextClassifier textClassifier 
                = new TextClassifier(
                        fileManager: new FakeFileManager(ObjectMother.ClassifyOutputCLE00AsJson_Content),
                        nowFunction: fakeNowFunction,
                        versionFunction: fakeVersionFunction);

            string folderPath = Path.GetFullPath(Filenames.ObjectMother.FakeFilePath);
            string fileName = $"nwngram_output_{Filenames.ObjectMother.FakeNowString}.json";
            string filePath = Path.Combine(folderPath, fileName);
            IFileInfoAdapter fakeJsonFile = new FakeFileInfoAdapter(true, filePath);

            // Act
            // Assert
            Assert.DoesNotThrow(() => 
                textClassifier.SaveClassifyOutput(ObjectMother.ClassifyOutput_CompleteLabeledExamples00, folderPath, disableIndexSerialization));

        }

        [Test]
        public void SaveOutput_ShouldThrowExpectedException_WhenWriteAllTextFails()
        {

            // Arrange
            Func<DateTime> fakeNowFunction = () => Filenames.ObjectMother.FakeNow;
            Func<string> fakeVersionFunction = () => "1.0.0";

            string writeExceptionMessage = "A random write-to-disk issue.";

            TextClassifier textClassifier 
                = new TextClassifier(
                        fileManager: new FakeFileManagerThrowingWriteExceptions(
                                        content: ObjectMother.ClassifyOutputCLE00AsJson_Content,
                                        writeExceptionMessage: writeExceptionMessage),
                        nowFunction: fakeNowFunction,
                        versionFunction: fakeVersionFunction);

            string folderPath = Path.GetFullPath(Filenames.ObjectMother.FakeFilePath);
            string fileName = $"nwngram_output_{Filenames.ObjectMother.FakeNowString}.json";
            string filePath = Path.Combine(folderPath, fileName);
            IFileInfoAdapter fakeJsonFile = new FakeFileInfoAdapter(true, filePath);

            // Act
            // Assert
            Exception e = Assert.Throws<Exception>(() => 
                textClassifier.SaveClassifyOutput(ObjectMother.ClassifyOutput_CompleteLabeledExamples00, folderPath, false));

            Assert.That(e.Message, Is.EqualTo(writeExceptionMessage));

        }

        #endregion

        #region Tests (CleanLabeledExamples)

        [TestCaseSource(nameof(cleanLabeledExamplesExceptionTestCases))]
        public void CleanLabeledExamples_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void CleanLabeledExamples_ShouldReturnExpectedList_WhenAllLabeledExamplesAreRemoved()
        {

            // Arrange
            Func<DateTime> fakeNowFunction = () => Filenames.ObjectMother.FakeNow;
            Func<string> fakeVersionFunction = () => "1.0.0";

            TextClassifier textClassifier 
                = new TextClassifier(
                        nowFunction: fakeNowFunction,
                        versionFunction: fakeVersionFunction);

            // Act
            List<LabeledExample> actual = 
                textClassifier.CleanLabeledExamples(
                    labeledExamples: LabeledExamples.ObjectMother.UntokenizableLabeledExamples,
                    tokenizerRuleSet: new NGramTokenizerRuleSet(),
                    removed: out _
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

            TextClassifier textClassifier 
                = new TextClassifier(
                        nowFunction: fakeNowFunction,
                        versionFunction: fakeVersionFunction);

            // Act
            List<LabeledExample> actual = 
                textClassifier.CleanLabeledExamples(
                    labeledExamples: LabeledExamples.ObjectMother.ShortLabeledExamples,
                    tokenizerRuleSet: new NGramTokenizerRuleSet(),
                    removed: out _
                );

            // Assert
            Assert.That(actual, Is.EqualTo(LabeledExamples.ObjectMother.ShortLabeledExamples));

        }

        #endregion

        #region Tests (Convert)

        [TestCaseSource(nameof(convertExceptionTestCases))]
        public void Convert_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        #endregion

        #region Tests (SimilarityIndexDisabler)

        [Test]
        public void SimilarityIndexDisabler_ShouldReplaceEverySimilarityIndexesWithAnEmptyCollection_WhenInvoked()
        {

            // Arrange
            // Act
            dynamic actual = TextClassifier.SimilarityIndexDisabler(ObjectMother.ClassifyOutput_CompleteLabeledExamples00);

            // Assert
            foreach (ExpandoObject result in actual.Results)
            {

                var similarityIndexes = ((ExpandoObject)actual.Results[0]).FirstOrDefault(x => x.Key == "SimilarityIndexes").Value;
                Assert.That(((List<SimilarityIndex>)similarityIndexes).Count, Is.EqualTo(0));

            }

        }

        #endregion

        #region Tests (Private Methods)

        [Test]
        public void Create_ShouldThrowExpectedException_WhenProvidedTypeIsNotSupported()
        {

            // Arrange
            TextClassifier textClassifier = new TextClassifier();

            try
            {

                // Act
                IFileInfoAdapter actual
                    = Utilities.ObjectMother.CallPrivateGenericMethod<TextClassifier, IFileInfoAdapter>(
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

        #endregion

        #region TearDown
        #endregion

        #region SupportMethods
        #endregion

    }
}