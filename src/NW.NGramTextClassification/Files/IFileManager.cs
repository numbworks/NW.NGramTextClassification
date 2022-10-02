using System.Collections.Generic;
using System.IO;
using System;

namespace NW.NGramTextClassification.Files
{
    /// <summary>Collects all the helper methods related to files' management.</summary>
    public interface IFileManager
    {

        /// <summary>
        /// Opens a text file, reads all lines of the file, and then closes the file.
        /// </summary>
        /// <exception cref="Exception"></exception>
        IEnumerable<string> ReadAllLines(IFileInfoAdapter file);

        /// <summary>
        /// Opens a text file, reads all lines of the file, and then closes the file.
        /// </summary>
        /// <exception cref="Exception"></exception>
        string ReadAllText(IFileInfoAdapter file);

        /// <summary>
        /// Creates a new file, writes a collection of strings to the file, and then closes the file.
        /// </summary>
        /// <exception cref="Exception"></exception> 
        void WriteAllLines(IFileInfoAdapter file, IEnumerable<string> content);

        /// <summary>
        /// Creates a new file, writes the specified string to the file, and then closes
        /// the file. If the target file already exists, it is overwritten.
        /// </summary>
        /// <exception cref="Exception"></exception>  
        void WriteAllText(IFileInfoAdapter file, string content);

        /// <summary>
        /// Initializes a <see cref="IFileInfoAdapter"/> instance for <paramref name="filePath"/>.
        /// </summary>    
        IFileInfoAdapter Create(string filePath);

        /// <summary>
        /// Initializes a <see cref="IFileInfoAdapter"/> instance for <paramref name="fileInfo"/>.
        /// </summary>        
        IFileInfoAdapter Create(FileInfo fileInfo);

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 30.05.2021
*/