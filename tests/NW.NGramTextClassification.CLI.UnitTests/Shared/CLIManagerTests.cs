using System;
using NW.NGramTextClassification.CLI.ArgumentParsing;
using NW.NGramTextClassification.CLI.Shared;
using McMaster.Extensions.CommandLineUtils;
using NUnit.Framework;

namespace NW.NGramTextClassification.CLI.UnitTests.Shared
{
    [TestFixture]
    public class CLIManagerTests
    {

        #region Fields

        private static TestCaseData[] tryRunTestCases =
        {

            new TestCaseData(
                new string[] { },
                0
            ).SetArgDisplayNames($"{nameof(tryRunTestCases)}_01"),

            new TestCaseData(
                new string[] { "SomeArgument" },
                1
            ).SetArgDisplayNames($"{nameof(tryRunTestCases)}_02"),

            new TestCaseData(
                new string[] { "SomeArgument", "AnotherArgument" },
                -1
            ).SetArgDisplayNames($"{nameof(tryRunTestCases)}_03")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [Test]
        public void CLIManager_ShouldCreateAnObjectOfTypeCLIManager_WhenInvoked()
        {

            // Arrange
            // Act
            CLIManager actual = new CLIManager();

            // Assert
            Assert.That(actual, Is.InstanceOf<CLIManager>());
            Assert.That(actual.ArgumentManager, Is.InstanceOf<IArgumentManager>());

        }

        [Test]
        public void CLIManager_ShouldUseProvidedArgumentManager_WhenInvoked()
        {

            // Arrange
            FakeArgumentManager expected = new FakeArgumentManager();

            // Act
            CLIManager actual = new CLIManager(argumentManager: expected);

            // Assert
            Assert.That(actual.ArgumentManager, Is.SameAs(expected));

        }

        [TestCaseSource(nameof(tryRunTestCases))]
        public void TryRun_ShouldReturnExpectedExitCode_WhenInvoked(string[] args, int expected)
        {

            // Arrange
            FakeArgumentManager fakeArgumentManager
                = new FakeArgumentManager(exitCode: expected);
            CLIManager cliManager
                = new CLIManager(argumentManager: fakeArgumentManager);

            // Act
            int actual = cliManager.TryRun(args);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));

        }

        [Test]
        public void TryRun_ShouldInvokeCreateOnce_WhenInvoked()
        {

            // Arrange
            FakeArgumentManager fakeArgumentManager = new FakeArgumentManager();
            CLIManager cliManager
                = new CLIManager(argumentManager: fakeArgumentManager);

            // Act
            cliManager.TryRun(new string[] { });

            // Assert
            Assert.That(fakeArgumentManager.CreateInvocations, Is.EqualTo(1));

        }

        [Test]
        public void TryRun_ShouldPassArgumentsToCommandLineApplication_WhenInvoked()
        {

            // Arrange
            string[] expected = new string[] { "SomeArgument", "AnotherArgument" };
            FakeArgumentManager fakeArgumentManager = new FakeArgumentManager();
            CLIManager cliManager
                = new CLIManager(argumentManager: fakeArgumentManager);

            // Act
            cliManager.TryRun(expected);

            // Assert
            Assert.That(fakeArgumentManager.ExecutedArguments, Is.EqualTo(expected));

        }

        [Test]
        public void TryRun_ShouldCreateNewCommandLineApplicationForEachInvocation_WhenInvokedMoreThanOnce()
        {

            // Arrange
            FakeArgumentManager fakeArgumentManager = new FakeArgumentManager();
            CLIManager cliManager
                = new CLIManager(argumentManager: fakeArgumentManager);

            // Act
            cliManager.TryRun(new string[] { });
            cliManager.TryRun(new string[] { });

            // Assert
            Assert.That(fakeArgumentManager.CreateInvocations, Is.EqualTo(2));

        }

        #endregion

        #region TearDown
        #endregion

        #region Support_methods

        private class FakeArgumentManager : IArgumentManager
        {

            public int ExitCode { get; }
            public int CreateInvocations { get; private set; }
            public string[] ExecutedArguments { get; private set; }

            public FakeArgumentManager(int exitCode = 0)
            {

                ExitCode = exitCode;

            }

            public CommandLineApplication Create()
            {

                CreateInvocations++;

                CommandLineApplication app = new CommandLineApplication();
                CommandArgument<string> firstArgument
                    = app.Argument<string>("firstArgument", string.Empty);
                CommandArgument<string> secondArgument
                    = app.Argument<string>("secondArgument", string.Empty);

                app.OnExecute(
                    () =>
                    {

                        ExecutedArguments
                            = new string[]
                            {

                                firstArgument.Value,
                                secondArgument.Value

                            };
                        ExecutedArguments
                            = Array.FindAll(
                                ExecutedArguments,
                                value => value != null);

                        return ExitCode;

                    });

                return app;

            }

        }

        #endregion

    }
}