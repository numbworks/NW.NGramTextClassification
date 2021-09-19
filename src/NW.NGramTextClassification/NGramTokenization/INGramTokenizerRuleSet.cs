namespace NW.NGramTextClassification.NGramTokenization
{
    /// <summary>A collection of rules for <see cref="INGramTokenizer"/>.</summary>
    public interface INGramTokenizerRuleSet
    {
        bool DoForBigram { get; }
        bool DoForMonogram { get; }
        bool DoForTrigram { get; }
        bool DoForFourgram { get; }
        bool DoForFivegram { get; }
    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 17.09.2021
*/
