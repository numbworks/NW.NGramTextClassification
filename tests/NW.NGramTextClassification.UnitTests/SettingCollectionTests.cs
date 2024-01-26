using System;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests
{
    [TestFixture]
    public class SettingCollectionTests
    {

        #region Fields

        private static TestCaseData[] settingCollectionExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new SettingCollection(
                                    truncateTextInLogMessagesAfter: SettingCollection.DefaultTruncateTextInLogMessagesAfter,
                                    minimumAccuracySingleLabel: SettingCollection.DefaultMinimumAccuracySingleLabel,
                                    minimumAccuracyMultipleLabels: SettingCollection.DefaultMinimumAccuracyMultipleLabels,
                                    folderPath: null
                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException("folderPath").Message
                ).SetArgDisplayNames($"{nameof(settingCollectionExceptionTestCases)}_01")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(settingCollectionExceptionTestCases))]
        public void SettingCollection_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void SettingCollection_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            SettingCollection actual1 = new SettingCollection();
            SettingCollection actual2 
                = new SettingCollection(
                        truncateTextInLogMessagesAfter: 10,
                        minimumAccuracySingleLabel: SettingCollection.DefaultMinimumAccuracySingleLabel,
                        minimumAccuracyMultipleLabels: SettingCollection.DefaultMinimumAccuracyMultipleLabels,
                        folderPath: SettingCollection.DefaultFolderPath
                        );

            // Assert
            Assert.IsInstanceOf<SettingCollection>(actual1);
            Assert.IsInstanceOf<SettingCollection>(actual2);

            Assert.IsInstanceOf<uint>(actual1.TruncateTextInLogMessagesAfter);
            Assert.IsInstanceOf<double>(actual1.MinimumAccuracySingleLabel);
            Assert.IsInstanceOf<double>(actual1.MinimumAccuracyMultipleLabels);
            Assert.IsInstanceOf<string>(actual1.FolderPath);

            Assert.IsInstanceOf<uint>(SettingCollection.DefaultTruncateTextInLogMessagesAfter);
            Assert.IsInstanceOf<double>(SettingCollection.DefaultMinimumAccuracySingleLabel);
            Assert.IsInstanceOf<double>(SettingCollection.DefaultMinimumAccuracyMultipleLabels);
            Assert.IsInstanceOf<string>(SettingCollection.DefaultFolderPath);

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
