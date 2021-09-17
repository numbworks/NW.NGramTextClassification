using System;
using System.Collections.Generic;

namespace NW.NGramTextClassification.LabeledExamples
{
    /// <summary>Collects all the methods to create a <see cref="LabeledExample"/>.</summary>
    public interface ILabeledExampleFactory
    {

        /// <summary>
        /// Initializes a <see cref="LabeledExample"/> out of the provided parameters.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        LabeledExample Create(ulong id, string label, string text);

        /// <summary>
        /// Initializes a <see cref="LabeledExample"/> out of the provided parameters.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        LabeledExample Create(ulong id, string label, string text, ITokenizationStrategy strategy);

        /// <summary>
        /// Initializes a <see cref="LabeledExample"/> out of the provided parameters.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        LabeledExample Create(ulong id, string label, string text, ITokenizationStrategy strategy, INGramTokenizerRuleSet ruleSet);

        /// <summary>
        /// Initializes a <see cref="LabeledExample"/> out of the provided parameters.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/> 
        List<LabeledExample> Create(List<(string label, string text)> tuples);

        /// <summary>
        /// Initializes a <see cref="LabeledExample"/> out of the provided parameters.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/> 
        List<LabeledExample> Create(List<(string label, string text)> tuples, ITokenizationStrategy strategy);

        /// <summary>
        /// Initializes a <see cref="LabeledExample"/> out of the provided parameters.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>         
        List<LabeledExample> Create(List<(string label, string text)> tuples, ITokenizationStrategy strategy, INGramTokenizerRuleSet ruleSet);

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 17.09.2021
*/