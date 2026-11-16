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

### scaffold
Creates three example JSON files on disk from which you can start your text classification process.

**--folderpath:{path}**
    Defines the custom path to the working directory where the application will save the three example JSON files, overriding the default application folder.

**--enableutc**
    Replaces local time with UTC time in timestamps.

### classify
Performs a text classification task on the data you provide.

**--labeledexamples:{filename}**
    Defines the name of the JSON file containing the pre-labeled training data used to train the classifier. This file must be located in the working folder.

**--textsnippets:{filename}**
    Defines the name of the JSON file containing the raw, unlabeled text strings that you want the application to classify. This file must be located in the working folder.

**--folderpath:{path}**
    Defines the custom path to the working directory where the application will look for input files and save the output, overriding the default application folder.

**--tokenizerruleset:{filename}**
    Defines the name of the JSON file that specifies the rules and configurations for how the text should be broken down into n-grams during tokenization.

**--minaccuracysingle:{number}**
    Defines the minimum accuracy or confidence threshold required to successfully classify a single text snippet.

**--minaccuracymultiple:{number}**
    Defines the minimum average accuracy or confidence threshold required when evaluating multiple text snippets across the session.

**--saveoutput**
    Defines whether the application should automatically save the results, metrics, and logs of the current classification output to the working folder upon completion.

**--cleanlabeledexamples**
    Defines whether the application should sanitize the labeled examples before running the tokenization process.

**--disableindexserialization**
    Defines whether to skip saving the calculated similarity index data to disk.

**--enableutc**
    Replaces local time with UTC time in timestamps.

# EXAMPLES
**Creates three example JSON files on disk:**

```text
./nwngram scaffold
```

**Performs a text classification task on the data you provide:**

```text
./nwngram classify --labeledexamples:LabeledExamples.json --textsnippets:TextSnippets.json
```

# AUTHOR
numbworks (numbworks@gmail.com)