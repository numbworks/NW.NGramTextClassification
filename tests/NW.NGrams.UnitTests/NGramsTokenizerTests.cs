using System;
using System.Collections.Generic;
using NUnit.Framework;
using RUBN.Shared;

namespace NW.NGrams.UnitTests
{
    [TestFixture]
    public class NGramsTokenizerTests
    {

        // Fields
        private static string _errFailure = "It hasn't been possible to tokenize the provided text.";
        private static TestCaseData[] _arrTestCases = 
        {

            new TestCaseData(
                "This is a sample text.",
                new TokenizationStrategyTrigrams() {
                    Pattern =  "[a-zA-Z0-9_]{1,}",
                    Delimiter = " ",
                    N = 3,
                    ConvertAllToLowercase = true,
                    },
                OutcomeStatuses.Success,
                new List<string>() {
                    "The provided text has been successfully tokenized."
                },
                new List<string>() {
                    "this is a",
                    "is a sample",
                    "a sample text"
                }
            ).SetName(nameof(Do_ShouldCreateReturnExpectedStatusMessagesAndNGrams_WhenInvoked) + " {01}"),

            new TestCaseData(
                "This is a.",
                new TokenizationStrategyTrigrams() {
                    Pattern =  "[a-zA-Z0-9_]{1,}",
                    Delimiter = " ",
                    N = 3,
                    ConvertAllToLowercase = true,
                    },
                OutcomeStatuses.Success,
                new List<string>() {
                    "The provided text has been successfully tokenized."
                },
                new List<string>() {
                    "this is a",
                }
            ).SetName(nameof(Do_ShouldCreateReturnExpectedStatusMessagesAndNGrams_WhenInvoked) + " {02}"),

            new TestCaseData(
                "This is a.",
                new TokenizationStrategyTrigrams() {
                    Pattern =  "[a-zA-Z0-9_]{1,}",
                    Delimiter = " ",
                    N = 3,
                    ConvertAllToLowercase = false,
                    },
                OutcomeStatuses.Success,
                new List<string>() {
                    "The provided text has been successfully tokenized."
                },
                new List<string>() {
                    "This is a",
                }
            ).SetName(nameof(Do_ShouldCreateReturnExpectedStatusMessagesAndNGrams_WhenInvoked) + " {03}"),

            new TestCaseData(
                String.Empty,
                new TokenizationStrategyTrigrams() {
                    Pattern =  "[a-zA-Z0-9_]{1,}",
                    Delimiter = " ",
                    N = 3,
                    ConvertAllToLowercase = false,
                    },
                OutcomeStatuses.Failure,
                new List<string>() {
                     "The parameter at position '1' is null or empty.",
                     _errFailure
                },
                null
            ).SetName(nameof(Do_ShouldCreateReturnExpectedStatusMessagesAndNGrams_WhenInvoked) + " {04}"),

            new TestCaseData(
                null,
                new TokenizationStrategyTrigrams() {
                    Pattern =  "[a-zA-Z0-9_]{1,}",
                    Delimiter = " ",
                    N = 3,
                    ConvertAllToLowercase = false,
                    },
                OutcomeStatuses.Failure,
                new List<string>() {
                     "The parameter at position '1' is null or empty.",
                     _errFailure
                },
                null
            ).SetName(nameof(Do_ShouldCreateReturnExpectedStatusMessagesAndNGrams_WhenInvoked) + " {05}"),

            new TestCaseData(
                "This is a.",
                new TokenizationStrategyTrigrams() {
                    Pattern =  null,
                    },
                OutcomeStatuses.Failure,
                new List<string>() {
                     "The parameter at position '0' is null or empty.",
                     _errFailure
                },
                null
            ).SetName(nameof(Do_ShouldCreateReturnExpectedStatusMessagesAndNGrams_WhenInvoked) + " {06}"),

            new TestCaseData(
                "This is a.",
                new TokenizationStrategyTrigrams() {
                    Pattern =  String.Empty,
                    },
                OutcomeStatuses.Failure,
                new List<string>() {
                     "The parameter at position '0' is null or empty.",
                     _errFailure
                },
                null
            ).SetName(nameof(Do_ShouldCreateReturnExpectedStatusMessagesAndNGrams_WhenInvoked) + " {07}"),

            new TestCaseData(
                "This is a.",
                new TokenizationStrategyTrigrams() {
                    Pattern =  "[a-zA-Z0-9_]{1,}",
                    Delimiter = " ",
                    N = 0,
                    ConvertAllToLowercase = false,
                    },
                OutcomeStatuses.Failure,
                new List<string>() {
                      "'N' must be at least equal to 1 (actual value:'0').",
                      _errFailure
                },
                null
            ).SetName(nameof(Do_ShouldCreateReturnExpectedStatusMessagesAndNGrams_WhenInvoked) + " {08}"),

            new TestCaseData(
                "This is a.",
                new TokenizationStrategyTrigrams() {
                    Pattern =  "[0-9]{1,}",
                    Delimiter = " ",
                    N = 3,
                    ConvertAllToLowercase = false,
                    },
                OutcomeStatuses.Failure,
                new List<string>() {
                      "No matches found in the provided text for the provided pattern: '[0-9]{1,}'.",
                      _errFailure
                },
                null
            )

        };

        // SetUp
        // Tests
        [TestCaseSource(nameof(_arrTestCases))]
        public void Do_ShouldCreateReturnExpectedStatusMessagesAndNGrams_WhenInvoked(
            string strText, 
            ITokenizationStrategy objTokenizationStrategy, 
            OutcomeStatuses objExpectedStatus,
            List<string> listExpectedMessages,
            List<string> listExpectedResult)
        {

            // Arrange
            Outcome objExpected = new Outcome(objExpectedStatus, listExpectedMessages, listExpectedResult);

            // Act
            Outcome objActual = new NGramsTokenizer().Do(objTokenizationStrategy, strText);

            // Assert
            Assert.AreEqual(objExpected.Status, objActual.Status);

            if (objActual.Messages != null)
                for (int i = 0; i < objExpected.Messages.Count; i++)
                    Assert.AreEqual(objExpected.Messages[i], objActual.Messages[i]);

            if (objActual.Result != null)
                for (int i = 0; i < ((List<string>)objExpected.Result).Count; i++)
                    Assert.AreEqual(((List<string>)objExpected.Result)[i], ((List<string>)objActual.Result)[i]);

        }

        [Test]
        public void Do_ShouldReturnFailureAndThrowAnException_WhenArraySubsetManagerIsNull()
        {

            // Arrange
            string strText = "This is a sample text.";
            ITokenizationStrategy objTokenizationStrategy = new TokenizationStrategyTrigrams()
            {
                Pattern = "[a-zA-Z0-9_]{1,}",
                Delimiter = " ",
                N = 3,
                ConvertAllToLowercase = true,
            };

            // Act
            Outcome objActual = new NGramsTokenizer() {
                ArraySubsetsManager = null }.Do(objTokenizationStrategy, strText);

            // Assert
            Assert.AreEqual(OutcomeStatuses.Exception, objActual.Status);

        }

        // TearDown

    }
}

/*

    Author: rua@sitecore.net
    Last Update: 02.02.2018

*/
