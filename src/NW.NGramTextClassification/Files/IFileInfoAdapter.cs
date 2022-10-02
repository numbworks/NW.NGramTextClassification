using System;
using System.IO;
using System.Runtime.Serialization;

namespace NW.NGramTextClassification.Files
{
    /// <summary>Adapter for <see cref="FileInfo"/>.</summary>
    public interface IFileInfoAdapter
    {

        /// <summary>
        /// Gets or sets the creation time of the current file or directory.
        /// </summary>
        DateTime CreationTime { get; set; }

        /// <summary>
        /// Gets or sets the creation time, in coordinated universal time (UTC), of the current
        /// file or directory.
        /// </summary> 
        DateTime CreationTimeUtc { get; set; }

        /// <summary>
        /// Gets or sets the time the current file or directory was last accessed.
        /// </summary>
        DateTime LastAccessTime { get; set; }

        /// <summary>
        /// Gets or sets the time, in coordinated universal time (UTC), that the current
        /// file or directory was last accessed.
        /// </summary>        
        DateTime LastAccessTimeUtc { get; set; }

        /// <summary>
        /// Gets or sets the time when the current file or directory was last written to.
        /// </summary>        
        DateTime LastWriteTime { get; set; }

        /// <summary>
        /// Gets or sets the time, in coordinated universal time (UTC), when the current
        /// file or directory was last written to.
        /// </summary>        
        DateTime LastWriteTimeUtc { get; set; }

        /// <summary>
        /// Gets the string representing the extension part of the file.
        /// </summary>        
        string Extension { get; }

        /// <summary>
        /// Gets the full path of the directory or file.
        /// </summary>        
        string FullName { get; }

        /// <summary>
        /// Gets or sets a value that determines if the current file is read only.
        /// </summary>
        bool IsReadOnly { get; set; }

        /// <summary>
        /// Gets a value indicating whether a file exists.
        /// </summary>
        bool Exists { get; }

        /// <summary>
        /// Gets a string representing the directory's full path.
        /// </summary>
        string DirectoryName { get; }

        /// <summary>
        /// Gets an instance of the parent directory.
        /// </summary>
        DirectoryInfo Directory { get; }

        /// <summary>
        /// Gets the size, in bytes, of the current file.
        /// </summary>
        long Length { get; }

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        FileAttributes Attributes { get; set; }

        /// <summary>
        /// Creates a <see cref="StreamWriter"/> that appends text to the file represented by
        /// this instance of the <see cref="FileInfo"/>.
        /// </summary>
        StreamWriter AppendText();

        /// <summary>
        /// Copies an existing file to a new file, disallowing the overwriting of an existing file.
        /// </summary>
        FileInfo CopyTo(string destFileName);

        /// <summary>
        /// Copies an existing file to a new file, allowing the overwriting of an existing file.
        /// </summary> 
        FileInfo CopyTo(string destFileName, bool overwrite);

        /// <summary>
        /// Creates a file.
        /// </summary>
        FileStream Create();

        /// <summary>
        /// Creates a <see cref="StreamWriter"/> that writes a new text file.
        /// </summary>
        StreamWriter CreateText();

        /// <summary>
        /// Decrypts a file that was encrypted by the current account using the <see cref="FileInfo.Encrypt"/> method.
        /// </summary>
        void Decrypt();

        /// <summary>
        /// Permanently deletes a file.
        /// </summary>
        void Delete();

        /// <summary>
        /// Encrypts a file so that only the account used to encrypt the file can decrypt it.
        /// </summary>
        void Encrypt();

        /// <summary>
        /// Moves a specified file to a new location, providing the option to specify a new file name.
        /// </summary>
        void MoveTo(string destFileName);

        /// <summary>
        /// Opens a file in the specified mode.
        /// </summary>        
        FileStream Open(FileMode mode);

        /// <summary>
        /// Opens a file in the specified mode with read, write, or read/write access.
        /// </summary>
        FileStream Open(FileMode mode, FileAccess access);

        /// <summary>
        /// Opens a file in the specified mode with read, write, or read/write access and
        /// the specified sharing option.
        /// </summary>
        FileStream Open(FileMode mode, FileAccess access, FileShare share);

        /// <summary>
        /// Creates a read-only <see cref="FileStream"/>.
        /// </summary>
        FileStream OpenRead();

        /// <summary>
        /// Creates a <see cref="StreamReader"/> with UTF8 encoding that reads from an existing
        /// text file.
        /// </summary>
        StreamReader OpenText();

        /// <summary>
        /// Creates a write-only <see cref="FileStream"/>.
        /// </summary> 
        FileStream OpenWrite();

        /// <summary>
        /// Replaces the contents of a specified file with the file described by the current
        /// <see cref="FileInfo"/> object, deleting the original file, and creating a backup
        /// of the replaced file.
        /// </summary>
        FileInfo Replace(string destinationFileName, string destinationBackupFileName);

        /// <summary>
        /// Replaces the contents of a specified file with the file described by the current
        /// <see cref="FileInfo"/> object, deleting the original file, and creating a backup
        /// of the replaced file. Also specifies whether to ignore merge errors.
        /// </summary>
        FileInfo Replace(string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors);

        /// <summary>
        /// Refreshes the state of the object.
        /// </summary>
        void Refresh();

        /// <summary>
        /// Sets the <see cref="SerializationInfo"/> object with the file
        /// name and additional exception information.
        /// </summary>
        void GetObjectData(SerializationInfo info, StreamingContext context);

        string ToString();

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 30.05.2021
*/