namespace NW.NGramTextClassification.UnitTests.Arrays
{
    public static class ObjectMother
    {

        #region Properties

        public static string[] Array01 = new[] { "Dodge", "Datsun", "Jaguar", "DeLorean" };

        public static string Array01_Delimiter01 = ";";
        public static uint Array01_StartIndex01 = 0;
        public static uint Array01_Length01 = 2;

        public static string[] Array01_WithDelimiter01
            = new[] {
                "Dodge",
                Array01_Delimiter01,
                "Datsun",
                Array01_Delimiter01,
                "Jaguar",
                Array01_Delimiter01,
                "DeLorean"
            };

        public static string[] Array01_Subset01 = new[] { "Dodge", "Datsun" };

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 25.09.2022
*/