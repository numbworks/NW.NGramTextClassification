using System;
using System.Collections;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests
{
    [TestFixture]
    public class ArrayManagerTests
    {

        // Fields
        private static TestCaseData[] addDelimiterExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new ArrayManager().AddDelimiter(null, ObjectMother.ArrayManager_Delimiter1)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.ArrayManager_VariableName_Arr).Message
                ).SetArgDisplayNames($"{nameof(addDelimiterExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new ArrayManager().AddDelimiter(Array.Empty<string>(), ObjectMother.ArrayManager_Delimiter1)
                    ),
                typeof(ArgumentException),
                MessageCollection.VariableContainsZeroItems.Invoke(ObjectMother.ArrayManager_VariableName_Arr)
                ).SetArgDisplayNames($"{nameof(addDelimiterExceptionTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                        () => new ArrayManager().AddDelimiter(ObjectMother.Validator_Array1, null)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.ArrayManager_VariableName_Delimiter).Message
                ).SetArgDisplayNames($"{nameof(addDelimiterExceptionTestCases)}_03"),

            new TestCaseData(
                new TestDelegate(
                        () => new ArrayManager().AddDelimiter(ObjectMother.Validator_Array1, string.Empty)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.ArrayManager_VariableName_Delimiter).Message
                ).SetArgDisplayNames($"{nameof(addDelimiterExceptionTestCases)}_04"),

        };
        private static TestCaseData[] getSubsetExceptionTestCases =
        {

            // ValidateArrayNull
            new TestCaseData(
                new TestDelegate(
                        () => new ArrayManager()
                                    .GetSubset(
                                        null, 
                                        ObjectMother.ArrayManager_StartIndex1,
                                        ObjectMother.ArrayManager_Length1)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.ArrayManager_VariableName_Arr).Message
                ).SetArgDisplayNames($"{nameof(getSubsetExceptionTestCases)}_01"),

            // ValidateArrayEmpty
            new TestCaseData(
                new TestDelegate(
                        () => new ArrayManager()
                                    .GetSubset(
                                        Array.Empty<string>(),
                                        ObjectMother.ArrayManager_StartIndex1,
                                        ObjectMother.ArrayManager_Length1)
                    ),
                typeof(ArgumentException),
                MessageCollection.VariableContainsZeroItems.Invoke(ObjectMother.ArrayManager_VariableName_Arr)
                ).SetArgDisplayNames($"{nameof(getSubsetExceptionTestCases)}_02"),

            // ValidateLength
            new TestCaseData(
                new TestDelegate(
                        () => new ArrayManager()
                                    .GetSubset(
                                        ObjectMother.ArrayManager_Array1,
                                        ObjectMother.ArrayManager_StartIndex1,
                                        0)
                    ),
                typeof(ArgumentException),
                MessageCollection.VariableCantBeLessThanOne.Invoke(ObjectMother.ArrayManager_VariableName_Length)
                ).SetArgDisplayNames($"{nameof(getSubsetExceptionTestCases)}_03")

        };

        // SetUp
        // Tests
        [TestCaseSource(nameof(addDelimiterExceptionTestCases))]
        public void AddDelimiter_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);
        [TestCaseSource(nameof(getSubsetExceptionTestCases))]
        public void GetSubset_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void AddDelimiter_ShouldAddADelimiterItemBetweenTheOtherItems_WhenProperArguments()
        {

            // Arrange
            // Act
            string[] actual = new ArrayManager().AddDelimiter(
                                                    ObjectMother.ArrayManager_Array1,
                                                    ObjectMother.ArrayManager_Delimiter1);

            // Assert
            Assert.AreEqual(
                    ObjectMother.ArrayManager_Array1_WithDelimiter1,
                    actual);

        }

        [Test]
        public void GetSubset_ShouldAReturnASubsetArray_WhenProperArguments()
        {

            // Arrange
            // Act
            // Assert

        }


        // TearDown
        // Support methods
        private void Method_ShouldThrowACertainException_WhenUnproperArguments
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
    Last Update: 12.01.2021

*/
