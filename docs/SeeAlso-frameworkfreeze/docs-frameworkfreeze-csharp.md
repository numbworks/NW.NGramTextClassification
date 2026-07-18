# frameworkfreeze-csharp
Contact: numbworks@gmail.com

## Revision History

| Date | Author | Description |
|---|---|---|
| 2026-05-14 | numbworks | Created. |
| 2026-06-28 | numbworks | Last update. |
                                   
## Introduction

A "framework freeze" is a strategy that advocates the usage of the same version of frameworks and dependencies among several projects by creating a reference document. 

The main scope of this strategy is to simplify planned updates by reducing the possible issues and by centralizing their resolution. 

This documents collects all the information regarding the "framework freeze" strategy adopted by all the `NW.*` applications.

# The Current Strategy

At the moment of writing, all `NW.*` applications target **.NET 8 LTS**:

```
...
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>disable</ImplicitUsings>
    <Nullable>disable</Nullable>
  </PropertyGroup>
...
```

# The Upcoming Strategy

To maximize stability and minimize maintenance effort, all `NW.*` applications will target only LTS (Long-Term Support) releases. LTS releases receive three years of security updates, bug fixes and official support.

Below the .NET roadmap:

| .NET Version | Release | Support Type | End of Support | Adoption |
| :--- | :--- | :--- | :--- | :--- |
| *12* | *Nov 2027* | *LTS* | *Nov 2030* | *Next Target?* |
| *11* | *Nov 2026* | *STS* | *May 2028* | *Skipped* |
| *10* | *Nov 2025* | *LTS* | *Nov 2028* | *Next Target?* |
| *9* | *Nov 2024* | *STS* | *May 2026* | *Skipped* |
| <u>8</u> | <u>Nov 2023</u> | <u>LTS</u> | <u>Nov 2026</u> | <u>Current</u> |

## Markdown Toolset

Suggested toolset to view and edit this Markdown file:

- [Visual Studio Code](https://code.visualstudio.com/)
- [Markdown Preview Enhanced](https://marketplace.visualstudio.com/items?itemName=shd101wyy.markdown-preview-enhanced)
- [Markdown PDF](https://marketplace.visualstudio.com/items?itemName=yzane.markdown-pdf)