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
                        saveSession: true                                        
                    );

            // Assert
            Assert.IsInstanceOf<ClassifyData>(actual);

            Assert.IsInstanceOf<string>(actual.LabeledExamples);
            Assert.IsInstanceOf<string>(actual.TextSnippets);
            Assert.IsInstanceOf<string>(actual.FolderPath);
            Assert.IsInstanceOf<string>(actual.TokenizerRuleSet);
            Assert.IsInstanceOf<double?>(actual.MinAccuracySingle);
            Assert.IsInstanceOf<double?>(actual.MinAccuracyMultiple);
            Assert.IsInstanceOf<bool>(actual.SaveSession);

        }

        #endregion

        #region TearDown

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 23.10.2022
*/
