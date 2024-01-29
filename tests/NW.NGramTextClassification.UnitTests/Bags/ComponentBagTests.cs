using System;
using NW.NGramTextClassification.AsciiBanner;
using NW.NGramTextClassification.Filenames;
using NW.NGramTextClassification.Files;
using NW.NGramTextClassification.LabeledExamples;
using NW.NGramTextClassification.NGramTokenization;
using NW.NGramTextClassification.Serializations;
using NW.NGramTextClassification.Similarity;
using NUnit.Framework;
using NW.NGramTextClassification.Bags;

namespace NW.NGramTextClassification.UnitTests.Bags
{
    [TestFixture]
    public class ComponentBagTests
    {

        #region Fields

        private static TestCaseData[] componentBagExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new ComponentBag(
                                        nGramsTokenizer: null,
                                        similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                                        roundingFunction: ComponentBag.DefaultRoundingFunction,
                                        textTruncatingFunction: ComponentBag.DefaultTextTruncatingFunction,
                                        loggingAction: ComponentBag.DefaultLoggingAction,
                                        labeledExampleManager: new LabeledExampleManager(),
                                        asciiBannerManager: new AsciiBannerManager(),
                                        loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                                        fileManager: new FileManager(),
                                        serializerFactory: new SerializerFactory(),
                                        filenameFactory: new FilenameFactory(),
                                        nowFunction: ComponentBag.DefaultNowFunction
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("nGramsTokenizer").Message
                ).SetArgDisplayNames($"{nameof(componentBagExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new ComponentBag(
                                        nGramsTokenizer: new NGramTokenizer(),
                                        similarityIndexCalculator: null,
                                        roundingFunction: ComponentBag.DefaultRoundingFunction,
                                        textTruncatingFunction: ComponentBag.DefaultTextTruncatingFunction,
                                        loggingAction: ComponentBag.DefaultLoggingAction,
                                        labeledExampleManager: new LabeledExampleManager(),
                                        asciiBannerManager: new AsciiBannerManager(),
                                        loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                                        fileManager: new FileManager(),
                                        serializerFactory: new SerializerFactory(),
                                        filenameFactory: new FilenameFactory(),
                                        nowFunction: ComponentBag.DefaultNowFunction
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("similarityIndexCalculator").Message
                ).SetArgDisplayNames($"{nameof(componentBagExceptionTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                        () => new ComponentBag(
                                        nGramsTokenizer: new NGramTokenizer(),
                                        similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                                        roundingFunction: null,
                                        textTruncatingFunction: ComponentBag.DefaultTextTruncatingFunction,
                                        loggingAction: ComponentBag.DefaultLoggingAction,
                                        labeledExampleManager: new LabeledExampleManager(),
                                        asciiBannerManager: new AsciiBannerManager(),
                                        loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                                        fileManager: new FileManager(),
                                        serializerFactory: new SerializerFactory(),
                                        filenameFactory: new FilenameFactory(),
                                        nowFunction: ComponentBag.DefaultNowFunction
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("roundingFunction").Message
                ).SetArgDisplayNames($"{nameof(componentBagExceptionTestCases)}_03"),

