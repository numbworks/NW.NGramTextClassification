using System;
using System.Collections.Generic;
using NW.NGramTextClassification.LabeledExamples;
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
                                        labeledExample: ObjectMother.LabeledExample01,
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
                                        labeledExamples: ObjectMother.LabeledExamples,
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
                    ObjectMother.LabeledExample01,
                    NGramTokenization.ObjectMother.NGramTokenizerRuleSet_Mono,
                    ObjectMother.TokenizedExample01_Mono
                ).SetArgDisplayNames($"{nameof(createOrDefaultTestCases)}_01"),

            new TestCaseData(
                    ObjectMother.LabeledExample01,
                    NGramTokenization.ObjectMother.NGramTokenizerRuleSet_MonoBi,
                    ObjectMother.TokenizedExample01_MonoBi
                ).SetArgDisplayNames($"{nameof(createOrDefaultTestCases)}_02"),

            new TestCaseData(
                    ObjectMother.LabeledExample01,
                    NGramTokenization.ObjectMother.NGramTokenizerRuleSet_MonoBiTri,
                    ObjectMother.TokenizedExample01_MonoBiTri
                ).SetArgDisplayNames($"{nameof(createOrDefaultTestCases)}_03"),

            new TestCaseData(
                    ObjectMother.LabeledExample01,
                    NGramTokenization.ObjectMother.NGramTokenizerRuleSet_MonoBiTriFour,
                    ObjectMother.TokenizedExample01_MonoBiTriFour
                ).SetArgDisplayNames($"{nameof(createOrDefaultTestCases)}_04"),

            new TestCaseData(
                    ObjectMother.LabeledExample01,
                    NGramTokenization.ObjectMother.NGramTokenizerRuleSet_MonoBiTriFourFive,
                    ObjectMother.TokenizedExample01_MonoBiTriFourFive
                ).SetArgDisplayNames($"{nameof(createOrDefaultTestCases)}_05")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(labeledExampleManagerExceptionTestCases))]
        public void LabeledExampleManager_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => UnitTests.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(createOrDefaultExceptionTestCases))]
        public void CreateOrDefault_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => UnitTests.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(createOrDefaultTestCases))]
        public void CreateOrDefault_ShouldReturnExpectedTokenizedExample_WhenProperParameters
            (LabeledExample labeledExample, INGramTokenizerRuleSet tokenizerRuleSet, TokenizedExample expected)
        {


            // Arrange
            LabeledExampleManager labeledExampleManager = new LabeledExampleManager();

            // Act
            TokenizedExample actual = labeledExampleManager.CreateOrDefault(labeledExample, tokenizerRuleSet);

            // Assert
            Assert.IsTrue(
                    ObjectMother.AreEqual(expected, actual)
                );

        }

        [Test]
        public void CreateOrDefault_ShouldReturnExpectedTokenizedExample_WhenListLabeledExamplesAndProperParameters()
        {

            // Arrange
            LabeledExampleManager labeledExampleManager = new LabeledExampleManager();

            // Act
            List<TokenizedExample> actual 
                = labeledExampleManager.CreateOrDefault(ObjectMother.LabeledExamples, LabeledExampleManager.DefaultTokenizerRuleSet);

            // Assert
            Assert.IsTrue(
                    ObjectMother.AreEqual(ObjectMother.TokenizedExamples, actual)
                );

        }

        [Test]
        public void LabeledExampleManager_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            LabeledExampleManager actual = new LabeledExampleManager(new NGramTokenizer());

            // Assert
            Assert.IsInstanceOf<LabeledExampleManager>(actual);

            Assert.IsInstanceOf<NGramTokenizer>(LabeledExampleManager.DefaultNGramTokenizer);
            Assert.IsInstanceOf<NGramTokenizerRuleSet>(LabeledExampleManager.DefaultTokenizerRuleSet);
            Assert.IsNull(LabeledExampleManager.DefaultTokenizedExample);
            Assert.IsNull(LabeledExampleManager.DefaultTokenizedExamples);

        }

        [Test]
        public void LabeledExampleManager_ShouldCreateAnInstanceOfThisType_WhenProperArgumentAndDefaultConstructor()
        {

            // Arrange
            // Act
            LabeledExampleManager actual = new LabeledExampleManager();

            // Assert
            Assert.IsInstanceOf<LabeledExampleManager>(actual);

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
                        labeledExample: ObjectMother.LabeledExample03_Untokenizable,
                        tokenizerRuleSet: NGramTokenization.ObjectMother.NGramTokenizerRuleSet_Five
                        );

            // Assert
            Assert.IsNull(actual);

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
                        labeledExamples: ObjectMother.LabeledExamples_Untokenizable,
                        tokenizerRuleSet: NGramTokenization.ObjectMother.NGramTokenizerRuleSet_Five
                        );

            // Assert
            Assert.IsNull(actual);

        }

        [Test]
        public void CreateOrDefault_ShouldReturnExpectedTokenizedExample_WhenDefaultConstructor()
        {

            // Arrange
            LabeledExampleManager labeledExampleManager = new LabeledExampleManager();

            // Act
            TokenizedExample actual = labeledExampleManager.CreateOrDefault(labeledExample: ObjectMother.LabeledExample01);

            // Assert
            Assert.IsTrue(
                    ObjectMother.AreEqual(ObjectMother.TokenizedExample01, actual)
                );

        }

        [Test]
        public void CreateOrDefault_ShouldReturnExpectedTokenizedExamples_WhenDefaultConstructor()
        {

            // Arrange
            LabeledExampleManager labeledExampleManager = new LabeledExampleManager();

            // Act
            List<TokenizedExample> actual = labeledExampleManager.CreateOrDefault(labeledExamples: ObjectMother.LabeledExamples);

            // Assert
            Assert.IsTrue(
                    ObjectMother.AreEqual(ObjectMother.TokenizedExamples, actual)
                );

        }

        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 25.09.2022
*/