using System;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests
{
    [TestFixture]
    public class TokenizationStrategyTests
    {

        // Fields
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

        // SetUp
        // Tests
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

        // TearDown
        // Support methods

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 22.01.2021

*/