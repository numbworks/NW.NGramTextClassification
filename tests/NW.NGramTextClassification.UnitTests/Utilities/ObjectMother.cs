using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NW.NGramTextClassification.LabeledExamples;
using NW.NGramTextClassification.NGrams;
using NW.NGramTextClassification.NGramTokenization;
using NW.NGramTextClassification.Similarity;

namespace NW.NGramTextClassification.UnitTests
{
    internal static class ObjectMother
    {

        #region Shared

        internal static ITokenizationStrategy Shared_TokenizationStrategyDefault = new TokenizationStrategy();
        internal static TokenizationStrategy Shared_TokenizationStrategyCustom = new TokenizationStrategy("[a-Z]", ";", false);

        internal static ulong Shared_Text1_LabeledExampleId = 1;
        internal static string Shared_Text1_Text = "We are looking for several skilled and driven developers to join our team.";
        internal static string Shared_Text1_Label = "en";
        internal static List<Monogram> Shared_Text1_TextAsMonograms = new List<Monogram>()
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
        internal static List<Bigram> Shared_Text1_TextAsBigrams = new List<Bigram>()
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
        internal static List<Trigram> Shared_Text1_TextAsTrigrams = new List<Trigram>()
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
        internal static List<Fourgram> Shared_Text1_TextAsFourgrams = new List<Fourgram>()
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
        internal static List<Fivegram> Shared_Text1_TextAsFivegrams = new List<Fivegram>()
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
        internal static List<INGram> Shared_Text1_TextAsNGrams = new List<INGram>() 
        {

            Shared_Text1_TextAsMonograms[0],
            Shared_Text1_TextAsMonograms[1],
            Shared_Text1_TextAsMonograms[2],
            Shared_Text1_TextAsMonograms[3],
            Shared_Text1_TextAsMonograms[4],
            Shared_Text1_TextAsMonograms[5],
            Shared_Text1_TextAsMonograms[6],
            Shared_Text1_TextAsMonograms[7],
            Shared_Text1_TextAsMonograms[8],
            Shared_Text1_TextAsMonograms[9],
            Shared_Text1_TextAsMonograms[10],
            Shared_Text1_TextAsMonograms[11],
            Shared_Text1_TextAsMonograms[12],

            Shared_Text1_TextAsBigrams[0],
            Shared_Text1_TextAsBigrams[1],
            Shared_Text1_TextAsBigrams[2],
            Shared_Text1_TextAsBigrams[3],
            Shared_Text1_TextAsBigrams[4],
            Shared_Text1_TextAsBigrams[5],
            Shared_Text1_TextAsBigrams[6],
            Shared_Text1_TextAsBigrams[7],
            Shared_Text1_TextAsBigrams[8],
            Shared_Text1_TextAsBigrams[9],
            Shared_Text1_TextAsBigrams[10],
            Shared_Text1_TextAsBigrams[11],
            Shared_Text1_TextAsBigrams[12],

            Shared_Text1_TextAsTrigrams[0],
            Shared_Text1_TextAsTrigrams[1],
            Shared_Text1_TextAsTrigrams[2],
            Shared_Text1_TextAsTrigrams[3],
            Shared_Text1_TextAsTrigrams[4],
            Shared_Text1_TextAsTrigrams[5],
            Shared_Text1_TextAsTrigrams[6],
            Shared_Text1_TextAsTrigrams[7],
            Shared_Text1_TextAsTrigrams[8],
            Shared_Text1_TextAsTrigrams[9],
            Shared_Text1_TextAsTrigrams[10],
            Shared_Text1_TextAsTrigrams[11],
            Shared_Text1_TextAsTrigrams[12],

            Shared_Text1_TextAsFourgrams[0],
            Shared_Text1_TextAsFourgrams[1],
            Shared_Text1_TextAsFourgrams[2],
            Shared_Text1_TextAsFourgrams[3],
            Shared_Text1_TextAsFourgrams[4],
            Shared_Text1_TextAsFourgrams[5],
            Shared_Text1_TextAsFourgrams[6],
            Shared_Text1_TextAsFourgrams[7],
            Shared_Text1_TextAsFourgrams[8],
            Shared_Text1_TextAsFourgrams[9],
            Shared_Text1_TextAsFourgrams[10],
            Shared_Text1_TextAsFourgrams[11],
            Shared_Text1_TextAsFourgrams[12],

            Shared_Text1_TextAsFivegrams[0],
            Shared_Text1_TextAsFivegrams[1],
            Shared_Text1_TextAsFivegrams[2],
            Shared_Text1_TextAsFivegrams[3],
            Shared_Text1_TextAsFivegrams[4],
            Shared_Text1_TextAsFivegrams[5],
            Shared_Text1_TextAsFivegrams[6],
            Shared_Text1_TextAsFivegrams[7],
            Shared_Text1_TextAsFivegrams[8],
            Shared_Text1_TextAsFivegrams[9],
            Shared_Text1_TextAsFivegrams[10],
            Shared_Text1_TextAsFivegrams[11],
            Shared_Text1_TextAsFivegrams[12]

        };

