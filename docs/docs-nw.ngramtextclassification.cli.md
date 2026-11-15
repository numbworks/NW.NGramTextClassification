# NW.NGramTextClassification.CLI
Contact: numbworks@gmail.com

## Revision History

| Date | Author | Description |
|---|---|---|
| 2021-10-13 | numbworks | Created. |
| 2026-11-15 | numbworks | Last update (v5.0.0). |

## Introduction

`NW.NGramTextClassification.CLI` is a command-line application built on the top of `NW.NGramTextClassification` library.

## CLI Reference

|Command|Sub Command|Options|Exit Codes|
|---|---|---|---|
|classify|||Success|
|classify||--labeledexamples:{filename}<br />--textsnippets:{filename}<br />*--folderpath:{path}*<br />*--tokenizerruleset:{filename}*<br />*--minaccuracysingle:{number}*<br />*--minaccuracymultiple:{number}*<br />*--saveoutput*<br />*--cleanlabeledexamples*<br />*--disableindexserialization*<br />*--enabletrue*|Success<br />Failure|

## Examples

The simplest command you can run is `classify`, which performs a text classification task on the data you provide. 

The command above requires that you have the two required files (`LabeledExamples.json` and `TextSnippets.json`) located in the same folder as the application, which by default it's the working folder for all the application's activities. 

At very least, the command will look like:

```
.\nwngram.exe classify --labeledexamples .\LabeledExamples.json --textsnippets .\TextSnippets.json
```

The command above will log something like this to the console:

```
****************************************************************************
'##::: ##:'##:::::'##:'##::: ##::'######:::'########:::::'###::::'##::::'##:
 ###:: ##: ##:'##: ##: ###:: ##:'##... ##:: ##.... ##:::'## ##::: ###::'###:
 ####: ##: ##: ##: ##: ####: ##: ##:::..::: ##:::: ##::'##:. ##:: ####'####:
 ## ## ##: ##: ##: ##: ## ## ##: ##::'####: ########::'##:::. ##: ## ### ##:
 ##. ####: ##: ##: ##: ##. ####: ##::: ##:: ##.. ##::: #########: ##. #: ##:
 ##:. ###: ##: ##: ##: ##:. ###: ##::: ##:: ##::. ##:: ##.... ##: ##:.:: ##:
 ##::. ##:. ###. ###:: ##::. ##:. ######::: ##:::. ##: ##:::: ##: ##:::: ##:
..::::..:::...::...:::..::::..:::......::::..:::::..::..:::::..::..:::::..::
*********************************************************Version: 5.0.0*****


[2026-11-15 19:16:09:545] The text classification process has started.
[2026-11-15 19:16:09:552] The provided 'FolderPath' is: '/workspaces/NW.NGramTextClassification/docs/ExampleFiles/'.
[2026-11-15 19:16:09:552] The provided 'MinimumAccuracySingleLabel' is: '0.5'.
[2026-11-15 19:16:09:553] The provided 'MinimumAccuracyMultipleLabels' is: '0'.
[2026-11-15 19:16:09:553] The provided 'SaveOutput' is: 'False'.
[2026-11-15 19:16:09:553] The provided 'CleanLabeledExamples' is: 'False'.
[2026-11-15 19:16:09:553] The provided 'DisableIndexSerialization' is: 'False'.
[2026-11-15 19:16:09:553] The provided 'EnableUTC' is: 'False'.
[2026-11-15 19:16:09:553] The provided 'LabeledExamplesFileName' is: 'LabeledExamples.json'.
[2026-11-15 19:16:09:553] The provided 'TextSnippetsFileName' is: 'TextSnippets.json'.
[2026-11-15 19:16:09:696] '2' LabeledExample objects have been loaded.
[2026-11-15 19:16:09:704] '2' TextSnippet objects have been loaded.
[2026-11-15 19:16:09:704] The provided 'NGramTokenizerRuleSet' is: '[ DoForMonogram: 'True', DoForBigram: 'True', DoForTrigram: 'True', DoForFourgram: 'True', DoForFivegram: 'True' ]'.
[2026-11-15 19:16:09:840] The text classification output has been successfully created.
[2026-11-15 19:16:09:840] The text classification output contains the following results:
[2026-11-15 19:16:09:840] [ Text: 'We are looking for several skilled and driven developers to join our team.', Label: 'en' ]
[2026-11-15 19:16:09:840] [ Text: 'Vår kund erbjuder trivsel, arbetsglädje och en trygg arbetsmiljö.', Label: 'sv' ]
[2026-11-15 19:16:09:840] The text classification process has been completed.
```

