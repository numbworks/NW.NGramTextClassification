﻿using System;
using System.Collections.Generic;
using NW.NGramTextClassification;
using NW.NGramTextClassification.Similarity;
using NW.NGramTextClassification.LabeledExamples;
using NW.NGramTextClassification.AsciiBanner;
using NW.NGramTextClassification.NGramTokenization;
using NW.NGramTextClassification.Serializations;
using NW.NGramTextClassification.Filenames;
using NW.NGramTextClassificationClient.Shared;
using NW.NGramTextClassificationClient.UnitTests.Utilities;
using NUnit.Framework;
using NW.NGramTextClassification.Bags;

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
                                componentBagFactory: null,
                                settingBagFactory: new SettingBagFactory(),
                                textClassifierFactory: new TextClassifierFactory())
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("componentBagFactory").Message
            ).SetArgDisplayNames($"{nameof(libraryBrokerExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                    () => new LibraryBroker(
                                componentBagFactory: new ComponentBagFactory(),
                                settingBagFactory: null,
                                textClassifierFactory: new TextClassifierFactory())
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("settingBagFactory").Message
            ).SetArgDisplayNames($"{nameof(libraryBrokerExceptionTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                    () => new LibraryBroker(
                                componentBagFactory: new ComponentBagFactory(),
                                settingBagFactory: new SettingBagFactory(),
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
            (List<string> messages, List<string> messagesAsciiBanner, ComponentBag fakeComponentBag) = CreateTuple();

            LibraryBroker libraryBroker
                = new LibraryBroker(
                        componentBagFactory: new FakeComponentBagFactory(fakeComponentBag),
                        settingBagFactory: new SettingBagFactory(),
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
            List<(string fileName, string content)> readBehaviours = new List<(string fileName, string content)>()
            {

                (fileName: "LabeledExamples.json", content: NGramTextClassification.UnitTests.LabeledExamples.ObjectMother.ShortLabeledExamplesAsJson_Content),
                (fileName: "TextSnippets.json", content: NGramTextClassification.UnitTests.TextSnippets.ObjectMother.TextSnippetsAsJson_Content),
                (fileName: "TokenizerRuleSet.json", content: NGramTextClassification.UnitTests.TextClassifications.ObjectMother.TokenizerRuleSetAsJson_Content)

            };

            (List<string> messages, List<string> messagesAsciiBanner, ComponentBag fakeComponentBag) = CreateTuple(readBehaviours);

            LibraryBroker libraryBroker
                = new LibraryBroker(
                        componentBagFactory: new FakeComponentBagFactory(fakeComponentBag),
                        settingBagFactory: new SettingBagFactory(),
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
                        saveSession: true,
                        cleanLabeledExamples: true,
                        disableIndexSerialization: false
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

        [Test]
        public void RunSessionClassify_ShouldThrowExceptionAndReturnFailure_WhenLabeledExamplesFailsToLoad()
        {

            // Arrange
            List<(string fileName, string content)> readBehaviours = new List<(string fileName, string content)>()
            {

                (fileName: "LabeledExamples.json", content: @"{ ""SomeField"": ""SomeValue"" }")

            };

            (List<string> messages, List<string> messagesAsciiBanner, ComponentBag fakeComponentBag) = CreateTuple(readBehaviours);

            LibraryBroker libraryBroker
                = new LibraryBroker(
                        componentBagFactory: new FakeComponentBagFactory(fakeComponentBag),
                        settingBagFactory: new SettingBagFactory(),
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
                        saveSession: true,
                        cleanLabeledExamples: true,
                        disableIndexSerialization: false
                    );

            Exception e 
                = new Exception(NGramTextClassificationClient.Shared.MessageCollection.LoadingFileNameReturnedDefault("LabeledExamples.json"));
            string expected = LibraryBroker.ErrorMessageFormatter(e.Message);

            // Act

            int actual = libraryBroker.RunSessionClassify(classifyData);

            // Assert
            Assert.AreEqual(LibraryBroker.Failure, actual);

            Assert.AreEqual(
                    expected: expected,
                    actual: messages[2]
                    );
        }

        [Test]
        public void RunSessionClassify_ShouldThrowExceptionAndReturnFailure_WhenTextSnippetsFailsToLoad()
        {

            // Arrange
            List<(string fileName, string content)> readBehaviours = new List<(string fileName, string content)>()
            {

                (fileName: "LabeledExamples.json", content: NGramTextClassification.UnitTests.LabeledExamples.ObjectMother.ShortLabeledExamplesAsJson_Content),
                (fileName: "TextSnippets.json", content: @"{ ""SomeField"": ""SomeValue"" }")

            };

            (List<string> messages, List<string> messagesAsciiBanner, ComponentBag fakeComponentBag) = CreateTuple(readBehaviours);

            LibraryBroker libraryBroker
                = new LibraryBroker(
                        componentBagFactory: new FakeComponentBagFactory(fakeComponentBag),
                        settingBagFactory: new SettingBagFactory(),
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
                        saveSession: true,
                        cleanLabeledExamples: true,
                        disableIndexSerialization: false
                    );

            Exception e
                = new Exception(NGramTextClassificationClient.Shared.MessageCollection.LoadingFileNameReturnedDefault("TextSnippets.json"));
            string expected = LibraryBroker.ErrorMessageFormatter(e.Message);

            // Act

            int actual = libraryBroker.RunSessionClassify(classifyData);

            // Assert
            Assert.AreEqual(LibraryBroker.Failure, actual);

            Assert.AreEqual(
                    expected: expected,
                    actual: messages[4]
                    );
        }

        [Test]
        public void RunSessionClassify_ShouldThrowExceptionAndReturnFailure_WhenTokenizerRuleSetFailsToLoad()
        {

            // Arrange
            List<(string fileName, string content)> readBehaviours = new List<(string fileName, string content)>()
            {

                (fileName: "LabeledExamples.json", content: NGramTextClassification.UnitTests.LabeledExamples.ObjectMother.ShortLabeledExamplesAsJson_Content),
                (fileName: "TextSnippets.json", content: NGramTextClassification.UnitTests.TextSnippets.ObjectMother.TextSnippetsAsJson_Content),
                (fileName: "TokenizerRuleSet.json", content: @"{ ""SomeField"": ""SomeValue"" }")

            };

            (List<string> messages, List<string> messagesAsciiBanner, ComponentBag fakeComponentBag) = CreateTuple(readBehaviours);

            LibraryBroker libraryBroker
                = new LibraryBroker(
                        componentBagFactory: new FakeComponentBagFactory(fakeComponentBag),
                        settingBagFactory: new SettingBagFactory(),
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
                        saveSession: true,
                        cleanLabeledExamples: true,
                        disableIndexSerialization: false
                    );

            Exception e
                = new Exception(NGramTextClassificationClient.Shared.MessageCollection.LoadingFileNameReturnedDefault("TokenizerRuleSet.json"));
            string expected = LibraryBroker.ErrorMessageFormatter(e.Message);

            // Act

            
            int actual = libraryBroker.RunSessionClassify(classifyData);

            // Assert
            Assert.AreEqual(LibraryBroker.Failure, actual);

            Assert.AreEqual(
                    expected: expected,
                    actual: messages[6]
                    );

        }

        #endregion

        #region TearDown
        #endregion

        #region Support_methods

        private (List<string>, List<string>, ComponentBag) CreateTuple
            (List<(string fileName, string content)> readBehaviours = null)
        {

            List<string> messages = new List<string>();
            Action<string> fakeLoggingAction = (message) => messages.Add(message);

            List<string> messagesAsciiBanner = new List<string>();
            Action<string> fakeLoggingActionAsciiBanner = (message) => messagesAsciiBanner.Add(message);

            ComponentBag componentBag = new ComponentBag(

                          nGramsTokenizer: new NGramTokenizer(),
                          similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                          roundingFunction: ComponentBag.DefaultRoundingFunction,
                          textTruncatingFunction: ComponentBag.DefaultTextTruncatingFunction,
                          loggingAction: fakeLoggingAction,
                          labeledExampleManager: new LabeledExampleManager(),
                          asciiBannerManager: new AsciiBannerManager(),
                          loggingActionAsciiBanner: fakeLoggingActionAsciiBanner,
                          fileManager: new FakeFileManagerWithDynamicRead(readBehaviours), // When we pass null, it means the test won't use it.
                          serializerFactory: new SerializerFactory(),
                          filenameFactory: new FilenameFactory(),
                          nowFunction: ComponentBag.DefaultNowFunction);

            return (messages, messagesAsciiBanner, componentBag);

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 26.01.2024
*/