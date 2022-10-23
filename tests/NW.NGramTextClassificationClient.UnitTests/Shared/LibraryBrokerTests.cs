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

        [Test]
        public void RunSessionClassify_ShouldReturnSuccess_WhenClassifyDataIsNotNull()
        {

            // Arrange
            List<(string fileName, string content)> readBehaviours = new List<(string fileName, string content)>();
            readBehaviours.Add(("LabeledExamples.json", NGramTextClassification.UnitTests.LabeledExamples.ObjectMother.ShortLabeledExamplesAsJson_Content));
            readBehaviours.Add(("TextSnippets.json", NGramTextClassification.UnitTests.TextSnippets.ObjectMother.TextSnippetsAsJson_Content));
            readBehaviours.Add(("TokenizerRuleSet.json", NGramTextClassification.UnitTests.TextClassifications.ObjectMother.TokenizerRuleSetAsJson_Content));

            (List<string> messages, List<string> messagesAsciiBanner, TextClassifierComponents fakeComponents) = CreateTuple(readBehaviours);

            LibraryBroker libraryBroker
                = new LibraryBroker(
                        componentsFactory: new FakeTextClassifierComponentsFactory(fakeComponents),
                        settingsFactory: new TextClassifierSettingsFactory(),
                        textClassifierFactory: new TextClassifierFactory()
                    );

            ClassifyData classifyData
                = new ClassifyData(
                        labeledExamples: "LabeledExamples.json",
                        textSnippets: "TextSnippets.json",
                        folderPath: @"C:\ngramtc\",
                        tokenizerRuleSet: "TokenizerRuleSet.json",
                        minAccuracySingle: 0.4,
                        minAccuracyMultiple: 0.7,
                        saveSession: true
                    );

            // Act
            
            int actual = libraryBroker.RunSessionClassify(classifyData);

            // Assert
            Assert.AreEqual(LibraryBroker.Success, actual);

            Assert.AreEqual(
                    expected: "Attempting to load a collection of 'LabeledExample' objects from: C:\\ngramtc\\LabeledExamples.json.",
                    actual: messages[0]
                    );
            Assert.AreEqual(
                    expected: "Attempting to load a collection of 'TextSnippet' objects from: C:\\ngramtc\\TextSnippets.json.",
                    actual: messages[2]
                    );
            Assert.AreEqual(
                    expected: "Attempting to load a 'NGramTokenizerRuleSet' object from: C:\\ngramtc\\TokenizerRuleSet.json.",
                    actual: messages[4]
                    );

            Assert.AreEqual(
                    expected: LibraryBroker.SeparatorLine,
                    actual: messagesAsciiBanner[0]
                    );
            Assert.AreEqual(
                    expected: new TextClassifier().AsciiBanner,
                    actual: messagesAsciiBanner[1]
                    );
            Assert.AreEqual(
                    expected: LibraryBroker.SeparatorLine,
                    actual: messagesAsciiBanner[2]
                    );

        }

        #endregion

        #region TearDown
        #endregion

        #region Support_methods

        private (List<string>, List<string>, TextClassifierComponents) CreateTuple
            (List<(string fileName, string content)> readBehaviours = null)
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
                          fileManager: new FakeFileManagerWithDynamicRead(readBehaviours), // When we pass null, it means the test won't use it.
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