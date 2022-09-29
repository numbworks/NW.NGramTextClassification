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

        public static string TextClassifierResult_AsString
            = $"[ Label: '{LabeledExamples.ObjectMother.ShortLabeledExample01.Label}', SimilarityIndexes: '{Similarity.ObjectMother.SimilarityIndexes.Count}', SimilarityIndexAverages: '{Similarity.ObjectMother.SimilarityIndexAverages.Count}' ]";
        public static string TextClassifierResult_AsStringWithNullLabel
            = $"[ Label: 'null', SimilarityIndexes: '{Similarity.ObjectMother.SimilarityIndexes.Count}', SimilarityIndexAverages: '{Similarity.ObjectMother.SimilarityIndexAverages.Count}' ]";
        public static string TextClassifierResult_AllNulls
            = $"[ Label: 'null', SimilarityIndexes: 'null', SimilarityIndexAverages: 'null' ]";

        public static TextClassifierResult TextClassifierResult_CompleteLabeledExamples00
            = new TextClassifierResult(
                    label: LabeledExamples.ObjectMother.CreateThirtyCompleteTokenizedExamples()[0].LabeledExample.Label,
                    indexes: Similarity.ObjectMother.CreateSimilarityIndexesForCompleteLabeledExample00(),
                    indexAverages: Similarity.ObjectMother.CreateSimilarityIndexAveragesForCompleteLabeledExample00()
                );
        public static TextClassifierResult TextClassifierResult_CompleteLabeledExamples01
            = new TextClassifierResult(
                    label: LabeledExamples.ObjectMother.CreateThirtyCompleteTokenizedExamples()[0].LabeledExample.Label,
                    indexes: null,      // TO-DO: Similarity.ObjectMother.CreateSimilarityIndexesForCompleteLabeledExample01(),
                    indexAverages: null // TO-DO: Similarity.ObjectMother.CreateSimilarityIndexAveragesForCompleteLabeledExample01()
                );

        public static List<string> Snippets_CompleteLabeledExamples00And01 = new List<string>()
        {

            LabeledExamples.ObjectMother.CreateThirtyCompleteTokenizedExamples()[0].LabeledExample.Label,
            LabeledExamples.ObjectMother.CreateThirtyCompleteTokenizedExamples()[1].LabeledExample.Label,

        };
        public static List<TextClassifierResult> TextClassifierResults_CompleteLabeledExamples00And01 = new List<TextClassifierResult>()
        {

            TextClassifierResult_CompleteLabeledExamples00,
            TextClassifierResult_CompleteLabeledExamples01

        };

        public static List<string> Snippets_CompleteLabeledExamples00AndUntokenizable = new List<string>()
        {

            LabeledExamples.ObjectMother.CreateThirtyCompleteTokenizedExamples()[0].LabeledExample.Label,
            LabeledExamples.ObjectMother.ShortLabeledExample03_Untokenizable.Text                               // TO-DO: 

        };
        public static List<TextClassifierResult> TextClassifierResults_CompleteLabeledExamples00AndDefault = new List<TextClassifierResult>()
        {

            TextClassifierResult_CompleteLabeledExamples00,
            TextClassifier.DefaultTextClassifierResult

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