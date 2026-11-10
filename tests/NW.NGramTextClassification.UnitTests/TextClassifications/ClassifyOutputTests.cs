using System;
using NW.NGramTextClassification.Bags;
using NW.NGramTextClassification.TextClassifications;
using NW.NGramTextClassification.TextClassifiers;
using NUnit.Framework;


namespace NW.NGramTextClassification.UnitTests.TextClassifications
{
    [TestFixture]
    public class ClassifyOutputTests
    {

        #region Fields

        private static TestCaseData[] classifyOutputExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new ClassifyOutput(
                                    minimumAccuracySingleLabel: SettingBag.DefaultMinimumAccuracySingleLabel,
                                    minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels,
                                    results: null,
                                    version: TextClassifier.DefaultVersionFunction()
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("results").Message
                ).SetArgDisplayNames($"{nameof(classifyOutputExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new ClassifyOutput(
                                    minimumAccuracySingleLabel: SettingBag.DefaultMinimumAccuracySingleLabel,
                                    minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels,
                                    results: TextClassifiers.ObjectMother.TextClassifierResults_CompleteLabeledExamples00,
                                    version: null
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("version").Message
                ).SetArgDisplayNames($"{nameof(classifyOutputExceptionTestCases)}_02")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(classifyOutputExceptionTestCases))]
        public void ClassifyOutput_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void ClassifyOutput_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            ClassifyOutput actual
                = new ClassifyOutput(
                        minimumAccuracySingleLabel: SettingBag.DefaultMinimumAccuracySingleLabel,
                        minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels,
                        results: TextClassifiers.ObjectMother.TextClassifierResults_CompleteLabeledExamples00,
                        version: TextClassifier.DefaultVersionFunction()
                    );

            // Assert
            Assert.That(actual, Is.InstanceOf<ClassifyOutput>());

        }

        #endregion

        #region TearDown
        #endregion

    }
}