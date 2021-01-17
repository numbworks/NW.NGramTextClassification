using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests
{
    [TestFixture]
    public class LabeledExampleFactoryTests
    {

        // Fields
        private static TestCaseData[] labeledExampleFactoryExceptionTestCases =
        {

            // ValidateObject
            new TestCaseData(
                new TestDelegate( 
                        () => new LabeledExampleFactory(
                                            null, 
                                            ObjectMother.LabeledExampleFactory_InitialId1
                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.LabeledExampleFactory_VariableName_Tokenizer).Message
                ).SetArgDisplayNames($"{nameof(labeledExampleFactoryExceptionTestCases)}_01")

        };
        private static TestCaseData[] createExceptionTestCases =
        {

            // ValidateStringNullOrWhiteSpace
            new TestCaseData(
                new TestDelegate(
                        () => new LabeledExampleFactory()
                                    .Create(
                                        ObjectMother.LabeledExampleFactory_Id1,
                                        null,
                                        ObjectMother.LabeledExampleFactory_Text1,
                                        new TokenizationStrategy(),
                                        new NGramTokenizerRuleSet()
                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.LabeledExampleFactory_VariableName_Label).Message
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_01"),
            new TestCaseData(
                new TestDelegate(
                        () => new LabeledExampleFactory()
                                    .Create(
                                        ObjectMother.LabeledExampleFactory_Id1,
                                        string.Empty,
                                        ObjectMother.LabeledExampleFactory_Text1,
                                        new TokenizationStrategy(),
                                        new NGramTokenizerRuleSet()
                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.LabeledExampleFactory_VariableName_Label).Message
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_02"),
            new TestCaseData(
                new TestDelegate(
                        () => new LabeledExampleFactory()
                                    .Create(
                                        ObjectMother.LabeledExampleFactory_Id1,
                                        ObjectMother.LabeledExampleFactory_LabelOnlyWhiteSpaces,
                                        ObjectMother.LabeledExampleFactory_Text1,
                                        new TokenizationStrategy(),
                                        new NGramTokenizerRuleSet()
                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.LabeledExampleFactory_VariableName_Label).Message
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_03"),

            // ValidateStringNullOrWhiteSpace
            new TestCaseData(
                new TestDelegate(
                        () => new LabeledExampleFactory()
                                    .Create(
                                        ObjectMother.LabeledExampleFactory_Id1,
                                        ObjectMother.LabeledExampleFactory_Label1,
                                        null,
                                        new TokenizationStrategy(),
                                        new NGramTokenizerRuleSet()
                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.LabeledExampleFactory_VariableName_Text).Message
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_04"),
            new TestCaseData(
                new TestDelegate(
                        () => new LabeledExampleFactory()
                                    .Create(
                                        ObjectMother.LabeledExampleFactory_Id1,
                                        ObjectMother.LabeledExampleFactory_Label1,
                                        string.Empty,
                                        new TokenizationStrategy(),
                                        new NGramTokenizerRuleSet()
                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.LabeledExampleFactory_VariableName_Text).Message
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_05"),
            new TestCaseData(
                new TestDelegate(
                        () => new LabeledExampleFactory()
                                    .Create(
                                        ObjectMother.LabeledExampleFactory_Id1,
                                        ObjectMother.LabeledExampleFactory_Label1,
                                        ObjectMother.LabeledExampleFactory_TextOnlyWhiteSpaces,
                                        new TokenizationStrategy(),
                                        new NGramTokenizerRuleSet()
                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.LabeledExampleFactory_VariableName_Text).Message
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_06"),

            // ValidateObject
            new TestCaseData(
                new TestDelegate(
                        () => new LabeledExampleFactory()
                                    .Create(
                                        ObjectMother.LabeledExampleFactory_Id1,
                                        ObjectMother.LabeledExampleFactory_Label1,
                                        ObjectMother.LabeledExampleFactory_Text1,
                                        null,
                                        new NGramTokenizerRuleSet()
                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.LabeledExampleFactory_VariableName_Strategy).Message
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_07"),

            // ValidateObject
            new TestCaseData(
                new TestDelegate(
                        () => new LabeledExampleFactory()
                                    .Create(
                                        ObjectMother.LabeledExampleFactory_Id1,
                                        ObjectMother.LabeledExampleFactory_Label1,
                                        ObjectMother.LabeledExampleFactory_Text1,
                                        new TokenizationStrategy(),
                                        null
                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.LabeledExampleFactory_VariableName_RuleSet).Message
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_08"),

            // ValidateList
            new TestCaseData(
                new TestDelegate(
                        () => new LabeledExampleFactory()
                                    .Create(
                                        null,
                                        new TokenizationStrategy(),
                                        new NGramTokenizerRuleSet()
                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.LabeledExampleFactory_VariableName_Tuples).Message
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_09"),
            new TestCaseData(
                new TestDelegate(
                        () => new LabeledExampleFactory()
                                    .Create(
                                        new List<(string label, string text)>(),
                                        new TokenizationStrategy(),
                                        new NGramTokenizerRuleSet()
                            )),
                typeof(ArgumentException),
                MessageCollection.VariableContainsZeroItems.Invoke(ObjectMother.LabeledExampleFactory_VariableName_Tuples)
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_10"),

            // ValidateObject
            new TestCaseData(
                new TestDelegate(
                        () => new LabeledExampleFactory()
                                    .Create(
                                        ObjectMother.LabeledExampleFactory_Tuples,
                                        null,
                                        new NGramTokenizerRuleSet()
                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.LabeledExampleFactory_VariableName_Strategy).Message
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_11"),

            // ValidateObject
            new TestCaseData(
                new TestDelegate(
                        () => new LabeledExampleFactory()
                                    .Create(
                                        ObjectMother.LabeledExampleFactory_Tuples,
                                        new TokenizationStrategy(),
                                        null
                            )),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.LabeledExampleFactory_VariableName_RuleSet).Message
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_12")

        };

        // SetUp
        // Tests
        [TestCaseSource(nameof(labeledExampleFactoryExceptionTestCases))]
        public void LabeledExampleFactory_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);
        [TestCaseSource(nameof(createExceptionTestCases))]
        public void Create_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        // TearDown
        // Support methods
        private void Method_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
        {

            // Arrange
            // Act
            // Assert
            Exception actual = Assert.Throws(expectedType, del);
            Assert.AreEqual(expectedMessage, actual.Message);

        }

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 17.01.2021

*/
