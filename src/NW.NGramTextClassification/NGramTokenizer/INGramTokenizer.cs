using System.Collections.Generic;

namespace NW.NGramTextClassification
{
    public interface INGramTokenizer
    {
        List<INGram> Do(string text, ITokenizationStrategy strategy, INGramTokenizerRuleSet ruleSet);
        List<INGram> Do(string text, ITokenizationStrategy strategy);
        List<INGram> Do(string text, INGramTokenizerRuleSet ruleSet);
        List<INGram> Do(string text);
    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 31.12.2020

*/