using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NW.NGramTextClassification.Files
{
    /// <inheritdoc cref="IFileAdapter"/>
    public class FileAdapter : IFileAdapter
    {

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="FileAdapter"/> instance.</summary>
        public FileAdapter() { }

        #endregion

        #region Methods_public

        public void AppendAllLines(string path, IEnumerable<string> contents)
            => File.AppendAllLines(path, contents);
        public void AppendAllLines(string path, IEnumerable<string> contents, Encoding encoding)
            => File.AppendAllLines(path, contents, encoding);
        public void AppendAllText(string path, string contents)
            => File.AppendAllText(path, contents);
        public void AppendAllText(string path, string contents, Encoding encoding)
            => File.AppendAllText(path, contents, encoding);
        public string[] ReadAllLines(string path)
            => File.ReadAllLines(path);
        public string[] ReadAllLines(string path, Encoding encoding)
            => File.ReadAllLines(path, encoding);
        public string ReadAllText(string path)
            => File.ReadAllText(path);
        public string ReadAllText(string path, Encoding encoding)
            => File.ReadAllText(path, encoding);
        public void WriteAllLines(string path, IEnumerable<string> contents)
            => File.WriteAllLines(path, contents);
        public void WriteAllLines(string path, IEnumerable<string> contents, Encoding encoding)
            => File.WriteAllLines(path, contents, encoding);
        public void WriteAllText(string path, string contents)
            => File.WriteAllText(path, contents);
        public void WriteAllText(string path, string contents, Encoding encoding)
            => File.WriteAllText(path, contents, encoding);

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 30.05.2021
*/