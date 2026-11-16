using System;
using NW.NGramTextClassification.Bags;
using NW.NGramTextClassification.CLI.ArgumentParsing;
using NUnit.Framework;

namespace NW.NGramTextClassification.CLI.UnitTests.ArgumentParsing
{
    [TestFixture]
    public class TextClassificationManagerFactoryTests
    {

        #region Fields

        private static TestCaseData[] createTestCases =
        {

            new TestCaseData(
                new SettingBag(
                    folderPath: @"C:\",
                    minimumAccuracySingleLabel: SettingBag.DefaultMinimumAccuracySingleLabel,
                    minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels,
                    saveOutput: SettingBag.DefaultSaveOutput,
                    cleanLabeledExamples: SettingBag.DefaultCleanLabeledExamples,
                    disableIndexSerialization: SettingBag.DefaultDisableIndexSerialization,
                    enableUTC: false
                ),
                ComponentBag.DefaultLoggingAction
            ).SetArgDisplayNames($"{nameof(createTestCases)}_01"),

            new TestCaseData(
                new SettingBag(
                    folderPath: @"C:\",
                    minimumAccuracySingleLabel: SettingBag.DefaultMinimumAccuracySingleLabel,
                    minimumAccuracyMultipleLabels: SettingBag.DefaultMinimumAccuracyMultipleLabels,
                    saveOutput: SettingBag.DefaultSaveOutput,
                    cleanLabeledExamples: SettingBag.DefaultCleanLabeledExamples,
                    disableIndexSerialization: SettingBag.DefaultDisableIndexSerialization,
                    enableUTC: true
                ),
                ComponentBag.DefaultUTCLoggingAction
            ).SetArgDisplayNames($"{nameof(createTestCases)}_02")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [Test]
        public void TextClassificationManagerFactory_ShouldCreateAnInstanceOfThisType_WhenInvoked()
        {

            // Arrange
            // Act
            TextClassificationManagerFactory actual = new TextClassificationManagerFactory();

            // Assert
            Assert.That(actual, Is.InstanceOf<TextClassificationManagerFactory>());
            Assert.That(TextClassificationManagerFactory.DefaultVersionFunction, Is.InstanceOf<Func<string>>());
            Assert.That(TextClassificationManagerFactory.DefaultErrorFormattingFunction, Is.InstanceOf<Func<string, string>>());

        }

        [TestCaseSource(nameof(createTestCases))]
        public void Create_ShouldReturnTextClassificationManagerAndLoggingAction_WhenSettingBagProvided
            (SettingBag settingBag, Action<string> expectedLoggingAction)
        {

            // Arrange
            TextClassificationManagerFactory textClassificationManagerFactory = new TextClassificationManagerFactory();

            // Act
            ITextClassificationManager actual = textClassificationManagerFactory.Create(settingBag, out Action<string> actualLoggingAction);

            // Assert
            Assert.That(actual, Is.InstanceOf<TextClassificationManager>());
            Assert.That(actualLoggingAction, Is.EqualTo(expectedLoggingAction));

        }

        #endregion

        #region TearDown
        #endregion

        #region SupportMethods
        #endregion

    }
}