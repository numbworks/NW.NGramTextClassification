using System;
using System.Collections.Generic;
using System.IO;
using NW.NGramTextClassification.Bags;
using NW.NGramTextClassification.LabeledExamples;
using NW.NGramTextClassification.NGramTokenization;
using NW.NGramTextClassification.TextClassifications;
using NW.NGramTextClassification.TextClassifiers;
using NW.NGramTextClassification.UnitTests.Utilities;
using NW.Shared.Files;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests
{
    [TestFixture]
    public class TextClassificationManagerTests
    {

        #region Fields

        private static TestCaseData[] classifyExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassificationManager().Classify(classifyInput: null)
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("classifyInput").Message
                ).SetArgDisplayNames($"{nameof(classifyExceptionTestCases)}_01")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests (TextClassificationManager)

        [Test]
        public void TextClassificationManager_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            TextClassificationManager actual = new TextClassificationManager();

            // Assert
            Assert.That(actual, Is.InstanceOf<TextClassificationManager>());
            Assert.That(TextClassificationManager.DefaultTokenizerRuleSet, Is.InstanceOf<INGramTokenizerRuleSet>());

        }

        [Test]
        public void TextClassificationManager_ShouldCreateAnInstanceOfThisType_WhenCustomArgumentsProvided()
        {

            // Arrange
            ComponentBag componentBag = new ComponentBag();
            SettingBag settingBag = new SettingBag(folderPath: @"C:\");

            // Act
            TextClassificationManager actual = new TextClassificationManager(componentBag, settingBag);

            // Assert
            Assert.That(actual, Is.InstanceOf<TextClassificationManager>());

        }

        #endregion

        #region Tests (Classify)

        [TestCaseSource(nameof(classifyExceptionTestCases))]
        public void Classify_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void Classify_ShouldLogExpectedMessages_WhenInvoked()
        {

            // Arrange
            List<string> logMessages = new List<string>();
            Action<string> loggingAction = (msg) => logMessages.Add(msg);

            string jsonContent = LabeledExamples.ObjectMother.LabeledExamplesAsJson_Content;
            FakeFileManager fileManager = new FakeFileManager(jsonContent);
            TextClassifier textClassifier = new TextClassifier(fileManager: fileManager);

            ComponentBag componentBag = new ComponentBag(
                fileManager: fileManager,
                textClassifier: textClassifier,
                loggingAction: loggingAction
            );

            SettingBag settingBag = new SettingBag(
                folderPath: @"C:\",
                minimumAccuracySingleLabel: SettingBag.DefaultMinimumAccuracySingleLabel,
                minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels,
                saveOutput: false,
                cleanLabeledExamples: false,
                disableIndexSerialization: false,
                enableUTC: false
            );

            ClassifyInput classifyInput = new ClassifyInput(
                labeledExamplesFileName: "LabeledExamples.json",
                textSnippetsFileName: "TextSnippets.json",
                tokenizerRuleSetFileName: null
            );

            TextClassificationManager textClassificationManager = new TextClassificationManager(componentBag, settingBag);

            // Act
            ClassifyOutput actual = textClassificationManager.Classify(classifyInput);

            // Assert
            Assert.That(logMessages[0], Is.EqualTo(NGramTextClassification.TextClassifications.MessageCollection.TextClassificationProcessStarted));
            Assert.That(logMessages[1], Is.EqualTo(NGramTextClassification.TextClassifications.MessageCollection.TheProvidedIs(nameof(SettingBag.FolderPath), settingBag.FolderPath)));
            Assert.That(logMessages[2], Is.EqualTo(NGramTextClassification.TextClassifications.MessageCollection.TheProvidedIs(nameof(SettingBag.MinimumAccuracySingleLabel), settingBag.MinimumAccuracySingleLabel.ToString())));
            Assert.That(logMessages[3], Is.EqualTo(NGramTextClassification.TextClassifications.MessageCollection.TheProvidedIs(nameof(SettingBag.MinimumAccuracyMultipleLabels), settingBag.MinimumAccuracyMultipleLabels.ToString())));
            Assert.That(logMessages[4], Is.EqualTo(NGramTextClassification.TextClassifications.MessageCollection.TheProvidedIs(nameof(SettingBag.SaveOutput), settingBag.SaveOutput.ToString())));
            Assert.That(logMessages[5], Is.EqualTo(NGramTextClassification.TextClassifications.MessageCollection.TheProvidedIs(nameof(SettingBag.CleanLabeledExamples), settingBag.CleanLabeledExamples.ToString())));
            Assert.That(logMessages[6], Is.EqualTo(NGramTextClassification.TextClassifications.MessageCollection.TheProvidedIs(nameof(SettingBag.DisableIndexSerialization), settingBag.DisableIndexSerialization.ToString())));
            Assert.That(logMessages[7], Is.EqualTo(NGramTextClassification.TextClassifications.MessageCollection.TheProvidedIs(nameof(SettingBag.EnableUTC), settingBag.EnableUTC.ToString())));
            Assert.That(logMessages[8], Is.EqualTo(NGramTextClassification.TextClassifications.MessageCollection.TheProvidedIs(nameof(ClassifyInput.LabeledExamplesFileName), classifyInput.LabeledExamplesFileName)));
            Assert.That(logMessages[9], Is.EqualTo(NGramTextClassification.TextClassifications.MessageCollection.TheProvidedIs(nameof(ClassifyInput.TextSnippetsFileName), classifyInput.TextSnippetsFileName)));
            // Assert.That(logMessages[10], Is.EqualTo(NGramTextClassification.TextClassifications.MessageCollection.XLabeledExamplesHaveBeenLoaded(labeledExamples)));
            // Assert.That(logMessages[11], Is.EqualTo(NGramTextClassification.TextClassifications.MessageCollection.XTextSnippetsHaveBeenLoaded(textSnippets)));
            Assert.That(logMessages[12], Is.EqualTo(NGramTextClassification.TextClassifications.MessageCollection.TheProvidedIs(nameof(NGramTokenizerRuleSet), TextClassificationManager.DefaultTokenizerRuleSet.ToString())));

            int resultsStartIndex = 13;
            Assert.That(logMessages[resultsStartIndex], Is.EqualTo(NGramTextClassification.TextClassifications.MessageCollection.TextClassificationOutputSuccessfullyCreated));
            Assert.That(logMessages[resultsStartIndex + 1], Is.EqualTo(NGramTextClassification.TextClassifications.MessageCollection.TextClassificationOutputContainsResults));
            Assert.That(logMessages[logMessages.Count - 1], Is.EqualTo(NGramTextClassification.TextClassifications.MessageCollection.TextClassificationProcessCompleted));

        }

        #endregion

        #region Tests (ClassifyOrNull)

        [Test]
        public void ClassifyOrNull_ShouldReturnNull_WhenExceptionIsThrown()
        {

            // Arrange
            TextClassificationManager textClassificationManager = new TextClassificationManager();
            ClassifyInput classifyInput = null;

            // Act
            ClassifyOutput actual = textClassificationManager.ClassifyOrNull(classifyInput);

            // Assert
            Assert.That(actual, Is.Null);

        }

        #endregion

        #region Tests (Scaffold)

        [Test]
        public void Scaffold_ShouldReturnFalse_WhenExceptionIsThrown()
        {

            // Arrange
            string writeExceptionMessage = "A random write-to-disk issue.";
            IFileManager fileManager = new FakeFileManagerThrowingWriteExceptions(content: "", writeExceptionMessage: writeExceptionMessage);
            TextClassifier textClassifier = new TextClassifier(fileManager: fileManager);
            ComponentBag componentBag = new ComponentBag(fileManager: fileManager, textClassifier: textClassifier);
            SettingBag settingBag = new SettingBag(folderPath: @"C:\");
            TextClassificationManager textClassificationManager = new TextClassificationManager(componentBag: componentBag, settingBag: settingBag);

            // Act
            textClassificationManager.Scaffold(out bool success);

            // Assert
            Assert.That(success, Is.False);

        }

        [Test]
        public void Scaffold_ShouldLogExpectedMessages_WhenInvoked()
        {

            // Arrange
            List<string> logMessages = new List<string>();
            Action<string> fakeLoggingAction = (msg) => logMessages.Add(msg);

            string folderPath = @"C:\";
            FakeFileManager fileManager = new FakeFileManager("Some content");
            TextClassifier textClassifier = new TextClassifier(fileManager: fileManager);
            ComponentBag componentBag = new ComponentBag(
                fileManager: fileManager,
                textClassifier: textClassifier,
                loggingAction: fakeLoggingAction
            );
            SettingBag settingBag = new SettingBag(folderPath: folderPath);
            TextClassificationManager textClassificationManager = new TextClassificationManager(componentBag: componentBag, settingBag: settingBag);

            IFileInfoAdapter labeledExamplesFile = new FileInfoAdapter(Path.Combine(folderPath, TextClassificationManager.DefaultLabeledExamplesFileName));
            IFileInfoAdapter textSnippetsFile = new FileInfoAdapter(Path.Combine(folderPath, TextClassificationManager.DefaultTextSnippetsFileName));
            IFileInfoAdapter tokenizerRuleSetFile = new FileInfoAdapter(Path.Combine(folderPath, TextClassificationManager.DefaultTokenizerRuleSetFileName));

            // Act
            textClassificationManager.Scaffold();

            // Assert
            Assert.That(logMessages[0], Is.EqualTo(NGramTextClassification.TextClassifications.MessageCollection.ScaffoldingProcessStarted));
            Assert.That(logMessages[1], Is.EqualTo(NGramTextClassification.TextClassifications.MessageCollection.FollowingExampleJSONFileCreated(labeledExamplesFile)));
            Assert.That(logMessages[2], Is.EqualTo(NGramTextClassification.TextClassifications.MessageCollection.FollowingExampleJSONFileCreated(textSnippetsFile)));
            Assert.That(logMessages[3], Is.EqualTo(NGramTextClassification.TextClassifications.MessageCollection.FollowingExampleJSONFileCreated(tokenizerRuleSetFile)));
            Assert.That(logMessages[4], Is.EqualTo(NGramTextClassification.TextClassifications.MessageCollection.ScaffoldingProcessCompleted));

        }

        #endregion

        #region Tests (Private Methods)

        [Test]
        public void CreateFile_ShouldReturnFileInfoAdapter_WhenProperArguments()
        {

            // Arrange
            FakeFileManager fileManager = new FakeFileManager("Some content");
            TextClassifier textClassifier = new TextClassifier(fileManager: fileManager);
            ComponentBag componentBag = new ComponentBag(fileManager: fileManager, textClassifier: textClassifier);
            TextClassificationManager textClassificationManager = new TextClassificationManager(componentBag: componentBag);
            
            string folderPath = @"C:\";
            string fileName = "test.json";

            // Act
            IFileInfoAdapter actual
                = ObjectMother.CallPrivateMethod<TextClassificationManager, IFileInfoAdapter>(
                        obj: textClassificationManager,
                        methodName: "CreateFile",
                        args: [folderPath, fileName]
                    );

            // Assert
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.FullName, Is.EqualTo(Path.Combine(folderPath, fileName)));

        }

        [Test]
        public void LoadLabeledExamplesOrThrow_ShouldReturnCollection_WhenProperFileProvided()
        {

            // Arrange
            FakeFileManager fileManager = new FakeFileManager(LabeledExamples.ObjectMother.LabeledExamplesAsJson_Content);
            TextClassifier textClassifier = new TextClassifier(fileManager: fileManager);
            ComponentBag componentBag = new ComponentBag(fileManager: fileManager, textClassifier: textClassifier);
            TextClassificationManager textClassificationManager = new TextClassificationManager(componentBag: componentBag);
            
            IFileInfoAdapter fileInfoAdapter = new FakeFileInfoAdapter(true, @"C:\LabeledExamples.json");

            // Act
            List<LabeledExample> actual
                = ObjectMother.CallPrivateMethod<TextClassificationManager, List<LabeledExample>>(
                        obj: textClassificationManager,
                        methodName: "LoadLabeledExamplesOrThrow",
                        args: [fileInfoAdapter]
                    );

            // Assert
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Count, Is.GreaterThan(0));

        }

        [Test]
        public void EstablishTokenizerRuleSet_ShouldReturnDefaultTokenizerRuleSet_WhenRuleSetFileNameIsNull()
        {

            // Arrange
            SettingBag settingBag = new SettingBag(folderPath: @"C:\");
            TextClassificationManager textClassificationManager = new TextClassificationManager(settingBag: settingBag);

            ClassifyInput classifyInput = new ClassifyInput(
                labeledExamplesFileName: "LabeledExamples.json",
                textSnippetsFileName: "TextSnippets.json",
                tokenizerRuleSetFileName: null
            );

            // Act
            NGramTokenizerRuleSet actual
                = ObjectMother.CallPrivateMethod<TextClassificationManager, NGramTokenizerRuleSet>(
                        obj: textClassificationManager,
                        methodName: "EstablishTokenizerRuleSet",
                        args: [classifyInput]
                    );

            // Assert
            Assert.That(actual, Is.EqualTo(TextClassificationManager.DefaultTokenizerRuleSet));

        }

        [Test]
        public void LogStartup_ShouldLogExpectedMessages_WhenInvoked()
        {

            // Arrange
            List<string> logMessages = new List<string>();
            Action<string> fakeLoggingAction = (msg) => logMessages.Add(msg);
            ComponentBag componentBag = new ComponentBag(loggingAction: fakeLoggingAction);
            SettingBag settingBag = new SettingBag(folderPath: @"C:\");
            TextClassificationManager textClassificationManager = new TextClassificationManager(componentBag: componentBag, settingBag: settingBag);

            ClassifyInput classifyInput = new ClassifyInput(
                labeledExamplesFileName: "LabeledExamples.json",
                textSnippetsFileName: "TextSnippets.json",
                tokenizerRuleSetFileName: "RuleSet.json"
            );

            // Act
            ObjectMother.CallPrivateMethod<TextClassificationManager, object>(
                    obj: textClassificationManager,
                    methodName: "LogStartup",
                    args: [classifyInput]
                );

            // Assert
            Assert.That(logMessages.Count, Is.GreaterThan(0));
            Assert.That(logMessages[0], Is.EqualTo(NGramTextClassification.TextClassifications.MessageCollection.TextClassificationProcessStarted));

        }

        [Test]
        public void LogException_ShouldLogFormattedException_WhenInvoked()
        {

            // Arrange
            List<string> logMessages = new List<string>();
            Action<string> fakeLoggingAction = (msg) => logMessages.Add(msg);
            Func<string, string> fakeErrorFormatting = (msg) => $"Error: {msg}";
            ComponentBag componentBag = new ComponentBag(
                loggingAction: fakeLoggingAction,
                errorFormattingFunction: fakeErrorFormatting
            );
            TextClassificationManager textClassificationManager = new TextClassificationManager(componentBag: componentBag);

            Exception e = new Exception("Test exception message");

            // Act
            ObjectMother.CallPrivateMethod<TextClassificationManager, object>(
                    obj: textClassificationManager,
                    methodName: "LogException",
                    args: [e]
                );

            // Assert
            Assert.That(logMessages.Count, Is.EqualTo(1));
            Assert.That(logMessages[0], Is.EqualTo($"Error: {e.Message}"));

        }

        [Test]
        public void LogRemovedLabeledExamples_ShouldLogExpectedMessages_WhenInvoked()
        {

            // Arrange
            List<string> logMessages = new List<string>();
            Action<string> fakeLoggingAction = (msg) => logMessages.Add(msg);
            ComponentBag componentBag = new ComponentBag(loggingAction: fakeLoggingAction);
            TextClassificationManager textClassificationManager = new TextClassificationManager(componentBag: componentBag);

            List<LabeledExample> cleanExamples = new List<LabeledExample>()
            {
                new LabeledExample(label: "en", text: "Clean example")
            };
            List<LabeledExample> removedExamples = new List<LabeledExample>()
            {
                new LabeledExample(label: "en", text: "Removed example")
            };

            // Act
            ObjectMother.CallPrivateMethod<TextClassificationManager, object>(
                    obj: textClassificationManager,
                    methodName: "LogRemovedLabeledExamples",
                    args: [cleanExamples, removedExamples]
                );

            // Assert
            Assert.That(logMessages.Count, Is.GreaterThan(0));

        }

        #endregion

        #region TearDown
        #endregion

        #region SupportMethods
        #endregion

    }
}