        internal static ulong Shared_Text2_LabeledExampleId = 2;
        internal static string Shared_Text2_Text = "Vår kund erbjuder trivsel, arbetsglädje och en trygg arbetsmiljö.";
        internal static string Shared_Text2_Label = "sv";
        internal static List<Monogram> Shared_Text2_TextAsMonograms = new List<Monogram>()
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
        internal static List<Bigram> Shared_Text2_TextAsBigrams = new List<Bigram>()
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
        internal static List<Trigram> Shared_Text2_TextAsTrigrams = new List<Trigram>()
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
        internal static List<Fourgram> Shared_Text2_TextAsFourgrams = new List<Fourgram>()
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
        internal static List<Fivegram> Shared_Text2_TextAsFivegrams = new List<Fivegram>()
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
        internal static List<INGram> Shared_Text2_TextAsNGrams = new List<INGram>()
        {

            Shared_Text2_TextAsMonograms[0],
            Shared_Text2_TextAsMonograms[1],
            Shared_Text2_TextAsMonograms[2],
            Shared_Text2_TextAsMonograms[3],
            Shared_Text2_TextAsMonograms[4],
            Shared_Text2_TextAsMonograms[5],
            Shared_Text2_TextAsMonograms[6],
            Shared_Text2_TextAsMonograms[7],
            Shared_Text2_TextAsMonograms[8],

            Shared_Text2_TextAsBigrams[0],
            Shared_Text2_TextAsBigrams[1],
            Shared_Text2_TextAsBigrams[2],
            Shared_Text2_TextAsBigrams[3],
            Shared_Text2_TextAsBigrams[4],
            Shared_Text2_TextAsBigrams[5],
            Shared_Text2_TextAsBigrams[6],
            Shared_Text2_TextAsBigrams[7],
            Shared_Text2_TextAsBigrams[8],

            Shared_Text2_TextAsTrigrams[0],
            Shared_Text2_TextAsTrigrams[1],
            Shared_Text2_TextAsTrigrams[2],
            Shared_Text2_TextAsTrigrams[3],
            Shared_Text2_TextAsTrigrams[4],
            Shared_Text2_TextAsTrigrams[5],
            Shared_Text2_TextAsTrigrams[6],
            Shared_Text2_TextAsTrigrams[7],
            Shared_Text2_TextAsTrigrams[8],

            Shared_Text2_TextAsFourgrams[0],
            Shared_Text2_TextAsFourgrams[1],
            Shared_Text2_TextAsFourgrams[2],
            Shared_Text2_TextAsFourgrams[3],
            Shared_Text2_TextAsFourgrams[4],
            Shared_Text2_TextAsFourgrams[5],
            Shared_Text2_TextAsFourgrams[6],
            Shared_Text2_TextAsFourgrams[7],
            Shared_Text2_TextAsFourgrams[8],

            Shared_Text2_TextAsFivegrams[0],
            Shared_Text2_TextAsFivegrams[1],
            Shared_Text2_TextAsFivegrams[2],
            Shared_Text2_TextAsFivegrams[3],
            Shared_Text2_TextAsFivegrams[4],
            Shared_Text2_TextAsFivegrams[5],
            Shared_Text2_TextAsFivegrams[6],
            Shared_Text2_TextAsFivegrams[7],
            Shared_Text2_TextAsFivegrams[8]

        };

