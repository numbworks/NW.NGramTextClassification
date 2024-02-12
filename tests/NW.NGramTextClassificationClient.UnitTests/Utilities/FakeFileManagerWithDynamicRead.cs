using System;
using System.Collections.Generic;
using System.IO;
using NW.NGramTextClassification.UnitTests.Utilities;
using NW.Shared.Files;

namespace NW.NGramTextClassificationClient.UnitTests.Utilities
{
    public class FakeFileManagerWithDynamicRead : IFileManager
    {

        #region Fields

        #endregion

        #region Properties

        public List<(string fileName, string content)> ReadBehaviours { get; }

        #endregion

        #region Constructors

        public FakeFileManagerWithDynamicRead(List<(string fileName, string content)> readBehaviours)         
        {

            ReadBehaviours = readBehaviours;

        }

        #endregion

        #region Methods_public

        public IEnumerable<string> ReadAllLines(IFileInfoAdapter file)
            => throw new NotImplementedException();
        public string ReadAllText(IFileInfoAdapter file)
        {

            foreach ((string fileName, string content) behaviour in ReadBehaviours)
                if (behaviour.fileName == file.Name)
                    return behaviour.content;

            throw new Exception($"'{file.Name}' not found in '{nameof(ReadBehaviours)}'.");

        }

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
    Last Update: 23.10.2022
*/