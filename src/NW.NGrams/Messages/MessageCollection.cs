using System;

namespace NW.NGrams
{
    public static class MessageCollection
    {

        // ArrayDelimiterManager
        public static Func<string, string> VariableContainsZeroItems { get; }
            = (variableName) => $"'{variableName}' contains zero items.";



    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 29.12.2020

*/