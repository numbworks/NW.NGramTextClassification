using System;
using System.Collections.Generic;
using NW.NGramTextClassification.NGramTokenization;

namespace NW.NGramTextClassification.LabeledExamples
{
    /// <summary>Collects all the helper methods related to <see cref="LabeledExample"/>.</summary>
    public interface ILabeledExampleManager
    {

        /// <summary>
        /// Initializes a <see cref="TokenizedExample"/> out of the provided parameters.
        /// <para>If one rule fails, no exception will be thrown and the method will continue processing the other rules.</para>
        /// <para>If all rules will fail, <see cref="LabeledExampleManager.DefaultTokenizedExample"/> will be returned.</para>
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        TokenizedExample CreateOrDefault(LabeledExample labeledExample, INGramTokenizerRuleSet tokenizerRuleSet);

        /// <summary>
        /// Initializes a <see cref="TokenizedExample"/> out of the provided parameters and <see cref="LabeledExampleManager.DefaultTokenizerRuleSet"/>.
        /// <para>If one rule fails, no exception will be thrown and the method will continue processing the other rules.</para>
        /// <para>If all rules will fail, <see cref="LabeledExampleManager.DefaultTokenizedExample"/> will be returned.</para>
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        TokenizedExample CreateOrDefault(LabeledExample labeledExample);

        /// <summary>
        /// Initializes a collection of <see cref="TokenizedExample"/> objects out of the provided parameters.
        /// <para>If at least one <see cref="TokenizedExample"/> object fails to be created, <see cref="LabeledExampleManager.DefaultTokenizedExamples"/> will be returned.</para>
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        List<TokenizedExample> CreateOrDefault(List<LabeledExample> labeledExamples, INGramTokenizerRuleSet tokenizerRuleSet);

        /// <summary>
        /// Initializes a collection of <see cref="TokenizedExample"/> objects out of the provided parameters and <see cref="LabeledExampleManager.DefaultTokenizerRuleSet"/>.
        /// <para>If at least one <see cref="TokenizedExample"/> object fails to be created, <see cref="LabeledExampleManager.DefaultTokenizedExamples"/> will be returned.</para>
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        List<TokenizedExample> CreateOrDefault(List<LabeledExample> labeledExamples);

        /// <summary>
        /// Returns all the <see cref="LabeledExample"/> objects in <paramref name="labeledExamples"/> that don't make the tokenizer to fail.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        List<LabeledExample> CleanLabeledExamples(List<LabeledExample> labeledExamples, INGramTokenizerRuleSet tokenizerRuleSet, out List<LabeledExample> removed);

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 04.11.2022
*/