        internal static string Shared_Text3_Text = "Kas siis selle maa keel Laulutuules ei või Taevani tõustes üles Igavikku omale otsida";
        internal static string Shared_Text3_Label = null;
        internal static List<Monogram> Shared_Text3_TextAsMonograms
            = new List<Monogram>() {

                new Monogram(Shared_TokenizationStrategyDefault, "kas"),
                new Monogram(Shared_TokenizationStrategyDefault, "siis"),
                new Monogram(Shared_TokenizationStrategyDefault, "selle"),
                new Monogram(Shared_TokenizationStrategyDefault, "maa"),
                new Monogram(Shared_TokenizationStrategyDefault, "keel"),
                new Monogram(Shared_TokenizationStrategyDefault, "laulutuules"),
                new Monogram(Shared_TokenizationStrategyDefault, "ei"),
                new Monogram(Shared_TokenizationStrategyDefault, "või"),
                new Monogram(Shared_TokenizationStrategyDefault, "taevani"),
                new Monogram(Shared_TokenizationStrategyDefault, "tõustes"),
                new Monogram(Shared_TokenizationStrategyDefault, "üles"),
                new Monogram(Shared_TokenizationStrategyDefault, "igavikku"),
                new Monogram(Shared_TokenizationStrategyDefault, "omale"),
                new Monogram(Shared_TokenizationStrategyDefault, "otsida")

            };
        internal static List<Bigram> Shared_Text3_TextAsBigrams
            = new List<Bigram>() {

                new Bigram(Shared_TokenizationStrategyDefault, "kas siis"),
                new Bigram(Shared_TokenizationStrategyDefault, "siis selle"),
                new Bigram(Shared_TokenizationStrategyDefault, "selle maa"),
                new Bigram(Shared_TokenizationStrategyDefault, "maa keel"),
                new Bigram(Shared_TokenizationStrategyDefault, "keel laulutuules"),
                new Bigram(Shared_TokenizationStrategyDefault, "laulutuules ei"),
                new Bigram(Shared_TokenizationStrategyDefault, "ei või"),
                new Bigram(Shared_TokenizationStrategyDefault, "või taevani"),
                new Bigram(Shared_TokenizationStrategyDefault, "taevani tõustes"),
                new Bigram(Shared_TokenizationStrategyDefault, "tõustes üles"),
                new Bigram(Shared_TokenizationStrategyDefault, "üles igavikku"),
                new Bigram(Shared_TokenizationStrategyDefault, "igavikku omale"),
                new Bigram(Shared_TokenizationStrategyDefault, "omale otsida"),
                new Bigram(Shared_TokenizationStrategyDefault, "otsida")

            };
        internal static List<Trigram> Shared_Text3_TextAsTrigrams
            = new List<Trigram>() {

                new Trigram(Shared_TokenizationStrategyDefault, "kas siis selle"),
                new Trigram(Shared_TokenizationStrategyDefault, "siis selle maa"),
                new Trigram(Shared_TokenizationStrategyDefault, "selle maa keel"),
                new Trigram(Shared_TokenizationStrategyDefault, "maa keel laulutuules"),
                new Trigram(Shared_TokenizationStrategyDefault, "keel laulutuules ei"),
                new Trigram(Shared_TokenizationStrategyDefault, "laulutuules ei või"),
                new Trigram(Shared_TokenizationStrategyDefault, "ei või taevani"),
                new Trigram(Shared_TokenizationStrategyDefault, "või taevani tõustes"),
                new Trigram(Shared_TokenizationStrategyDefault, "taevani tõustes üles"),
                new Trigram(Shared_TokenizationStrategyDefault, "tõustes üles igavikku"),
                new Trigram(Shared_TokenizationStrategyDefault, "üles igavikku omale"),
                new Trigram(Shared_TokenizationStrategyDefault, "igavikku omale otsida"),
                new Trigram(Shared_TokenizationStrategyDefault, "omale otsida"),
                new Trigram(Shared_TokenizationStrategyDefault, "otsida")

            };
        internal static List<Fourgram> Shared_Text3_TextAsFourgrams
            = new List<Fourgram>() {

                new Fourgram(Shared_TokenizationStrategyDefault, "kas siis selle maa"),
                new Fourgram(Shared_TokenizationStrategyDefault, "siis selle maa keel"),
                new Fourgram(Shared_TokenizationStrategyDefault, "selle maa keel laulutuules"),
                new Fourgram(Shared_TokenizationStrategyDefault, "maa keel laulutuules ei"),
                new Fourgram(Shared_TokenizationStrategyDefault, "maakeel laulutuules ei või"),
                new Fourgram(Shared_TokenizationStrategyDefault, "maalaulutuules ei või taevani"),
                new Fourgram(Shared_TokenizationStrategyDefault, "maaei või taevani tõustes"),
                new Fourgram(Shared_TokenizationStrategyDefault, "maavõi taevani tõustes üles"),
                new Fourgram(Shared_TokenizationStrategyDefault, "maataevani tõustes üles igavikku"),
                new Fourgram(Shared_TokenizationStrategyDefault, "maatõustes üles igavikku omale"),
                new Fourgram(Shared_TokenizationStrategyDefault, "maaüles igavikku omale otsida"),
                new Fourgram(Shared_TokenizationStrategyDefault, "maaigavikku omale otsida"),
                new Fourgram(Shared_TokenizationStrategyDefault, "maaomale otsida"),
                new Fourgram(Shared_TokenizationStrategyDefault, "maaotsida")

            };
        internal static List<Fivegram> Shared_Text3_TextAsFivegrams
            = new List<Fivegram>()
            {

                new Fivegram(Shared_TokenizationStrategyDefault, "kas siis selle maa keel"),
                new Fivegram(Shared_TokenizationStrategyDefault, "siis selle maa keel laulutuules"),
                new Fivegram(Shared_TokenizationStrategyDefault, "selle maa keel laulutuules ei"),
                new Fivegram(Shared_TokenizationStrategyDefault, "maa keel laulutuules ei või"),
                new Fivegram(Shared_TokenizationStrategyDefault, "keel laulutuules ei või taevani"),
                new Fivegram(Shared_TokenizationStrategyDefault, "laulutuules ei või taevani tõustes"),
                new Fivegram(Shared_TokenizationStrategyDefault, "ei või taevani tõustes üles"),
                new Fivegram(Shared_TokenizationStrategyDefault, "või taevani tõustes üles igavikku"),
                new Fivegram(Shared_TokenizationStrategyDefault, "taevani tõustes üles igavikku omale"),
                new Fivegram(Shared_TokenizationStrategyDefault, "tõustes üles igavikku omale otsida"),
                new Fivegram(Shared_TokenizationStrategyDefault, "üles igavikku omale otsida"),
                new Fivegram(Shared_TokenizationStrategyDefault, "igavikku omale otsida"),
                new Fivegram(Shared_TokenizationStrategyDefault, "omale otsida"),
                new Fivegram(Shared_TokenizationStrategyDefault, "otsida")

            };
        internal static List<INGram> Shared_Text3_TextAsNGrams = new List<INGram>()
        {

            Shared_Text3_TextAsMonograms[0],
            Shared_Text3_TextAsMonograms[1],
            Shared_Text3_TextAsMonograms[2],
            Shared_Text3_TextAsMonograms[3],
            Shared_Text3_TextAsMonograms[4],
            Shared_Text3_TextAsMonograms[5],
            Shared_Text3_TextAsMonograms[6],
            Shared_Text3_TextAsMonograms[7],
            Shared_Text3_TextAsMonograms[8],
            Shared_Text3_TextAsMonograms[9],
            Shared_Text3_TextAsMonograms[10],
            Shared_Text3_TextAsMonograms[11],
            Shared_Text3_TextAsMonograms[12],
            Shared_Text3_TextAsMonograms[13],

            Shared_Text3_TextAsBigrams[0],
            Shared_Text3_TextAsBigrams[1],
            Shared_Text3_TextAsBigrams[2],
            Shared_Text3_TextAsBigrams[3],
            Shared_Text3_TextAsBigrams[4],
            Shared_Text3_TextAsBigrams[5],
            Shared_Text3_TextAsBigrams[6],
            Shared_Text3_TextAsBigrams[7],
            Shared_Text3_TextAsBigrams[8],
            Shared_Text3_TextAsBigrams[9],
            Shared_Text3_TextAsBigrams[10],
            Shared_Text3_TextAsBigrams[11],
            Shared_Text3_TextAsBigrams[12],
            Shared_Text3_TextAsBigrams[13],

            Shared_Text3_TextAsTrigrams[0],
            Shared_Text3_TextAsTrigrams[1],
            Shared_Text3_TextAsTrigrams[2],
            Shared_Text3_TextAsTrigrams[3],
            Shared_Text3_TextAsTrigrams[4],
            Shared_Text3_TextAsTrigrams[5],
            Shared_Text3_TextAsTrigrams[6],
            Shared_Text3_TextAsTrigrams[7],
            Shared_Text3_TextAsTrigrams[8],
            Shared_Text3_TextAsTrigrams[9],
            Shared_Text3_TextAsTrigrams[10],
            Shared_Text3_TextAsTrigrams[11],
            Shared_Text3_TextAsTrigrams[12],
            Shared_Text3_TextAsTrigrams[13],

            Shared_Text3_TextAsFourgrams[0],
            Shared_Text3_TextAsFourgrams[1],
            Shared_Text3_TextAsFourgrams[2],
            Shared_Text3_TextAsFourgrams[3],
            Shared_Text3_TextAsFourgrams[4],
            Shared_Text3_TextAsFourgrams[5],
            Shared_Text3_TextAsFourgrams[6],
            Shared_Text3_TextAsFourgrams[7],
            Shared_Text3_TextAsFourgrams[8],
            Shared_Text3_TextAsFourgrams[9],
            Shared_Text3_TextAsFourgrams[10],
            Shared_Text3_TextAsFourgrams[11],
            Shared_Text3_TextAsFourgrams[12],
            Shared_Text3_TextAsFourgrams[13],

            Shared_Text3_TextAsFivegrams[0],
            Shared_Text3_TextAsFivegrams[1],
            Shared_Text3_TextAsFivegrams[2],
            Shared_Text3_TextAsFivegrams[3],
            Shared_Text3_TextAsFivegrams[4],
            Shared_Text3_TextAsFivegrams[5],
            Shared_Text3_TextAsFivegrams[6],
            Shared_Text3_TextAsFivegrams[7],
            Shared_Text3_TextAsFivegrams[8],
            Shared_Text3_TextAsFivegrams[9],
            Shared_Text3_TextAsFivegrams[10],
            Shared_Text3_TextAsFivegrams[11],
            Shared_Text3_TextAsFivegrams[12],
            Shared_Text3_TextAsFivegrams[13]

        };

