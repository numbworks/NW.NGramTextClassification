using System;
using System.Collections.Generic;
using System.Linq;

namespace NW.NGramTextClassification
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

        public static void ValidateArray<T>(string[] arr, string variableName) where T : Exception
        {

            if (arr == null)
                throw CreateException<T>(variableName);
            if (arr.Length == 0)
                throw CreateException<T>(MessageCollection.VariableContainsZeroItems.Invoke(variableName));

        }
        public static void ValidateArray(string[] arr, string variableName)
            => ValidateArray<ArgumentNullException>(arr, variableName);

        public static void ValidateLength<T>(uint length) where T : Exception
        {

            if (length < 1)
                throw CreateException<T>(MessageCollection.VariableCantBeLessThanOne.Invoke(nameof(length)));

        }
        public static void ValidateLength(uint length)
            => ValidateLength<ArgumentException>(length);

        public static void ValidateSimilarityIndexAverages(List<SimilarityIndexAverage> indexAverages)
        {

            if (HasOnlyZeros(indexAverages))
                throw new Exception(MessageCollection.TheMethodDidntReturnExpectedOutcome.Invoke(nameof(HasOnlyZeros), true));

            if (HasDuplicates(indexAverages))
                throw new Exception(MessageCollection.TheMethodDidntReturnExpectedOutcome.Invoke(nameof(HasDuplicates), true));

        }

        // Methods (private)
        private static T CreateException<T>(string message) where T : Exception
            => (T)Activator.CreateInstance(typeof(T), message);
        private static bool HasOnlyZeros(List<SimilarityIndexAverage> indexAverages)
        {

            /*
             *
             * Label    Average
             * sv       0
             * en       0
             * 
             * 		=> { 0, 0 } 
             * 		=> true
             * 
             */

            if (indexAverages.Where(Item => Item.Value == 0).Count() == indexAverages.Count)
                return true;

            return false;

        }
        private static bool HasDuplicates(List<SimilarityIndexAverage> indexAverages)
        {

            /*
             *
             * Label    Average
             * sv       0.1
             * en       0.1
             * dk       0.1
             * 
             * 		=> { 0.1, 0.1, 0.1 } 
             * 		=> 1 
             * 		=> 1 != 3 
             * 		=> true
             * 
             */

            if (indexAverages.Select(Item => Item.Value).Distinct().Count() == indexAverages.Count)
                return false;

            return true;

        }


    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 31.12.2020

*/
