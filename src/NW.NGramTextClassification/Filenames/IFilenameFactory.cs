using System;

namespace NW.NGramTextClassification.Filenames
{
    /// <summary>Collects all the methods related to create filenames for this library.</summary>
    public interface IFilenameFactory
    {

        /// <summary>
        /// Returns a dated filename based on <paramref name="folderPath"/> and <see cref="FilenameFactory.DefaultLabeledExamplesToken"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        string CreateForLabeledExamplesJson(string folderPath, DateTime now);

        /// <summary>
        /// Returns a dated filename based on <paramref name="folderPath"/> and <see cref="FilenameFactory.DefaultSessionToken"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>        
        string CreateForSessionJson(string folderPath, DateTime now);

        /// <summary>
        /// Returns a dated filename based on <paramref name="folderPath"/> and <see cref="FilenameFactory.DefaultTextSnippetsToken"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>           
        string CreateForTextSnippetsJson(string folderPath, DateTime now);
    
    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 13.10.2022
*/