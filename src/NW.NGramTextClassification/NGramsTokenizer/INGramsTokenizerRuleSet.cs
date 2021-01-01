namespace NW.NGrams
{
    public interface INGramsTokenizerRuleSet
    {
        bool DoForBigrams { get; }
        bool DoForMonograms { get; }
        bool DoForTrigrams { get; }
    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 30.12.2020

*/