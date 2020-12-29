using System;

namespace NW.NGrams
{
    public static class MessageCollection
    {

        // ArrayManager
        public static Func<string, string> VariableContainsZeroItems { get; }
            = (variableName) => $"'{variableName}' contains zero items.";
        public static Func<string, string> VariableCantBeLessThanOne { get; }
            = (variableName) => $"'{variableName}' can't be less than one.";


    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 29.12.2020

*/