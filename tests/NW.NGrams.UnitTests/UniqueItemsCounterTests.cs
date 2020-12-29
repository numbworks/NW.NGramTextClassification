using System;
using System.Collections.Generic;
using NUnit.Framework;
using RUBN.Shared;

namespace NW.NGrams.UnitTests
{
    [TestFixture]
    public class UniqueItemsCounterTests
    {

        // Fields
        private static string _msgSuccess = "The unique items in the provided List have been counted (before: '{0}', after: '{1}').";
        private static string _errFailure = "It hasn't been possible to count the unique items in the provided List.";
        private static TestCaseData[] _arrTestCases = 
        {

            new TestCaseData(
                new List<string>() {
                    "Item1", "Item2", "Item3", "Item4", "Item5", "Item6",
                    "Item1", "Item1", "Item2" },
                new Outcome(
                    OutcomeStatuses.Success,
                    new List<string>() {
                        String.Format(_msgSuccess, 9, 6) },
                    6)
            ).SetName(nameof(Do_ShouldReturnExpectedStatusMessagesAndResult_WhenInvoked) + " {01}"),

            new TestCaseData(
                new List<string>() { },
                new Outcome(
                    OutcomeStatuses.Failure,
                    new List<string>() {
                        "The parameter at position '0' is null or empty.",
                        _errFailure },
                    null)
            ).SetName(nameof(Do_ShouldReturnExpectedStatusMessagesAndResult_WhenInvoked) + " {02}"),

            new TestCaseData(
                null,
                new Outcome(
                    OutcomeStatuses.Failure,
                    new List<string>() {
                        "The parameter at position '0' is null or empty.",
                        _errFailure },
                    null)
            ).SetName(nameof(Do_ShouldReturnExpectedStatusMessagesAndResult_WhenInvoked) + " {03}")

        };

        // SetUp
        // Tests
        [TestCaseSource(nameof(_arrTestCases))]
        public void Do_ShouldReturnExpectedStatusMessagesAndResult_WhenInvoked(List<string> listItems, Outcome objExpected)
        {

            // Arrange
            // Act
            Outcome objActual = new UniqueItemsCounter().Do(listItems);

            // Assert
            Assert.AreEqual(objExpected.Status, objActual.Status);

            if (objActual.Messages != null)
                for (int i = 0; i < objExpected.Messages.Count; i++)
                    Assert.AreEqual(objExpected.Messages[i], objActual.Messages[i]);

            if (objActual.Result != null)
                Assert.AreEqual((int)objExpected.Result, (int)objActual.Result);

        }

        [Test]
        public void Do_ShouldThrowAnException_WhenParameterValidatorIsNull()
        {

            // Arrange
            List<string> listItems = new List<string>() {
                    "Item1", "Item2", "Item3", "Item4", "Item5", "Item6",
                    "Item1", "Item1", "Item2" };
            UniqueItemsCounter objCounter = new UniqueItemsCounter();
            objCounter.ParametersValidator = null;

            // Act
            Outcome objActual = objCounter.Do(listItems);

            // Assert
            Assert.IsTrue(objActual.IsException());

        }

        // TearDown

    }
}

/*

    Author: rua@sitecore.net
    Last Update: 02.02.2018

*/
