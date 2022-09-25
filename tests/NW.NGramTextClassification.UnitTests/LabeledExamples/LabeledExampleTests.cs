using System;
using NW.NGramTextClassification.LabeledExamples;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests.LabeledExamples
{
    [TestFixture]
    public class LabeledExampleTests
    {

        #region Fields

        private static TestCaseData[] labeledExampleExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate( () => new LabeledExample(
                                                label: null,
                                                text: ObjectMother.LabeledExample01.Text
                                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException("label").Message
                ).SetArgDisplayNames($"{nameof(labeledExampleExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate( () => new LabeledExample(
                                                label: ObjectMother.LabeledExample01.Label,
                                                text: null
                                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException("text").Message
                ).SetArgDisplayNames($"{nameof(labeledExampleExceptionTestCases)}_02")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(labeledExampleExceptionTestCases))]
        public void LabeledExample_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void ToString_ShouldReturnExpectedString_WhenInvoked()
        {

            // Arrange
            // Act
            string actual
                = ObjectMother.LabeledExample01.ToString();

            // Assert
            Assert.AreEqual(
                    ObjectMother.LabeledExample01_AsString,
                    actual);

        }

        [Test]
        public void LabeledExample_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            LabeledExample actual
                = new LabeledExample(
                        label: ObjectMother.LabeledExample01.Label,
                        text: ObjectMother.LabeledExample01.Text
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
    Last Update: 25.09.2022
*/