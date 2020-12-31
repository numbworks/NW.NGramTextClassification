using System;
using System.Collections.Generic;

namespace NW.NGrams
{
    public class SimilarityIndexCalculatorJaccard : ISimilarityIndexCalculator
    {

        // Fields
        // Properties
        // Constructors
        public SimilarityIndexCalculatorJaccard() { }

        // Methods
        public double Do(List<string> list1, List<string> list2, Func<double, double> roundingStrategy)
        {

            Validator.ValidateList(list1, nameof(list1));
            Validator.ValidateList(list2, nameof(list2));
            Validator.ValidateObject(roundingStrategy, nameof(roundingStrategy));

            HashSet<string> hashset = new HashSet<string>(list1);
            hashset.IntersectWith(new HashSet<string>(list2));
            int commonItems = hashset.Count;

            hashset = new HashSet<string>(list1);
            hashset.UnionWith(list2);
            int allItems = hashset.Count;
       
            double jaccardIndex = commonItems / (double)allItems;

            return roundingStrategy(jaccardIndex);

        }
        public double Do(List<string> list1, List<string> list2)
            => Do(list1, list2, RoundingStategies.TwoDecimalDigits);

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 31.12.2020

*/