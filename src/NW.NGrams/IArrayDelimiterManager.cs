using RUBN.Shared;

namespace NW.NGrams
{
    public interface IArrayDelimiterManager
    {

        IParametersValidator ParametersValidator { get; set; }

        /// <summary>
        /// It adds a provided delimiter among the items of the provided array.
        /// For ex., if the delimiter is " ": [ "This", "is", "a" ] => [ "This", " ", "is", " ", "a" ].
        /// A 'null' delimiter is a valid delimiter.
        /// </summary>
        Outcome AddDelimiter(string[] arr, string strDelimiter);
    }
}

/*
 *
 *  Author: numbworks@gmail.com
 *  Last Update: 02.02.2018 
 * 
 */
