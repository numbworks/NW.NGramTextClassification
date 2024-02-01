using System;
using NW.NGramTextClassificationClient.ApplicationSession;
using NUnit.Framework;
using McMaster.Extensions.CommandLineUtils.Validation;

namespace NW.NGramTextClassificationClient.UnitTests
{
    [TestFixture]
    public class SessionManagerBagTests
    {

        #region Fields

        private static TestCaseData[] sessionManagerBagExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new SessionManagerBag(null)
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("doubleManager").Message
            ).SetArgDisplayNames($"{nameof(sessionManagerBagExceptionTestCases)}_01")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(sessionManagerBagExceptionTestCases))]
        public void SessionManagerBag_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [Test]
        public void SessionManagerBag_ShouldCreateAnObjectOfThisType_WhenInvoked()
        {

            // Arrange
            // Act
            SessionManagerBag actual1 = new SessionManagerBag();
            SessionManagerBag actual2 = new SessionManagerBag(new DoubleManager());

            // Assert
            Assert.That(actual1, Is.InstanceOf<SessionManagerBag>());
            Assert.That(actual2, Is.InstanceOf<SessionManagerBag>());

            Assert.That(actual1.DoubleManager, Is.InstanceOf<IDoubleManager>());
            Assert.That(actual1.MinimumAccuracyValidator, Is.InstanceOf<IOptionValidator>());

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
    Last Update: 01.02.2024
*/