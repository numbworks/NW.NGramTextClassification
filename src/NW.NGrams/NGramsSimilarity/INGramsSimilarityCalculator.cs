using System;
using System.Collections.Generic;

namespace NW.NGrams
{
    public interface INGramsSimilarityCalculator
    {

        Func<double, double> RoundingStrategy { get; }

        double Do(List<string> list1, List<string> list2);

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 29.12.2020

*/
