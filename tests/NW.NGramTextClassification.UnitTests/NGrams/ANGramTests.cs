using System;
using NUnit.Framework;
using NW.NGramTextClassification.Messages;
using NW.NGramTextClassification.NGrams;
using NW.NGramTextClassification.NGramTokenization;

namespace NW.NGramTextClassification.UnitTests
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
                                    ObjectMother.ANGram_FakeGram1_Value
                            )),
                typeof(ArgumentException),
                MessageCollection.Validator_VariableCantBeLessThanOne.Invoke("n")
                ).SetArgDisplayNames($"{nameof(aNGramExceptionTestCases)}_01"),

            // ValidateObject
            new TestCaseData(
                new TestDelegate(
                        () => new FakeGram(
                                    1,
                                    null,
                                    ObjectMother.ANGram_FakeGram1_Value
                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException("stategy").Message
                ).SetArgDisplayNames($"{nameof(aNGramExceptionTestCases)}_02"),

            // Validator.ValidateStringNullOrWhiteSpace
            new TestCaseData(
                new TestDelegate(
                        () => new FakeGram(
                                    1,
                                    new TokenizationStrategy(),
                                    ObjectMother.ANGram_FakeGram_ValueOnlyWhiteSpaces
                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException("value").Message
                ).SetArgDisplayNames($"{nameof(aNGramExceptionTestCases)}_03")

        };
        private static TestCaseData[] equalityMethodsTestCases = {

            new TestCaseData(
                    ObjectMother.ANGram_FakeGram1,
                    ObjectMother.ANGram_FakeGram1,
                    true
                ).SetArgDisplayNames($"{nameof(equalityMethodsTestCases)}_01"),

            new TestCaseData(
                    ObjectMother.ANGram_FakeGram1,
                    new FakeGram(
                        ObjectMother.ANGram_FakeGram1_N,
                        new TokenizationStrategy(), // Tests if TokenizationStrategy.Equals() works as expected.
                        ObjectMother.ANGram_FakeGram1_Value),
                    true
                ).SetArgDisplayNames($"{nameof(equalityMethodsTestCases)}_02"),

            new TestCaseData(
                    ObjectMother.ANGram_FakeGram1,
                    new FakeGram(
                        ObjectMother.ANGram_FakeGram1_N,
                        ObjectMother.ANGram_TokenizationStrategyCustom,
                        ObjectMother.ANGram_FakeGram1_Value),
                    false
                ).SetArgDisplayNames($"{nameof(equalityMethodsTestCases)}_03"),

            new TestCaseData(
                    ObjectMother.ANGram_FakeGram1,
                    ObjectMother.ANGram_FakeGram2,
                    false
                ).SetArgDisplayNames($"{nameof(equalityMethodsTestCases)}_04"),

            new TestCaseData(
                    ObjectMother.ANGram_FakeGram1,
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
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

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
            bool actual = ObjectMother.ANGram_FakeGram1.Equals("some_string");

            // Assert
            Assert.IsFalse(actual);

        }

        [Test]
        public void EqualityOperator_ShouldReturnFalse_WhenLeftMemberIsNull()
        {

            // Arrange
            // Act
            bool actual = null == ObjectMother.ANGram_FakeGram1;

            // Assert
            Assert.IsFalse(actual);

        }

        [Test]
        public void GetHashCode_ShouldReturnExpectedHashCode_WhenInvoked()
        {

            // Arrange
            // Act
            int actual = ObjectMother.ANGram_FakeGram1.GetHashCode();

            // Assert
            Assert.AreEqual(ObjectMother.ANGram_FakeGram1_HashCode, actual);

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