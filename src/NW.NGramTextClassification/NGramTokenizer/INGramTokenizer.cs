using System;
using System.Collections.Generic;
using NW.NGramTextClassification.NGrams;

namespace NW.NGramTextClassification
{

    /// <summary>Collects methods that break a text into tokens of type <see cref="INGram"/>.</summary>
    public interface INGramTokenizer
    {

        /// <summary>
        /// Breaks <paramref name="text"/> into a collection of tokens according to <paramref name="strategy"/> and <paramref name="ruleSet"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        List<INGram> Do(string text, ITokenizationStrategy strategy, INGramTokenizerRuleSet ruleSet);

        /// <summary>
        /// Breaks <paramref name="text"/> into a collection of tokens according to <paramref name="strategy"/> and a default <paramref name="ruleSet"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        List<INGram> Do(string text, ITokenizationStrategy strategy);

        /// <summary>
        /// Breaks <paramref name="text"/> into a collection of tokens according to <paramref name="ruleSet"/> and a default <paramref name="strategy"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        List<INGram> Do(string text, INGramTokenizerRuleSet ruleSet);

        /// <summary>
        /// Breaks <paramref name="text"/> into a collection of tokens using default <paramref name="ruleSet"/> and <paramref name="strategy"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        List<INGram> Do(string text);
    
    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 17.09.2021
*/