        internal static LabeledExample Shared_Text1_LabeledExample
            = new LabeledExample(
                        Shared_Text1_LabeledExampleId,
                        Shared_Text1_Label,
                        Shared_Text1_Text,
                        Shared_Text1_TextAsNGrams);

        internal static LabeledExample Shared_Text2_LabeledExample
            = new LabeledExample(
                        Shared_Text2_LabeledExampleId,
                        Shared_Text2_Label,
                        Shared_Text2_Text,
                        Shared_Text2_TextAsNGrams);

        internal static List<LabeledExample> Shared_LabeledExamples
            = new List<LabeledExample>()
                {
                    Shared_Text1_LabeledExample,
                    Shared_Text2_LabeledExample
                };

        #endregion







        #region LabeledExampleFactory

        internal static uint LabeledExampleFactory_InitialId1 = 0;
        internal static (string label, string text) LabeledExampleFactory_Tuple1 = (Shared_Text1_Label, Shared_Text1_Text);
        internal static (string label, string text) LabeledExampleFactory_Tuple2 = (Shared_Text2_Label, Shared_Text2_Text);
        internal static List<(string label, string text)> LabeledExampleFactory_Tuples
            = new List<(string label, string text)>()
            {
                LabeledExampleFactory_Tuple1,
                LabeledExampleFactory_Tuple2
            };

