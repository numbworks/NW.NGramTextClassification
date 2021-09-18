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

            // ValidateObject
            new TestCaseData(
                new TestDelegate( () => new NGramTokenizer(null) ),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.NGramTokenizer_VariableName_ArrayManager).Message
                ).SetArgDisplayNames($"{nameof(nGramTokenizerExceptionTestCases)}_01")

        };
        private static TestCaseData[] doExceptionTestCases =
        {

            // ValidateStringNullOrWhiteSpace
            new TestCaseData(
                new TestDelegate(
                            () => new NGramTokenizer().Do(null)
                        ),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.NGramTokenizer_VariableName_Text).Message
                ).SetArgDisplayNames($"{nameof(doExceptionTestCases)}_01"),
            new TestCaseData(
                new TestDelegate(
                            () => new NGramTokenizer().Do(string.Empty)
                        ),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.NGramTokenizer_VariableName_Text).Message
                ).SetArgDisplayNames($"{nameof(doExceptionTestCases)}_02"),
            new TestCaseData(
                new TestDelegate(
                            () => new NGramTokenizer().Do(ObjectMother.NGramTokenizer_TextOnlyWhiteSpaces)
                        ),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.NGramTokenizer_VariableName_Text).Message
                ).SetArgDisplayNames($"{nameof(doExceptionTestCases)}_03"),

            // ValidateObject
            new TestCaseData(
                new TestDelegate(
                            () => new NGramTokenizer()
                                        .Do(
                                            ObjectMother.NGramTokenizer_Text1,
                                            null,
                                            new NGramTokenizerRuleSet()
                        )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.NGramTokenizer_VariableName_Strategy).Message
                ).SetArgDisplayNames($"{nameof(doExceptionTestCases)}_04"),
            new TestCaseData(
                new TestDelegate(
                            () => new NGramTokenizer()
                                        .Do(
                                            ObjectMother.NGramTokenizer_Text1,
                                            new TokenizationStrategy(),
                                            null
                        )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.NGramTokenizer_VariableName_RuleSet).Message
                ).SetArgDisplayNames($"{nameof(doExceptionTestCases)}_05"),

            // TokenizeText<T>
            new TestCaseData(
                new TestDelegate(
                            () => new NGramTokenizer()
                                        .Do(
                                            "some string",
                                            new NGramTokenizerRuleSet(true, true, true, false, false)
                                            )
                        ),
                typeof(Exception),
                MessageCollection.NGramTokenizer_TheRuleCantBeAppliedTo.Invoke("DoForTrigrams", "some string")
                ).SetArgDisplayNames($"{nameof(doExceptionTestCases)}_06"),

            new TestCaseData(
                new TestDelegate(
                            () => new NGramTokenizer()
                                        .Do(
                                            ObjectMother.NGramTokenizer_TextNonAlphanumerical,
                                            new NGramTokenizerRuleSet(true, false, false, false, false))
                        ),
                typeof(Exception),
                MessageCollection.NGramTokenizer_TheRuleCantBeAppliedTo.Invoke("DoForMonograms", ObjectMother.NGramTokenizer_TextNonAlphanumerical)
                ).SetArgDisplayNames($"{nameof(doExceptionTestCases)}_07"),

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(nGramTokenizerExceptionTestCases))]
        public void NGramTokenizer_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(doExceptionTestCases))]
        public void Do_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void Do_ShouldReturnACollectionOfNGrams_WhenProperArguments()
        {

            // Arrange
            // Act
            List<INGram> actual1 = new NGramTokenizer()
                                        .Do(
                                            ObjectMother.NGramTokenizer_Text1,
                                            new TokenizationStrategy(),
                                            new NGramTokenizerRuleSet());
            List<INGram> actual2 = new NGramTokenizer()
                                        .Do(
                                            ObjectMother.NGramTokenizer_Text1,
                                            new TokenizationStrategy());
            List<INGram> actual3 = new NGramTokenizer()
                                        .Do(
                                            ObjectMother.NGramTokenizer_Text1,
                                            new NGramTokenizerRuleSet());
            List<INGram> actual4 = new NGramTokenizer()
                                        .Do(
                                            ObjectMother.NGramTokenizer_Text1);

            // Assert
            Assert.IsTrue(
                    ObjectMother.AreEqual(
                        ObjectMother.NGramTokenizer_Text1_NGrams,
                        actual1));
            Assert.IsTrue(
                    ObjectMother.AreEqual(
                        ObjectMother.NGramTokenizer_Text1_NGrams,
                        actual2));
            Assert.IsTrue(
                    ObjectMother.AreEqual(
                        ObjectMother.NGramTokenizer_Text1_NGrams,
                        actual3));
            Assert.IsTrue(
                    ObjectMother.AreEqual(
                        ObjectMother.NGramTokenizer_Text1_NGrams,
                        actual4));

        }

        [Test]
        public void NGramTokenizer_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            NGramTokenizer actual1
                = new NGramTokenizer(
                        new ArrayManager()
                    );
            NGramTokenizer actual2
                = new NGramTokenizer();

            // Assert
            Assert.IsInstanceOf<NGramTokenizer>(actual1);
            Assert.IsInstanceOf<NGramTokenizer>(actual2);

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