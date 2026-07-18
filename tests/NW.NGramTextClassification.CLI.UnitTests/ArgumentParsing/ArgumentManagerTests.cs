using System;
using McMaster.Extensions.CommandLineUtils;
using NW.NGramTextClassification.CLI.ArgumentParsing;
using NW.NGramTextClassification.CLI.ApplicationSession;
using NW.NGramTextClassification.CLI.AsciiBanners;
using NW.NGramTextClassification.CLI.TerminalWindows;
using NW.NGramTextClassification.CLI.Shared;
using NUnit.Framework;
using NW.NGramTextClassification.Bags;
using System.Collections.Generic;
using NW.NGramTextClassification.NGramTokenization;
using NW.NGramTextClassification.Similarity;
using NW.NGramTextClassification.LabeledExamples;
using NW.NGramTextClassification.CLI.UnitTests.Utilities;
using NW.Shared.Serialization;
using NW.NGramTextClassification.Filenames;
using McMaster.Extensions.CommandLineUtils.Validation;
using System.Linq;

namespace NW.NGramTextClassification.CLI.UnitTests.ArgumentParsing
{
    [TestFixture]
    public class ArgumentManagerTests
    {

        #region Fields

        private static TestCaseData[] createExecuteTestCases =
        {

            new TestCaseData(
                new string[] { },
                ArgumentManager.Success
            ).SetArgDisplayNames($"{nameof(createExecuteTestCases)}_01"),

            new TestCaseData(
                new string[] { CommandLineString.COMMAND_ABOUT_NAME },
                ArgumentManager.Success
            ).SetArgDisplayNames($"{nameof(createExecuteTestCases)}_02"),

            new TestCaseData(
                new string[] { CommandLineString.COMMAND_SESSION_NAME },
                ArgumentManager.Success
            ).SetArgDisplayNames($"{nameof(createExecuteTestCases)}_03")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [Test]
        public void ArgumentManager_ShouldCreateAnObjectOfTypeArgumentManager_WhenInvoked()
        {

            // Arrange
            FakeComponentBagFactory fakeComponentBagFactory = CreateFakeComponentBagFactory();

            // Act
            ArgumentManager actual = new ArgumentManager(componentBagFactory: fakeComponentBagFactory);

            // Assert
            Assert.That(actual, Is.InstanceOf<ArgumentManager>());
            Assert.That(actual.DoubleManager, Is.InstanceOf<IDoubleManager>());
            Assert.That(actual.MinimumAccuracyValidator, Is.InstanceOf<IOptionValidator>());
            Assert.That(actual.AsciiBannerManager, Is.InstanceOf<IAsciiBannerManager>());
            Assert.That(actual.TerminalWindowManager, Is.InstanceOf<ITerminalWindowManager>());
            Assert.That(actual.TextClassifierFactory, Is.InstanceOf<ITextClassifierFactory>());
            Assert.That(actual.ComponentBagFactory, Is.InstanceOf<IComponentBagFactory>());
            Assert.That(actual.SettingBagFactory, Is.InstanceOf<ISettingBagFactory>());

            Assert.That(ArgumentManager.Success, Is.InstanceOf<int>());
            Assert.That(ArgumentManager.Failure, Is.InstanceOf<int>());
            Assert.That(ArgumentManager.SeparatorLine, Is.InstanceOf<string>());
            Assert.That(ArgumentManager.ErrorMessageFormatter, Is.InstanceOf<Func<string, string>>());
            Assert.That(ArgumentManager.DefaultTokenizerRuleSet, Is.InstanceOf<NGramTokenizerRuleSet>());

        }

        [Test]
        public void ArgumentManager_ShouldUseProvidedDependencies_WhenInvoked()
        {

            // Arrange
            DoubleManager doubleManager = new DoubleManager();
            AsciiBannerManager asciiBannerManager = new AsciiBannerManager();
            TerminalWindowManager terminalWindowManager = new TerminalWindowManager();
            TextClassifierFactory textClassifierFactory = new TextClassifierFactory();
            SettingBagFactory settingBagFactory = new SettingBagFactory();
            FakeComponentBagFactory fakeComponentBagFactory = CreateFakeComponentBagFactory();

            // Act
            ArgumentManager actual
                = new ArgumentManager(
                        doubleManager: doubleManager,
                        asciiBannerManager: asciiBannerManager,
                        terminalWindowManager: terminalWindowManager,
                        textClassifierFactory: textClassifierFactory,
                        componentBagFactory: fakeComponentBagFactory,
                        settingBagFactory: settingBagFactory);

            // Assert
            Assert.That(doubleManager, Is.SameAs(actual.DoubleManager));
            Assert.That(asciiBannerManager, Is.SameAs(actual.AsciiBannerManager));
            Assert.That(terminalWindowManager, Is.SameAs(actual.TerminalWindowManager));
            Assert.That(textClassifierFactory, Is.SameAs(actual.TextClassifierFactory));
            Assert.That(settingBagFactory, Is.SameAs(actual.SettingBagFactory));
            Assert.That(fakeComponentBagFactory, Is.SameAs(actual.ComponentBagFactory));

        }


        [TestCaseSource(nameof(createExecuteTestCases))]
        public void Create_ShouldReturnExpectedExitCode_WhenExecuted(string[] args, int expected)
        {

            // Arrange
            FakeComponentBagFactory fakeComponentBagFactory = CreateFakeComponentBagFactory();
            CommandLineApplication app = new ArgumentManager(componentBagFactory: fakeComponentBagFactory).Create();

            // Act
            int actual = app.Execute(args);

            // Assert
            Assert.That(expected, Is.EqualTo(actual));

        }

        [Test]
        public void Create_ShouldCreateAnObjectOfTypeCommandLineApplication_WhenInvoked()
        {

            // Arrange
            FakeComponentBagFactory fakeComponentBagFactory = CreateFakeComponentBagFactory();
            ArgumentManager argumentManager = new ArgumentManager(componentBagFactory: fakeComponentBagFactory);

            // Act
            CommandLineApplication actual = argumentManager.Create();

            // Assert
            Assert.That(actual, Is.InstanceOf<CommandLineApplication>());

        }

        [Test]
        public void Create_ShouldSetExpectedApplicationInformation_WhenInvoked()
        {

            // Arrange
            FakeComponentBagFactory fakeComponentBagFactory = CreateFakeComponentBagFactory();
            ArgumentManager argumentManager = new ArgumentManager(componentBagFactory: fakeComponentBagFactory);

            // Act
            CommandLineApplication actual = argumentManager.Create();

            // Assert
            Assert.That(CommandLineString.APPLICATION_NAME, Is.EqualTo(actual.Name));
            Assert.That(CommandLineString.APPLICATION_DESCRIPTION, Is.EqualTo(actual.Description));

        }

        [Test]
        public void Create_ShouldAddExpectedCommands_WhenInvoked()
        {

            // Arrange
            FakeComponentBagFactory fakeComponentBagFactory = CreateFakeComponentBagFactory();
            ArgumentManager argumentManager = new ArgumentManager(componentBagFactory: fakeComponentBagFactory);

            // Act
            CommandLineApplication actual = argumentManager.Create();

            // Assert
            Assert.That(
                actual.Commands.Any(
                    command => command.Name == CommandLineString.COMMAND_ABOUT_NAME),
                Is.True);

            Assert.That(
                actual.Commands.Any(
                    command => command.Name == CommandLineString.COMMAND_SESSION_NAME),
                Is.True);

        }

        [Test]
        public void Create_ShouldAddExpectedAboutCommand_WhenInvoked()
        {

            // Arrange
            FakeComponentBagFactory fakeComponentBagFactory = CreateFakeComponentBagFactory();
            ArgumentManager argumentManager = new ArgumentManager(componentBagFactory: fakeComponentBagFactory);

            // Act
            CommandLineApplication actual = argumentManager.Create();

            CommandLineApplication aboutCommand
                = actual.Commands.Single(
                    command => command.Name == CommandLineString.COMMAND_ABOUT_NAME);

            // Assert
            Assert.That(aboutCommand, Is.InstanceOf<CommandLineApplication>());
            Assert.That(CommandLineString.COMMAND_ABOUT_DESCR, Is.EqualTo(aboutCommand.Description));

        }

        [Test]
        public void Create_ShouldAddExpectedSessionCommand_WhenInvoked()
        {

            // Arrange
            FakeComponentBagFactory fakeComponentBagFactory = CreateFakeComponentBagFactory();
            ArgumentManager argumentManager = new ArgumentManager(componentBagFactory: fakeComponentBagFactory);

            // Act
            CommandLineApplication actual = argumentManager.Create();

            CommandLineApplication sessionCommand
                = actual.Commands.Single(
                    command => command.Name == CommandLineString.COMMAND_SESSION_NAME);

            // Assert
            Assert.That(sessionCommand, Is.InstanceOf<CommandLineApplication>());
            Assert.That(CommandLineString.COMMAND_SESSION_DESCR, Is.EqualTo(sessionCommand.Description));

        }

        [Test]
        public void Create_ShouldAddExpectedClassifySubCommand_WhenInvoked()
        {

            // Arrange
            FakeComponentBagFactory fakeComponentBagFactory = CreateFakeComponentBagFactory();
            ArgumentManager argumentManager = new ArgumentManager(componentBagFactory: fakeComponentBagFactory);

            // Act
            CommandLineApplication actual = argumentManager.Create();

            CommandLineApplication sessionCommand
                = actual.Commands.Single(
                    command => command.Name == CommandLineString.COMMAND_SESSION_NAME);

            CommandLineApplication classifyCommand
                = sessionCommand.Commands.Single(
                    command => command.Name == CommandLineString.SUBCOMMAND_CLASSIFY_NAME);

            // Assert
            Assert.That(classifyCommand, Is.InstanceOf<CommandLineApplication>());
            Assert.That(CommandLineString.SUBCOMMAND_CLASSIFY_DESCR, Is.EqualTo(classifyCommand.Description));

        }

        [Test]
        public void Create_ShouldAddExpectedClassifyOptions_WhenInvoked()
        {

            // Arrange
            FakeComponentBagFactory fakeComponentBagFactory = CreateFakeComponentBagFactory();
            ArgumentManager argumentManager = new ArgumentManager(componentBagFactory: fakeComponentBagFactory);

            // Act
            CommandLineApplication actual = argumentManager.Create();

            CommandLineApplication sessionCommand
                = actual.Commands.Single(
                    command => command.Name == CommandLineString.COMMAND_SESSION_NAME);

            CommandLineApplication classifyCommand
                = sessionCommand.Commands.Single(
                    command => command.Name == CommandLineString.SUBCOMMAND_CLASSIFY_NAME);

            // Assert
            Assert.That(
                classifyCommand.Options.Single(
                    option => option.Description == CommandLineString.OPTION_LABELEDEXAMPLES_DESCR),
                Is.InstanceOf<CommandOption>());

            Assert.That(
                classifyCommand.Options.Single(
                    option => option.Description == CommandLineString.OPTION_TEXTSNIPPETS_DESCR),
                Is.InstanceOf<CommandOption>());

            Assert.That(
                classifyCommand.Options.Single(
                    option => option.Description == CommandLineString.OPTION_FOLDERPATH_DESCR),
                Is.InstanceOf<CommandOption>());

            Assert.That(
                classifyCommand.Options.Single(
                    option => option.Description == CommandLineString.OPTION_TOKENIZERRULESET_DESCR),
                Is.InstanceOf<CommandOption>());

            Assert.That(
                classifyCommand.Options.Single(
                    option => option.Description == CommandLineString.OPTION_MINACCURACYSINGLE_DESCR),
                Is.InstanceOf<CommandOption>());

            Assert.That(
                classifyCommand.Options.Single(
                    option => option.Description == CommandLineString.OPTION_MINACCURACYMULTIPLE_DESCR),
                Is.InstanceOf<CommandOption>());

            Assert.That(
                classifyCommand.Options.Single(
                    option => option.Description == CommandLineString.OPTION_SAVESESSION_DESCR),
                Is.InstanceOf<CommandOption>());

            Assert.That(
                classifyCommand.Options.Single(
                    option => option.Description == CommandLineString.OPTION_CLEANLABELEDEXAMPLES_DESCR),
                Is.InstanceOf<CommandOption>());

            Assert.That(
                classifyCommand.Options.Single(
                    option => option.Description == CommandLineString.OPTION_DISABLEINDEXSERIALIZATION_DESCR),
                Is.InstanceOf<CommandOption>());

        }

        [Test]
        public void Create_ShouldLogAsciiBanner_WhenRootCommandExecuted()        {

            // Arrange
            List<string> messagesAsciiBanner = new List<string>();

            ComponentBag fakeComponentBag = CreateFakeComponentBag( messagesAsciiBanner: messagesAsciiBanner);
            FakeComponentBagFactory fakeComponentBagFactory = new FakeComponentBagFactory(fakeComponentBag);
            ArgumentManager argumentManager = new ArgumentManager(componentBagFactory: fakeComponentBagFactory);

            CommandLineApplication app = argumentManager.Create();

            // Act
            int actual = app.Execute(new string[] { });

            // Assert
            Assert.That(ArgumentManager.Success, Is.EqualTo(actual));
            Assert.That(messagesAsciiBanner.Count, Is.EqualTo(3));
            Assert.That(ArgumentManager.SeparatorLine, Is.EqualTo(messagesAsciiBanner[0]));
            Assert.That(messagesAsciiBanner[1], Is.InstanceOf<string>());
            Assert.That(ArgumentManager.SeparatorLine, Is.EqualTo(messagesAsciiBanner[2]));

        }

        [Test]
        public void Create_ShouldLogAboutInformation_WhenAboutCommandExecuted()
        {

            // Arrange
            List<string> messagesAsciiBanner = new List<string>();

            ComponentBag fakeComponentBag = CreateFakeComponentBag( messagesAsciiBanner: messagesAsciiBanner);
            FakeComponentBagFactory fakeComponentBagFactory = new FakeComponentBagFactory(fakeComponentBag);
            ArgumentManager argumentManager = new ArgumentManager(componentBagFactory: fakeComponentBagFactory);

            CommandLineApplication app = argumentManager.Create();

            // Act
            int actual = app.Execute(new string[] { CommandLineString.COMMAND_ABOUT_NAME });

            // Assert
            Assert.That(ArgumentManager.Success, Is.EqualTo(actual));
            Assert.That(ArgumentManager.SeparatorLine, Is.EqualTo(messagesAsciiBanner[0]));
            Assert.That(CommandLineString.APPLICATION_DESCRIPTION, Is.EqualTo(messagesAsciiBanner[3]));
            Assert.That(ArgumentManager.SeparatorLine, Is.EqualTo(messagesAsciiBanner[4]));
            Assert.That(CommandLineString.COMMAND_ABOUT_INFO_AUTHOR, Is.EqualTo(messagesAsciiBanner[5]));
            Assert.That(CommandLineString.COMMAND_ABOUT_INFO_EMAIL, Is.EqualTo(messagesAsciiBanner[6]));
            Assert.That(CommandLineString.COMMAND_ABOUT_INFO_URL, Is.EqualTo(messagesAsciiBanner[7]));
            Assert.That(CommandLineString.COMMAND_ABOUT_INFO_LICENSE, Is.EqualTo(messagesAsciiBanner[8]));
            Assert.That(ArgumentManager.SeparatorLine, Is.EqualTo(messagesAsciiBanner[9]));

        }

        [Ignore("")]
        [Test]
        public void Create_ShouldLogAsciiBanner_WhenSessionCommandExecuted()
        {

            // Arrange
            List<string> messagesAsciiBanner = new List<string>();

            ComponentBag fakeComponentBag = CreateFakeComponentBag( messagesAsciiBanner: messagesAsciiBanner);
            FakeComponentBagFactory fakeComponentBagFactory = new FakeComponentBagFactory(fakeComponentBag);
            ArgumentManager argumentManager = new ArgumentManager(componentBagFactory: fakeComponentBagFactory);

            CommandLineApplication app = argumentManager.Create();

            // Act
            int actual = app.Execute(new string[] { CommandLineString.COMMAND_ABOUT_NAME });

            // Assert
            Assert.That(ArgumentManager.Success, Is.EqualTo(actual));
            Assert.That(messagesAsciiBanner.Count, Is.EqualTo(3));
            Assert.That(ArgumentManager.SeparatorLine, Is.EqualTo(messagesAsciiBanner[0]));
            Assert.That(messagesAsciiBanner[1], Is.InstanceOf<string>());
            Assert.That(ArgumentManager.SeparatorLine, Is.EqualTo(messagesAsciiBanner[2]));

        }


        [Test]
        public void LogAsciiBanner_ShouldLogExpectedMessages_WhenInvoked()
        {

            // Arrange
            List<string> messagesAsciiBanner = new List<string>();
            ArgumentManager argumentManager = CreateFakeArgumentManager(messagesAsciiBanner: messagesAsciiBanner);

            // Act
            ObjectMother.CallPrivateMethod<ArgumentManager, object>(argumentManager, "LogAsciiBanner", new object[] { });

            // Assert
            Assert.That(messagesAsciiBanner.Count, Is.EqualTo(3));
            Assert.That(ArgumentManager.SeparatorLine, Is.EqualTo(messagesAsciiBanner[0]));
            Assert.That(messagesAsciiBanner[1], Is.InstanceOf<string>());
            Assert.That(ArgumentManager.SeparatorLine, Is.EqualTo(messagesAsciiBanner[2]));

        }

        [Test]
        public void LogFooter_ShouldLogExpectedMessage_WhenInvoked()
        {

            // Arrange
            List<string> messagesAsciiBanner = new List<string>();
            ArgumentManager argumentManager = CreateFakeArgumentManager(messagesAsciiBanner: messagesAsciiBanner);

            // Act
            ObjectMother.CallPrivateMethod<ArgumentManager, object>(argumentManager, "LogFooter", new object[] { });

            // Assert
            Assert.That(messagesAsciiBanner.Count, Is.EqualTo(1));
            Assert.That(ArgumentManager.SeparatorLine, Is.EqualTo(messagesAsciiBanner[0]));

        }

        [Test]
        public void LogAbout_ShouldLogExpectedMessages_WhenInvoked()
        {

            // Arrange
            List<string> messagesAsciiBanner = new List<string>();
            ArgumentManager argumentManager = CreateFakeArgumentManager(messagesAsciiBanner: messagesAsciiBanner);

            // Act
            ObjectMother.CallPrivateMethod<ArgumentManager, object>(argumentManager, "LogAbout", new object[] { });

            // Assert
            Assert.That(messagesAsciiBanner.Count, Is.EqualTo(6));
            Assert.That(CommandLineString.APPLICATION_DESCRIPTION, Is.EqualTo(messagesAsciiBanner[0]));
            Assert.That(ArgumentManager.SeparatorLine, Is.EqualTo(messagesAsciiBanner[1]));
            Assert.That(CommandLineString.COMMAND_ABOUT_INFO_AUTHOR, Is.EqualTo(messagesAsciiBanner[2]));
            Assert.That(CommandLineString.COMMAND_ABOUT_INFO_EMAIL, Is.EqualTo(messagesAsciiBanner[3]));
            Assert.That(CommandLineString.COMMAND_ABOUT_INFO_URL, Is.EqualTo(messagesAsciiBanner[4]));
            Assert.That(CommandLineString.COMMAND_ABOUT_INFO_LICENSE, Is.EqualTo(messagesAsciiBanner[5]));

        }

        [Test]
        public void LogAndReturnFailure_ShouldReturnFailureAndLogException_WhenInvoked()
        {

            // Arrange
            List<string> messages = new List<string>();
            List<string> messagesAsciiBanner = new List<string>();

            ArgumentManager argumentManager = CreateFakeArgumentManager(messages: messages,messagesAsciiBanner: messagesAsciiBanner);

            Exception exception = new Exception("Some error");

            // Act
            int actual = ObjectMother.CallPrivateMethod<ArgumentManager, int>(argumentManager, "LogAndReturnFailure", new object[] { exception });

            // Assert
            Assert.That(ArgumentManager.Failure, Is.EqualTo(actual));
            Assert.That(ArgumentManager.ErrorMessageFormatter(exception.Message), Is.EqualTo(messages[0]));
            Assert.That(ArgumentManager.SeparatorLine, Is.EqualTo(messagesAsciiBanner[0]));

        }

        [Test]
        public void LogAndReturnFailure_ShouldLogInnerException_WhenExceptionHasInnerException()
        {

            // Arrange
            List<string> messages = new List<string>();
            List<string> messagesAsciiBanner = new List<string>();

            ArgumentManager argumentManager = CreateFakeArgumentManager(messages: messages, messagesAsciiBanner: messagesAsciiBanner);

            Exception innerException = new Exception("Inner error");
            Exception exception = new Exception("Outer error", innerException);

            // Act
            int actual = ObjectMother.CallPrivateMethod<ArgumentManager, int>(argumentManager, "LogAndReturnFailure", new object[] { exception });

            // Assert
            Assert.That(ArgumentManager.Failure, Is.EqualTo(actual));
            Assert.That(ArgumentManager.ErrorMessageFormatter(exception.Message), Is.EqualTo(messages[0]));
            Assert.That(ArgumentManager.ErrorMessageFormatter(innerException.Message), Is.EqualTo(messages[1]));
            Assert.That(ArgumentManager.SeparatorLine, Is.EqualTo(messagesAsciiBanner[0]));

        }

        [Test]
        public void Defaultize_ShouldReturnExpectedClassifyData_WhenValuesAreNotNull()
        {

            // Arrange
            ArgumentManager argumentManager = CreateFakeArgumentManager();

            ClassifyData expected
                = new ClassifyData(
                        labeledExamples: "LabeledExamples.json",
                        textSnippets: "TextSnippets.json",
                        folderPath: @"C:\nwngram\",
                        tokenizerRuleSet: "TokenizerRuleSet.json",
                        minAccuracySingle: 0.4,
                        minAccuracyMultiple: 0.7,
                        saveSession: true,
                        cleanLabeledExamples: true,
                        disableIndexSerialization: true);

            // Act
            ClassifyData actual
                = ObjectMother.CallPrivateMethod<ArgumentManager, ClassifyData>(
                    argumentManager,
                    "Defaultize",
                    new object[] { expected });

            // Assert
            Assert.That(expected.LabeledExamples, Is.EqualTo(actual.LabeledExamples));
            Assert.That(expected.TextSnippets, Is.EqualTo(actual.TextSnippets));
            Assert.That(expected.FolderPath, Is.EqualTo(actual.FolderPath));
            Assert.That(expected.TokenizerRuleSet, Is.EqualTo(actual.TokenizerRuleSet));
            Assert.That(expected.MinAccuracySingle, Is.EqualTo(actual.MinAccuracySingle));
            Assert.That(expected.MinAccuracyMultiple, Is.EqualTo(actual.MinAccuracyMultiple));
            Assert.That(expected.SaveSession, Is.EqualTo(actual.SaveSession));
            Assert.That(expected.CleanLabeledExamples, Is.EqualTo(actual.CleanLabeledExamples));
            Assert.That(expected.DisableIndexSerialization, Is.EqualTo(actual.DisableIndexSerialization));

        }

        [Test]
        public void Defaultize_ShouldReturnExpectedDefaultValues_WhenValuesAreNull()
        {

            // Arrange
            ArgumentManager argumentManager = CreateFakeArgumentManager();

            ClassifyData classifyData
                = new ClassifyData(
                        labeledExamples: "LabeledExamples.json",
                        textSnippets: "TextSnippets.json",
                        folderPath: null,
                        tokenizerRuleSet: null,
                        minAccuracySingle: null,
                        minAccuracyMultiple: null,
                        saveSession: false,
                        cleanLabeledExamples: false,
                        disableIndexSerialization: false);

            // Act
            ClassifyData actual
                = ObjectMother.CallPrivateMethod<ArgumentManager, ClassifyData>(
                    argumentManager,
                    "Defaultize",
                    new object[] { classifyData });

            // Assert
            Assert.That(SettingBag.DefaultFolderPath, Is.EqualTo(actual.FolderPath));
            Assert.That(SettingBag.DefaultMinimumAccuracySingleLabel, Is.EqualTo(actual.MinAccuracySingle));
            Assert.That(SettingBag.DefaultMinimumAccuracyMultipleLabels, Is.EqualTo(actual.MinAccuracyMultiple));

        }

        [Test]
        public void RunAboutMain_ShouldReturnSuccessAndLogExpectedMessages_WhenInvoked()
        {

            // Arrange
            List<string> messagesAsciiBanner = new List<string>();
            ArgumentManager argumentManager = CreateFakeArgumentManager(messagesAsciiBanner: messagesAsciiBanner);

            // Act
            int actual
                = ObjectMother.CallPrivateMethod<ArgumentManager, int>(
                    argumentManager,
                    "RunAboutMain",
                    new object[] { });

            // Assert
            Assert.That(ArgumentManager.Success, Is.EqualTo(actual));
            Assert.That(CommandLineString.APPLICATION_DESCRIPTION, Is.EqualTo(messagesAsciiBanner[3]));
            Assert.That(CommandLineString.COMMAND_ABOUT_INFO_AUTHOR, Is.EqualTo(messagesAsciiBanner[5]));
            Assert.That(CommandLineString.COMMAND_ABOUT_INFO_EMAIL, Is.EqualTo(messagesAsciiBanner[6]));
            Assert.That(CommandLineString.COMMAND_ABOUT_INFO_URL, Is.EqualTo(messagesAsciiBanner[7]));
            Assert.That(CommandLineString.COMMAND_ABOUT_INFO_LICENSE, Is.EqualTo(messagesAsciiBanner[8]));
            Assert.That(ArgumentManager.SeparatorLine, Is.EqualTo(messagesAsciiBanner[9]));

        }

        [Test]
        public void RunSessionClassify_ShouldReturnFailureAndLogException_WhenClassifyDataIsNull()
        {

            // Arrange
            List<string> messages = new List<string>();
            List<string> messagesAsciiBanner = new List<string>();

            ArgumentManager argumentManager
                = CreateFakeArgumentManager(
                    messages: messages,
                    messagesAsciiBanner: messagesAsciiBanner);

            // Act
            int actual
                = ObjectMother.CallPrivateMethod<ArgumentManager, int>(
                    argumentManager,
                    "RunSessionClassify",
                    new object[] { null });

            // Assert
            Assert.That(ArgumentManager.Failure, Is.EqualTo(actual));
            Assert.That(ArgumentManager.ErrorMessageFormatter(new NullReferenceException().Message), Is.EqualTo(messages[0]));
            Assert.That(ArgumentManager.SeparatorLine, Is.EqualTo(messagesAsciiBanner[messagesAsciiBanner.Count - 1]));

        }

        #endregion

        #region TearDown
        #endregion

        #region Support_methods

        private ComponentBag CreateFakeComponentBag(List<string> messages = null, List<string> messagesAsciiBanner = null)
        {

            messages = messages ?? new List<string>();
            messagesAsciiBanner = messagesAsciiBanner ?? new List<string>();

            Action<string> fakeLoggingAction
                = (message) => messages.Add(message);

            Action<string> fakeLoggingActionAsciiBanner
                = (message) => messagesAsciiBanner.Add(message);

            ComponentBag componentBag
                = new ComponentBag(
                        nGramsTokenizer: new NGramTokenizer(),
                        similarityIndexCalculator: new SimilarityIndexCalculatorJaccard(),
                        roundingFunction: ComponentBag.DefaultRoundingFunction,
                        textTruncatingFunction: ComponentBag.DefaultTextTruncatingFunction,
                        loggingAction: fakeLoggingAction,
                        labeledExampleManager: new LabeledExampleManager(),
                        loggingActionAsciiBanner: fakeLoggingActionAsciiBanner,
                        fileManager: new FakeFileManagerWithDynamicRead(new List<(string fileName, string content)>()),
                        serializerFactory: new SerializerFactory(),
                        filenameFactory: new FilenameFactory(),
                        nowFunction: ComponentBag.DefaultNowFunction,
                        versionFunction: ComponentBag.DefaultVersionFunction);

            return componentBag;

        }
        private FakeComponentBagFactory CreateFakeComponentBagFactory()
        {
            
            ComponentBag fakeComponentBag = CreateFakeComponentBag();
            FakeComponentBagFactory fakeComponentBagFactory = new FakeComponentBagFactory(fakeComponentBag);

            return fakeComponentBagFactory;

        }
        private ArgumentManager CreateFakeArgumentManager(List<string> messages = null, List<string> messagesAsciiBanner = null)
        {

            ComponentBag fakeComponentBag = CreateFakeComponentBag(messages, messagesAsciiBanner);
            ArgumentManager argumentManager = new ArgumentManager(componentBagFactory: new FakeComponentBagFactory(fakeComponentBag));

            return argumentManager;

        }

        #endregion


    }
}