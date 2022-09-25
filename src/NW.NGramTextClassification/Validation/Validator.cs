using System;
using System.Collections.Generic;
using NW.NGramTextClassification.Messages;

namespace NW.NGramTextClassification.Validation
{
    /// <summary>Collects all the validation methods.</summary>
    public static class Validator
    {

        #region ValidateLength

        /// <summary>Throws an exception of type <see cref="TException"/> when <paramref name="length"/> is less than one.</summary>
        public static void ValidateLength<TException>(uint length) where TException : Exception
        {

            if (length < 1)
                throw CreateException<TException>(MessageCollection.Validator_VariableCantBeLessThanOne(nameof(length)));

        }

        /// <summary>Throws an exception of type <see cref="ArgumentException"/> when <paramref name="length"/> is less than one.</summary>        
        public static void ValidateLength(uint length)
            => ValidateLength<ArgumentException>(length);

        #endregion

        #region ValidateObject

        /// <summary>Throws an exception of type <see cref="TException"/> when <paramref name="obj"/> is null.</summary>
        public static void ValidateObject<TException>(object obj, string variableName) where TException : Exception
        {

            if (obj == null)
                throw CreateException<TException>(variableName);

        }

        /// <summary>Throws an exception of type <see cref="ArgumentNullException"/> when <paramref name="obj"/> is null.</summary>
        public static void ValidateObject(object obj, string variableName)
            => ValidateObject<ArgumentNullException>(obj, variableName);

        #endregion

        #region ValidateArray

        /// <summary>Throws an exception of type <see cref="ArgumentNullException"/> when <paramref name="arr"/> is null or of type <see cref="ArgumentException"/> when <paramref name="arr"/> is empty.</summary>
        public static void ValidateArray<UArray>(UArray[] arr, string variableName)
        {

            ValidateArrayNull<ArgumentNullException, UArray>(arr, variableName);
            ValidateArrayEmpty<ArgumentException, UArray>(arr, variableName);

        }

        /// <summary>Throws an exception of type <see cref="ArgumentNullException"/> when <paramref name="arr"/> is null.</summary>
        public static void ValidateArrayNull<TException, UArray>(UArray[] arr, string variableName) where TException : Exception
        {

            if (arr == null)
                throw CreateException<TException>(variableName);

        }

        /// <summary>Throws an exception of type <see cref="ArgumentException"/> when <paramref name="arr"/> is empty.</summary>
        public static void ValidateArrayEmpty<TException, UArray>(UArray[] arr, string variableName) where TException : Exception
        {

            if (arr.Length == 0)
                throw CreateException<TException>(MessageCollection.Validator_VariableContainsZeroItems(variableName));

        }

        #endregion

        #region ValidateList

        /// <summary>Throws an exception of type <see cref="ArgumentNullException"/> when <paramref name="list"/> is null or of type <see cref="ArgumentException"/> when <paramref name="list"/> is empty.</summary>
        public static void ValidateList<UList>(List<UList> list, string variableName)
        {

            ValidateListNull<ArgumentNullException, UList>(list, variableName);
            ValidateListEmpty<ArgumentException, UList>(list, variableName);

        }

        /// <summary>Throws an exception of type <see cref="ArgumentNullException"/> when <paramref name="arr"/> is null.</summary>        
        public static void ValidateListNull<TException, UList>(List<UList> list, string variableName) where TException : Exception
        {

            if (list == null)
                throw CreateException<ArgumentNullException>(variableName);

        }

        /// <summary>Throws an exception of type <see cref="ArgumentException"/> when <paramref name="list"/> is empty.</summary>        
        public static void ValidateListEmpty<T, UList>(List<UList> list, string variableName) where T : Exception
        {

            if (list.Count == 0)
                throw CreateException<ArgumentException>(MessageCollection.Validator_VariableContainsZeroItems(variableName));

        }

        #endregion

        #region ValidateStringNullOrWhiteSpace

