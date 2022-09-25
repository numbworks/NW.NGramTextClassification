using System;
using NUnit.Framework;
using NW.NGramTextClassification.Messages;
using NW.NGramTextClassification.NGrams;
using NW.NGramTextClassification.NGramTokenization;

namespace NW.NGramTextClassification.UnitTests.NGrams
{
    [TestFixture]
    public class ANGramTests
    {

        #region Fields

        private static TestCaseData[] aNGramExceptionTestCases =
        {

            // ValidateN
            new TestCaseData(
                new TestDelegate(
                        () => new FakeGram(
                                    0,
                                    new TokenizationStrategy(),
                                    LabeledExamples.ObjectMother.LabeledExample01_Monograms[0].Value
                            )),
                typeof(ArgumentException),
                MessageCollection.Validator_VariableCantBeLessThanOne("n")
                ).SetArgDisplayNames($"{nameof(aNGramExceptionTestCases)}_01"),

            // ValidateObject
            new TestCaseData(
                new TestDelegate(
                        () => new FakeGram(
                                    1,
                                    null,
                                    LabeledExamples.ObjectMother.LabeledExample01_Monograms[0].Value
                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException("strategy").Message
                ).SetArgDisplayNames($"{nameof(aNGramExceptionTestCases)}_02"),

            // Validator.ValidateStringNullOrWhiteSpace
            new TestCaseData(
                new TestDelegate(
                        () => new FakeGram(
                                    1,
                                    new TokenizationStrategy(),
                                    UnitTests.ObjectMother.Validator_StringOnlyWhiteSpaces
                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException("value").Message
                ).SetArgDisplayNames($"{nameof(aNGramExceptionTestCases)}_03")

        };
        private static TestCaseData[] equalityMethodsTestCases = {

            new TestCaseData(
                    ObjectMother.FakeGram01,
                    ObjectMother.FakeGram01,
                    true
                ).SetArgDisplayNames($"{nameof(equalityMethodsTestCases)}_01"),

            new TestCaseData(
                    ObjectMother.FakeGram01,
                    new FakeGram(
                        ObjectMother.FakeGram01_N,
                        new TokenizationStrategy(), // Tests if TokenizationStrategy.Equals() works as expected.
                        LabeledExamples.ObjectMother.LabeledExample01_Monograms[0].Value),
                    true
                ).SetArgDisplayNames($"{nameof(equalityMethodsTestCases)}_02"),

            new TestCaseData(
                    ObjectMother.FakeGram01,
                    new FakeGram(
                        ObjectMother.FakeGram01_N,
                        UnitTests.ObjectMother.Shared_TokenizationStrategyCustom,
                        LabeledExamples.ObjectMother.LabeledExample01_Monograms[0].Value),
                    false
                ).SetArgDisplayNames($"{nameof(equalityMethodsTestCases)}_03"),

            new TestCaseData(
                    ObjectMother.FakeGram01,
                    ObjectMother.FakeGram02,
                    false
                ).SetArgDisplayNames($"{nameof(equalityMethodsTestCases)}_04"),

            new TestCaseData(
                    ObjectMother.FakeGram01,
                    null,
                    false
                ).SetArgDisplayNames($"{nameof(equalityMethodsTestCases)}_05")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(aNGramExceptionTestCases))]
        public void ANGram_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => UnitTests.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(equalityMethodsTestCases))]
        public void EqualsAndEqualityOperators_ShouldReturnTheExpectedBoolean_WhenInvoked
            (ANGram a, ANGram b, bool expected)
        {

            // Arrange
            // Act
            bool actual1 = a.Equals(b);
            bool actual2 = a == b;
            bool actual3 = a != b;

            // Assert
            Assert.AreEqual(expected, actual1);
            Assert.AreEqual(expected, actual2);
            Assert.AreNotEqual(expected, actual3);

        }

        [Test]
        public void Equals_ShouldReturnReturnFalse_WhenComparedWithAnObjectOfDifferentType()
        {

            // Arrange
            // Act
            bool actual = ObjectMother.FakeGram01.Equals("some_string");

            // Assert
            Assert.IsFalse(actual);

        }

        [Test]
        public void EqualityOperator_ShouldReturnFalse_WhenLeftMemberIsNull()
        {

            // Arrange
            // Act
            bool actual = null == ObjectMother.FakeGram01;

            // Assert
            Assert.IsFalse(actual);

        }

        [Test]
        public void GetHashCode_ShouldReturnExpectedHashCode_WhenInvoked()
        {

            // Arrange
            // Act
            int actual = ObjectMother.FakeGram01.GetHashCode();

            // Assert
            Assert.AreEqual(ObjectMother.FakeGram01_HashCode, actual);

        }

        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 25.09.2021
*/