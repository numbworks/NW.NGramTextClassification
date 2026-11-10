using System;
using NW.NGramTextClassification.Bags;
using NW.NGramTextClassification.TextClassifications;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests.TextClassifications
{
    [TestFixture]
    public class TextClassifierSessionTests
    {

        #region Fields

        private static TestCaseData[] textClassifierSessionExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifierSession(
                                    minimumAccuracySingleLabel: SettingBag.DefaultMinimumAccuracySingleLabel,
                                    minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels,
                                    results: null,
                                    version: ComponentBag.DefaultVersionFunction()
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("results").Message
                ).SetArgDisplayNames($"{nameof(textClassifierSessionExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifierSession(
                                    minimumAccuracySingleLabel: SettingBag.DefaultMinimumAccuracySingleLabel,
                                    minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels,
                                    results: ObjectMother.TextClassifierResults_CompleteLabeledExamples00,
                                    version: null
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException("version").Message
                ).SetArgDisplayNames($"{nameof(textClassifierSessionExceptionTestCases)}_02")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(textClassifierSessionExceptionTestCases))]
        public void TextClassifierSession_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void TextClassifierSession_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            TextClassifierSession actual
                = new TextClassifierSession(
                        minimumAccuracySingleLabel: SettingBag.DefaultMinimumAccuracySingleLabel,
                        minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels,
                        results: ObjectMother.TextClassifierResults_CompleteLabeledExamples00,
                        version: ComponentBag.DefaultVersionFunction()
                    );

            // Assert
            Assert.That(actual, Is.InstanceOf<TextClassifierSession>());

        }

        #endregion

        #region TearDown
        #endregion

    }
}