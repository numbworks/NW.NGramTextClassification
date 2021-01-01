using System.Collections.Generic;

namespace NW.NGrams
{
    public interface ILabeledExtractFactory
    {
        List<LabeledExtract> Create(List<(string label, string text)> tuples);
        List<LabeledExtract> Create(List<(string label, string text)> tuples, ITokenizationStrategy strategy);
        List<LabeledExtract> Create(List<(string label, string text)> tuples, ITokenizationStrategy strategy, INGramsTokenizerRuleSet ruleSet);
        LabeledExtract Create(ulong id, string label, string text);
        LabeledExtract Create(ulong id, string label, string text, ITokenizationStrategy strategy);
        LabeledExtract Create(ulong id, string label, string text, ITokenizationStrategy strategy, INGramsTokenizerRuleSet ruleSet);
    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 31.12.2020

*/