using System;
using System.Collections.Generic;
using System.Linq;
using NW.NGramTextClassification.NGrams;

namespace NW.NGramTextClassification.Similarity
{
    /// <inheritdoc cref="ISimilarityIndexCalculator"/>
    public class SimilarityIndexCalculatorJaccard : ISimilarityIndexCalculator
    {

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors

        /// <summary>Initializes a <see cref="SimilarityIndexCalculatorJaccard"/> instance.</summary>
        public SimilarityIndexCalculatorJaccard() { }

        #endregion

        #region Methods_public

        public double Do(List<INGram> list1, List<INGram> list2, Func<double, double> roundingFunction)
        {

            Validator.ValidateList(list1, nameof(list1));
            Validator.ValidateList(list2, nameof(list2));
            Validator.ValidateObject(roundingFunction, nameof(roundingFunction));

            return Do(
                    list1.Select(item => item.Value).ToList(),
                    list2.Select(item => item.Value).ToList(),
                    roundingFunction);

        }

        #endregion

        #region Methods_private

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

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 18.09.2021
*/