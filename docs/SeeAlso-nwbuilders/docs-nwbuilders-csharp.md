# nwbuilders-csharp
Contact: numbworks@gmail.com

## Revision History

| Date | Author | Description |
|---|---|---|
| 2026-03-10 | numbworks | Created. |
| 2026-04-06 | numbworks | Last update. |

## Introduction

`nwbuilders` is a collection of guidelines and configuration templates that simplify and standardize the cross-compilation and packaging workflow for CLI applications on local build agents.

## Overview

We target the following architectures and operating systems:

- Linux executables (AMD64 and ARM64)
- Windows executables (AMD64)

The `dotnet publish` command supports all of them via cross-compilation, therefore it's a smooth sailing.

## The Configuration

Here the expected structure of the project:

```
...
scripts/
├── nwbuilders
    ├── numbworks_logo_256x256px.ico
    ├── numbworks_logo_256x256px.png
    └── <cli_name>.md
└── ...
src/
├── ...
├── ...
├── NW.<library_name>
    ├── ...
    ├── NW.<library_name>.csproj
    └── ...
└── NW.<library_name>.CLI
    ├── ...
    ├── NW.<library_name>.CLI.csproj
    └── Program.cs
...
```

As shown, all the business logic is encapsulated in the `NW.<library_name>` project, while an additional `NW.<library_name>.CLI` project is responsible for exposing its entry-point(s) thru a command-line interface.

In order to build and package the executable according to our needs, we'll need to customize `NW.<library_name>.CLI.csproj` and store the required files in `scripts/nwbuilders`.

## The Host Machine

For our setup, a separate build agent is not required. 

The `dotnet publish` command runs flawlessy in the same Linux devcontainer used for application development and, once triggered, it will save the resulting packages on a folder on the development's machine hard drive using a mount point.

To launch the `nwbuilders` configuration:

1. Open the solution's folder in Visual Studio Code
2. Click on `Terminal` > `Run Task` > `publish`
3. Done!

## The Artifacts

Here the list of the expected artifacts:

- `<cli_name>-v<version>-linux-arm64.zip`
- `<cli_name>-v<version>-linux-arm64.deb`
- `<cli_name>-v<version>-linux-amd64.zip`
- `<cli_name>-v<version>-linux-amd64.deb`
- `<cli_name>-v<version>-win-amd64.zip`

More context:

1. Each ZIP package contains only the executable. The Windows variant will be supplemented with an application icon and embedded metadata.
2. The DEB packages, once installed, will provide the executable file, the menu item with the icon and a `man` page.

## Notes About The Metadata

Here an example of how to define metadata in `NW.<library_name>.CLI.csproj`:

```xml
<PropertyGroup>
    <OutputType>Exe</OutputType>
    ...
</PropertyGroup>
...
<PropertyGroup>
    <AssemblyName>nwzsd</AssemblyName>
    <Version>2.0.0</Version>
    <IncludeSourceRevisionInInformationalVersion>false</IncludeSourceRevisionInInformationalVersion>
    <Authors>numbworks</Authors>
    <Company>numbworks</Company>
    <Product>NW.ZooSketchDownloader</Product>
    <Copyright>numbworks</Copyright>
    <ApplicationIcon>$(MSBuildThisFileDirectory)../../scripts/nwbuilders/numbworks_logo_256x256px.ico</ApplicationIcon>
    <Description>nwzsd (NW.ZooSketchDownloader) is an application that extracts 'Lo Zoo di 105' sketch download metadata (JSON or MetaLink) from 'Radio 105' website.</Description>
</PropertyGroup>
```

## Notes About The `ZipCustomOutput` Target

Here how the `ZipCustomOutput` target is defined in `NW.<library_name>.CLI.csproj`:

```xml
<Target Name="ZipCustomOutput" AfterTargets="Publish">
<PropertyGroup>
    <ExecutableExtension Condition="$(RuntimeIdentifier.StartsWith('win'))">.exe</ExecutableExtension>
    <ExecutableExtension Condition="!$(RuntimeIdentifier.StartsWith('win'))"></ExecutableExtension>

    <BinaryName>$(AssemblyName)$(ExecutableExtension)</BinaryName>
    <ZipFileName>$(AssemblyName)-$(Version)-$(RuntimeIdentifier).zip</ZipFileName>
    
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
```

## Notes About The `CreateDebianPackage` Target

Here how the `CreateDebianPackage` target is defined in `NW.<library_name>.CLI.csproj`:

