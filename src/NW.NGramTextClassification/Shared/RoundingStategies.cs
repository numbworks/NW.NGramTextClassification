using System;

namespace NW.NGrams
{
    public static class RoundingStategies
    {

        // Properties
        public static Func<double, double> TwoDecimalDigits { get; } 
            = x => Math.Round(x, 2, MidpointRounding.AwayFromZero);
        public static Func<double, double> SixDecimalDigits { get; } 
            = x => Math.Round(x, 6, MidpointRounding.AwayFromZero);

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 29.12.2020

*/