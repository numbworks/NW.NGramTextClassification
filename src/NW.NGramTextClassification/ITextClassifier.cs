using System;
using System.Collections.Generic;
using NW.NGramTextClassification.LabeledExamples;
using NW.NGramTextClassification.NGramTokenization;

namespace NW.NGramTextClassification
{
    /// <summary>The entry point of this library.</summary>
    public interface ITextClassifier
    {

        /// <summary>
        /// Attempts to assign a label to <paramref name="text"/> by learning from <paramref name="labeledExamples"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>          
        TextClassifierResult PredictLabel(string text, INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples);

        /// <summary>
        /// Attempts to assign a label to <paramref name="text"/> by learning from <paramref name="labeledExamples"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>        
        TextClassifierResult PredictLabel(string text, List<LabeledExample> labeledExamples);

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 19.09.2021
*/