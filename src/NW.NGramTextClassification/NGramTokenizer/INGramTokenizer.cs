using System.Collections.Generic;

namespace NW.NGramTextClassification
{
    public interface INGramTokenizer
    {
        List<INGram> Do(string text, ITokenizationStrategy strategy, INGramsTokenizerRuleSet ruleSet);
        List<INGram> Do(string text, ITokenizationStrategy strategy);
        List<INGram> Do(string text, INGramsTokenizerRuleSet ruleSet);
        List<INGram> Do(string text);
    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 31.12.2020

*/