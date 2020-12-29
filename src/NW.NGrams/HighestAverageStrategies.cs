using System;
using System.Collections.Generic;
using System.Linq;

namespace NW.NGrams
{
    public static class HighestAverageStrategies
    {

        // Properties
        // public static double UncategorizableThreesold { get; } = 0.6;
        public static 
            Func<List<LabeledTextSimilarityAverage>, Boolean> AreAllZeros { get; } = (List) 
                => {

                    /*
                     *
                     * Label    Average
                     * sv       0
                     * en       0
                     * 
                     * 		=> { 0, 0 } => true
                     * 
                     */

                   if (List.Where(Item => Item.Average == 0).Count() == List.Count) return true;
                   else return false;

                };
        public static
            Func<List<LabeledTextSimilarityAverage>, Boolean> AreDistinct { get; } = (List) => 
                {

                    /*
                     *
                     * Label    Average
                     * sv       0.1
                     * en       0.1
                     * dk       0.1
                     * 
                     * 		=> { 0.1, 0.1, 0.1 } => 1 => 1 != 3 => false
                     * 
                     */

                    if (List.Select(Item => Item.Average).Distinct().Count() == List.Count) return true;
                    else return false;

                };
        public static
            Func<List<LabeledTextSimilarityAverage>, LabeledTextSimilarityAverage> GetHighest { get; } = (List) =>
                {

                    /*
                     * 
                     * Label    Average
                     * sv       0.19
                     * en       0.45
                     * 
                     *      => { Label: "en", Average: 0.45 }
                     * 
                     */

                    return List.OrderByDescending(Item => Item.Average).ToList().First();

                };

    }
}

/*
 *
 *  Author: numbworks@gmail.com
 *  Last Update: 28.01.2018
 * 
 */
