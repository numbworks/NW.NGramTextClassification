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
                                                text: ObjectMother.ShortLabeledExample01.Text
                                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException("label").Message
                ).SetArgDisplayNames($"{nameof(labeledExampleExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate( () => new LabeledExample(
                                                label: ObjectMother.ShortLabeledExample01.Label,
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
                = ObjectMother.ShortLabeledExample01.ToString();

            // Assert
            Assert.That(
                    ObjectMother.ShortLabeledExample01_AsString,
                    Is.EqualTo(actual));

        }

        [Test]
        public void LabeledExample_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            LabeledExample actual
                = new LabeledExample(
                        label: ObjectMother.ShortLabeledExample01.Label,
                        text: ObjectMother.ShortLabeledExample01.Text
                    );

            // Assert
            Assert.That(actual, Is.InstanceOf<LabeledExample>());

        }

        #endregion

        #region TearDown
        #endregion

    }

}

/*
    Author: numbworks@gmail.com
    Last Update: 30.01.2024
*/