using System;
using System.Collections.Generic;
using System.Linq;

namespace NW.NGramTextClassification
{
    public class SimilarityIndexCalculatorJaccard : ISimilarityIndexCalculator
    {

        // Fields
        // Properties
        // Constructors
        public SimilarityIndexCalculatorJaccard() { }

        // Methods
        public double Do(List<INGram> list1, List<INGram> list2, Func<double, double> roundingStrategy)
        {

            Validator.ValidateList(list1, nameof(list1));
            Validator.ValidateList(list2, nameof(list2));
            Validator.ValidateObject(roundingStrategy, nameof(roundingStrategy));

            return Do(
                    list1.Select(item => item.Value).ToList(),
                    list2.Select(item => item.Value).ToList(),
                    roundingStrategy);

        }
        public double Do(List<INGram> list1, List<INGram> list2)
            => Do(list1, list2, RoundingStategies.TwoDecimalDigits);

        // Methods (private)
        private double Do(List<string> list1, List<string> list2, Func<double, double> roundingStrategy)
        {

            HashSet<string> hashset = new HashSet<string>(list1);
            hashset.IntersectWith(new HashSet<string>(list2));
            int commonItems = hashset.Count;

            hashset = new HashSet<string>(list1);
            hashset.UnionWith(list2);
            int allItems = hashset.Count;

            double jaccardIndex = commonItems / (double)allItems;

            return roundingStrategy(jaccardIndex);

        }

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 31.12.2020

*/