using System;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests
{
    [TestFixture]
    public class TextClassifierSettingsTests
    {

        #region Fields

        private static TestCaseData[] textClassifierSettingsExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifierSettings(
                                    truncateTextInLogMessagesAfter: TextClassifierSettings.DefaultTruncateTextInLogMessagesAfter,
                                    minimumAccuracySingleLabel: TextClassifierSettings.DefaultMinimumAccuracySingleLabel,
                                    minimumAccuracyMultipleLabels: TextClassifierSettings.DefaultMinimumAccuracyMultipleLabels,
                                    folderPath: null
                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException("folderPath").Message
                ).SetArgDisplayNames($"{nameof(textClassifierSettingsExceptionTestCases)}_01")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(textClassifierSettingsExceptionTestCases))]
        public void TextClassifierSettings_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void TextClassifierSettings_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            TextClassifierSettings actual1 = new TextClassifierSettings();
            TextClassifierSettings actual2 
                = new TextClassifierSettings(
                        truncateTextInLogMessagesAfter: 10,
                        minimumAccuracySingleLabel: TextClassifierSettings.DefaultMinimumAccuracySingleLabel,
                        minimumAccuracyMultipleLabels: TextClassifierSettings.DefaultMinimumAccuracyMultipleLabels,
                        folderPath: TextClassifierSettings.DefaultFolderPath
                        );

            // Assert
            Assert.IsInstanceOf<TextClassifierSettings>(actual1);
            Assert.IsInstanceOf<TextClassifierSettings>(actual2);

            Assert.IsInstanceOf<uint>(actual1.TruncateTextInLogMessagesAfter);
            Assert.IsInstanceOf<double>(actual1.MinimumAccuracySingleLabel);
            Assert.IsInstanceOf<double>(actual1.MinimumAccuracyMultipleLabels);
            Assert.IsInstanceOf<string>(actual1.FolderPath);

            Assert.IsInstanceOf<uint>(TextClassifierSettings.DefaultTruncateTextInLogMessagesAfter);
            Assert.IsInstanceOf<double>(TextClassifierSettings.DefaultMinimumAccuracySingleLabel);
            Assert.IsInstanceOf<double>(TextClassifierSettings.DefaultMinimumAccuracyMultipleLabels);
            Assert.IsInstanceOf<string>(TextClassifierSettings.DefaultFolderPath);

        }

        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 22.10.2022
*/
