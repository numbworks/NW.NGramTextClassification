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
                                                ObjectMother.LabeledExample_Id1,
                                                null,
                                                ObjectMother.LabeledExample_Text1,
                                                ObjectMother.LabeledExample_TextAsNGrams1
                                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.LabeledExample_VariableName_Label).Message
                ).SetArgDisplayNames($"{nameof(labeledExampleExceptionTestCases)}_01"),
            new TestCaseData(
                new TestDelegate( () => new LabeledExample(
                                                ObjectMother.LabeledExample_Id1,
                                                string.Empty,
                                                ObjectMother.LabeledExample_Text1,
                                                ObjectMother.LabeledExample_TextAsNGrams1
                                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.LabeledExample_VariableName_Label).Message
                ).SetArgDisplayNames($"{nameof(labeledExampleExceptionTestCases)}_02"),
            new TestCaseData(
                new TestDelegate( () => new LabeledExample(
                                                ObjectMother.LabeledExample_Id1,
                                                ObjectMother.LabeledExample_LabelOnlyWhiteSpaces,
                                                ObjectMother.LabeledExample_Text1,
                                                ObjectMother.LabeledExample_TextAsNGrams1
                                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.LabeledExample_VariableName_Label).Message
                ).SetArgDisplayNames($"{nameof(labeledExampleExceptionTestCases)}_03"),

            // ValidateStringNullOrWhiteSpace
            new TestCaseData(
                new TestDelegate( () => new LabeledExample(
                                                ObjectMother.LabeledExample_Id1,
                                                ObjectMother.LabeledExample_Label1,
                                                null,
                                                ObjectMother.LabeledExample_TextAsNGrams1
                                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.LabeledExample_VariableName_Text).Message
                ).SetArgDisplayNames($"{nameof(labeledExampleExceptionTestCases)}_04"),
            new TestCaseData(
                new TestDelegate( () => new LabeledExample(
                                                ObjectMother.LabeledExample_Id1,
                                                ObjectMother.LabeledExample_Label1,
                                                string.Empty,
                                                ObjectMother.LabeledExample_TextAsNGrams1
                                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.LabeledExample_VariableName_Text).Message
                ).SetArgDisplayNames($"{nameof(labeledExampleExceptionTestCases)}_05"),
            new TestCaseData(
                new TestDelegate( () => new LabeledExample(
                                                ObjectMother.LabeledExample_Id1,
                                                ObjectMother.LabeledExample_Label1,
                                                ObjectMother.LabeledExample_TextOnlyWhiteSpaces,
                                                ObjectMother.LabeledExample_TextAsNGrams1
                                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.LabeledExample_VariableName_Text).Message
                ).SetArgDisplayNames($"{nameof(labeledExampleExceptionTestCases)}_06"),

            // ValidateList
            new TestCaseData(
                new TestDelegate( () => new LabeledExample(
                                                ObjectMother.LabeledExample_Id1,
                                                ObjectMother.LabeledExample_Label1,
                                                ObjectMother.LabeledExample_Text1,
                                                null
                                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.LabeledExample_VariableName_TextAsNGrams).Message
                ).SetArgDisplayNames($"{nameof(labeledExampleExceptionTestCases)}_07"),
            new TestCaseData(
                new TestDelegate( () => new LabeledExample(
                                                ObjectMother.LabeledExample_Id1,
                                                ObjectMother.LabeledExample_Label1,
                                                ObjectMother.LabeledExample_Text1,
                                                new List<INGram>()
                                            )),
                typeof(ArgumentException),
                MessageCollection.Validator_VariableContainsZeroItems.Invoke(ObjectMother.LabeledExample_VariableName_TextAsNGrams)
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
                        ObjectMother.LabeledExample_Id1,
                        ObjectMother.LabeledExample_Label1,
                        ObjectMother.LabeledExample_Text1,
                        ObjectMother.LabeledExample_TextAsNGrams1
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
    Last Update: 17.09.2021
*/