using System;
using NW.NGramTextClassification.AsciiBanner;
using NW.NGramTextClassification.Filenames;
using NW.NGramTextClassification.Files;
using NW.NGramTextClassification.LabeledExamples;
using NW.NGramTextClassification.NGramTokenization;
using NW.NGramTextClassification.Serializations;
using NW.NGramTextClassification.Similarity;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests
{
    [TestFixture]
    public class TextClassifierComponentsTests
    {

        #region Fields

        private static TestCaseData[] textClassifierComponentsExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifierComponents(
                                        nGramsTokenizer: null,
                                        similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                                        roundingFunction: TextClassifierComponents.DefaultRoundingFunction,
                                        textTruncatingFunction: TextClassifierComponents.DefaultTextTruncatingFunction,
                                        loggingAction: TextClassifierComponents.DefaultLoggingAction,
                                        labeledExampleManager: new LabeledExampleManager(),
                                        asciiBannerManager: new AsciiBannerManager(),
                                        loggingActionAsciiBanner: TextClassifierComponents.DefaultLoggingActionAsciiBanner,
                                        fileManager: new FileManager(),
                                        serializerFactory: new SerializerFactory(),
                                        filenameFactory: new FilenameFactory()
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("nGramsTokenizer").Message
                ).SetArgDisplayNames($"{nameof(textClassifierComponentsExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifierComponents(
                                        nGramsTokenizer: new NGramTokenizer(),
                                        similarityIndexCalculator: null,
                                        roundingFunction: TextClassifierComponents.DefaultRoundingFunction,
                                        textTruncatingFunction: TextClassifierComponents.DefaultTextTruncatingFunction,
                                        loggingAction: TextClassifierComponents.DefaultLoggingAction,
                                        labeledExampleManager: new LabeledExampleManager(),
                                        asciiBannerManager: new AsciiBannerManager(),
                                        loggingActionAsciiBanner: TextClassifierComponents.DefaultLoggingActionAsciiBanner,
                                        fileManager: new FileManager(),
                                        serializerFactory: new SerializerFactory(),
                                        filenameFactory: new FilenameFactory()
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("similarityIndexCalculator").Message
                ).SetArgDisplayNames($"{nameof(textClassifierComponentsExceptionTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifierComponents(
                                        nGramsTokenizer: new NGramTokenizer(),
                                        similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                                        roundingFunction: null,
                                        textTruncatingFunction: TextClassifierComponents.DefaultTextTruncatingFunction,
                                        loggingAction: TextClassifierComponents.DefaultLoggingAction,
                                        labeledExampleManager: new LabeledExampleManager(),
                                        asciiBannerManager: new AsciiBannerManager(),
                                        loggingActionAsciiBanner: TextClassifierComponents.DefaultLoggingActionAsciiBanner,
                                        fileManager: new FileManager(),
                                        serializerFactory: new SerializerFactory(),
                                        filenameFactory: new FilenameFactory()
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("roundingFunction").Message
                ).SetArgDisplayNames($"{nameof(textClassifierComponentsExceptionTestCases)}_03"),

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifierComponents(
                                        nGramsTokenizer: new NGramTokenizer(),
                                        similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                                        roundingFunction: TextClassifierComponents.DefaultRoundingFunction,
                                        textTruncatingFunction: null,
                                        loggingAction: TextClassifierComponents.DefaultLoggingAction,
                                        labeledExampleManager: new LabeledExampleManager(),
                                        asciiBannerManager: new AsciiBannerManager(),
                                        loggingActionAsciiBanner: TextClassifierComponents.DefaultLoggingActionAsciiBanner,
                                        fileManager: new FileManager(),
                                        serializerFactory: new SerializerFactory(),
                                        filenameFactory: new FilenameFactory()
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("textTruncatingFunction").Message
                ).SetArgDisplayNames($"{nameof(textClassifierComponentsExceptionTestCases)}_04"),

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifierComponents(
                                        nGramsTokenizer: new NGramTokenizer(),
                                        similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                                        roundingFunction: TextClassifierComponents.DefaultRoundingFunction,
                                        textTruncatingFunction: TextClassifierComponents.DefaultTextTruncatingFunction,
                                        loggingAction: null,
                                        labeledExampleManager: new LabeledExampleManager(),
                                        asciiBannerManager: new AsciiBannerManager(),
                                        loggingActionAsciiBanner: TextClassifierComponents.DefaultLoggingActionAsciiBanner,
                                        fileManager: new FileManager(),
                                        serializerFactory: new SerializerFactory(),
                                        filenameFactory: new FilenameFactory()
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("loggingAction").Message
                ).SetArgDisplayNames($"{nameof(textClassifierComponentsExceptionTestCases)}_05"),

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifierComponents(
                                        nGramsTokenizer: new NGramTokenizer(),
                                        similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                                        roundingFunction: TextClassifierComponents.DefaultRoundingFunction,
                                        textTruncatingFunction: TextClassifierComponents.DefaultTextTruncatingFunction,
                                        loggingAction: TextClassifierComponents.DefaultLoggingAction,
                                        labeledExampleManager: null,
                                        asciiBannerManager: new AsciiBannerManager(),
                                        loggingActionAsciiBanner: TextClassifierComponents.DefaultLoggingActionAsciiBanner,
                                        fileManager: new FileManager(),
                                        serializerFactory: new SerializerFactory(),
                                        filenameFactory: new FilenameFactory()
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("labeledExampleManager").Message
                ).SetArgDisplayNames($"{nameof(textClassifierComponentsExceptionTestCases)}_06"),

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifierComponents(
                                        nGramsTokenizer: new NGramTokenizer(),
                                        similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                                        roundingFunction: TextClassifierComponents.DefaultRoundingFunction,
                                        textTruncatingFunction: TextClassifierComponents.DefaultTextTruncatingFunction,
                                        loggingAction: TextClassifierComponents.DefaultLoggingAction,
                                        labeledExampleManager: new LabeledExampleManager(),
                                        asciiBannerManager: null,
                                        loggingActionAsciiBanner: TextClassifierComponents.DefaultLoggingActionAsciiBanner,
                                        fileManager: new FileManager(),
                                        serializerFactory: new SerializerFactory(),
                                        filenameFactory: new FilenameFactory()
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("asciiBannerManager").Message
                ).SetArgDisplayNames($"{nameof(textClassifierComponentsExceptionTestCases)}_07"),

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifierComponents(
                                        nGramsTokenizer: new NGramTokenizer(),
                                        similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                                        roundingFunction: TextClassifierComponents.DefaultRoundingFunction,
                                        textTruncatingFunction: TextClassifierComponents.DefaultTextTruncatingFunction,
                                        loggingAction: TextClassifierComponents.DefaultLoggingAction,
                                        labeledExampleManager: new LabeledExampleManager(),
                                        asciiBannerManager: new AsciiBannerManager(),
                                        loggingActionAsciiBanner:  null,
                                        fileManager: new FileManager(),
                                        serializerFactory: new SerializerFactory(),
                                        filenameFactory: new FilenameFactory()
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("loggingActionAsciiBanner").Message
                ).SetArgDisplayNames($"{nameof(textClassifierComponentsExceptionTestCases)}_08"),

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifierComponents(
                                        nGramsTokenizer: new NGramTokenizer(),
                                        similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                                        roundingFunction: TextClassifierComponents.DefaultRoundingFunction,
                                        textTruncatingFunction: TextClassifierComponents.DefaultTextTruncatingFunction,
                                        loggingAction: TextClassifierComponents.DefaultLoggingAction,
                                        labeledExampleManager: new LabeledExampleManager(),
                                        asciiBannerManager: new AsciiBannerManager(),
                                        loggingActionAsciiBanner:  TextClassifierComponents.DefaultLoggingActionAsciiBanner,
                                        fileManager: null,
                                        serializerFactory: new SerializerFactory(),
                                        filenameFactory: new FilenameFactory()
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("fileManager").Message
                ).SetArgDisplayNames($"{nameof(textClassifierComponentsExceptionTestCases)}_09"),

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifierComponents(
                                        nGramsTokenizer: new NGramTokenizer(),
                                        similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                                        roundingFunction: TextClassifierComponents.DefaultRoundingFunction,
                                        textTruncatingFunction: TextClassifierComponents.DefaultTextTruncatingFunction,
                                        loggingAction: TextClassifierComponents.DefaultLoggingAction,
                                        labeledExampleManager: new LabeledExampleManager(),
                                        asciiBannerManager: new AsciiBannerManager(),
                                        loggingActionAsciiBanner:  TextClassifierComponents.DefaultLoggingActionAsciiBanner,
                                        fileManager: new FileManager(),
                                        serializerFactory: null,
                                        filenameFactory: new FilenameFactory()
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("serializerFactory").Message
                ).SetArgDisplayNames($"{nameof(textClassifierComponentsExceptionTestCases)}_10"),

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifierComponents(
                                        nGramsTokenizer: new NGramTokenizer(),
                                        similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                                        roundingFunction: TextClassifierComponents.DefaultRoundingFunction,
                                        textTruncatingFunction: TextClassifierComponents.DefaultTextTruncatingFunction,
                                        loggingAction: TextClassifierComponents.DefaultLoggingAction,
                                        labeledExampleManager: new LabeledExampleManager(),
                                        asciiBannerManager: new AsciiBannerManager(),
                                        loggingActionAsciiBanner:  TextClassifierComponents.DefaultLoggingActionAsciiBanner,
                                        fileManager: new FileManager(),
                                        serializerFactory: new SerializerFactory(),
                                        filenameFactory: null
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("filenameFactory").Message
                ).SetArgDisplayNames($"{nameof(textClassifierComponentsExceptionTestCases)}_11")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(textClassifierComponentsExceptionTestCases))]
        public void TextClassifierComponents_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void TextClassifierComponents_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            TextClassifierComponents actual = new TextClassifierComponents();

            // Assert
            Assert.IsInstanceOf<TextClassifierComponents>(actual);

            Assert.IsInstanceOf<INGramTokenizer>(actual.NGramsTokenizer);
            Assert.IsInstanceOf<ISimilarityIndexCalculator>(actual.SimilarityIndexCalculator);
            Assert.IsInstanceOf<Func<double, double>>(actual.RoundingFunction);
            Assert.IsInstanceOf<Func<string, uint, string>>(actual.TextTruncatingFunction);
            Assert.IsInstanceOf<Action<string>>(actual.LoggingAction);
            Assert.IsInstanceOf<ILabeledExampleManager>(actual.LabeledExampleManager);
            Assert.IsInstanceOf<IAsciiBannerManager>(actual.AsciiBannerManager);
            Assert.IsInstanceOf<Action<string>>(actual.LoggingActionAsciiBanner);
            Assert.IsInstanceOf<IFileManager>(actual.FileManager);
            Assert.IsInstanceOf<ISerializerFactory>(actual.SerializerFactory);
            Assert.IsInstanceOf<IFilenameFactory>(actual.FilenameFactory);

            Assert.IsInstanceOf<Func<double, double>>(TextClassifierComponents.DefaultRoundingFunction);
            Assert.IsInstanceOf<Func<string, uint, string>>(TextClassifierComponents.DefaultTextTruncatingFunction);
            Assert.IsInstanceOf<string>(TextClassifierComponents.DefaultLoggingActionDateFormat);
            Assert.IsInstanceOf<Action<string>>(TextClassifierComponents.DefaultLoggingAction);
            Assert.IsInstanceOf<Action<string>>(TextClassifierComponents.DefaultLoggingActionAsciiBanner);

        }

        [TestCase(null, (uint)20, null)]
        [TestCase("", (uint)20, "")]
        [TestCase("some string", (uint)20, "some string")]
        [TestCase("some string some string", (uint)20, "some string some str...")]
        [TestCase("a", (uint)20, "a")]
        public void DefaultTextTruncatingFunction_ShouldTruncateTextAsExpected_WhenInvoked
            (string text, uint length, string expected)
        {

            // Arrange
            // Act
            string actual = TextClassifierComponents.DefaultTextTruncatingFunction(text, length);

            // Assert
            Assert.AreEqual(expected, actual);

        }

        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 13.10.2022
*/