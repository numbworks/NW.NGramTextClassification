using System;
using System.Collections.Generic;
using RUBN.Shared;

namespace NW.NGrams
{
    public interface INGramsSimilarityCalculator
    {

        IParametersValidator ParametersValidator { get; set; }
        Func<double, double> RoundingStrategy { get; }

        Outcome Do(List<string> listA, List<string> listB);

    }
}

/*
 *
 *  Author: numbworks@gmail.com
 *  Last Update: 20.01.2018 
 * 
 */
