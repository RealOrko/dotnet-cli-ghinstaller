# dotnet-cli-ghinstaller

## Build DOTNET Core

This helps bootstrap building tar and zip balls that have been downloaded using this tool for DOTNET Core. 

### Synopsis

```
ghi build-dotnet
----------------

   -binary, -b or GHI_DOTNET_BINARY env var
      The path to the dotnet binary or executable eg. -b /usr/bin/dotnet

   -arguments, -a or GHI_DOTNET_ARGS env var
      The parameters for the dotnet binary or executable eg. -a build or -a restore or -a publish

   -directory, -d or GHI_DOTNET_DIRECTORY env var
      The parameters for the working directory containing the dotnet source code eg. -d ./v1.2/

   -output or -o
      The path for the binary output, relative to -d eg. -o ../my-shiny-new-binary-output-drectory/

   -timeout or -t
      The timeout for how long the go build process should take in minutes eg. -t 120
``` 

### Examples

To download and build a dotnet project using tags:

```bash
ghi download-tag -o realorko -r dotnet-cli-ghinstaller -f v0.0.6 -t
ghi untar -t v0.0.6.tar -o ./v0.0.6
ghi build-dotnet -d ./v0.0.6/RealOrko-dotnet-cli-ghinstaller-a697075/src/ghinstaller -o ../../../../build 
```
