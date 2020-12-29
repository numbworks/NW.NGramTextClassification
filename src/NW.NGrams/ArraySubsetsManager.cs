using System;
using RUBN.Shared;

namespace NW.NGrams
{
    public class ArraySubsetsManager : IArraySubsetsManager
    {

        // Fields
        // Properties
        public IParametersValidator ParametersValidator { get; set; } = new ParametersValidator();

        // Constructors
        public ArraySubsetsManager() { }

        // Methods
        public Outcome GetSubset(string[] arr, int intStartIndex, int intLength)
        {

            string msgSuccess =
                "The required subset has been successfully created out of the provided array (arr.Length: '{0}', arrSubset.Length: '{1}').";
            string errFailure = "It hasn't been possible to create the required subset out of the provided array.";
            string errAtLeastOne = "'{0}' must be at least equal to 1 (actual value:'{1}').";

            try
            {

                Outcome objReturn = ParametersValidator.AreNullOrEmpty(new object[] { arr });
                if (objReturn.IsFailureOrException())
                    return OutcomeBuilder.Clone(objReturn).Append(errFailure).Get();

                if (intLength < 1)
                    return OutcomeBuilder.CreateFailure(
                        String.Format(errAtLeastOne, nameof(intLength), intLength.ToString())).Get();

                string[] arrSubset = new string[intLength];
                Array.Copy(arr, intStartIndex, arrSubset, 0, intLength);

                return OutcomeBuilder.CreateSuccess(
                    String.Format(msgSuccess, arr.Length.ToString(), arrSubset.Length.ToString()),
                    arrSubset).Get();

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
 *  Last Update: 03.02.2018 
 *  Description: It collects some useful methods related to array subsets.
 * 
 */
