using System;
using System.Collections.Generic;
using System.Linq;

namespace NW.NGrams
{
    public static class HighestAverageStrategies
    {

        // Properties
        public static Func<List<SimilarityIndexAverage>, bool> AreAllZeros { get; } 
            = (list) 
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

                        if (list.Where(Item => Item.Value == 0).Count() == list.Count)
                            return true;

                        return false;

                    };
        public static Func<List<SimilarityIndexAverage>, bool> AreDistinct { get; } 
            = (list) 
                => {

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

                        if (list.Select(Item => Item.Value).Distinct().Count() == list.Count)
                            return true;

                        return false;

                    };
        public static Func<List<SimilarityIndexAverage>, SimilarityIndexAverage> GetHighest { get; } 
            = (list) 
                => {

                        /*
                         * 
                         * Label    Average
                         * sv       0.19
                         * en       0.45
                         * 
                         *      => { Label: "en", Average: 0.45 }
                         * 
                         */

                        return list.OrderByDescending(Item => Item.Value).ToList().First();

                    };

        // public static double UncategorizableThreesold { get; } = 0.6;

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 29.12.2020

*/