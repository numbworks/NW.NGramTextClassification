using System;
using System.Collections.Generic;
using RUBN.Shared;

namespace NW.NGrams
{
    public class JaccardIndexCalculator : INGramsSimilarityCalculator
    {

        // Fields
        // Properties
        public IParametersValidator ParametersValidator { get; set; } = new ParametersValidator();
        public Func<double, double> RoundingStrategy { get; } = RoundingStategies.TwoDecimalDigits;

        // Constructors
        public JaccardIndexCalculator() { }

        // Methods
        /// <summary>
        /// It returns a double containing the JaccardIndex. The precision is established by RoundingStrategy.
        /// </summary>
        public Outcome Do(List<string> listA, List<string> listB)
        {

            string msgSuccess = "The Jaccard Index out of the provided Lists has been calculated.";
            string errFailure = "It hasn't been possible to calculate the Jaccard Index out of the provided Lists.";

            try
            {

                Outcome objReturn = ParametersValidator.AreNullOrEmpty(
                new object[] { listA, listB });
                if (objReturn.IsFailureOrException())
                    return OutcomeBuilder.Clone(objReturn).Append(errFailure).Get();

                HashSet<string> hshTmp = new HashSet<string>(listA);
                hshTmp.IntersectWith(new HashSet<string>(listB));
                int intIntersection = hshTmp.Count;

                hshTmp = new HashSet<string>(listA);
                hshTmp.UnionWith(listB);
                int intUnion = hshTmp.Count;
       
                double dblJaccardIndex = (double)intIntersection / (double)intUnion;

                return OutcomeBuilder.CreateSuccess(
                    msgSuccess,
                    RoundingStrategy(dblJaccardIndex)).Get();

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
 *  Description: It represents a Jaccard Index calculator.
 * 
 */
