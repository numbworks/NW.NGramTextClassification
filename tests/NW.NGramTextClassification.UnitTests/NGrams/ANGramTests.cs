using System;
using NUnit.Framework;
using NW.NGramTextClassification.NGrams;
using NW.NGramTextClassification.NGramTokenization;
using NW.NGramTextClassification.UnitTests.Utilities;
using NW.Shared.Validation;

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
                                    LabeledExamples.ObjectMother.ShortLabeledExample01_Monograms[0].Value
                            )),
                typeof(ArgumentException),
                Shared.Validation.MessageCollection.VariableCantBeLessThan("n", 1)
                ).SetArgDisplayNames($"{nameof(aNGramExceptionTestCases)}_01"),

            // ValidateObject
            new TestCaseData(
                new TestDelegate(
                        () => new FakeGram(
                                    1,
                                    null,
                                    LabeledExamples.ObjectMother.ShortLabeledExample01_Monograms[0].Value
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
                                    Utilities.ObjectMother.StringOnlyWhiteSpaces
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
                        LabeledExamples.ObjectMother.ShortLabeledExample01_Monograms[0].Value),
                    true
                ).SetArgDisplayNames($"{nameof(equalityMethodsTestCases)}_02"),

            new TestCaseData(
                    ObjectMother.FakeGram01,
                    new FakeGram(
                        ObjectMother.FakeGram01_N,
                        NGramTokenization.ObjectMother.TokenizationStrategy_LettersSemicolon,
                        LabeledExamples.ObjectMother.ShortLabeledExample01_Monograms[0].Value),
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
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

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
            Assert.That(expected, Is.EqualTo(actual1));
            Assert.That(expected, Is.EqualTo(actual2));
            Assert.That(expected, Is.Not.EqualTo(actual3));

        }

        [Test]
        public void Equals_ShouldReturnReturnFalse_WhenComparedWithAnObjectOfDifferentType()
        {

            // Arrange
            // Act
            bool actual = ObjectMother.FakeGram01.Equals("some_string");

            // Assert
            Assert.That(actual, Is.False);

        }

        [Test]
        public void EqualityOperator_ShouldReturnFalse_WhenLeftMemberIsNull()
        {

            // Arrange
            // Act
            bool actual = null == ObjectMother.FakeGram01;

            // Assert
            Assert.That(actual, Is.False);

        }

        [Test]
        public void GetHashCode_ShouldReturnExpectedHashCode_WhenInvoked()
        {

            // Arrange
            // Act
            int actual = ObjectMother.FakeGram01.GetHashCode();

            // Assert
            Assert.That(ObjectMother.FakeGram01_HashCode, Is.EqualTo(actual));

        }

        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 12.02.2024
*/