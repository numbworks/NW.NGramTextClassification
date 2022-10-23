using System;
using NW.NGramTextClassificationClient.Shared;
using NW.NGramTextClassification.NGramTokenization;
using NW.NGramTextClassification.UnitTests.Utilities;
using NUnit.Framework;
using System.Collections.Generic;
using NW.NGramTextClassification;
using NW.NGramTextClassification.Similarity;
using NW.NGramTextClassification.LabeledExamples;
using NW.NGramTextClassification.AsciiBanner;
using NW.NGramTextClassification.Files;
using NW.NGramTextClassification.Serializations;
using NW.NGramTextClassification.Filenames;
using NW.NGramTextClassificationClient.UnitTests.Utilities;

namespace NW.NGramTextClassificationClient.UnitTests
{
    [TestFixture]
    public class LibraryBrokerTests
    {

        #region Fields

        private static TestCaseData[] libraryBrokerExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new LibraryBroker(
                                componentsFactory: null,
                                settingsFactory: new TextClassifierSettingsFactory(),
                                textClassifierFactory: new TextClassifierFactory())
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("componentsFactory").Message
            ).SetArgDisplayNames($"{nameof(libraryBrokerExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                    () => new LibraryBroker(
                                componentsFactory: new TextClassifierComponentsFactory(),
                                settingsFactory: null,
                                textClassifierFactory: new TextClassifierFactory())
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("settingsFactory").Message
            ).SetArgDisplayNames($"{nameof(libraryBrokerExceptionTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                    () => new LibraryBroker(
                                componentsFactory: new TextClassifierComponentsFactory(),
                                settingsFactory: new TextClassifierSettingsFactory(),
                                textClassifierFactory: null)
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("textClassifierFactory").Message
            ).SetArgDisplayNames($"{nameof(libraryBrokerExceptionTestCases)}_03")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(libraryBrokerExceptionTestCases))]
        public void LibraryBroker_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void LibraryBroker_ShouldCreateAnObjectOfTypeLibraryBroker_WhenInvoked()
        {

            // Arrange
            // Act
            LibraryBroker actual = new LibraryBroker();

            // Assert
            Assert.IsInstanceOf<LibraryBroker>(actual);
            Assert.IsInstanceOf<int>(LibraryBroker.Success);
            Assert.IsInstanceOf<int>(LibraryBroker.Failure);
            Assert.IsInstanceOf<string>(LibraryBroker.SeparatorLine);
            Assert.IsInstanceOf<Func<string, string>>(LibraryBroker.ErrorMessageFormatter);
            Assert.IsInstanceOf<NGramTokenizerRuleSet>(LibraryBroker.DefaultTokenizerRuleSet);

        }

        [Test]
        public void RunSessionClassify_ShouldReturnFailureAndLogException_WhenClassifyDataIsNull()
        {

            // Arrange
            (List<string> messages, List<string> messagesAsciiBanner, TextClassifierComponents fakeComponents) = CreateTuple();

            LibraryBroker libraryBroker
                = new LibraryBroker(
                        componentsFactory: new FakeTextClassifierComponentsFactory(fakeComponents),
                        settingsFactory: new TextClassifierSettingsFactory(),
                        textClassifierFactory: new TextClassifierFactory()
                    );

            // Act
            int actual = libraryBroker.RunSessionClassify(null);

            // Assert
            Assert.AreEqual(LibraryBroker.Failure, actual);
            Assert.AreEqual(
                    expected: LibraryBroker.ErrorMessageFormatter(new ArgumentNullException("classifyData").Message),
                    actual: messages[0]
                    );
            Assert.AreEqual(
                    expected: LibraryBroker.SeparatorLine,
                    actual: messagesAsciiBanner[0]
                    );

        }

        #endregion

        #region TearDown
        #endregion

        #region Support_methods

        private (List<string>, List<string>, TextClassifierComponents) CreateTuple()
        {

            List<string> messages = new List<string>();
            Action<string> fakeLoggingAction = (message) => messages.Add(message);

            List<string> messagesAsciiBanner = new List<string>();
            Action<string> fakeLoggingActionAsciiBanner = (message) => messagesAsciiBanner.Add(message);

            TextClassifierComponents components = new TextClassifierComponents(

                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: TextClassifierComponents.DefaultRoundingFunction,
                          textTruncatingFunction: TextClassifierComponents.DefaultTextTruncatingFunction,
                          loggingAction: fakeLoggingAction,
                          labeledExampleManager: new LabeledExampleManager(),
                          asciiBannerManager: new AsciiBannerManager(),
                          loggingActionAsciiBanner: fakeLoggingActionAsciiBanner,
                          fileManager: new FileManager(),
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: TextClassifierComponents.DefaultNowFunction);

            return (messages, messagesAsciiBanner, components);

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 23.10.2022
*/