using System.Collections.Generic;
using System.Linq;

namespace NW.NGramTextClassification.UnitTests
{
    internal static class ObjectMother
    {

        // Validator
        internal static string[] Validator_Array1 = new[] { "Dodge", "Datsun", "Jaguar", "DeLorean" };
        internal static Car Validator_Object1 = new Car()
        {
            Brand = "Dodge",
            Model = "Charger",
            Year = 1966,
            Price = 13500,
            Currency = "USD"
        };
        internal static uint Validator_Length1 = 3;
        internal static string Validator_VariableName_Variable = "variable";
        internal static string Validator_VariableName_Length = "length";
        internal static string Validator_VariableName_N = "n";
        internal static List<string> List1 = Validator_Array1.ToList();
        internal static ushort N1 = (ushort)Validator_Length1;
        internal static string Validator_String1 = "Dodge";
        internal static string Validator_StringOnlyWhiteSpaces = "   ";

        // ArrayManager
        internal static string ArrayManager_VariableName_Arr = "arr";
        internal static string ArrayManager_VariableName_Delimiter = "delimiter";
        internal static string ArrayManager_VariableName_StartIndex = "startIndex";
        internal static string ArrayManager_VariableName_Length = Validator_VariableName_Length;
        internal static string ArrayManager_VariableName_ArrLength = "arr.Length";
        internal static string ArrayManager_VariableName_StartIndexPlusLength = "startIndex + length";
        internal static string ArrayManager_Delimiter1 = ";";
        internal static uint ArrayManager_StartIndex1 = 0;
        internal static uint ArrayManager_Length1 = 2;
        internal static string[] ArrayManager_Array1 = Validator_Array1;
        internal static string[] ArrayManager_Array1_WithDelimiter1 
            = new[] {
                "Dodge",
                ArrayManager_Delimiter1,
                "Datsun",
                ArrayManager_Delimiter1,
                "Jaguar",
                ArrayManager_Delimiter1,
                "DeLorean"
            };
        internal static string[] ArrayManager_Array1_Subset1 = new[] { "Dodge", "Datsun" };

        // LabeledExample
        internal static string LabeledExample_VariableName_Label = "label";
        internal static string LabeledExample_VariableName_Text = "text";
        internal static string LabeledExample_VariableName_TextAsNGrams = "textAsNGrams";
        internal static ulong LabeledExample_Id1 = 1;
        internal static string LabeledExample_Label1 = "some_label";
        internal static string LabeledExample_LabelOnlyWhiteSpaces = Validator_StringOnlyWhiteSpaces;
        internal static string LabeledExample_Text1 = "We are looking for several skilled and driven developers to join our team.";
        internal static string LabeledExample_TextOnlyWhiteSpaces = Validator_StringOnlyWhiteSpaces;
        internal static INGram LabeledExample_Monogram1 = new Monogram(LabeledExample_Text1);
        internal static INGram LabeledExample_Bigram1 = new Bigram(LabeledExample_Text1);
        internal static INGram LabeledExample_Trigram1 = new Trigram(LabeledExample_Text1);
        internal static List<INGram> LabeledExample_TextAsNGrams1 
            = new List<INGram>() {
                LabeledExample_Monogram1,
                LabeledExample_Bigram1,
                LabeledExample_Trigram1
            };
        internal static LabeledExample LabeledExample1
            = new LabeledExample(
                    LabeledExample_Id1,
                    LabeledExample_Label1,
                    LabeledExample_Text1,
                    LabeledExample_TextAsNGrams1
                );
        internal static string LabeledExample1_AsStringTruncatedAt7 
            = string.Concat(
                        $"[ Id: '{LabeledExample_Id1}', ",
                        $"Label: '{LabeledExample_Label1}', ",
                        $"Text: '{LabeledExample_Text1.Substring(0, 7)}...', ",
                        $"TextAsNGrams: '{LabeledExample_TextAsNGrams1.Count.ToString()}' ]"
                    );
        internal static string LabeledExample1_AsStringTruncatedAtDefault
            = string.Concat(
                        $"[ Id: '{LabeledExample_Id1}', ",
                        $"Label: '{LabeledExample_Label1}', ",
                        $"Text: '{LabeledExample_Text1.Substring(0, (int)TextClassifierSettings.DefaultTruncateTextInLogMessagesAfter)}...', ",
                        $"TextAsNGrams: '{LabeledExample_TextAsNGrams1.Count.ToString()}' ]"
                    );

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 17.01.2021

*/