            new TestCaseData(
                new TestDelegate(
                        () => new ComponentBag(
                                        nGramsTokenizer: new NGramTokenizer(),
                                        similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                                        roundingFunction: ComponentBag.DefaultRoundingFunction,
                                        textTruncatingFunction: null,
                                        loggingAction: ComponentBag.DefaultLoggingAction,
                                        labeledExampleManager: new LabeledExampleManager(),
                                        asciiBannerManager: new AsciiBannerManager(),
                                        loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                                        fileManager: new FileManager(),
                                        serializerFactory: new SerializerFactory(),
                                        filenameFactory: new FilenameFactory(),
                                        nowFunction: ComponentBag.DefaultNowFunction
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("textTruncatingFunction").Message
                ).SetArgDisplayNames($"{nameof(componentBagExceptionTestCases)}_04"),

            new TestCaseData(
                new TestDelegate(
                        () => new ComponentBag(
                                        nGramsTokenizer: new NGramTokenizer(),
                                        similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                                        roundingFunction: ComponentBag.DefaultRoundingFunction,
                                        textTruncatingFunction: ComponentBag.DefaultTextTruncatingFunction,
                                        loggingAction: null,
                                        labeledExampleManager: new LabeledExampleManager(),
                                        asciiBannerManager: new AsciiBannerManager(),
                                        loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                                        fileManager: new FileManager(),
                                        serializerFactory: new SerializerFactory(),
                                        filenameFactory: new FilenameFactory(),
                                        nowFunction: ComponentBag.DefaultNowFunction
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("loggingAction").Message
                ).SetArgDisplayNames($"{nameof(componentBagExceptionTestCases)}_05"),

            new TestCaseData(
                new TestDelegate(
                        () => new ComponentBag(
                                        nGramsTokenizer: new NGramTokenizer(),
                                        similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                                        roundingFunction: ComponentBag.DefaultRoundingFunction,
                                        textTruncatingFunction: ComponentBag.DefaultTextTruncatingFunction,
                                        loggingAction: ComponentBag.DefaultLoggingAction,
                                        labeledExampleManager: null,
                                        asciiBannerManager: new AsciiBannerManager(),
                                        loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                                        fileManager: new FileManager(),
                                        serializerFactory: new SerializerFactory(),
                                        filenameFactory: new FilenameFactory(),
                                        nowFunction: ComponentBag.DefaultNowFunction
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("labeledExampleManager").Message
                ).SetArgDisplayNames($"{nameof(componentBagExceptionTestCases)}_06"),

            new TestCaseData(
                new TestDelegate(
                        () => new ComponentBag(
                                        nGramsTokenizer: new NGramTokenizer(),
                                        similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                                        roundingFunction: ComponentBag.DefaultRoundingFunction,
                                        textTruncatingFunction: ComponentBag.DefaultTextTruncatingFunction,
                                        loggingAction: ComponentBag.DefaultLoggingAction,
                                        labeledExampleManager: new LabeledExampleManager(),
                                        asciiBannerManager: null,
                                        loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                                        fileManager: new FileManager(),
                                        serializerFactory: new SerializerFactory(),
                                        filenameFactory: new FilenameFactory(),
                                        nowFunction: ComponentBag.DefaultNowFunction
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("asciiBannerManager").Message
                ).SetArgDisplayNames($"{nameof(componentBagExceptionTestCases)}_07"),

            new TestCaseData(
                new TestDelegate(
                        () => new ComponentBag(
                                        nGramsTokenizer: new NGramTokenizer(),
                                        similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                                        roundingFunction: ComponentBag.DefaultRoundingFunction,
                                        textTruncatingFunction: ComponentBag.DefaultTextTruncatingFunction,
                                        loggingAction: ComponentBag.DefaultLoggingAction,
                                        labeledExampleManager: new LabeledExampleManager(),
                                        asciiBannerManager: new AsciiBannerManager(),
                                        loggingActionAsciiBanner:  null,
                                        fileManager: new FileManager(),
                                        serializerFactory: new SerializerFactory(),
                                        filenameFactory: new FilenameFactory(),
                                        nowFunction: ComponentBag.DefaultNowFunction
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("loggingActionAsciiBanner").Message
                ).SetArgDisplayNames($"{nameof(componentBagExceptionTestCases)}_08"),

            new TestCaseData(
                new TestDelegate(
                        () => new ComponentBag(
                                        nGramsTokenizer: new NGramTokenizer(),
                                        similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                                        roundingFunction: ComponentBag.DefaultRoundingFunction,
                                        textTruncatingFunction: ComponentBag.DefaultTextTruncatingFunction,
                                        loggingAction: ComponentBag.DefaultLoggingAction,
                                        labeledExampleManager: new LabeledExampleManager(),
                                        asciiBannerManager: new AsciiBannerManager(),
                                        loggingActionAsciiBanner:  ComponentBag.DefaultLoggingActionAsciiBanner,
                                        fileManager: null,
                                        serializerFactory: new SerializerFactory(),
                                        filenameFactory: new FilenameFactory(),
                                        nowFunction: ComponentBag.DefaultNowFunction
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("fileManager").Message
                ).SetArgDisplayNames($"{nameof(componentBagExceptionTestCases)}_09"),

            new TestCaseData(
                new TestDelegate(
                        () => new ComponentBag(
                                        nGramsTokenizer: new NGramTokenizer(),
                                        similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                                        roundingFunction: ComponentBag.DefaultRoundingFunction,
                                        textTruncatingFunction: ComponentBag.DefaultTextTruncatingFunction,
                                        loggingAction: ComponentBag.DefaultLoggingAction,
                                        labeledExampleManager: new LabeledExampleManager(),
                                        asciiBannerManager: new AsciiBannerManager(),
                                        loggingActionAsciiBanner:  ComponentBag.DefaultLoggingActionAsciiBanner,
                                        fileManager: new FileManager(),
                                        serializerFactory: null,
                                        filenameFactory: new FilenameFactory(),
                                        nowFunction: ComponentBag.DefaultNowFunction
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("serializerFactory").Message
                ).SetArgDisplayNames($"{nameof(componentBagExceptionTestCases)}_10"),

            new TestCaseData(
                new TestDelegate(
                        () => new ComponentBag(
                                        nGramsTokenizer: new NGramTokenizer(),
                                        similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                                        roundingFunction: ComponentBag.DefaultRoundingFunction,
                                        textTruncatingFunction: ComponentBag.DefaultTextTruncatingFunction,
                                        loggingAction: ComponentBag.DefaultLoggingAction,
                                        labeledExampleManager: new LabeledExampleManager(),
                                        asciiBannerManager: new AsciiBannerManager(),
                                        loggingActionAsciiBanner:  ComponentBag.DefaultLoggingActionAsciiBanner,
                                        fileManager: new FileManager(),
                                        serializerFactory: new SerializerFactory(),
                                        filenameFactory: null,
                                        nowFunction: ComponentBag.DefaultNowFunction
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("filenameFactory").Message
                ).SetArgDisplayNames($"{nameof(componentBagExceptionTestCases)}_11"),

            new TestCaseData(
                new TestDelegate(
                        () => new ComponentBag(
                                        nGramsTokenizer: new NGramTokenizer(),
                                        similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                                        roundingFunction: ComponentBag.DefaultRoundingFunction,
                                        textTruncatingFunction: ComponentBag.DefaultTextTruncatingFunction,
                                        loggingAction: ComponentBag.DefaultLoggingAction,
                                        labeledExampleManager: new LabeledExampleManager(),
                                        asciiBannerManager: new AsciiBannerManager(),
                                        loggingActionAsciiBanner:  ComponentBag.DefaultLoggingActionAsciiBanner,
                                        fileManager: new FileManager(),
                                        serializerFactory: new SerializerFactory(),
                                        filenameFactory: new FilenameFactory(),
                                        nowFunction: null
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("nowFunction").Message
                ).SetArgDisplayNames($"{nameof(componentBagExceptionTestCases)}_12")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(componentBagExceptionTestCases))]
        public void ComponentBag_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void ComponentBag_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            ComponentBag actual = new ComponentBag();

            // Assert
            Assert.IsInstanceOf<ComponentBag>(actual);

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

            Assert.IsInstanceOf<Func<double, double>>(ComponentBag.DefaultRoundingFunction);
            Assert.IsInstanceOf<Func<string, uint, string>>(ComponentBag.DefaultTextTruncatingFunction);
            Assert.IsInstanceOf<string>(ComponentBag.DefaultLoggingActionDateFormat);
            Assert.IsInstanceOf<Action<string>>(ComponentBag.DefaultLoggingAction);
            Assert.IsInstanceOf<Action<string>>(ComponentBag.DefaultLoggingActionAsciiBanner);
            Assert.IsInstanceOf<Func<DateTime>>(ComponentBag.DefaultNowFunction);

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
            string actual = ComponentBag.DefaultTextTruncatingFunction(text, length);

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