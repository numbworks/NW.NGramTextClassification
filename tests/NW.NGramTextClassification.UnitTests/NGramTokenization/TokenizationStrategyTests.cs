using System;
using NUnit.Framework;
using NW.NGramTextClassification.NGramTokenization;

namespace NW.NGramTextClassification.UnitTests.NGramTokenization
{
    [TestFixture]
    public class TokenizationStrategyTests
    {

        #region Fields

        private static TestCaseData[] tokenizationStrategyExceptionTestCases =
        {

            // ValidateStringNullOrWhiteSpace
            new TestCaseData(
                new TestDelegate(
                        () => new TokenizationStrategy(
                                    null,
                                    TokenizationStrategy.DefaultDelimiter,
                                    TokenizationStrategy.DefaultToLowercase
                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException("pattern").Message
                ).SetArgDisplayNames($"{nameof(tokenizationStrategyExceptionTestCases)}_01"),
            new TestCaseData(
                new TestDelegate(
                        () => new TokenizationStrategy(
                                    "  ",
                                    TokenizationStrategy.DefaultDelimiter,
                                    TokenizationStrategy.DefaultToLowercase
                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException("pattern").Message
                ).SetArgDisplayNames($"{nameof(tokenizationStrategyExceptionTestCases)}_02"),

            // ValidateStringNullOrEmpty
            new TestCaseData(
                new TestDelegate(
                        () => new TokenizationStrategy(
                                    TokenizationStrategy.DefaultPattern,
                                    null,
                                    TokenizationStrategy.DefaultToLowercase
                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException("delimiter").Message
                ).SetArgDisplayNames($"{nameof(tokenizationStrategyExceptionTestCases)}_03"),
            new TestCaseData(
                new TestDelegate(
                        () => new TokenizationStrategy(
                                    TokenizationStrategy.DefaultPattern,
                                    string.Empty,
                                    TokenizationStrategy.DefaultToLowercase
                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException("delimiter").Message
                ).SetArgDisplayNames($"{nameof(tokenizationStrategyExceptionTestCases)}_04"),

        };
        private static TestCaseData[] equalityMethodsTestCases = {

            new TestCaseData(
                    ObjectMother.TokenizationStrategy_Default,
                    ObjectMother.TokenizationStrategy_Default,
                    true
                ).SetArgDisplayNames($"{nameof(equalityMethodsTestCases)}_01"),

            new TestCaseData(
                    ObjectMother.TokenizationStrategy_Default,
                    new TokenizationStrategy(),
                    true
                ).SetArgDisplayNames($"{nameof(equalityMethodsTestCases)}_02"),

            new TestCaseData(
                    ObjectMother.TokenizationStrategy_Default,
                    ObjectMother.TokenizationStrategy_LettersSemicolon,
                    false
                ).SetArgDisplayNames($"{nameof(equalityMethodsTestCases)}_03"),

            new TestCaseData(
                    ObjectMother.TokenizationStrategy_Default,
                    null,
                    false
                ).SetArgDisplayNames($"{nameof(equalityMethodsTestCases)}_04")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(tokenizationStrategyExceptionTestCases))]
        public void TokenizationStrategy_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void ToString_ShouldReturnTheExpectedString_WhenInvoked()
        {

            // Arrange
            // Act
            string actual = new TokenizationStrategy().ToString();

            // Assert
            Assert.That(
                string.Equals(
                    ObjectMother.TokenizationStrategy_Default_AsString,
                    actual,
                    StringComparison.InvariantCulture), 
                Is.False);

        }

        [Test]
        public void TokenizationStrategy_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            TokenizationStrategy actual1
                = new TokenizationStrategy(
                        TokenizationStrategy.DefaultPattern,
                        TokenizationStrategy.DefaultDelimiter,
                        TokenizationStrategy.DefaultToLowercase
                    );
            TokenizationStrategy actual2
                = new TokenizationStrategy();

            // Assert
            Assert.That(actual1, Is.InstanceOf<TokenizationStrategy>());
            Assert.That(actual2, Is.InstanceOf<TokenizationStrategy>());

        }

        [TestCaseSource(nameof(equalityMethodsTestCases))]
        public void EqualsAndEqualityOperators_ShouldReturnTheExpectedBoolean_WhenInvoked
            (TokenizationStrategy a, TokenizationStrategy b, bool expected)
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
            bool actual = ObjectMother.TokenizationStrategy_Default.Equals("some_string");

            // Assert
            Assert.That(actual, Is.False);

        }

        [Test]
        public void EqualityOperator_ShouldReturnFalse_WhenLeftMemberIsNull()
        {

            // Arrange
            // Act
            bool actual = null == ObjectMother.TokenizationStrategy_Default;

            // Assert
            Assert.That(actual, Is.False);

        }

        [Test]
        public void GetHashCode_ShouldReturnExpectedHashCode_WhenInvoked()
        {

            // Arrange
            // Act
            int actual = ObjectMother.TokenizationStrategy_Default.GetHashCode();

            // Assert
            Assert.That(ObjectMother.TokenizationStrategy_Default_HashCode, Is.EqualTo(actual));

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