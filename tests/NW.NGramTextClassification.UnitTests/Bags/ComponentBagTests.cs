using System;
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

        private static TestCaseData[] componentBagDefaultValueTestCases =
        {

            new TestCaseData(
                new Func<ComponentBag>(
                        () => new ComponentBag(
                                    nGramsTokenizer: new NGramTokenizer(),
                                    similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                                    roundingFunction: null,
                                    textTruncatingFunction: ComponentBag.DefaultTextTruncatingFunction,
                                    loggingAction: ComponentBag.DefaultLoggingAction,
                                    labeledExampleManager: new LabeledExampleManager(),
                                    loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                                    fileManager: new FileManager(),
                                    serializerFactory: new SerializerFactory(),
                                    filenameFactory: new FilenameFactory(),
                                    nowFunction: ComponentBag.DefaultNowFunction,
                                    versionFunction: ComponentBag.DefaultVersionFunction
                                )),
                new Action<ComponentBag>(componentBag => Assert.That(componentBag.RoundingFunction, Is.EqualTo(ComponentBag.DefaultRoundingFunction)))
                ).SetArgDisplayNames($"{nameof(componentBagDefaultValueTestCases)}_01"),

            new TestCaseData(
                new Func<ComponentBag>(
                        () => new ComponentBag(
                                    nGramsTokenizer: new NGramTokenizer(),
                                    similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                                    roundingFunction: ComponentBag.DefaultRoundingFunction,
                                    textTruncatingFunction: null,
                                    loggingAction: ComponentBag.DefaultLoggingAction,
                                    labeledExampleManager: new LabeledExampleManager(),
                                    loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                                    fileManager: new FileManager(),
                                    serializerFactory: new SerializerFactory(),
                                    filenameFactory: new FilenameFactory(),
                                    nowFunction: ComponentBag.DefaultNowFunction,
                                    versionFunction: ComponentBag.DefaultVersionFunction
                                )),
                new Action<ComponentBag>(componentBag => Assert.That(componentBag.TextTruncatingFunction, Is.EqualTo(ComponentBag.DefaultTextTruncatingFunction)))
                ).SetArgDisplayNames($"{nameof(componentBagDefaultValueTestCases)}_02"),

            new TestCaseData(
                new Func<ComponentBag>(
                        () => new ComponentBag(
                                    nGramsTokenizer: new NGramTokenizer(),
                                    similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                                    roundingFunction: ComponentBag.DefaultRoundingFunction,
                                    textTruncatingFunction: ComponentBag.DefaultTextTruncatingFunction,
                                    loggingAction: null,
                                    labeledExampleManager: new LabeledExampleManager(),
                                    loggingActionAsciiBanner: ComponentBag.DefaultLoggingActionAsciiBanner,
                                    fileManager: new FileManager(),
                                    serializerFactory: new SerializerFactory(),
                                    filenameFactory: new FilenameFactory(),
                                    nowFunction: ComponentBag.DefaultNowFunction,
                                    versionFunction: ComponentBag.DefaultVersionFunction
                                )),
                new Action<ComponentBag>(componentBag => Assert.That(componentBag.LoggingAction, Is.EqualTo(ComponentBag.DefaultLoggingAction)))
                ).SetArgDisplayNames($"{nameof(componentBagDefaultValueTestCases)}_03"),

            new TestCaseData(
                new Func<ComponentBag>(
                        () => new ComponentBag(
                                    nGramsTokenizer: new NGramTokenizer(),
                                    similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                                    roundingFunction: ComponentBag.DefaultRoundingFunction,
                                    textTruncatingFunction: ComponentBag.DefaultTextTruncatingFunction,
                                    loggingAction: ComponentBag.DefaultLoggingAction,
                                    labeledExampleManager: new LabeledExampleManager(),
                                    loggingActionAsciiBanner:  null,
                                    fileManager: new FileManager(),
                                    serializerFactory: new SerializerFactory(),
                                    filenameFactory: new FilenameFactory(),
                                    nowFunction: ComponentBag.DefaultNowFunction,
                                    versionFunction: ComponentBag.DefaultVersionFunction
                                )),
                new Action<ComponentBag>(componentBag => Assert.That(componentBag.LoggingActionAsciiBanner, Is.EqualTo(ComponentBag.DefaultLoggingActionAsciiBanner)))
                ).SetArgDisplayNames($"{nameof(componentBagDefaultValueTestCases)}_04"),

            new TestCaseData(
                new Func<ComponentBag>(
                        () => new ComponentBag(
                                        nGramsTokenizer: new NGramTokenizer(),
                                        similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                                        roundingFunction: ComponentBag.DefaultRoundingFunction,
                                        textTruncatingFunction: ComponentBag.DefaultTextTruncatingFunction,
                                        loggingAction: ComponentBag.DefaultLoggingAction,
                                        labeledExampleManager: new LabeledExampleManager(),
                                        loggingActionAsciiBanner:  ComponentBag.DefaultLoggingActionAsciiBanner,
                                        fileManager: new FileManager(),
                                        serializerFactory: new SerializerFactory(),
                                        filenameFactory: new FilenameFactory(),
                                        nowFunction: null,
                                        versionFunction: ComponentBag.DefaultVersionFunction
                                )),
                new Action<ComponentBag>(componentBag => Assert.That(componentBag.NowFunction, Is.EqualTo(ComponentBag.DefaultNowFunction)))
                ).SetArgDisplayNames($"{nameof(componentBagDefaultValueTestCases)}_05"),

            new TestCaseData(
                new Func<ComponentBag>(
                        () => new ComponentBag(
                                        nGramsTokenizer: new NGramTokenizer(),
                                        similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                                        roundingFunction: ComponentBag.DefaultRoundingFunction,
                                        textTruncatingFunction: ComponentBag.DefaultTextTruncatingFunction,
                                        loggingAction: ComponentBag.DefaultLoggingAction,
                                        labeledExampleManager: new LabeledExampleManager(),
                                        loggingActionAsciiBanner:  ComponentBag.DefaultLoggingActionAsciiBanner,
                                        fileManager: new FileManager(),
                                        serializerFactory: new SerializerFactory(),
                                        filenameFactory: new FilenameFactory(),
                                        nowFunction: ComponentBag.DefaultNowFunction,
                                        versionFunction: null
                                )),
                new Action<ComponentBag>(componentBag => Assert.That(componentBag.VersionFunction, Is.EqualTo(ComponentBag.DefaultVersionFunction)))
                ).SetArgDisplayNames($"{nameof(componentBagDefaultValueTestCases)}_06")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(componentBagDefaultValueTestCases))]
        public void ComponentBag_ShouldUseDefaultValue_WhenParameterIsNull(Func<ComponentBag> initFunction, Action<ComponentBag> assertAction)
        {

            // Arrange
            // Act
            ComponentBag componentBag = initFunction();

            // Assert
            assertAction(componentBag);

        }

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
            Assert.That(actual.LoggingActionAsciiBanner, Is.InstanceOf<Action<string>>());
            Assert.That(actual.FileManager, Is.InstanceOf<IFileManager>());
            Assert.That(actual.SerializerFactory, Is.InstanceOf<ISerializerFactory>());
            Assert.That(actual.FilenameFactory, Is.InstanceOf<IFilenameFactory>());
            Assert.That(actual.NowFunction, Is.InstanceOf<Func<DateTime>>());
            Assert.That(actual.VersionFunction, Is.InstanceOf<Func<string>>());

            Assert.That(ComponentBag.DefaultRoundingFunction, Is.InstanceOf<Func<double, double>>());
            Assert.That(ComponentBag.DefaultTextTruncatingFunction, Is.InstanceOf<Func<string, uint, string>>());
            Assert.That(ComponentBag.DefaultLoggingActionDateFormat, Is.InstanceOf<string>());
            Assert.That(ComponentBag.DefaultLoggingAction, Is.InstanceOf<Action<string>>());
            Assert.That(ComponentBag.DefaultLoggingActionAsciiBanner, Is.InstanceOf<Action<string>>());
            Assert.That(ComponentBag.DefaultNowFunction,Is.InstanceOf<Func<DateTime>>());
            Assert.That(ComponentBag.DefaultVersionFunction,Is.InstanceOf<Func<string>>());

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