namespace NW.NGrams
{
    public interface IArrayDelimiterManager
    {

        /// <summary>
        /// It adds a provided delimiter among the items of the provided array.
        /// For ex., if the delimiter is " ": [ "This", "is", "a" ] => [ "This", " ", "is", " ", "a" ].
        /// A 'null' delimiter is a valid delimiter.
        /// </summary>
        string[] AddDelimiter(string[] arr, string delimiter);

    }
}

/*
 *
 *  Author: numbworks@gmail.com
 *  Last Update: 02.02.2018 
 *  Description: It collects some useful methods related to delimiters among arrays' items.
 * 
 */