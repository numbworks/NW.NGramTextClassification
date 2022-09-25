using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NW.NGramTextClassification.NGrams;
using NW.NGramTextClassification.NGramTokenization;
using NW.NGramTextClassification.Similarity;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests
{
    public static class ObjectMother
    {

        #region SimilarityIndex

        public static string SimilarityIndex01_Text = LabeledExamples.ObjectMother.LabeledExample01.Text;
        public static string SimilarityIndex01_Label = "en";
        public static double SimilarityIndex01_Value = 0.23;
        public static SimilarityIndex SimilarityIndex01
            = new SimilarityIndex(
                    text: SimilarityIndex01_Text, 
                    label: SimilarityIndex01_Label, 
                    value: SimilarityIndex01_Value);
        public static string SimilarityIndex01_AsString
            = $"[ Text: '{SimilarityIndex01_Text}', Label: '{SimilarityIndex01_Label}', Value: '{SimilarityIndex01_Value}' ]";

        public static string SimilarityIndex02_Text = LabeledExamples.ObjectMother.LabeledExample02.Text;
        public static string SimilarityIndex02_Label = "sv";
        public static double SimilarityIndex02_Value = 0.76;
        public static SimilarityIndex SimilarityIndex02
            = new SimilarityIndex(
                    text: SimilarityIndex02_Text,
                    label: SimilarityIndex02_Label,
                    value: SimilarityIndex02_Value);

        #endregion

        #region SimilarityIndexAverage

        public static string SimilarityIndexAverage01_Label = "en";
        public static double SimilarityIndexAverage01_Value = 0.54;
        public static SimilarityIndexAverage SimilarityIndexAverage01
            = new SimilarityIndexAverage(SimilarityIndexAverage01_Label, SimilarityIndexAverage01_Value);
        public static string SimilarityIndexAverage01_AsString
            = $"[ Label: '{SimilarityIndexAverage01_Label}', Value: '{SimilarityIndexAverage01_Value}' ]";

        #endregion

        #region TextClassifier

        public static SimilarityIndex TextClassifier_Text1_SimilarityIndex1 = new SimilarityIndex(LabeledExamples.ObjectMother.LabeledExample01.Text, "en", 1);
        public static SimilarityIndex TextClassifier_Text1_SimilarityIndex2 = new SimilarityIndex(LabeledExamples.ObjectMother.LabeledExample02.Text, "sv", 0);
        public static List<SimilarityIndex> TextClassifier_Text1_SimilarityIndexes
            = new List<SimilarityIndex>()
            {
                TextClassifier_Text1_SimilarityIndex1,
                TextClassifier_Text1_SimilarityIndex2
            };
        public static SimilarityIndexAverage TextClassifier_Text1_SimilarityIndexAverage1
            = new SimilarityIndexAverage("en", 1);
        public static SimilarityIndexAverage TextClassifier_Text1_SimilarityIndexAverage2
            = new SimilarityIndexAverage("sv", 0);
        public static List<SimilarityIndexAverage> TextClassifier_Text1_SimilarityIndexAverages
            = new List<SimilarityIndexAverage>()
            {
                TextClassifier_Text1_SimilarityIndexAverage1,
                TextClassifier_Text1_SimilarityIndexAverage2
            };
        public static TextClassifierResult TextClassifier_Text1_TextClassifierResult
            = new TextClassifierResult(
                        LabeledExamples.ObjectMother.LabeledExample01.Label,
                        TextClassifier_Text1_SimilarityIndexes,
                        TextClassifier_Text1_SimilarityIndexAverages);
        public static List<INGram> TextClassifier_Text1_NGrams = LabeledExamples.ObjectMother.LabeledExample01_NGrams;
        public static List<string> TextClassifier_Text1_UniqueLabels
            = new List<string>()
            {
                TextClassifier_Text1_SimilarityIndex1.Label,
                TextClassifier_Text1_SimilarityIndex2.Label,
            };

        public static TextClassifierResult TextClassifier_TextClassifierResult_LabeledExamples00
            = new TextClassifierResult(
                    label: LabeledExamples.ObjectMother.CreateThirtyTokenizedExamples()[0].LabeledExample.Label,
                    indexes: CreateThirtySimilarityIndexes(),
                    indexAverages: CreateSimilarityIndexAveragesForThirty()
                );

        #endregion

        #region TextClassifierResult

        public static List<SimilarityIndex> TextClassifierResult_SimilarityIndexes
            = new List<SimilarityIndex>()
                {
                    SimilarityIndex01,
                    SimilarityIndex02
                };
        public static List<SimilarityIndexAverage> TextClassifierResult_SimilarityIndexAverages
            = new List<SimilarityIndexAverage>()
                {
                    SimilarityIndexAverage01
                };
        public static string TextClassifierResult_AsString
            = $"[ Label: '{LabeledExamples.ObjectMother.LabeledExample01.Label}', SimilarityIndexes: '{TextClassifierResult_SimilarityIndexes.Count}', SimilarityIndexAverages: '{TextClassifierResult_SimilarityIndexAverages.Count}' ]";
        public static string TextClassifierResult_AsStringWithNullLabel
            = $"[ Label: 'null', SimilarityIndexes: '{TextClassifierResult_SimilarityIndexes.Count}', SimilarityIndexAverages: '{TextClassifierResult_SimilarityIndexAverages.Count}' ]";
        public static string TextClassifierResult_AllNulls
            = $"[ Label: 'null', SimilarityIndexes: 'null', SimilarityIndexAverages: 'null' ]";
        public static string TextClassifierResult_VariableName_Indexes = "indexes";
        public static string TextClassifierResult_VariableName_IndexAverages = "indexAverages";

        #endregion


        #region Validator

        public static string[] Validator_Array1 = new[] { "Dodge", "Datsun", "Jaguar", "DeLorean" };
        public static Car Validator_Object1 = new Car()
        {
            Brand = "Dodge",
            Model = "Charger",
            Year = 1966,
            Price = 13500,
            Currency = "USD"
        };
        public static uint Validator_Length1 = 3;
        public static string Validator_VariableName_Variable = "variable";
        public static string Validator_VariableName_Length = "length";
        public static string Validator_VariableName_N = "n";
        public static List<string> List1 = Validator_Array1.ToList();
        public static ushort N1 = (ushort)Validator_Length1;
        public static string Validator_String1 = "Dodge";
        public static string Validator_StringOnlyWhiteSpaces = "   ";

        #endregion

        #region Methods
      
        public static bool AreEqual(SimilarityIndex obj1, SimilarityIndex obj2)
        {

            return string.Equals(obj1.Text, obj2.Text, StringComparison.InvariantCulture)
                    && string.Equals(obj1.Label, obj2.Label, StringComparison.InvariantCulture)
                    && (obj1.Value == obj2.Value);

        }
        public static bool AreEqual(SimilarityIndexAverage obj1, SimilarityIndexAverage obj2)
        {

            return string.Equals(obj1.Label, obj2.Label, StringComparison.InvariantCulture)
                    && (obj1.Value == obj2.Value);

        }
        public static bool AreEqual(TextClassifierResult obj1, TextClassifierResult obj2)
        {

            return string.Equals(obj1.Label, obj2.Label, StringComparison.InvariantCulture)
                    && AreEqual(obj1.SimilarityIndexAverages, obj2.SimilarityIndexAverages)
                    && AreEqual(obj1.SimilarityIndexes, obj2.SimilarityIndexes);

        }

        public static bool AreEqual(List<SimilarityIndex> list1, List<SimilarityIndex> list2)
            => AreEqual(list1, list2, (obj1, obj2) => AreEqual(obj1, obj2));
        public static bool AreEqual(List<SimilarityIndexAverage> list1, List<SimilarityIndexAverage> list2)
            => AreEqual(list1, list2, (obj1, obj2) => AreEqual(obj1, obj2));

        public static TReturn CallPrivateMethod<TClass, TReturn>(TClass obj, string methodName, object[] args)
        {

            Type type = typeof(TClass);

            return (TReturn)type.GetTypeInfo().GetDeclaredMethod(methodName).Invoke(obj, args);

        }
        public static void Method_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
        {

            // Arrange
            // Act
            // Assert
            Exception actual = Assert.Throws(expectedType, del);
            Assert.AreEqual(expectedMessage, actual.Message);

        }
        public static bool AreEqual<T>(List<T> list1, List<T> list2, Func<T, T, bool> comparer)
        {

            if (list1 == null && list2 == null)
                return true;

            if (list1 == null || list2 == null)
                return false;

            if (list1.Count != list2.Count)
                return false;

            for (int i = 0; i < list1.Count; i++)
                if (comparer(list1[i], list2[i]) == false)
                    return false;

            return true;

        }

        public static List<SimilarityIndex> CreateThirtySimilarityIndexes()
        {

            List<SimilarityIndex> similarityIndexes = new List<SimilarityIndex>()
            {

                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyTokenizedExamples()[0].LabeledExample.Text, label: "en", value: 1),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyTokenizedExamples()[1].LabeledExample.Text, label: "en", value: 0.031696),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyTokenizedExamples()[2].LabeledExample.Text, label: "en", value: 0.017512),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyTokenizedExamples()[3].LabeledExample.Text, label: "en", value: 0.022472),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyTokenizedExamples()[4].LabeledExample.Text, label: "en", value: 0.017927),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyTokenizedExamples()[5].LabeledExample.Text, label: "en", value: 0.018717),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyTokenizedExamples()[6].LabeledExample.Text, label: "en", value: 0.014844),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyTokenizedExamples()[7].LabeledExample.Text, label: "en", value: 0.017185),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyTokenizedExamples()[8].LabeledExample.Text, label: "en", value: 0.024096),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyTokenizedExamples()[9].LabeledExample.Text, label: "en", value: 0.020548),

                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyTokenizedExamples()[10].LabeledExample.Text, label: "sv", value: 0.000741),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyTokenizedExamples()[11].LabeledExample.Text, label: "sv", value: 0),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyTokenizedExamples()[12].LabeledExample.Text, label: "sv", value: 0),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyTokenizedExamples()[13].LabeledExample.Text, label: "sv", value: 0.000802),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyTokenizedExamples()[14].LabeledExample.Text, label: "sv", value: 0.000745),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyTokenizedExamples()[15].LabeledExample.Text, label: "sv", value: 0),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyTokenizedExamples()[16].LabeledExample.Text, label: "sv", value: 0.000861),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyTokenizedExamples()[17].LabeledExample.Text, label: "sv", value: 0.00074),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyTokenizedExamples()[18].LabeledExample.Text, label: "sv", value: 0.000733),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyTokenizedExamples()[19].LabeledExample.Text, label: "sv", value: 0.000905),

                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyTokenizedExamples()[20].LabeledExample.Text, label: "dk", value: 0.002333),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyTokenizedExamples()[21].LabeledExample.Text, label: "dk", value: 0.000895),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyTokenizedExamples()[22].LabeledExample.Text, label: "dk", value: 0.000843),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyTokenizedExamples()[23].LabeledExample.Text, label: "dk", value: 0.004918),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyTokenizedExamples()[24].LabeledExample.Text, label: "dk", value: 0.001668),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyTokenizedExamples()[25].LabeledExample.Text, label: "dk", value: 0.003027),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyTokenizedExamples()[26].LabeledExample.Text, label: "dk", value: 0.002618),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyTokenizedExamples()[27].LabeledExample.Text, label: "dk", value: 0.00367),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyTokenizedExamples()[28].LabeledExample.Text, label: "dk", value: 0.002575),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyTokenizedExamples()[29].LabeledExample.Text, label: "dk", value: 0.003934)

            };

            return similarityIndexes;

        }
        public static List<SimilarityIndexAverage> CreateSimilarityIndexAveragesForThirty()
        {

            List<SimilarityIndexAverage> similarityIndexAverages = new List<SimilarityIndexAverage>()
            {

                new SimilarityIndexAverage(label: "en", value: 0.1185),
                new SimilarityIndexAverage(label: "sv", value: 0.000553),
                new SimilarityIndexAverage(label: "dk", value: 0.002648)

            };

            return similarityIndexAverages;

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 25.09.2022
*/