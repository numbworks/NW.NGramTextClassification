using System;
using System.Collections.Generic;

namespace NW.NGramTextClassification
{
    public interface ISimilarityIndexCalculator
    {
        double Do(List<INGram> list1, List<INGram> list2, Func<double, double> roundingStrategy);
    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 31.12.2020

*/