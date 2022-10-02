using System;
using System.Collections.Generic;
using System.IO;
using NW.NGramTextClassification.Files;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests.Files
{

    [TestFixture]
    public class FileManagerTests
    {

        #region Fields

        private static TestCaseData[] fileManagerExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate( () => new FileManager(null) ),
                typeof(ArgumentNullException),
                new ArgumentNullException("fileAdapter").Message
                ).SetArgDisplayNames($"{nameof(fileManagerExceptionTestCases)}_01"),

        };
        private static TestCaseData[] readAllLinesExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new FileManager().ReadAllLines(null)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException("file").Message
                ).SetArgDisplayNames($"{nameof(readAllLinesExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new FileManager().ReadAllLines(ObjectMother.FileInfoAdapterDoesntExist)
                    ),
                typeof(ArgumentException),
                NGramTextClassification.Validation.MessageCollection.ProvidedPathDoesntExist(ObjectMother.FileInfoAdapterDoesntExist)
                ).SetArgDisplayNames($"{nameof(readAllLinesExceptionTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                        () => new FileManager(ObjectMother.FileAdapterReadAllMethodsThrowIOException)
                                    .ReadAllLines(ObjectMother.FileInfoAdapterExists)
                    ),
                typeof(Exception),
                MessageCollection.NotPossibleToRead(
                                    ObjectMother.FileInfoAdapterExists,
                                    ObjectMother.FileAdapterIOException)
                ).SetArgDisplayNames($"{nameof(readAllLinesExceptionTestCases)}_03")

        };
        private static TestCaseData[] readAllTextExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new FileManager().ReadAllText(null)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException("file").Message
                ).SetArgDisplayNames($"{nameof(readAllTextExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new FileManager().ReadAllText(ObjectMother.FileInfoAdapterDoesntExist)
                    ),
                typeof(ArgumentException),
                NGramTextClassification.Validation.MessageCollection.ProvidedPathDoesntExist(ObjectMother.FileInfoAdapterDoesntExist)
                ).SetArgDisplayNames($"{nameof(readAllTextExceptionTestCases)}_02"),

            new TestCaseData(
                new TestDelegate(
                        () => new FileManager(ObjectMother.FileAdapterReadAllMethodsThrowIOException)
                                    .ReadAllText(ObjectMother.FileInfoAdapterExists)
                    ),
                typeof(Exception),
                MessageCollection.NotPossibleToRead(
                                    ObjectMother.FileInfoAdapterExists,
                                    ObjectMother.FileAdapterIOException)
                ).SetArgDisplayNames($"{nameof(readAllTextExceptionTestCases)}_03")

        };
        private static TestCaseData[] writeAllLinesExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new FileManager().WriteAllLines(null, ObjectMother.ContentMultipleLines)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException("file").Message
                ).SetArgDisplayNames($"{nameof(writeAllLinesExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new FileManager(ObjectMother.FileAdapterWriteAllMethodsThrowIOException)
                                    .WriteAllLines(
                                        ObjectMother.FileInfoAdapterExists,
                                        ObjectMother.ContentMultipleLines)
                    ),
                typeof(Exception),
                MessageCollection.NotPossibleToWrite(
                                    ObjectMother.FileInfoAdapterExists,
                                    ObjectMother.FileAdapterIOException)
                ).SetArgDisplayNames($"{nameof(writeAllLinesExceptionTestCases)}_02")

        };
        private static TestCaseData[] writeAllTextExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new FileManager().WriteAllText(null, ObjectMother.ContentSingleLine)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException("file").Message
                ).SetArgDisplayNames($"{nameof(writeAllTextExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new FileManager(ObjectMother.FileAdapterWriteAllMethodsThrowIOException)
                                    .WriteAllText(
                                        ObjectMother.FileInfoAdapterExists,
                                        ObjectMother.ContentSingleLine)
                    ),
                typeof(Exception),
                MessageCollection.NotPossibleToWrite(
                                    ObjectMother.FileInfoAdapterExists,
                                    ObjectMother.FileAdapterIOException)
                ).SetArgDisplayNames($"{nameof(writeAllTextExceptionTestCases)}_02")

        };
        private static TestCaseData[] createExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => new FileManager().Create((string)null)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException("filePath").Message
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => new FileManager().Create((FileInfo)null)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException("fileInfo").Message
                ).SetArgDisplayNames($"{nameof(createExceptionTestCases)}_02")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(fileManagerExceptionTestCases))]
        public void FileManager_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(readAllLinesExceptionTestCases))]
        public void ReadAllLines_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);
        [Test]
        public void ReadAllLines_ShouldReturnStrings_WhenFileExists()
        {

            // Arrange
            // Act
            IEnumerable<string> actual
                = new FileManager(ObjectMother.FileAdapterAllMethodsWork)
                            .ReadAllLines(ObjectMother.FileInfoAdapterExists);

            // Assert
            Assert.AreEqual(ObjectMother.ContentMultipleLines, actual);

        }

        [TestCaseSource(nameof(readAllTextExceptionTestCases))]
        public void ReadAllText_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);
        [Test]
        public void ReadAllText_ShouldReturnString_WhenFileExists()
        {

            // Arrange
            // Act
            string actual
                = new FileManager(ObjectMother.FileAdapterAllMethodsWork)
                            .ReadAllText(ObjectMother.FileInfoAdapterExists);

            // Assert
            Assert.AreEqual(ObjectMother.ContentSingleLine, actual);

        }

        [TestCaseSource(nameof(writeAllLinesExceptionTestCases))]
        public void WriteAllLines_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);
        [Test]
        public void WriteAllLines_ShouldSuccessfullyWriteToFile_WhenNoIOIssuesArise()
        {

            // Arrange
            // Act
            // Assert
            try
            {

                new FileManager(ObjectMother.FileAdapterAllMethodsWork)
                        .WriteAllLines(ObjectMother.FileInfoAdapterExists, ObjectMother.ContentMultipleLines);
                Assert.IsTrue(true);

            }
            catch
            {

                Assert.IsFalse(false);

            }

        }

        [TestCaseSource(nameof(writeAllTextExceptionTestCases))]
        public void WriteAllText_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);
        [Test]
        public void WriteAllText_ShouldSuccessfullyWriteToFile_WhenNoIOIssuesArise()
        {

            // Arrange
            // Act
            // Assert
            try
            {

                new FileManager(ObjectMother.FileAdapterAllMethodsWork)
                        .WriteAllText(ObjectMother.FileInfoAdapterExists, ObjectMother.ContentSingleLine);
                Assert.IsTrue(true);

            }
            catch
            {

                Assert.IsFalse(false);

            }

        }

        [TestCaseSource(nameof(createExceptionTestCases))]
        public void Create_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => Utilities.ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);
        [Test]
        public void Create_ShouldRetunAIFileInfoAdapterObject_WhenInvoked()
        {

            // Arrange
            // Act
            IFileInfoAdapter actual = new FileManager().Create(@"J:\");

            // Assert
            Assert.IsInstanceOf<IFileInfoAdapter>(actual);

        }

        #endregion

        #region TearDown
        #endregion

    }

}
/*
    Author: numbworks@gmail.com
    Last Update: 30.06.2022
*/
