using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NW.NGramTextClassification.NGrams;
using NW.NGramTextClassification.Similarity;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests
{
    public static class ObjectMother
    {

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
                    indexes: Similarity.ObjectMother.CreateThirtySimilarityIndexes(),
                    indexAverages: Similarity.ObjectMother.CreateSimilarityIndexAveragesForThirty()
                );

        #endregion

        #region TextClassifierResult

        public static string TextClassifierResult01_AsString
            = $"[ Label: '{LabeledExamples.ObjectMother.LabeledExample01.Label}', SimilarityIndexes: '{Similarity.ObjectMother.SimilarityIndexes.Count}', SimilarityIndexAverages: '{Similarity.ObjectMother.SimilarityIndexAverages.Count}' ]";       
        public static string TextClassifierResult_AsStringWithNullLabel
            = $"[ Label: 'null', SimilarityIndexes: '{Similarity.ObjectMother.SimilarityIndexes.Count}', SimilarityIndexAverages: '{Similarity.ObjectMother.SimilarityIndexAverages.Count}' ]";
        public static string TextClassifierResult_AllNulls
            = $"[ Label: 'null', SimilarityIndexes: 'null', SimilarityIndexAverages: 'null' ]";

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
      
        public static bool AreEqual(TextClassifierResult obj1, TextClassifierResult obj2)
        {

            return string.Equals(obj1.Label, obj2.Label, StringComparison.InvariantCulture)
                    && Similarity.ObjectMother.AreEqual(obj1.SimilarityIndexAverages, obj2.SimilarityIndexAverages)
                    && Similarity.ObjectMother.AreEqual(obj1.SimilarityIndexes, obj2.SimilarityIndexes);

        }

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

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 25.09.2022
*/