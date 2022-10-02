using System;
using System.IO;
using System.Runtime.Serialization;
using NW.NGramTextClassification.Validation;

namespace NW.NGramTextClassification.Files
{
    /// <inheritdoc cref="IFileInfoAdapter"/>
    public class FileInfoAdapter : IFileInfoAdapter
    {

        #region Fields

        private FileInfo _fileInfo;

        #endregion

        #region Properties

        public bool IsReadOnly
        {
            get { return _fileInfo.IsReadOnly; }
            set { _fileInfo.IsReadOnly = value; }
        }
        public bool Exists
        {
            get { return _fileInfo.Exists; }
        }
        public string DirectoryName
        {
            get { return _fileInfo.DirectoryName; }
        }
        public DirectoryInfo Directory
        {
            get { return _fileInfo.Directory; }
        }
        public long Length
        {
            get { return _fileInfo.Length; }
        }
        public string Name
        {
            get { return _fileInfo.Name; }
        }
        public DateTime LastWriteTime
        {
            get { return _fileInfo.LastWriteTime; }
            set { _fileInfo.LastWriteTime = value; }
        }
        public DateTime LastAccessTimeUtc
        {
            get { return _fileInfo.LastAccessTimeUtc; }
            set { _fileInfo.LastAccessTimeUtc = value; }
        }
        public DateTime LastAccessTime
        {
            get { return _fileInfo.LastAccessTime; }
            set { _fileInfo.LastAccessTime = value; }
        }
        public string FullName
        {
            get { return _fileInfo.FullName; }
        }
        public string Extension
        {
            get { return _fileInfo.Extension; }
        }
        public DateTime CreationTime
        {
            get { return _fileInfo.CreationTime; }
            set { _fileInfo.CreationTime = value; }
        }
        public DateTime LastWriteTimeUtc
        {
            get { return _fileInfo.LastWriteTimeUtc; }
            set { _fileInfo.LastWriteTimeUtc = value; }
        }
        public FileAttributes Attributes
        {
            get { return _fileInfo.Attributes; }
            set { _fileInfo.Attributes = value; }
        }
        public DateTime CreationTimeUtc
        {
            get { return _fileInfo.CreationTimeUtc; }
            set { _fileInfo.CreationTimeUtc = value; }
        }

        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="FileInfoAdapter"/> instance.</summary>
        /// <exception cref="ArgumentNullException"/>
        public FileInfoAdapter(FileInfo fileInfo)
        {

            Validator.ValidateObject(fileInfo, nameof(fileInfo));

            _fileInfo = fileInfo;

        }

        /// <summary>Initializes a <see cref="FileInfoAdapter"/> instance.</summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="UnauthorizedAccessException"/>
        /// <exception cref="PathTooLongException"/>
        /// <exception cref="NotSupportedException"/>
        public FileInfoAdapter(string fileName)
        {

            _fileInfo = new FileInfo(fileName);

        }

        #endregion

        #region Methods_public

        public StreamWriter AppendText()
            => _fileInfo.AppendText();
        public FileInfo CopyTo(string destFileName)
            => _fileInfo.CopyTo(destFileName);
        public FileInfo CopyTo(string destFileName, bool overwrite)
            => _fileInfo.CopyTo(destFileName, overwrite);
        public FileStream Create()
            => _fileInfo.Create();
        public StreamWriter CreateText()
            => _fileInfo.CreateText();
        public void Decrypt()
            => _fileInfo.Decrypt();
        public void Delete()
            => _fileInfo.Delete();
        public void Encrypt()
            => _fileInfo.Encrypt();
        public void MoveTo(string destFileName)
            => _fileInfo.MoveTo(destFileName);
        public FileStream Open(FileMode mode, FileAccess access, FileShare share)
            => _fileInfo.Open(mode, access, share);
        public FileStream Open(FileMode mode, FileAccess access)
            => _fileInfo.Open(mode, access);
        public FileStream Open(FileMode mode)
            => _fileInfo.Open(mode);
        public FileStream OpenRead()
            => _fileInfo.OpenRead();
        public StreamReader OpenText()
            => _fileInfo.OpenText();
        public FileStream OpenWrite()
            => _fileInfo.OpenWrite();
        public FileInfo Replace(string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors)
            => _fileInfo.Replace(destinationFileName, destinationBackupFileName, ignoreMetadataErrors);
        public FileInfo Replace(string destinationFileName, string destinationBackupFileName)
            => _fileInfo.Replace(destinationFileName, destinationBackupFileName);
        public override string ToString()
            => _fileInfo.ToString();
        public void GetObjectData(SerializationInfo info, StreamingContext context)
            => _fileInfo.GetObjectData(info, context);
        public void Refresh()
            => _fileInfo.Refresh();

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 08.10.2021
*/