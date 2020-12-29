using System;

namespace NW.NGrams
{
    public static class MessageCollection
    {

        // ArrayDelimiterManager
        public static Func<string, string> VariableContainsZeroItems { get; }
            = (variableName) => $"'{variableName}' contains zero items.";

        // ArraySubsetsManager
        public static Func<string, string> VariableCantBeLessThanOne { get; }
            = (variableName) => $"'{variableName}' can't be less than one.";


    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 29.12.2020

*/