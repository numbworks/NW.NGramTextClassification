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
                    new NGramTokenizerRuleSet(true, false, false, false, false),
                    new LabeledExample(
                            ObjectMother.Shared_Text1_LabeledExampleId,
                            ObjectMother.Shared_Text1_Label,
                            ObjectMother.Shared_Text1_Text,
                            ObjectMother.CreateNGrams(
                                ObjectMother.Shared_Text1_TextAsMonograms
                                )
                        )
                ).SetArgDisplayNames($"{nameof(tryCreateForRuleSetTestCases)}_01"),

            new TestCaseData(
                    ObjectMother.Shared_Text1_LabeledExampleId,
                    ObjectMother.Shared_Text1_Label,
                    ObjectMother.Shared_Text1_Text,
                    new NGramTokenizerRuleSet(true, true, false, false, false),
                    new LabeledExample(
                            ObjectMother.Shared_Text1_LabeledExampleId,
                            ObjectMother.Shared_Text1_Label,
                            ObjectMother.Shared_Text1_Text,
                            ObjectMother.CreateNGrams(
                                ObjectMother.Shared_Text1_TextAsMonograms,
                                ObjectMother.Shared_Text1_TextAsBigrams
                                )
                        )
                ).SetArgDisplayNames($"{nameof(tryCreateForRuleSetTestCases)}_02"),

            new TestCaseData(
                    ObjectMother.Shared_Text1_LabeledExampleId,
                    ObjectMother.Shared_Text1_Label,
                    ObjectMother.Shared_Text1_Text,
                    new NGramTokenizerRuleSet(true, true, true, false, false),
                    new LabeledExample(
                            ObjectMother.Shared_Text1_LabeledExampleId,
                            ObjectMother.Shared_Text1_Label,
                            ObjectMother.Shared_Text1_Text,
                            ObjectMother.CreateNGrams(
                                ObjectMother.Shared_Text1_TextAsMonograms,
                                ObjectMother.Shared_Text1_TextAsBigrams,
                                ObjectMother.Shared_Text1_TextAsTrigrams
                                )
                        )
                ).SetArgDisplayNames($"{nameof(tryCreateForRuleSetTestCases)}_03"),

            new TestCaseData(
                    ObjectMother.Shared_Text1_LabeledExampleId,
                    ObjectMother.Shared_Text1_Label,
                    ObjectMother.Shared_Text1_Text,
                    new NGramTokenizerRuleSet(true, true, true, true, false),
                    new LabeledExample(
                            ObjectMother.Shared_Text1_LabeledExampleId,
                            ObjectMother.Shared_Text1_Label,
                            ObjectMother.Shared_Text1_Text,
                            ObjectMother.CreateNGrams(
                                ObjectMother.Shared_Text1_TextAsMonograms,
                                ObjectMother.Shared_Text1_TextAsBigrams,
                                ObjectMother.Shared_Text1_TextAsTrigrams,
                                ObjectMother.Shared_Text1_TextAsFourgrams
                                )
                        )
                ).SetArgDisplayNames($"{nameof(tryCreateForRuleSetTestCases)}_04"),

            new TestCaseData(
                    ObjectMother.Shared_Text1_LabeledExampleId,
                    ObjectMother.Shared_Text1_Label,
                    ObjectMother.Shared_Text1_Text,
                    new NGramTokenizerRuleSet(true, true, true, true, true),
                    new LabeledExample(
                            ObjectMother.Shared_Text1_LabeledExampleId,
                            ObjectMother.Shared_Text1_Label,
                            ObjectMother.Shared_Text1_Text,
                            ObjectMother.CreateNGrams(
                                ObjectMother.Shared_Text1_TextAsMonograms,
                                ObjectMother.Shared_Text1_TextAsBigrams,
                                ObjectMother.Shared_Text1_TextAsTrigrams,
                                ObjectMother.Shared_Text1_TextAsFourgrams,
                                ObjectMother.Shared_Text1_TextAsFivegrams
                                )
                        )
                ).SetArgDisplayNames($"{nameof(tryCreateForRuleSetTestCases)}_05")

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
        public void TryCreateForRuleSet_ShouldReturnNull_WhenUnproperParameters()
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
                        tokenizerRuleSet: new NGramTokenizerRuleSet(false, false, false, false, false)
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