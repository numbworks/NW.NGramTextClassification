using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using NW.NGramTextClassification.LabeledExamples;
using NW.NGramTextClassification.NGrams;
using NW.NGramTextClassification.NGramTokenization;
using NW.NGramTextClassification.Similarity;

namespace NW.NGramTextClassification.UnitTests
{
    public static class ObjectMother
    {

        #region Shared

        public static ITokenizationStrategy Shared_TokenizationStrategyDefault = new TokenizationStrategy();
        public static TokenizationStrategy Shared_TokenizationStrategyCustom = new TokenizationStrategy("[a-Z]", ";", false);

        public static LabeledExample Shared_LabeledExample01 
            = new LabeledExample(label: "en", text: "We are looking for several skilled and driven developers to join our team.");
        public static List<Monogram> Shared_LabeledExample01_Monograms = new List<Monogram>()
        {

            new Monogram(Shared_TokenizationStrategyDefault, "we"),
            new Monogram(Shared_TokenizationStrategyDefault, "are"),
            new Monogram(Shared_TokenizationStrategyDefault, "looking"),
            new Monogram(Shared_TokenizationStrategyDefault, "for"),
            new Monogram(Shared_TokenizationStrategyDefault, "several"),
            new Monogram(Shared_TokenizationStrategyDefault, "skilled"),
            new Monogram(Shared_TokenizationStrategyDefault, "and"),
            new Monogram(Shared_TokenizationStrategyDefault, "driven"),
            new Monogram(Shared_TokenizationStrategyDefault, "developers"),
            new Monogram(Shared_TokenizationStrategyDefault, "to"),
            new Monogram(Shared_TokenizationStrategyDefault, "join"),
            new Monogram(Shared_TokenizationStrategyDefault, "our"),
            new Monogram(Shared_TokenizationStrategyDefault, "team")

        };
        public static List<Bigram> Shared_LabeledExample01_Bigrams = new List<Bigram>()
        {

            new Bigram(Shared_TokenizationStrategyDefault, "we are"),
            new Bigram(Shared_TokenizationStrategyDefault, "are looking"),
            new Bigram(Shared_TokenizationStrategyDefault, "looking for"),
            new Bigram(Shared_TokenizationStrategyDefault, "for several"),
            new Bigram(Shared_TokenizationStrategyDefault, "several skilled"),
            new Bigram(Shared_TokenizationStrategyDefault, "skilled and"),
            new Bigram(Shared_TokenizationStrategyDefault, "and driven"),
            new Bigram(Shared_TokenizationStrategyDefault, "driven developers"),
            new Bigram(Shared_TokenizationStrategyDefault, "developers to"),
            new Bigram(Shared_TokenizationStrategyDefault, "to join"),
            new Bigram(Shared_TokenizationStrategyDefault, "join our"),
            new Bigram(Shared_TokenizationStrategyDefault, "our team"),
            new Bigram(Shared_TokenizationStrategyDefault, "team")

        };
        public static List<Trigram> Shared_LabeledExample01_Trigrams = new List<Trigram>()
        {

            new Trigram(Shared_TokenizationStrategyDefault, "we are looking"),
            new Trigram(Shared_TokenizationStrategyDefault, "are looking for"),
            new Trigram(Shared_TokenizationStrategyDefault, "looking for several"),
            new Trigram(Shared_TokenizationStrategyDefault, "for several skilled"),
            new Trigram(Shared_TokenizationStrategyDefault, "several skilled and"),
            new Trigram(Shared_TokenizationStrategyDefault, "skilled and driven"),
            new Trigram(Shared_TokenizationStrategyDefault, "and driven developers"),
            new Trigram(Shared_TokenizationStrategyDefault, "driven developers to"),
            new Trigram(Shared_TokenizationStrategyDefault, "developers to join"),
            new Trigram(Shared_TokenizationStrategyDefault, "to join our"),
            new Trigram(Shared_TokenizationStrategyDefault, "join our team"),
            new Trigram(Shared_TokenizationStrategyDefault, "our team"),
            new Trigram(Shared_TokenizationStrategyDefault, "team")

        };
        public static List<Fourgram> Shared_LabeledExample01_Fourgrams = new List<Fourgram>()
        {

            new Fourgram(Shared_TokenizationStrategyDefault, "we are looking for"),
            new Fourgram(Shared_TokenizationStrategyDefault, "are looking for several"),
            new Fourgram(Shared_TokenizationStrategyDefault, "looking for several skilled"),
            new Fourgram(Shared_TokenizationStrategyDefault, "for several skilled and"),
            new Fourgram(Shared_TokenizationStrategyDefault, "several skilled and driven"),
            new Fourgram(Shared_TokenizationStrategyDefault, "skilled and driven developers"),
            new Fourgram(Shared_TokenizationStrategyDefault, "and driven developers to"),
            new Fourgram(Shared_TokenizationStrategyDefault, "driven developers to join"),
            new Fourgram(Shared_TokenizationStrategyDefault, "developers to join our"),
            new Fourgram(Shared_TokenizationStrategyDefault, "to join our team"),
            new Fourgram(Shared_TokenizationStrategyDefault, "join our team"),
            new Fourgram(Shared_TokenizationStrategyDefault, "our team"),
            new Fourgram(Shared_TokenizationStrategyDefault, "team")

        };
        public static List<Fivegram> Shared_LabeledExample01_Fivegrams = new List<Fivegram>()
        {

            new Fivegram(Shared_TokenizationStrategyDefault, "we are looking for several"),
            new Fivegram(Shared_TokenizationStrategyDefault, "are looking for several skilled"),
            new Fivegram(Shared_TokenizationStrategyDefault, "looking for several skilled and"),
            new Fivegram(Shared_TokenizationStrategyDefault, "for several skilled and driven"),
            new Fivegram(Shared_TokenizationStrategyDefault, "several skilled and driven developers"),
            new Fivegram(Shared_TokenizationStrategyDefault, "skilled and driven developers to"),
            new Fivegram(Shared_TokenizationStrategyDefault, "and driven developers to join"),
            new Fivegram(Shared_TokenizationStrategyDefault, "driven developers to join our"),
            new Fivegram(Shared_TokenizationStrategyDefault, "developers to join our team"),
            new Fivegram(Shared_TokenizationStrategyDefault, "to join our team"),
            new Fivegram(Shared_TokenizationStrategyDefault, "join our team"),
            new Fivegram(Shared_TokenizationStrategyDefault, "our team"),
            new Fivegram(Shared_TokenizationStrategyDefault, "team")

        };
        public static List<INGram> Shared_LabeledExample01_NGrams = new List<INGram>() 
        {

            Shared_LabeledExample01_Monograms[0],
            Shared_LabeledExample01_Monograms[1],
            Shared_LabeledExample01_Monograms[2],
            Shared_LabeledExample01_Monograms[3],
            Shared_LabeledExample01_Monograms[4],
            Shared_LabeledExample01_Monograms[5],
            Shared_LabeledExample01_Monograms[6],
            Shared_LabeledExample01_Monograms[7],
            Shared_LabeledExample01_Monograms[8],
            Shared_LabeledExample01_Monograms[9],
            Shared_LabeledExample01_Monograms[10],
            Shared_LabeledExample01_Monograms[11],
            Shared_LabeledExample01_Monograms[12],

            Shared_LabeledExample01_Bigrams[0],
            Shared_LabeledExample01_Bigrams[1],
            Shared_LabeledExample01_Bigrams[2],
            Shared_LabeledExample01_Bigrams[3],
            Shared_LabeledExample01_Bigrams[4],
            Shared_LabeledExample01_Bigrams[5],
            Shared_LabeledExample01_Bigrams[6],
            Shared_LabeledExample01_Bigrams[7],
            Shared_LabeledExample01_Bigrams[8],
            Shared_LabeledExample01_Bigrams[9],
            Shared_LabeledExample01_Bigrams[10],
            Shared_LabeledExample01_Bigrams[11],
            Shared_LabeledExample01_Bigrams[12],

            Shared_LabeledExample01_Trigrams[0],
            Shared_LabeledExample01_Trigrams[1],
            Shared_LabeledExample01_Trigrams[2],
            Shared_LabeledExample01_Trigrams[3],
            Shared_LabeledExample01_Trigrams[4],
            Shared_LabeledExample01_Trigrams[5],
            Shared_LabeledExample01_Trigrams[6],
            Shared_LabeledExample01_Trigrams[7],
            Shared_LabeledExample01_Trigrams[8],
            Shared_LabeledExample01_Trigrams[9],
            Shared_LabeledExample01_Trigrams[10],
            Shared_LabeledExample01_Trigrams[11],
            Shared_LabeledExample01_Trigrams[12],

            Shared_LabeledExample01_Fourgrams[0],
            Shared_LabeledExample01_Fourgrams[1],
            Shared_LabeledExample01_Fourgrams[2],
            Shared_LabeledExample01_Fourgrams[3],
            Shared_LabeledExample01_Fourgrams[4],
            Shared_LabeledExample01_Fourgrams[5],
            Shared_LabeledExample01_Fourgrams[6],
            Shared_LabeledExample01_Fourgrams[7],
            Shared_LabeledExample01_Fourgrams[8],
            Shared_LabeledExample01_Fourgrams[9],
            Shared_LabeledExample01_Fourgrams[10],
            Shared_LabeledExample01_Fourgrams[11],
            Shared_LabeledExample01_Fourgrams[12],

            Shared_LabeledExample01_Fivegrams[0],
            Shared_LabeledExample01_Fivegrams[1],
            Shared_LabeledExample01_Fivegrams[2],
            Shared_LabeledExample01_Fivegrams[3],
            Shared_LabeledExample01_Fivegrams[4],
            Shared_LabeledExample01_Fivegrams[5],
            Shared_LabeledExample01_Fivegrams[6],
            Shared_LabeledExample01_Fivegrams[7],
            Shared_LabeledExample01_Fivegrams[8],
            Shared_LabeledExample01_Fivegrams[9],
            Shared_LabeledExample01_Fivegrams[10],
            Shared_LabeledExample01_Fivegrams[11],
            Shared_LabeledExample01_Fivegrams[12]

        };
        public static TokenizedExample Shared_TokenizedExample01
            = new TokenizedExample(labeledExample: Shared_LabeledExample01, nGrams: Shared_LabeledExample01_NGrams);
        public static string Shared_LabeledExample01_AsString
            = string.Concat(
                        "[ ",
                        $"Label: '{Shared_LabeledExample01.Label}', ",
                        $"Text: '{Shared_LabeledExample01.Text}' ",
                        "]"
                    );
        public static string Shared_TokenizedExample01_AsString
            = string.Concat(
                        "[ ",
                        $"Label: '{Shared_TokenizedExample01.LabeledExample.Label}', ",
                        $"Text: '{Shared_TokenizedExample01.LabeledExample.Text}', ",
                        $"NGrams: '{Shared_TokenizedExample01.NGrams.Count}' ",
                        "]"
                    );

