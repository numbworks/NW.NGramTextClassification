using System;
using System.Collections.Generic;
using System.IO;
using NW.NGramTextClassification.Files;

namespace NW.NGramTextClassification.UnitTests.Utilities
{
    public class FakeFileManager : IFileManager
    {

        #region Fields

        #endregion

        #region Properties

        public string Content { get; }

        #endregion

        #region Constructors

        public FakeFileManager(string content)         
        {

            Content = content;

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
        {
            // Writes the content into the file.
        }
        public void WriteAllText(IFileInfoAdapter file, string content)
        {
            // Writes the content into the file.
        }

        #endregion

        #region Methods_private

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 18.08.2021
*/