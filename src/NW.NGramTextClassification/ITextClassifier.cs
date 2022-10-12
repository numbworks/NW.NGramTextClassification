using System;
using System.Collections.Generic;
using NW.NGramTextClassification.Files;
using NW.NGramTextClassification.LabeledExamples;
using NW.NGramTextClassification.NGramTokenization;
using NW.NGramTextClassification.TextClassifications;
using NW.NGramTextClassification.TextSnippets;

namespace NW.NGramTextClassification
{
    /// <summary>The entry point of this library.</summary>
    public interface ITextClassifier
    {

        /// <summary>
        /// Attempts to assign a label to <paramref name="textSnippet"/> by learning from <paramref name="labeledExamples"/>.
        /// <para>If one rule in <paramref name="tokenizerRuleSet"/> fails, no exception will be thrown and the method will continue processing the other rules.</para>
        /// <para>If all rules will fail, <see cref="TextClassifier.DefaultTextClassifierResult"/> will be added to the <see cref="TextClassifierSession"/>.</para>
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>      
        TextClassifierSession ClassifyOrDefault(TextSnippet textSnippet, INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples);

        /// <summary>
        /// Attempts to assign a label to <paramref name="textSnippet"/> by learning from <paramref name="labeledExamples"/> and by using a default <see cref="INGramTokenizerRuleSet"/>.
        /// <para>If one rule in the default <see cref="INGramTokenizerRuleSet"/> fails, no exception will be thrown and the method will continue processing the other rules.</para>
        /// <para>If all rules will fail, <see cref="TextClassifier.DefaultTextClassifierResult"/> will be added to the <see cref="TextClassifierSession"/>.</para>
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>       
        TextClassifierSession ClassifyOrDefault(TextSnippet textSnippet, List<LabeledExample> labeledExamples);

        /// <summary>
        /// Attempts to assign a label to every snippet of text in <paramref name="textSnippets"/> by learning from <paramref name="labeledExamples"/>.
        /// <para>If one rule in <paramref name="tokenizerRuleSet"/> fails, no exception will be thrown and the method will continue processing the other rules.</para>
        /// <para>If all rules will fail for one snippet, the corresponding <see cref="TextClassifierResult"/> will be <see cref="TextClassifier.DefaultTextClassifierResult"/>.</para>
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>      
        TextClassifierSession ClassifyMany(List<TextSnippet> textSnippets, INGramTokenizerRuleSet tokenizerRuleSet, List<LabeledExample> labeledExamples);

        /// <summary>
        /// Attempts to assign a label to every snippet of text in <paramref name="textSnippets"/> by learning from <paramref name="labeledExamples"/>.
        /// <para>If one rule in the default <see cref="INGramTokenizerRuleSet"/> fails, no exception will be thrown and the method will continue processing the other rules.</para>
        /// <para>If all rules will fail for one snippet, the corresponding <see cref="TextClassifierResult"/> will be <see cref="TextClassifier.DefaultTextClassifierResult"/>.</para>
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>      
        TextClassifierSession ClassifyMany(List<TextSnippet> textSnippets, List<LabeledExample> labeledExamples);

        /// <summary>Logs the library's ascii banner.</summary>
        void LogAsciiBanner();

        /// <summary>
        /// Loads a collection of <see cref="LabeledExample"/> objects from the provided <paramref name="jsonFile"/>. 
        /// <para>If the content of the file is null/empty/invalid or an exception is thrown, <see cref="LabeledExampleSerializer.Default"/> will be returned.</para>
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>     
        List<LabeledExample> LoadLabeledExamplesOrDefault(IFileInfoAdapter jsonFile);

        /// <summary>
        /// Loads a collection of <see cref="LabeledExample"/> objects from the provided <paramref name="filePath"/>. 
        /// <para>If the content of the file is null/empty/invalid or an exception is thrown, <see cref="LabeledExampleSerializer.Default"/> will be returned.</para>
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>   
        List<LabeledExample> LoadLabeledExamplesOrDefault(string filePath);

        /// <summary>
        /// Loads a collection of <see cref="TextSnippet"/> objects from the provided <paramref name="jsonFile"/>. 
        /// <para>If the content of the file is null/empty/invalid or an exception is thrown, <see cref="TextSnippetSerializer.Default"/> will be returned.</para>
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>     
        List<TextSnippet> LoadTextSnippetsOrDefault(IFileInfoAdapter jsonFile);

        /// <summary>
        /// Loads a collection of <see cref="TextSnippet"/> objects from the provided <paramref name="filePath"/>. 
        /// <para>If the content of the file is null/empty/invalid or an exception is thrown, <see cref="TextSnippetSerializer.Default"/> will be returned.</para>
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>   
        List<TextSnippet> LoadTextSnippetsOrDefault(string filePath);

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 12.10.2022
*/