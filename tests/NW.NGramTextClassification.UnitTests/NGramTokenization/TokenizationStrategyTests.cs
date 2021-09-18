using System;
using NUnit.Framework;
using NW.NGramTextClassification.NGramTokenization;

namespace NW.NGramTextClassification.UnitTests
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
                new ArgumentNullException(ObjectMother.TokenizationStrategy_VariableName_Pattern).Message
                ).SetArgDisplayNames($"{nameof(tokenizationStrategyExceptionTestCases)}_01"),
            new TestCaseData(
                new TestDelegate(
                        () => new TokenizationStrategy(
                                    "  ",
                                    TokenizationStrategy.DefaultDelimiter,
                                    TokenizationStrategy.DefaultToLowercase
                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.TokenizationStrategy_VariableName_Pattern).Message
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
                new ArgumentNullException(ObjectMother.TokenizationStrategy_VariableName_Delimiter).Message
                ).SetArgDisplayNames($"{nameof(tokenizationStrategyExceptionTestCases)}_03"),
            new TestCaseData(
                new TestDelegate(
                        () => new TokenizationStrategy(
                                    TokenizationStrategy.DefaultPattern,
                                    string.Empty,
                                    TokenizationStrategy.DefaultToLowercase
                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.TokenizationStrategy_VariableName_Delimiter).Message
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
                    ObjectMother.TokenizationStrategy_Custom,
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
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void ToString_ShouldReturnTheExpectedString_WhenInvoked()
        {

            // Arrange
            // Act
            string actual = new TokenizationStrategy().ToString();

            // Assert
            Assert.IsTrue(
                string.Equals(
                    ObjectMother.TokenizationStrategy_ToString,
                    actual,
                    StringComparison.InvariantCulture));

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
            Assert.IsInstanceOf<TokenizationStrategy>(actual1);
            Assert.IsInstanceOf<TokenizationStrategy>(actual2);

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
            Assert.AreEqual(expected, actual1);
            Assert.AreEqual(expected, actual2);
            Assert.AreNotEqual(expected, actual3);

        }

        [Test]
        public void Equals_ShouldReturnReturnFalse_WhenComparedWithAnObjectOfDifferentType()
        {

            // Arrange
            // Act
            bool actual = ObjectMother.TokenizationStrategy_Default.Equals("some_string");

            // Assert
            Assert.IsFalse(actual);

        }

        [Test]
        public void EqualityOperator_ShouldReturnFalse_WhenLeftMemberIsNull()
        {

            // Arrange
            // Act
            bool actual = null == ObjectMother.TokenizationStrategy_Default;

            // Assert
            Assert.IsFalse(actual);

        }

        [Test]
        public void GetHashCode_ShouldReturnExpectedHashCode_WhenInvoked()
        {

            // Arrange
            // Act
            int actual = ObjectMother.TokenizationStrategy_Default.GetHashCode();

            // Assert
            Assert.AreEqual(ObjectMother.TokenizationStrategy_DefaultHashCode, actual);

        }


        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 18.09.2021
*/