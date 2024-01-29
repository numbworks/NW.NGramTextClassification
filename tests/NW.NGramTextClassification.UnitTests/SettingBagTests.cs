using System;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests
{
    [TestFixture]
    public class SettingBagTests
    {

        #region Fields

        private static TestCaseData[] settingBagExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new SettingBag(
                                    truncateTextInLogMessagesAfter: SettingBag.DefaultTruncateTextInLogMessagesAfter,
                                    minimumAccuracySingleLabel: SettingBag.DefaultMinimumAccuracySingleLabel,
                                    minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels,
                                    folderPath: null
                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException("folderPath").Message
                ).SetArgDisplayNames($"{nameof(settingBagExceptionTestCases)}_01")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(settingBagExceptionTestCases))]
        public void SettingBag_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void SettingBag_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            SettingBag actual1 = new SettingBag();
            SettingBag actual2 
                = new SettingBag(
                        truncateTextInLogMessagesAfter: 10,
                        minimumAccuracySingleLabel: SettingBag.DefaultMinimumAccuracySingleLabel,
                        minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels,
                        folderPath: SettingBag.DefaultFolderPath
                        );

            // Assert
            Assert.IsInstanceOf<SettingBag>(actual1);
            Assert.IsInstanceOf<SettingBag>(actual2);

            Assert.IsInstanceOf<uint>(actual1.TruncateTextInLogMessagesAfter);
            Assert.IsInstanceOf<double>(actual1.MinimumAccuracySingleLabel);
            Assert.IsInstanceOf<double>(actual1.MinimumAccuracyMultipleLabels);
            Assert.IsInstanceOf<string>(actual1.FolderPath);

            Assert.IsInstanceOf<uint>(SettingBag.DefaultTruncateTextInLogMessagesAfter);
            Assert.IsInstanceOf<double>(SettingBag.DefaultMinimumAccuracySingleLabel);
            Assert.IsInstanceOf<double>(SettingBag.DefaultMinimumAccuracyMultipleLabels);
            Assert.IsInstanceOf<string>(SettingBag.DefaultFolderPath);

        }

        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 26.01.2024
*/
