# nwbuilders-csharp
Contact: numbworks@gmail.com

## Revision History

| Date | Author | Description |
|---|---|---|
| 2026-03-10 | numbworks | Created. |
| 2026-07-27 | numbworks | Last update. |

## Introduction

`nwbuilders` is a collection of guidelines and configuration templates that simplify and standardize the cross-compilation and packaging workflow for CLI applications on local build agents.

## Target Architectures

We target the following operating systems and architectures:

- Debian Linux installers (AMD64 and ARM64)
- Generic Linux executables (AMD64 and ARM64)
- Windows executables (AMD64)

**Disclaimer**: as an open‑source developer, I intentionally and firmly chose not to target Apple's MacOS.

The reasons are:

- <u>Paid certification</u>: distributing binaries requires an annual developer license, which is incompatible with free software distribution.
- <u>Expensive hardware</u>: building and testing MacOS apps requires proprietary Apple hardware with a high entry cost and a closed ecosystem.
- <u>Lack of open‑source investment</u>: Apple is not among the companies that actively support open‑source projects with funding or engineering resources.
- <u>No local cross‑compilation</u>: Python applications cannot be cross‑compiled for MacOS from Linux or Windows, breaking my unified build workflow.

If you are a MacOS user, you can still attempt to compile or run my applications from the source code.

## Application Packaging Tools

The `dotnet publish` command supports all of the targetted architectures and operating systems via cross-compilation, therefore it's a smooth sailing.

## Project Structure And File Roles

An example of the project structure:

```
...
scripts/
├── nwbuilders
    ├── numbworks_logo_256x256px.ico
    ├── numbworks_logo_256x256px.png
    ├── NW.NGramTextClassification.CLI.md
    ├── NW.NGramTextClassification.md
    └── nwngram.md
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
.gitattributes
...
Directory.Build.props
...
```

|File|Role|
|---|---|
|`numbworks_logo_256x256px.ico`|This is the icon file for the Linux executable.|
|`numbworks_logo_256x256px.png`|This is the icon file for the Windows executable.|
|`NW.NGramTextClassification.CLI.md`|This is the source file for the NuGet tool's documentation file.|
|`NW.NGramTextClassification.md`|This is the source file for the NuGet package's documentation file.|
|`nwngram.md`|This is the source file for the `man` entry installed with the Debian packages.|
|`NW.NGramTextClassification.csproj`|This contains the building instructions for the NuGet package.|
|`NW.NGramTextClassification.CLI.csproj`|This contains the building instructions for the NuGet tool and for the CLI executable.|
|`.gitattributes`|This enforces LF end‑of‑line formatting for Linux‑sensitive files (such as `*.sh` and `man` source files).|
|`Directory.Build.props`|This contains the key information about the application (name, version, description).|

## The GitAttributes File

The `.gitattributes` file must include the following lines (replace the `md` file as needed):

```
...
# Force LF for EOL‑sensitive files (CRLF breaks them on Linux)
nwngram.md text eol=lf
```

If these rules were added after the corresponding files were already created, open the devcontainer terminal and apply the normalization:

```bash
git add --renormalize .
git commit -m "Apply EOL rules"
```

To verify that the rules are now applied correctly:

```bash
git ls-files --eol
```

## The Host Machine

For this build pipeline, a separate host machine is not required. The `dotnet publish` command runs flawlessy in the same Linux devcontainer used for application development (AMD64). Once the build pipeline has been triggered, the resulting artifacts are saved to a folder on the development machine's hard drive via a mount point.

To trigger the build pipeline:

1. Open the solution's folder in Visual Studio Code
2. Add the following tasks to `.vscode/tasks.json`:

```json
{
    "version": "2.0.0",
    "tasks": [
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
        }
        
    ]
}
```

3. Click on `Terminal` > `Run Task` > `solution: pack and publish`
4. Done!

## The Artifacts

An example of the expected artifacts generated by the build pipeline:

|Artifact|Description|
|---|---|
|`nwngram-v4.2.1-linux-amd64.deb`|Debian installer for AMD64.|
|`nwngram-v4.2.1-linux-amd64.zip`|Generic Linux executable for AMD64.|
|`nwngram-v4.2.1-linux-arm64.deb`|Debian installer for ARM64.|
|`nwngram-v4.2.1-linux-arm64.zip`|Generic Linux executable for ARM64.|
|`nwngram-v4.2.1-win64.zip`|Windows executable for AMD64.|
|`NW.NGramTextClassification.4.2.1.nupkg`|NuGet package.|
|`NW.NGramTextClassification.CLI.4.2.1.nupkg`|NuGet tool.|

