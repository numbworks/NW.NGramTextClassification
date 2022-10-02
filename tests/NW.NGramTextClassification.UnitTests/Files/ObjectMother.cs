using System.Collections.Generic;
using System.IO;
using System.Linq;
using NW.NGramTextClassification.Files;
using NW.NGramTextClassification.UnitTests.Utilities;

namespace NW.NGramTextClassification.UnitTests.Files
{
    public static class ObjectMother
    {

        #region Properties

        public static string ContentSingleLine = "First line";
        public static IEnumerable<string> ContentMultipleLines =
            new List<string>() {
                "First line",
                "Second line"
            };
        public static string FileInfoAdapterFullName = @"C:\somefile.txt";

        public static IFileInfoAdapter FileInfoAdapterDoesntExist
            => new FakeFileInfoAdapter(false, FileInfoAdapterFullName);
        public static IFileInfoAdapter FileInfoAdapterExists
            => new FakeFileInfoAdapter(true, FileInfoAdapterFullName);

        public static IOException FileAdapterIOException = new IOException("Impossible to access the file.");
        public static IFileAdapter FileAdapterReadAllMethodsThrowIOException
            => new FakeFileAdapter(
                    fakeReadAllLines: () => throw FileAdapterIOException,
                    fakeReadAllText: () => throw FileAdapterIOException
                );
        public static IFileAdapter FileAdapterWriteAllMethodsThrowIOException
            => new FakeFileAdapter(
                    fakeWriteAllLines: () => throw FileAdapterIOException,
                    fakeWriteAllText: () => throw FileAdapterIOException
                );

        public static IFileAdapter FileAdapterAllMethodsWork
            => new FakeFileAdapter(
                    fakeReadAllLines: () => ContentMultipleLines.ToArray(),
                    fakeReadAllText: () => ContentSingleLine,
                    fakeWriteAllLines: () => { },
                    fakeWriteAllText: () => { }
                );

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 01.07.2022
*/