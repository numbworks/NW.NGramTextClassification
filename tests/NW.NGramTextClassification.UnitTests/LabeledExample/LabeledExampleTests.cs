using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace NW.NGramTextClassification.UnitTests
{
    [TestFixture]
    public class LabeledExampleTests
    {

        // Fields
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
                MessageCollection.VariableContainsZeroItems.Invoke(ObjectMother.LabeledExample_VariableName_TextAsNGrams)
                ).SetArgDisplayNames($"{nameof(labeledExampleExceptionTestCases)}_08"),


        };

        // SetUp
        // Tests
        [TestCaseSource(nameof(labeledExampleExceptionTestCases))]
        public void LabeledExample_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void ToString_ShouldTruncateTextInACustomWay_WhenTruncateAfterTextIsProvided()
        {

            // Arrange
            // Act
            string actual = ObjectMother.LabeledExample1.ToString(7);

            // Assert
            Assert.AreEqual(
                    ObjectMother.LabeledExample1_AsStringTruncatedAt7, 
                    actual);

        }

        [Test]
        public void ToString_ShouldTruncateTextInADefaultWay_WhenInvokedWithoutTruncateAfterText()
        {

            // Arrange
            // Act
            string actual 
                = ObjectMother.LabeledExample1.ToString();

            // Assert
            Assert.AreEqual(
                    ObjectMother.LabeledExample1_AsStringTruncatedAtDefault,
                    actual);

        }

        // TearDown
        // Support methods
        private void Method_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
        {

            // Arrange
            // Act
            // Assert
            Exception actual = Assert.Throws(expectedType, del);
            Assert.AreEqual(expectedMessage, actual.Message);


        }
    }

}

/*

    Author: rua@sitecore.net
    Last Update: 17.01.2021

*/