Additional information:

1. Each ZIP package contains only the executable. The Windows variant will be supplemented with an application icon and embedded metadata.
2. The DEB packages, once installed, will provide the executable file, the menu item with the icon and a `man` page.

## The Building Instructions

The building instructions are encapsulated in the following files. The version number and the referenced documentation files need to be updated before each new release.

`Directory.Build.props`:

```xml
<Project>

  <PropertyGroup>
    <AssemblyVersion>4.2.1</AssemblyVersion>
    <FileVersion>4.2.1</FileVersion>
    <Version>4.2.1</Version>
    <InformationalVersion>4.2.1</InformationalVersion>
    <GenerateAssemblyInfo>true</GenerateAssemblyInfo>
    <Deterministic>true</Deterministic>
  </PropertyGroup>

  <PropertyGroup>
    <BaseIntermediateOutputPath>/workspaces/.dotnet-obj/$(MSBuildProjectName)/</BaseIntermediateOutputPath>
    <IntermediateOutputPath>$(BaseIntermediateOutputPath)obj/</IntermediateOutputPath>
    <MSBuildProjectExtensionsPath>$(BaseIntermediateOutputPath)msbuild/</MSBuildProjectExtensionsPath>
    <RestoreBaseIntermediateOutputPath>$(BaseIntermediateOutputPath)</RestoreBaseIntermediateOutputPath>
    <RestoreIntermediateOutputPath>$(BaseIntermediateOutputPath)restore/</RestoreIntermediateOutputPath>

    <BaseOutputPath>/workspaces/.dotnet-bin/$(MSBuildProjectName)/</BaseOutputPath>
    <OutputPath>$(BaseOutputPath)bin/</OutputPath>
  </PropertyGroup>

  <PropertyGroup>
    <LangVersion>12.0</LangVersion>
  </PropertyGroup>

</Project>
```

`NW.NGramTextClassification.csproj`:

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <ItemGroup>
    <PackageReference Include="nw.shared.files" Version="1.0.0" />
    <PackageReference Include="nw.shared.serialization" Version="1.0.0" />
    <PackageReference Include="nw.shared.validation" Version="1.0.0" />
  </ItemGroup>

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>disable</ImplicitUsings>
    <Nullable>disable</Nullable>
    <GenerateDocumentationFile>false</GenerateDocumentationFile>
    <NoWarn>$(NoWarn);SYSLIB0014;1701;1702;1591</NoWarn>
  </PropertyGroup>

  <ItemGroup>
    <None Include="../../scripts/nwbuilders/numbworks_logo_256x256px.png" Pack="true" PackagePath="\" />
    <None Include="../../scripts/nwbuilders/NW.NGramTextClassification.md" Pack="true" PackagePath="\" />
  </ItemGroup>

  <PropertyGroup>
    <Version>4.2.1</Version>
    <AssemblyVersion>4.2.1.0</AssemblyVersion>
    <FileVersion>4.2.1.0</FileVersion>
    <Authors>numbworks</Authors>
    <Copyright>numbworks 2026</Copyright>
    <Description>NW.NGramTextClassification is a library to perform Text Classification on the string of text you provide. Text Classification is a machine learning technique that calculates the similarity between the string of text you need to categorize and a collection of already categorized strings you provide to the library.</Description>
    <IsPackable>true</IsPackable>
    <PackageRequireLicenseAcceptance>true</PackageRequireLicenseAcceptance>
    <PackageLicenseExpression>MIT</PackageLicenseExpression>
    <PackageIcon>numbworks_logo_256x256px.png</PackageIcon>
    <PackageReadmeFile>NW.NGramTextClassification.md</PackageReadmeFile>
  </PropertyGroup>

  <PropertyGroup>
    <PackageProjectUrl>https://github.com/numbworks/NW.NGramTextClassification</PackageProjectUrl>
    <RepositoryUrl>https://github.com/numbworks/NW.NGramTextClassification</RepositoryUrl>
    <RepositoryType>git</RepositoryType>
    <PackageTags>library csharp machine learning text classification ngrams</PackageTags>
  </PropertyGroup>