        #endregion 

        #region NGramTokenizer

        internal static string NGramTokenizer_TextNonAlphanumerical = ";;;-- £/£&$£";

        #endregion

        /* --------------------------------------------------- */

        #region ANGram

        internal static ushort ANGram_FakeGram1_N = 1;
        internal static ushort ANGram_FakeGram2_N = 1;
        internal static FakeGram ANGram_FakeGram1
            = new FakeGram(ANGram_FakeGram1_N, Shared_TokenizationStrategyDefault, Shared_Text1_TextAsMonograms[0].Value);
        internal static FakeGram ANGram_FakeGram2
            = new FakeGram(ANGram_FakeGram2_N, Shared_TokenizationStrategyDefault, Shared_Text1_TextAsMonograms[1].Value);
        internal static int ANGram_FakeGram1_HashCode
            = (ANGram_FakeGram1_N, Shared_TokenizationStrategyDefault, Shared_Text1_TextAsMonograms[0].Value).GetHashCode();

        #endregion

        #region ArrayManager

        internal static string ArrayManager_Delimiter1 = ";";
        internal static uint ArrayManager_StartIndex1 = 0;
        internal static uint ArrayManager_Length1 = 2;
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

        #endregion

        #region LabeledExample

        internal static LabeledExample LabeledExample1
            = new LabeledExample(
                    Shared_Text1_LabeledExampleId,
                    Shared_Text1_Label,
                    Shared_Text1_Text,
                    Shared_Text1_TextAsNGrams
                );
        internal static string LabeledExample1_AsString
            = string.Concat(
                        $"[ Id: '{Shared_Text1_LabeledExampleId}', ",
                        $"Label: '{Shared_Text1_Label}', ",
                        $"Text: '{Shared_Text1_Text}', ",
                        $"TextAsNGrams: '{Shared_Text1_TextAsNGrams.Count}' ]"
                    );

