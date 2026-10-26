# nwpublishers-csharp
Contact: numbworks@gmail.com

## Revision History

| Date | Author | Description |
|---|---|---|
| 2026-09-29 | numbworks | Created. |
| 2026-09-29 | numbworks | Last update. |

## Introduction

This guide collects all the information required to publish a C# package or tool using GitHub Actions and a NuGet API key stored in the GitHub repository.

## Pre-Requisites

Before publishing your C# application via GitHub Actions, make sure that you have:

1. Configured your GitHub repository secrets with a valid NuGet API key named `NUGET_API_KEY`;
2. Set up your workflow file at `{root}/.github/workflows/dotnet.yml`.

## Getting Started

1. Launch Visual Studio Code;
2. Click on <ins>File</ins> > <ins>Open folder</ins> > `<project_name>`;
3. Create `{root}/.github/workflows/dotnet.yml` out of the following template and replace `NW.ProjectName` with your solution name:

    ```yaml
    name: .NET

    on:
      push:
        branches: [ master ]
      pull_request:
        branches: [ master ]

    jobs:
      build:
        runs-on: windows-latest
        steps:
        - uses: actions/checkout@v2
          
        - name: Install dotnet SDK 8.x
          uses: actions/setup-dotnet@v1
          with:
            dotnet-version: 8.x

        - name: Display installed SDKs
          run:  dotnet --info
          
        - name: Restore dependencies
          run: dotnet restore -s https://api.nuget.org/v3/index.json --verbosity n
          
        - name: Build
          working-directory: ./
          run: dotnet build --no-restore
          
        - name: Run unit tests
          working-directory: ./
          run: dotnet test --no-build --verbosity normal
          
        - name: Find packages
          run: Get-ChildItem -Recurse -Filter *.nupkg | Select-Object FullName

        - name: Pack library
          working-directory: ./src/NW.ProjectName
          run: dotnet pack --configuration Release --no-build --output bin/Release

        - name: Publish library to NuGet.org
          run: Get-ChildItem ./src/NW.ProjectName/bin/Release/*.nupkg | ForEach-Object { dotnet nuget push $_.FullName --source https://api.nuget.org/v3/index.json --api-key ${{ secrets.NUGET_API_KEY }} --skip-duplicate }

        - name: Pack tool
          working-directory: ./src/NW.ProjectName.CLI
          run: dotnet pack --configuration Release --no-build

        - name: Publish tool to NuGet.org
          run: Get-ChildItem ./src/NW.ProjectName.CLI/nupkg/*.nupkg | ForEach-Object { dotnet nuget push $_.FullName --source https://api.nuget.org/v3/index.json --api-key ${{ secrets.NUGET_API_KEY }} --skip-duplicate }
    ```

4. Once you push your changes to the `master` branch, GitHub Actions will automatically restore dependencies, build the solution, run unit tests, pack the target libraries/tools, and publish them to NuGet.org using the stored API key;
5. Done!

Regarding package versions, please ensure your project file version is properly incremented before pushing, as NuGet does not allow overwriting an existing version package if it has been previously uploaded.

## How-To Verify A Package

You are able to verify that your published NuGet package installs or runs as expected by installing it directly from NuGet.org using the dotnet CLI in your terminal:

```bash
dotnet add package {package_name}
```

Or if it is a .NET tool:

```bash
dotnet tool install --global {tool_name}
```

## Markdown Toolset

Suggested toolset to view and edit this Markdown file:

- [Visual Studio Code](https://code.visualstudio.com/)
- [Markdown Preview Enhanced](https://marketplace.visualstudio.com/items?itemName=shd101wyy.markdown-preview-enhanced)
- [Markdown PDF](https://marketplace.visualstudio.com/items?itemName=yzane.markdown-pdf)