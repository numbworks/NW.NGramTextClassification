using System;

namespace NW.NGrams
{
    public class ArraySubsetsManager : IArraySubsetsManager
    {

        // Fields
        // Properties
        // Constructors
        public ArraySubsetsManager() { }

        // Methods
        public string[] GetSubset(string[] arr, uint startIndex, uint length)
        {

            if (arr == null)
                throw new ArgumentNullException(nameof(arr));
            if (arr.Length == 0)
                throw new ArgumentNullException(MessageCollection.VariableContainsZeroItems.Invoke(nameof(arr)));
            if (length < 1)
                throw new ArgumentException(MessageCollection.VariableCantBeLessThanOne.Invoke(nameof(length)));

            string[] subset = new string[length];
            Array.Copy(arr, startIndex, subset, 0, length);

            return subset;

        }

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 29.12.2020

*/