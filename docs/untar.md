# dotnet-cli-ghinstaller

## Untar

Here is an example of [untarring archives](https://github.com/cloudfoundry/bosh-bootloader/tags) for the `bosh-bootloader` 
project in the `cloudfoundry` organisation. Please see [download-release](https://github.com/RealOrko/dotnet-cli-ghinstaller/blob/master/docs/download-release.md) 
and [download-tag](https://github.com/RealOrko/dotnet-cli-ghinstaller/blob/master/docs/download-tag.md).

### Synopsis

```
ghi untar
---------

   -tar or -t
      The path to the tar file you would to extract eg. -t myrelease.tar

   -output or -o
      The output folder that needs to be created eg. -o myrelease/
```

### Examples 

Extract tar:

```
ghi untar -t v8.4.0.tar -o ./v8.4.0
```