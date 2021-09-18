namespace NW.NGramTextClassification.NGramTokenization
{
    /// <summary>A collection of rules for <see cref="INGramTokenizer"/>.</summary>
    public interface INGramTokenizerRuleSet
    {
        bool DoForBigrams { get; }
        bool DoForMonograms { get; }
        bool DoForTrigrams { get; }
        bool DoForFourgrams { get; }
        bool DoForFivegrams { get; }
    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 17.09.2021
*/
