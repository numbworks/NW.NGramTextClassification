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
    public class ComponentCollectionTests
    {

        #region Fields

        private static TestCaseData[] componentCollectionExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new ComponentCollection(
                                        nGramsTokenizer: null,
                                        similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                                        roundingFunction: ComponentCollection.DefaultRoundingFunction,
                                        textTruncatingFunction: ComponentCollection.DefaultTextTruncatingFunction,
                                        loggingAction: ComponentCollection.DefaultLoggingAction,
                                        labeledExampleManager: new LabeledExampleManager(),
                                        asciiBannerManager: new AsciiBannerManager(),
                                        loggingActionAsciiBanner: ComponentCollection.DefaultLoggingActionAsciiBanner,
                                        fileManager: new FileManager(),
                                        serializerFactory: new SerializerFactory(),
                                        filenameFactory: new FilenameFactory(),
                                        nowFunction: ComponentCollection.DefaultNowFunction
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("nGramsTokenizer").Message
                ).SetArgDisplayNames($"{nameof(componentCollectionExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new ComponentCollection(
                                        nGramsTokenizer: new NGramTokenizer(),
                                        similarityIndexCalculator: null,
                                        roundingFunction: ComponentCollection.DefaultRoundingFunction,
                                        textTruncatingFunction: ComponentCollection.DefaultTextTruncatingFunction,
                                        loggingAction: ComponentCollection.DefaultLoggingAction,
                                        labeledExampleManager: new LabeledExampleManager(),
                                        asciiBannerManager: new AsciiBannerManager(),
                                        loggingActionAsciiBanner: ComponentCollection.DefaultLoggingActionAsciiBanner,
                                        fileManager: new FileManager(),
                                        serializerFactory: new SerializerFactory(),
                                        filenameFactory: new FilenameFactory(),
                                        nowFunction: ComponentCollection.DefaultNowFunction
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("similarityIndexCalculator").Message
                ).SetArgDisplayNames($"{nameof(componentCollectionExceptionTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                        () => new ComponentCollection(
                                        nGramsTokenizer: new NGramTokenizer(),
                                        similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                                        roundingFunction: null,
                                        textTruncatingFunction: ComponentCollection.DefaultTextTruncatingFunction,
                                        loggingAction: ComponentCollection.DefaultLoggingAction,
                                        labeledExampleManager: new LabeledExampleManager(),
                                        asciiBannerManager: new AsciiBannerManager(),
                                        loggingActionAsciiBanner: ComponentCollection.DefaultLoggingActionAsciiBanner,
                                        fileManager: new FileManager(),
                                        serializerFactory: new SerializerFactory(),
                                        filenameFactory: new FilenameFactory(),
                                        nowFunction: ComponentCollection.DefaultNowFunction
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("roundingFunction").Message
                ).SetArgDisplayNames($"{nameof(componentCollectionExceptionTestCases)}_03"),

            new TestCaseData(
                new TestDelegate(
                        () => new ComponentCollection(
                                        nGramsTokenizer: new NGramTokenizer(),
                                        similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                                        roundingFunction: ComponentCollection.DefaultRoundingFunction,
                                        textTruncatingFunction: null,
                                        loggingAction: ComponentCollection.DefaultLoggingAction,
                                        labeledExampleManager: new LabeledExampleManager(),
                                        asciiBannerManager: new AsciiBannerManager(),
                                        loggingActionAsciiBanner: ComponentCollection.DefaultLoggingActionAsciiBanner,
                                        fileManager: new FileManager(),
                                        serializerFactory: new SerializerFactory(),
                                        filenameFactory: new FilenameFactory(),
                                        nowFunction: ComponentCollection.DefaultNowFunction
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("textTruncatingFunction").Message
                ).SetArgDisplayNames($"{nameof(componentCollectionExceptionTestCases)}_04"),

            new TestCaseData(
                new TestDelegate(
                        () => new ComponentCollection(
                                        nGramsTokenizer: new NGramTokenizer(),
                                        similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                                        roundingFunction: ComponentCollection.DefaultRoundingFunction,
                                        textTruncatingFunction: ComponentCollection.DefaultTextTruncatingFunction,
                                        loggingAction: null,
                                        labeledExampleManager: new LabeledExampleManager(),
                                        asciiBannerManager: new AsciiBannerManager(),
                                        loggingActionAsciiBanner: ComponentCollection.DefaultLoggingActionAsciiBanner,
                                        fileManager: new FileManager(),
                                        serializerFactory: new SerializerFactory(),
                                        filenameFactory: new FilenameFactory(),
                                        nowFunction: ComponentCollection.DefaultNowFunction
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("loggingAction").Message
                ).SetArgDisplayNames($"{nameof(componentCollectionExceptionTestCases)}_05"),

            new TestCaseData(
                new TestDelegate(
                        () => new ComponentCollection(
                                        nGramsTokenizer: new NGramTokenizer(),
                                        similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                                        roundingFunction: ComponentCollection.DefaultRoundingFunction,
                                        textTruncatingFunction: ComponentCollection.DefaultTextTruncatingFunction,
                                        loggingAction: ComponentCollection.DefaultLoggingAction,
                                        labeledExampleManager: null,
                                        asciiBannerManager: new AsciiBannerManager(),
                                        loggingActionAsciiBanner: ComponentCollection.DefaultLoggingActionAsciiBanner,
                                        fileManager: new FileManager(),
                                        serializerFactory: new SerializerFactory(),
                                        filenameFactory: new FilenameFactory(),
                                        nowFunction: ComponentCollection.DefaultNowFunction
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("labeledExampleManager").Message
                ).SetArgDisplayNames($"{nameof(componentCollectionExceptionTestCases)}_06"),

            new TestCaseData(
                new TestDelegate(
                        () => new ComponentCollection(
                                        nGramsTokenizer: new NGramTokenizer(),
                                        similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                                        roundingFunction: ComponentCollection.DefaultRoundingFunction,
                                        textTruncatingFunction: ComponentCollection.DefaultTextTruncatingFunction,
                                        loggingAction: ComponentCollection.DefaultLoggingAction,
                                        labeledExampleManager: new LabeledExampleManager(),
                                        asciiBannerManager: null,
                                        loggingActionAsciiBanner: ComponentCollection.DefaultLoggingActionAsciiBanner,
                                        fileManager: new FileManager(),
                                        serializerFactory: new SerializerFactory(),
                                        filenameFactory: new FilenameFactory(),
                                        nowFunction: ComponentCollection.DefaultNowFunction
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("asciiBannerManager").Message
                ).SetArgDisplayNames($"{nameof(componentCollectionExceptionTestCases)}_07"),

            new TestCaseData(
                new TestDelegate(
                        () => new ComponentCollection(
                                        nGramsTokenizer: new NGramTokenizer(),
                                        similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                                        roundingFunction: ComponentCollection.DefaultRoundingFunction,
                                        textTruncatingFunction: ComponentCollection.DefaultTextTruncatingFunction,
                                        loggingAction: ComponentCollection.DefaultLoggingAction,
                                        labeledExampleManager: new LabeledExampleManager(),
                                        asciiBannerManager: new AsciiBannerManager(),
                                        loggingActionAsciiBanner:  null,
                                        fileManager: new FileManager(),
                                        serializerFactory: new SerializerFactory(),
                                        filenameFactory: new FilenameFactory(),
                                        nowFunction: ComponentCollection.DefaultNowFunction
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("loggingActionAsciiBanner").Message
                ).SetArgDisplayNames($"{nameof(componentCollectionExceptionTestCases)}_08"),

            new TestCaseData(
                new TestDelegate(
                        () => new ComponentCollection(
                                        nGramsTokenizer: new NGramTokenizer(),
                                        similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                                        roundingFunction: ComponentCollection.DefaultRoundingFunction,
                                        textTruncatingFunction: ComponentCollection.DefaultTextTruncatingFunction,
                                        loggingAction: ComponentCollection.DefaultLoggingAction,
                                        labeledExampleManager: new LabeledExampleManager(),
                                        asciiBannerManager: new AsciiBannerManager(),
                                        loggingActionAsciiBanner:  ComponentCollection.DefaultLoggingActionAsciiBanner,
                                        fileManager: null,
                                        serializerFactory: new SerializerFactory(),
                                        filenameFactory: new FilenameFactory(),
                                        nowFunction: ComponentCollection.DefaultNowFunction
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("fileManager").Message
                ).SetArgDisplayNames($"{nameof(componentCollectionExceptionTestCases)}_09"),

            new TestCaseData(
                new TestDelegate(
                        () => new ComponentCollection(
                                        nGramsTokenizer: new NGramTokenizer(),
                                        similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                                        roundingFunction: ComponentCollection.DefaultRoundingFunction,
                                        textTruncatingFunction: ComponentCollection.DefaultTextTruncatingFunction,
                                        loggingAction: ComponentCollection.DefaultLoggingAction,
                                        labeledExampleManager: new LabeledExampleManager(),
                                        asciiBannerManager: new AsciiBannerManager(),
                                        loggingActionAsciiBanner:  ComponentCollection.DefaultLoggingActionAsciiBanner,
                                        fileManager: new FileManager(),
                                        serializerFactory: null,
                                        filenameFactory: new FilenameFactory(),
                                        nowFunction: ComponentCollection.DefaultNowFunction
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("serializerFactory").Message
                ).SetArgDisplayNames($"{nameof(componentCollectionExceptionTestCases)}_10"),

            new TestCaseData(
                new TestDelegate(
                        () => new ComponentCollection(
                                        nGramsTokenizer: new NGramTokenizer(),
                                        similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                                        roundingFunction: ComponentCollection.DefaultRoundingFunction,
                                        textTruncatingFunction: ComponentCollection.DefaultTextTruncatingFunction,
                                        loggingAction: ComponentCollection.DefaultLoggingAction,
                                        labeledExampleManager: new LabeledExampleManager(),
                                        asciiBannerManager: new AsciiBannerManager(),
                                        loggingActionAsciiBanner:  ComponentCollection.DefaultLoggingActionAsciiBanner,
                                        fileManager: new FileManager(),
                                        serializerFactory: new SerializerFactory(),
                                        filenameFactory: null,
                                        nowFunction: ComponentCollection.DefaultNowFunction
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("filenameFactory").Message
                ).SetArgDisplayNames($"{nameof(componentCollectionExceptionTestCases)}_11"),

            new TestCaseData(
                new TestDelegate(
                        () => new ComponentCollection(
                                        nGramsTokenizer: new NGramTokenizer(),
                                        similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                                        roundingFunction: ComponentCollection.DefaultRoundingFunction,
                                        textTruncatingFunction: ComponentCollection.DefaultTextTruncatingFunction,
                                        loggingAction: ComponentCollection.DefaultLoggingAction,
                                        labeledExampleManager: new LabeledExampleManager(),
                                        asciiBannerManager: new AsciiBannerManager(),
                                        loggingActionAsciiBanner:  ComponentCollection.DefaultLoggingActionAsciiBanner,
                                        fileManager: new FileManager(),
                                        serializerFactory: new SerializerFactory(),
                                        filenameFactory: new FilenameFactory(),
                                        nowFunction: null
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("nowFunction").Message
                ).SetArgDisplayNames($"{nameof(componentCollectionExceptionTestCases)}_12")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(componentCollectionExceptionTestCases))]
        public void ComponentCollection_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void ComponentCollection_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            ComponentCollection actual = new ComponentCollection();

            // Assert
            Assert.IsInstanceOf<ComponentCollection>(actual);

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
            Assert.IsInstanceOf<Func<DateTime>>(actual.NowFunction);

            Assert.IsInstanceOf<Func<double, double>>(ComponentCollection.DefaultRoundingFunction);
            Assert.IsInstanceOf<Func<string, uint, string>>(ComponentCollection.DefaultTextTruncatingFunction);
            Assert.IsInstanceOf<string>(ComponentCollection.DefaultLoggingActionDateFormat);
            Assert.IsInstanceOf<Action<string>>(ComponentCollection.DefaultLoggingAction);
            Assert.IsInstanceOf<Action<string>>(ComponentCollection.DefaultLoggingActionAsciiBanner);
            Assert.IsInstanceOf<Func<DateTime>>(ComponentCollection.DefaultNowFunction);

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
            string actual = ComponentCollection.DefaultTextTruncatingFunction(text, length);

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
    Last Update: 25.01.2024
*/