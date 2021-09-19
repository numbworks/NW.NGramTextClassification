using System;
using System.Collections.Generic;
using NW.NGramTextClassification.LabeledExamples;
using NW.NGramTextClassification.Messages;
using NW.NGramTextClassification.NGramTokenization;
using NUnit.Framework;

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
                                        ObjectMother.LabeledExampleFactory_Id1,
                                        null,
                                        ObjectMother.LabeledExampleFactory_Text1,
                                        new NGramTokenizerRuleSet()
                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException("label").Message
                ).SetArgDisplayNames($"{nameof(tryCreateForRuleSetForTextExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new LabeledExampleFactory()
                                    .TryCreateForRuleSet(
                                        ObjectMother.LabeledExampleFactory_Id1,
                                        ObjectMother.LabeledExampleFactory_Label1,
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
                                        ObjectMother.LabeledExampleFactory_Id1,
                                        ObjectMother.LabeledExampleFactory_Label1,
                                        ObjectMother.LabeledExampleFactory_Text1,
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

        [Test]
        public void TryCreateForRuleSet_ShouldReturnExpectedLabeledExample_WhenInvoked()
        {


            // Arrange
            // Act
            LabeledExample actual = new LabeledExampleFactory.TryCreateForRuleSet();


            // Assert

        }


        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 19.09.2021
*/