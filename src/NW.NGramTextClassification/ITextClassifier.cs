﻿using System;
using System.Collections.Generic;
using NW.NGramTextClassification.LabeledExamples;
using NW.NGramTextClassification.NGramTokenization;
using NW.NGramTextClassification.TextClassifications;
using NW.NGramTextClassification.TextSnippets;
using NW.Shared.Files;
using NW.Shared.Serialization;

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

        /// <summary>
        /// Logs the library's ascii banner.
        /// </summary>
        void LogAsciiBanner();

        /// <summary>
        /// Convert <paramref name="filePath"/> to <see cref="IFileInfoAdapter"/>
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        IFileInfoAdapter Convert(string filePath);

        /// <summary>
        /// Loads a collection of <see cref="LabeledExample"/> objects from the provided <paramref name="jsonFile"/>. 
        /// <para>If the content of the file is null/empty/invalid or an exception is thrown, <see cref="Serializer{LabeledExample}.Default"/> will be returned.</para>
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>     
        List<LabeledExample> LoadLabeledExamplesOrDefault(IFileInfoAdapter jsonFile);

        /// <summary>
        /// Loads a collection of <see cref="TextSnippet"/> objects from the provided <paramref name="jsonFile"/>. 
        /// <para>If the content of the file is null/empty/invalid or an exception is thrown, <see cref="Serializer{TextSnippet}.Default"/> will be returned.</para>
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>     
        List<TextSnippet> LoadTextSnippetsOrDefault(IFileInfoAdapter jsonFile);

        /// <summary>
        /// Loads a <see cref="NGramTokenizerRuleSet"/> object from the provided <paramref name="jsonFile"/>. 
        /// <para>If the content of the file is null/empty/invalid or an exception is thrown, <c>default(NGramTokenizerRuleSet)</c> will be returned.</para>
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>     
        NGramTokenizerRuleSet LoadTokenizerRuleSetOrDefault(IFileInfoAdapter jsonFile);

        /// <summary>
        /// Saves the provided collection of <see cref="LabeledExample"/> objects as JSON into <paramref name="folderPath"/>. 
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="Exception"/>
        void SaveLabeledExamples(List<LabeledExample> labeledExamples, string folderPath);

        /// <summary>
        /// Saves the provided collection of <see cref="TextSnippet"/> objects as JSON into <paramref name="folderPath"/>. 
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="Exception"/>
        void SaveTextSnippets(List<TextSnippet> textSnippets, string folderPath);

        /// <summary>
        /// Saves the provided <see cref="TextClassifierSession"/> object as JSON into <paramref name="folderPath"/>. 
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="Exception"/>
        void SaveSession(TextClassifierSession session, string folderPath, bool disableIndexSerialization);

        /// <summary>
        /// Returns all the <see cref="LabeledExample"/> objects in <paramref name="labeledExamples"/> that don't make the tokenizer to fail.
        /// <para>The method logs the objects that get removed.</para>
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        List<LabeledExample> CleanLabeledExamples(List<LabeledExample> labeledExamples, INGramTokenizerRuleSet tokenizerRuleSet);

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 14.02.2024
*/