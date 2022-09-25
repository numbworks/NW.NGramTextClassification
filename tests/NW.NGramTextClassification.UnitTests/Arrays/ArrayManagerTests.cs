using System;
using NW.NGramTextClassification.Arrays;
using NW.NGramTextClassification.Messages;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests.Arrays
{
    [TestFixture]
    public class ArrayManagerTests
    {

        #region Fields

        private static TestCaseData[] addDelimiterExceptionTestCases =
        {

            // ValidateArrayNull
            new TestCaseData(
                new TestDelegate(
                        () => new ArrayManager()
                                    .AddDelimiter(
                                        null,
                                        ObjectMother.Array01_Delimiter01)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException("arr").Message
                ).SetArgDisplayNames($"{nameof(addDelimiterExceptionTestCases)}_01"),

            // ValidateArrayEmpty
            new TestCaseData(
                new TestDelegate(
                        () => new ArrayManager()
                                    .AddDelimiter(
                                        Array.Empty<string>(),
                                        ObjectMother.Array01_Delimiter01)
                    ),
                typeof(ArgumentException),
                NGramTextClassification.Validation.MessageCollection.VariableContainsZeroItems("arr")
                ).SetArgDisplayNames($"{nameof(addDelimiterExceptionTestCases)}_02"),

            // ValidateStringNullOrEmpty
            new TestCaseData(
                new TestDelegate(
                        () => new ArrayManager()
                                    .AddDelimiter(
                                        Validation.ObjectMother.Array01,
                                        null)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException("delimiter").Message
                ).SetArgDisplayNames($"{nameof(addDelimiterExceptionTestCases)}_03"),

            // ValidateStringNullOrEmpty
            new TestCaseData(
                new TestDelegate(
                        () => new ArrayManager()
                                    .AddDelimiter(
                                        Validation.ObjectMother.Array01,
                                        string.Empty)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException("delimiter").Message
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
                                        ObjectMother.Array01_StartIndex01,
                                        ObjectMother.Array01_Length01)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException("arr").Message
                ).SetArgDisplayNames($"{nameof(getSubsetExceptionTestCases)}_01"),

            // ValidateArrayEmpty
            new TestCaseData(
                new TestDelegate(
                        () => new ArrayManager()
                                    .GetSubset(
                                        Array.Empty<string>(),
                                        ObjectMother.Array01_StartIndex01,
                                        ObjectMother.Array01_Length01)
                    ),
                typeof(ArgumentException),
                NGramTextClassification.Validation.MessageCollection.VariableContainsZeroItems("arr")
                ).SetArgDisplayNames($"{nameof(getSubsetExceptionTestCases)}_02"),

            // ValidateLength
            new TestCaseData(
                new TestDelegate(
                        () => new ArrayManager()
                                    .GetSubset(
                                        Validation.ObjectMother.Array01,
                                        ObjectMother.Array01_StartIndex01,
                                        0)
                    ),
                typeof(ArgumentException),
                NGramTextClassification.Validation.MessageCollection.VariableCantBeLessThanOne("length")
                ).SetArgDisplayNames($"{nameof(getSubsetExceptionTestCases)}_03"),

            // ThrowIfFirstIsGreaterOrEqual
            new TestCaseData(
                new TestDelegate(
                        () => new ArrayManager()
                                    .GetSubset(
                                        Validation.ObjectMother.Array01, // arr.Length = 4
                                        4,
                                        ObjectMother.Array01_Length01)
                    ),
                typeof(ArgumentException),
                NGramTextClassification.Validation.MessageCollection.FirstValueIsGreaterOrEqualThanSecondValue("startIndex", "arr.Length")
                ).SetArgDisplayNames($"{nameof(getSubsetExceptionTestCases)}_04"),

            // ThrowIfFirstIsGreater
            new TestCaseData(
                new TestDelegate(
                        () => new ArrayManager()
                                    .GetSubset(
                                        Validation.ObjectMother.Array01, // arr.Length = 4
                                        0,
                                        5)
                    ),
                typeof(ArgumentException),
                NGramTextClassification.Validation.MessageCollection.FirstValueIsGreaterThanSecondValue("length", "arr.Length")
                ).SetArgDisplayNames($"{nameof(getSubsetExceptionTestCases)}_05"),

            // ThrowIfFirstIsGreater
            new TestCaseData(
                new TestDelegate(
                        () => new ArrayManager()
                                    .GetSubset(
                                        Validation.ObjectMother.Array01, // arr.Length = 4
                                        2,
                                        3)
                    ),
                typeof(ArgumentException),
                NGramTextClassification.Validation.MessageCollection.FirstValueIsGreaterThanSecondValue("startIndex + length", "arr.Length")
                ).SetArgDisplayNames($"{nameof(getSubsetExceptionTestCases)}_06")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(addDelimiterExceptionTestCases))]
        public void AddDelimiter_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(getSubsetExceptionTestCases))]
        public void GetSubset_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void AddDelimiter_ShouldAddADelimiterItemBetweenTheOtherItems_WhenProperArguments()
        {

            // Arrange
            // Act
            string[] actual 
                = new ArrayManager().AddDelimiter(
                                        Validation.ObjectMother.Array01,
                                        ObjectMother.Array01_Delimiter01);

            // Assert
            Assert.AreEqual(
                    ObjectMother.Array01_WithDelimiter01,
                    actual);

        }

        [Test]
        public void GetSubset_ShouldReturnASubsetArray_WhenProperArguments()
        {

            // Arrange
            // Act
            string[] actual 
                = new ArrayManager().GetSubset(
                                        Validation.ObjectMother.Array01,
                                        ObjectMother.Array01_StartIndex01,
                                        ObjectMother.Array01_Length01);

            // Assert
            Assert.AreEqual(
                    ObjectMother.Array01_Subset01,
                    actual);

        }

        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 25.09.2022
*/
