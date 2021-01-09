using System;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests
{
    [TestFixture]
    public class ValidatorTests
    {

        // Fields
        private static TestCaseData[] validateArrayExceptionTextCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateArray<string>(
                                null,
                                ObjectMother.VariableName)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.VariableName).Message
                ),

            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateArray(
                                Array.Empty<string>(),
                                ObjectMother.VariableName)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException(MessageCollection.VariableContainsZeroItems.Invoke(ObjectMother.VariableName)).Message
                ),

            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateArray<ArgumentException, string>(
                                null, 
                                ObjectMother.VariableName)
                    ),
                typeof(ArgumentException),
                new ArgumentException(ObjectMother.VariableName).Message
                ),

        };
        private static TestCaseData[] validateLengthExceptionTextCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateLength(0)
                    ),
                typeof(ArgumentException),
                new ArgumentException(
                        MessageCollection.VariableCantBeLessThanOne.Invoke(ObjectMother.VariableName_Length)).Message
                ),

            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateLength<Exception>(0)
                    ),
                typeof(Exception),
                new Exception(
                        MessageCollection.VariableCantBeLessThanOne.Invoke(ObjectMother.VariableName_Length)).Message
                ),

        };
        private static TestCaseData[] validateObjectExceptionTextCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateObject(null, ObjectMother.VariableName)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.VariableName).Message
                ),

            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateObject<ArgumentException>(null, ObjectMother.VariableName)
                    ),
                typeof(ArgumentException),
                new ArgumentException(ObjectMother.VariableName).Message
                ),

        };

        // SetUp
        // Tests
        #region ValidateArrayTestMethods
        [TestCaseSource(nameof(validateArrayExceptionTextCases))]
        public void ValidateArray_ShouldThrowACertainException_WhenUnproperArguments
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
        #endregion

        #region ValidateLengthTestMethods
        [TestCaseSource(nameof(validateLengthExceptionTextCases))]
        public void ValidateLength_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
        {

            // Arrange
            // Act
            // Assert
            Exception actual = Assert.Throws(expectedType, del);
            Assert.AreEqual(expectedMessage, actual.Message);

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
        #endregion

        #region ValidateObjectTestMethods
        [TestCaseSource(nameof(validateObjectExceptionTextCases))]
        public void ValidateObject_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
        {

            // Arrange
            // Act
            // Assert
            Exception actual = Assert.Throws(expectedType, del);
            Assert.AreEqual(expectedMessage, actual.Message);

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
        #endregion

        // TearDown
        // Support methods

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 08.01.2021

*/
