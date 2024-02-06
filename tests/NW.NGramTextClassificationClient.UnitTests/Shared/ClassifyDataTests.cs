using NW.NGramTextClassificationClient.Shared;
using NUnit.Framework;

namespace NW.NGramTextClassificationClient.UnitTests.Shared
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
                        folderPath: @"C:\ngramtc\",
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

/*
    Author: numbworks@gmail.com
    Last Update: 01.02.2024
*/
