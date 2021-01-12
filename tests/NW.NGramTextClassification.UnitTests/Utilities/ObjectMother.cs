using System.Collections.Generic;
using System.Linq;

namespace NW.NGramTextClassification.UnitTests
{
    internal static class ObjectMother
    {

        // ArrayManager
        internal static string Delimiter1 = ";";
        internal static string VariableName_AddDelimiter_Arr = "arr";
        internal static string VariableName_AddDelimiter_Delimiter = "delimiter";

        // Validator
        internal static string[] Array1 = new[] { "Dodge", "Datsun", "Jaguar", "DeLorean" };
        internal static Car Object1 = new Car()
                {
                    Brand = "Dodge",
                    Model = "Charger",
                    Year = 1966,
                    Price = 13500,
                    Currency = "USD"
                };
        internal static uint Length1 = 3;
        internal static string VariableName = "variable";
        internal static string VariableName_Length = "length";
        internal static string VariableName_N = "n";
        internal static List<string> List1 = Array1.ToList();
        internal static ushort N1 = (ushort)Length1;
        internal static string String1 = "Dodge";
        internal static string String_WhiteSpaces = "   ";

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 08.01.2021

*/
