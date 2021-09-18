using System;
using System.Collections.Generic;
using NW.NGramTextClassification.Validation;

namespace NW.NGramTextClassification.Arrays
{
    /// <inheritdoc cref="IArrayManager"/>
    public class ArrayManager : IArrayManager
    {

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="ArrayManager"/> instance.</summary>
        public ArrayManager() { }

        #endregion

        #region Methods_public

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
            Validator.ThrowIfFirstIsGreaterOrEqual((int)startIndex, nameof(startIndex), arr.Length, "arr.Length");
            Validator.ThrowIfFirstIsGreater((int)length, nameof(length), arr.Length, "arr.Length");
            Validator.ThrowIfFirstIsGreater((int)(startIndex + length), "startIndex + length", arr.Length, "arr.Length");

            string[] subset = new string[length];
            Array.Copy(arr, startIndex, subset, 0, length);

            return subset;

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 17.09.2021
*/
