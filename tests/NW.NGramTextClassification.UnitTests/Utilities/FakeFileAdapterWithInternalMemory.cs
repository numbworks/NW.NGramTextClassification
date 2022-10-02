using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NW.NGramTextClassification.Files;

namespace NW.NGramTextClassification.UnitTests.Utilities
{
    public class FakeFileAdapterWithInternalMemory : IFileAdapter
    {

        #region Fields

        private string[] _AllLines;
        private string _AllText;

        #endregion

        #region Properties
        #endregion

        #region Constructors

        public FakeFileAdapterWithInternalMemory() { }

        #endregion

        #region Methods_public

        public string[] ReadAllLines(string path)
            => _AllLines;
        public string ReadAllText(string path)
            => _AllText;
        public void WriteAllLines(string path, IEnumerable<string> contents)
            => _AllLines = contents.ToArray();
        public void WriteAllText(string path, string contents)
            => _AllText = contents;

        public void AppendAllLines(string path, IEnumerable<string> contents)
            => throw new NotImplementedException();
        public void AppendAllLines(string path, IEnumerable<string> contents, Encoding encoding)
            => throw new NotImplementedException();
        public void AppendAllText(string path, string contents)
            => throw new NotImplementedException();
        public void AppendAllText(string path, string contents, Encoding encoding)
            => throw new NotImplementedException();
        public string[] ReadAllLines(string path, Encoding encoding)
            => throw new NotImplementedException();
        public string ReadAllText(string path, Encoding encoding)
            => throw new NotImplementedException();
        public void WriteAllLines(string path, IEnumerable<string> contents, Encoding encoding)
            => throw new NotImplementedException();
        public void WriteAllText(string path, string contents, Encoding encoding)
            => throw new NotImplementedException();

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 08.10.2021
*/