        #endregion

        #region NGramTokenizerRuleSet

        internal static string NGramTokenizerRuleSet_ToString
            = "[ DoForMonograms: 'True', DoForBigrams: 'True', DoForTrigrams: 'True', DoForFourgrams: 'False', DoForFivegrams: 'False' ]";

        #endregion

        #region SimilarityIndex

        internal static ulong SimilarityIndex_Id1 = 1;
        internal static string SimilarityIndex_Label1 = "en";
        internal static double SimilarityIndex_Value1 = 0.23;
        internal static ulong SimilarityIndex_Id2 = 2;
        internal static string SimilarityIndex_Label2 = "sv";
        internal static double SimilarityIndex_Value2 = 0.76;
        internal static SimilarityIndex SimilarityIndex1
            = new SimilarityIndex(SimilarityIndex_Id1, SimilarityIndex_Label1, SimilarityIndex_Value1);
        internal static SimilarityIndex SimilarityIndex2
            = new SimilarityIndex(SimilarityIndex_Id2, SimilarityIndex_Label2, SimilarityIndex_Value2);
        internal static string SimilarityIndex_ToString1
            = $"[ Id: '{SimilarityIndex_Id1}', Label: '{SimilarityIndex_Label1}', Value: '{SimilarityIndex_Value1}' ]";

