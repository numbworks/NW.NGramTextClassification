using System;
using System.Collections.Generic;
using System.Reflection;
using NW.NGramTextClassification.TextClassifications;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests.TextClassifications
{
    public static class ObjectMother
    {

        #region Properties

        public static string TextClassifierResult01_AsString
            = $"[ Label: '{LabeledExamples.ObjectMother.ShortLabeledExample01.Label}', SimilarityIndexes: '{Similarity.ObjectMother.SimilarityIndexes.Count}', SimilarityIndexAverages: '{Similarity.ObjectMother.SimilarityIndexAverages.Count}' ]";
        public static string TextClassifierResult_AsStringWithNullLabel
            = $"[ Label: 'null', SimilarityIndexes: '{Similarity.ObjectMother.SimilarityIndexes.Count}', SimilarityIndexAverages: '{Similarity.ObjectMother.SimilarityIndexAverages.Count}' ]";
        public static string TextClassifierResult_AllNulls
            = $"[ Label: 'null', SimilarityIndexes: 'null', SimilarityIndexAverages: 'null' ]";

        public static TextClassifierResult TextClassifierResult02_CompleteLabeledExamples00
            = new TextClassifierResult(
                    label: LabeledExamples.ObjectMother.CreateThirtyCompleteTokenizedExamples()[0].LabeledExample.Label,
                    indexes: Similarity.ObjectMother.CreateSimilarityIndexesForCompleteLabeledExample00(),
                    indexAverages: Similarity.ObjectMother.CreateSimilarityIndexAveragesForCompleteLabeledExample00()
                );

        public static List<string> Snippets_ShortLabeledExamples0102 = new List<string>()
        {

            LabeledExamples.ObjectMother.ShortLabeledExample01.Text,
            LabeledExamples.ObjectMother.ShortLabeledExample02.Text

        };

        #endregion

        #region Methods

        public static bool AreEqual(TextClassifierResult obj1, TextClassifierResult obj2)
        {

            return string.Equals(obj1.Label, obj2.Label, StringComparison.InvariantCulture)
                    && Similarity.ObjectMother.AreEqual(obj1.SimilarityIndexAverages, obj2.SimilarityIndexAverages)
                    && Similarity.ObjectMother.AreEqual(obj1.SimilarityIndexes, obj2.SimilarityIndexes);

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 29.09.2022
*/