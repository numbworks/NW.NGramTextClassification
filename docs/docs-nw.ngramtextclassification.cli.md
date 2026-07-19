# NW.NGramTextClassification.CLI
Contact: numbworks@gmail.com

## Revision History

| Date | Author | Description |
|---|---|---|
| 2021-10-13 | numbworks | Created. |
| 2026-07-17 | numbworks | Last update (v4.2.1). |

## Introduction

`NW.NGramTextClassification.CLI` is a command-line application built on the top of `NW.NGramTextClassification` library.

## CLI Reference

|Command|Sub Command|Options|Exit Codes|
|---|---|---|---|
|session|||Success|
|session|classify|--labeledexamples:{filename}<br />--textsnippets:{filename}<br />*--folderpath:{path}*<br />*--tokenizerruleset:{filename}*<br />*--minaccuracysingle:{number}*<br />*--minaccuracymultiple:{number}*<br />*--savesession*<br />*--cleanlabeledexamples*<br />*--disableindexserialization*|Success<br />Failure|

## Examples

The simplest command you can run is `session classify`, which performs a text classification task on the data you provide. At very least, the command will look like:

```powershell
PS C:\nwngram>.\nwngram.exe session classify --labeledexamples:LabeledExamples.json --textsnippets:TextSnippets.json
```

The command above requires that you have the two required files (`LabeledExamples.json` and `TextSnippets.json`) located in the same folder as the application, which by default it's the working folder for all the application's activities. 

The command above will log something like this to the console:

```
...
[2022-10-29 22:11:47:640] Attempting to load a collection of 'LabeledExample' objects from: C:\nwngram\LabeledExamples.json.
[2022-10-29 22:11:47:747] A collection of 'LabeledExample' objects has been successfully loaded.
[2022-10-29 22:11:47:748] Attempting to load a collection of 'TextSnippet' objects from: C:\nwngram\TextSnippets.json.
[2022-10-29 22:11:47:749] A collection of 'TextSnippet' objects has been successfully loaded.
[2022-10-29 22:11:47:750] The provided snippets are: '2'.
[2022-10-29 22:11:47:767] The provided snippet has been tokenized into '65' INGram object.
[2022-10-29 22:11:47:769] The provided LabeledExample objects have been thru the tokenization process.
...
[2022-10-29 22:11:47:792] The 'SimilarityIndexAverage' object with the highest value is: '[ Label: 'sv', Value: '1' ]'.
[2022-10-29 22:11:47:792] The result of the classification task is: 'sv'.
[2022-10-29 22:11:47:792] The classification task has been successful.
```

If you wish to store the files elsewhere, you can specify a new working folder by using the `folderpath` option - i.e. `--folderpath:C:\Documents`

## Markdown Toolset

Suggested toolset to view and edit this Markdown file:

- [Visual Studio Code](https://code.visualstudio.com/)
- [Markdown Preview Enhanced](https://marketplace.visualstudio.com/items?itemName=shd101wyy.markdown-preview-enhanced)
- [Markdown PDF](https://marketplace.visualstudio.com/items?itemName=yzane.markdown-pdf)