If you wish to store the outcome to a handy JSON file, you can use the `--saveoutput` option and (optionally) the `--disableindexserialization` option for a shorter output. 

```
.\nwngram.exe classify --labeledexamples .\LabeledExamples.json --textsnippets .\TextSnippets.json --saveoutput --disableindexserialization
```

The produced JSON will look like:

```json
{
  "MinimumAccuracySingleLabel": 0.5,
  "MinimumAccuracyMultipleLabels": 0,
  "Results": [
    {
      "TextSnippet": {
        "Text": "We are looking for several skilled and driven developers to join our team."
      },
      "Label": "en",
      "SimilarityIndexes": [],
      "SimilarityIndexAverages": [
        {
          "Label": "en",
          "Value": 1
        },
        {
          "Label": "sv",
          "Value": 0
        }
      ]
    },
    {
      "TextSnippet": {
        "Text": "Vår kund erbjuder trivsel, arbetsglädje och en trygg arbetsmiljö."
      },
      "Label": "sv",
      "SimilarityIndexes": [],
      "SimilarityIndexAverages": [
        {
          "Label": "en",
          "Value": 0
        },
        {
          "Label": "sv",
          "Value": 1
        }
      ]
    }
  ],
  "Version": "5.0.0"
}
```

If you wish to store the files elsewhere, you can specify a new working folder by using the `--folderpath` option - i.e. `--folderpath:C:\Documents`

If your `LabeledExamples.json` file contains invalid text excerpts, such as:

```json
[
    {
      "Label": "ham",
      "Text": ":)"
    },
    {
      "Label": "ham",
      "Text": ":-) :-)"
    }
  ] 
```

the log of application will end as following:

```
...
[2026-11-15 19:19:12:327] '2' LabeledExample objects have been loaded.
[2026-11-15 19:19:12:333] '2' TextSnippet objects have been loaded.
[2026-11-15 19:19:12:333] The provided 'NGramTokenizerRuleSet' is: '[ DoForMonogram: 'True', DoForBigram: 'True', DoForTrigram: 'True', DoForFourgram: 'True', DoForFivegram: 'True' ]'.
[2026-11-15 19:19:12:378] ERROR: The provided ITokenizationStrategy pattern ('[\w0-9_]{1,}') retuns zero matches against the provided text (':)').
```

All the labeled examples that might prevent the application to run can be ignored by using the `--cleanlabeledexamples` option:

```
...
[2026-11-15 19:22:43:162] '2' LabeledExample objects have been loaded.
[2026-11-15 19:22:43:169] '2' TextSnippet objects have been loaded.
[2026-11-15 19:22:43:169] The provided 'NGramTokenizerRuleSet' is: '[ DoForMonogram: 'True', DoForBigram: 'True', DoForTrigram: 'True', DoForFourgram: 'True', DoForFivegram: 'True' ]'.
[2026-11-15 19:22:43:388] The provided LabeledExample objects have been thru the cleaning process. Now they are: '0'.
[2026-11-15 19:22:43:409] ERROR: 'LabeledExamples' contains zero items.
```

## Markdown Toolset

Suggested toolset to view and edit this Markdown file:

- [Visual Studio Code](https://code.visualstudio.com/)
- [Markdown Preview Enhanced](https://marketplace.visualstudio.com/items?itemName=shd101wyy.markdown-preview-enhanced)
- [Markdown PDF](https://marketplace.visualstudio.com/items?itemName=yzane.markdown-pdf)