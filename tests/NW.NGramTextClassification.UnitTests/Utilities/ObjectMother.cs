﻿using System;
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
        internal static string LabeledExample_Label1 = "en";
        internal static string LabeledExample_LabelOnlyWhiteSpaces = Validator_StringOnlyWhiteSpaces;
        internal static string LabeledExample_Text1 = "We are looking for several skilled and driven developers to join our team.";
        internal static string LabeledExample_TextOnlyWhiteSpaces = Validator_StringOnlyWhiteSpaces;
        internal static string LabeledExample_Text1_MonogramValue1 = "we";
        internal static string LabeledExample_Text1_BigramValue1 = "we are";
        internal static string LabeledExample_Text1_TrigramValue1 = "we are looking";
        internal static List<INGram> LabeledExample_TextAsNGrams1 
            = new List<INGram>() {
                new Monogram(LabeledExample_Text1_MonogramValue1),
                new Bigram(LabeledExample_Text1_BigramValue1),
                new Trigram(LabeledExample_Text1_TrigramValue1)
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

        // LabeledExampleFactory
        internal static string LabeledExampleFactory_VariableName_Tokenizer = "tokenizer";
        internal static string LabeledExampleFactory_VariableName_Label = "label";
        internal static string LabeledExampleFactory_VariableName_Text = "text";
        internal static string LabeledExampleFactory_VariableName_Strategy = "strategy";
        internal static string LabeledExampleFactory_VariableName_RuleSet = "ruleSet";
        internal static string LabeledExampleFactory_VariableName_Tuples = "tuples";
        internal static uint LabeledExampleFactory_InitialId1 = 0;
        internal static ulong LabeledExampleFactory_Id1 = LabeledExample_Id1;
        internal static string LabeledExampleFactory_Label1 = LabeledExample_Label1;
        internal static string LabeledExampleFactory_LabelOnlyWhiteSpaces = LabeledExample_LabelOnlyWhiteSpaces;
        internal static string LabeledExampleFactory_Label2 = "sv";
        internal static string LabeledExampleFactory_Text1 = LabeledExample_Text1;
        internal static string LabeledExampleFactory_TextOnlyWhiteSpaces = LabeledExample_TextOnlyWhiteSpaces;
        internal static string LabeledExampleFactory_Text2 = "Vår kund erbjuder trivsel, arbetsglädje och en trygg arbetsmiljö.";
        internal static (string label, string text) LabeledExampleFactory_Tuple1 = (LabeledExampleFactory_Label1, LabeledExampleFactory_Text1);
        internal static (string label, string text) LabeledExampleFactory_Tuple2 = (LabeledExampleFactory_Label2, LabeledExampleFactory_Text2);
        internal static List<(string label, string text)> LabeledExampleFactory_Tuples
            = new List<(string label, string text)>()
            {
                LabeledExampleFactory_Tuple1,
                LabeledExampleFactory_Tuple2
            };

        // "We are looking for several skilled and driven developers to join our team."
        internal static string LabeledExampleFactory_Text1_MonogramValue1 = LabeledExample_Text1_MonogramValue1;
        internal static string LabeledExampleFactory_Text1_MonogramValue2 = "are";
        internal static string LabeledExampleFactory_Text1_MonogramValue3 = "looking";
        internal static string LabeledExampleFactory_Text1_MonogramValue4 = "for";
        internal static string LabeledExampleFactory_Text1_MonogramValue5 = "several";
        internal static string LabeledExampleFactory_Text1_MonogramValue6 = "skilled";
        internal static string LabeledExampleFactory_Text1_MonogramValue7 = "and";
        internal static string LabeledExampleFactory_Text1_MonogramValue8 = "driven";
        internal static string LabeledExampleFactory_Text1_MonogramValue9 = "developers";
        internal static string LabeledExampleFactory_Text1_MonogramValue10 = "to";
        internal static string LabeledExampleFactory_Text1_MonogramValue11 = "join";
        internal static string LabeledExampleFactory_Text1_MonogramValue12 = "our";
        internal static string LabeledExampleFactory_Text1_MonogramValue13 = "team";
        internal static string LabeledExampleFactory_Text1_BigramValue1 = LabeledExample_Text1_BigramValue1;
        internal static string LabeledExampleFactory_Text1_BigramValue2 = "are looking";
        internal static string LabeledExampleFactory_Text1_BigramValue3 = "looking for";
        internal static string LabeledExampleFactory_Text1_BigramValue4 = "for several";
        internal static string LabeledExampleFactory_Text1_BigramValue5 = "several skilled";
        internal static string LabeledExampleFactory_Text1_BigramValue6 = "skilled and";
        internal static string LabeledExampleFactory_Text1_BigramValue7 = "and driven";
        internal static string LabeledExampleFactory_Text1_BigramValue8 = "driven developers";
        internal static string LabeledExampleFactory_Text1_BigramValue9 = "developers to";
        internal static string LabeledExampleFactory_Text1_BigramValue10 = "to join";
        internal static string LabeledExampleFactory_Text1_BigramValue11 = "join our";
        internal static string LabeledExampleFactory_Text1_BigramValue12 = "our team";
        internal static string LabeledExampleFactory_Text1_BigramValue13 = "team";
        internal static string LabeledExampleFactory_Text1_TrigramValue1 = LabeledExample_Text1_TrigramValue1;
        internal static string LabeledExampleFactory_Text1_TrigramValue2 = "are looking for";
        internal static string LabeledExampleFactory_Text1_TrigramValue3 = "looking for several";
        internal static string LabeledExampleFactory_Text1_TrigramValue4 = "for several skilled";
        internal static string LabeledExampleFactory_Text1_TrigramValue5 = "several skilled and";
        internal static string LabeledExampleFactory_Text1_TrigramValue6 = "skilled and driven";
        internal static string LabeledExampleFactory_Text1_TrigramValue7 = "and driven developers";
        internal static string LabeledExampleFactory_Text1_TrigramValue8 = "driven developers to";
        internal static string LabeledExampleFactory_Text1_TrigramValue9 = "developers to join";
        internal static string LabeledExampleFactory_Text1_TrigramValue10 = "to join our";
        internal static string LabeledExampleFactory_Text1_TrigramValue11 = "join our team";
        internal static string LabeledExampleFactory_Text1_TrigramValue12 = "our team";
        internal static string LabeledExampleFactory_Text1_TrigramValue13 = "team";
        internal static List<INGram> LabeledExampleFactory_Text1_NGrams1
            = new List<INGram>() {

                new Monogram(LabeledExampleFactory_Text1_MonogramValue1),
                new Monogram(LabeledExampleFactory_Text1_MonogramValue2),
                new Monogram(LabeledExampleFactory_Text1_MonogramValue3),
                new Monogram(LabeledExampleFactory_Text1_MonogramValue4),
                new Monogram(LabeledExampleFactory_Text1_MonogramValue5),
                new Monogram(LabeledExampleFactory_Text1_MonogramValue6),
                new Monogram(LabeledExampleFactory_Text1_MonogramValue7),
                new Monogram(LabeledExampleFactory_Text1_MonogramValue8),
                new Monogram(LabeledExampleFactory_Text1_MonogramValue9),
                new Monogram(LabeledExampleFactory_Text1_MonogramValue10),
                new Monogram(LabeledExampleFactory_Text1_MonogramValue11),
                new Monogram(LabeledExampleFactory_Text1_MonogramValue12),
                new Monogram(LabeledExampleFactory_Text1_MonogramValue13),
                new Bigram(LabeledExampleFactory_Text1_BigramValue1),
                new Bigram(LabeledExampleFactory_Text1_BigramValue2),
                new Bigram(LabeledExampleFactory_Text1_BigramValue3),
                new Bigram(LabeledExampleFactory_Text1_BigramValue4),
                new Bigram(LabeledExampleFactory_Text1_BigramValue5),
                new Bigram(LabeledExampleFactory_Text1_BigramValue6),
                new Bigram(LabeledExampleFactory_Text1_BigramValue7),
                new Bigram(LabeledExampleFactory_Text1_BigramValue8),
                new Bigram(LabeledExampleFactory_Text1_BigramValue9),
                new Bigram(LabeledExampleFactory_Text1_BigramValue10),
                new Bigram(LabeledExampleFactory_Text1_BigramValue11),
                new Bigram(LabeledExampleFactory_Text1_BigramValue12),
                new Bigram(LabeledExampleFactory_Text1_BigramValue13),
                new Trigram(LabeledExampleFactory_Text1_TrigramValue1),
                new Trigram(LabeledExampleFactory_Text1_TrigramValue2),
                new Trigram(LabeledExampleFactory_Text1_TrigramValue3),
                new Trigram(LabeledExampleFactory_Text1_TrigramValue4),
                new Trigram(LabeledExampleFactory_Text1_TrigramValue5),
                new Trigram(LabeledExampleFactory_Text1_TrigramValue6),
                new Trigram(LabeledExampleFactory_Text1_TrigramValue7),
                new Trigram(LabeledExampleFactory_Text1_TrigramValue8),
                new Trigram(LabeledExampleFactory_Text1_TrigramValue9),
                new Trigram(LabeledExampleFactory_Text1_TrigramValue10),
                new Trigram(LabeledExampleFactory_Text1_TrigramValue11),
                new Trigram(LabeledExampleFactory_Text1_TrigramValue12),
                new Trigram(LabeledExampleFactory_Text1_TrigramValue13)

            };
        internal static LabeledExample LabeledExampleFactory_LabeledExample1
            = new LabeledExample(
                        LabeledExampleFactory_Id1,
                        LabeledExampleFactory_Label1,
                        LabeledExampleFactory_Text1,
                        LabeledExampleFactory_Text1_NGrams1);

        // Methods
        internal static bool AreEqual(INGram obj1, INGram obj2)
        {

            return (obj1.N == obj2.N)
                    && AreEqual(obj1.Strategy, obj2.Strategy)
                    && string.Equals(obj1.Value, obj2.Value, StringComparison.InvariantCulture);

        }
        internal static bool AreEqual(ITokenizationStrategy obj1, ITokenizationStrategy obj2)
        {

            return string.Equals(obj1.Delimiter, obj2.Delimiter, StringComparison.InvariantCulture)
                    && string.Equals(obj1.Pattern, obj2.Pattern, StringComparison.InvariantCulture)
                    && (obj1.ToLowercase == obj2.ToLowercase);

        }
        internal static bool AreEqual(List<INGram> list1, List<INGram> list2)
        {

            if (list1 == null && list2 == null)
                return true;

            if (list1 == null || list2 == null)
                return false;

            if (list1.Count != list2.Count)
                return false;

            for (int i = 0; i < list1.Count; i++)
                if (AreEqual(list1[i], list2[i]) == false)
                    return false;

            return true;

        }
        internal static bool AreEqual(LabeledExample obj1, LabeledExample obj2)
        {

            return (obj1.Id == obj2.Id)
                    && string.Equals(obj1.Label, obj2.Label, StringComparison.InvariantCulture)
                    && string.Equals(obj1.Text, obj2.Text, StringComparison.InvariantCulture)
                    && AreEqual(obj1.TextAsNGrams, obj2.TextAsNGrams);

        }

    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 19.01.2021

*/
