using System;
using System.Collections.Generic;
using NW.NGramTextClassification.LabeledExamples;
using NW.NGramTextClassification.Messages;
using NW.NGramTextClassification.NGramTokenization;
using NUnit.Framework;
using NW.NGramTextClassification.NGrams;

namespace NW.NGramTextClassification.UnitTests
{
    [TestFixture]
    public class LabeledExampleFactoryTests
    {

        #region Fields

        private static TestCaseData[] labeledExampleFactoryExceptionTestCases =
        {

            // ValidateObject
            new TestCaseData(
                new TestDelegate(
                        () => new LabeledExampleFactory(
                                            null,
                                            ObjectMother.LabeledExampleFactory_InitialId1
                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException("tokenizer").Message
                ).SetArgDisplayNames($"{nameof(labeledExampleFactoryExceptionTestCases)}_01")

        };
        private static TestCaseData[] tryCreateForRuleSetForTextExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new LabeledExampleFactory()
                                    .TryCreateForRuleSet(
                                        ObjectMother.Shared_Text1_LabeledExampleId,
                                        null,
                                        ObjectMother.Shared_Text1_Text,
                                        new NGramTokenizerRuleSet()
                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException("label").Message
                ).SetArgDisplayNames($"{nameof(tryCreateForRuleSetForTextExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new LabeledExampleFactory()
                                    .TryCreateForRuleSet(
                                        ObjectMother.Shared_Text1_LabeledExampleId,
                                        ObjectMother.Shared_Text1_Label,
                                        null,
                                        new NGramTokenizerRuleSet()
                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException("text").Message
                ).SetArgDisplayNames($"{nameof(tryCreateForRuleSetForTextExceptionTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                        () => new LabeledExampleFactory()
                                    .TryCreateForRuleSet(
                                        ObjectMother.Shared_Text1_LabeledExampleId,
                                        ObjectMother.Shared_Text1_Label,
                                        ObjectMother.Shared_Text1_Text,
                                        null
                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException("tokenizerRuleSet").Message
                ).SetArgDisplayNames($"{nameof(tryCreateForRuleSetForTextExceptionTestCases)}_03")


        };
        private static TestCaseData[] tryCreateForRuleSetForTuplesExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new LabeledExampleFactory()
                                    .TryCreateForRuleSet(
                                        ObjectMother.LabeledExampleFactory_Tuples,
                                        null
                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException("tokenizerRuleSet").Message
                ).SetArgDisplayNames($"{nameof(tryCreateForRuleSetForTuplesExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new LabeledExampleFactory()
                                    .TryCreateForRuleSet(
                                        null,
                                        new NGramTokenizerRuleSet()
                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException("tuples").Message
                ).SetArgDisplayNames($"{nameof(tryCreateForRuleSetForTuplesExceptionTestCases)}_02"),

        };
        private static TestCaseData[] tryCreateForRuleSetTestCases =
        {

            new TestCaseData(
                    ObjectMother.Shared_Text1_LabeledExampleId,
                    ObjectMother.Shared_Text1_Label,
                    ObjectMother.Shared_Text1_Text,
                    ObjectMother.Shared_NGramTokenizerRuleSet_Mono,
                    ObjectMother.Shared_Text1_LabeledExample_Mono
                ).SetArgDisplayNames($"{nameof(tryCreateForRuleSetTestCases)}_01"),

            new TestCaseData(
                    ObjectMother.Shared_Text1_LabeledExampleId,
                    ObjectMother.Shared_Text1_Label,
                    ObjectMother.Shared_Text1_Text,
                    ObjectMother.Shared_NGramTokenizerRuleSet_MonoBi,
                    ObjectMother.Shared_Text1_LabeledExample_MonoBi
                ).SetArgDisplayNames($"{nameof(tryCreateForRuleSetTestCases)}_02"),

            new TestCaseData(
                    ObjectMother.Shared_Text1_LabeledExampleId,
                    ObjectMother.Shared_Text1_Label,
                    ObjectMother.Shared_Text1_Text,
                    ObjectMother.Shared_NGramTokenizerRuleSet_MonoBiTri,
                    ObjectMother.Shared_Text1_LabeledExample_MonoBiTri
                ).SetArgDisplayNames($"{nameof(tryCreateForRuleSetTestCases)}_03"),

            new TestCaseData(
                    ObjectMother.Shared_Text1_LabeledExampleId,
                    ObjectMother.Shared_Text1_Label,
                    ObjectMother.Shared_Text1_Text,
                    ObjectMother.Shared_NGramTokenizerRuleSet_MonoBiTriFour,
                    ObjectMother.Shared_Text1_LabeledExample_MonoBiTriFour
                ).SetArgDisplayNames($"{nameof(tryCreateForRuleSetTestCases)}_04"),

            new TestCaseData(
                    ObjectMother.Shared_Text1_LabeledExampleId,
                    ObjectMother.Shared_Text1_Label,
                    ObjectMother.Shared_Text1_Text,
                    ObjectMother.Shared_NGramTokenizerRuleSet_MonoBiTriFourFive,
                    ObjectMother.Shared_Text1_LabeledExample_MonoBiTriFourFive
                ).SetArgDisplayNames($"{nameof(tryCreateForRuleSetTestCases)}_05")

        };
        private static TestCaseData[] tryCreateForRuleSetCollectionTestCases =
        {

            new TestCaseData(
                    ObjectMother.Shared_Tuples_Text1,
                    ObjectMother.Shared_NGramTokenizerRuleSet_Mono,
                    ObjectMother.Shared_LabeledExamples_Text1_Mono
                ).SetArgDisplayNames($"{nameof(tryCreateForRuleSetCollectionTestCases)}_01"),

            new TestCaseData(
                    ObjectMother.Shared_Tuples_Text1Text2,
                    ObjectMother.Shared_NGramTokenizerRuleSet_Mono,
                    ObjectMother.Shared_LabeledExamples_Text1Text2_Mono
                ).SetArgDisplayNames($"{nameof(tryCreateForRuleSetCollectionTestCases)}_02"),

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(labeledExampleFactoryExceptionTestCases))]
        public void LabeledExampleFactory_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(tryCreateForRuleSetForTextExceptionTestCases))]
        public void TryCreateForRuleSetForText_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(tryCreateForRuleSetForTuplesExceptionTestCases))]
        public void TryCreateForRuleSetForTuples_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void LabeledExampleFactory_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            LabeledExampleFactory actual = new LabeledExampleFactory();

            // Assert
            Assert.IsInstanceOf<LabeledExampleFactory>(actual);
            Assert.AreEqual(1, LabeledExampleFactory.DefaultInitialId);
            Assert.IsInstanceOf<NGramTokenizerRuleSet>(LabeledExampleFactory.DefaultTokenizerRuleSet);

        }

        [TestCaseSource(nameof(tryCreateForRuleSetTestCases))]
        public void TryCreateForRuleSet_ShouldReturnExpectedLabeledExample_WhenProperParameters
            (ulong id, string label, string text, INGramTokenizerRuleSet tokenizerRuleSet, LabeledExample expected)
        {


            // Arrange
            LabeledExampleFactory labeledExampleFactory = new LabeledExampleFactory();

            // Act
            LabeledExample actual = labeledExampleFactory.TryCreateForRuleSet(id, label, text, tokenizerRuleSet);

            // Assert
            Assert.IsTrue(
                    ObjectMother.AreEqual(expected, actual)
                );

        }

        [Test]
        public void TryCreateForRuleSet_ShouldReturnNullInsteadOfALabeledExample_WhenUnproperParameters()
        {

            // Arrange
            LabeledExampleFactory labeledExampleFactory = new LabeledExampleFactory();

            // Act
            LabeledExample actual 
                = labeledExampleFactory
                    .TryCreateForRuleSet(
                        id: ObjectMother.Shared_Text1_LabeledExampleId, 
                        label: ObjectMother.Shared_Text1_Label, 
                        text: ObjectMother.Shared_Text1_TextOnlyFirstWord, 
                        tokenizerRuleSet: ObjectMother.Shared_NGramTokenizerRuleSet_OnlyFive
                        );

            // Assert
            Assert.IsNull(actual);

        }

        [TestCaseSource(nameof(tryCreateForRuleSetCollectionTestCases))]
        public void TryCreateForRuleSet_ShouldReturnExpectedCollectionOfLabeledExamples_WhenProperParameters
            (List<(string label, string text)> tuples, INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> expected)
        {

            // Arrange
            LabeledExampleFactory labeledExampleFactory = new LabeledExampleFactory();

            // Act
            List<LabeledExample> actual = labeledExampleFactory.TryCreateForRuleSet(tuples, tokenizerRuleSet);

            // Assert
            Assert.IsTrue(
                    ObjectMother.AreEqual(expected, actual)
                );

        }

        [Test]
        public void TryCreateForRuleSet_ShouldReturnNullInsteadOfACollectionOfLabeledExamples_WhenUnproperParameters()
        {

            // Arrange
            LabeledExampleFactory labeledExampleFactory = new LabeledExampleFactory();

            // Act
            List<LabeledExample> actual 
                = labeledExampleFactory
                    .TryCreateForRuleSet(
                        tuples: ObjectMother.Shared_Tuples_TextOnlyFirstWord, 
                        tokenizerRuleSet: ObjectMother.Shared_NGramTokenizerRuleSet_OnlyFive
                        );

            // Assert
            Assert.IsNull(actual);

        }

        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 22.09.2021
*/