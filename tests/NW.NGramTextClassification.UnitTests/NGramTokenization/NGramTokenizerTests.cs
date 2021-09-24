using System;
using System.Collections.Generic;
using NW.NGramTextClassification.Arrays;
using NW.NGramTextClassification.Messages;
using NW.NGramTextClassification.NGrams;
using NW.NGramTextClassification.NGramTokenization;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests
{
    [TestFixture]
    public class NGramTokenizerTests
    {

        #region Fields

        private static TestCaseData[] nGramTokenizerExceptionTestCases =
        {

            new TestCaseData(
                    new TestDelegate( () => new NGramTokenizer(null, new TokenizationStrategy()) ),
                    typeof(ArgumentNullException),
                    new ArgumentNullException("arrayManager").Message
                ).SetArgDisplayNames($"{nameof(nGramTokenizerExceptionTestCases)}_01"),

            new TestCaseData(
                    new TestDelegate( () => new NGramTokenizer(new ArrayManager(), null) ),
                    typeof(ArgumentNullException),
                    new ArgumentNullException("tokenizationStrategy").Message
                ).SetArgDisplayNames($"{nameof(nGramTokenizerExceptionTestCases)}_02"),

        };
        private static TestCaseData[] tryDoForRuleSetTestCases =
        {

            new TestCaseData(
                    ObjectMother.Shared_Text1_Text,
                    ObjectMother.Shared_NGramTokenizerRuleSet_Mono,
                    ObjectMother.CreateNGrams(
                        ObjectMother.Shared_Text1_TextAsMonograms
                        )
                ).SetArgDisplayNames($"{nameof(tryDoForRuleSetTestCases)}_01"),

            new TestCaseData(
                    ObjectMother.Shared_Text1_Text,
                    ObjectMother.Shared_NGramTokenizerRuleSet_MonoBi,
                    ObjectMother.CreateNGrams(
                        ObjectMother.Shared_Text1_TextAsMonograms,
                        ObjectMother.Shared_Text1_TextAsBigrams
                        )
                ).SetArgDisplayNames($"{nameof(tryDoForRuleSetTestCases)}_02"),

            new TestCaseData(
                    ObjectMother.Shared_Text1_Text,
                    ObjectMother.Shared_NGramTokenizerRuleSet_MonoBiTri,
                    ObjectMother.CreateNGrams(
                        ObjectMother.Shared_Text1_TextAsMonograms,
                        ObjectMother.Shared_Text1_TextAsBigrams,
                        ObjectMother.Shared_Text1_TextAsTrigrams
                        )
                ).SetArgDisplayNames($"{nameof(tryDoForRuleSetTestCases)}_03"),

            new TestCaseData(
                    ObjectMother.Shared_Text1_Text,
                    ObjectMother.Shared_NGramTokenizerRuleSet_MonoBiTriFour,
                    ObjectMother.CreateNGrams(
                        ObjectMother.Shared_Text1_TextAsMonograms,
                        ObjectMother.Shared_Text1_TextAsBigrams,
                        ObjectMother.Shared_Text1_TextAsTrigrams,
                        ObjectMother.Shared_Text1_TextAsFourgrams
                        )
                ).SetArgDisplayNames($"{nameof(tryDoForRuleSetTestCases)}_04"),

            new TestCaseData(
                    ObjectMother.Shared_Text1_Text,
                    ObjectMother.Shared_NGramTokenizerRuleSet_MonoBiTriFourFive,
                    ObjectMother.CreateNGrams(
                        ObjectMother.Shared_Text1_TextAsMonograms,
                        ObjectMother.Shared_Text1_TextAsBigrams,
                        ObjectMother.Shared_Text1_TextAsTrigrams,
                        ObjectMother.Shared_Text1_TextAsFourgrams,
                        ObjectMother.Shared_Text1_TextAsFivegrams
                        )
                ).SetArgDisplayNames($"{nameof(tryDoForRuleSetTestCases)}_05")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(nGramTokenizerExceptionTestCases))]
        public void NGramTokenizer_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void NGramTokenizer_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            NGramTokenizer actual1 = new NGramTokenizer(new ArrayManager(), new TokenizationStrategy());
            NGramTokenizer actual2 = new NGramTokenizer();

            // Assert
            Assert.IsInstanceOf<NGramTokenizer>(actual1);
            Assert.IsInstanceOf<NGramTokenizer>(actual2);

        }

        [TestCaseSource(nameof(tryDoForRuleSetTestCases))]
        public void TryDoRuleSet_ShouldReturnExpectedCollectionOfNGrams_WhenProperParameters
            (string text, INGramTokenizerRuleSet tokenizerRuleSet, List<INGram> expected)
        {


            // Arrange
            NGramTokenizer nGramTokenizer = new NGramTokenizer();

            // Act
            List<INGram> actual = nGramTokenizer.TryDoForRuleSet(text, tokenizerRuleSet);

            // Assert
            Assert.IsTrue(
                    ObjectMother.AreEqual(expected, actual)
                );

        }

        #endregion

        #region TearDown
        #endregion

        #region Support_Methods

        private static TestCaseData[] CreateDoForRuleSetTestCases(string baseName)
        {

            List<TestCaseData> testCases = new List<TestCaseData>()
            {




            };

            return testCases.ToArray();

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 24.09.2021
*/