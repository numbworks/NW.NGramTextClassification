using System;
using System.Collections.Generic;
using RUBN.Shared;

namespace NW.NGrams
{
    public class ArrayDelimiterManager : IArrayDelimiterManager
    {

        // Fields
        // Properties
        public IParametersValidator ParametersValidator { get; set; } = new ParametersValidator();

        // Constructors
        public ArrayDelimiterManager() { }

        // Methods
        public Outcome AddDelimiter(string[] arr, string strDelimiter)
        {

            string msgSuccess = String.Format(
                    "The provided delimiter ('{0}') has been successfully added among the items of the provided array.", 
                    strDelimiter);
            string errFailure 
                = "It hasn't been possible to add the provided delimiter among the items of the provided array.";

            try
            {

                Outcome objReturn = ParametersValidator.IsNullOrEmpty(arr);
                if (objReturn.IsFailureOrException())
                    return OutcomeBuilder.Clone(objReturn).Append(errFailure).Get();

                List<string> listReturn = new List<string>();
                for (int i = 0; i < arr.Length; i++)
                {
                    listReturn.Add(arr[i]);
                    if (i != (arr.Length - 1))
                        listReturn.Add(strDelimiter);

                }

                return OutcomeBuilder.CreateSuccess(msgSuccess,listReturn.ToArray()).Get();

            }
            catch (Exception e)
            {

                return OutcomeBuilder.CreateException(e).Append(errFailure).Get();

            }

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
