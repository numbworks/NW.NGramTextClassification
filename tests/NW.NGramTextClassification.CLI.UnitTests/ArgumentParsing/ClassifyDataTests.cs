using NW.NGramTextClassification.CLI.ArgumentParsing;
using NUnit.Framework;

namespace NW.NGramTextClassification.CLI.UnitTests.ArgumentParsing
{
    [TestFixture]
    public class ClassifyDataTests
    {

        #region Fields

        #endregion

        #region SetUp

        #endregion

        #region Tests

        [Test]
        public void ClassifyData_ShouldCreateAnInstanceOfThisType_WhenInvoked()
        {

            // Arrange
            // Act
            ClassifyData actual
                = new ClassifyData(
                        labeledExamples: "LabeledExamples.json",
                        textSnippets: "TextSnippets.json",
                        folderPath: @"C:\nwngram\",
                        tokenizerRuleSet: "TokenizerRuleSet.json",
                        minAccuracySingle: 0.4,
                        minAccuracyMultiple: 0.7,
                        saveSession: true,
                        cleanLabeledExamples: true,
                        disableIndexSerialization: false
                    );

            // Assert
            Assert.That(actual, Is.InstanceOf<ClassifyData>());

            Assert.That(actual.LabeledExamples, Is.InstanceOf<string>());
            Assert.That(actual.TextSnippets, Is.InstanceOf<string>());
            Assert.That(actual.FolderPath, Is.InstanceOf<string>());
            Assert.That(actual.TokenizerRuleSet, Is.InstanceOf<string>());
            Assert.That(actual.MinAccuracySingle, Is.InstanceOf<double?>());
            Assert.That(actual.MinAccuracyMultiple, Is.InstanceOf<double?>());
            Assert.That(actual.SaveSession, Is.InstanceOf<bool>());
            Assert.That(actual.CleanLabeledExamples, Is.InstanceOf<bool>());
            Assert.That(actual.DisableIndexSerialization, Is.InstanceOf<bool>());

        }

        #endregion

        #region TearDown

        #endregion

    }
}