        public static LabeledExample Shared_LabeledExample02
            = new LabeledExample(label: "sv", text: "Vår kund erbjuder trivsel, arbetsglädje och en trygg arbetsmiljö.");
        public static List<Monogram> Shared_LabeledExample02_Monograms = new List<Monogram>()
        {

            new Monogram(Shared_TokenizationStrategyDefault, "vår"),
            new Monogram(Shared_TokenizationStrategyDefault, "kund"),
            new Monogram(Shared_TokenizationStrategyDefault, "erbjuder"),
            new Monogram(Shared_TokenizationStrategyDefault, "trivsel"),
            new Monogram(Shared_TokenizationStrategyDefault, "arbetsglädje"),
            new Monogram(Shared_TokenizationStrategyDefault, "och"),
            new Monogram(Shared_TokenizationStrategyDefault, "en"),
            new Monogram(Shared_TokenizationStrategyDefault, "trygg"),
            new Monogram(Shared_TokenizationStrategyDefault, "arbetsmiljö")

        };
        public static List<Bigram> Shared_LabeledExample02_Bigrams = new List<Bigram>()
        {

            new Bigram(Shared_TokenizationStrategyDefault, "vår kund"),
            new Bigram(Shared_TokenizationStrategyDefault, "kund erbjuder"),
            new Bigram(Shared_TokenizationStrategyDefault, "erbjuder trivsel"),
            new Bigram(Shared_TokenizationStrategyDefault, "trivsel arbetsglädje"),
            new Bigram(Shared_TokenizationStrategyDefault, "arbetsglädje och"),
            new Bigram(Shared_TokenizationStrategyDefault, "och en"),
            new Bigram(Shared_TokenizationStrategyDefault, "en trygg"),
            new Bigram(Shared_TokenizationStrategyDefault, "trygg arbetsmiljö"),
            new Bigram(Shared_TokenizationStrategyDefault, "arbetsmiljö")

        };
        public static List<Trigram> Shared_LabeledExample02_Trigrams = new List<Trigram>()
        {

            new Trigram(Shared_TokenizationStrategyDefault, "vår kund erbjuder"),
            new Trigram(Shared_TokenizationStrategyDefault, "kund erbjuder trivsel"),
            new Trigram(Shared_TokenizationStrategyDefault, "erbjuder trivsel arbetsglädje"),
            new Trigram(Shared_TokenizationStrategyDefault, "trivsel arbetsglädje och"),
            new Trigram(Shared_TokenizationStrategyDefault, "arbetsglädje och en"),
            new Trigram(Shared_TokenizationStrategyDefault, "och en trygg"),
            new Trigram(Shared_TokenizationStrategyDefault, "en trygg arbetsmiljö"),
            new Trigram(Shared_TokenizationStrategyDefault, "trygg arbetsmiljö"),
            new Trigram(Shared_TokenizationStrategyDefault, "arbetsmiljö")

        };
        public static List<Fourgram> Shared_LabeledExample02_Fourgrams = new List<Fourgram>()
        {

            new Fourgram(Shared_TokenizationStrategyDefault, "vår kund erbjuder trivsel"),
            new Fourgram(Shared_TokenizationStrategyDefault, "kund erbjuder trivsel arbetsglädje"),
            new Fourgram(Shared_TokenizationStrategyDefault, "erbjuder trivsel arbetsglädje och"),
            new Fourgram(Shared_TokenizationStrategyDefault, "trivsel arbetsglädje och en"),
            new Fourgram(Shared_TokenizationStrategyDefault, "arbetsglädje och en trygg"),
            new Fourgram(Shared_TokenizationStrategyDefault, "och en trygg arbetsmiljö"),
            new Fourgram(Shared_TokenizationStrategyDefault, "en trygg arbetsmiljö"),
            new Fourgram(Shared_TokenizationStrategyDefault, "trygg arbetsmiljö"),
            new Fourgram(Shared_TokenizationStrategyDefault, "arbetsmiljö")

        };
        public static List<Fivegram> Shared_LabeledExample02_Fivegrams = new List<Fivegram>()
        {

            new Fivegram(Shared_TokenizationStrategyDefault, "vår kund erbjuder trivsel arbetsglädje"),
            new Fivegram(Shared_TokenizationStrategyDefault, "kund erbjuder trivsel arbetsglädje och"),
            new Fivegram(Shared_TokenizationStrategyDefault, "erbjuder trivsel arbetsglädje och en"),
            new Fivegram(Shared_TokenizationStrategyDefault, "trivsel arbetsglädje och en trygg"),
            new Fivegram(Shared_TokenizationStrategyDefault, "arbetsglädje och en trygg arbetsmiljö"),
            new Fivegram(Shared_TokenizationStrategyDefault, "och en trygg arbetsmiljö"),
            new Fivegram(Shared_TokenizationStrategyDefault, "en trygg arbetsmiljö"),
            new Fivegram(Shared_TokenizationStrategyDefault, "trygg arbetsmiljö"),
            new Fivegram(Shared_TokenizationStrategyDefault, "arbetsmiljö")

        };
        public static List<INGram> Shared_LabeledExample02_NGrams = new List<INGram>()
        {

            Shared_LabeledExample02_Monograms[0],
            Shared_LabeledExample02_Monograms[1],
            Shared_LabeledExample02_Monograms[2],
            Shared_LabeledExample02_Monograms[3],
            Shared_LabeledExample02_Monograms[4],
            Shared_LabeledExample02_Monograms[5],
            Shared_LabeledExample02_Monograms[6],
            Shared_LabeledExample02_Monograms[7],
            Shared_LabeledExample02_Monograms[8],

            Shared_LabeledExample02_Bigrams[0],
            Shared_LabeledExample02_Bigrams[1],
            Shared_LabeledExample02_Bigrams[2],
            Shared_LabeledExample02_Bigrams[3],
            Shared_LabeledExample02_Bigrams[4],
            Shared_LabeledExample02_Bigrams[5],
            Shared_LabeledExample02_Bigrams[6],
            Shared_LabeledExample02_Bigrams[7],
            Shared_LabeledExample02_Bigrams[8],

            Shared_LabeledExample02_Trigrams[0],
            Shared_LabeledExample02_Trigrams[1],
            Shared_LabeledExample02_Trigrams[2],
            Shared_LabeledExample02_Trigrams[3],
            Shared_LabeledExample02_Trigrams[4],
            Shared_LabeledExample02_Trigrams[5],
            Shared_LabeledExample02_Trigrams[6],
            Shared_LabeledExample02_Trigrams[7],
            Shared_LabeledExample02_Trigrams[8],

            Shared_LabeledExample02_Fourgrams[0],
            Shared_LabeledExample02_Fourgrams[1],
            Shared_LabeledExample02_Fourgrams[2],
            Shared_LabeledExample02_Fourgrams[3],
            Shared_LabeledExample02_Fourgrams[4],
            Shared_LabeledExample02_Fourgrams[5],
            Shared_LabeledExample02_Fourgrams[6],
            Shared_LabeledExample02_Fourgrams[7],
            Shared_LabeledExample02_Fourgrams[8],

            Shared_LabeledExample02_Fivegrams[0],
            Shared_LabeledExample02_Fivegrams[1],
            Shared_LabeledExample02_Fivegrams[2],
            Shared_LabeledExample02_Fivegrams[3],
            Shared_LabeledExample02_Fivegrams[4],
            Shared_LabeledExample02_Fivegrams[5],
            Shared_LabeledExample02_Fivegrams[6],
            Shared_LabeledExample02_Fivegrams[7],
            Shared_LabeledExample02_Fivegrams[8]

        };
        public static TokenizedExample Shared_TokenizedExample02
            = new TokenizedExample(labeledExample: Shared_LabeledExample02, nGrams: Shared_LabeledExample02_NGrams);

