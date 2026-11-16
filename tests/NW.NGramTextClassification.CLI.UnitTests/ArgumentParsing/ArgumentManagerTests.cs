using System;
using System.Collections.Generic;
using System.Linq;
using NW.NGramTextClassification.CLI.ArgumentParsing;
using NW.NGramTextClassification.CLI.AsciiBanners;
using NW.NGramTextClassification.CLI.TerminalWindows;
using McMaster.Extensions.CommandLineUtils;
using McMaster.Extensions.CommandLineUtils.Validation;
using NUnit.Framework;

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
                new string[] { CommandLineString.COMMAND_CLASSIFY_NAME },
                ArgumentManager.Failure
            ).SetArgDisplayNames($"{nameof(createExecuteTestCases)}_02"),

            new TestCaseData(
                new string[] { CommandLineString.COMMAND_SCAFFOLD_NAME },
                ArgumentManager.Success
            ).SetArgDisplayNames($"{nameof(createExecuteTestCases)}_03")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests (ArgumentManager)

        [Test]
        public void ArgumentManager_ShouldCreateAnObjectOfTypeArgumentManager_WhenInvoked()
        {

            // Arrange
            // Act
            ArgumentManager actual = new ArgumentManager();

            // Assert
            Assert.That(actual, Is.InstanceOf<ArgumentManager>());
            Assert.That(actual.DoubleManager, Is.InstanceOf<IDoubleManager>());
            Assert.That(actual.MinimumAccuracyValidator, Is.InstanceOf<IOptionValidator>());
            Assert.That(actual.AsciiBannerManager, Is.InstanceOf<IAsciiBannerManager>());
            Assert.That(actual.TerminalWindowManager, Is.InstanceOf<ITerminalWindowManager>());
            Assert.That(actual.TextClassificationManagerFactory, Is.InstanceOf<ITextClassificationManagerFactory>());
            Assert.That(actual.LoggingActionAsciiBanner, Is.InstanceOf<Action<string>>());

            Assert.That(ArgumentManager.Success, Is.InstanceOf<int>());
            Assert.That(ArgumentManager.Failure, Is.InstanceOf<int>());
            Assert.That(ArgumentManager.SeparatorLine, Is.InstanceOf<string>());
            Assert.That(ArgumentManager.DefaultLoggingActionAsciiBanner, Is.InstanceOf<Action<string>>());

        }

        [Test]
        public void ArgumentManager_ShouldUseProvidedDependencies_WhenInvoked()
        {

            // Arrange
            DoubleManager doubleManager = new DoubleManager();
            AsciiBannerManager asciiBannerManager = new AsciiBannerManager();
            TerminalWindowManager terminalWindowManager = new TerminalWindowManager();
            TextClassificationManagerFactory textClassificationManagerFactory = new TextClassificationManagerFactory();
            Action<string> loggingActionAsciiBanner = (msg) => { };

            // Act
            ArgumentManager actual
                = new ArgumentManager(
                        doubleManager: doubleManager,
                        asciiBannerManager: asciiBannerManager,
                        terminalWindowManager: terminalWindowManager,
                        textClassificationManagerFactory: textClassificationManagerFactory,
                        loggingActionAsciiBanner: loggingActionAsciiBanner);

            // Assert
            Assert.That(doubleManager, Is.SameAs(actual.DoubleManager));
            Assert.That(asciiBannerManager, Is.SameAs(actual.AsciiBannerManager));
            Assert.That(terminalWindowManager, Is.SameAs(actual.TerminalWindowManager));
            Assert.That(textClassificationManagerFactory, Is.SameAs(actual.TextClassificationManagerFactory));
            Assert.That(loggingActionAsciiBanner, Is.SameAs(actual.LoggingActionAsciiBanner));

        }

        #endregion

        #region Tests (Create)

        [TestCaseSource(nameof(createExecuteTestCases))]
        public void Create_ShouldReturnExpectedExitCode_WhenExecuted(string[] args, int expected)
        {

            // Arrange
            CommandLineApplication app = new ArgumentManager().Create();

            // Act
            int actual = app.Execute(args);

            // Assert
            Assert.That(expected, Is.EqualTo(actual));

        }

        [Test]
        public void Create_ShouldCreateAnObjectOfTypeCommandLineApplication_WhenInvoked()
        {

            // Arrange
            // Act
            CommandLineApplication actual = new ArgumentManager().Create();

            // Assert
            Assert.That(actual, Is.InstanceOf<CommandLineApplication>());

        }

        [Test]
        public void Create_ShouldSetExpectedApplicationInformation_WhenInvoked()
        {

            // Arrange
            // Act
            CommandLineApplication actual = new ArgumentManager().Create();

            // Assert
            Assert.That(CommandLineString.APPLICATION_NAME, Is.EqualTo(actual.Name));
            Assert.That(CommandLineString.APPLICATION_DESCRIPTION, Is.EqualTo(actual.Description));

        }

        [Test]
        public void Create_ShouldAddExpectedClassifyCommand_WhenInvoked()
        {

            // Arrange
            // Act
            CommandLineApplication actual = new ArgumentManager().Create();

            CommandLineApplication classifyCommand
                = actual.Commands.Single(
                    command => command.Name == CommandLineString.COMMAND_CLASSIFY_NAME);

            // Assert
            Assert.That(classifyCommand, Is.InstanceOf<CommandLineApplication>());
            Assert.That(CommandLineString.COMMAND_CLASSIFY_DESCR, Is.EqualTo(classifyCommand.Description));

        }

        [Test]
        public void Create_ShouldAddExpectedClassifyOptions_WhenInvoked()
        {

            // Arrange
            // Act
            CommandLineApplication actual = new ArgumentManager().Create();

            CommandLineApplication classifyCommand
                = actual.Commands.Single(
                    command => command.Name == CommandLineString.COMMAND_CLASSIFY_NAME);

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
                    option => option.Description == CommandLineString.OPTION_SAVEOUTPUT_DESCR),
                Is.InstanceOf<CommandOption>());

            Assert.That(
                classifyCommand.Options.Single(
                    option => option.Description == CommandLineString.OPTION_CLEANLABELEDEXAMPLES_DESCR),
                Is.InstanceOf<CommandOption>());

            Assert.That(
                classifyCommand.Options.Single(
                    option => option.Description == CommandLineString.OPTION_DISABLEINDEXSERIALIZATION_DESCR),
                Is.InstanceOf<CommandOption>());

            Assert.That(
                classifyCommand.Options.Single(
                    option => option.Description == CommandLineString.OPTION_ENABLEUTC_DESCR),
                Is.InstanceOf<CommandOption>());

        }

        [Test]
        public void Create_ShouldAddExpectedScaffoldCommand_WhenInvoked()
        {

            // Arrange
            // Act
            CommandLineApplication actual = new ArgumentManager().Create();

            CommandLineApplication scaffoldCommand
                = actual.Commands.Single(
                    command => command.Name == CommandLineString.COMMAND_SCAFFOLD_NAME);

            // Assert
            Assert.That(scaffoldCommand, Is.InstanceOf<CommandLineApplication>());
            Assert.That(CommandLineString.COMMAND_SCAFFOLD_DESCR, Is.EqualTo(scaffoldCommand.Description));

        }

        [Test]
        public void Create_ShouldAddExpectedScaffoldOptions_WhenInvoked()
        {

            // Arrange
            // Act
            CommandLineApplication actual = new ArgumentManager().Create();

            CommandLineApplication scaffoldCommand
                = actual.Commands.Single(
                    command => command.Name == CommandLineString.COMMAND_SCAFFOLD_NAME);

            // Assert
            Assert.That(
                scaffoldCommand.Options.Single(
                    option => option.Description == CommandLineString.OPTION_FOLDERPATH_DESCR),
                Is.InstanceOf<CommandOption>());

        }

        #endregion

        #region Tests (Private Methods)

        [Test]
        public void LogAsciiBanner_ShouldLogExpectedMessages_WhenInvoked()
        {

            // Arrange
            List<string> messagesAsciiBanner = new List<string>();
            Action<string> loggingActionAsciiBanner = (msg) => messagesAsciiBanner.Add(msg);

            ArgumentManager argumentManager = new ArgumentManager(loggingActionAsciiBanner: loggingActionAsciiBanner);

            // Act
            ObjectMother.CallPrivateMethod<ArgumentManager, object>(
                    obj: argumentManager, 
                    methodName: "LogAsciiBanner", 
                    args: new object[] { }
                );

            // Assert
            Assert.That(messagesAsciiBanner.Count, Is.EqualTo(3));
            Assert.That(ArgumentManager.SeparatorLine, Is.EqualTo(messagesAsciiBanner[0]));
            Assert.That(messagesAsciiBanner[1], Is.InstanceOf<string>());
            Assert.That(ArgumentManager.SeparatorLine, Is.EqualTo(messagesAsciiBanner[2]));

        }

        [Test]
        public void ShowHeader_ShouldLogAsciiBannerAndReturnSuccess_WhenInvoked()
        {

            // Arrange
            List<string> messagesAsciiBanner = new List<string>();
            Action<string> loggingActionAsciiBanner = (msg) => messagesAsciiBanner.Add(msg);

            ArgumentManager argumentManager = new ArgumentManager(loggingActionAsciiBanner: loggingActionAsciiBanner);

            // Act
            int actual = ObjectMother.CallPrivateMethod<ArgumentManager, int>(
                    obj: argumentManager, 
                    methodName: "ShowHeader", 
                    args: new object[] { }
                );

            // Assert
            Assert.That(ArgumentManager.Success, Is.EqualTo(actual));
            Assert.That(messagesAsciiBanner.Count, Is.EqualTo(3));

        }

        #endregion

        #region TearDown
        #endregion

        #region SupportMethods
        #endregion

    }
}