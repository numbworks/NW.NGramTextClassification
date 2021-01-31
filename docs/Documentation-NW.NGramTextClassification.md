# NW.NGramTextClassification
Contact: numbworks@gmail.com

## Revision History

| Date | Author | Description |
|---|---|---|
| 2020-12-27 | numbworks | Created. |
| 2021-01-30 | numbworks | Added examples, re-organized the document. |

## Introduction

`NW.NGramTextClassification` is a `.NET Standard 2.0` library written in `C#` to perform `Text Classification` on the string of text you provide. 

`Text Classification` is a `machine learning` technique that calculates the similarity between the string of text you need to categorize and a collection of already categorized strings you provide to the library to learn from it. 

The `accuracy` of the classification process is augmented by the technique used to split the text strings before the `similarity calculation`, which is based on collections of `N-Grams` instead of collections of single words. In this context, a `n-gram` is a contiguous sequence of `n words`, where `n` can be equal to 1 (`monogram`), 2 (`bigram`), 3 (`trigram`) and so on.

A `Text Classification` library can be super-useful for the resolution of many problems, such as `spam detection` or `language detection` in an automated environment.

## Example 1: Main Scenario 

Let's imagine we do have `several strings of text` we need to `detect the language for`, but `we don't want to pay` for using the `Google Translate API` or similar other product. 

We do know that these strings of text can be among three different languages (English, Swedish and Danish), but nothing more than that.

To perform the task above you need only few lines of code:

```csharp
using System;
using System.Collections.Generic;
using NW.NGramTextClassification;

/*...*/

string text = "We are looking for several skilled and driven developers to join our team.";
List<LabeledExample> labeledExamples = CreateLabeledExamples();

ITextClassifier textClassifier = new TextClassifier();
TextClassifierResult result = textClassifier.PredictLabel(text, labeledExamples);

Console.WriteLine(result.Label);
```

As you can see, the entry point of the library is the `TextClassifier` class and it offers a quite self-explanatory `PredictLabel` method.

The `text` variable contains the string of text that we need to categorize, while the `labeledExamples` variable contains the collection of strings that already have a label associated to them and that we will use to train the library.

Here how `labeledExamples` gets populated (strings are truncated):

```csharp
/*...*/

private static List<LabeledExample> CreateLabeledExamples()
{

    List<(string label, string text)> tuples = new List<(string label, string text)>()
    {

        ("en", "VerksamhetsbeskrivningGoGift is a company which focuses on [...]"),
        ("en", "You will report to the Team Manager. You will get the opportunity [...]"),
        /*...*/
        ("sv", "Conic Restaurants AB äger och driver idag SUBWAY restauranger [...]"),
        ("sv", "Du ska vara noggrann, van vid att ta eget ansvar och gilla att [...]"),
        /*...*/ 
        ("dk", "Har du lyst til et nært samarbejde med kolleger i en klinik [...]"),
        ("dk", "Lægesekretær/SOSU-assistent 20-25 timer ugentligt til almen [...]"),

    };

    return new LabeledExampleFactory().Create(tuples);

}

/*...*/
```

More `LabeledExamples` you do provide for each label and more accurate will be the prediction. Closer to the uncategorized text the `LabeledExamples` are and higher will be the accuracy - for ex. `LabeledExamples` about anthropology in multiple languages fed into the library for predicting the language of an uncategorized text about anthropology.  


...

```csharp

```

```csharp

```


## What's a Labeled Example?

A `LabeledExample` is defined as following:

```csharp
/*...*/

public class LabeledExample
{

    // Fields
    // Properties
    public ulong Id { get; }
    public string Label { get; }
    public string Text { get; }
    public List<INGram> TextAsNGrams { get; }

    // Constructors
    public LabeledExample
        (ulong id, string label, string text, List<INGram> textAsNGrams) { /*...*/ }

    /*...*/

}
```

`LabeledExample` objects can be created very easily by using the `LabeledExampleFactory` helper class, which basically automate the creation of the collection of `NGrams` for the `text` you provide:

```csharp
/*...*/

public class LabeledExampleFactory : ILabeledExampleFactory
{

    /*...*/

    // Constructors
    public LabeledExampleFactory
        (INGramTokenizer tokenizer, uint initialId) { /*...*/ }
    public LabeledExampleFactory()
        : this(new NGramTokenizer(), DefaultInitialId) { }

    // Methods (public)
    public LabeledExample Create
        (ulong id, string label, string text, ITokenizationStrategy strategy, INGramTokenizerRuleSet ruleSet) { /*...*/ }
            
    /*...*/

}
```

It requires you to provide an `INGramTokenizer`, an `ITokenizationStrategy` and an `INGramTokenizerRuleSet` that will influence how the `List<INGram>` property of the `LabeledExample` object will be populated.

If your `text` is "*We are looking for several skilled and driven developers to join our team.*" and you tokenize it by using `LabeledExampleFactory`'s default settings and dependencies, the resulting `List<INGram>` will look like:

```csharp
List<INGram> textAsNGrams = new List<INGram>() {

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

By default, the library use three types of `INGram` (`Monogram`, `Bigram` and `Trigram`), but nothing prevents you by using just one of these or enable the disabled ones (`Fourgram` and `Fivegram`) or to fork the library and extend it even further.

Using `Monogram`, `Bigram` and `Trigram` together does usually provide a quite good accuracy in the most common scenarios.

## Markdown Toolset

Suggested toolset to view and edit this Markdown file:

- [Visual Studio Code](https://code.visualstudio.com/)
- [Markdown Preview Enhanced](https://marketplace.visualstudio.com/items?itemName=shd101wyy.markdown-preview-enhanced)
- [Markdown PDF](https://marketplace.visualstudio.com/items?itemName=yzane.markdown-pdf)