using System;
using System.Collections.Generic;
using NW.NGramTextClassification.LabeledExamples;
using NW.NGramTextClassification.NGrams;
using NW.NGramTextClassification.NGramTokenization;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests.LabeledExamples
{
    [TestFixture]
    public class LabeledExampleManagerTests
    {

        #region Fields
        private static TestCaseData[] labeledExampleManagerExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new LabeledExampleManager(null)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException("tokenizer").Message
                ).SetArgDisplayNames($"{nameof(labeledExampleManagerExceptionTestCases)}_01")

        };
        private static TestCaseData[] createOrDefaultExceptionTestCases =
        {

            // First method signature
            new TestCaseData(
                new TestDelegate(
                        () => new LabeledExampleManager()
                                    .CreateOrDefault(
                                        labeledExample: null,
                                        tokenizerRuleSet: new NGramTokenizerRuleSet()
                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException("labeledExample").Message
                ).SetArgDisplayNames($"{nameof(createOrDefaultExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new LabeledExampleManager()
                                    .CreateOrDefault(
                                        labeledExample: ObjectMother.ShortLabeledExample01,
                                        tokenizerRuleSet: null
                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException("tokenizerRuleSet").Message
                ).SetArgDisplayNames($"{nameof(createOrDefaultExceptionTestCases)}_02"),

            // Second method signature
            new TestCaseData(
                new TestDelegate(
                        () => new LabeledExampleManager()
                                    .CreateOrDefault(
                                        labeledExamples: null,
                                        tokenizerRuleSet: new NGramTokenizerRuleSet()
                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException("labeledExamples").Message
                ).SetArgDisplayNames($"{nameof(createOrDefaultExceptionTestCases)}_03"),

            new TestCaseData(
                new TestDelegate(
                        () => new LabeledExampleManager()
                                    .CreateOrDefault(
                                        labeledExamples: ObjectMother.ShortLabeledExamples,
                                        tokenizerRuleSet: null
                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException("tokenizerRuleSet").Message
                ).SetArgDisplayNames($"{nameof(createOrDefaultExceptionTestCases)}_04")

        };
        private static TestCaseData[] createOrDefaultTestCases =
        {

            // First method signature
            new TestCaseData(
                    ObjectMother.ShortLabeledExample01,
                    NGramTokenization.ObjectMother.NGramTokenizerRuleSet_Mono,
                    ObjectMother.ShortTokenizedExample01_Mono
                ).SetArgDisplayNames($"{nameof(createOrDefaultTestCases)}_01"),

            new TestCaseData(
                    ObjectMother.ShortLabeledExample01,
                    NGramTokenization.ObjectMother.NGramTokenizerRuleSet_MonoBi,
                    ObjectMother.ShortTokenizedExample01_MonoBi
                ).SetArgDisplayNames($"{nameof(createOrDefaultTestCases)}_02"),

            new TestCaseData(
                    ObjectMother.ShortLabeledExample01,
                    NGramTokenization.ObjectMother.NGramTokenizerRuleSet_MonoBiTri,
                    ObjectMother.ShortTokenizedExample01_MonoBiTri
                ).SetArgDisplayNames($"{nameof(createOrDefaultTestCases)}_03"),

            new TestCaseData(
                    ObjectMother.ShortLabeledExample01,
                    NGramTokenization.ObjectMother.NGramTokenizerRuleSet_MonoBiTriFour,
                    ObjectMother.ShortTokenizedExample01_MonoBiTriFour
                ).SetArgDisplayNames($"{nameof(createOrDefaultTestCases)}_04"),

            new TestCaseData(
                    ObjectMother.ShortLabeledExample01,
                    NGramTokenization.ObjectMother.NGramTokenizerRuleSet_MonoBiTriFourFive,
                    ObjectMother.ShortTokenizedExample01_MonoBiTriFourFive
                ).SetArgDisplayNames($"{nameof(createOrDefaultTestCases)}_05")

        };
        private static TestCaseData[] cleanLabeledExamplesExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new LabeledExampleManager()
                                    .CleanLabeledExamples(
                                        labeledExamples: null,
                                        tokenizerRuleSet: new NGramTokenizerRuleSet(),
                                        out _
                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException("labeledExamples").Message
                ).SetArgDisplayNames($"{nameof(cleanLabeledExamplesExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new LabeledExampleManager()
                                    .CleanLabeledExamples(
                                        labeledExamples: ObjectMother.ShortLabeledExamples,
                                        tokenizerRuleSet: null,
                                        out _
                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException("tokenizerRuleSet").Message
                ).SetArgDisplayNames($"{nameof(cleanLabeledExamplesExceptionTestCases)}_02")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests (LabeledExampleManager)

        [TestCaseSource(nameof(labeledExampleManagerExceptionTestCases))]
        public void LabeledExampleManager_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void LabeledExampleManager_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            LabeledExampleManager actual = new LabeledExampleManager(new NGramTokenizer());

            // Assert
            Assert.That(actual, Is.InstanceOf<LabeledExampleManager>());

            Assert.That(LabeledExampleManager.DefaultNGramTokenizer, Is.InstanceOf<NGramTokenizer>());
            Assert.That(LabeledExampleManager.DefaultTokenizerRuleSet, Is.InstanceOf<NGramTokenizerRuleSet>());
            Assert.That(LabeledExampleManager.DefaultTokenizedExample, Is.Null);
            Assert.That(LabeledExampleManager.DefaultTokenizedExamples, Is.Null);

        }

        [Test]
        public void LabeledExampleManager_ShouldCreateAnInstanceOfThisType_WhenProperArgumentAndDefaultConstructor()
        {

            // Arrange
            // Act
            LabeledExampleManager actual = new LabeledExampleManager();

            // Assert
            Assert.That(actual, Is.InstanceOf<LabeledExampleManager>());

        }

        #endregion

        #region Tests (CreateOrDefault)

        [TestCaseSource(nameof(createOrDefaultExceptionTestCases))]
        public void CreateOrDefault_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(createOrDefaultTestCases))]
        public void CreateOrDefault_ShouldReturnExpectedTokenizedExample_WhenProperParameters
            (LabeledExample labeledExample, INGramTokenizerRuleSet tokenizerRuleSet, TokenizedExample expected)
        {

            // Arrange
            LabeledExampleManager labeledExampleManager = new LabeledExampleManager();

            // Act
            TokenizedExample actual = labeledExampleManager.CreateOrDefault(labeledExample, tokenizerRuleSet);

            // Assert
            Assert.That(
                    ObjectMother.AreEqual(expected, actual),
                    Is.True
                );

        }

        [Test]
        public void CreateOrDefault_ShouldReturnExpectedTokenizedExample_WhenListLabeledExamplesAndProperParameters()
        {

            // Arrange
            LabeledExampleManager labeledExampleManager = new LabeledExampleManager();

            // Act
            List<TokenizedExample> actual 
                = labeledExampleManager.CreateOrDefault(ObjectMother.ShortLabeledExamples, LabeledExampleManager.DefaultTokenizerRuleSet);

            // Assert
            Assert.That(
                    ObjectMother.AreEqual(ObjectMother.ShortTokenizedExamples, actual),
                    Is.True
                );

        }

        [Test]
        public void CreateOrDefault_ShouldReturnDefault_WhenUnproperLabeledExample()
        {

            // Arrange
            LabeledExampleManager labeledExampleManager = new LabeledExampleManager();

            // Act
            TokenizedExample actual 
                = labeledExampleManager
                    .CreateOrDefault(
                        labeledExample: ObjectMother.ShortLabeledExample03_Untokenizable,
                        tokenizerRuleSet: NGramTokenization.ObjectMother.NGramTokenizerRuleSet_Five
                        );

            // Assert
            Assert.That(actual, Is.Null);

        }

        [Test]
        public void CreateOrDefault_ShouldReturnDefault_WhenUnproperLabeledExamples()
        {

            // Arrange
            LabeledExampleManager labeledExampleManager = new LabeledExampleManager();

            // Act
            List<TokenizedExample> actual
                = labeledExampleManager
                    .CreateOrDefault(
                        labeledExamples: ObjectMother.ShortLabeledExamples_Untokenizable,
                        tokenizerRuleSet: NGramTokenization.ObjectMother.NGramTokenizerRuleSet_Five
                        );

            // Assert
            Assert.That(actual, Is.Null);

        }

        [Test]
        public void CreateOrDefault_ShouldReturnExpectedTokenizedExample_WhenDefaultConstructor()
        {

            // Arrange
            LabeledExampleManager labeledExampleManager = new LabeledExampleManager();

            // Act
            TokenizedExample actual = labeledExampleManager.CreateOrDefault(labeledExample: ObjectMother.ShortLabeledExample01);

            // Assert
            Assert.That(
                    ObjectMother.AreEqual(ObjectMother.ShortTokenizedExample01, actual),
                    Is.True
                );

        }

        [Test]
        public void CreateOrDefault_ShouldReturnExpectedTokenizedExamples_WhenDefaultConstructor()
        {

            // Arrange
            LabeledExampleManager labeledExampleManager = new LabeledExampleManager();

            // Act
            List<TokenizedExample> actual = labeledExampleManager.CreateOrDefault(labeledExamples: ObjectMother.ShortLabeledExamples);

            // Assert
            Assert.That(
                    ObjectMother.AreEqual(ObjectMother.ShortTokenizedExamples, actual),
                    Is.True
                );

        }

        #endregion

        #region Tests (CreateOrNull)

        [Test]
        public void CreateOrNull_ShouldReturnNull_WhenExceptionIsThrown()
        {
            // Arrange
            LabeledExampleManager labeledExampleManager = new LabeledExampleManager(new FakeNGramTokenizer());

            // Act
            TokenizedExample actual 
                = labeledExampleManager
                    .CreateOrNull(
                        labeledExample: ObjectMother.ShortLabeledExample01,
                        tokenizerRuleSet: new NGramTokenizerRuleSet()
                        );

            // Assert
            Assert.That(actual, Is.Null);
        }

        #endregion

        #region Tests (CleanLabeledExamples)

        [TestCaseSource(nameof(cleanLabeledExamplesExceptionTestCases))]
        public void CleanLabeledExamples_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void CleanLabeledExamples_ShouldReturnExpectedCollections_WhenUntokenizableLabeledExamples()
        {

            // Arrange
            LabeledExampleManager labeledExampleManager = new LabeledExampleManager();
            List<LabeledExample> expected = new List<LabeledExample>();

            // Act
            List<LabeledExample> actualRemoved;
            List<LabeledExample> actual 
                = labeledExampleManager
                    .CleanLabeledExamples(
                        labeledExamples: ObjectMother.UntokenizableLabeledExamples,
                        tokenizerRuleSet: new NGramTokenizerRuleSet(),
                        out actualRemoved);

            // Assert
            Assert.That(
                    ObjectMother.AreEqual(ObjectMother.UntokenizableLabeledExamples, actualRemoved),
                    Is.True
                );
            Assert.That(
                    ObjectMother.AreEqual(expected, actual),
                    Is.True
                );

        }

        [Test]
        public void CleanLabeledExamples_ShouldReturnExpectedCollections_WhenAllValidExamples()
        {

            // Arrange
            LabeledExampleManager labeledExampleManager = new LabeledExampleManager();
            List<LabeledExample> expected = new List<LabeledExample>();

            // Act
            List<LabeledExample> actualRemoved;
            List<LabeledExample> actualClean 
                = labeledExampleManager
                    .CleanLabeledExamples(
                        labeledExamples: ObjectMother.ShortLabeledExamples,
                        tokenizerRuleSet: NGramTokenization.ObjectMother.NGramTokenizerRuleSet_Mono,
                        out actualRemoved);

            // Assert
            Assert.That(
                    ObjectMother.AreEqual(expected, actualRemoved),
                    Is.True
                );
            Assert.That(
                    ObjectMother.AreEqual(ObjectMother.ShortLabeledExamples, actualClean),
                    Is.True
                );

        }

        #endregion

        #region Tests (Create)

        [Test]
        public void Create_ShouldReturnExpectedTokenizedExample_WhenProperParameters()
        {
            // Arrange
            LabeledExampleManager labeledExampleManager = new LabeledExampleManager();

            // Act
            TokenizedExample actual = labeledExampleManager.Create(
                labeledExample: ObjectMother.ShortLabeledExample01,
                tokenizerRuleSet: NGramTokenization.ObjectMother.NGramTokenizerRuleSet_Mono
            );

            // Assert
            Assert.That(
                ObjectMother.AreEqual(ObjectMother.ShortTokenizedExample01_Mono, actual),
                Is.True
            );
        }

        [Test]
        public void Create_ShouldReturnExpectedTokenizedExample_WhenDefaultConstructor()
        {
            // Arrange
            LabeledExampleManager labeledExampleManager = new LabeledExampleManager();

            // Act
            TokenizedExample actual = labeledExampleManager.Create(
                labeledExample: ObjectMother.ShortLabeledExample01
            );

            // Assert
            Assert.That(
                ObjectMother.AreEqual(ObjectMother.ShortTokenizedExample01, actual),
                Is.True
            );
        }

        [Test]
        public void Create_ShouldReturnExpectedTokenizedExamples_WhenDefaultConstructorList()
        {
            // Arrange
            LabeledExampleManager labeledExampleManager = new LabeledExampleManager();

            // Act
            List<TokenizedExample> actual = labeledExampleManager.Create(
                labeledExamples: ObjectMother.ShortLabeledExamples
            );

            // Assert
            Assert.That(
                ObjectMother.AreEqual(ObjectMother.ShortTokenizedExamples, actual),
                Is.True
            );
        }

        #endregion

        #region TearDown
        #endregion

        #region Support Classes

        private class FakeNGramTokenizer : INGramTokenizer
        {

            public List<INGram> DoForRuleSet(string text, INGramTokenizerRuleSet ruleSet)
                => throw new Exception("Simulated exception");

            public List<INGram> DoForRuleSetOrDefault(string text, INGramTokenizerRuleSet ruleSet)
                => throw new Exception("Simulated exception");

            public List<Monogram> DoForMonogram(string text) => throw new NotImplementedException();
            public List<Bigram> DoForBigram(string text) => throw new NotImplementedException();
            public List<Trigram> DoForTrigram(string text) => throw new NotImplementedException();
            public List<Fourgram> DoForFourgram(string text) => throw new NotImplementedException();
            public List<Fivegram> DoForFivegram(string text) => throw new NotImplementedException();
            public ushort GetN<T>() where T : INGram => throw new NotImplementedException();

        }

        #endregion

    }
}