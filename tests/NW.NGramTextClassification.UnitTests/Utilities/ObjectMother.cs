using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests.Utilities
{
    public static class ObjectMother
    {

        #region Properties

        public static string TextClassifierResult01_AsString
            = $"[ Label: '{LabeledExamples.ObjectMother.LabeledExample01.Label}', SimilarityIndexes: '{Similarity.ObjectMother.SimilarityIndexes.Count}', SimilarityIndexAverages: '{Similarity.ObjectMother.SimilarityIndexAverages.Count}' ]";       
        public static string TextClassifierResult_AsStringWithNullLabel
            = $"[ Label: 'null', SimilarityIndexes: '{Similarity.ObjectMother.SimilarityIndexes.Count}', SimilarityIndexAverages: '{Similarity.ObjectMother.SimilarityIndexAverages.Count}' ]";
        public static string TextClassifierResult_AllNulls
            = $"[ Label: 'null', SimilarityIndexes: 'null', SimilarityIndexAverages: 'null' ]";

        public static TextClassifierResult TextClassifierResult02_LabeledExamples00
            = new TextClassifierResult(
                    label: LabeledExamples.ObjectMother.CreateThirtyTokenizedExamples()[0].LabeledExample.Label,
                    indexes: Similarity.ObjectMother.CreateThirtySimilarityIndexes(),
                    indexAverages: Similarity.ObjectMother.CreateSimilarityIndexAveragesForThirty()
                );

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