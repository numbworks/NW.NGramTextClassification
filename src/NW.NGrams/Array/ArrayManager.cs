using System;
using System.Collections.Generic;

namespace NW.NGrams
{
    public class ArrayManager : IArrayManager
    {

        // Fields
        // Properties
        // Constructors
        public ArrayManager() { }

        // Methods
        public string[] AddDelimiter(string[] arr, string delimiter)
        {

            if (arr == null)
                throw new ArgumentNullException(nameof(arr));
            if (arr.Length == 0)
                throw new ArgumentNullException(MessageCollection.VariableContainsZeroItems.Invoke(nameof(arr)));

            List<string> list = new List<string>();
            for (int i = 0; i < arr.Length; i++)
            {
                list.Add(arr[i]);
                if (i != (arr.Length - 1))
                    list.Add(delimiter);

            }

            return list.ToArray();

        }
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