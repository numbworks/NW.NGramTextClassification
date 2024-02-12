using System;
using System.Collections.Generic;
using NW.NGramTextClassification.Bags;
using NW.NGramTextClassification.NGrams;
using NW.NGramTextClassification.Similarity;
using NW.Shared.Validation;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests.Similarity
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
                                        LabeledExamples.ObjectMother.ShortLabeledExample02_NGrams,
                                        ComponentBag.DefaultRoundingFunction
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("list1").Message
                ).SetArgDisplayNames($"{nameof(doExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new SimilarityIndexCalculatorJaccard()
                                    .Do(
                                        new List<INGram>(),
                                        LabeledExamples.ObjectMother.ShortLabeledExample02_NGrams,
                                        ComponentBag.DefaultRoundingFunction
                                )),
                typeof(ArgumentException),
                MessageCollection.VariableContainsZeroItems("list1")
                ).SetArgDisplayNames($"{nameof(doExceptionTestCases)}_02"),

            // ValidateList
            new TestCaseData(
                new TestDelegate(
                        () => new SimilarityIndexCalculatorJaccard()
                                    .Do(
                                        LabeledExamples.ObjectMother.ShortLabeledExample01_NGrams,
                                        null,
                                        ComponentBag.DefaultRoundingFunction
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("list2").Message
                ).SetArgDisplayNames($"{nameof(doExceptionTestCases)}_03"),

            new TestCaseData(
                new TestDelegate(
                        () => new SimilarityIndexCalculatorJaccard()
                                    .Do(
                                        LabeledExamples.ObjectMother.ShortLabeledExample01_NGrams,
                                        new List<INGram>(),
                                        ComponentBag.DefaultRoundingFunction
                                )),
                typeof(ArgumentException),
                MessageCollection.VariableContainsZeroItems("list2")
                ).SetArgDisplayNames($"{nameof(doExceptionTestCases)}_04"),

            // ValidateObject
            new TestCaseData(
                new TestDelegate(
                        () => new SimilarityIndexCalculatorJaccard()
                                    .Do(
                                        LabeledExamples.ObjectMother.ShortLabeledExample01_NGrams,
                                        LabeledExamples.ObjectMother.ShortLabeledExample02_NGrams,
                                        null
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("roundingFunction").Message
                ).SetArgDisplayNames($"{nameof(doExceptionTestCases)}_05")

        };
        private static TestCaseData[] doTestCases =
        {

            new TestCaseData(
                    LabeledExamples.ObjectMother.ShortLabeledExample01_NGrams,
                    LabeledExamples.ObjectMother.ShortLabeledExample01_NGrams,
                    1.00
                ).SetArgDisplayNames($"{nameof(doTestCases)}_01"),

            new TestCaseData(
                    LabeledExamples.ObjectMother.ShortLabeledExample01_NGrams,
                    LabeledExamples.ObjectMother.ShortLabeledExample02_NGrams,
                    0.00
                ).SetArgDisplayNames($"{nameof(doTestCases)}_02"),

            new TestCaseData(
                    LabeledExamples.ObjectMother.ShortLabeledExample01_NGrams.GetRange(0, 2),
                    LabeledExamples.ObjectMother.ShortLabeledExample01_NGrams.GetRange(0, 4),
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
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(doTestCases))]
        public void Do_ShouldReturnTheExpectedValue_WhenProperArguments
            (List<INGram> list1, List<INGram> list2, double expected)
        {

            // Arrange
            // Act
            double actual
                = new SimilarityIndexCalculatorJaccard()
                        .Do(list1, list2, ComponentBag.DefaultRoundingFunction);

            // Assert
            Assert.That(expected, Is.EqualTo(actual));

        }

        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 12.02.2024
*/