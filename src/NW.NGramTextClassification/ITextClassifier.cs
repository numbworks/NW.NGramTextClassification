using System.Collections.Generic;

namespace NW.NGrams
{
    public interface ITextClassifier
    {
        TextClassifierResult Do(string text, ITokenizationStrategy strategy, INGramsTokenizerRuleSet ruleSet, List<LabeledExtract> labeledExtracts);
    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 31.12.2020

*/