using System;
using System.Collections.Generic;
using NW.NGramTextClassification.NGrams;

namespace NW.NGramTextClassification.NGramTokenization
{
    /// <summary>Collects methods that break a text into tokens of type <see cref="INGram"/>.</summary>
    public interface INGramTokenizer
    {

        /// <summary>Returns N for the provided <see cref="INGram"/>.</summary>
        ushort GetN<T>() where T : INGram;

        /// <summary>Breaks <paramref name="text"/> into a collection of tokens according to <see cref="Monogram.N"/>.</summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        List<Monogram> DoForMonogram(string text);

        /// <summary>Breaks <paramref name="text"/> into a collection of tokens according to <see cref="Bigram.N"/>.</summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        List<Bigram> DoForBigram(string text);

        /// <summary>Breaks <paramref name="text"/> into a collection of tokens according to <see cref="Trigram.N"/>.</summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        List<Trigram> DoForTrigram(string text);

        /// <summary>Breaks <paramref name="text"/> into a collection of tokens according to <see cref="Fourgram.N"/>.</summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        List<Fourgram> DoForFourgram(string text);

        /// <summary>Breaks <paramref name="text"/> into a collection of tokens according to <see cref="Fivegram.N"/>.</summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        List<Fivegram> DoForFivegram(string text);

        /// <summary>
        /// Breaks <paramref name="text"/> into a collection of tokens according to <paramref name="tokenizerRuleset"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        List<INGram> DoForRuleset(string text, INGramTokenizerRuleSet tokenizerRuleset);

        /// <summary>
        /// Attempts to breaks <paramref name="text"/> into a collection of tokens according to <paramref name="tokenizerRuleset"/>.
        /// <para>If one rule fails, no exception will be thrown and the method will continue processing the other rules.</para>
        /// <para>If all rules will fail, null will be returned.</para>
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        List<INGram> TryDoForRuleset(string text, INGramTokenizerRuleSet tokenizerRuleset);

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 19.09.2021
*/