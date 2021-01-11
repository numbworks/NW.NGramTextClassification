using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests
{
    [TestFixture]
    public class ValidatorTests
    {

        // Fields
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
                typeof(ArgumentException),
                MessageCollection.VariableContainsZeroItems.Invoke(ObjectMother.VariableName)
                ).SetArgDisplayNames($"{nameof(validateArrayExceptionTestCases)}_02")

        };
        private static TestCaseData[] validateListExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateList(
                                (List<string>)null, 
                                ObjectMother.VariableName)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.VariableName).Message
                ).SetArgDisplayNames($"{nameof(validateListExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateList(
                                new List<string>() { },
                                ObjectMother.VariableName)
                    ),
                typeof(ArgumentException),
                MessageCollection.VariableContainsZeroItems.Invoke(ObjectMother.VariableName)
                ).SetArgDisplayNames($"{nameof(validateListExceptionTestCases)}_02"),

        };

        // SetUp
        // Tests
        [TestCaseSource(nameof(validateLengthExceptionTestCases))]
        public void ValidateLength_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ValidateMethod_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);
        [TestCaseSource(nameof(validateObjectExceptionTestCases))]
        public void ValidateObject_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ValidateMethod_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);
        [TestCaseSource(nameof(validateArrayExceptionTestCases))]
        public void ValidateArray_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ValidateMethod_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);
        [TestCaseSource(nameof(validateListExceptionTestCases))]
        public void ValidateList_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ValidateMethod_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

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

        [Test]
        public void ValidateArray_ShouldDoNothing_WhenProperArgument()
        {

            try
            {

                // Arrange
                // Act
                Validator.ValidateArray(ObjectMother.Array1, ObjectMother.VariableName);

            }
            catch (Exception ex)
            {

                // Assert
                Assert.Fail(ex.Message);

            }

        }

        [Test]
        public void ValidateList_ShouldDoNothing_WhenProperArgument()
        {

            try
            {

                // Arrange
                // Act
                Validator.ValidateList(ObjectMother.List1, ObjectMother.VariableName);

            }
            catch (Exception ex)
            {

                // Assert
                Assert.Fail(ex.Message);

            }

        }

        // TearDown
        // Support methods
        private void ValidateMethod_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
        {

            // Arrange
            // Act
            // Assert
            Exception actual = Assert.Throws(expectedType, del);
            Assert.AreEqual(expectedMessage, actual.Message);

        }

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 08.01.2021

*/
