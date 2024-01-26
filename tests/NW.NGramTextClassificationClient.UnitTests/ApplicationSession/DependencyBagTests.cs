using System;
using NW.NGramTextClassificationClient.ApplicationSession;
using NUnit.Framework;
using McMaster.Extensions.CommandLineUtils.Validation;

namespace NW.NGramTextClassificationClient.UnitTests
{
    [TestFixture]
    public class DependencyBagTests
    {

        #region Fields

        private static TestCaseData[] dependencyBagExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new DependencyBag(null)
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("doubleManager").Message
            ).SetArgDisplayNames($"{nameof(dependencyBagExceptionTestCases)}_01")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(dependencyBagExceptionTestCases))]
        public void DependencyBag_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void DependencyBag_ShouldCreateAnObjectOfThisType_WhenInvoked()
        {

            // Arrange
            // Act
            DependencyBag actual1 = new DependencyBag();
            DependencyBag actual2 = new DependencyBag(new DoubleManager());

            // Assert
            Assert.IsInstanceOf<DependencyBag>(actual1);
            Assert.IsInstanceOf<DependencyBag>(actual2);

            Assert.IsInstanceOf<IDoubleManager>(actual1.DoubleManager);
            Assert.IsInstanceOf<IOptionValidator>(actual1.MinimumAccuracyValidator);

        }

        #endregion

        #region TearDown
        #endregion

        #region Support_methods
        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 26.01.2024
*/