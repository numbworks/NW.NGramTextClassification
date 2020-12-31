using System.Collections.Generic;

namespace NW.NGrams
{
    public interface INGramsTokenizer
    {
        List<T> DoFor<T>(ITokenizationStrategy strategy, string text) where T : INGram;
        List<T> DoFor<T>(string text) where T : INGram;
        List<INGram> DoForRuleset(INGramsTokenizerRuleSet ruleSet, ITokenizationStrategy strategy, string text);
        List<INGram> DoForRuleset(ITokenizationStrategy strategy, string text);
        List<INGram> DoForRuleset(string text);
    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 29.12.2020

*/