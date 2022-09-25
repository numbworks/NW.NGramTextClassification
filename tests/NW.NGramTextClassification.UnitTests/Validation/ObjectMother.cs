using System.Collections.Generic;
using System.Linq;

namespace NW.NGramTextClassification.UnitTests.Validation
{
    public static class ObjectMother
    {

        #region Properties

        public static string[] Array01 = new[] { "Dodge", "Datsun", "Jaguar", "DeLorean" };
        public static Car Object01 = new Car()
        {
            Brand = "Dodge",
            Model = "Charger",
            Year = 1966,
            Price = 13500,
            Currency = "USD"
        };
        public static uint Length01 = 3;
        public static string VariableName_Variable = "variable";
        public static string VariableName_Length = "length";
        public static string VariableName_N = "n";
        public static List<string> List01 = Array01.ToList();
        public static ushort N01 = (ushort)Length01;
        public static string String01 = "Dodge";
        public static string StringOnlyWhiteSpaces = "   ";

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 25.09.2022
*/