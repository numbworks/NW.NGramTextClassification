using NW.NGramTextClassification.Bags;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests.Bags
{
    [TestFixture]
    public class SettingBagTests
    {

        #region Fields
        #endregion

        #region SetUp
        #endregion

        #region Tests

        [Test]
        public void SettingBag_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            SettingBag actual1 = new SettingBag();
            SettingBag actual2
                = new SettingBag(
                        folderPath: SettingBag.DefaultFolderPath,
                        minimumAccuracySingleLabel: SettingBag.DefaultMinimumAccuracySingleLabel,
                        minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels,
                        saveOutput: SettingBag.DefaultSaveOutput,
                        cleanLabeledExamples: SettingBag.DefaultCleanLabeledExamples,
                        disableIndexSerialization: SettingBag.DefaultDisableIndexSerialization,
                        enableUTC: SettingBag.DefaultEnableUTC
                        );

            // Assert
            Assert.That(actual1, Is.InstanceOf<SettingBag>());
            Assert.That(actual2, Is.InstanceOf<SettingBag>());

            Assert.That(actual1.FolderPath, Is.InstanceOf<string>());
            Assert.That(actual1.MinimumAccuracySingleLabel, Is.InstanceOf<double>());
            Assert.That(actual1.MinimumAccuracyMultipleLabels, Is.InstanceOf<double>());
            Assert.That(actual1.SaveOutput, Is.InstanceOf<bool>());
            Assert.That(actual1.CleanLabeledExamples, Is.InstanceOf<bool>());
            Assert.That(actual1.DisableIndexSerialization, Is.InstanceOf<bool>());
            Assert.That(actual1.EnableUTC, Is.InstanceOf<bool>());

            Assert.That(actual2.FolderPath, Is.InstanceOf<string>());
            Assert.That(actual2.MinimumAccuracySingleLabel, Is.InstanceOf<double>());
            Assert.That(actual2.MinimumAccuracyMultipleLabels, Is.InstanceOf<double>());
            Assert.That(actual2.SaveOutput, Is.InstanceOf<bool>());
            Assert.That(actual2.CleanLabeledExamples, Is.InstanceOf<bool>());
            Assert.That(actual2.DisableIndexSerialization, Is.InstanceOf<bool>());
            Assert.That(actual2.EnableUTC, Is.InstanceOf<bool>());

            Assert.That(SettingBag.DefaultMinimumAccuracySingleLabel, Is.InstanceOf<double>());
            Assert.That(SettingBag.DefaultMinimumAccuracyMultipleLabels, Is.InstanceOf<double>());
            Assert.That(SettingBag.DefaultFolderPath, Is.InstanceOf<string>());
            Assert.That(SettingBag.DefaultSaveOutput, Is.InstanceOf<bool>());
            Assert.That(SettingBag.DefaultCleanLabeledExamples, Is.InstanceOf<bool>());
            Assert.That(SettingBag.DefaultDisableIndexSerialization, Is.InstanceOf<bool>());
            Assert.That(SettingBag.DefaultEnableUTC, Is.InstanceOf<bool>());

        }

        #endregion

        #region TearDown
        #endregion

    }
}