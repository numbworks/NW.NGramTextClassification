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
                        () => new ArrayManager().AddDelimiter(null, ObjectMother.Delimiter1)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.VariableName_AddDelimiter_Arr).Message
                ).SetArgDisplayNames($"{nameof(addDelimiterExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new ArrayManager().AddDelimiter(Array.Empty<string>(), ObjectMother.Delimiter1)
                    ),
                typeof(ArgumentException),
                MessageCollection.VariableContainsZeroItems.Invoke(ObjectMother.VariableName_AddDelimiter_Arr)
                ).SetArgDisplayNames($"{nameof(addDelimiterExceptionTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                        () => new ArrayManager().AddDelimiter(ObjectMother.Array1, null)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.VariableName_AddDelimiter_Delimiter).Message
                ).SetArgDisplayNames($"{nameof(addDelimiterExceptionTestCases)}_03"),

            new TestCaseData(
                new TestDelegate(
                        () => new ArrayManager().AddDelimiter(ObjectMother.Array1, string.Empty)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.VariableName_AddDelimiter_Delimiter).Message
                ).SetArgDisplayNames($"{nameof(addDelimiterExceptionTestCases)}_04"),

        };
        private static TestCaseData[] getSubsetExceptionTestCases =
        {



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
