using System.Collections.Generic;

namespace NW.NGramTextClassification
{
    public interface ILabeledExampleFactory
    {
        List<LabeledExample> Create(List<(string label, string text)> tuples);
        List<LabeledExample> Create(List<(string label, string text)> tuples, ITokenizationStrategy strategy);
        List<LabeledExample> Create(List<(string label, string text)> tuples, ITokenizationStrategy strategy, INGramTokenizerRuleSet ruleSet);
        LabeledExample Create(ulong id, string label, string text);
        LabeledExample Create(ulong id, string label, string text, ITokenizationStrategy strategy);
        LabeledExample Create(ulong id, string label, string text, ITokenizationStrategy strategy, INGramTokenizerRuleSet ruleSet);
    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 31.12.2020

*/