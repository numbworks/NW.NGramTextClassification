using System.Collections.Generic;

namespace NW.NGrams
{
    public interface ILabeledExtractFactory
    {
        List<LabeledExtract> CreateFor<T>(List<(string label, string text)> tuples) where T : INGram;
        List<LabeledExtract> CreateFor<T>(List<(string label, string text)> tuples, ITokenizationStrategy strategy) where T : INGram;
        LabeledExtract CreateFor<T>(ulong id, string label, ITokenizationStrategy strategy, string text) where T : INGram;
        LabeledExtract CreateFor<T>(ulong id, string label, string text) where T : INGram;
        List<LabeledExtract> CreateForRuleset(List<(string label, string text)> tuples);
        List<LabeledExtract> CreateForRuleset(List<(string label, string text)> tuples, INGramsTokenizerRuleSet ruleSet, ITokenizationStrategy strategy);
        List<LabeledExtract> CreateForRuleset(List<(string label, string text)> tuples, ITokenizationStrategy strategy);
        LabeledExtract CreateForRuleset(ulong id, string label, INGramsTokenizerRuleSet ruleSet, ITokenizationStrategy strategy, string text);
        LabeledExtract CreateForRuleset(ulong id, string label, ITokenizationStrategy strategy, string text);
        LabeledExtract CreateForRuleset(ulong id, string label, string text);
    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 30.12.2020

*/