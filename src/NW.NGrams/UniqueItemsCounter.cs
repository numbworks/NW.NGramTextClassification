using System;
using System.Collections.Generic;
using RUBN.Shared;

namespace NW.NGrams
{
    public class UniqueItemsCounter : IUniqueItemsCounter
    {

        // Fields
        // Properties
        public IParametersValidator ParametersValidator { get; set; } = new ParametersValidator();

        // Constructors
        public UniqueItemsCounter() { }

        // Methods
        public Outcome Do(List<string> list)
        {

            string msgSuccess = "The unique items in the provided List have been counted (before: '{0}', after: '{1}').";
            string errFailure = "It hasn't been possible to count the unique items in the provided List.";

            try
            {

                Outcome objReturn = ParametersValidator.IsNullOrEmpty(list);
                if (objReturn.IsFailureOrException())
                    return OutcomeBuilder.Clone(objReturn).Append(errFailure).Get();

                HashSet<string> hsh = new HashSet<string>();
                foreach (string str in list)
                    hsh.Add(str);

                return OutcomeBuilder.CreateSuccess(
                    String.Format(msgSuccess, list.Count.ToString(), hsh.Count.ToString()),
                    hsh.Count).Get();

            }
            catch(Exception e)
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
 *  Description: It represents a counter of unique items.
 * 
 */
