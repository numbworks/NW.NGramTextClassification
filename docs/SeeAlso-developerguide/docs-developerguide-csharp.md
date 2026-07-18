# developerguide-csharp
Contact: numbworks@gmail.com

## Revision History

| Date | Author | Description |
|---|---|---|
| 2026-05-10 | numbworks | Created. |
| 2026-07-04 | numbworks | Last update. |

## Introduction

This guide collects all the information to get started with a `NW.*` project as a developer.

## Getting Started (as a developer)

To run this application as a developer:

1. Download and install [Visual Studio Code](https://code.visualstudio.com/Download);
2. Download and install [Docker](https://www.docker.com/products/docker-desktop/);
3. Download and install [Git](https://git-scm.com/downloads);
4. Launch Visual Studio Code and open the project's folder;
5. if not asked, click on <ins>View</ins> > <ins>Command Palette</ins> and type:

    ```
    > Dev Container: Reopen in Container
    ```

6. Wait some minutes for the container defined in the <ins>.devcointainer</ins> folder to be built;
7. Done!

To run the executable you just build:

```bash
cd /workspaces/.dotnet-bin/NW.ZooSketches.CLI/bin/net8.0
./nwzsd
```

## Unit Tests

In order to run the unit tests, you have two options:

1. VSCode sidebar -> `Testing` icon -> `Run Tests`.
2. VSCode menubar -> `Terminal` -> `New Terminal` -> `dotnet test`.

If you want to run the tests of a specific project of the solution:

```bash
dotnet test tests/NW.XPaths.UnitTests/NW.XPaths.UnitTests.csproj
```

In order to see the unit tests coverage, you have two options as well:

1. VSCode menubar -> `Terminal` -> `Run Task` -> `test coverage summary`.
2. VSCode menubar -> `Terminal` -> `New Terminal` and run the following command:

  ```bash
  resultsdir="$(mktemp -d)" reportdir="$(mktemp -d)" ; trap 'rm -rf "$resultsdir" "$reportdir"' EXIT ; dotnet test --collect:"XPlat Code Coverage" --results-directory "$resultsdir" && reportgenerator -reports:"$resultsdir/**/coverage.cobertura.xml" -targetdir:"$reportdir" -reporttypes:TextSummary && cat "$reportdir/Summary.txt"
  ```

The two bullet-points above depend on the following utility:

```bash
dotnet tool install -g dotnet-reportgenerator-globaltool
```

When adding parametric unit tests to the code, don't forget to uniquely name each `TestCase` using the `SetArgDisplayNames`, otherwise they will run but they will show up as one test (not multiple):

```csharp
private static TestCaseData[] exceptionTestCases =
{
    new TestCaseData(
        new TestDelegate( () => new XPathManager().GetAttributeValues(null!, "sometext")),
        typeof(ArgumentNullException),
        new ArgumentNullException("htmlText").Message
        ).SetArgDisplayNames($"{nameof(exceptionTestCases)}_01"),
    ...
}
```

## Devcontainers and dotnet

Managing a C# solution in VSCode instead of Visual Studio requires the user to rely upon the `dotnet` command for the majority of the operations:

```bash
dotnet new sln -n NW.ZooSketchDownloader
```

```bash
dotnet new classlib -n NW.ZooSketches -o src/NW.ZooSketches
dotnet sln add src/NW.ZooSketches/NW.ZooSketches.csproj
dotnet add src/NW.ZooSketches/NW.ZooSketches.csproj package HtmlAgilityPack
```

```bash
dotnet new nunit -n NW.ZooSketches.UnitTests -o tests/NW.ZooSketches.UnitTests
dotnet sln add tests/NW.ZooSketches.UnitTests/NW.ZooSketches.UnitTests.csproj
dotnet add tests/NW.ZooSketches.UnitTests/NW.ZooSketches.UnitTests.csproj reference src/NW.ZooSketches/NW.ZooSketches.csproj
dotnet add tests/NW.ZooSketches.UnitTests/NW.ZooSketches.UnitTests.csproj package Microsoft.NET.Test.Sdk
dotnet add tests/NW.ZooSketches.UnitTests/NW.ZooSketches.UnitTests.csproj package NUnit3TestAdapter
dotnet add tests/NW.ZooSketches.UnitTests/NW.ZooSketches.UnitTests.csproj package NSubstitute
```

```bash
dotnet run --project src/NW.ZooSketches.CLI
```

```bash
dotnet restore --force
```

## Devcontainers and C# templates

The devcontainer pre-installs a class template:

```dockerfile
...
COPY data/TemplateClass /opt/templates/TemplateClass
RUN dotnet new install /opt/templates/TemplateClass
...
```

To create a new class starting from the template:

```bash
dotnet new install ./data/TemplateClass
cd src/NW.ZooSketches/Sketches/
dotnet new classtemplate -n SketchCollection --namespace NW.ZooSketches
```

If problems arise:

```bash
rm -rf /home/vscode/.templateengine
dotnet new --debug:reinit
```

## Devcontainers and C# Dev Kit

The **C# Dev Kit** extension for VSCode (`ms-dotnettools.csdevkit`) requires two runtimes (`9.0.12~x64~aspnetcore` and `10.0.2~x64~aspnetcore`) to work. When used inside a devcontainer, it attempts to download these runtimes from scratch every time the user rebuilds the container.

Even if the user tries to pre‑install these runtimes in the `Dockerfile` using `dotnet-install.sh`, the extension will still re-download them after each rebuild. 

This happens because the **C# Dev Kit** expects extension-specific metadata (a manifest file, a SHA‑256 hash, and other internal markers) that `dotnet-install.sh` can't generate or replicate.

These runtimes are downloaded in the following folder: 

```
$HOME/.vscode-server/data/User/globalStorage/ms-dotnettools.vscode-dotnet-runtime
``` 

One way to work around the issue is to make the **C# Dev Kit** download the runtime in a Docker volume instead of storing them inside the devcontainer’s filesystem.

From the second rebuild onward, the extension can then reuse the volume as a cache. This route is not immediate, though, because the Docker volumes are always created as `root`, while the folder in which the C# Dev Kit attempts to write is owned by the `vscode` user. 

If the volume is mounted as‑is, the following error is raised: 

```
mkdir: cannot create directory '/home/vscode/.vscode-server/bin': Permission denied
```

The only workable solution consists of three steps:

1. Declare `USER vscode` at the end of the `Dockerfile`. This ensures that VS Code Server installs as `vscode`, that this user creates the symlink 
and that the mounted volume will be chowned by the same user.
2. Mount the Docker volume outside of `$HOME`:  

```
...
"mounts": [
		"source=vscode-dotnet-runtime,target=/dotnet-runtime-cache,type=volume",
		...
```

3. After VS Code Server installs, fix the volume ownership and symlink the runtime folder into the mounted volume:

```
"postCreateCommand": "sudo chown -R vscode:vscode /dotnet-runtime-cache && ..."
```

Once the devcontainer has been built for the first time, the user can verify that everything is as expected using the following commands:

```
dotnet --version 
```

```
ls  ~/.vscode-server/data/User/globalStorage/ms-dotnettools.vscode-dotnet-runtime/.dotnet
```

**Side note**: even if **C# Dev Kit** fetches the runtimes from the Docker volume as expected, it stills "calls home" at every rebuild to check if a more updated runtime is available. 

In case of a positive answer, it will attempt to download it the latest version, breaking the determinism of the environment once again.

## Devcontainers and Microsoft base images

The devcontainer used by this application is based upon a base image provided by the Microosft Artifact Registry:

```dockerfile
FROM mcr.microsoft.com/devcontainers/dotnet:8.0
```

This base images "calls home" everytime the user rebuilds the container, which means that the rebuilding process will fail if the user is offline: 

```
=> ERROR [internal] load metadata for mcr.microsoft.com/devcontainers/do  0.0s
------
 > [internal] load metadata for mcr.microsoft.com/devcontainers/dotnet:8.0:
------
Dockerfile:1
--------------------
   1 | >>> FROM mcr.microsoft.com/devcontainers/dotnet:8.0
   2 |
   3 |     RUN apt -y update && \
--------------------
ERROR: failed to build: failed to solve: mcr.microsoft.com/devcontainers/dotnet:
8.0: failed to resolve source metadata for mcr.microsoft.com/devcontainers/dotne
t:8.0: failed to do request: Head "https://mcr.microsoft.com/v2/devcontainers/do
tnet/manifests/8.0": dialing mcr.microsoft.com:443 container via direct connecti
on because Docker Desktop has no HTTPS proxy: connecting to mcr.microsoft.com:44
3: dial tcp: lookup mcr.microsoft.com: no such host
```

One possible workaround is to: 

1. Retrieve the SHA-256 digest for your architecture:

  ```
  docker buildx imagetools inspect mcr.microsoft.com/devcontainers/dotnet:8.0
  ```
  ```
  ...
  Name:        mcr.microsoft.com/devcontainers/dotnet:8.0@sha256:e4fbb49c3ee92933e40c6974b42dda3051827b7ff932463edbd0b575f63a612c
  MediaType:   application/vnd.oci.image.manifest.v1+json
  Platform:    linux/amd64
  ...
  ```

2. Update your `Dockerfile` accordingly:

  ```
  FROM mcr.microsoft.com/devcontainers/dotnet@sha256:<DIGEST>
  ```

3. Done!

If you are loading a VSCode project built upon a devcontainer defined using a SHA-256 digest, you can pull the specific base image using this command:

```
docker pull mcr.microsoft.com/devcontainers/dotnet@sha256:<DIGEST>
```

## Markdown Toolset

Suggested toolset to view and edit this Markdown file:

- [Visual Studio Code](https://code.visualstudio.com/)
- [Markdown Preview Enhanced](https://marketplace.visualstudio.com/items?itemName=shd101wyy.markdown-preview-enhanced)
- [Markdown PDF](https://marketplace.visualstudio.com/items?itemName=yzane.markdown-pdf)