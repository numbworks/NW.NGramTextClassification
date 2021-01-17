using System;
using System.Collections.Generic;

namespace NW.NGramTextClassification
{
    public static class Validator
    {

        // Fields
        // Properties
        // Methods (public)
        public static void ValidateLength<T>(uint length) where T : Exception
        {

            if (length < 1)
                throw CreateException<T>(MessageCollection.VariableCantBeLessThanOne.Invoke(nameof(length)));

        }
        public static void ValidateLength(uint length)
            => ValidateLength<ArgumentException>(length);

        public static void ValidateObject<T>(object obj, string variableName) where T : Exception
        {

            if (obj == null)
                throw CreateException<T>(variableName);

        }
        public static void ValidateObject(object obj, string variableName)
            => ValidateObject<ArgumentNullException>(obj, variableName);

        public static void ValidateArray<U>(U[] arr, string variableName)
        {

            ValidateArrayNull<ArgumentNullException, U>(arr, variableName);
            ValidateArrayEmpty<ArgumentException, U>(arr, variableName);

        }
        public static void ValidateArrayNull<T, U>(U[] arr, string variableName) where T : Exception
        {

            if (arr == null)
                throw CreateException<T>(variableName);

        }
        public static void ValidateArrayEmpty<T, U>(U[] arr, string variableName) where T : Exception
        {

            if (arr.Length == 0)
                throw CreateException<T>(MessageCollection.VariableContainsZeroItems.Invoke(variableName));

        }

        public static void ValidateList<U>(List<U> list, string variableName)
        {

            ValidateListNull<ArgumentNullException, U>(list, variableName);
            ValidateListEmpty<ArgumentException, U>(list, variableName);

        }
        public static void ValidateListNull<T, U>(List<U> list, string variableName) where T : Exception
        {

            if (list == null)
                throw CreateException<ArgumentNullException>(variableName);

        }
        public static void ValidateListEmpty<T, U>(List<U> list, string variableName) where T : Exception
        {

            if (list.Count == 0)
                throw CreateException<ArgumentException>(MessageCollection.VariableContainsZeroItems.Invoke(variableName));

        }

        public static void ValidateN<T>(ushort n) where T : Exception
        {

            if (n < 1)
                throw CreateException<T>(MessageCollection.VariableCantBeLessThanOne.Invoke(nameof(n)));

        }
        public static void ValidateN(ushort n)
            => ValidateN<ArgumentException>(n);

        public static void ValidateStringNullOrWhiteSpace<T>(string str, string variableName) where T : Exception
        {

            if (string.IsNullOrWhiteSpace(str))
                throw CreateException<T>(variableName);

        }
        public static void ValidateStringNullOrWhiteSpace(string str, string variableName)
            => ValidateStringNullOrWhiteSpace<ArgumentNullException>(str, variableName);
        public static void ValidateStringNullOrEmpty<T>(string str, string variableName) where T : Exception
        {

            if (string.IsNullOrEmpty(str))
                throw CreateException<T>(variableName);

        }
        public static void ValidateStringNullOrEmpty(string str, string variableName)
            => ValidateStringNullOrEmpty<ArgumentNullException>(str, variableName);

        public static void ThrowIfFirstIsGreaterOrEqual<T>(int value1, string variableName1, int value2, string variableName2) where T : Exception
        {

            if (value1 >= value2)
                throw CreateException<T>(MessageCollection.FirstValueIsGreaterOrEqualThanSecondValue.Invoke(variableName1, variableName2));

        }
        public static void ThrowIfFirstIsGreaterOrEqual(int value1, string variableName1, int value2, string variableName2)
            => ThrowIfFirstIsGreaterOrEqual<ArgumentException>(value1, variableName1, value2, variableName2);
        public static void ThrowIfFirstIsGreater<T>(int value1, string variableName1, int value2, string variableName2) where T : Exception
        {

            if (value1 >= value2)
                throw CreateException<T>(MessageCollection.FirstValueIsGreaterThanSecondValue.Invoke(variableName1, variableName2));

        }
        public static void ThrowIfFirstIsGreater(int value1, string variableName1, int value2, string variableName2)
            => ThrowIfFirstIsGreater<ArgumentException>(value1, variableName1, value2, variableName2);

        // Methods (private)
        private static T CreateException<T>(string message) where T : Exception
            => (T)Activator.CreateInstance(typeof(T), message);

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 01.01.2020

*/
