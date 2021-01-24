using System.Collections.Generic;

namespace NW.NGramTextClassification
{
    public interface ITextClassifier
    {
        TextClassifierResult PredictLabel
            (string text, ITokenizationStrategy strategy, INGramTokenizerRuleSet ruleSet, List<LabeledExample> labeledExamples);
        TextClassifierResult PredictLabel
            (string text, INGramTokenizerRuleSet ruleSet, List<LabeledExample> labeledExamples);
        TextClassifierResult PredictLabel
            (string text, List<LabeledExample> labeledExamples);
    }
}

/*

    Author: numbworks@gmail.com
    Last Update: 31.12.2020

*/