using System;
using System.Collections.Generic;
using System.IO;
using NW.Shared.Files;

namespace NW.NGramTextClassification.UnitTests.Utilities
{
    public class FakeFileManagerThrowingWriteExceptions : IFileManager
    {

        #region Fields

        #endregion

        #region Properties

        public string Content { get; }
        public string WriteExceptionMessage { get; }

        #endregion

        #region Constructors

        public FakeFileManagerThrowingWriteExceptions(string content, string writeExceptionMessage)
        {

            Content = content;
            WriteExceptionMessage = writeExceptionMessage;

        }

        #endregion

        #region Methods_public

        public IEnumerable<string> ReadAllLines(IFileInfoAdapter file)
            => Content.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        public string ReadAllText(IFileInfoAdapter file)
            => Content;

        public IFileInfoAdapter Create(string filePath)
            => new FakeFileInfoAdapter(true, filePath);
        public IFileInfoAdapter Create(FileInfo fileInfo)
            => new FakeFileInfoAdapter(true, fileInfo.FullName);

        public void WriteAllLines(IFileInfoAdapter file, IEnumerable<string> content)
            => throw new Exception(WriteExceptionMessage);
        public void WriteAllText(IFileInfoAdapter file, string content)
            => throw new Exception(WriteExceptionMessage);

        #endregion

        #region Methods_private

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 15.10.2022
*/