# knownissues-csharp
Contact: numbworks@gmail.com

## Revision History

| Date | Author | Description |
|---|---|---|
| 2026-05-18 | numbworks | Created. |
| 2026-10-26 | numbworks | Last update. |

## Introduction

This documents collects all the known issues related to some or all the `NW.*` applications written in C#.

## Known Issues: `Resources.resx` and cross-platform VSCode

Despite the devcontainer being exactly the same, in order for the project to run on both VSCode for Windows and Linux, the `Resources.resx` file needs to have the exact case in the file paths - e.g.:

```xml
...
<data name="ShortLabeledExamplesAsJson" type="System.Resources.ResXFileRef, System.Windows.Forms">
  <value>..\Resources\ShortLabeledExamplesAsJson.txt;System.String, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089;utf-8</value>
</data>
...
```

This happens because Windows (NTFS) is case-insensitive, while Linux (ext4) is strictly case-sensitive, therefore the `Resources.resx` must contain the exact physical case of the files and folders on your disk to work on both platforms.

## Markdown Toolset

Suggested toolset to view and edit this Markdown file:

- [Visual Studio Code](https://code.visualstudio.com/)
- [Markdown Preview Enhanced](https://marketplace.visualstudio.com/items?itemName=shd101wyy.markdown-preview-enhanced)
- [Markdown PDF](https://marketplace.visualstudio.com/items?itemName=yzane.markdown-pdf)