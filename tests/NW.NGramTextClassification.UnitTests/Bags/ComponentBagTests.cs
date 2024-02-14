using System;
using NW.NGramTextClassification.AsciiBanner;
using NW.NGramTextClassification.Bags;
using NW.NGramTextClassification.Filenames;
using NW.NGramTextClassification.LabeledExamples;
using NW.NGramTextClassification.NGramTokenization;
using NW.NGramTextClassification.Similarity;
using NW.Shared.Files;
using NW.Shared.Serialization;
using NUnit.Framework;

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
            Assert.That(actual, Is.InstanceOf<ComponentBag>());

            Assert.That(actual.NGramsTokenizer, Is.InstanceOf<INGramTokenizer>());
            Assert.That(actual.SimilarityIndexCalculator, Is.InstanceOf<ISimilarityIndexCalculator>());
            Assert.That(actual.RoundingFunction, Is.InstanceOf<Func<double, double>>());
            Assert.That(actual.TextTruncatingFunction, Is.InstanceOf<Func<string, uint, string>>());
            Assert.That(actual.LoggingAction, Is.InstanceOf<Action<string>>());
            Assert.That(actual.LabeledExampleManager, Is.InstanceOf<ILabeledExampleManager>());
            Assert.That(actual.AsciiBannerManager, Is.InstanceOf<IAsciiBannerManager>());
            Assert.That(actual.LoggingActionAsciiBanner, Is.InstanceOf<Action<string>>());
            Assert.That(actual.FileManager, Is.InstanceOf<IFileManager>());
            Assert.That(actual.SerializerFactory, Is.InstanceOf<ISerializerFactory>());
            Assert.That(actual.FilenameFactory, Is.InstanceOf<IFilenameFactory>());
            Assert.That(actual.NowFunction, Is.InstanceOf<Func<DateTime>>());

            Assert.That(ComponentBag.DefaultRoundingFunction, Is.InstanceOf<Func<double, double>>());
            Assert.That(ComponentBag.DefaultTextTruncatingFunction, Is.InstanceOf<Func<string, uint, string>>());
            Assert.That(ComponentBag.DefaultLoggingActionDateFormat, Is.InstanceOf<string>());
            Assert.That(ComponentBag.DefaultLoggingAction, Is.InstanceOf<Action<string>>());
            Assert.That(ComponentBag.DefaultLoggingActionAsciiBanner, Is.InstanceOf<Action<string>>());
            Assert.That(ComponentBag.DefaultNowFunction,Is.InstanceOf<Func<DateTime>>());

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
            Assert.That(expected, Is.EqualTo(actual));

        }

        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 14.02.2024
*/