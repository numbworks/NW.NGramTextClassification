using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests
{
    [TestFixture]
    public class TextClassifierTests
    {

        // Fields
        private static TestCaseData[] textClassifierExceptionTestCases =
        {

            // ValidateObject
            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier(
                                        null,
                                        new TextClassifierSettings()
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.TextClassifier_VariableName_Components).Message
                ).SetArgDisplayNames($"{nameof(textClassifierExceptionTestCases)}_01"),

            // ValidateObject
            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier(
                                        new TextClassifierComponents(),
                                        null
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.TextClassifier_VariableName_Settings).Message
                ).SetArgDisplayNames($"{nameof(textClassifierExceptionTestCases)}_02")

        };
        private static TestCaseData[] predictLabelExceptionTestCases =
        {

            // ValidateStringNullOrWhiteSpace
            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .PredictLabel(
                                        null,
                                        ObjectMother.TextClassifier_LabeledExamples
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.TextClassifier_VariableName_Text).Message
                ).SetArgDisplayNames($"{nameof(predictLabelExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .PredictLabel(
                                        ObjectMother.TextClassifier_TextOnlyWhiteSpaces,
                                        ObjectMother.TextClassifier_LabeledExamples
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.TextClassifier_VariableName_Text).Message
                ).SetArgDisplayNames($"{nameof(predictLabelExceptionTestCases)}_02"),


            // ValidateObject
            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .PredictLabel(
                                        ObjectMother.TextClassifier_Text1,
                                        null,
                                        new NGramTokenizerRuleSet(),
                                        ObjectMother.TextClassifier_LabeledExamples
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.TextClassifier_VariableName_Strategy).Message
                ).SetArgDisplayNames($"{nameof(predictLabelExceptionTestCases)}_03"),

            // ValidateObject
            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .PredictLabel(
                                        ObjectMother.TextClassifier_Text1,
                                        new TokenizationStrategy(),
                                        null,
                                        ObjectMother.TextClassifier_LabeledExamples
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.TextClassifier_VariableName_RuleSet).Message
                ).SetArgDisplayNames($"{nameof(predictLabelExceptionTestCases)}_04"),

            // ValidateList
            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .PredictLabel(
                                        ObjectMother.TextClassifier_Text1,
                                        null
                                )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.TextClassifier_VariableName_LabeledExamples).Message
                ).SetArgDisplayNames($"{nameof(predictLabelExceptionTestCases)}_05"),

            new TestCaseData(
                new TestDelegate(
                        () => new TextClassifier()
                                    .PredictLabel(
                                        ObjectMother.TextClassifier_Text1,
                                        new List<LabeledExample>()
                                )),
                typeof(ArgumentException),
                MessageCollection.VariableContainsZeroItems.Invoke(ObjectMother.TextClassifier_VariableName_LabeledExamples)
                ).SetArgDisplayNames($"{nameof(predictLabelExceptionTestCases)}_06")

        };

        // SetUp
        // Tests
        [TestCaseSource(nameof(textClassifierExceptionTestCases))]
        public void TextClassifier_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(predictLabelExceptionTestCases))]
        public void PredictLabel_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        // TearDown
        // Support methods


    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 31.12.2020

*/
