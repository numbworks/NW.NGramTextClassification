# NW.NGramTextClassificationScripts
Contact: numbworks@gmail.com

## Revision History

| Date | Author | Description |
|---|---|---|
| 2022-12-26 | numbworks | Created. |

## Introduction

This documentation file is related to the content of the `scripts` folder.

## ngramtchelper

This `Powershell` script will:

1. Load a `text classification` dataset in CSV format;
    - For ex.: "_Spam Text Message Classification_" dataset on Kaggle
2. Convert all its rows to `LabeledExamples` and `TextSnippets`;
3. Save the outcomes as JSON files.

To use this script, please:

1. Create a working folder somewhere on your computer;
2. Store a CSV dataset in it;
3. Open this script in Visual Studio Code or similar code editor;
4. Edit the `Variables` section:

    ```powershell
    # Variables
    [System.IO.DirectoryInfo]$workingFolder = [System.IO.DirectoryInfo]::new("C:\wf-ngramtc")
    [string]$datasetFileName = "dataset.csv"
    [string]$delimiter = ","
    [string]$labelColumnName = "Category"
    [string]$textColumnName = "Message"
    [string]$labeledExamplesFileName = "labeledexamples.json"
    [string]$textSnippetsFileName = "textsnippets.json"
    ```

5. Run the script;
6. Done!

The script will also generate a log that could be useful for debugging purposes:

```
[2022-12-26 12:34] Beginning processing the provided dataset...
[2022-12-26 12:34] Validating the provided variables...
[2022-12-26 12:34] The provided variables have been validated.
[2022-12-26 12:34] WorkingFolder: 'C:\wf-ngramtc'.
[2022-12-26 12:34] DatasetFileName: 'dataset.csv'.
[2022-12-26 12:34] Delimiter: ','.
[2022-12-26 12:34] LabelColumnName: 'Category'.
[2022-12-26 12:34] TextColumnName: 'Message'.
[2022-12-26 12:34] LabeledExamplesFileName: 'labeledexamples.json'.
[2022-12-26 12:34] TextSnippetsFileName: 'textsnippets.json'.      
[2022-12-26 12:34] Loading the provided dataset...
[2022-12-26 12:34] The provided dataset has been loaded.
[2022-12-26 12:34] The provided dataset has been converted to '5572' LabeledExamples and TextSnippets objects.
[2022-12-26 12:34] The Labeled Examples File has been successfully created: 'C:\wf-ngramtc\labeledexamples.json'.
[2022-12-26 12:34] The Text Snippets File has been successfully created: 'C:\wf-ngramtc\textsnippets.json'.
[2022-12-26 12:34] Dataset processing completed.
```


**Note**: this Powershell 7.x script is multi-platform, but it has been tested on Windows only.

## Markdown Toolset

Suggested toolset to view and edit this Markdown file:

- [Visual Studio Code](https://code.visualstudio.com/)
- [Markdown Preview Enhanced](https://marketplace.visualstudio.com/items?itemName=shd101wyy.markdown-preview-enhanced)
- [Markdown PDF](https://marketplace.visualstudio.com/items?itemName=yzane.markdown-pdf)