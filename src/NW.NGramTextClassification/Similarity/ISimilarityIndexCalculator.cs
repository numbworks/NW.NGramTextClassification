using System;
using System.Collections.Generic;
using NW.NGramTextClassification.NGrams;

namespace NW.NGramTextClassification.Similarity
{
    /// <summary>A component able to calculate a <see cref="SimilarityIndex"/>.</summary>
    public interface ISimilarityIndexCalculator
    {

        /// <summary>Calculates the similarity between <paramref name="list1"/> and <paramref name="list2"/>.</summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/> 
        double Do(List<INGram> list1, List<INGram> list2, Func<double, double> roundingStrategy);
    
    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 18.09.2021
*/