using System;
using System.IO;
using System.Runtime.Serialization;
using NW.NGramTextClassification.Files;

namespace NW.NGramTextClassification.UnitTests.Utilities
{
    public class FakeFileInfoAdapter : IFileInfoAdapter
    {

        #region Fields
        #endregion

        #region Properties

        public FileAttributes Attributes
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
        public DateTime CreationTime
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
        public DateTime CreationTimeUtc
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
        public DirectoryInfo Directory
            => throw new NotImplementedException();
        public string DirectoryName
            => new FileInfo(FullName).DirectoryName;
        public string Extension
            => throw new NotImplementedException();
        public bool IsReadOnly
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
        public DateTime LastAccessTime
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
        public DateTime LastAccessTimeUtc
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
        public DateTime LastWriteTime
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
        public DateTime LastWriteTimeUtc
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
        public long Length
            => throw new NotImplementedException();
        public string Name
            => new FileInfo(FullName).Name;

        public bool Exists { get; }
        public string FullName { get; }

        #endregion

        #region Constructors

        public FakeFileInfoAdapter(bool exists, string fullName)
        {

            Exists = exists;
            FullName = fullName;

        }

        #endregion

        #region Methods_public

        public StreamWriter AppendText()
            => throw new NotImplementedException();
        public FileInfo CopyTo(string destFileName)
            => throw new NotImplementedException();
        public FileInfo CopyTo(string destFileName, bool overwrite)
            => throw new NotImplementedException();
        public FileStream Create()
            => throw new NotImplementedException();
        public StreamWriter CreateText()
            => throw new NotImplementedException();
        public void Decrypt()
            => throw new NotImplementedException();
        public void Delete()
            => throw new NotImplementedException();
        public void Encrypt()
            => throw new NotImplementedException();
        public void GetObjectData(SerializationInfo info, StreamingContext context)
            => throw new NotImplementedException();
        public void MoveTo(string destFileName)
            => throw new NotImplementedException();
        public FileStream Open(FileMode mode)
            => throw new NotImplementedException();
        public FileStream Open(FileMode mode, FileAccess access)
            => throw new NotImplementedException();
        public FileStream Open(FileMode mode, FileAccess access, FileShare share)
            => throw new NotImplementedException();
        public FileStream OpenRead()
            => throw new NotImplementedException();
        public StreamReader OpenText()
            => throw new NotImplementedException();
        public FileStream OpenWrite()
            => throw new NotImplementedException();
        public void Refresh()
            => throw new NotImplementedException();
        public FileInfo Replace(string destinationFileName, string destinationBackupFileName)
            => throw new NotImplementedException();
        public FileInfo Replace(string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors)
            => throw new NotImplementedException();

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 08.10.2021
*/