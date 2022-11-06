using System;
using System.Collections.Generic;
using NW.NGramTextClassification.TextClassifications;
using NW.NGramTextClassification.NGramTokenization;
using NW.NGramTextClassification.TextSnippets;

namespace NW.NGramTextClassification.UnitTests.TextClassifications
{
    public static class ObjectMother
    {

        #region Properties

        public static string TextClassifierResult_AsString
            = string.Concat(
                    $"[ ",
                    $"TextSnippet: '{TextSnippets.ObjectMother.TextSnippet}', ",
                    $"Label: '{LabeledExamples.ObjectMother.ShortLabeledExample01.Label}', ",
                    $"SimilarityIndexes: '{Similarity.ObjectMother.SimilarityIndexes.Count}', ",
                    $"SimilarityIndexAverages: '{Similarity.ObjectMother.SimilarityIndexAverages.Count}' ",
                    "]"
                );

        public static string TextClassifierResult_AsStringWithNullLabel
            = string.Concat(
                    $"[ ",
                    $"TextSnippet: '{TextSnippets.ObjectMother.TextSnippet}', ",
                    $"Label: 'null', ",
                    $"SimilarityIndexes: '{Similarity.ObjectMother.SimilarityIndexes.Count}', ",
                    $"SimilarityIndexAverages: '{Similarity.ObjectMother.SimilarityIndexAverages.Count}' ",
                    "]"
                );

        public static string TextClassifierResult_AsStringWithNullTextSnippet
            = string.Concat(
                    $"[ ",
                    $"TextSnippet: 'null', ",
                    $"Label: '{LabeledExamples.ObjectMother.ShortLabeledExample01.Label}', ",
                    $"SimilarityIndexes: '{Similarity.ObjectMother.SimilarityIndexes.Count}', ",
                    $"SimilarityIndexAverages: '{Similarity.ObjectMother.SimilarityIndexAverages.Count}' ",
                    "]"
                );

        public static string TextClassifierResult_AllNulls
            = $"[ TextSnippet: 'null', Label: 'null', SimilarityIndexes: 'null', SimilarityIndexAverages: 'null' ]";

        public static TextClassifierResult TextClassifierResult_CompleteLabeledExamples00
            = new TextClassifierResult(
                    textSnippet: new TextSnippet(text: LabeledExamples.ObjectMother.CreateThirtyCompleteTokenizedExamples()[0].LabeledExample.Text),
                    label: LabeledExamples.ObjectMother.CreateThirtyCompleteTokenizedExamples()[0].LabeledExample.Label,
                    indexes: Similarity.ObjectMother.CreateSimilarityIndexesForCompleteLabeledExample00(),
                    indexAverages: Similarity.ObjectMother.CreateSimilarityIndexAveragesForCompleteLabeledExample00()
                );


        public static List<string> Snippets_CompleteLabeledExamples00 = new List<string>()
        {

            LabeledExamples.ObjectMother.CreateThirtyCompleteLabeledExamples()[0].Text

        };
        public static List<TextClassifierResult> TextClassifierResults_CompleteLabeledExamples00 = new List<TextClassifierResult>()
        {

            TextClassifierResult_CompleteLabeledExamples00

        };

        public static List<string> Snippets_Untokenizable = new List<string>()
        {

            LabeledExamples.ObjectMother.ShortLabeledExample03_Untokenizable.Text

        };
        public static List<TextClassifierResult> TextClassifierResults_Untokenizable = new List<TextClassifierResult>()
        {

            TextClassifier.DefaultTextClassifierResult

        };

        public static TextClassifierSession TextClassifierSession_Default
            = new TextClassifierSession(
                settings: new TextClassifierSettings(),
                results: new List<TextClassifierResult>() {
                                TextClassifier.DefaultTextClassifierResult },
                version: new TextClassifier().Version
                );

        public static string TextClassifierrSessionCLE00AsJson_Content = Properties.Resources.TextClassifierrSessionCLE00AsJson;
        public static TextClassifierSession TextClassifierSession_CompleteLabeledExamples00
            = new TextClassifierSession(
                    settings: new TextClassifierSettings(),
                    results: TextClassifierResults_CompleteLabeledExamples00,
                    version: "3.6.0.0"
                );

        public static string TokenizerRuleSetAsJson_Content = Properties.Resources.TokenizerRuleSetAsJson;
        public static NGramTokenizerRuleSet TokenizerRuleSet
            = new NGramTokenizerRuleSet(
                    doForMonogram: true,
                    doForBigram: true,
                    doForTrigram: true,
                    doForFourgram: true,
                    doForFivegram: true
                    );

        #endregion

        #region Methods

        public static bool AreEqual(TextClassifierResult obj1, TextClassifierResult obj2)
        {

            return TextSnippets.ObjectMother.AreEqual(obj1.TextSnippet, obj2.TextSnippet)
                    && string.Equals(obj1.Label, obj2.Label, StringComparison.InvariantCulture)
                    && Similarity.ObjectMother.AreEqual(obj1.SimilarityIndexAverages, obj2.SimilarityIndexAverages)
                    && Similarity.ObjectMother.AreEqual(obj1.SimilarityIndexes, obj2.SimilarityIndexes);

        }
        public static bool AreEqual(List<TextClassifierResult> list1, List<TextClassifierResult> list2)
            => Utilities.ObjectMother.AreEqual(list1, list2, (obj1, obj2) => AreEqual(obj1, obj2));
        public static bool AreEqual(double double1, double double2)
            => Math.Abs(double1 - double2) < 0.0001;
        public static bool AreEqual(TextClassifierSession obj1, TextClassifierSession obj2)
        {

            return AreEqual(obj1.MinimumAccuracySingleLabel, obj2.MinimumAccuracySingleLabel)
                    && AreEqual(obj1.MinimumAccuracyMultipleLabels, obj2.MinimumAccuracyMultipleLabels)
                    && AreEqual(obj1.Results, obj2.Results)
                    && string.Equals(obj1.Version, obj2.Version, StringComparison.InvariantCulture);

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 06.11.2022
*/