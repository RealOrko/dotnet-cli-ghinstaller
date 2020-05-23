# dotnet-cli-ghinstaller

## Untar

Here is an example of [unzipping archives](https://github.com/cloudfoundry/bosh-bootloader/tags) for the `bosh-bootloader` 
project in the `cloudfoundry` organisation. Please see [download-release](https://github.com/RealOrko/dotnet-cli-ghinstaller/blob/master/docs/download-release.md) 
and [download-tag](https://github.com/RealOrko/dotnet-cli-ghinstaller/blob/master/docs/download-tag.md).

### Synopsis

```
ghi unzip
---------

   -zip or -z
      The path to the zip file you would to extract eg. -z myrelease.zip

   -output or -o
      The output folder that needs to be created eg. -o myrelease/
```

### Examples 

Extract tar:

```
ghi untar -z v8.4.0.zip -o ./v8.4.0
```