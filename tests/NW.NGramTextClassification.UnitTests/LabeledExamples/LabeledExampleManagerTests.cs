using System;
using System.Collections.Generic;
using NW.NGramTextClassification.LabeledExamples;
using NW.NGramTextClassification.NGramTokenization;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests
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
                                        labeledExample: ObjectMother.Shared_LabeledExample01,
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
                                        labeledExamples: ObjectMother.Shared_LabeledExamples,
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
                    ObjectMother.Shared_LabeledExample01,
                    ObjectMother.Shared_RuleSet_Mono,
                    ObjectMother.Shared_TokenizedExample01_Mono
                ).SetArgDisplayNames($"{nameof(createOrDefaultTestCases)}_01"),

            new TestCaseData(
                    ObjectMother.Shared_LabeledExample01,
                    ObjectMother.Shared_RuleSet_MonoBi,
                    ObjectMother.Shared_TokenizedExample01_MonoBi
                ).SetArgDisplayNames($"{nameof(createOrDefaultTestCases)}_02"),

            new TestCaseData(
                    ObjectMother.Shared_LabeledExample01,
                    ObjectMother.Shared_RuleSet_MonoBiTri,
                    ObjectMother.Shared_TokenizedExample01_MonoBiTri
                ).SetArgDisplayNames($"{nameof(createOrDefaultTestCases)}_03"),

            new TestCaseData(
                    ObjectMother.Shared_LabeledExample01,
                    ObjectMother.Shared_RuleSet_MonoBiTriFour,
                    ObjectMother.Shared_TokenizedExample01_MonoBiTriFour
                ).SetArgDisplayNames($"{nameof(createOrDefaultTestCases)}_04"),

            new TestCaseData(
                    ObjectMother.Shared_LabeledExample01,
                    ObjectMother.Shared_RuleSet_MonoBiTriFourFive,
                    ObjectMother.Shared_TokenizedExample01_MonoBiTriFourFive
                ).SetArgDisplayNames($"{nameof(createOrDefaultTestCases)}_05"),

            // Second method signature
            new TestCaseData(
                    ObjectMother.Shared_LabeledExamples,
                    LabeledExampleManager.DefaultTokenizerRuleSet,
                    ObjectMother.Shared_TokenizedExamples
                ).SetArgDisplayNames($"{nameof(createOrDefaultTestCases)}_06"),

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(labeledExampleManagerExceptionTestCases))]
        public void LabeledExampleManager_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(createOrDefaultExceptionTestCases))]
        public void CreateOrDefault_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

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
        public void LabeledExampleManager_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            LabeledExampleManager actual = new LabeledExampleManager();

            // Assert
            Assert.IsInstanceOf<LabeledExampleManager>(actual);
            Assert.IsInstanceOf<NGramTokenizer>(LabeledExampleManager.DefaultNGramTokenizer);
            Assert.IsInstanceOf<NGramTokenizerRuleSet>(LabeledExampleManager.DefaultTokenizerRuleSet);

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
                        labeledExample: ObjectMother.Shared_LabeledExample03_Untokenizable,
                        tokenizerRuleSet: ObjectMother.Shared_RuleSet_Five
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
                        labeledExamples: ObjectMother.Shared_LabeledExamples_Untokenizable,
                        tokenizerRuleSet: ObjectMother.Shared_RuleSet_Five
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
            TokenizedExample actual = labeledExampleManager.CreateOrDefault(labeledExample: ObjectMother.Shared_LabeledExample01);

            // Assert
            Assert.IsTrue(
                    ObjectMother.AreEqual(ObjectMother.Shared_TokenizedExample01, actual)
                );

        }

        [Test]
        public void CreateOrDefault_ShouldReturnExpectedTokenizedExamples_WhenDefaultConstructor()
        {

            // Arrange
            LabeledExampleManager labeledExampleManager = new LabeledExampleManager();

            // Act
            List<TokenizedExample> actual = labeledExampleManager.CreateOrDefault(labeledExamples: ObjectMother.Shared_LabeledExamples);

            // Assert
            Assert.IsTrue(
                    ObjectMother.AreEqual(ObjectMother.Shared_TokenizedExamples, actual)
                );

        }

        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 19.09.2022
*/