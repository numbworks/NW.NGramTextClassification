using System;
using System.ComponentModel.DataAnnotations;
using NW.NGramTextClassificationClient.ApplicationSession;
using NW.NGramTextClassificationClient.Shared;
using NUnit.Framework;
using McMaster.Extensions.CommandLineUtils;

namespace NW.NGramTextClassificationClient.UnitTests.ApplicationSession
{
    [TestFixture]
    public class MinimumAccuracyValidatorTests
    {

        #region Fields

        private static TestCaseData[] minimumAccuracyValidatorExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new MinimumAccuracyValidator(null)
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("doubleManager").Message
            ).SetArgDisplayNames($"{nameof(minimumAccuracyValidatorExceptionTestCases)}_01")

        };

        #endregion

        #region SetUp

        #endregion

        #region Tests

        [TestCaseSource(nameof(minimumAccuracyValidatorExceptionTestCases))]
        public void MinimumAccuracyValidator_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void MinimumAccuracyValidator_ShouldCreateAnObjectOfThisType_WhenInvoked()
        {

            // Arrange
            // Act
            MinimumAccuracyValidator actual = new MinimumAccuracyValidator(new DoubleManager());

            // Assert
            Assert.IsInstanceOf<MinimumAccuracyValidator>(actual);

        }

        [TestCase("somegarbage")]
        public void GetValidationResult_ShouldReturnExpectedErrorMessage_WhenInvalidOptionValue(string value)
        {

            // Arrange
            CommandOption option = new CommandOption(MessageCollection.Session_Option_MinAccuracySingle_Template, CommandOptionType.SingleValue);
            option.DefaultValue = value;
            ValidationContext context = new ValidationContext(option);
            string valueName = nameof(MinimumAccuracyValidator).Replace("Validator", string.Empty);
            string expected = MessageCollection.ValueIsInvalidOrNotWithinRange(valueName, option.Value());

            // Act
            ValidationResult actual = new MinimumAccuracyValidator(new DoubleManager()).GetValidationResult(option, context);

            // Assert
            Assert.AreEqual(expected, actual.ErrorMessage);

        }

        [TestCase("0.0")]
        [TestCase("0.3")]
        [TestCase("1.0")]
        [TestCase("")]
        [TestCase((string)null)]
        public void GetValidationResult_ShouldReturnSuccess_WhenValidOptionValue(string value)
        {

            // Arrange
            CommandOption option = new CommandOption(MessageCollection.Session_Option_MinAccuracySingle_Template, CommandOptionType.SingleValue);
            option.DefaultValue = value;
            ValidationContext context = new ValidationContext(option);

            // Act
            ValidationResult actual = new MinimumAccuracyValidator(new DoubleManager()).GetValidationResult(option, context);

            // Assert
            Assert.AreEqual(ValidationResult.Success, actual);

        }

        #endregion

        #region TearDown

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 23.10.2022
*/
