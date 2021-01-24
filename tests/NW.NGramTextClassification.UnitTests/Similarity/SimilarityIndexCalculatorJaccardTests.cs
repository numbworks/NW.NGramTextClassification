using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests
{
    [TestFixture]
    public class SimilarityIndexCalculatorJaccardTests
    {

        // Fields
        private static TestCaseData[] doExceptionTestCases =
        {

            // ValidateList
            new TestCaseData(
                new TestDelegate(
                        () => new SimilarityIndexCalculatorJaccard()
                                    .Do(
                                        null,
                                        ObjectMother.SimilarityIndexCalculatorJaccard_List2,
                                        TextClassifierComponents.DefaultRoundingFunction
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.SimilarityIndexCalculatorJaccard_VariableName_List1).Message
                ).SetArgDisplayNames($"{nameof(doExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new SimilarityIndexCalculatorJaccard()
                                    .Do(
                                        new List<INGram>(),
                                        ObjectMother.SimilarityIndexCalculatorJaccard_List2,
                                        TextClassifierComponents.DefaultRoundingFunction
                                )),
                typeof(ArgumentException),
                MessageCollection.VariableContainsZeroItems.Invoke(ObjectMother.SimilarityIndexCalculatorJaccard_VariableName_List1)
                ).SetArgDisplayNames($"{nameof(doExceptionTestCases)}_02"),

            // ValidateList
            new TestCaseData(
                new TestDelegate(
                        () => new SimilarityIndexCalculatorJaccard()
                                    .Do(
                                        ObjectMother.SimilarityIndexCalculatorJaccard_List1,
                                        null,
                                        TextClassifierComponents.DefaultRoundingFunction
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.SimilarityIndexCalculatorJaccard_VariableName_List2).Message
                ).SetArgDisplayNames($"{nameof(doExceptionTestCases)}_03"),

            new TestCaseData(
                new TestDelegate(
                        () => new SimilarityIndexCalculatorJaccard()
                                    .Do(
                                        ObjectMother.SimilarityIndexCalculatorJaccard_List2,
                                        new List<INGram>(),
                                        TextClassifierComponents.DefaultRoundingFunction
                                )),
                typeof(ArgumentException),
                MessageCollection.VariableContainsZeroItems.Invoke(ObjectMother.SimilarityIndexCalculatorJaccard_VariableName_List2)
                ).SetArgDisplayNames($"{nameof(doExceptionTestCases)}_04"),

            // ValidateObject
            new TestCaseData(
                new TestDelegate(
                        () => new SimilarityIndexCalculatorJaccard()
                                    .Do(
                                        ObjectMother.SimilarityIndexCalculatorJaccard_List1,
                                        ObjectMother.SimilarityIndexCalculatorJaccard_List2,
                                        null
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.SimilarityIndexCalculatorJaccard_VariableName_RoundingFunction).Message
                ).SetArgDisplayNames($"{nameof(doExceptionTestCases)}_05")

        };
        private static TestCaseData[] doTestCases =
        {

            new TestCaseData(
                    ObjectMother.SimilarityIndexCalculatorJaccard_List1,
                    ObjectMother.SimilarityIndexCalculatorJaccard_List1,
                    1.00
                ),

            new TestCaseData(
                    ObjectMother.SimilarityIndexCalculatorJaccard_List1,
                    ObjectMother.SimilarityIndexCalculatorJaccard_List2,
                    0.00
                ),

            new TestCaseData(
                    ObjectMother.SimilarityIndexCalculatorJaccard_List1.GetRange(0, 2),
                    ObjectMother.SimilarityIndexCalculatorJaccard_List1.GetRange(0, 4),
                    0.50
                )

        };

        // SetUp
        // Tests
        [TestCaseSource(nameof(doExceptionTestCases))]
        public void Do_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(doTestCases))]
        public void Do_ShouldReturnTheExpectedValue_WhenProperArguments
            (List<INGram> list1, List<INGram> list2, double expected)
        {

            // Arrange
            // Act
            double actual 
                = new SimilarityIndexCalculatorJaccard()
                        .Do(list1, list2, TextClassifierComponents.DefaultRoundingFunction);

            // Assert
            Assert.AreEqual(expected, actual);

        }

        // TearDown
        // Support methods

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 24.01.2021

*/
