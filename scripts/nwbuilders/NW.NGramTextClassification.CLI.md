# NW.NGramTextClassification.CLI

`NW.NGramTextClassification.CLI` is a NuGet tool to perform text classification tasks on the text snippets you provide.

## Getting Started

If `/home/nw.ngramtextclassification` is the folder in which the `.nupkg` file is located, you can install it and run it using these commands:

```cmd
dotnet tool install --global NW.NGramTextClassification.CLI --add-source /home/nw.ngramtextclassification
nwngram
```

## CLI Reference

|Command|Sub Command|Options|Exit Codes|
|---|---|---|---|
|session|||Success|
|session|classify|--labeledexamples:{filename}<br />--textsnippets:{filename}<br />*--folderpath:{path}*<br />*--tokenizerruleset:{filename}*<br />*--minaccuracysingle:{number}*<br />*--minaccuracymultiple:{number}*<br />*--savesession*<br />*--cleanlabeledexamples*<br />*--disableindexserialization*|Success<br />Failure|

## Examples

The simplest command you can run is `session classify`, which performs a text classification task on the data you provide. 

The command above requires that you have the two required files (`LabeledExamples.json` and `TextSnippets.json`) located in the same folder as the application, which by default it's the working folder for all the application's activities. 

At very least, the command will look like:

