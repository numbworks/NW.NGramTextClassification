using RUBN.Shared;

namespace NW.NGrams
{
    public interface IArraySubsetsManager
    {

        IParametersValidator ParametersValidator { get; set; }

        /// <summary>
        /// It creates a subset of the provided length for the provided array. 
        /// </summary>
        Outcome GetSubset(string[] arr, int intStartIndex, int intLength);
    }
}

/*
 *
 *  Author: numbworks@gmail.com
 *  Last Update: 20.01.2018 
 * 
 */
