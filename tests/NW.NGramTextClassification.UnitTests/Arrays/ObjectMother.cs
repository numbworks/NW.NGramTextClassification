namespace NW.NGramTextClassification.UnitTests.Arrays
{
    public static class ObjectMother
    {

        #region Properties

        public static string ArrayManager_Delimiter01 = ";";
        public static uint ArrayManager_StartIndex01 = 0;
        public static uint ArrayManager_Length01 = 2;

        public static string[] ArrayManager_Array01_WithDelimiter01
            = new[] {
                "Dodge",
                ArrayManager_Delimiter01,
                "Datsun",
                ArrayManager_Delimiter01,
                "Jaguar",
                ArrayManager_Delimiter01,
                "DeLorean"
            };

        public static string[] ArrayManager_Array01_Subset01 = new[] { "Dodge", "Datsun" };

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 25.09.2022
*/