</Project>
```

`NW.NGramTextClassification.CLI.csproj`:

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <ItemGroup>
    <ProjectReference Include="..\NW.NGramTextClassification\NW.NGramTextClassification.csproj" />
  </ItemGroup>

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>disable</ImplicitUsings>
    <Nullable>disable</Nullable>
    <GenerateDocumentationFile>false</GenerateDocumentationFile>
    <NoWarn>$(NoWarn);SYSLIB0014;1701;1702;1591</NoWarn>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="McMaster.Extensions.CommandLineUtils" Version="4.1.0" />
    <PackageReference Include="nw.shared.serialization" Version="1.0.0" />
    <PackageReference Include="nw.shared.validation" Version="1.0.0" />
  </ItemGroup>

  <PropertyGroup>
    <AssemblyName>NW.NGramTextClassification.CLI</AssemblyName>
    <ExecutableName>nwngram</ExecutableName>
    <Version>4.2.1</Version>
    <IncludeSourceRevisionInInformationalVersion>false</IncludeSourceRevisionInInformationalVersion>
    <Authors>numbworks</Authors>
    <Company>numbworks</Company>
    <Product>NW.NGramTextClassification</Product>
    <Copyright>numbworks</Copyright>
    <ApplicationIcon>$(MSBuildThisFileDirectory)../../scripts/nwbuilders/numbworks_logo_256x256px.ico</ApplicationIcon>
    <Description>nwngram is a CLI application to perform Text Classification on the string of text you provide. Text Classification is a machine learning technique that calculates the similarity between the string of text you need to categorize and a collection of already categorized strings you provide to the library.</Description>
    <PackageRequireLicenseAcceptance>false</PackageRequireLicenseAcceptance>
    <PackageLicenseExpression>MIT</PackageLicenseExpression>  
  </PropertyGroup>

  <Target Name="RenamePublishedExecutable" AfterTargets="Publish">
    <Exec Condition="Exists('$(PublishDir)NW.NGramTextClassification.CLI')" 
          Command="mv &quot;$(PublishDir)NW.NGramTextClassification.CLI&quot; &quot;$(PublishDir)$(ExecutableName)&quot;" />

    <Exec Condition="Exists('$(PublishDir)NW.NGramTextClassification.CLI.exe')" 
          Command="mv &quot;$(PublishDir)NW.NGramTextClassification.CLI.exe&quot; &quot;$(PublishDir)$(ExecutableName).exe&quot;" />
  </Target>

  <Target Name="ZipCustomOutput" AfterTargets="Publish">
    <PropertyGroup>
      <ExecutableExtension Condition="$(RuntimeIdentifier.StartsWith('win'))">.exe</ExecutableExtension>
      <ExecutableExtension Condition="!$(RuntimeIdentifier.StartsWith('win'))"></ExecutableExtension>

      <BinaryName>$(ExecutableName)$(ExecutableExtension)</BinaryName>

      <ZipFileName Condition="'$(RuntimeIdentifier)' == 'linux-x64'">$(ExecutableName)-v$(Version)-linux-amd64.zip</ZipFileName>
      <ZipFileName Condition="'$(RuntimeIdentifier)' == 'linux-arm64'">$(ExecutableName)-v$(Version)-linux-arm64.zip</ZipFileName>
      <ZipFileName Condition="'$(RuntimeIdentifier)' == 'win-x64'">$(ExecutableName)-v$(Version)-win64.zip</ZipFileName>
      <ZipFileName Condition="'$(ZipFileName)' == ''">$(ExecutableName)-v$(Version)-$(RuntimeIdentifier).zip</ZipFileName>
      
      <SourceFilePath>$(PublishDir)$(BinaryName)</SourceFilePath>
      <FinalZipPath>$(PublishDir)..\$(ZipFileName)</FinalZipPath>
    </PropertyGroup>

    <Error Condition="!Exists('$(SourceFilePath)')" Text="Executable not found at $(SourceFilePath)" />

    <ZipDirectory 
      SourceDirectory="$(PublishDir)" 
      DestinationFile="$(FinalZipPath)" 
      Overwrite="true" />

    <RemoveDir Directories="$(PublishDir)" />
    
    <Message Importance="high" Text="Zipped package successfully created: $(FinalZipPath)" />
  </Target>

  <Target Name="CreateDebianPackage" AfterTargets="ZipCustomOutput" Condition="!$(RuntimeIdentifier.StartsWith('win'))">
    
    <PropertyGroup>
      <ZipFileDir>/home/nw.ngramtextclassification/</ZipFileDir>

      <ZipFileRuntimeIdentifier>$(RuntimeIdentifier)</ZipFileRuntimeIdentifier>
      <ZipFileRuntimeIdentifier Condition="'$(RuntimeIdentifier)' == 'linux-x64'">linux-amd64</ZipFileRuntimeIdentifier>
      
      <ZipFileName>$(ExecutableName)-v$(Version)-$(ZipFileRuntimeIdentifier).zip</ZipFileName>
      <SourceZipPath>$(ZipFileDir)$(ZipFileName)</SourceZipPath>

      <MdSourcePath>$(MSBuildProjectDirectory)/../../scripts/nwbuilders/$(ExecutableName).md</MdSourcePath>
      <TempManPath>/tmp/$(ExecutableName).1</TempManPath>
      
      <DebArch Condition="'$(RuntimeIdentifier)' == 'linux-x64'">amd64</DebArch>
      <DebArch Condition="'$(RuntimeIdentifier)' == 'linux-arm64'">arm64</DebArch>
      <DebArch Condition="'$(DebArch)' == ''">all</DebArch>

      <DebLayoutDir>/tmp/$(ExecutableName)-$(DebArch)-layout</DebLayoutDir>
      <PackageFileName>$(ExecutableName)-v$(Version)-linux-$(DebArch).deb</PackageFileName>
    </PropertyGroup>

    <Exec Command="go-md2man -in &quot;$(MdSourcePath)&quot; -out &quot;$(TempManPath)&quot;" />

    <RemoveDir Directories="$(DebLayoutDir)" />
    <MakeDir Directories="$(DebLayoutDir)/DEBIAN" />
    <MakeDir Directories="$(DebLayoutDir)/usr/bin" />
    <MakeDir Directories="$(DebLayoutDir)/usr/share/man/man1" />
    
    <PropertyGroup>
      <UnzipTemp>/tmp/unzip-$(DebArch)</UnzipTemp>
    </PropertyGroup>

    <RemoveDir Directories="$(UnzipTemp)" />
    <MakeDir Directories="$(UnzipTemp)" />
    <Exec Command="unzip -q &quot;$(SourceZipPath)&quot; -d &quot;$(UnzipTemp)&quot;" />

    <Exec Command="if [ -f &quot;$(UnzipTemp)/$(ExecutableName)&quot; ]; then cp &quot;$(UnzipTemp)/$(ExecutableName)&quot; &quot;$(DebLayoutDir)/usr/bin/&quot;; else cp &quot;$(UnzipTemp)/$(ExecutableName).dll&quot; &quot;$(DebLayoutDir)/usr/bin/$(ExecutableName)&quot;; fi" />
    <Exec Command="chmod 755 &quot;$(DebLayoutDir)/usr/bin/$(ExecutableName)&quot;" />

    <Exec Command="cp &quot;$(TempManPath)&quot; &quot;$(DebLayoutDir)/usr/share/man/man1/$(ExecutableName).1&quot;" />
    <Exec Command="gzip -n9 &quot;$(DebLayoutDir)/usr/share/man/man1/$(ExecutableName).1&quot;" />

    <MakeDir Directories="$(DebLayoutDir)/usr/share/applications" />
    <MakeDir Directories="$(DebLayoutDir)/usr/share/pixmaps" />

    <Exec Command="cp &quot;$(MSBuildProjectDirectory)/../../scripts/nwbuilders/numbworks_logo_256x256px.png&quot; &quot;$(DebLayoutDir)/usr/share/pixmaps/$(ExecutableName).png&quot;" />

    <ItemGroup>
      <DesktopLines Include="[Desktop Entry]" />
      <DesktopLines Include="Name=$(ExecutableName)" />
      <DesktopLines Include="Exec=/bin/bash -c &quot;/usr/bin/$(ExecutableName)%3B exec bash&quot;" />
      <DesktopLines Include="Icon=$(ExecutableName)" />
      <DesktopLines Include="Type=Application" />
      <DesktopLines Include="Categories=Utility;" />
      <DesktopLines Include="Terminal=true" />
    </ItemGroup>

    <WriteLinesToFile 
        File="$(DebLayoutDir)/usr/share/applications/$(ExecutableName).desktop" 
        Lines="@(DesktopLines)" 
        Overwrite="true" />

    <Exec Command="chmod 644 &quot;$(DebLayoutDir)/usr/share/applications/$(ExecutableName).desktop&quot;" />
    <Exec Command="chmod 644 &quot;$(DebLayoutDir)/usr/share/pixmaps/$(ExecutableName).png&quot;" />

    <ItemGroup>
      <ControlLines Include="Package: $(ExecutableName)" />
      <ControlLines Include="Version: $(Version)" />
      <ControlLines Include="Section: utils" />
      <ControlLines Include="Priority: optional" />
      <ControlLines Include="Architecture: $(DebArch)" />
      <ControlLines Include="Maintainer: $(Authors)" />
      <ControlLines Include="Description: $(Description)" />
    </ItemGroup>

    <WriteLinesToFile File="$(DebLayoutDir)/DEBIAN/control" Lines="@(ControlLines)" Overwrite="true" />

    <ItemGroup>
      <PostLines Include="#!/bin/sh" />
      <PostLines Include="set -e" />
      <PostLines Include="if [ &quot;%241&quot; = &quot;configure&quot; ]; then" />
      <PostLines Include="    mandb -q" />
      <PostLines Include="    update-desktop-database /usr/share/applications || true" />
      <PostLines Include="    update-icon-caches /usr/share/icons/hicolor || true" />
      <PostLines Include="fi" />
      <PostLines Include="exit 0" />
    </ItemGroup>

    <WriteLinesToFile File="$(DebLayoutDir)/DEBIAN/postinst" Lines="@(PostLines)" Overwrite="true" />
    
    <Exec Command="chmod 755 &quot;$(DebLayoutDir)/DEBIAN/postinst&quot;" />
    <Exec Command="cd &quot;$(DebLayoutDir)&quot; &amp;&amp; dpkg-deb --build --root-owner-group . &quot;$(ZipFileDir)/$(PackageFileName)&quot;" />

    <RemoveDir Directories="$(UnzipTemp)" />
    <RemoveDir Directories="$(DebLayoutDir)" />
    <Exec Command="rm -f &quot;$(TempManPath)&quot;" />

    <Message Importance="high" Text="Debian package successfully created: $(ZipFileDir)/$(PackageFileName)" />
  </Target>

  <ItemGroup>
    <None Include="../../scripts/nwbuilders/numbworks_logo_256x256px.png" Pack="true" PackagePath="\" />
    <None Include="../../scripts/nwbuilders/NW.NGramTextClassification.CLI.md" Pack="true" PackagePath="\" />
  </ItemGroup>

  <PropertyGroup>
    <PackAsTool>true</PackAsTool>
    <ToolCommandName>nwngram</ToolCommandName>
    <PackageId>NW.NGramTextClassification.CLI</PackageId>
    <PackageOutputPath>./nupkg</PackageOutputPath>
    <PackageReadmeFile>NW.NGramTextClassification.CLI.md</PackageReadmeFile>
    <PackageIcon>numbworks_logo_256x256px.png</PackageIcon>
  </PropertyGroup>

  <PropertyGroup>
    <PackageProjectUrl>https://github.com/numbworks/NW.NGramTextClassification</PackageProjectUrl>
    <RepositoryUrl>https://github.com/numbworks/NW.NGramTextClassification</RepositoryUrl>
    <RepositoryType>git</RepositoryType>
    <PackageTags>library csharp machine learning text classification ngrams</PackageTags>
  </PropertyGroup>

  <PropertyGroup>
    <IsTestProject>false</IsTestProject>
    <IsTestingPlatformApplication>false</IsTestingPlatformApplication>
    <GenerateTestingPlatformEntryPoint>false</GenerateTestingPlatformEntryPoint>
  </PropertyGroup>

</Project>
```

