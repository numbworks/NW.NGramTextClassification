using System;
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
                ).SetArgDisplayNames($"{nameof(labeledExampleFactoryExceptionTestCases)}_01"),

        };

        // SetUp
        // Tests
        [TestCaseSource(nameof(labeledExampleFactoryExceptionTestCases))]
        public void LabeledExampleFactory_ShouldThrowACertainException_WhenUnproperArguments
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
