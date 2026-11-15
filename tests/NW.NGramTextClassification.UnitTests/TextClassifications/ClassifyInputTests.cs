using NW.NGramTextClassification.TextClassifications;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests.TextClassifications
{
    [TestFixture]
    public class ClassifyInputTests
    {

        #region Fields
        #endregion

        #region SetUp
        #endregion

        #region Tests

        [Test]
        public void ClassifyInput_ShouldCreateAnInstanceOfThisType_WhenProperArguments()
        {

            // Arrange
            string expectedLabeledExamplesFileName = "labeledExamples.json";
            string expectedTextSnippetsFileName = "textSnippets.json";
            string expectedTokenizerRuleSetFileName = "tokenizerRuleSet.json";

            // Act
            ClassifyInput actual
                = new ClassifyInput(
                        labeledExamplesFileName: expectedLabeledExamplesFileName,
                        textSnippetsFileName: expectedTextSnippetsFileName,
                        tokenizerRuleSetFileName: expectedTokenizerRuleSetFileName
                    );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(actual, Is.InstanceOf<ClassifyInput>());
                Assert.That(actual.LabeledExamplesFileName, Is.EqualTo(expectedLabeledExamplesFileName));
                Assert.That(actual.TextSnippetsFileName, Is.EqualTo(expectedTextSnippetsFileName));
                Assert.That(actual.TokenizerRuleSetFileName, Is.EqualTo(expectedTokenizerRuleSetFileName));
            });

        }

        [Test]
        public void ClassifyInput_ShouldCreateAnInstanceOfThisType_WhenOptionalArgumentsAreNotProvided()
        {

            // Arrange
            string expectedLabeledExamplesFileName = "labeledExamples.json";
            string expectedTextSnippetsFileName = "textSnippets.json";

            // Act
            ClassifyInput actual
                = new ClassifyInput(
                        labeledExamplesFileName: expectedLabeledExamplesFileName,
                        textSnippetsFileName: expectedTextSnippetsFileName
                    );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(actual, Is.InstanceOf<ClassifyInput>());
                Assert.That(actual.LabeledExamplesFileName, Is.EqualTo(expectedLabeledExamplesFileName));
                Assert.That(actual.TextSnippetsFileName, Is.EqualTo(expectedTextSnippetsFileName));
                Assert.That(actual.TokenizerRuleSetFileName, Is.EqualTo(ClassifyInput.DefaultTokenizerRuleSetFileName));
            });

        }

        #endregion

        #region TearDown
        #endregion

    }
}