`NW.NGramTextClassification.UnitTests.csproj`, `NW.NGramTextClassification.CLI.UnitTests.csproj`:

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>disable</ImplicitUsings>
    <Nullable>disable</Nullable>
    <IsPackable>false</IsPackable>
    <IsTestProject>true</IsTestProject>
  </PropertyGroup>

  ...

</Project>
```

## The Publishing Strategies

The artifacts generated by the build pipeline are manually attached to the Github release.

The NuGet package and the NuGet tool are automatically published on NuGet\.org via GitHub Actions.

`.github/workflows/dotnet.yml`:

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
      working-directory: ./src/NW.NGramTextClassification
      run: dotnet pack --configuration Release --no-build --output bin/Release

    - name: Publish library to NuGet.org
      run: Get-ChildItem ./src/NW.NGramTextClassification/bin/Release/*.nupkg | ForEach-Object { dotnet nuget push $_.FullName --source https://api.nuget.org/v3/index.json --api-key ${{ secrets.NUGET_API_KEY }} --skip-duplicate }

    - name: Pack tool
      working-directory: ./src/NW.NGramTextClassification.CLI
      run: dotnet pack --configuration Release --no-build

    - name: Publish tool to NuGet.org
      run: Get-ChildItem ./src/NW.NGramTextClassification.CLI/nupkg/*.nupkg | ForEach-Object { dotnet nuget push $_.FullName --source https://api.nuget.org/v3/index.json --api-key ${{ secrets.NUGET_API_KEY }} --skip-duplicate }
```

