using System;
using NUnit.Framework;
using NW.NGramTextClassification.Bags;

namespace NW.NGramTextClassification.UnitTests.Bags
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
            Assert.That(actual1, Is.InstanceOf<SettingBag>());
            Assert.That(actual2, Is.InstanceOf<SettingBag>());

            Assert.That(actual1.TruncateTextInLogMessagesAfter,Is.InstanceOf<uint>());
            Assert.That(actual1.MinimumAccuracySingleLabel,Is.InstanceOf<double>());
            Assert.That(actual1.MinimumAccuracyMultipleLabels,Is.InstanceOf<double>());
            Assert.That(actual1.FolderPath, Is.InstanceOf<string>());

            Assert.That(SettingBag.DefaultTruncateTextInLogMessagesAfter, Is.InstanceOf<uint>());
            Assert.That(SettingBag.DefaultMinimumAccuracySingleLabel, Is.InstanceOf<double>());
            Assert.That(SettingBag.DefaultMinimumAccuracyMultipleLabels, Is.InstanceOf<double>());
            Assert.That(SettingBag.DefaultFolderPath, Is.InstanceOf<string>());

        }

        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 30.01.2024
*/
