using System.Collections.Generic;
using System.Text;
using System.IO;

namespace NW.NGramTextClassification.Files
{
    /// <summary>Adapter for <see cref="File"/>.</summary>
    public interface IFileAdapter
    {

        /// <summary>
        /// Appends lines to a file, and then closes the file. 
        /// If the specified file does not exist, this method creates a file, 
        /// writes the specified lines to the file, and then closes the file.
        /// </summary>
        void AppendAllLines(string path, IEnumerable<string> contents);

        /// <summary>
        /// Appends lines to a file by using a specified encoding, and then closes the file.
        /// If the specified file does not exist, this method creates a file, writes the
        /// specified lines to the file, and then closes the file.
        /// </summary>        
        void AppendAllLines(string path, IEnumerable<string> contents, Encoding encoding);

        /// <summary>
        /// Opens a file, appends the specified string to the file, and then closes the file.
        /// If the file does not exist, this method creates a file, writes the specified
        /// string to the file, then closes the file.
        /// </summary>  
        void AppendAllText(string path, string contents);

        /// <summary>
        /// Appends the specified string to the file, creating the file if it does not already exist.
        /// </summary>      
        void AppendAllText(string path, string contents, Encoding encoding);

        /// <summary>
        /// Opens a text file, reads all lines of the file, and then closes the file.
        /// </summary>
        string[] ReadAllLines(string path);

        /// <summary>
        /// Opens a file, reads all lines of the file with the specified encoding, and then closes the file.
        /// </summary>
        string[] ReadAllLines(string path, Encoding encoding);

        /// <summary>
        /// Opens a text file, reads all lines of the file, and then closes the file.
        /// </summary>
        string ReadAllText(string path);

        /// <summary>
        /// Opens a file, reads all lines of the file with the specified encoding, and then closes the file.
        /// </summary>
        string ReadAllText(string path, Encoding encoding);

        /// <summary>
        /// Creates a new file, writes a collection of strings to the file, and then closes the file.
        /// </summary>
        void WriteAllLines(string path, IEnumerable<string> contents);

        /// <summary>
        /// Creates a new file by using the specified encoding, writes a collection of strings 
        /// to the file, and then closes the file.
        /// </summary>
        void WriteAllLines(string path, IEnumerable<string> contents, Encoding encoding);

        /// <summary>
        /// Creates a new file, writes the specified string to the file, and then closes
        /// the file. If the target file already exists, it is overwritten.
        /// </summary>
        void WriteAllText(string path, string contents);

        /// <summary>
        /// Creates a new file, writes the specified string to the file using the specified
        /// encoding, and then closes the file. If the target file already exists, it is
        /// overwritten.
        /// </summary>
        void WriteAllText(string path, string contents, Encoding encoding);

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 30.05.2021
*/