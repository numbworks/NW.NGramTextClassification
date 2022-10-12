using System;

namespace NW.NGramTextClassification.Filenames
{
    /// <summary>Collects all the methods related to create filenames for this library.</summary>
    public interface IFilenameFactory
    {

        /// <summary>
        /// Returns a dated filename based on <paramref name="filePath"/> and <see cref="FilenameFactory.DefaultLabeledExamplesToken"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        string CreateForLabeledExamplesJson(string filePath, DateTime now);

        /// <summary>
        /// Returns a dated filename based on <paramref name="filePath"/> and <see cref="FilenameFactory.DefaultSessionToken"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>        
        string CreateForSessionJson(string filePath, DateTime now);

        /// <summary>
        /// Returns a dated filename based on <paramref name="filePath"/> and <see cref="FilenameFactory.DefaultTextSnippetsToken"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>           
        string CreateForTextSnippetsJson(string filePath, DateTime now);
    
    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 12.10.2022
*/