## The Debian Packages

`Exec=/bin/bash -c '/usr/bin/${PROJECT_ALIAS}; exec bash'` will launch the installed CLI application without closing the terminal window afterwards. Bash's full-path is used to increase compatibility.

## The Man Page

Let's say that the source file for the `man` page is called `nwxxx.md`.

To build and preview the `man` page:

```bash
go-md2man -in nwxxx.md -out nwxxx.1
man ./nwxxx.1
```

The preview doesn't show the man page with 100% accuracy, therefore you need to install the `deb` package and run `man nwxxx`. Please know that you can't use the devcontainer for this, but you require a real Linux machine. 

If you try, you'll get the following error messages:

```bash
$ man nwxxx
No manual entry for nwxxx

$ ls -l /usr/share/man/man1/nwxxx.1.gz
ls: cannot access '/usr/share/man/man1/nwxxx.1.gz': No such file or directory 
```

The reason is that the devcontainer is a slimmed-down Linux environment and it has a "No Documentation" policy active. This means that a system-level filter is intercepting the installation and deleting documentation to save disk space. 

You can verify it by typing the following command:

```bash
$ nano /etc/dpkg/dpkg.cfg.d/docker
```

```
...
path-exclude /usr/share/man/*
....
```

Side note: the Markdown file used for `go-md2man` must have LF as end-of-line.

