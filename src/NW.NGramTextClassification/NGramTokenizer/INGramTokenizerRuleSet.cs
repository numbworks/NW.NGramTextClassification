namespace NW.NGramTextClassification
{
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
    Last Update: 30.01.2021

*/
