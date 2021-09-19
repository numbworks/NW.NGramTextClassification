using System;
using System.Collections.Generic;
using NW.NGramTextClassification.NGramTokenization;

namespace NW.NGramTextClassification.LabeledExamples
{
    /// <summary>Collects all the methods to create a <see cref="LabeledExample"/>.</summary>
    public interface ILabeledExampleFactory
    {

        /// <summary>
        /// Initializes a <see cref="LabeledExample"/> out of the provided parameters.
        /// <para>If one rule fails, no exception will be thrown and the method will continue processing the other rules.</para>
        /// <para>If all rules will fail, null will be returned.</para>
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        LabeledExample TryCreateForRuleSet(ulong id, string label, string text, INGramTokenizerRuleSet tokenizerRuleSet);

        /// <summary>
        /// Initializes a <see cref="LabeledExample"/> out of the provided parameters and <see cref="LabeledExampleFactory.DefaultTokenizerRuleSet"/>.
        /// <para>If one rule fails, no exception will be thrown and the method will continue processing the other rules.</para>
        /// <para>If all rules will fail, null will be returned.</para>
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        LabeledExample TryCreateForRuleSet(ulong id, string label, string text);

        /// <summary>
        /// Initializes a collection of <see cref="LabeledExample"/> objects out of the provided parameters.
        /// <para>If one rule fails, no exception will be thrown and the method will continue processing the other rules.</para>
        /// <para>If all rules will fail, null will be returned.</para>
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        List<LabeledExample> TryCreateForRuleSet(List<(string label, string text)> tuples, INGramTokenizerRuleSet tokenizerRuleSet);

        /// <summary>
        /// Initializes a collection of <see cref="LabeledExample"/> objects out of the provided parameters and <see cref="LabeledExampleFactory.DefaultTokenizerRuleSet"/>.
        /// <para>If one rule fails, no exception will be thrown and the method will continue processing the other rules.</para>
        /// <para>If all rules will fail, null will be returned.</para>
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        List<LabeledExample> TryCreateForRuleSet(List<(string label, string text)> tuples);

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 19.09.2021
*/