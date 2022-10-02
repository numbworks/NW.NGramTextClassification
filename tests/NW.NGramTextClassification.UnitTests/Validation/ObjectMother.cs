using System;
using System.Collections.Generic;
using System.Linq;
using NW.NGramTextClassification.UnitTests.Utilities;

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
        public static string VariableName_N1 = "n1";
        public static string VariableName_N2 = "n2";
        public static List<string> List01 = Array01.ToList();
        public static uint Value01 = Length01;
        public static string String01 = "Dodge";
        public static string StringOnlyWhiteSpaces = "   ";
        public static Dictionary<string, int> SubScrapers_Proper = new Dictionary<string, int>()
        {

            { "urls", 20 },
            { "titles", 20 },
            { "createDates", 20 },
            { "applicationDates", 20 },
            { "workAreas", 20 },
            { "workAreasWithoutZones", 20 },
            { "workingHours", 20 },
            { "jobTypes", 20 },
            { "jobIds", 20 }

        };
        public static Dictionary<string, int> SubScrapers_Wrong = new Dictionary<string, int>()
        {

            { "urls", 19 },
            { "titles", 20 },
            { "createDates", 20 },
            { "applicationDates", 20 },
            { "workAreas", 20 },
            { "workAreasWithoutZones", 20 },
            { "workingHours", 20 },
            { "jobTypes", 20 },
            { "jobIds", 20 }

        };
        public static DateTime DateTimeOlder = new DateTime(2019, 09, 01, 00, 00, 00, 000);
        public static DateTime DateTimeNewer = new DateTime(2019, 12, 31, 23, 59, 59, 999);

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 02.05.2022
*/