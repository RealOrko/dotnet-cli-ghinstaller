# dotnet-cli-ghinstaller

![Build](https://github.com/RealOrko/dotnet-cli-ghinstaller/workflows/Build/badge.svg)
![Publish](https://github.com/RealOrko/dotnet-cli-ghinstaller/workflows/Publish/badge.svg)

An installer for GitHub releases that contain OS specific binaries for the dotnet cli

## Why do I need this?

You find yourself having tried apt, dnf, snapd, brew, chocolately and you just need a tidy way to get the 
binaries downloaded and built from GitHub with a single script that works across most platforms. 

## Prerequisites

You need to install the [DOTNET Core SDK](https://dotnet.microsoft.com/download).

## Install

```
dotnet tool install -g dotnet-cli-ghinstaller
```

## Commands

Here are some examples and descriptions of all the possible commands. 
For any help please type the following command after installing: 

```bash
ghi
```

If you would like help with a specific command just type

```bash
ghi <command>
```

Available commands are:
 - [`list-release`](https://github.com/RealOrko/dotnet-cli-ghinstaller/blob/master/docs/list-release.md)
 - [`download-release`](https://github.com/RealOrko/dotnet-cli-ghinstaller/blob/master/docs/download-release.md)
 - [`list-tag`](https://github.com/RealOrko/dotnet-cli-ghinstaller/blob/master/docs/list-tag.md)
 - [`download-tag`](https://github.com/RealOrko/dotnet-cli-ghinstaller/blob/master/docs/download-tag.md)
 - [`untar`](https://github.com/RealOrko/dotnet-cli-ghinstaller/blob/master/docs/untar.md)
 - [`unzip`](https://github.com/RealOrko/dotnet-cli-ghinstaller/blob/master/docs/unzip.md)
 - [`build-dotnet`](https://github.com/RealOrko/dotnet-cli-ghinstaller/blob/master/docs/build-dotnet.md)
 - [`build-go`](https://github.com/RealOrko/dotnet-cli-ghinstaller/blob/master/docs/build-golang.md)
 - [`install`](https://github.com/RealOrko/dotnet-cli-ghinstaller/blob/master/docs/install.md)

## Uninstall

```
dotnet tool uninstall -g dotnet-cli-ghinstaller
``` 
