using System;
using NW.NGramTextClassification.Filenames;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests.Filenames
{
    [TestFixture]
    public class FilenameFactoryTests
    {

        #region Fields

        private static TestCaseData[] createMethodExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                    () => new FilenameFactory().CreateForTextSnippetsJson(folderPath: null, now: ObjectMother.FakeNow)
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("folderPath").Message
            ).SetArgDisplayNames($"{nameof(createMethodExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                    () => new FilenameFactory().CreateForLabeledExamplesJson(folderPath: null, now: ObjectMother.FakeNow)
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("folderPath").Message
            ).SetArgDisplayNames($"{nameof(createMethodExceptionTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                    () => new FilenameFactory().CreateForSessionJson(folderPath: null, now: ObjectMother.FakeNow)
                ),
                typeof(ArgumentNullException),
                new ArgumentNullException("folderPath").Message
            ).SetArgDisplayNames($"{nameof(createMethodExceptionTestCases)}_03")

        };
        private static TestCaseData[] createMethodTestCases =
        {

            new TestCaseData(
                    new Func<string>(
                            () => new FilenameFactory()
                                        .CreateForTextSnippetsJson(
                                            folderPath: ObjectMother.FakeFilePath,
                                            now: ObjectMother.FakeNow)
                        ),
                    ObjectMother.Filename_TextSnippetsJson
                ).SetArgDisplayNames($"{nameof(createMethodTestCases)}_01"),

            new TestCaseData(
                    new Func<string>(
                            () => new FilenameFactory()
                                        .CreateForLabeledExamplesJson(
                                            folderPath: ObjectMother.FakeFilePath,
                                            now: ObjectMother.FakeNow)
                        ),
                    ObjectMother.Filename_LabeledExamplesJson
                ).SetArgDisplayNames($"{nameof(createMethodTestCases)}_02"),

            new TestCaseData(
                    new Func<string>(
                            () => new FilenameFactory()
                                        .CreateForSessionJson(
                                            folderPath: ObjectMother.FakeFilePath,
                                            now: ObjectMother.FakeNow)
                        ),
                    ObjectMother.Filename_SessionJson
                ).SetArgDisplayNames($"{nameof(createMethodTestCases)}_03")

        };

        #endregion

        #region SetUp

        #endregion

        #region Tests

        [TestCaseSource(nameof(createMethodExceptionTestCases))]
        public void CreateMethod_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(createMethodTestCases))]
        public void CreateMethod_ShouldReturnExpectedString_WhenInvoked(Func<string> func, string expected)
        {

            // Arrange
            // Act
            string actual = func();

            // Assert
            Assert.That(expected, Is.EqualTo(actual));

        }

        [Test]
        public void FilenameFactory_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
        {

            // Arrange
            // Act
            FilenameFactory actual = new FilenameFactory();

            // Assert
            Assert.That(actual, Is.InstanceOf<FilenameFactory>());

            Assert.That(FilenameFactory.DefaultFileNameTemplate, Is.InstanceOf<string>());
            Assert.That(FilenameFactory.DefaultFormatNow, Is.InstanceOf<string>());
            Assert.That(FilenameFactory.DefaultJsonExtension, Is.InstanceOf<string>());
            Assert.That(FilenameFactory.DefaultLabeledExamplesToken, Is.InstanceOf<string>());
            Assert.That(FilenameFactory.DefaultMainToken, Is.InstanceOf<string>());
            Assert.That(FilenameFactory.DefaultSessionToken, Is.InstanceOf<string>());
            Assert.That(FilenameFactory.DefaultTextSnippetsToken, Is.InstanceOf<string>());

        }

        #endregion

        #region TearDown

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 30.01.2024
*/
