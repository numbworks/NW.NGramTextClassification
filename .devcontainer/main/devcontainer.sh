#!/usr/bin/env bash
set -euo pipefail

PATH_CACHE=/cache
PATH_WORKSPACES=/workspaces
PATH_HOME=/home/vscode

sudo mkdir -p \
  "$PATH_CACHE/obj" \
  "$PATH_CACHE/bin" \
  "$PATH_CACHE/runtime" \
  "$PATH_CACHE/nuget" \
  "$PATH_HOME/.nuget" \
  "$PATH_WORKSPACES"

sudo rm -rf "$PATH_WORKSPACES/.dotnet-obj"
sudo rm -rf "$PATH_WORKSPACES/.dotnet-bin"
sudo rm -rf /dotnet-runtime-cache
sudo rm -rf "$PATH_HOME/.nuget/packages"

sudo ln -s "$PATH_CACHE/obj" "$PATH_WORKSPACES/.dotnet-obj"
sudo ln -s "$PATH_CACHE/bin" "$PATH_WORKSPACES/.dotnet-bin"
sudo ln -s "$PATH_CACHE/runtime" /dotnet-runtime-cache
sudo ln -s "$PATH_CACHE/nuget" "$PATH_HOME/.nuget/packages"

sudo chown -R vscode:vscode "$PATH_CACHE"

sudo chown -h vscode:vscode \
  "$PATH_WORKSPACES/.dotnet-obj" \
  "$PATH_WORKSPACES/.dotnet-bin" \
  /dotnet-runtime-cache \
  "$PATH_HOME/.nuget/packages"

mkdir -p "$HOME/.vscode-server/data/User/globalStorage"
rm -rf "$HOME/.vscode-server/data/User/globalStorage/ms-dotnettools.vscode-dotnet-runtime"
ln -s /dotnet-runtime-cache "$HOME/.vscode-server/data/User/globalStorage/ms-dotnettools.vscode-dotnet-runtime"