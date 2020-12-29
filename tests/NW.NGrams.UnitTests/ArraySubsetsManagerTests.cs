using System;
using System.Collections.Generic;
using NUnit.Framework;
using RUBN.Shared;

namespace NW.NGrams.UnitTests
{
    [TestFixture]
    public class ArraySubsetsManagerTests
    {

        // Fields
        private static int _intStartIndex = 0;
        private static int _intLength = 3;
        private static TestCaseData[] _arrTestCases = 
        {

            new TestCaseData(
                new string[] { "This", "is", "a", "sample", "text" },
                OutcomeBuilder.CreateSuccess(
                    new List<string>() { String.Format(
                        "The required subset has been successfully created out of the provided array (arr.Length: '{0}', arrSubset.Length: '{1}').",
                        5.ToString(),
                        _intLength.ToString()) },
                    new string[] { "This", "is", "a" }
                    ).Get()
            ).SetName(nameof(GetSubset_ShouldReturnExpectedStatusMessageAndStringArray_WhenInvoked) + " {01}"),

            new TestCaseData(
                new string[] { "This", "is", "a" },
                OutcomeBuilder.CreateSuccess(
                    new List<string>() { String.Format(
                        "The required subset has been successfully created out of the provided array (arr.Length: '{0}', arrSubset.Length: '{1}').",
                        3.ToString(),
                        _intLength.ToString()) },
                    new string[] { "This", "is", "a" }).Get()
            ).SetName(nameof(GetSubset_ShouldReturnExpectedStatusMessageAndStringArray_WhenInvoked) + " {02}"),

            new TestCaseData(
                null,
                OutcomeBuilder.CreateFailure(
                        new List<string>() {
                        "The parameter at position '0' is null or empty.",
                        "It hasn't been possible to create the required subset out of the provided array.",
                    },
                    null).Get()
            ).SetName(nameof(GetSubset_ShouldReturnExpectedStatusMessageAndStringArray_WhenInvoked) + " {03}"),

            new TestCaseData(
                new string[] { },
                OutcomeBuilder.CreateFailure(
                    new List<string>() {
                        "The parameter at position '0' is null or empty.",
                        "It hasn't been possible to create the required subset out of the provided array.",
                    },
                    null).Get()                    
            ).SetName(nameof(GetSubset_ShouldReturnExpectedStatusMessageAndStringArray_WhenInvoked) + " {04}")

        };

        // SetUp
        // Tests
        [TestCaseSource(nameof(_arrTestCases))]
        public void GetSubset_ShouldReturnExpectedStatusMessageAndStringArray_WhenInvoked
            (string[] arrActual, Outcome objExpected)
        {

            // Arrange
            // Act
            Outcome objActual = new ArraySubsetsManager().GetSubset(arrActual, _intStartIndex, _intLength);

            // Assert
            Assert.AreEqual(objExpected.Status, objActual.Status);
            Assert.AreEqual(objExpected.Messages[0], objActual.Messages[0]);
            Assert.AreEqual(objExpected.Result, objActual.Result);

        }

        [Test]
        public void GetSubset_ShouldThrowAndException_WhenArrLenghtIsShorterThanIntLength()
        {

            // Arrange
            string[] arrActual = new string[] { "This", "is" };

            // Act
            Outcome objActual = new ArraySubsetsManager().GetSubset(arrActual, _intStartIndex, _intLength);

            // Assert
            Assert.IsTrue(objActual.IsException());

        }

        [Test]
        public void GetSubset_ShouldReturnFailureAndTheExpectedMessage_WheIntLengthIsLessThanOne()
        {

            // Arrange
            string[] arrActual = new string[] { "This", "is" };
            int intLength = 0;
            string errAtLeastOne = "'intLength' must be at least equal to 1 (actual value:'0').";

            // Act
            Outcome objActual = new ArraySubsetsManager().GetSubset(arrActual, _intStartIndex, intLength);

            // Assert
            Assert.AreEqual(OutcomeStatuses.Failure, objActual.Status);
            StringAssert.Contains(errAtLeastOne, objActual.Messages[0]);

        }

        [Test]
        public void GetSubset_ShouldThrowAndException_WhenParametersValidatorIsNull()
        {

            // Arrange
            ArraySubsetsManager objArraySubsetsManager = new ArraySubsetsManager() { ParametersValidator = null };
            string[] arrActual = new string[] { "This", "is", "a", "sample", "text" };

            // Act
            Outcome objActual = objArraySubsetsManager.GetSubset(arrActual, _intStartIndex, _intLength);

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
