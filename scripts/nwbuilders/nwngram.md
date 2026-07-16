% nwngram

# NAME
nwngram - sketch download metadata extractor for 'Lo Zoo di 105'

# SYNOPSIS
**nwngram** [command] [options]

# DESCRIPTION
**nwngram** is a CLI application that extracts 'Lo Zoo di 105' sketch download metadata (JSON or MetaLink) from 'Radio 105' website.

# OPTIONS
**-?, -h, --help**
Show help and usage information.

# COMMANDS

### describe
Performs the least possible amount of calls to the root webpage in order to describe the current situation.

**--sketchmanagertype <PodcastItems|ZooScenette>**
    Defines the Sketch Manager to use. Default: 'PodcastItems'.

**--enablesave**
    Enables saving output to disk. Default: 'True'.

**--maxcollections \<maxcollections\>**
    Maximum number of collections to consider. Default: '65535' (ZooScenette) or '800' (PodcastItems).

### fetch
Performs the highest amount of calls to the root webpage in order to create comprehensive MetaLink files to download the sketches afterwards.

**--sketchmanagertype <PodcastItems|ZooScenette>**
    Defines the Sketch Manager to use. Default: 'PodcastItems'.

**--enablesave**
    Enables saving output to disk. Default: 'True'.

**--onlyselectedcollections \<onlyselectedcollections\>**
    Restricts processing to the provided collection names (space-separated). Default: 'null' (ZooScenette) or '["Lo Zoo di 105"]' (PodcastItems).

**--sketchcutoffdate \<sketchcutoffdate\>**
    Includes only sketches newer than this date, formatted as 'yyyy-MM-dd'. Default: 'Today - 2 months' (ZooScenette) or 'Today - 3 days' (PodcastItems).

**--podcastitemspagerange \<podcastitemspagerange\>**
    Range of pages to fetch if PodcastItems, formatted as 'firstpage:lastpage' (e.g., 1:25). Ignored if ZooScenette. Default: 'null' (PodcastItems).

### list
Performs the least possible amount of calls to the root webpage in order to list the most recent sketches available.

**--sketchmanagertype <PodcastItems|ZooScenette>**
    Defines the Sketch Manager to use. Default: 'PodcastItems'.

**--enablesave**
    Enables saving output to disk. Default: 'True'.

**--sketchcutoffdate \<sketchcutoffdate\>**
    Includes only sketches newer than this date, formatted as 'yyyy-MM-dd'. Default: 'Today - 2 months' (ZooScenette) or 'Today - 3 days' (PodcastItems).

# EXAMPLES
**List all the available commands:**

```text
nwngram
```

**List all the available options for each command:**

```text
nwngram describe -h
nwngram fetch -h
nwngram list -h
```

**Run 'describe' with custom options:**

```text
nwngram describe \
    --enablesave false \
    --maxcollections 2

nwngram describe \
    --sketchmanagertype ZooScenette \
    --enablesave false \
    --maxcollections 2
```

**Run 'fetch' with custom options:**

```text
nwngram fetch \
    --enablesave false \
    --podcastitemspagerange 1:10

nwngram fetch \
    --sketchmanagertype ZooScenette \
    --enablesave false \
    --onlyselectedcollections varie \
    --sketchcutoffdate 2026-01-01
```

**Run 'list' with custom options:**

```text
nwngram list \
    --enablesave false \
    --sketchcutoffdate 2026-01-01

nwngram list \
    --sketchmanagertype ZooScenette \
    --enablesave false \
    --sketchcutoffdate 2026-01-01
```

# AUTHOR
numbworks (numbworks@gmail.com)