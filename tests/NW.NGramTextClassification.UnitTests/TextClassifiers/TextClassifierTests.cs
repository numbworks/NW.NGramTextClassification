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

        private static TestCaseData[] classifyOrDefaultExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .ClassifyOrDefault(
                                        textSnippet: null,
                                        tokenizerRuleSet: NGramTokenization.ObjectMother.NGramTokenizerRuleSet_MonoBiTriFourFive,
                                        labeledExamples: LabeledExamples.ObjectMother.ShortLabeledExamples,
                                        minimumAccuracySingleLabel: SettingBag.DefaultMinimumAccuracySingleLabel,
                                        minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels
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
                                        labeledExamples: LabeledExamples.ObjectMother.ShortLabeledExamples,
                                        minimumAccuracySingleLabel: SettingBag.DefaultMinimumAccuracySingleLabel,
                                        minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels
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
                                        labeledExamples: null,
                                        minimumAccuracySingleLabel: SettingBag.DefaultMinimumAccuracySingleLabel,
                                        minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels
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
                    TextClassifiers.ObjectMother.ClassifyOutput_Default
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
                    TextClassifiers.ObjectMother.ClassifyOutput_Default
                ).SetArgDisplayNames($"{nameof(classifyOrDefaultWhenAllRulesFailedTestCases)}_02"),


        };
        private static TestCaseData[] classifyOrDefaultWhenUntokenizableExamples =
        {

            new TestCaseData(
                    new TextSnippet(
                            text: LabeledExamples.ObjectMother.CreateThirtyCompleteLabeledExamples()[0].Text),
                    NGramTokenization.ObjectMother.NGramTokenizerRuleSet_Five,
                    LabeledExamples.ObjectMother.ShortLabeledExamples_Untokenizable,
                    TextClassifiers.ObjectMother.ClassifyOutput_Default
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
                    0.0,
                    SettingBag.DefaultMinimumAccuracyMultipleLabels,
                    new ClassifyOutput(
                            minimumAccuracySingleLabel: 0.0,
                            minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels,
                            results: TextClassifiers.ObjectMother.TextClassifierResults_CompleteLabeledExamples00,
                            version: TextClassifier.DefaultVersionFunction()
                        )
                ).SetArgDisplayNames($"{nameof(classifyOrDefaultWhenThirtyLabeledExamplesAndSuccessfulClassification)}_01")

        };
        private static TestCaseData[] classifyManyExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .ClassifyManyOrDefault(
                                        textSnippets: null,
                                        tokenizerRuleSet: NGramTokenization.ObjectMother.NGramTokenizerRuleSet_MonoBiTriFourFive,
                                        labeledExamples: LabeledExamples.ObjectMother.ShortLabeledExamples,
                                        minimumAccuracySingleLabel: SettingBag.DefaultMinimumAccuracySingleLabel,
                                        minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("textSnippets").Message
                ).SetArgDisplayNames($"{nameof(classifyManyExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .ClassifyManyOrDefault(
                                        textSnippets: TextClassifiers.ObjectMother.Snippets_CompleteLabeledExamples00.Select(text => new TextSnippet(text: text)).ToList(),
                                        tokenizerRuleSet: null,
                                        labeledExamples: LabeledExamples.ObjectMother.ShortLabeledExamples,
                                        minimumAccuracySingleLabel: SettingBag.DefaultMinimumAccuracySingleLabel,
                                        minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("tokenizerRuleSet").Message
                ).SetArgDisplayNames($"{nameof(classifyManyExceptionTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .ClassifyManyOrDefault(
                                        textSnippets: TextClassifiers.ObjectMother.Snippets_CompleteLabeledExamples00.Select(text => new TextSnippet(text: text)).ToList(),
                                        tokenizerRuleSet: NGramTokenization.ObjectMother.NGramTokenizerRuleSet_MonoBiTriFourFive,
                                        labeledExamples: null,
                                        minimumAccuracySingleLabel: SettingBag.DefaultMinimumAccuracySingleLabel,
                                        minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("labeledExamples").Message
                ).SetArgDisplayNames($"{nameof(classifyManyExceptionTestCases)}_03")

        };
        private static TestCaseData[] classifyManyTestCases =
        {

            new TestCaseData(
                    TextClassifiers.ObjectMother.Snippets_CompleteLabeledExamples00.Select(text => new TextSnippet(text: text)).ToList(),
                    TextClassifier.DefaultNGramTokenizerRuleSet,
                    LabeledExamples.ObjectMother.CreateThirtyCompleteLabeledExamples(),
                    0.0,
                    SettingBag.DefaultMinimumAccuracyMultipleLabels,
                    new ClassifyOutput(
                            minimumAccuracySingleLabel: 0.0,
                            minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels,
                            results: TextClassifiers.ObjectMother.TextClassifierResults_CompleteLabeledExamples00,
                            version: TextClassifier.DefaultVersionFunction()
                        )
                ).SetArgDisplayNames($"{nameof(classifyManyTestCases)}_01"),

            new TestCaseData(
                    TextClassifiers.ObjectMother.Snippets_Untokenizable.Select(text => new TextSnippet(text: text)).ToList(),
                    NGramTokenization.ObjectMother.NGramTokenizerRuleSet_Five,
                    LabeledExamples.ObjectMother.CreateThirtyCompleteLabeledExamples(),
                    0.0,
                    SettingBag.DefaultMinimumAccuracyMultipleLabels,
                    new ClassifyOutput(
                            minimumAccuracySingleLabel: 0.0,
                            minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels,
                            results: TextClassifiers.ObjectMother.TextClassifierResults_Untokenizable,
                            version: TextClassifier.DefaultVersionFunction()
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
            Assert.That(actual, Is.InstanceOf<TextClassifier>());

            Assert.That(TextClassifier.DefaultNGramTokenizerRuleSet, Is.InstanceOf<INGramTokenizerRuleSet>());
            Assert.That(TextClassifier.DefaultTextClassifierResult, Is.InstanceOf<TextClassifierResult>());
            Assert.That(TextClassifier.SimilarityIndexDisabler, Is.InstanceOf<Func<ClassifyOutput, dynamic>>());
            Assert.That(TextClassifier.DefaultNowFunction, Is.InstanceOf<Func<DateTime>>());
            Assert.That(TextClassifier.DefaultUTCNowFunction, Is.InstanceOf<Func<DateTime>>());
            Assert.That(TextClassifier.DefaultVersionFunction, Is.InstanceOf<Func<string>>());
            Assert.That(TextClassifier.DefaultRoundingFunction, Is.InstanceOf<Func<double, double>>());

        }

        [TestCaseSource(nameof(classifyOrDefaultWhenAllRulesFailedTestCases))]
        public void ClassifyOrDefault_ShouldReturnExpectedTextClassifierSession_WhenAllRulesFailed
            (TextSnippet textSnippet, INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples, ClassifyOutput expected)
        {

            // Arrange
            // Act
            ClassifyOutput actual 
                = new TextClassifier().ClassifyOrDefault(
                    textSnippet, 
                    tokenizerRuleSet, 
                    labeledExamples, 
                    SettingBag.DefaultMinimumAccuracySingleLabel,
                    SettingBag.DefaultMinimumAccuracyMultipleLabels);

            // Assert
            Assert.That(
                    TextClassifiers.ObjectMother.AreEqual(expected, actual),
                    Is.True
                );

        }

        [TestCaseSource(nameof(classifyOrDefaultWhenUntokenizableExamples))]
        public void ClassifyOrDefault_ShouldReturnSessionWithDefaultClassifierResult_WhenUntokenizableExamples
            (TextSnippet textSnippet, INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples, ClassifyOutput expected)
        {

            // Arrange
            // Act
            ClassifyOutput actual 
                = new TextClassifier().ClassifyOrDefault(
                    textSnippet, 
                    tokenizerRuleSet, 
                    labeledExamples, 
                    SettingBag.DefaultMinimumAccuracySingleLabel,
                    SettingBag.DefaultMinimumAccuracyMultipleLabels);

            // Assert
            Assert.That(
                    TextClassifiers.ObjectMother.AreEqual(expected, actual),
                    Is.True
                );

        }

        [TestCaseSource(nameof(classifyOrDefaultWhenOneLabeledExampleAndSuccessfulClassification))]
        public void ClassifyOrDefault_ShouldReturnExpectedLabel_WhenOneLabeledExampleAndSuccessfulClassification
            (TextSnippet textSnippet, INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples, string expectedLabel)
        {

            // Arrange
            // Act
            ClassifyOutput actual 
                = new TextClassifier().ClassifyOrDefault(
                    textSnippet, 
                    tokenizerRuleSet, 
                    labeledExamples,
                    SettingBag.DefaultMinimumAccuracySingleLabel,
                    SettingBag.DefaultMinimumAccuracyMultipleLabels);

            // Assert
            Assert.That(expectedLabel, Is.EqualTo(actual.Results[0].Label));

        }

        [TestCaseSource(nameof(classifyOrDefaultWhenThirtyLabeledExamplesAndSuccessfulClassification))]
        public void ClassifyOrDefault_ShouldReturnExpectedTextClassifierSession_WhenThirtyLabeledExamplesAndSuccessfulClassification(
            TextSnippet textSnippet, 
            INGramTokenizerRuleSet tokenizerRuleSet, 
            List<LabeledExample> labeledExamples,
            double minimumAccuracySingleLabel,
            double minimumAccuracyMultipleLabels,
            ClassifyOutput expected)
        {

            // Arrange
            // Act
            ClassifyOutput actual 
                = new TextClassifier().ClassifyOrDefault(
                    textSnippet, 
                    tokenizerRuleSet, 
                    labeledExamples,
                    minimumAccuracySingleLabel,
                    minimumAccuracyMultipleLabels);

            // Assert
            Assert.That(
                    TextClassifiers.ObjectMother.AreEqual(expected, actual),
                    Is.True
                );

        }

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

        [TestCaseSource(nameof(classifyManyExceptionTestCases))]
        public void ClassifyMany_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(classifyManyTestCases))]
        public void ClassifyMany_ShouldReturnExpectedTextClassifierSession_WhenInvoked(
            List<TextSnippet> textSnippets, 
            INGramTokenizerRuleSet tokenizerRuleSet, 
            List<LabeledExample> labeledExamples, 
            double minimumAccuracySingleLabel,
            double minimumAccuracyMultipleLabels,
            ClassifyOutput expected)
        {

            // Arrange
            // Act
            ClassifyOutput actual
                = new TextClassifier().ClassifyManyOrDefault(
                    textSnippets: textSnippets, 
                    tokenizerRuleSet: tokenizerRuleSet, 
                    labeledExamples: labeledExamples,
                    minimumAccuracySingleLabel: minimumAccuracySingleLabel, 
                    minimumAccuracyMultipleLabels: minimumAccuracyMultipleLabels);

            // Assert
            Assert.That(
                    TextClassifiers.ObjectMother.AreEqual(expected, actual),
                    Is.True
                );

        }


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
                    fileManager: new FakeFileManager(LabeledExamples.ObjectMother.ShortLabeledExamplesAsJson_Content));

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


        [TestCaseSource(nameof(loadTextSnippetsOrDefaultExceptionTestCases))]
        public void LoadTextSnippetsOrDefault_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void LoadTextSnippetsOrDefault_ShouldReturnExpectedCollectionOfTextSnippets_WhenProperJsonFileContent()
        {

            // Arrange
            TextClassifier textClassifier 
                = new TextClassifier(fileManager: new FakeFileManager(LabeledExamples.ObjectMother.ShortLabeledExamplesAsJson_Content));

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


        [TestCaseSource(nameof(loadTokenizerRuleSetOrDefaultExceptionTestCases))]
        public void LoadTokenizerRuleSetOrDefault_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void LoadTokenizerRuleSetOrDefault_ShouldReturnExpectedTokenizerRuleSet_WhenProperJsonFileContent()
        {

            // Arrange
            TextClassifier textClassifier 
                = new TextClassifier(fileManager: new FakeFileManager(TextClassifiers.ObjectMother.TokenizerRuleSetAsJson_Content));

            IFileInfoAdapter fakeJsonFile = new FakeFileInfoAdapter(true, @"C:\TokenizerRuleSet.json");

            // Act
            NGramTokenizerRuleSet actual = textClassifier.LoadTokenizerRuleSetOrDefault(fakeJsonFile);

            // Assert
            Assert.That(
                    NGramTokenization.ObjectMother.AreEqual(TextClassifiers.ObjectMother.TokenizerRuleSet, actual),
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


        [Test]
        public void SaveLabeledExamples_ShouldSaveCollectionOfLabeledExamplesAndNotThrowException_WhenProperArguments()
        {

            // Arrange
            Func<DateTime> fakeNowFunction = () => Filenames.ObjectMother.FakeNow;
            Func<string> fakeVersionFunction = () => "1.0.0";

            TextClassifier textClassifier 
                = new TextClassifier(
                    fileManager: new FakeFileManager(LabeledExamples.ObjectMother.ShortLabeledExamplesAsJson_Content),
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
        public void SaveTextSnippets_ShouldSaveCollectionOfTextSnippetsAndNotThrowException_WhenProperArguments()
        {

            // Arrange
            Func<DateTime> fakeNowFunction = () => Filenames.ObjectMother.FakeNow;
            Func<string> fakeVersionFunction = () => "1.0.0";

            TextClassifier textClassifier 
                = new TextClassifier(
                    fileManager: new FakeFileManager(LabeledExamples.ObjectMother.ShortLabeledExamplesAsJson_Content),
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

        [TestCase(false)]
        [TestCase(true)]
        public void SaveSession_ShouldSaveSessionAndNotThrowException_WhenInvoked(bool disableIndexSerialization)
        {

            // Arrange
            Func<DateTime> fakeNowFunction = () => Filenames.ObjectMother.FakeNow;
            Func<string> fakeVersionFunction = () => "1.0.0";

            TextClassifier textClassifier 
                = new TextClassifier(
                        fileManager: new FakeFileManager(TextClassifiers.ObjectMother.ClassifyOutputCLE00AsJson_Content),
                        nowFunction: fakeNowFunction,
                        versionFunction: fakeVersionFunction);

            string folderPath = Path.GetFullPath(Filenames.ObjectMother.FakeFilePath);
            string fileName = $"nwngram_session_{Filenames.ObjectMother.FakeNowString}.json";
            string filePath = Path.Combine(folderPath, fileName);
            IFileInfoAdapter fakeJsonFile = new FakeFileInfoAdapter(true, filePath);

            // Act
            // Assert
            Assert.DoesNotThrow(() => 
                textClassifier.SaveClassifyOutput(TextClassifiers.ObjectMother.ClassifyOutput_CompleteLabeledExamples00, folderPath, disableIndexSerialization));

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
                                            content: LabeledExamples.ObjectMother.ShortLabeledExamplesAsJson_Content,
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
                                    content: LabeledExamples.ObjectMother.ShortLabeledExamplesAsJson_Content,
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

        [Test]
        public void SaveSession_ShouldThrowExpectedException_WhenWriteAllTextFails()
        {

            // Arrange
            Func<DateTime> fakeNowFunction = () => Filenames.ObjectMother.FakeNow;
            Func<string> fakeVersionFunction = () => "1.0.0";

            string writeExceptionMessage = "A random write-to-disk issue.";

            TextClassifier textClassifier 
                = new TextClassifier(
                        fileManager: new FakeFileManagerThrowingWriteExceptions(
                                        content: TextClassifiers.ObjectMother.ClassifyOutputCLE00AsJson_Content,
                                        writeExceptionMessage: writeExceptionMessage),
                        nowFunction: fakeNowFunction,
                        versionFunction: fakeVersionFunction);

            string folderPath = Path.GetFullPath(Filenames.ObjectMother.FakeFilePath);
            string fileName = $"nwngram_session_{Filenames.ObjectMother.FakeNowString}.json";
            string filePath = Path.Combine(folderPath, fileName);
            IFileInfoAdapter fakeJsonFile = new FakeFileInfoAdapter(true, filePath);

            // Act
            // Assert
            Exception e = Assert.Throws<Exception>(() => 
                textClassifier.SaveClassifyOutput(TextClassifiers.ObjectMother.ClassifyOutput_CompleteLabeledExamples00, folderPath, false));

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

        [TestCaseSource(nameof(convertExceptionTestCases))]
        public void Convert_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

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

            TextClassifier textClassifier 
                = new TextClassifier(
                        nowFunction: fakeNowFunction,
                        versionFunction: fakeVersionFunction);

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
            dynamic actual = TextClassifier.SimilarityIndexDisabler(ObjectMother.ClassifyOutput_CompleteLabeledExamples00);

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