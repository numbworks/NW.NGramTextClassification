using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;

namespace NW.NGramTextClassification.UnitTests
{
    [TestFixture]
    public class ValidatorTests
    {

        // Fields
        private static TestCaseData[] validateArrayExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateArray<string>(
                                null,
                                ObjectMother.VariableName)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.VariableName).Message
                ).SetArgDisplayNames($"{nameof(validateArrayExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateArray(
                                Array.Empty<string>(),
                                ObjectMother.VariableName)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException(MessageCollection.VariableContainsZeroItems.Invoke(ObjectMother.VariableName)).Message
                ).SetArgDisplayNames($"{nameof(validateArrayExceptionTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateArray<ArgumentException, string>(
                                null,
                                ObjectMother.VariableName)
                    ),
                typeof(ArgumentException),
                new ArgumentException(ObjectMother.VariableName).Message
                ).SetArgDisplayNames($"{nameof(validateArrayExceptionTestCases)}_03"),

        };
        private static TestCaseData[] validateLengthExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateLength(0)
                    ),
                typeof(ArgumentException),
                new ArgumentException(
                        MessageCollection.VariableCantBeLessThanOne.Invoke(ObjectMother.VariableName_Length)).Message
                ).SetArgDisplayNames($"{nameof(validateLengthExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateLength<Exception>(0)
                    ),
                typeof(Exception),
                new Exception(
                        MessageCollection.VariableCantBeLessThanOne.Invoke(ObjectMother.VariableName_Length)).Message
                ).SetArgDisplayNames($"{nameof(validateLengthExceptionTestCases)}_02")

        };
        private static TestCaseData[] validateObjectExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateObject(null, ObjectMother.VariableName)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.VariableName).Message
                ).SetArgDisplayNames($"{nameof(validateObjectExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateObject<ArgumentException>(null, ObjectMother.VariableName)
                    ),
                typeof(ArgumentException),
                new ArgumentException(ObjectMother.VariableName).Message
                ).SetArgDisplayNames($"{nameof(validateObjectExceptionTestCases)}_02")

        };
        private static TestCaseData[] allExceptionTestCases =
                                            new List<TestCaseData>()
                                                .Concat(validateArrayExceptionTestCases)
                                                .Concat(validateLengthExceptionTestCases)
                                                .Concat(validateObjectExceptionTestCases)
                                                .ToArray();

        // SetUp
        // Tests
        /*
         * This one will work correctly in both NCrunch and Test Explorer 
         * if using NUnit 3.13.0 and NUnit3 Adapter 3.17.0.
         * 
         */
        [TestCaseSource(nameof(allExceptionTestCases))]
        public void ValidateMethod_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
        {

            // Arrange
            // Act
            // Assert
            Exception actual = Assert.Throws(expectedType, del);
            Assert.AreEqual(expectedMessage, actual.Message);

        }

        [Test]
        public void ValidateArray_ShouldDoNothing_WhenProperArgument()
        {

            try
            {

                // Arrange
                // Act
                Validator.ValidateArray(ObjectMother.Array1, ObjectMother.VariableName);
                Validator.ValidateArray<ArgumentException, string>(ObjectMother.Array1, ObjectMother.VariableName);

            }
            catch (Exception ex)
            {

                // Assert
                Assert.Fail(ex.Message);

            }

        }

        [Test]
        public void ValidateLength_ShouldDoNothing_WhenProperArgument()
        {

            try
            {

                // Arrange
                // Act
                Validator.ValidateLength(ObjectMother.Length1);
                Validator.ValidateLength<ArgumentException>(ObjectMother.Length1);

            }
            catch (Exception ex)
            {

                // Assert
                Assert.Fail(ex.Message);

            }

        }

        [Test]
        public void ValidateObject_ShouldDoNothing_WhenProperArgument()
        {

            try
            {

                // Arrange
                // Act
                Validator.ValidateObject(ObjectMother.Object1, ObjectMother.VariableName);
                Validator.ValidateObject<ArgumentException>(ObjectMother.Object1, ObjectMother.VariableName);

            }
            catch (Exception ex)
            {

                // Assert
                Assert.Fail(ex.Message);

            }           

        }

        // TearDown
        // Support methods

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 08.01.2021

*/
