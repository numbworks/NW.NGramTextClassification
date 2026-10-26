# nwmakefiles-csharp
Contact: numbworks@gmail.com

## Revision History

| Date | Author | Description |
|---|---|---|
| 2026-07-27 | numbworks | Created. |
| 2026-07-27 | numbworks | Last update. |

## Introduction

`nwmakefiles` is a collection of reusable files and processes that streamline pre-release checks and utility actions.

## Project Structure And File Roles

An example of the project structure:

```
...
.vscode/
    ├── ...
    └── tasks.json
...
scripts/
├── nwmakefiles/
    └── nwxmldocchecking.csx
└── ...
src/
├── NW.NGramTextClassification
    ├── ...
    ├── NW.NGramTextClassification.csproj
    └── ...
└── NW.NGramTextClassification.CLI
    ├── ...
    ├── NW.NGramTextClassification.CLI.csproj
    └── Program.cs
...
```

|File|Role|
|---|---|
|`tasks.json`|Contains all the tasks available via VSCode's `Run Task`.|
|`nwxmldocchecking.csx`|Dotnet script that lists all the public members that lacks of XML Docs.|

## The Tasks File

An example of `tasks.json`:

```json
{
    "version": "2.0.0",
    "tasks": [

        /* ======= WHOLE SOLUTION ======= */

        {
            "label": "solution: build",
            "command": "dotnet",
            "type": "process",
            "args": [
                "build",
                "${workspaceFolder}/NW.NGramTextClassification.sln",
                "/property:GenerateFullPaths=true",
                "/consoleloggerparameters:NoSummary;ForceNoAlign"
            ],
            "problemMatcher": "$msCompile"
        },
        {
            "label": "solution: calculate-commitavg",
            "type": "shell",
            "command": "clear && nwcavg",
            "args": [],
            "options": {
                "cwd": "${workspaceFolder}"
            },
            "problemMatcher": []
        },
        {
            "label": "solution: docstrings-verbose",
            "command": "dotnet",
            "type": "process",
            "args": [
                "script",
                "${workspaceFolder}/scripts/nwmakefiles/nwxmldocchecking.csx",
                "${workspaceFolder}/NW.NGramTextClassification.sln"
            ],
            "problemMatcher": []
        },
        {
            "label": "solution: pack and publish",
            "type": "shell",
            "command": "echo \"Executing all pack and publish tasks...\"",
            "dependsOn": [
                "library: pack",
                "cli: pack",
                "cli: publish"
            ],
            "dependsOrder": "sequence",
            "problemMatcher": []
        },

        /* ======= LIBRARY ======= */

        {
            "label": "library: coverage-verbose",
            "type": "shell",
            "command": "bash",
            "args": [
                "-lc",
                "export CSPROJ=\"tests/NW.NGramTextClassification.UnitTests/NW.NGramTextClassification.UnitTests.csproj\"; export FLT=\"NW.NGramTextClassification\"; set -euo pipefail; resultsdir=\"$(mktemp -d)\"; reportdir=\"$(mktemp -d)\"; cleanup(){ rm -rf \"$resultsdir\" \"$reportdir\"; }; trap cleanup EXIT; dotnet test \"$CSPROJ\" --collect:\"XPlat Code Coverage\" --results-directory \"$resultsdir\" && reportgenerator -reports:\"$resultsdir/**/coverage.cobertura.xml\" -targetdir:\"$reportdir\" -reporttypes:TextSummary -assemblyfilters:+\"$FLT\" && cat \"$reportdir/Summary.txt\""
            ],
            "options": {
                "cwd": "${workspaceFolder}"
            },
            "problemMatcher": [],
            "presentation": {
                "reveal": "always",
                "panel": "dedicated",
                "clear": true
            }
        },
        {
            "label": "library: pack",
            "type": "shell",
            "command": "dotnet pack -c Release --output /home/nw.ngramtextclassification/",
            "options": {
                "cwd": "${workspaceFolder}/src/NW.NGramTextClassification"
            },
            "problemMatcher": "$msCompile"
        },
        {
            "label": "library: update-codecoverage",
            "type": "shell",
            "command": "bash",
            "args": [
                "-lc",
                "export CSPROJ=\"tests/NW.NGramTextClassification.UnitTests/NW.NGramTextClassification.UnitTests.csproj\"; export FLT=\"NW.NGramTextClassification\"; export SUFFIX=\"NW.NGramTextClassification\"; set -euo pipefail; resultsdir=\"$(mktemp -d)\"; reportdir=\"$(mktemp -d)\"; cleanup(){ rm -rf \"$resultsdir\" \"$reportdir\"; }; trap cleanup EXIT; dotnet test \"$CSPROJ\" --collect:\"XPlat Code Coverage\" --results-directory \"$resultsdir\" >/dev/null 2>&1 && reportgenerator -reports:\"$resultsdir/**/coverage.cobertura.xml\" -targetdir:\"$reportdir\" -reporttypes:TextSummary -assemblyfilters:+\"$FLT\" >/dev/null 2>&1; value=$(grep -oP \"Line coverage:\\s+\\K\\d+(\\.\\d+)?(?=%)\" \"$reportdir/Summary.txt\" || true); if [ -z \"$value\" ]; then value=\"0\"; fi; int_value=$(echo \"$value\" | cut -d. -f1); if [ \"$int_value\" -le 39 ]; then color=\"red\"; elif [ \"$int_value\" -le 69 ]; then color=\"orange\"; else color=\"green\"; fi; url=\"https://img.shields.io/badge/coverage-${value}%25-${color}\"; echo \"$url\" > \"./codecoverage-${SUFFIX}.txt\"; curl -s \"$url\" -o \"./codecoverage-${SUFFIX}.svg\" >/dev/null 2>&1 && echo \"coverage-${SUFFIX} updated successfully!\" || echo \"coverage-${SUFFIX} failed to update coverage badge.\""
            ],
            "options": {
                "cwd": "${workspaceFolder}"
            },
            "problemMatcher": [],
            "presentation": {
                "reveal": "always",
                "panel": "dedicated",
                "clear": true
            }
        },

        /* ======= CLI ======= */

        {
            "label": "cli: coverage-verbose",
            "type": "shell",
            "command": "bash",
            "args": [
                "-lc",
                "export CSPROJ=\"tests/NW.NGramTextClassification.CLI.UnitTests/NW.NGramTextClassification.CLI.UnitTests.csproj\"; export FLT=\"NW.NGramTextClassification.CLI\"; set -euo pipefail; resultsdir=\"$(mktemp -d)\"; reportdir=\"$(mktemp -d)\"; cleanup(){ rm -rf \"$resultsdir\" \"$reportdir\"; }; trap cleanup EXIT; dotnet test \"$CSPROJ\" --collect:\"XPlat Code Coverage\" --results-directory \"$resultsdir\" && reportgenerator -reports:\"$resultsdir/**/coverage.cobertura.xml\" -targetdir:\"$reportdir\" -reporttypes:TextSummary -assemblyfilters:+\"$FLT\" && cat \"$reportdir/Summary.txt\""
            ],
            "options": {
                "cwd": "${workspaceFolder}"
            },
            "problemMatcher": [],
            "presentation": {
                "reveal": "always",
                "panel": "dedicated",
                "clear": true
            }
        },
        {
            "label": "cli: pack",
            "type": "shell",
            "command": "dotnet pack -c Release --output /home/nw.ngramtextclassification/",
            "options": {
                "cwd": "${workspaceFolder}/src/NW.NGramTextClassification.CLI"
            },
            "problemMatcher": "$msCompile"
        },
        {
            "label": "cli: publish",
            "type": "shell",
            "command": "for RID in linux-arm64 win-x64 linux-x64; do dotnet publish /p:PublishSingleFile=true /p:PublishTrimmed=false /p:DebugType=None --self-contained true -c Release -r $RID --output /home/nw.ngramtextclassification/temp/; done",
            "options": {
                "cwd": "${workspaceFolder}/src/NW.NGramTextClassification.CLI"
            },
            "problemMatcher": "$msCompile"
        },
        {
            "label": "cli: update-codecoverage",
            "type": "shell",
            "command": "bash",
            "args": [
                "-lc",
                "export CSPROJ=\"tests/NW.NGramTextClassification.CLI.UnitTests/NW.NGramTextClassification.CLI.UnitTests.csproj\"; export FLT=\"NW.NGramTextClassification.CLI\"; export SUFFIX=\"NW.NGramTextClassification.cli\"; set -euo pipefail; resultsdir=\"$(mktemp -d)\"; reportdir=\"$(mktemp -d)\"; cleanup(){ rm -rf \"$resultsdir\" \"$reportdir\"; }; trap cleanup EXIT; dotnet test \"$CSPROJ\" --collect:\"XPlat Code Coverage\" --results-directory \"$resultsdir\" >/dev/null 2>&1 && reportgenerator -reports:\"$resultsdir/**/coverage.cobertura.xml\" -targetdir:\"$reportdir\" -reporttypes:TextSummary -assemblyfilters:+\"$FLT\" >/dev/null 2>&1; value=$(grep -oP \"Line coverage:\\s+\\K\\d+(\\.\\d+)?(?=%)\" \"$reportdir/Summary.txt\" || true); if [ -z \"$value\" ]; then value=\"0\"; fi; int_value=$(echo \"$value\" | cut -d. -f1); if [ \"$int_value\" -le 39 ]; then color=\"red\"; elif [ \"$int_value\" -le 69 ]; then color=\"orange\"; else color=\"green\"; fi; url=\"https://img.shields.io/badge/coverage-${value}%25-${color}\"; echo \"$url\" > \"./codecoverage-${SUFFIX}.txt\"; curl -s \"$url\" -o \"./codecoverage-${SUFFIX}.svg\" >/dev/null 2>&1 && echo \"coverage-${SUFFIX} updated successfully!\" || echo \"coverage-${SUFFIX} failed to update coverage badge.\""
            ],
            "options": {
                "cwd": "${workspaceFolder}"
            },
            "problemMatcher": [],
            "presentation": {
                "reveal": "always",
                "panel": "dedicated",
                "clear": true
            }
        }
        
    ]
}
```

To implement it:

1. Open the solution's folder in Visual Studio Code.
2. Update the example above according to your application and save it as `.vscode/tasks.json`.
3. Click on `Terminal` > `Run Task`  and select the task you want to run.
4. Done!

## Notes: `nwxmldocchecking.csx`

This script requires `dotnet-script` to be installed in the devcontainer before it can run:

```
dotnet tool install -g dotnet-script
dotnet script ./scripts/nwmakefiles/nwdocstringchecking.csx ./NW.NGramTextClassification.sln
```

This script has been develop to overcome the limitations of the equivalent `dotnet build` command:

```
dotnet build --no-incremental -p:NoWarn=1701%3B1702 2>&1 | grep 'CS1591'
```

## Source

The following project is used for the examples in this document:

- [NW.NGramTextClassification](https://github.com/numbworks/NW.NGramTextClassification)

## Markdown Toolset

Suggested toolset to view and edit this Markdown file:

- [Visual Studio Code](https://code.visualstudio.com/)
- [Markdown Preview Enhanced](https://marketplace.visualstudio.com/items?itemName=shd101wyy.markdown-preview-enhanced)
- [Markdown PDF](https://marketplace.visualstudio.com/items?itemName=yzane.markdown-pdf)