```xml
<Target Name="CreateDebianPackage" AfterTargets="ZipCustomOutput" Condition="!$(RuntimeIdentifier.StartsWith('win'))">

    <PropertyGroup>
        <ZipFileDir>/home/nw.zoosketchdownloader/</ZipFileDir>
        <ZipFileName>$(AssemblyName)-$(Version)-$(RuntimeIdentifier).zip</ZipFileName>
        <SourceZipPath>$(ZipFileDir)$(ZipFileName)</SourceZipPath>
        <MdSourcePath>$(MSBuildProjectDirectory)/../../scripts/nwbuilders/$(AssemblyName).md</MdSourcePath>
        <TempManPath>/tmp/$(AssemblyName).1</TempManPath>

        <DebArch Condition="'$(RuntimeIdentifier)' == 'linux-x64'">amd64</DebArch>
        <DebArch Condition="'$(RuntimeIdentifier)' == 'linux-arm64'">arm64</DebArch>
        <DebArch Condition="'$(DebArch)' == ''">all</DebArch>

        <DebLayoutDir>/tmp/$(AssemblyName)-$(DebArch)-layout</DebLayoutDir>
        <PackageFileName>$(AssemblyName)-$(Version)-$(DebArch).deb</PackageFileName>
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

    <Exec Command="if [ -f &quot;$(UnzipTemp)/$(AssemblyName)&quot; ]; then cp &quot;$(UnzipTemp)/$(AssemblyName)&quot; &quot;$(DebLayoutDir)/usr/bin/&quot;; else cp &quot;$(UnzipTemp)/$(AssemblyName).dll&quot; &quot;$(DebLayoutDir)/usr/bin/$(AssemblyName)&quot;; fi" />
    <Exec Command="chmod 755 &quot;$(DebLayoutDir)/usr/bin/$(AssemblyName)&quot;" />

    <Exec Command="cp &quot;$(TempManPath)&quot; &quot;$(DebLayoutDir)/usr/share/man/man1/$(AssemblyName).1&quot;" />
    <Exec Command="gzip -n9 &quot;$(DebLayoutDir)/usr/share/man/man1/$(AssemblyName).1&quot;" />

    <MakeDir Directories="$(DebLayoutDir)/usr/share/applications" />
    <MakeDir Directories="$(DebLayoutDir)/usr/share/pixmaps" />

    <Exec Command="cp &quot;$(MSBuildProjectDirectory)/../../scripts/nwbuilders/numbworks_logo_256x256px.png&quot; &quot;$(DebLayoutDir)/usr/share/pixmaps/$(AssemblyName).png&quot;" />

<ItemGroup>
    <DesktopLines Include="[Desktop Entry]" />
    <DesktopLines Include="Name=$(AssemblyName)" />
    <DesktopLines Include="Exec=/bin/bash -c &quot;/usr/bin/$(AssemblyName)%3B exec bash&quot;" />
    <DesktopLines Include="Icon=$(AssemblyName)" />
    <DesktopLines Include="Type=Application" />
    <DesktopLines Include="Categories=Utility;" />
    <DesktopLines Include="Terminal=true" />
</ItemGroup>

<WriteLinesToFile 
    File="$(DebLayoutDir)/usr/share/applications/$(AssemblyName).desktop" 
    Lines="@(DesktopLines)" 
    Overwrite="true" />

<Exec Command="chmod 644 &quot;$(DebLayoutDir)/usr/share/applications/$(AssemblyName).desktop&quot;" />
<Exec Command="chmod 644 &quot;$(DebLayoutDir)/usr/share/pixmaps/$(AssemblyName).png&quot;" />

<ItemGroup>
    <ControlLines Include="Package: $(AssemblyName)" />
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
```

For converting `<cli_name>.md` to `<cli_name>.man`, the `go-md2man` application is required to be installed in the devcontainer, as shown in this example `.devcontainer/Dockerfile`:

```Dockerfile
FROM mcr.microsoft.com/devcontainers/dotnet@sha256:e4fbb49c3ee92933e40c6974b42dda3051827b7ff932463edbd0b575f63a612c

RUN apt -y update && \
    apt -y upgrade && \
    apt -y install git && \
    apt -y install go-md2man
...
```

## Notes About `publish` In `launch.json`

Here the excerpt of `.vscode/launch.json` that is responsible to trigger the targets in `NW.<library_name>.CLI.csproj` via `Run Task`:

```json
{
    "version": "2.0.0",
    "tasks": [
        {
            "label": "publish",
            "type": "shell",
            "command": "for RID in linux-arm64 win-x64 linux-x64; do dotnet publish /p:PublishSingleFile=true /p:PublishTrimmed=false /p:DebugType=None --self-contained true -c Release -r $RID --output /home/nw.zoosketchdownloader/temp/; done",
            "options": {
                "cwd": "${workspaceFolder}/src/NW.ZooSketches.CLI"
            },
            "problemMatcher": "$msCompile"
        },
        ...
}
```

## Notes About The Debian Packages

- `Exec=/bin/bash -c '/usr/bin/${PROJECT_ALIAS}; exec bash'` will launch the installed CLI application without closing the terminal window afterwards. Bash's full-path is used to increase compatibility.

## Notes About The `man` Pages

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

## Markdown Toolset

Suggested toolset to view and edit this Markdown file:

- [Visual Studio Code](https://code.visualstudio.com/)
- [Markdown Preview Enhanced](https://marketplace.visualstudio.com/items?itemName=shd101wyy.markdown-preview-enhanced)
- [Markdown PDF](https://marketplace.visualstudio.com/items?itemName=yzane.markdown-pdf)