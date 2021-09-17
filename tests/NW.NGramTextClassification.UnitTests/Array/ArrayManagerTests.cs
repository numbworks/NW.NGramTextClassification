using System;
using NUnit.Framework;
using NW.NGramTextClassification.Arrays;

namespace NW.NGramTextClassification.UnitTests
{
    [TestFixture]
    public class ArrayManagerTests
    {

        // Fields
        private static TestCaseData[] addDelimiterExceptionTestCases =
        {

            // ValidateArrayNull
            new TestCaseData(
                new TestDelegate(
                        () => new ArrayManager()
                                    .AddDelimiter(
                                        null, 
                                        ObjectMother.ArrayManager_Delimiter1)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.ArrayManager_VariableName_Arr).Message
                ).SetArgDisplayNames($"{nameof(addDelimiterExceptionTestCases)}_01"),

            // ValidateArrayEmpty
            new TestCaseData(
                new TestDelegate(
                        () => new ArrayManager()
                                    .AddDelimiter(
                                        Array.Empty<string>(), 
                                        ObjectMother.ArrayManager_Delimiter1)
                    ),
                typeof(ArgumentException),
                MessageCollection.Validator_VariableContainsZeroItems.Invoke(ObjectMother.ArrayManager_VariableName_Arr)
                ).SetArgDisplayNames($"{nameof(addDelimiterExceptionTestCases)}_02"),

            // ValidateStringNullOrEmpty
            new TestCaseData(
                new TestDelegate(
                        () => new ArrayManager()
                                    .AddDelimiter(
                                        ObjectMother.Validator_Array1, 
                                        null)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.ArrayManager_VariableName_Delimiter).Message
                ).SetArgDisplayNames($"{nameof(addDelimiterExceptionTestCases)}_03"),

            // ValidateStringNullOrEmpty
            new TestCaseData(
                new TestDelegate(
                        () => new ArrayManager()
                                    .AddDelimiter(
                                        ObjectMother.Validator_Array1, 
                                        string.Empty)
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
                MessageCollection.Validator_VariableContainsZeroItems.Invoke(ObjectMother.ArrayManager_VariableName_Arr)
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
                MessageCollection.Validator_VariableCantBeLessThanOne.Invoke(ObjectMother.ArrayManager_VariableName_Length)
                ).SetArgDisplayNames($"{nameof(getSubsetExceptionTestCases)}_03"),

            // ThrowIfFirstIsGreaterOrEqual
            new TestCaseData(
                new TestDelegate(
                        () => new ArrayManager()
                                    .GetSubset(
                                        ObjectMother.ArrayManager_Array1, // arr.Length = 4
                                        4,
                                        ObjectMother.ArrayManager_Length1)
                    ),
                typeof(ArgumentException),
                MessageCollection.Validator_FirstValueIsGreaterOrEqualThanSecondValue.Invoke
                    (ObjectMother.ArrayManager_VariableName_StartIndex, ObjectMother.ArrayManager_VariableName_ArrLength)
                ).SetArgDisplayNames($"{nameof(getSubsetExceptionTestCases)}_04"),

            // ThrowIfFirstIsGreater
            new TestCaseData(
                new TestDelegate(
                        () => new ArrayManager()
                                    .GetSubset(
                                        ObjectMother.ArrayManager_Array1, // arr.Length = 4
                                        0,
                                        5)
                    ),
                typeof(ArgumentException),
                MessageCollection.Validator_FirstValueIsGreaterThanSecondValue.Invoke
                    (ObjectMother.ArrayManager_VariableName_Length, ObjectMother.ArrayManager_VariableName_ArrLength)
                ).SetArgDisplayNames($"{nameof(getSubsetExceptionTestCases)}_05"),

            // ThrowIfFirstIsGreater
            new TestCaseData(
                new TestDelegate(
                        () => new ArrayManager()
                                    .GetSubset(
                                        ObjectMother.ArrayManager_Array1, // arr.Length = 4
                                        2,
                                        3)
                    ),
                typeof(ArgumentException),
                MessageCollection.Validator_FirstValueIsGreaterThanSecondValue.Invoke
                    (ObjectMother.ArrayManager_VariableName_StartIndexPlusLength, ObjectMother.ArrayManager_VariableName_ArrLength)
                ).SetArgDisplayNames($"{nameof(getSubsetExceptionTestCases)}_06")

        };

        // SetUp
        // Tests
        [TestCaseSource(nameof(addDelimiterExceptionTestCases))]
        public void AddDelimiter_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);
        [TestCaseSource(nameof(getSubsetExceptionTestCases))]
        public void GetSubset_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

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
        public void GetSubset_ShouldReturnASubsetArray_WhenProperArguments()
        {

            // Arrange
            // Act
            string[] actual = new ArrayManager().GetSubset(
                                                    ObjectMother.ArrayManager_Array1,
                                                    ObjectMother.ArrayManager_StartIndex1,
                                                    ObjectMother.ArrayManager_Length1);

            // Assert
            Assert.AreEqual(
                    ObjectMother.ArrayManager_Array1_Subset1,
                    actual);

        }

        // TearDown
        // Support methods

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 17.09.2021
*/
