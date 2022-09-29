using System;
using System.Collections.Generic;
using NW.NGramTextClassification.Similarity;

namespace NW.NGramTextClassification.UnitTests.Similarity
{
    public static class ObjectMother
    {

        #region Properties

        public static string SimilarityIndex01_Text = LabeledExamples.ObjectMother.ShortLabeledExample01.Text;
        public static string SimilarityIndex01_Label = "en";
        public static double SimilarityIndex01_Value = 0.23;

        public static SimilarityIndex SimilarityIndex01
            = new SimilarityIndex(
                    text: SimilarityIndex01_Text, 
                    label: SimilarityIndex01_Label, 
                    value: SimilarityIndex01_Value);
        public static string SimilarityIndex01_AsString
            = $"[ Text: '{SimilarityIndex01_Text}', Label: '{SimilarityIndex01_Label}', Value: '{SimilarityIndex01_Value}' ]";

        public static string SimilarityIndex02_Text = LabeledExamples.ObjectMother.ShortLabeledExample02.Text;
        public static string SimilarityIndex02_Label = "sv";
        public static double SimilarityIndex02_Value = 0.76;

        public static SimilarityIndex SimilarityIndex02
            = new SimilarityIndex(
                    text: SimilarityIndex02_Text,
                    label: SimilarityIndex02_Label,
                    value: SimilarityIndex02_Value);

        public static string SimilarityIndexAverage01_Label = "en";
        public static double SimilarityIndexAverage01_Value = 0.54;

        public static SimilarityIndexAverage SimilarityIndexAverage01
            = new SimilarityIndexAverage(SimilarityIndexAverage01_Label, SimilarityIndexAverage01_Value);
        public static string SimilarityIndexAverage01_AsString
            = $"[ Label: '{SimilarityIndexAverage01_Label}', Value: '{SimilarityIndexAverage01_Value}' ]";

        public static List<SimilarityIndex> SimilarityIndexes
            = new List<SimilarityIndex>()
                {
                    SimilarityIndex01,
                    SimilarityIndex02
                };

        public static List<SimilarityIndexAverage> SimilarityIndexAverages
            = new List<SimilarityIndexAverage>()
                {
                    SimilarityIndexAverage01
                };

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

        public static bool AreEqual(List<SimilarityIndex> list1, List<SimilarityIndex> list2)
            => Utilities.ObjectMother.AreEqual(list1, list2, (obj1, obj2) => AreEqual(obj1, obj2));
        public static bool AreEqual(List<SimilarityIndexAverage> list1, List<SimilarityIndexAverage> list2)
            => Utilities.ObjectMother.AreEqual(list1, list2, (obj1, obj2) => AreEqual(obj1, obj2));

        public static List<SimilarityIndex> CreateSimilarityIndexesForCompleteLabeledExample00()
        {

            List<SimilarityIndex> similarityIndexes = new List<SimilarityIndex>()
            {

                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyCompleteTokenizedExamples()[0].LabeledExample.Text, label: "en", value: 1),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyCompleteTokenizedExamples()[1].LabeledExample.Text, label: "en", value: 0.031696),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyCompleteTokenizedExamples()[2].LabeledExample.Text, label: "en", value: 0.017512),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyCompleteTokenizedExamples()[3].LabeledExample.Text, label: "en", value: 0.022472),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyCompleteTokenizedExamples()[4].LabeledExample.Text, label: "en", value: 0.017927),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyCompleteTokenizedExamples()[5].LabeledExample.Text, label: "en", value: 0.018717),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyCompleteTokenizedExamples()[6].LabeledExample.Text, label: "en", value: 0.014844),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyCompleteTokenizedExamples()[7].LabeledExample.Text, label: "en", value: 0.017185),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyCompleteTokenizedExamples()[8].LabeledExample.Text, label: "en", value: 0.024096),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyCompleteTokenizedExamples()[9].LabeledExample.Text, label: "en", value: 0.020548),

                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyCompleteTokenizedExamples()[10].LabeledExample.Text, label: "sv", value: 0.000741),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyCompleteTokenizedExamples()[11].LabeledExample.Text, label: "sv", value: 0),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyCompleteTokenizedExamples()[12].LabeledExample.Text, label: "sv", value: 0),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyCompleteTokenizedExamples()[13].LabeledExample.Text, label: "sv", value: 0.000802),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyCompleteTokenizedExamples()[14].LabeledExample.Text, label: "sv", value: 0.000745),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyCompleteTokenizedExamples()[15].LabeledExample.Text, label: "sv", value: 0),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyCompleteTokenizedExamples()[16].LabeledExample.Text, label: "sv", value: 0.000861),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyCompleteTokenizedExamples()[17].LabeledExample.Text, label: "sv", value: 0.00074),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyCompleteTokenizedExamples()[18].LabeledExample.Text, label: "sv", value: 0.000733),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyCompleteTokenizedExamples()[19].LabeledExample.Text, label: "sv", value: 0.000905),

                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyCompleteTokenizedExamples()[20].LabeledExample.Text, label: "dk", value: 0.002333),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyCompleteTokenizedExamples()[21].LabeledExample.Text, label: "dk", value: 0.000895),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyCompleteTokenizedExamples()[22].LabeledExample.Text, label: "dk", value: 0.000843),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyCompleteTokenizedExamples()[23].LabeledExample.Text, label: "dk", value: 0.004918),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyCompleteTokenizedExamples()[24].LabeledExample.Text, label: "dk", value: 0.001668),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyCompleteTokenizedExamples()[25].LabeledExample.Text, label: "dk", value: 0.003027),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyCompleteTokenizedExamples()[26].LabeledExample.Text, label: "dk", value: 0.002618),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyCompleteTokenizedExamples()[27].LabeledExample.Text, label: "dk", value: 0.00367),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyCompleteTokenizedExamples()[28].LabeledExample.Text, label: "dk", value: 0.002575),
                new SimilarityIndex(text: LabeledExamples.ObjectMother.CreateThirtyCompleteTokenizedExamples()[29].LabeledExample.Text, label: "dk", value: 0.003934)

            };

            return similarityIndexes;

        }
        public static List<SimilarityIndexAverage> CreateSimilarityIndexAveragesForCompleteLabeledExample00()
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
    Last Update: 29.09.2022
*/