```
nwngram session classify --labeledexamples .\LabeledExamples.json --textsnippets .\TextSnippets.json
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
*********************************************************Version: 4.2.1*****


[2026-07-19 20:28:06:229] Attempting to load a collection of 'LabeledExample' objects from: C:\Users\Rubèn\Desktop\nw.ngramtextclassification\LabeledExamples.json.
[2026-07-19 20:28:06:259] A collection of 'LabeledExample' objects has been successfully loaded.
[2026-07-19 20:28:06:260] Attempting to load a collection of 'TextSnippet' objects from: C:\Users\Rubèn\Desktop\nw.ngramtextclassification\TextSnippets.json.
[2026-07-19 20:28:06:261] A collection of 'TextSnippet' objects has been successfully loaded.
[2026-07-19 20:28:06:263] The provided snippets are: '2'.
[2026-07-19 20:28:06:276] The provided LabeledExample objects have been thru the tokenization process.
[2026-07-19 20:28:06:282] The provided snippet has been tokenized into '65' INGram object.
[2026-07-19 20:28:06:282] The provided snippet has been tokenized into '45' INGram object.
[2026-07-19 20:28:06:282] Comparing the provided snippet against the following TokenizedExample: '[ Label: 'en', Text: 'We are looking for several skilled and driven developers to join our team.', NGrams: '65' ]'...
[2026-07-19 20:28:06:282] Comparing the provided snippet against the following TokenizedExample: '[ Label: 'en', Text: 'We are looking for several skilled and driven developers to join our team.', NGrams: '65' ]'...
[2026-07-19 20:28:06:283] The calculated 'SimilarityIndex' value is '0'.
[2026-07-19 20:28:06:283] The calculated 'SimilarityIndex' value is '1'.
[2026-07-19 20:28:06:283] The rounded 'SimilarityIndex' value is '1'.
[2026-07-19 20:28:06:283] The rounded 'SimilarityIndex' value is '0'.
[2026-07-19 20:28:06:284] The following SimilarityIndex object has been added to the list: '[ Text: 'We are looking for several skilled and driven developers to join our team.', Label: 'en', Value: '0' ]'.
[2026-07-19 20:28:06:284] Comparing the provided snippet against the following TokenizedExample: '[ Label: 'sv', Text: 'Vår kund erbjuder trivsel, arbetsglädje och en trygg arbetsmiljö.', NGrams: '45' ]'...
[2026-07-19 20:28:06:284] The following SimilarityIndex object has been added to the list: '[ Text: 'We are looking for several skilled and driven developers to join our team.', Label: 'en', Value: '1' ]'.
[2026-07-19 20:28:06:284] Comparing the provided snippet against the following TokenizedExample: '[ Label: 'sv', Text: 'Vår kund erbjuder trivsel, arbetsglädje och en trygg arbetsmiljö.', NGrams: '45' ]'...
[2026-07-19 20:28:06:284] The calculated 'SimilarityIndex' value is '1'.
[2026-07-19 20:28:06:284] The calculated 'SimilarityIndex' value is '0'.
[2026-07-19 20:28:06:284] The rounded 'SimilarityIndex' value is '0'.
[2026-07-19 20:28:06:284] The following SimilarityIndex object has been added to the list: '[ Text: 'Vår kund erbjuder trivsel, arbetsglädje och en trygg arbetsmiljö.', Label: 'sv', Value: '0' ]'.
[2026-07-19 20:28:06:284] The tokenized snippet has been successfully compared against the provided list of TokenizedExample objects.
[2026-07-19 20:28:06:284] The rounded 'SimilarityIndex' value is '1'.
[2026-07-19 20:28:06:284] The following SimilarityIndex object has been added to the list: '[ Text: 'Vår kund erbjuder trivsel, arbetsglädje och en trygg arbetsmiljö.', Label: 'sv', Value: '1' ]'.
[2026-07-19 20:28:06:284] The tokenized snippet has been successfully compared against the provided list of TokenizedExample objects.
[2026-07-19 20:28:06:284] '2' SimilarityIndex objects have been computed.
[2026-07-19 20:28:06:284] '2' SimilarityIndex objects have been computed.
[2026-07-19 20:28:06:285] The following unique labels have been found in the provided SimilarityIndex list: '[en, sv]'.
[2026-07-19 20:28:06:285] The following unique labels have been found in the provided SimilarityIndex list: '[en, sv]'.
[2026-07-19 20:28:06:285] Calculating 'SimilarityIndexAverage' for the following label: 'en'...
[2026-07-19 20:28:06:285] Calculating 'SimilarityIndexAverage' for the following label: 'en'...
[2026-07-19 20:28:06:287] The calculated 'SimilarityIndexAverage' value is '1'.
[2026-07-19 20:28:06:287] The calculated 'SimilarityIndexAverage' value is '0'.
[2026-07-19 20:28:06:287] The rounded 'SimilarityIndexAverage' value is '0'.
[2026-07-19 20:28:06:287] The rounded 'SimilarityIndexAverage' value is '1'.
[2026-07-19 20:28:06:287] The following SimilarityIndexAverage object has been added to the list: '[ Label: 'en', Value: '0' ]'.
[2026-07-19 20:28:06:287] The following SimilarityIndexAverage object has been added to the list: '[ Label: 'en', Value: '1' ]'.
[2026-07-19 20:28:06:287] Calculating 'SimilarityIndexAverage' for the following label: 'sv'...
[2026-07-19 20:28:06:287] Calculating 'SimilarityIndexAverage' for the following label: 'sv'...
[2026-07-19 20:28:06:287] The calculated 'SimilarityIndexAverage' value is '0'.
[2026-07-19 20:28:06:287] The calculated 'SimilarityIndexAverage' value is '1'.
[2026-07-19 20:28:06:287] The rounded 'SimilarityIndexAverage' value is '1'.
[2026-07-19 20:28:06:287] The rounded 'SimilarityIndexAverage' value is '0'.
[2026-07-19 20:28:06:287] The following SimilarityIndexAverage object has been added to the list: '[ Label: 'sv', Value: '1' ]'.
[2026-07-19 20:28:06:287] The following SimilarityIndexAverage object has been added to the list: '[ Label: 'sv', Value: '0' ]'.
[2026-07-19 20:28:06:287] '2' SimilarityIndexAverage objects have been computed.
[2026-07-19 20:28:06:287] '2' SimilarityIndexAverage objects have been computed.
[2026-07-19 20:28:06:288] The following verification returned false: 'AreAllIndexAveragesEqualToZero'.
[2026-07-19 20:28:06:288] The following verification returned false: 'AreAllIndexAveragesEqualToZero'.
[2026-07-19 20:28:06:288] The following verification returned false: 'IsSingleLabelAndHigherEqualThanMinimumAccuracy'.
[2026-07-19 20:28:06:288] The following verification returned false: 'IsSingleLabelAndHigherEqualThanMinimumAccuracy'.
[2026-07-19 20:28:06:288] The following verification returned false: 'IsSingleLabelAndLessThanMinimumAccuracy'.
[2026-07-19 20:28:06:288] The following verification returned false: 'IsSingleLabelAndLessThanMinimumAccuracy'.
[2026-07-19 20:28:06:290] The following verification returned false: 'AreAllIndexAveragesSameValue'.
[2026-07-19 20:28:06:290] The following verification returned false: 'AreAllIndexAveragesSameValue'.
[2026-07-19 20:28:06:291] The following verification returned false: 'AreTwoHighestIndexAveragesSameValue'.
[2026-07-19 20:28:06:291] The following verification returned false: 'AreTwoHighestIndexAveragesSameValue'.
[2026-07-19 20:28:06:291] The following verification returned false: 'IsLessThanMinimumAccuracyMultipleLabels'.
[2026-07-19 20:28:06:291] The following verification returned false: 'IsLessThanMinimumAccuracyMultipleLabels'.
[2026-07-19 20:28:06:291] The 'SimilarityIndexAverage' object with the highest value is: '[ Label: 'sv', Value: '1' ]'.
[2026-07-19 20:28:06:291] The 'SimilarityIndexAverage' object with the highest value is: '[ Label: 'en', Value: '1' ]'.
[2026-07-19 20:28:06:291] The result of the classification task is: 'en'.
[2026-07-19 20:28:06:291] The classification task has been successful.
[2026-07-19 20:28:06:291] The result of the classification task is: 'sv'.
[2026-07-19 20:28:06:291] The classification task has been successful.
```

If you wish to store the outcome to a handy JSON file, you can use the `--savesession` option and (optionally) the `--disableindexserialization` option for a shorter output. 

```
nwngram session classify --labeledexamples .\LabeledExamples.json --textsnippets .\TextSnippets.json --savesession --disableindexserialization
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
  "Version": "4.2.1"
}
```

If you wish to store the files elsewhere, you can specify a new working folder by using the `--folderpath` option - i.e. `--folderpath:C:\Documents`

## License
MIT