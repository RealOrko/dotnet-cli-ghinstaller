# dotnet-cli-ghinstaller

## Build Golang

This helps bootstrap building tar and zip balls that have been downloaded using this tool for Golang. 

### Synopsis

```
ghi build-go
------------

   -binary, -b or GHI_GO_BINARY env var
      The path to the go binary or executable eg. -b /usr/bin/go

   -arguments, -a or GHI_GO_ARGS env var
      The parameters for the go binary or executable eg. -a build or -a get

   -directory, -d or GHI_GO_DIRECTORY env var
      The parameters for the working directory containing the go source code eg. -d ./v1.2/

   -gopath, -gp or GHI_GO_PATH env var
      The target directory for the go path to be used eg. -gp ./go

   -output or -o
      The path for the output binary, relative to -d eg. -o ../my-shiny-new-go-binary

   -timeout or -t
      The timeout for how long the go build process should take in minutes eg. -t 120

``` 

### Examples

To download and build a go project using releases:

```bash
ghi download-tag -o cloudfoundry -r cli -f v6.51.0 -t
ghi untar -t v6.51.0.tar -o ./v6.51.0
ghi build-go -a get -d ./v6.51.0/cloudfoundry-cli-2acd156/ || true
ghi build-go -a build -d ./v6.51.0/cloudfoundry-cli-2acd156/ -o ../../cli
```
