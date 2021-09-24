using System;
using NW.NGramTextClassification.NGramTokenization;
using NUnit.Framework;
using NW.NGramTextClassification.Messages;

namespace NW.NGramTextClassification.UnitTests
{
    [TestFixture]
    public class NGramTokenizerRuleSetTests
    {

        #region Fields

        private static TestCaseData[] nGramTokenizerRuleSetExceptionTestCases =
        {

            new TestCaseData(
                    new TestDelegate( () => new NGramTokenizerRuleSet(false, false, false, false, false) ),
                    typeof(ArgumentException),
                    MessageCollection.NGramTokenizerRuleSet_AtLeastOneArgumentMustBeTrue
                ).SetArgDisplayNames($"{nameof(nGramTokenizerRuleSetExceptionTestCases)}_01")
        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(nGramTokenizerRuleSetExceptionTestCases))]
        public void NGramTokenizerRuleSet_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void NGramTokenizerRuleSet_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            NGramTokenizerRuleSet actual1 = new NGramTokenizerRuleSet(true, false, true, false, false);
            NGramTokenizerRuleSet actual2 = new NGramTokenizerRuleSet();

            // Assert
            Assert.IsInstanceOf<NGramTokenizerRuleSet>(actual1);
            Assert.IsInstanceOf<NGramTokenizerRuleSet>(actual2);

        }

        #endregion

        #region TearDown
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 24.09.2021
*/