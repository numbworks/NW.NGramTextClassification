using System;
using System.Collections;
using System.Collections.Generic;

namespace NW.NGramTextClassification.UnitTests
{
    internal static class ObjectMother
    {

        // Validator
        internal static string[] Array1 = new[] { "Dodge", "Datsun" };
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

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 08.01.2021

*/
