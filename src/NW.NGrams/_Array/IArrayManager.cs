namespace NW.NGrams
{
    public interface IArrayManager
    {

        /// <summary>
        /// It adds a provided delimiter among the items of the provided array.
        /// For ex., if the delimiter is " ": [ "This", "is", "a" ] => [ "This", " ", "is", " ", "a" ].
        /// A 'null' delimiter is a valid delimiter.
        /// </summary>
        string[] AddDelimiter(string[] arr, string delimiter);

        /// <summary>
        /// It creates a subset of the provided length for the provided array. 
        /// </summary>
        string[] GetSubset(string[] arr, uint startIndex, uint length);

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 29.12.2020

*/