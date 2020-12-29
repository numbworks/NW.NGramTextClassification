using System;
using System.Collections.Generic;
using NUnit.Framework;
using RUBN.Shared;

namespace NW.NGrams.UnitTests
{
    [TestFixture]
    public class ArrayDelimiterManagerTests
    {

        // Fields
        private static string _strDelimiter = " ";
        private static TestCaseData[] _arrTestCases =
        {

            new TestCaseData(
                new string[] { "This", "is", "a", "sample", "text" },
                new Outcome(
                    OutcomeStatuses.Success,
                        new List<string>() { String.Format(
                            "The provided delimiter ('{0}') has been successfully added among the items of the provided array.",
                            _strDelimiter) },
                        new string[] { "This", " ", "is", " ", "a", " ", "sample", " ", "text" })
            ).SetName(nameof(AddDelimiter_ShouldReturnExpectedStatusMessageAndStringArray_WhenInvoked) + " {01}"),

            new TestCaseData(
                new string[] { "This", "is", "a", "sample" },
                new Outcome(
                    OutcomeStatuses.Success,
                        new List<string>() { String.Format(
                            "The provided delimiter ('{0}') has been successfully added among the items of the provided array.",
                            _strDelimiter) },
                        new string[] { "This", " ", "is", " ", "a", " ", "sample" })
            ).SetName(nameof(AddDelimiter_ShouldReturnExpectedStatusMessageAndStringArray_WhenInvoked) + " {02}"),

            new TestCaseData(
                new string[] { "This" },
                new Outcome(
                    OutcomeStatuses.Success,
                        new List<string>() { String.Format(
                            "The provided delimiter ('{0}') has been successfully added among the items of the provided array.",
                            _strDelimiter) },
                        new string[] { "This" })
            ).SetName(nameof(AddDelimiter_ShouldReturnExpectedStatusMessageAndStringArray_WhenInvoked) + " {03}"),

            new TestCaseData(
                null,
                new Outcome(
                    OutcomeStatuses.Failure,
                        new List<string>() { "The parameter at position '0' is null or empty." },
                        null)
            ).SetName(nameof(AddDelimiter_ShouldReturnExpectedStatusMessageAndStringArray_WhenInvoked) + " {04}"),

            new TestCaseData(
                new string[] { },
                new Outcome(
                    OutcomeStatuses.Failure,
                        new List<string>() { "The parameter at position '0' is null or empty." },
                        null)
            ).SetName(nameof(AddDelimiter_ShouldReturnExpectedStatusMessageAndStringArray_WhenInvoked) + " {05}"),

        };

        // SetUp
        // Tests
        [TestCaseSource(nameof(_arrTestCases))]
        public void AddDelimiter_ShouldReturnExpectedStatusMessageAndStringArray_WhenInvoked
            (string[] arrActual, Outcome objExpected)
        {

            // Arrange
            // Act
            Outcome objActual = new ArrayDelimiterManager().AddDelimiter(arrActual, _strDelimiter);

            // Assert
            Assert.AreEqual(objExpected.Status, objActual.Status);
            Assert.AreEqual(objExpected.Messages[0], objActual.Messages[0]);
            Assert.AreEqual(objExpected.Result, objActual.Result);

        }

        [Test]
        public void AddDelimiter_ShouldThrowAnException_WhenParametersValidatorIsNull()
        {

            // Arrange
            string[] arr = new string[] { "This", "is", "a", "sample", "text" };
            ArrayDelimiterManager objDelimiterManager = new ArrayDelimiterManager() { ParametersValidator = null };

            // Act
            Outcome objActual = objDelimiterManager.AddDelimiter(arr, _strDelimiter);

            // Assert
            Assert.IsTrue(objActual.IsException());

        }

        // TearDown

    }
}

/*

    Author: rua@sitecore.net
    Last Update: 16.02.2018

*/
