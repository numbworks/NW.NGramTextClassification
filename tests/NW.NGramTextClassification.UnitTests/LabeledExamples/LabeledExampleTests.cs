using System;
using System.Collections.Generic;
using NW.NGramTextClassification.LabeledExamples;
using NW.NGramTextClassification.Messages;
using NW.NGramTextClassification.NGrams;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests
{
    [TestFixture]
    public class LabeledExampleTests
    {

        #region Fields

        private static TestCaseData[] labeledExampleExceptionTestCases =
        {

            // ValidateStringNullOrWhiteSpace
            new TestCaseData(
                new TestDelegate( () => new LabeledExample(
                                                ObjectMother.Shared_Text1_LabeledExampleId,
                                                null,
                                                ObjectMother.Shared_Text1_Text,
                                                ObjectMother.Shared_Text1_TextAsNGrams
                                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException("label").Message
                ).SetArgDisplayNames($"{nameof(labeledExampleExceptionTestCases)}_01"),
            new TestCaseData(
                new TestDelegate( () => new LabeledExample(
                                                ObjectMother.Shared_Text1_LabeledExampleId,
                                                string.Empty,
                                                ObjectMother.Shared_Text1_Text,
                                                ObjectMother.Shared_Text1_TextAsNGrams
                                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException("label").Message
                ).SetArgDisplayNames($"{nameof(labeledExampleExceptionTestCases)}_02"),
            new TestCaseData(
                new TestDelegate( () => new LabeledExample(
                                                ObjectMother.Shared_Text1_LabeledExampleId,
                                                ObjectMother.Validator_StringOnlyWhiteSpaces,
                                                ObjectMother.Shared_Text1_Text,
                                                ObjectMother.Shared_Text1_TextAsNGrams
                                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException("label").Message
                ).SetArgDisplayNames($"{nameof(labeledExampleExceptionTestCases)}_03"),

            // ValidateStringNullOrWhiteSpace
            new TestCaseData(
                new TestDelegate( () => new LabeledExample(
                                                ObjectMother.Shared_Text1_LabeledExampleId,
                                                ObjectMother.Shared_Text1_Label,
                                                null,
                                                ObjectMother.Shared_Text1_TextAsNGrams
                                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException("text").Message
                ).SetArgDisplayNames($"{nameof(labeledExampleExceptionTestCases)}_04"),
            new TestCaseData(
                new TestDelegate( () => new LabeledExample(
                                                ObjectMother.Shared_Text1_LabeledExampleId,
                                                ObjectMother.Shared_Text1_Label,
                                                string.Empty,
                                                ObjectMother.Shared_Text1_TextAsNGrams
                                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException("text").Message
                ).SetArgDisplayNames($"{nameof(labeledExampleExceptionTestCases)}_05"),
            new TestCaseData(
                new TestDelegate( () => new LabeledExample(
                                                ObjectMother.Shared_Text1_LabeledExampleId,
                                                ObjectMother.Shared_Text1_Label,
                                                ObjectMother.Validator_StringOnlyWhiteSpaces,
                                                ObjectMother.Shared_Text1_TextAsNGrams
                                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException("text").Message
                ).SetArgDisplayNames($"{nameof(labeledExampleExceptionTestCases)}_06"),

            // ValidateList
            new TestCaseData(
                new TestDelegate( () => new LabeledExample(
                                                ObjectMother.Shared_Text1_LabeledExampleId,
                                                ObjectMother.Shared_Text1_Label,
                                                ObjectMother.Shared_Text1_Text,
                                                null
                                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException("textAsNGrams").Message
                ).SetArgDisplayNames($"{nameof(labeledExampleExceptionTestCases)}_07"),
            new TestCaseData(
                new TestDelegate( () => new LabeledExample(
                                                ObjectMother.Shared_Text1_LabeledExampleId,
                                                ObjectMother.Shared_Text1_Label,
                                                ObjectMother.Shared_Text1_Text,
                                                new List<INGram>()
                                            )),
                typeof(ArgumentException),
                MessageCollection.Validator_VariableContainsZeroItems.Invoke("textAsNGrams")
                ).SetArgDisplayNames($"{nameof(labeledExampleExceptionTestCases)}_08"),

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(labeledExampleExceptionTestCases))]
        public void LabeledExample_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void ToString_ShouldReturnExpectedString_WhenInvoked()
        {

            // Arrange
            // Act
            string actual
                = ObjectMother.LabeledExample1.ToString();

            // Assert
            Assert.AreEqual(
                    ObjectMother.LabeledExample1_AsString,
                    actual);

        }

        [Test]
        public void LabeledExample_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            LabeledExample actual
                = new LabeledExample(
                        ObjectMother.Shared_Text1_LabeledExampleId,
                        ObjectMother.Shared_Text1_Label,
                        ObjectMother.Shared_Text1_Text,
                        ObjectMother.Shared_Text1_TextAsNGrams
                    );

            // Assert
            Assert.IsInstanceOf<LabeledExample>(actual);

        }

        #endregion

        #region TearDown
        #endregion

    }

}

/*
    Author: numbworks@gmail.com
    Last Update: 20.09.2021
*/