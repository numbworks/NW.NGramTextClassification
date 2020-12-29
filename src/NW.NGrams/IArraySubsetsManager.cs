namespace NW.NGrams
{
    public interface IArraySubsetsManager
    {

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