        public static List<LabeledExample> Shared_LabeledExamples = new List<LabeledExample>()
        {

            Shared_LabeledExample01,
            Shared_LabeledExample02

        };
        public static List<TokenizedExample> Shared_TokenizedExamples = new List<TokenizedExample>()
        {

            Shared_TokenizedExample01,
            Shared_TokenizedExample02

        };

        public static INGramTokenizerRuleSet Shared_RuleSet_Mono
            = new NGramTokenizerRuleSet(true, false, false, false, false);
        public static INGramTokenizerRuleSet Shared_RuleSet_MonoBi
            = new NGramTokenizerRuleSet(true, true, false, false, false);
        public static INGramTokenizerRuleSet Shared_RuleSet_MonoBiTri
            = new NGramTokenizerRuleSet(true, true, true, false, false);
        public static INGramTokenizerRuleSet Shared_RuleSet_MonoBiTriFour
            = new NGramTokenizerRuleSet(true, true, true, true, false);
        public static INGramTokenizerRuleSet Shared_RuleSet_MonoBiTriFourFive
            = new NGramTokenizerRuleSet(true, true, true, true, true);
        public static INGramTokenizerRuleSet Shared_RuleSet_Five
            = new NGramTokenizerRuleSet(false, false, false, false, true);

        public static TokenizedExample Shared_TokenizedExample01_Mono
            = new TokenizedExample(
                    labeledExample: Shared_LabeledExample01,
                    nGrams: CreateNGrams(
                                monograms: Shared_LabeledExample01_Monograms
                        ));
        public static TokenizedExample Shared_TokenizedExample01_MonoBi
            = new TokenizedExample(
                    labeledExample: Shared_LabeledExample01,
                    nGrams: CreateNGrams(
                                monograms: Shared_LabeledExample01_Monograms,
                                bigrams: Shared_LabeledExample01_Bigrams
                        ));
        public static TokenizedExample Shared_TokenizedExample01_MonoBiTri
            = new TokenizedExample(
                    labeledExample: Shared_LabeledExample01,
                    nGrams: CreateNGrams(
                                monograms: Shared_LabeledExample01_Monograms,
                                bigrams: Shared_LabeledExample01_Bigrams,
                                trigrams: Shared_LabeledExample01_Trigrams
                        ));
        public static TokenizedExample Shared_TokenizedExample01_MonoBiTriFour
            = new TokenizedExample(
                    labeledExample: Shared_LabeledExample01,
                    nGrams: CreateNGrams(
                                monograms: Shared_LabeledExample01_Monograms,
                                bigrams: Shared_LabeledExample01_Bigrams,
                                trigrams: Shared_LabeledExample01_Trigrams,
                                fourgrams: Shared_LabeledExample01_Fourgrams
                        ));
        public static TokenizedExample Shared_TokenizedExample01_MonoBiTriFourFive
            = new TokenizedExample(
                        labeledExample: Shared_LabeledExample01,
                        nGrams: CreateNGrams(
                                    monograms: Shared_LabeledExample01_Monograms,
                                    bigrams: Shared_LabeledExample01_Bigrams,
                                    trigrams: Shared_LabeledExample01_Trigrams,
                                    fourgrams: Shared_LabeledExample01_Fourgrams,
                                    fivegrams: Shared_LabeledExample01_Fivegrams
                            ));

        public static LabeledExample Shared_LabeledExample03_Untokenizable
            = new LabeledExample(label: "en", text: "We");
        public static List<LabeledExample> Shared_LabeledExamples_Untokenizable = new List<LabeledExample>()
        {

            Shared_LabeledExample01,
            Shared_LabeledExample02,
            Shared_LabeledExample03_Untokenizable

        };

