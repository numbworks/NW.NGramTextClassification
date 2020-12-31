using System.Collections.Generic;

namespace NW.NGrams
{
    public interface INGramsTokenizer
    {
        List<T> Do<T>(ITokenizationStrategy strategy, string text) where T : INGram;
        List<T> Do<T>(string text) where T : INGram;
        List<INGram> DoForMany(INGramsTokenizerRuleSet ruleSet, ITokenizationStrategy strategy, string text);
        List<INGram> DoForMany(ITokenizationStrategy strategy, string text);
        List<INGram> DoForMany(string text);
    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 29.12.2020

*/