## Notes: Publishing On NuGet\.org

Publishing on NuGet\.org requires the following manual processes:

1. Creating an API key on NuGet.org:
    - Go to http://nuget.org and login.
    - Go to https://www.nuget.org/account/apikeys?forceApiKeys=true
    - Add or regerate an API Key.
    - Copy and paste the API key somewhere.
    - Done!
2. Adding the API key on Github:
    - Go to the repository's main page - e.g. https://github.com/numbworks/NW.NGramTextClassification
    - Click on `Settings` tab => `Secrets and variables` => `Actions` => Add or update `NUGET_API_KEY`.
    - Done!
3. (Optional) Previewing the documentation of the `.nupkg` packages before publishing:
    - Go to http://nuget.org and login.
    - Click on `Upload` in the top navigation bar.
    - Drag and drop your generated `.nupkg` file.
    - Stop here! Do not click the final `Submit` button! Once done, just click on `Cancel`!
    - Done!

## Source

The following project is used for the examples in this document:

- [NW.NGramTextClassification](https://github.com/numbworks/NW.NGramTextClassification)

## Markdown Toolset

Suggested toolset to view and edit this Markdown file:

- [Visual Studio Code](https://code.visualstudio.com/)
- [Markdown Preview Enhanced](https://marketplace.visualstudio.com/items?itemName=shd101wyy.markdown-preview-enhanced)
- [Markdown PDF](https://marketplace.visualstudio.com/items?itemName=yzane.markdown-pdf)