using System;
using NW.NGramTextClassification.CLI.TerminalWindows;
using NUnit.Framework;
using NSubstitute;

namespace NW.NGramTextClassification.CLI.UnitTests.TerminalWindows
{
    [TestFixture]
    public class TerminalWindowManagerTests
    {

        #region Tests

        [Test]
        public void TerminalWindowManager_ShouldUseDefaultFunctions_WhenParametersAreNull()
        {

            // Arrange & Act
            TerminalWindowManager terminalWindowManager = new TerminalWindowManager(null, null);

            // Assert
            Assert.That(terminalWindowManager.ConsoleWidthFunction, Is.EqualTo(TerminalWindowManager.DefaultConsoleWidthFunction));
            Assert.That(terminalWindowManager.SttySizeFunction, Is.EqualTo(TerminalWindowManager.DefaultSttySizeFunction));

        }

        [Test]
        public void GetOrCutoff_ShouldReturnConsoleWidth_WhenConsoleWidthFunctionReturnsValue()
        {

            // Arrange
            Func<uint?> consoleWidthFunction = Substitute.For<Func<uint?>>();
            consoleWidthFunction.Invoke().Returns((uint)120);

            Func<uint?> sttySizeFunction = Substitute.For<Func<uint?>>();

            // Act
            uint actual = new TerminalWindowManager(consoleWidthFunction, sttySizeFunction).GetOrCutoff();

            // Assert
            Assert.That(actual, Is.EqualTo(120));
            sttySizeFunction.DidNotReceive().Invoke();

        }

        [Test]
        public void GetOrCutoff_ShouldReturnSttySize_WhenSttySizeFunctionReturnsValue()
        {

            // Arrange
            Func<uint?> consoleWidthFunction = Substitute.For<Func<uint?>>();
            consoleWidthFunction.Invoke().Returns((uint?)null);

            Func<uint?> sttySizeFunction = Substitute.For<Func<uint?>>();
            sttySizeFunction.Invoke().Returns((uint)85);

            // Act
            uint actual = new TerminalWindowManager(consoleWidthFunction, sttySizeFunction).GetOrCutoff();

            // Assert
            Assert.That(actual, Is.EqualTo(85));

        }

        [Test]
        public void GetOrCutoff_ShouldReturnCutoffWidth_WhenConsoleWidthFunctionAndSttySizeFunctionReturnNull()
        {

            // Arrange
            Func<uint?> consoleWidthFunction = Substitute.For<Func<uint?>>();
            consoleWidthFunction.Invoke().Returns((uint?)null);

            Func<uint?> sttySizeFunction = Substitute.For<Func<uint?>>();
            sttySizeFunction.Invoke().Returns((uint?)null);

            // Act
            uint actual = new TerminalWindowManager(consoleWidthFunction, sttySizeFunction).GetOrCutoff();

            // Assert
            Assert.That(actual, Is.EqualTo(TerminalWindowManager.CutoffWidth));

        }

        #endregion

    }
}