        #endregion

        #region SimilarityIndexAverage

        internal static string SimilarityIndexAverage_Label1 = "en";
        internal static double SimilarityIndexAverage_Value1 = 0.54;
        internal static SimilarityIndexAverage SimilarityIndexAverage1
            = new SimilarityIndexAverage(SimilarityIndexAverage_Label1, SimilarityIndexAverage_Value1);
        internal static string SimilarityIndexAverage_ToString1
            = $"[ Label: '{SimilarityIndexAverage_Label1}', Value: '{SimilarityIndexAverage_Value1}' ]";

        #endregion

        #region TextClassifier

        internal static SimilarityIndex TextClassifier_Text1_SimilarityIndex1 = new SimilarityIndex(1, "en", 1);
        internal static SimilarityIndex TextClassifier_Text1_SimilarityIndex2 = new SimilarityIndex(2, "sv", 0);
        internal static List<SimilarityIndex> TextClassifier_Text1_SimilarityIndexes
            = new List<SimilarityIndex>()
            {
                TextClassifier_Text1_SimilarityIndex1,
                TextClassifier_Text1_SimilarityIndex2
            };
        internal static SimilarityIndexAverage TextClassifier_Text1_SimilarityIndexAverage1
            = new SimilarityIndexAverage("en", 1);
        internal static SimilarityIndexAverage TextClassifier_Text1_SimilarityIndexAverage2
            = new SimilarityIndexAverage("sv", 0);
        internal static List<SimilarityIndexAverage> TextClassifier_Text1_SimilarityIndexAverages
            = new List<SimilarityIndexAverage>()
            {
                TextClassifier_Text1_SimilarityIndexAverage1,
                TextClassifier_Text1_SimilarityIndexAverage2
            };
        internal static TextClassifierResult TextClassifier_Text1_TextClassifierResult
            = new TextClassifierResult(
                        Shared_Text1_Label,
                        TextClassifier_Text1_SimilarityIndexes,
                        TextClassifier_Text1_SimilarityIndexAverages);
        internal static List<INGram> TextClassifier_Text1_NGrams = Shared_Text1_TextAsNGrams;
        internal static List<string> TextClassifier_Text1_UniqueLabels
            = new List<string>()
            {
                TextClassifier_Text1_SimilarityIndex1.Label,
                TextClassifier_Text1_SimilarityIndex2.Label,
            };

        internal static SimilarityIndex TextClassifier_Text3_SimilarityIndex1 = new SimilarityIndex(1, "en", 0);
        internal static SimilarityIndex TextClassifier_Text3_SimilarityIndex2 = new SimilarityIndex(2, "sv", 0);
        internal static List<SimilarityIndex> TextClassifier_Text3_SimilarityIndexes
            = new List<SimilarityIndex>()
            {
                TextClassifier_Text3_SimilarityIndex1,
                TextClassifier_Text3_SimilarityIndex2
            };
        internal static SimilarityIndexAverage TextClassifier_Text3_SimilarityIndexAverage1
            = new SimilarityIndexAverage("en", 0);
        internal static SimilarityIndexAverage TextClassifier_Text3_SimilarityIndexAverage2
            = new SimilarityIndexAverage("sv", 0);
        internal static List<SimilarityIndexAverage> TextClassifier_Text3_SimilarityIndexAverages
            = new List<SimilarityIndexAverage>()
            {
                TextClassifier_Text3_SimilarityIndexAverage1,
                TextClassifier_Text3_SimilarityIndexAverage2
            };
        internal static TextClassifierResult TextClassifier_Text3_TextClassifierResult
            = new TextClassifierResult(
                        Shared_Text3_Label,
                        TextClassifier_Text3_SimilarityIndexes,
                        TextClassifier_Text3_SimilarityIndexAverages);
        internal static List<string> TextClassifier_Text3_UniqueLabels
            = new List<string>()
            {
                TextClassifier_Text3_SimilarityIndex1.Label,
                TextClassifier_Text3_SimilarityIndex2.Label,
            };

