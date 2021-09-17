using System;

namespace NW.NGramTextClassification.Arrays
{
    /// <summary>Collects all the utility methods related to array management.</summary>
    public interface IArrayManager
    {

        /// <summary>
        /// It adds a provided delimiter among the items of the provided array. 'null' is a valid delimiter.
        /// <para>Example: if the delimiter is " ", an array like [ "This", "is", "a" ] becomes [ "This", " ", "is", " ", "a" ].</para>
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/> 
        string[] AddDelimiter(string[] arr, string delimiter);

        /// <summary>
        /// It creates a subset of the provided length for the provided array. 
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>  
        string[] GetSubset(string[] arr, uint startIndex, uint length);

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 17.09.2021
*/