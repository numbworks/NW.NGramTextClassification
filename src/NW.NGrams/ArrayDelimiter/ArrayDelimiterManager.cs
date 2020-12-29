using System;
using System.Collections.Generic;

namespace NW.NGrams
{
    public class ArrayDelimiterManager : IArrayDelimiterManager
    {

        // Fields
        // Properties
        // Constructors
        public ArrayDelimiterManager() { }

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

    }
}

/*
 *
 *  Author: numbworks@gmail.com
 *  Last Update: 02.02.2018 
 *  Description: It collects some useful methods related to delimiters among arrays' items.
 * 
 */