        #endregion

        #region ANGram

        public static ushort ANGram_FakeGram1_N = 1;
        public static ushort ANGram_FakeGram2_N = 1;
        public static FakeGram ANGram_FakeGram1
            = new FakeGram(ANGram_FakeGram1_N, Shared_TokenizationStrategyDefault, Shared_LabeledExample01_Monograms[0].Value);
        public static FakeGram ANGram_FakeGram2
            = new FakeGram(ANGram_FakeGram2_N, Shared_TokenizationStrategyDefault, Shared_LabeledExample01_Monograms[1].Value);
        public static int ANGram_FakeGram1_HashCode
            = (ANGram_FakeGram1_N, Shared_TokenizationStrategyDefault, Shared_LabeledExample01_Monograms[0].Value).GetHashCode();

        #endregion

        #region ArrayManager

        public static string ArrayManager_Delimiter1 = ";";
        public static uint ArrayManager_StartIndex1 = 0;
        public static uint ArrayManager_Length1 = 2;
        public static string[] ArrayManager_Array1_WithDelimiter1
            = new[] {
                "Dodge",
                ArrayManager_Delimiter1,
                "Datsun",
                ArrayManager_Delimiter1,
                "Jaguar",
                ArrayManager_Delimiter1,
                "DeLorean"
            };
        public static string[] ArrayManager_Array1_Subset1 = new[] { "Dodge", "Datsun" };

        #endregion

        #region NGramTokenizer

        public static string NGramTokenizer_Text_NonAlphanumerical = ";;;-- £/£&$£";
        public static TokenizationStrategy NGramTokenizer_TokenizationStrategy_NonAlphanumerical
            = new TokenizationStrategy(
                    pattern: NGramTokenizer_Text_NonAlphanumerical,
                    delimiter: TokenizationStrategy.DefaultDelimiter,
                    toLowercase: TokenizationStrategy.DefaultToLowercase
                );

        #endregion

        #region SimilarityIndex

        public static string SimilarityIndex01_Text = Shared_LabeledExample01.Text;
        public static string SimilarityIndex01_Label = "en";
        public static double SimilarityIndex01_Value = 0.23;
        public static SimilarityIndex SimilarityIndex01
            = new SimilarityIndex(
                    text: SimilarityIndex01_Text, 
                    label: SimilarityIndex01_Label, 
                    value: SimilarityIndex01_Value);
        public static string SimilarityIndex01_AsString
            = $"[ Text: '{SimilarityIndex01_Text}', Label: '{SimilarityIndex01_Label}', Value: '{SimilarityIndex01_Value}' ]";

        public static string SimilarityIndex02_Text = Shared_LabeledExample02.Text;
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

        public static SimilarityIndex TextClassifier_Text1_SimilarityIndex1 = new SimilarityIndex(Shared_LabeledExample01.Text, "en", 1);
        public static SimilarityIndex TextClassifier_Text1_SimilarityIndex2 = new SimilarityIndex(Shared_LabeledExample02.Text, "sv", 0);
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
                        Shared_LabeledExample01.Label,
                        TextClassifier_Text1_SimilarityIndexes,
                        TextClassifier_Text1_SimilarityIndexAverages);
        public static List<INGram> TextClassifier_Text1_NGrams = Shared_LabeledExample01_NGrams;
        public static List<string> TextClassifier_Text1_UniqueLabels
            = new List<string>()
            {
                TextClassifier_Text1_SimilarityIndex1.Label,
                TextClassifier_Text1_SimilarityIndex2.Label,
            };

