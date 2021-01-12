using System;
using System.Collections.Generic;

namespace NW.NGramTextClassification
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

            Validator.ValidateArray(arr, nameof(arr));
            Validator.ValidateStringNullOrEmpty(delimiter, nameof(delimiter)); // Whitespace is a valid delimiter

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

            Validator.ValidateArray(arr, nameof(arr));
            Validator.ValidateLength(length);

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