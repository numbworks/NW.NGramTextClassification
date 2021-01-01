using System.Collections.Generic;

namespace NW.NGramTextClassification
{
    public interface ITextClassifier
    {
        TextClassifierResult Predict
            (string text, ITokenizationStrategy strategy, INGramsTokenizerRuleSet ruleSet, List<LabeledExtract> labeledExtracts);
        TextClassifierResult Predict
            (string text, INGramsTokenizerRuleSet ruleSet, List<LabeledExtract> labeledExtracts);
        TextClassifierResult Predict
            (string text, List<LabeledExtract> labeledExtracts);
    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 31.12.2020

*/