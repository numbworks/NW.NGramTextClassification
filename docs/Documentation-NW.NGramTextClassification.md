# NW.NGramTextClassification
Contact: numbworks@gmail.com

## Revision History

| Date | Author | Description |
|---|---|---|
| 2020-12-27 | numbworks | Created. |
| 2021-01-30 | numbworks | Added examples, re-organized the document. |
| 2021-02-15 | numbworks | Completed "Example 1: Main Scenario". |
| 2021-09-24 | numbworks | Version numbers removed, updated examples for v2.0.0. |
| 2022-09-27 | numbworks | Updated to v3.0.0. |
| 2022-10-16 | numbworks | Updated to v3.5.0. |

## Introduction

`NW.NGramTextClassification` is a .NET Standard library to perform text classification tasks on the text snippets you provide.

## What does "text classification" mean?

`Text Classification` is a `machine learning` technique that calculates the similarity between the string of text you need to categorize and a collection of already categorized strings you provide to the library to learn from it. 

Every string of text is decomposed in collections of `n-grams` and compared by using a `similarity calculator`. A `n-gram` is a contiguous sequence of `n words`, where `n` can be equal to 1 (`monogram`), 2 (`bigram`), 3 (`trigram`) and so on.

A `Text Classification` library can be very useful for the resolution of many problems, such as `spam detection` or `language detection` in an automated environment.

## A use case scenario: language detection

We have several strings of text and we want to detect their language. 

We do know beforehand that these strings of text can be among three different languages, and we do have a batch of pre-labeled strings for each of them, but nothing more than that.

To perform the task above you need only few lines of code:

```csharp
using System;
using System.Collections.Generic;
using NW.NGramTextClassification;
using NW.NGramTextClassification.LabeledExamples;
using NW.NGramTextClassification.TextClassifications;
using NW.NGramTextClassification.TextSnippets;

/*...*/

TextSnippet textSnippet 
    = new TextSnippet(text: "We are looking for several skilled and driven developers to join our team.");
List<LabeledExample> labeledExamples = CreateLabeledExamples();

ITextClassifier textClassifier = new TextClassifier();
TextClassifierSession session = textClassifier.ClassifyOrDefault(textSnippet, labeledExamples);

Console.WriteLine(session.Results[0].Label);
```

The entry point of the library (`TextClassifier`) offers a quite self-explanatory `ClassifyOrDefault` method.

The `textSnippet` object contains the string of text that we need to categorize, while the `labeledExamples` is a collection of strings that already have a label associated to them and that we will use to train the library.

Here how `labeledExamples` is getting populated (strings are truncated at `[...]` for a readibility purpose):

```csharp
private static List<LabeledExample> CreateLabeledExamples()
{

    List<LabeledExample> labeledExamples = new List<LabeledExample>()
    {

        new LabeledExample(label: "en", text: "VerksamhetsbeskrivningGoGift is a company which focuses on [...]"),
        new LabeledExample(label: "en", text: "You will report to the Team Manager. You will get the opportunity [...]"),
        /*...*/
        new LabeledExample(label: "sv", text: "Conic Restaurants AB äger och driver idag SUBWAY restauranger [...]"),
        new LabeledExample(label: "sv", text: "Du ska vara noggrann, van vid att ta eget ansvar och gilla att [...]"),
        /*...*/
        new LabeledExample(label: "dk", text: "Har du lyst til et nært samarbejde med kolleger i en klinik [...]"),
        new LabeledExample(label: "dk", text: "Lægesekretær/SOSU-assistent 20-25 timer ugentligt til almen [...]"),
        /*...*/

    };

    return labeledExamples;

}
```

Once you have both `textSnippet` and `labeledExamples` variables properly set, you can initialize a `TextClassifier` object and call its `ClassifyOrDefault` method.

Apart from `Label`, the `TextClassifierResult` object contains some diagnostic information, which we can use to improve the accuracy of the classsification. 

Here how the `TextClassifierResult` class definition looks like:

```csharp
public class TextClassifierResult
{

    public string Label { get; }
    public List<SimilarityIndex> SimilarityIndexes { get; }
    public List<SimilarityIndexAverage> SimilarityIndexAverages { get; }

    /* ... */
}
```

The core logic of the `ClassifyOrDefault` method is the following one (some logging statements have been removed for readibility purpose):

```csharp
private TextClassifierResult CreateResult(List<INGram> nGrams, List<TokenizedExample> tokenizedExamples)
{

    List<SimilarityIndex> indexes = GetSimilarityIndexes(nGrams, tokenizedExamples);
    List<SimilarityIndexAverage> indexAverages = GetSimilarityIndexAverages(indexes);

    string label = GetLabel(indexAverages);
    _components.LoggingAction.Invoke(MessageCollection.TextClassifier_PredictedLabelIs(label));

    if (label == null)
        _components.LoggingAction.Invoke(MessageCollection.TextClassifier_PredictionHasFailedTryIncreasingTheAmountOfProvidedLabeledExamples);
    else
        _components.LoggingAction.Invoke(MessageCollection.TextClassifier_PredictionHasBeenSuccessful);

    TextClassifierResult result = new TextClassifierResult(label, indexes, indexAverages);

    return result;

} 
```