        #endregion

        #region TextClassifierResult

        internal static List<SimilarityIndex> TextClassifierResult_SimilarityIndexes1
            = new List<SimilarityIndex>()
                {
                    SimilarityIndex1,
                    SimilarityIndex2
                };
        internal static List<SimilarityIndexAverage> TextClassifierResult_SimilarityIndexAverages1
            = new List<SimilarityIndexAverage>()
                {
                    SimilarityIndexAverage1
                };
        internal static string TextClassifierResult_ToString1
            = $"[ Label: '{Shared_Text1_Label}', SimilarityIndexes: '{TextClassifierResult_SimilarityIndexes1.Count}', SimilarityIndexAverages: '{TextClassifierResult_SimilarityIndexAverages1.Count}' ]";
        internal static string TextClassifierResult_ToString1WithNullLabel
            = $"[ Label: 'null', SimilarityIndexes: '{TextClassifierResult_SimilarityIndexes1.Count}', SimilarityIndexAverages: '{TextClassifierResult_SimilarityIndexAverages1.Count}' ]";
        internal static string TextClassifierResult_VariableName_Indexes = "indexes";
        internal static string TextClassifierResult_VariableName_IndexAverages = "indexAverages";

        #endregion

        #region TokenizationStrategy

        internal static string TokenizationStrategy_ToString
            = $"[ Pattern: '{TokenizationStrategy.DefaultPattern}', Delimiter: '{TokenizationStrategy.DefaultDelimiter}', ToLowercase: '{TokenizationStrategy.DefaultToLowercase}' ]";
        internal static int TokenizationStrategy_DefaultHashCode
            = (TokenizationStrategy.DefaultPattern, TokenizationStrategy.DefaultDelimiter, TokenizationStrategy.DefaultToLowercase).GetHashCode();

        #endregion

        #region Validator

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

        #endregion

        #region Methods

        internal static bool AreEqual(List<INGram> list1, List<INGram> list2)
        {

            if (list1 == null && list2 == null)
                return true;

            if (list1 == null || list2 == null)
                return false;

            if (list1.Count != list2.Count)
                return false;

            for (int i = 0; i < list1.Count; i++)
                if (list1[i].Equals(list2[i]) == false)
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
        internal static bool AreEqual(List<LabeledExample> list1, List<LabeledExample> list2)
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
        internal static bool AreEqual(SimilarityIndex obj1, SimilarityIndex obj2)
        {

            return (obj1.Id== obj2.Id)
                    && string.Equals(obj1.Label, obj2.Label, StringComparison.InvariantCulture)
                    && (obj1.Value == obj2.Value);

        }
        internal static bool AreEqual(List<SimilarityIndex> list1, List<SimilarityIndex> list2)
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
        internal static bool AreEqual(SimilarityIndexAverage obj1, SimilarityIndexAverage obj2)
        {

            return string.Equals(obj1.Label, obj2.Label, StringComparison.InvariantCulture)
                    && (obj1.Value == obj2.Value);

        }
        internal static bool AreEqual(List<SimilarityIndexAverage> list1, List<SimilarityIndexAverage> list2)
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
        internal static bool AreEqual(TextClassifierResult obj1, TextClassifierResult obj2)
        {

            return string.Equals(obj1.Label, obj2.Label, StringComparison.InvariantCulture)
                    && AreEqual(obj1.SimilarityIndexAverages, obj2.SimilarityIndexAverages)
                    && AreEqual(obj1.SimilarityIndexes, obj2.SimilarityIndexes);

        }
        internal static void Method_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
        {

            // Arrange
            // Act
            // Assert
            Exception actual = Assert.Throws(expectedType, del);
            Assert.AreEqual(expectedMessage, actual.Message);

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 19.09.2021
*/