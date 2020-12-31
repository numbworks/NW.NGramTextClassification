using System;
using System.Collections;
using System.Collections.Generic;

namespace NW.NGrams
{
    public static class Validator
    {

        // Fields
        // Properties
        // Methods (public)
        public static void ValidateObject<T>(object obj, string variableName) where T : Exception
        {

            if (obj == null)
                throw CreateException<T>(variableName);

        }
        public static void ValidateObject(object obj, string variableName)
            => ValidateObject<ArgumentNullException>(obj, variableName);

        public static void ValidateString<T>(string str, string variableName) where T : Exception
        {

            if (string.IsNullOrWhiteSpace(str))
                throw CreateException<T>(variableName);

        }
        public static void ValidateString(string str, string variableName)
            => ValidateString<ArgumentNullException>(str, variableName);

        public static void ValidateList<T, U>(List<U> list, string variableName) where T : Exception
        {

            if (list == null)
                throw CreateException<T>(variableName);
            if (list.Count == 0)
                throw CreateException<T>(MessageCollection.VariableContainsZeroItems.Invoke(variableName));

        }
        public static void ValidateList<U>(List<U> list, string variableName)
            => ValidateList<ArgumentNullException, U>(list, variableName);

        public static void ValidateN<T>(ushort n) where T : Exception
        {

            if (n < 1)
                throw CreateException<T>(MessageCollection.VariableCantBeLessThanOne.Invoke(nameof(n)));

        }
        public static void ValidateN(ushort n)
            => ValidateN<ArgumentException>(n);

        // Methods (private)
        private static T CreateException<T>(string message) where T : Exception
            => (T)Activator.CreateInstance(typeof(T), message);

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 30.12.2020

*/
