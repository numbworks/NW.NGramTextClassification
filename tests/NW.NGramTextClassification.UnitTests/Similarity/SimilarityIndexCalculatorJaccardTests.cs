using System;
using System.Collections.Generic;
using NW.NGramTextClassification.Messages;
using NW.NGramTextClassification.NGrams;
using NW.NGramTextClassification.Similarity;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests
{
    [TestFixture]
    public class SimilarityIndexCalculatorJaccardTests
    {

        #region Fields

        private static TestCaseData[] doExceptionTestCases =
        {

            // ValidateList
            new TestCaseData(
                new TestDelegate(
                        () => new SimilarityIndexCalculatorJaccard()
                                    .Do(
                                        null,
                                        ObjectMother.Shared_Text2_TextAsNGrams,
                                        TextClassifierComponents.DefaultRoundingFunction
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("list1").Message
                ).SetArgDisplayNames($"{nameof(doExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new SimilarityIndexCalculatorJaccard()
                                    .Do(
                                        new List<INGram>(),
                                        ObjectMother.Shared_Text2_TextAsNGrams,
                                        TextClassifierComponents.DefaultRoundingFunction
                                )),
                typeof(ArgumentException),
                MessageCollection.Validator_VariableContainsZeroItems.Invoke("list1")
                ).SetArgDisplayNames($"{nameof(doExceptionTestCases)}_02"),

            // ValidateList
            new TestCaseData(
                new TestDelegate(
                        () => new SimilarityIndexCalculatorJaccard()
                                    .Do(
                                        ObjectMother.Shared_Text1_TextAsNGrams,
                                        null,
                                        TextClassifierComponents.DefaultRoundingFunction
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("list2").Message
                ).SetArgDisplayNames($"{nameof(doExceptionTestCases)}_03"),

            new TestCaseData(
                new TestDelegate(
                        () => new SimilarityIndexCalculatorJaccard()
                                    .Do(
                                        ObjectMother.Shared_Text1_TextAsNGrams,
                                        new List<INGram>(),
                                        TextClassifierComponents.DefaultRoundingFunction
                                )),
                typeof(ArgumentException),
                MessageCollection.Validator_VariableContainsZeroItems.Invoke("list2")
                ).SetArgDisplayNames($"{nameof(doExceptionTestCases)}_04"),

            // ValidateObject
            new TestCaseData(
                new TestDelegate(
                        () => new SimilarityIndexCalculatorJaccard()
                                    .Do(
                                        ObjectMother.Shared_Text1_TextAsNGrams,
                                        ObjectMother.Shared_Text2_TextAsNGrams,
                                        null
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("roundingFunction").Message
                ).SetArgDisplayNames($"{nameof(doExceptionTestCases)}_05")

        };
        private static TestCaseData[] doTestCases =
        {

            new TestCaseData(
                    ObjectMother.Shared_Text1_TextAsNGrams,
                    ObjectMother.Shared_Text1_TextAsNGrams,
                    1.00
                ).SetArgDisplayNames($"{nameof(doTestCases)}_01"),

            new TestCaseData(
                    ObjectMother.Shared_Text1_TextAsNGrams,
                    ObjectMother.Shared_Text2_TextAsNGrams,
                    0.00
                ).SetArgDisplayNames($"{nameof(doTestCases)}_02"),

            new TestCaseData(
                    ObjectMother.Shared_Text1_TextAsNGrams.GetRange(0, 2),
                    ObjectMother.Shared_Text1_TextAsNGrams.GetRange(0, 4),
                    0.50
                ).SetArgDisplayNames($"{nameof(doTestCases)}_03")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

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

        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 21.09.2021
*/