using NW.NGramTextClassification.NGramTokenization;

namespace NW.NGramTextClassification.UnitTests.NGramTokenization
{
    public static class ObjectMother
    {

        #region Properties

        public static ITokenizationStrategy TokenizationStrategy_Default = new TokenizationStrategy();
        public static string TokenizationStrategy_Default_AsString
            = $"[ Pattern: '{TokenizationStrategy.DefaultPattern}', Delimiter: '{TokenizationStrategy.DefaultDelimiter}', ToLowercase: '{TokenizationStrategy.DefaultToLowercase}' ]";
        public static int TokenizationStrategy_Default_HashCode
            = (TokenizationStrategy.DefaultPattern, TokenizationStrategy.DefaultDelimiter, TokenizationStrategy.DefaultToLowercase).GetHashCode();

        public static TokenizationStrategy TokenizationStrategy_LettersSemicolon = new TokenizationStrategy("[a-Z]", ";", false);

        public static string Pattern_NonAlphanumerical = ";;;-- £/£&$£";
        public static TokenizationStrategy TokenizationStrategy_NonAlphanumerical
            = new TokenizationStrategy(
                    pattern: Pattern_NonAlphanumerical,
                    delimiter: TokenizationStrategy.DefaultDelimiter,
                    toLowercase: TokenizationStrategy.DefaultToLowercase
                );

        public static INGramTokenizerRuleSet NGramTokenizerRuleSet_Mono
            = new NGramTokenizerRuleSet(true, false, false, false, false);
        public static INGramTokenizerRuleSet NGramTokenizerRuleSet_MonoBi
            = new NGramTokenizerRuleSet(true, true, false, false, false);
        public static INGramTokenizerRuleSet NGramTokenizerRuleSet_MonoBiTri
            = new NGramTokenizerRuleSet(true, true, true, false, false);
        public static INGramTokenizerRuleSet NGramTokenizerRuleSet_MonoBiTriFour
            = new NGramTokenizerRuleSet(true, true, true, true, false);
        public static INGramTokenizerRuleSet NGramTokenizerRuleSet_MonoBiTriFourFive
            = new NGramTokenizerRuleSet(true, true, true, true, true);
        public static INGramTokenizerRuleSet NGramTokenizerRuleSet_Five
            = new NGramTokenizerRuleSet(false, false, false, false, true);

        #endregion

        #region Methods

        public static bool AreEqual(NGramTokenizerRuleSet obj1, NGramTokenizerRuleSet obj2)
        {

            return Equals(obj1.DoForMonogram, obj2.DoForMonogram)
                    && Equals(obj1.DoForBigram, obj2.DoForBigram)
                    && Equals(obj1.DoForTrigram, obj2.DoForTrigram)
                    && Equals(obj1.DoForFourgram, obj2.DoForFourgram)
                    && Equals(obj1.DoForFivegram, obj2.DoForFivegram);

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 18.10.2022
*/