using System.Collections.Generic;
using NUnit.Framework;
using RUBN.Shared;

namespace NW.NGrams.UnitTests
{
    [TestFixture]
    public class JaccardIndexCalculatorTests
    {

        // Fields
        private static string _msgSuccess = "The Jaccard Index out of the provided Lists has been calculated.";
        private static string _errFailure = "It hasn't been possible to calculate the Jaccard Index out of the provided Lists.";
        private static TestCaseData[] _arrTestCases = 
        {

            new TestCaseData(
                new List<string>() {
                    "smu is the", "is the best", "the best college", "best college in", "college in texas" },
                new List<string>() {
                    "in texas the", "texas the best", "the best college", "best college is", "college is tcu" },
                new Outcome(
                    OutcomeStatuses.Success,
                    new List<string>() {
                        _msgSuccess },
                    0.11)
            ).SetName(nameof(Do_ShouldReturnExpectedStatusMessagesAndResult_WhenInvoked) + " {01}"),

            new TestCaseData(
                new List<string>(),
                new List<string>() {
                    "in texas the", "texas the best", "the best college", "best college is", "college is tcu" },
                new Outcome(
                    OutcomeStatuses.Failure,
                    new List<string>() {
                        "The parameter at position '0' is null or empty.",
                        _errFailure },
                    null)
            ).SetName(nameof(Do_ShouldReturnExpectedStatusMessagesAndResult_WhenInvoked) + " {02}"),

            new TestCaseData(
                null,
                new List<string>() {
                    "in texas the", "texas the best", "the best college", "best college is", "college is tcu" },
                new Outcome(
                    OutcomeStatuses.Failure,
                    new List<string>() {
                        "The parameter at position '0' is null or empty.",
                        _errFailure },
                    null)
            ).SetName(nameof(Do_ShouldReturnExpectedStatusMessagesAndResult_WhenInvoked) + " {03}"),

            new TestCaseData(
                new List<string>() {
                    "smu is the", "is the best", "the best college", "best college in", "college in texas" },
                new List<string>(),
                new Outcome(
                    OutcomeStatuses.Failure,
                    new List<string>() {
                        "The parameter at position '1' is null or empty.",
                        _errFailure },
                    null)
            ).SetName(nameof(Do_ShouldReturnExpectedStatusMessagesAndResult_WhenInvoked) + " {04}"),

            new TestCaseData(
                new List<string>() {
                    "smu is the", "is the best", "the best college", "best college in", "college in texas" },
                null,
                new Outcome(
                    OutcomeStatuses.Failure,
                    new List<string>() {
                        "The parameter at position '1' is null or empty.",
                        _errFailure },
                    null)
            ).SetName(nameof(Do_ShouldReturnExpectedStatusMessagesAndResult_WhenInvoked) + " {05}")

        };

        // SetUp
        // Tests
        [TestCaseSource(nameof(_arrTestCases))]
        public void Do_ShouldReturnExpectedStatusMessagesAndResult_WhenInvoked(
            List<string> listA,
            List<string> listB,
            Outcome objExpected)
        {

            // Arrange
            // Act
            Outcome objActual = new JaccardIndexCalculator().Do(listA, listB);

            // Assert
            Assert.AreEqual(objExpected.Status, objActual.Status);

            if (objActual.Messages != null)
                for (int i = 0; i < objExpected.Messages.Count; i++)
                    Assert.AreEqual(objExpected.Messages[i], objActual.Messages[i]);

            if (objActual.Result != null)
                Assert.AreEqual((double)objExpected.Result, (double)objActual.Result);
            
        }

        [Test]
        public void Do_ShouldThrowAnException_WhenParametersValidatorIsNull()
        {

            // Arrange
            JaccardIndexCalculator objIndexCalculator = new JaccardIndexCalculator();
            objIndexCalculator.ParametersValidator = null;

            // Act
            Outcome objActual = objIndexCalculator.Do(new List<string>(), new List<string>());

            // Assert
            Assert.IsTrue(objActual.IsException());

        }

        // TearDown

    }
}

/*

    Author: rua@sitecore.net
    Last Update: 03.02.2018

*/
