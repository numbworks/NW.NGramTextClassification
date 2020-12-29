using System;
using System.Collections.Generic;

namespace NW.NGrams
{
    public class JaccardIndexCalculator : INGramsSimilarityCalculator
    {

        // Fields
        // Properties
        public Func<double, double> RoundingStrategy { get; }

        // Constructors
        public JaccardIndexCalculator(Func<double, double> roundingStrategy)
        {

            if (roundingStrategy == null)
                throw new ArgumentNullException(nameof(roundingStrategy));

            RoundingStrategy = roundingStrategy;

        }

        // Methods
        /// <summary>
        /// It returns a double containing the JaccardIndex. The precision is established by RoundingStrategy.
        /// </summary>
        public double Do(List<string> list1, List<string> list2)
        {

            if (list1 == null)
                throw new ArgumentNullException(nameof(list1));
            if (list1.Count == 0)
                throw new ArgumentNullException(MessageCollection.VariableContainsZeroItems.Invoke(nameof(list1)));
            if (list2 == null)
                throw new ArgumentNullException(nameof(list2));
            if (list2.Count == 0)
                throw new ArgumentNullException(MessageCollection.VariableContainsZeroItems.Invoke(nameof(list2)));

            HashSet<string> hashset = new HashSet<string>(list1);
            hashset.IntersectWith(new HashSet<string>(list2));
            int commonItems = hashset.Count;

            hashset = new HashSet<string>(list1);
            hashset.UnionWith(list2);
            int allItems = hashset.Count;
       
            double jaccardIndex = commonItems / (double)allItems;

            return RoundingStrategy(jaccardIndex);

        }

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 29.12.2020

*/