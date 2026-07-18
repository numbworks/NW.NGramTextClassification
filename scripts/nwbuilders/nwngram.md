% nwngram

# NAME
nwngram - perform text classification tasks on your text snippets

# SYNOPSIS
**nwngram** [command] [options]

# DESCRIPTION
**nwngram** is a CLI application to perform text classification tasks on the text snippets you provide.

# OPTIONS
**-?, -h, --help**
Show help and usage information.

# COMMANDS

### session classify
Performs a text classification task on the data you provide.

**--labeledexamples:{filename}**
    Defines the name of the JSON file containing the pre-labeled training data used to train the classifier. This file must be located in the working folder.

**--textsnippets:{filename}**
    Defines the name of the JSON file containing the raw, unlabeled text strings that you want the application to classify. This file must be located in the working folder.

**--folderpath:{path}**
    Defines the custom path to the working directory where the application will look for input files and save any session outputs, overriding the default application folder.

**--tokenizerruleset:{filename}**
    Defines the name of the JSON file that specifies the rules and configurations for how the text should be broken down into n-grams during tokenization.

**--minaccuracysingle:{number}**
    Defines the minimum accuracy or confidence threshold required to successfully classify a single text snippet.

**--minaccuracymultiple:{number}**
    Defines the minimum average accuracy or confidence threshold required when evaluating multiple text snippets across the session.

**--savesession**
    Defines whether the application should automatically save the results, metrics, and logs of the current classification session to the working folder upon completion.

**--cleanlabeledexamples**
    Defines whether the application should sanitize the labeled examples before running the tokenization process.

**--disableindexserialization**
    Defines whether to skip saving the calculated similarity index data to disk.

# EXAMPLES
**Performs a text classification task on the data you provide:**

```text
./nwngram session classify --labeledexamples:LabeledExamples.json --textsnippets:TextSnippets.json
```


# AUTHOR
numbworks (numbworks@gmail.com)