        /// <summary>Throws an exception of type <see cref="TException"/> when <paramref name="str"/> is null or whitespace.</summary> 
        public static void ValidateStringNullOrWhiteSpace<TException>(string str, string variableName) where TException : Exception
        {

            if (string.IsNullOrWhiteSpace(str))
                throw CreateException<TException>(variableName);

        }

        /// <summary>Throws an exception of type <see cref="ArgumentNullException"/> when <paramref name="str"/> is null or whitespace.</summary>         
        public static void ValidateStringNullOrWhiteSpace(string str, string variableName)
            => ValidateStringNullOrWhiteSpace<ArgumentNullException>(str, variableName);

        /// <summary>Throws an exception of type <see cref="TException"/> when <paramref name="str"/> is null or empty.</summary>
        public static void ValidateStringNullOrEmpty<TException>(string str, string variableName) where TException : Exception
        {

            if (string.IsNullOrEmpty(str))
                throw CreateException<TException>(variableName);

        }

        /// <summary>Throws an exception of type <see cref="ArgumentNullException"/> when <paramref name="str"/> is null or empty.</summary>  
        public static void ValidateStringNullOrEmpty(string str, string variableName)
            => ValidateStringNullOrEmpty<ArgumentNullException>(str, variableName);

        #endregion

        #region ThrowIfFirst

        /// <summary>Throws an exception of type <see cref="TException"/> when <paramref name="value1"/> is greater or equal than <paramref name="value2"/>.</summary> 
        public static void ThrowIfFirstIsGreaterOrEqual<TException>(int value1, string variableName1, int value2, string variableName2) where TException : Exception
        {

            if (value1 >= value2)
                throw CreateException<TException>(MessageCollection.Validator_FirstValueIsGreaterOrEqualThanSecondValue(variableName1, variableName2));

        }

        /// <summary>Throws an exception of type <see cref="ArgumentException"/> when <paramref name="value1"/> is greater or equal than <paramref name="value2"/>.</summary> 
        public static void ThrowIfFirstIsGreaterOrEqual(int value1, string variableName1, int value2, string variableName2)
            => ThrowIfFirstIsGreaterOrEqual<ArgumentException>(value1, variableName1, value2, variableName2);

        /// <summary>Throws an exception of type <see cref="TException"/> when <paramref name="value1"/> is greater than <paramref name="value2"/>.</summary> 
        public static void ThrowIfFirstIsGreater<TException>(int value1, string variableName1, int value2, string variableName2) where TException : Exception
        {

            if (value1 > value2)
                throw CreateException<TException>(MessageCollection.Validator_FirstValueIsGreaterThanSecondValue(variableName1, variableName2));

        }

        /// <summary>Throws an exception of type <see cref="ArgumentException"/> when <paramref name="value1"/> is greater than <paramref name="value2"/>.</summary> 
        public static void ThrowIfFirstIsGreater(int value1, string variableName1, int value2, string variableName2)
            => ThrowIfFirstIsGreater<ArgumentException>(value1, variableName1, value2, variableName2);

        #endregion

        #region ThrowIfLess

        /// <summary>Throws an exception of type <see cref="TException"/> when <paramref name="value"/> is less than one.</summary>
        public static void ThrowIfLessThanOne<TException>(uint value, string variableName) where TException : Exception
        {

            if (value < 1)
                throw CreateException<TException>(MessageCollection.Validator_VariableCantBeLessThanOne(variableName));

        }

        /// <summary>Throws an exception of type <see cref="ArgumentException"/> when <paramref name="length"/> is less than one.</summary>
        public static void ThrowIfLessThanOne(uint value, string variableName)
            => ThrowIfLessThanOne<ArgumentException>(value, variableName);

        #endregion

        #region Methods_private

        private static T CreateException<T>(string message) where T : Exception
            => (T)Activator.CreateInstance(typeof(T), message);

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 17.09.2021
*/