The content of the `textSnippet` variable gets tokenized into a collection of `INGrams`, and each of them is compared to the provided collection of `LabeledExamples`.

The outcome is a collection of `SimilarityIndexes`, which looks like:

| Id | Label | Value |
|---|---|---|
|1|en|0,01995|
|2|en|0,014888|
|...|...|...|
|11|sv|0,002268|
|12|sv|0|
|...|...|...|
|21|dk|0,005025|
|22|dk|0|
|...|...|...|

All these values need to be averaged, so that we have one average value for each label:

| Label | Value |
|---|---|
|en|0,016777|
|sv|0,000942|
|dk|0,003026|

The `GetLabel` method will run this collection of `SimilarityIndexAverage` objects thru a bunch of strategies, and eventually return the label with the highest average value (highest similarity), which in this case is `en`.

 The `ClassifyOrDefault` method can return:

- a `TextClassifierResult` object containing the presumed label
- a `TextClassifierResult` object containing a `null` label.

The `TextClassifierResult` object is contained into a `TextClassifierSession` object, which contains additional information about the classification process. 

The `TextClassifierSession` class definition looks like:

```csharp
public class TextClassifierSession
{

    public double MinimumAccuracySingleLabel { get; }
    public double MinimumAccuracyMultipleLabels { get; }
    public List<TextClassifierResult> Results { get; }
    public string Version { get; }

    /* ... */

}
```

## The tokenizazion process

The library component responsible for the tokenization process is `NGramTokenizer` and it requires a `NGramTokenizerRuleSet` in order to create a collection of `INGram` objects our of whatever string of text. 

A `NGramTokenizerRuleSet` looks like the following:

```csharp
INGramTokenizerRuleSet nGramTokenizerRuleSet 
    = new NGramTokenizerRuleSet(
            doForMonogram: true, 
            doForBigram: true, 
            doForTrigram: true, 
            doForFourgram: false, 
            doForFivegram: false
        );
```

If your `textSnippet` is "*We are looking for several skilled and driven developers to join our team.*", the `NGramTokenizerRuleSet` above will generate the following collection of `INGram` objects:

```csharp
List<INGram> nGrams = new List<INGram>() {

    new Monogram("we"),
    new Monogram("are"),
    new Monogram("looking"),
    new Monogram("for"),
    new Monogram("several"),
    new Monogram("skilled"),
    new Monogram("and"),
    new Monogram("driven"),
    new Monogram("developers"),
    new Monogram("to"),
    new Monogram("join"),
    new Monogram("our"),
    new Monogram("team"),
    new Bigram("we are"),
    new Bigram("are looking"),
    new Bigram("looking for"),
    new Bigram("for several"),
    new Bigram("several skilled"),
    new Bigram("skilled and"),
    new Bigram("and driven"),
    new Bigram("driven developers"),
    new Bigram("developers to"),
    new Bigram("to join"),
    new Bigram("join our"),
    new Bigram("our team"),
    new Bigram("team"),
    new Trigram("we are looking"),
    new Trigram("are looking for"),
    new Trigram("looking for several"),
    new Trigram("for several skilled"),
    new Trigram("several skilled and"),
    new Trigram("skilled and driven"),
    new Trigram("and driven developers"),
    new Trigram("driven developers to"),
    new Trigram("developers to join"),
    new Trigram("to join our"),
    new Trigram("join our team"),
    new Trigram("our team"),
    new Trigram("team")

};
```

## Improving the accuracy of the classification

The accuracy of the classification can be improved:

1. by increasing the number of `LabeledExamples` for each label
2. by using a collection of `LabeledExamples` that is as closer as possible to the knowledge domain of the uncategorized text - for ex. attempting to detect the language of a piece of text about anthropology using a collection of multilingual `LabeledExample` objects about the same topic.
3. Increasing the number of `INGram` objects - for ex. using only `Monograms`, `Bigrams` and `Trigrams` is good enough in most common scenarios, but `Fourgrams` and `Fivegrams` are also available in the library.

## Load and save

The library is able to load and save different key-objects using JSON format. 

Here an example of each JSON file produced by the library:

1. [LabeledExamples.json](ExampleFiles/LabeledExamples.json)
2. [TextSnippets.json](ExampleFiles/TextSnippets.json)
3. [Session.json](ExampleFiles/Session.json) - v.3.5.0.0
4. [TokenizerRuleSet.json](ExampleFiles/TokenizerRuleSet.json)

## Markdown Toolset

Suggested toolset to view and edit this Markdown file:

- [Visual Studio Code](https://code.visualstudio.com/)
- [Markdown Preview Enhanced](https://marketplace.visualstudio.com/items?itemName=shd101wyy.markdown-preview-enhanced)
- [Markdown PDF](https://marketplace.visualstudio.com/items?itemName=yzane.markdown-pdf)