        public static TextClassifierResult TextClassifier_TextClassifierResult_LabeledExamples00
            = new TextClassifierResult(
                    label: CreateThirtyTokenizedExamples()[0].LabeledExample.Label,
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
            = $"[ Label: '{Shared_LabeledExample01.Label}', SimilarityIndexes: '{TextClassifierResult_SimilarityIndexes.Count}', SimilarityIndexAverages: '{TextClassifierResult_SimilarityIndexAverages.Count}' ]";
        public static string TextClassifierResult_AsStringWithNullLabel
            = $"[ Label: 'null', SimilarityIndexes: '{TextClassifierResult_SimilarityIndexes.Count}', SimilarityIndexAverages: '{TextClassifierResult_SimilarityIndexAverages.Count}' ]";
        public static string TextClassifierResult_AllNulls
            = $"[ Label: 'null', SimilarityIndexes: 'null', SimilarityIndexAverages: 'null' ]";
        public static string TextClassifierResult_VariableName_Indexes = "indexes";
        public static string TextClassifierResult_VariableName_IndexAverages = "indexAverages";

        #endregion

        #region TokenizationStrategy

        public static string TokenizationStrategy_ToString
            = $"[ Pattern: '{TokenizationStrategy.DefaultPattern}', Delimiter: '{TokenizationStrategy.DefaultDelimiter}', ToLowercase: '{TokenizationStrategy.DefaultToLowercase}' ]";
        public static int TokenizationStrategy_DefaultHashCode
            = (TokenizationStrategy.DefaultPattern, TokenizationStrategy.DefaultDelimiter, TokenizationStrategy.DefaultToLowercase).GetHashCode();

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

        public static bool AreEqual(LabeledExample obj1, LabeledExample obj2)
        {

            return string.Equals(obj1.Text, obj2.Text, StringComparison.InvariantCulture)
                    && string.Equals(obj1.Label, obj2.Label, StringComparison.InvariantCulture);

        }
        public static bool AreEqual(TokenizedExample obj1, TokenizedExample obj2)
        {

            return AreEqual(obj1.LabeledExample, obj2.LabeledExample)
                    && AreEqual(obj1.NGrams, obj2.NGrams);

        }
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

        public static bool AreEqual(List<TokenizedExample> list1, List<TokenizedExample> list2)
            => AreEqual(list1, list2, (obj1, obj2) => AreEqual(obj1, obj2));
        public static bool AreEqual(List<SimilarityIndex> list1, List<SimilarityIndex> list2)
            => AreEqual(list1, list2, (obj1, obj2) => AreEqual(obj1, obj2));
        public static bool AreEqual(List<SimilarityIndexAverage> list1, List<SimilarityIndexAverage> list2)
            => AreEqual(list1, list2, (obj1, obj2) => AreEqual(obj1, obj2));

        public static bool AreEqual(List<Monogram> list1, List<Monogram> list2)
            => AreEqual(list1, list2, (obj1, obj2) => obj1.Equals(obj2));
        public static bool AreEqual(List<Bigram> list1, List<Bigram> list2)
            => AreEqual(list1, list2, (obj1, obj2) => obj1.Equals(obj2));
        public static bool AreEqual(List<Trigram> list1, List<Trigram> list2)
            => AreEqual(list1, list2, (obj1, obj2) => obj1.Equals(obj2));
        public static bool AreEqual(List<Fourgram> list1, List<Fourgram> list2)
            => AreEqual(list1, list2, (obj1, obj2) => obj1.Equals(obj2));
        public static bool AreEqual(List<Fivegram> list1, List<Fivegram> list2)
            => AreEqual(list1, list2, (obj1, obj2) => obj1.Equals(obj2));
        public static bool AreEqual(List<INGram> list1, List<INGram> list2)
            => AreEqual(list1, list2, (obj1, obj2) => obj1.Equals(obj2));

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

        public static List<INGram> CreateNGrams
            (List<Monogram> monograms, List<Bigram> bigrams = null, List<Trigram> trigrams = null, List<Fourgram> fourgrams = null, List<Fivegram> fivegrams = null)
        {

            List<INGram> ngrams = new List<INGram>();
            ngrams.AddRange(monograms);

            if (bigrams != null)
                ngrams.AddRange(bigrams);

            if (trigrams != null)
                ngrams.AddRange(trigrams);

            if (fourgrams != null)
                ngrams.AddRange(fourgrams);

            if (fivegrams != null)
                ngrams.AddRange(fivegrams);

            return ngrams;

        }

        public static List<LabeledExample> CreateThirtyLabeledExamples()
        {

            List<LabeledExample> labeledExamples = new List<LabeledExample>()
            {

                new LabeledExample("en", "VerksamhetsbeskrivningGoGift is a company which focuses on innovative gifts and gift cards solutions. GoGift has activities in every Nordic country and addresses both private as well as corporate customers. GoGift distributes gift cards for thousands of shops, brands and experiences delivered with post, email or SMS. The Super Gift Card, one of the most popular gifts, makes it possible for the gift recipient to choose their own gift at GoGift.com. With more than 7000 business to business customers worldwide GoGift is also a preferred supplier of corporate gifts.ArbetsuppgifterYou will play a very important role developing and maintaining our core platform as well as numerous APIs, applications and websites. You will be a key player in our dev team with highly skilled and experienced software developers (public and external), UX/UI designers, dev ops specialists and online content manager."),
                new LabeledExample("en", "You will report to the Team Manager. You will get the opportunity to define your own tasks as well influence planning and decisions regarding technologies and architecture going forward. After a focused introduction, you will be involved in developing our core engine and existing platforms as well as numerous new projects; front-end, back-end, public and external API, native and web apps depending on your skill set and expertise. Your main tasks will be to design and develop new features, maintain the existing products and optimise workflows based on feedback from different stake holders. You will be working with a fully automated test and deploy set up where code quality and smart scalable solutions are of the essence. Collaboration and team work is at the core of this position and you will be expected to be able to handle several projects or tasks simultaneously."),
                new LabeledExample("en", "You will become part of a very exciting and international company, with big ambitions, yet informal work environment. Utbildning/Erfarenhet' Excellent written and verbal communication skills (English) ' Comfortable contributing in a group and able to work independently ' Experience with .NET development ' Experience developing with a least a couple or more languages and technologies such as: o C#, JavaScript, o Html/CSS o SQL, Maria DB o ASP .Net MVC, A plus if you are familiar with one or more of the following technologies and tools: Xamarin CI/CD Docker Kubernetes React SASS GitHub Angular Experience with RESTful API development."),
                new LabeledExample("en", "E.ON is one of the world's largest investor-owned power and gas companies. At facilities across Europe, Russia, and North America, our nearly 62,000 employees generated just under EUR 122.5 billion in sales. We have an ambitious objective: to make energy cleaner and better wherever we operate.E.ON is looking for a Controller Business IT Everyone at E.ON is unique and there’s a common goal that pulls us all together. It’s our dedication to convert to 100% renewable and recycled energy by 2025. That’s no small ambition. To make it happen, we need ground-breaking people brave enough to make a difference.  As a Controller Business IT you will give professional guidance to accountable IT Manager in international environment to ensure cost efficiency as well as cost effectiveness. You will be partnering with the assigned business controlling to ensure transparency and influenceability of IT costs including recommendations for consumption improvements."),
                new LabeledExample("en", "The role also includes to ensure integration of commercial IT-processes and business processes for the assigned business units, especially provide information relevant for planning.  You’ll be part of our international team and working close to business to active support of Business IT managers, IT board members and controllers of business units in all IT topics, especially regarding charging if IT cost.  This includes tasks such as:  ' Create effective reports by KPI &amp; trend based commentary including actions for managers. ' Support Global and Local Portfolio Management in CAPEX allocation as well as fund approval and reporting of actual consumptions. ' Perform international Business/IT reviews including Consumption and technology KPIs, active Cost Management for FTE-based services as well as IT-Project Budget consumption for Business IT Regional. ' Create efficiency potentials and attractive value propositions based on benchmarks, business specific consumption KPIs and market proven technology KPIs."),
                new LabeledExample("en", "'High NPS from E.ON´s business excecutives for high transparency of IT cost and IT processes.' Supporting in special tasks set by CIO or business unit CFOs.   The role is located in Malmö.  For this position, we’re looking for a person that’s is business focused, has a high developed team spirit and very good social competencies. You have the ability to work under pressure, willingness for continuous learning and the ability to collaborate actively across cultural and organizational boundaries. Our team work close together with the software and product development to ensure that Axis product portfolio is up-to-date and meet our customers’ requirements."),
                new LabeledExample("en", "We offer a pleasant workplace with opportunities to develop.  A suitable candidate needs:  ' Academic degree in Economics ' Profound experience in finance and controlling area ' Experience in drafting decision proposals on executive level ' Experience in driving financial review meetings on management level ' Excellent language skill in English and Swedish(verbal and written)   In order to be successful in the role, it’s important that you identify with the values of E.ON:  ”We put our customers first, we work together, we improve and innovate, we win together and act responsible with an open mind”.   The selection process will take place continuously, so please send in your application in English as soon as possible.   If you have questions related to the role, please contact the recruiting manager. If you have questions related to the unions, please contact. Apply today and join us on our journey towards a sustainable society!"),
                new LabeledExample("en", "About Axis Communications Axis offers intelligent security solutions that enable a smarter, safer world. As the market leader in network video, Axis is driving the industry by continually launching innovative network products based on an open platform - delivering high value to customers through a global partner network. Axis has long-term relationships with partners and provides them with knowledge and ground-breaking network products in existing and new markets.Axis has more than 2,700 dedicated employees in more than 50 countries around the world, supported by a global network of over 80,000 partners. For more information about Axis, please visit our website.Reference number: 2182OUR TEAMWe are a team within Axis Research &amp; Development organization, overall   responsible for Axis Common Firmware PLatform and upgrading products with new firmware."),
                new LabeledExample("en", "WHAT WILL YOU DO? You will be a part of a team working with our common firmware at a system level. The team’s main responsibilities are to keep track of the firmware platform system resources and handle system wide activities such as open source upgrades.WHO ARE WE LOOKING FOR?We believe you are a curious person with a holistic and open source mindset. It is important that you have a genuine interest in software and tools at a system level.Suitable background for the position:Master’s/Bachelor’s degree in engineering, system science or similarExperience of general software developmentExperience of Linux environmentExperience of Embedded systemExperience of programing languages such as C and PythonExperience of Agile methods such as Scrum &amp; KanbanKnowledge of Docker, Jenkins, git, Elasticsearch and Kibana is an advantage.WHAT CAN WE DO FOR YOU?"),
                new LabeledExample("en", "Working with the Platform Management team will give you possibility to influence Axis firmware at a system level. You will have the opportunity to get broad knowledge of Axis’ products &amp; solutions, working closely with different parts of the company. Axis is an organization that values creativity and promotes teamwork and openness. This is a great opportunity to use and develop your skills as part of an exciting, successful organization that is already the world leader in network video. In exchange for your dedication Axis can offer you an innovative and global environment where you can develop both as a professional and as an individual.READY TO ACT?Axis is a company realizing the benefits of a diverse workforce. We know that diversity in groups creates a better working environment and promotes creativity, something that is fundamental for our success. Would you like to grow with us? Find out more from our recruiting manager."),

                new LabeledExample("sv", "Conic Restaurants AB äger och driver idag SUBWAY restauranger i Göteborg, Uddevalla, Skövde, Halmstad, Helsingborg, Malmö, Lund, Kristianstad och Kalmar. Subway är en av världens största restaurangkedjor med över 45 000 restauranger i över 100 länder och ett självklart val för alla som vill ha ett nyttigare och fräschare snabbmatsalternativ.Conic äger också varumärket “Togogo” och driver Togogos första restaurang på Centralstationen i Göteborg. Vi är ett expansivt företag och vår målsättning är att under de kommande åren öppna eller förvärva fler restauranger och att utveckla vår verksamhet. Vår expansion bygger på att vi lyckas attrahera de allra bästa inom vår bransch.Vi vill vara en modern arbetsgivare där medarbetare trivs på jobbet och får en bra balans mellan arbete och fritid. Vi behåller duktiga medarbetare genom att erbjuda intressanta utvecklingsmöjligheter.Vi behöver nu förstärka vårt team och söker dig som är glad, flexibel och som får både kunder och kollegor att trivas."),
                new LabeledExample("sv", "Du ska vara noggrann, van vid att ta eget ansvar och gilla att arbeta i ett högt tempo. Dina arbetsuppgifter kommer att bestå av försäljning och beredning av våra produkter samt att hålla rent och snyggt i restaurangen. Vi jobbar ständigt med att hålla hög produktkvalitet, god hygien och genuin kundservice. Nyckeln till detta tror vi är teamkänsla och söker därför dig som gillar att vara en del av en grupp.Erfarenhet från restaurang, café, livsmedel, kassa eller liknande är meriterande men inte ett krav. Som nyanställd hos oss på Subway kommer du under vår introduktion att utbildas i alla olika delar och moment du förväntas behärska. Vi lägger därför stor vikt vid vilja och iniativförmåga. Vi söker dig som inspirerar dina kollegor, är ambitiös och vill visa vad du går för.Hos oss avgör alltid dina ambitioner hur långt du vill utvecklas. För rätt person finns det stora chanser till att inom vår organisation utvecklas och få mer ansvar."),
                new LabeledExample("sv", "Vi söker nu dig som vill arbeta deltid och då våra restauranger har öppet både dag, kväll och helg söker vi dig som kan arbeta alla tider på dygnet. Vi tillämpar alltid en provanställning på sex månader och följer kollektivavtal. Tjänsten är på deltid 6 timmar per vecka.Vi kommer tillsätta tjänsterna så snart som möjligt och kommer löpande hålla intervjuer, så vänta inte med din ansökan!"),
                new LabeledExample("sv", "I Malmö stad arbetar vi med respekt, engagemang och kreativitet för att utveckla Malmö. Vi har Sveriges viktigaste jobb. Hos Malmö stad finns framtidens arbetsplats. Här gör vi skillnad. Vi skapar en ekologisk, ekonomisk och socialt hållbar stad. Vill du vara med och påverka? När du arbetar inom hemtjänst Dammfri och Fågelbacken cyklar du till våra brukare i den lugna innerstaden med närheten till Malmös vackra parker, restauranger och butiker. Du kommer att få arbeta med kollegor som ställer upp för dig, bemöter dig med respekt och med ett närvarande och tillgängligt ledarskap. Vi ser fram emot att ha dig i vårt team på Dammfri eller Fågelbacken! ARBETSUPPGIFTERDu kommer att tillhöra område Dammfri eller Fågelbacken. Tillsammans med dina kollegor i servicegruppen kommer du att ansvara för utförandet av beviljade bistånd städ och inköp."),
                new LabeledExample("sv", "KVALIFIKATIONERVi söker dig som har ett brinnande intresse av att underlätta vardagen för Malmöbon. Du är en person som;•är ansvarstagande•har lätt för att samarbeta•är initiativtagande•är kommunikativ, gärna med erfarenhet av dokumentation samt rapportering•är lugn och kan inge trygghet till våra brukare•har cykelvanaDet är meriterande om du har erfarenhet av städning. I ditt personliga brev vill vi att du tydligt beskriver vad det är som lockar dig att söka just denna tjänst. ÖVRIGTMalmö stad strävar efter att medarbetarna ska avspegla den mångfald som finns bland befolkningen. Vi välkomnar därför sökande som kan bidra till att vi som arbetsgivare kan tillgodose Malmöbornas behov.För att skicka in din ansökan, klicka på ansökningslänken i annonsen. Frågor om hur du ansöker och lägger in ditt CV ställer du direkt till Visma Recruit 0771- 10 10 29. Frågor om specifika jobb besvaras av den rekryterare eller kontaktperson som anges i annonsen."),
                new LabeledExample("sv", "Vill du arbeta på ett av Sveriges största byggbolag? Då är detta jobbet för dig!Om kundföretagetTill vår kund inom byggbranschen söker vi nu flitiga elmontörer. Kunden förser företag runt om i Sverige med rätt utrustning genom hela byggprojektet. De erbjuder hjälp med allt från etablering till färdigställande av projekt samt att skapa effektiva, säkra och gröna arbetsplatser.Dina arbetsuppgifterDin uppgift är att se till att elförsörjningen på olika byggarbetsplatser fungerar. Tjänsten innefattar arbete som exempelvis att bygga och serva elcentraler, undercentraler och fördelningscentraler till byggarbetsplatser. Arbetet är förlagt mestadels inomhus men även utomhusinstallationer kan förekomma.Din profilVi söker dig med avslutad gymnasieutbildning från elprogrammet, yrkesutbildning eller motsvarande utbildning. Meriterande om du arbetat som elektriker tidigare. Som person uppskattar du att arbeta i ett högt tempo och för att uppfylla kravprofilen för tjänsten är viktigt att du har ett högt säkerhetstänk."),
                new LabeledExample("sv", "Om vårt företagOnePartnerGroup är en svenskägd koncern. Vare sig du behöver hjälp med rekrytering, bemanning, funktionslösningar, omställning, utbildning eller HR kan du som kund hos oss räkna med att vi gör allt för att vara snabba och lätta att arbeta med, utan att tumma på vår professionalitet. Vi är helt sonika en komplett leverantör av kompetens inom både yrkesarbetar- och tjänstemannasektorn.Tillsammans med våra 2500 medarbetare är vi verksamma på ett 40-tal orter i Sverige. Med hjälp av vår stora lokala kännedom, erfarenhet och vårt engagemang strävar vi alltid efter att vara den lokalaste samarbetspartnern.Besök oss gärna på <a href='http://www.onepartnergroup.se' target='_blank' >www.onepartnergroup.se</a>"),
                new LabeledExample("sv", "Söker du ett spännande jobb inom gaming-världen? Vill du jobba centralt på ett coolt företag som är lite crazy och nytänkande? Fortsätt då läsa, vi har jobbet för dig!Dina arbetsuppgifterSom frontend-utvecklare hos vår kund kommer du framför allt att jobba med kodning. Du jobbar projektbaserat och i team för det aktuella projektet. Hos vår kund får du möjlighet att arbeta både med befintliga spel samt nyutveckling och lansering av spel.  Här får du möjlighet att jobba med erfarna och mycket komptetenta programmerare i ett företag som växer, det finns med andra ord stora möjligheter för att utvecklas! De språk du kommer att arbeta i är JavaScript, CSS, HTML5, Redux och React. Om företagetVår kund arbetar med spelutveckling av casinospel och gamblingspel. Genom att kombinera funktioner inom sina spel, kan du vinna pengar. Spelen riktar sig till de personer som vill tjäna pengar på att spela spel på mobilen. Bolaget grundades 2014 och växer snabbt."),
                new LabeledExample("sv", "I dagsläget är de ca 30 personer i Malmö där produktionen sköts, både förbättring av buggar och nyskapande. Resten av företaget finns på Malta. Företaget är väldigt måna om sin personal och dess välmående. Varje år skickas man på seminarier, kurser, kick-offer och de har återkommande AW:s. Frukost och energidryck serveras dagligen. Här jobba personer som är drivna, vill ha kul tillsammans och göra ett så  bra jobb sm möjligt.Din profilFör att trivas hos vår kund ska du vara en lagspelare. Du ska tycka om att ha ett utbyte av kunskap med dina kollegor, och det är inte fel om du gillar att stanna en stund och umgås efter jobb under lättsamma former.Skallkrav: 3 års erfarenhet inom kodning, behöver inte ha utbildning men kunskap! Språken; JavaScript, CSS, Redux, React och HTML5EngelskaMeriterande: 'Erfarenhet av iGamingJobbat i team tidigareOm ossFramtiden AB är ett rekryterings- och bemanningsföretag och vår specialitet är Unga Talanger."),
                new LabeledExample("sv", "Det innebär att vi hjälper våra kunder att hitta de bästa medarbetarna som fortfarande är i början av sin karriär. Just nu har vi hundratals lediga jobb. Framtiden AB arbetar med både bemanning och rekrytering och vår idé är att minska avståndet mellan utbildningen och arbetslivet. Vi gör vad vi heter – hjälper våra kunder till en bättre framtid genom den personal som vi förmedlar. För denna tjänst kommer du vara anställd av oss på Framtiden AB och arbeta som konsult hos ett av våra kundföretag.VillkorStart: omgående Arbetstider: Kontorstid med flexVi behandlar ansökningar löpande, skicka in din redan idag!"),

                new LabeledExample("dk", "Har du lyst til et nært samarbejde med kolleger i en klinik, der sætter patienten i centrum? Hos Vitanova får du rig mulighed for at udvikle dig, når du i en ledende funktion arbejder ud fra to af vores kerneværdier: støtte og nærvær. Som lægesekretær hos Vitanova bliver du en vigtig del af sekretariatet, når du er med til at opdatere og forny vores processer i samarbejde med den øvrige ledelse, ligesom du også har ansvar for at oplære personale, fortæller personalechef og jordemoder Rie Koldorf Jørgensen. Vi arbejder tværfagligt sammen med fokus på individuel behandling af vores patienter. Derfor vil du møde både glade patienter, men også nogle der trænger til trøstende ord. Som frontmedarbejder har du daglig patientkontakt både på mail og telefon, ligesom du også tager del i praktiske opgaver for vores behandlerteam, når de trænger til en hjælpende hånd."),
                new LabeledExample("dk", "Lægesekretær/SOSU-assistent 20-25 timer ugentligt til almen lægepraksis, Amager Speciallæger i Almen Medicin Boiesen, Binderup, Møller og Svendsen 4-lægepraksis med ca. 6500 patienter søger stabil, fleksibel og serviceminded sekretær. Du skal være indstillet på en travl og udfordrende hverdag og være vant til at have flere bolde i luften ad gangen. Dit job vil bestå i patientkontakt telefonisk og ved personlig henvendelse samt forefaldende sekretariatsopgaver. Du vil komme til at samarbejde tæt med 1 sekretær, 2 sygeplejersker og 4 læger. Erfaring fra almen praksis er en klar fordel. Vi kan tilbyde søde patienter, velordnede arbejdsforhold og gode samarbejdsrelationer."),
                new LabeledExample("dk", "MENY søger flere MADarbejdere – er du vores nye delikatesseassistent? MENY Nærum er byens og hele Danmarks førende fødevaremarked, og vi søger en passioneret og engageret delikatesseassistent. Så ka' du flække en hummer og skære hovedet af en grøn asparges? Er du kreativ, elsker du at arbejde med mad og synes MENYs delikatessekoncept er spændende? Ja, så er jobbet måske lige dig. Meny Du skal blandt andet: Være en del af den daglige produktion og sikre en fantastisk kundebetjening Sikre at vores stærke koncepter og faste rutiner overholdes Være med til at udvikle nye retter og måltidsløsninger Du får et spændende og varieret job i en dynamisk virksomhed, hvor du kan være med til at præge din egen arbejdssituation og udvikling."),
                new LabeledExample("dk", "Gram Equipment A/S, Kolding Vil du være med til at udvikle og definere Gram Equipments fremtidige ERP-løsning? Har du en god projekt- og procesforståelse? Brænder du for at analysere og optimere arbejdsgange? Vil du være en del af en global virksomhed, der udvikler og sælger verdens mest innovative og fascinerende udstyr til industriel isproduktion? Gram Equipment Med reference til vores Head of IT søger vi en ERP-konsulent, der skal være med til at sikre en stabil drift af den nuværende NAV-løsning samt udvikle og definere Gram Equipments fremtidige forretningsløsning i et nyt ERP-system. Dine arbejdsopgaver vil bl.a. omfatte at: Konfigure MS Dynamics Nav i relation til ændringer i forretningen Tilsikre, at NAV-brugerflader samt andre applikationer modsvarer forretningens behov Fungere som projektleder på udvalgte IT-projekter"),
                new LabeledExample("dk", "Solrød Bibliotek søger en studerende til udlånsvagt og formidling af materialer og kulturaktiviteter Solrød Kommune Opgaverne består primært af biblioteksopgaver i udlånet og håndtering af forespørgsler fra borgere, som vi servicerer gennem nærvær, smil og litterært engagement. Arbejdet kræver, at du kan opsøge og gå i dialog med vores brugere og appellere til deres nysgerrighed og pirre deres læselyst og vidensbegær. I forbindelse med arbejdet, vil der også være forskellige praktiske opgaver i biblioteksrummet, så det altid fremstår ordentligt og indbydende. Vi kan tilbyde en arbejdsplads med mulighed for personlig og faglig udvikling, udfordringer og selvstændighed en spændende, lærerig og sjov arbejdsdag i et godt kollegialt miljø engagerede og kreative kollegaer, der arbejder tæt sammen på tværs af fagligheder"),
                new LabeledExample("dk", "Salgsassistent Fendt, Glostrup Vi søger en salgsassistent til at varetage vores ordreproces for Fendt i Danmark – primært ordrebehandling og salgssupport. Jobbet indbefatter daglig kontakt til vores sælgere og forhandlere i Danmark samt jævnlig kontakt med vores fabrikker i Tyskland og Italien. Opgaver: Fakturering/kreditering Opfølgning og vedligeholdelse af ordrer Opdatering i vores ordresystemer Vi tilbyder: Et spændende og udfordrende job Fast placering hos AGCO Danmark A/S i Glostrup Gode fremtidige muligheder inden for AGCO-koncernen"),
                new LabeledExample("dk", "Lead Buyer VVS Bravida Danmark A/S, Brøndby Vil du bistå Bravidas afdelinger med at skabe synergi og besparelser på indkøb af VVS-komponenter? Brænder du for at vise, hvad en god indkøbsstrategi kan gøre for virksomheden? Du vil som lead buyer for Vvs-komponenter arbejde på tværs af organisationen i Danmark. Din fornemmeste opgave bliver at støtte vores driftsafdelinger med at skabe synergi og besparelser inden for projektindkøb. Dine primære opgaver bliver at: Gøre en proaktiv indsats med at bundle volumener på tværs af afdelinger i Bravida, og herefter udbyde disse til leverandører både nationalt og internationalt Udfordre vores afdelinger til at tænke i nye løsninger inden for produkt- og leverandørvalg Sikre kvalitet i leverancer"),
                new LabeledExample("dk", "Freight Forwarding Assistent Blue Water Shipping A/S, Aalborg Hos Blue Water Shipping A/S tror vi på at kundefokus, teamwork og købmandskab er nogle af de vigtigste elementer til succes. Til vores speditionsafdeling i Aalborg søger vi nu en serviceminded kollega, der brænder for struktur og detaljer. Blue Water Shipping A/S Du vil blive en del af at team med 4 kolleger hvor høj faglighed, en uformel teamkultur og et stærkt sammenhold er de vigtigste drivkræfter for at sikre fælles succes. Vores nye kollega vil håndtere administrative opgaver, såsom: Oprette og behandle indkomne bookinger Specialistrolle i bookingsystemet Afregning og fakturering"),
                new LabeledExample("dk", "Montør Hoyer A/S, Hadsten Har du lyst til at indgå i et montageteam af velkvalificerede medarbejdere hos Hoyer, hvor der ofte er travlt, og opgaverne varierer? Så er dette en oplagt mulighed for dig. Vi er på jagt efter en ny topmotiveret montør, der ønsker at gøre en forskel for både kunder og kollegaer. Du er ikke bange for at tage fat og trives i en uformel hverdag med en direkte omgangstone. Af montageopgaver kan bl.a. nævnes udskiftning af flanger og lejer, isætning af PTC, varmebånd og klixon samt andre kundespecifikke montageopgaver. Erfaring i arbejde med elektromotorer er et klart plus, men er ikke et must, da oplæring vil finde sted."),
                new LabeledExample("dk", "Business Development Manager CCCCCCC ApS, Frederiksberg ccccccc er et kreativt studio, som specialiserer sig i motion design og animation. Fra vores kontor på Frederiksberg skaber vi visuelle fortællinger for en bred palet af kunder fra hele verden. Vi insisterer på, at både indhold og visuelt udtryk skal leve op til de højeste standarder inden for feltet. Vi søger en forretningsudvikler, der tænker formål over profit - og værdiskabelse over kold kanvas. Vi søger dig, som er optaget af at gøre visioner til virkelighed. Det betyder, at du er strategisk stærk, og at du gennem salg af højkvalitets motion design og animation kan bidrage til at føre ccccccc i nye og spændende retninger. Jobets opgaver vil bl.a. bestå af Oversætte virksomhedens visioner til konkrete markedsføringsstrategier og handleplaner. Kortlægning og udvikling af value propositions. Flytte mindset fra lokalt til globalt.")

            };


            return labeledExamples;

        }
        public static List<TokenizedExample> CreateThirtyTokenizedExamples()
        {

            List<LabeledExample> labeledExamples = CreateThirtyLabeledExamples();
            List<TokenizedExample> tokenizedExamples = new LabeledExampleManager().CreateOrDefault(labeledExamples);

            return tokenizedExamples;

        }
        public static List<SimilarityIndex> CreateThirtySimilarityIndexes()
        {

            List<SimilarityIndex> similarityIndexes = new List<SimilarityIndex>()
            {

                new SimilarityIndex(text: CreateThirtyTokenizedExamples()[0].LabeledExample.Text, label: "en", value: 1),
                new SimilarityIndex(text: CreateThirtyTokenizedExamples()[1].LabeledExample.Text, label: "en", value: 0.031696),
                new SimilarityIndex(text: CreateThirtyTokenizedExamples()[2].LabeledExample.Text, label: "en", value: 0.017512),
                new SimilarityIndex(text: CreateThirtyTokenizedExamples()[3].LabeledExample.Text, label: "en", value: 0.022472),
                new SimilarityIndex(text: CreateThirtyTokenizedExamples()[4].LabeledExample.Text, label: "en", value: 0.017927),
                new SimilarityIndex(text: CreateThirtyTokenizedExamples()[5].LabeledExample.Text, label: "en", value: 0.018717),
                new SimilarityIndex(text: CreateThirtyTokenizedExamples()[6].LabeledExample.Text, label: "en", value: 0.014844),
                new SimilarityIndex(text: CreateThirtyTokenizedExamples()[7].LabeledExample.Text, label: "en", value: 0.017185),
                new SimilarityIndex(text: CreateThirtyTokenizedExamples()[8].LabeledExample.Text, label: "en", value: 0.024096),
                new SimilarityIndex(text: CreateThirtyTokenizedExamples()[9].LabeledExample.Text, label: "en", value: 0.020548),

                new SimilarityIndex(text: CreateThirtyTokenizedExamples()[10].LabeledExample.Text, label: "sv", value: 0.000741),
                new SimilarityIndex(text: CreateThirtyTokenizedExamples()[11].LabeledExample.Text, label: "sv", value: 0),
                new SimilarityIndex(text: CreateThirtyTokenizedExamples()[12].LabeledExample.Text, label: "sv", value: 0),
                new SimilarityIndex(text: CreateThirtyTokenizedExamples()[13].LabeledExample.Text, label: "sv", value: 0.000802),
                new SimilarityIndex(text: CreateThirtyTokenizedExamples()[14].LabeledExample.Text, label: "sv", value: 0.000745),
                new SimilarityIndex(text: CreateThirtyTokenizedExamples()[15].LabeledExample.Text, label: "sv", value: 0),
                new SimilarityIndex(text: CreateThirtyTokenizedExamples()[16].LabeledExample.Text, label: "sv", value: 0.000861),
                new SimilarityIndex(text: CreateThirtyTokenizedExamples()[17].LabeledExample.Text, label: "sv", value: 0.00074),
                new SimilarityIndex(text: CreateThirtyTokenizedExamples()[18].LabeledExample.Text, label: "sv", value: 0.000733),
                new SimilarityIndex(text: CreateThirtyTokenizedExamples()[19].LabeledExample.Text, label: "sv", value: 0.000905),

                new SimilarityIndex(text: CreateThirtyTokenizedExamples()[20].LabeledExample.Text, label: "dk", value: 0.002333),
                new SimilarityIndex(text: CreateThirtyTokenizedExamples()[21].LabeledExample.Text, label: "dk", value: 0.000895),
                new SimilarityIndex(text: CreateThirtyTokenizedExamples()[22].LabeledExample.Text, label: "dk", value: 0.000843),
                new SimilarityIndex(text: CreateThirtyTokenizedExamples()[23].LabeledExample.Text, label: "dk", value: 0.004918),
                new SimilarityIndex(text: CreateThirtyTokenizedExamples()[24].LabeledExample.Text, label: "dk", value: 0.001668),
                new SimilarityIndex(text: CreateThirtyTokenizedExamples()[25].LabeledExample.Text, label: "dk", value: 0.003027),
                new SimilarityIndex(text: CreateThirtyTokenizedExamples()[26].LabeledExample.Text, label: "dk", value: 0.002618),
                new SimilarityIndex(text: CreateThirtyTokenizedExamples()[27].LabeledExample.Text, label: "dk", value: 0.00367),
                new SimilarityIndex(text: CreateThirtyTokenizedExamples()[28].LabeledExample.Text, label: "dk", value: 0.002575),
                new SimilarityIndex(text: CreateThirtyTokenizedExamples()[29].LabeledExample.Text, label: "dk", value: 0.003934)

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
    Last Update: 24.09.2022
*/