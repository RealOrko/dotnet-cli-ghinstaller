# dotnet-cli-ghinstaller

## Downloading Tags

Here is an example of [downloading tags](https://github.com/cloudfoundry/bosh-bootloader/tags) for the `bosh-bootloader` 
project in the `cloudfoundry` organisation.
Please see more about [untarring](https://github.com/RealOrko/dotnet-cli-ghinstaller/blob/master/docs/untar.md) 
and [unzipping](https://github.com/RealOrko/dotnet-cli-ghinstaller/blob/master/docs/unzip.md).

### Synopsis

```
ghi download-tag
----------------

   -owner, -o or GHI_ORGANISATION env var
      The name of the GitHub Organisation/Owner eg. -o realorko

   -repository, -r or GHI_REPOSITORY env var
      The name of the GitHub Organisation eg. -r dotnet-cli-zip

   -find or -f
      The name of the tag to filter on eg. -f v1

   -tarball or -t
      Only download the tarball url of the tag eg. -t

   -zipball or -z
      Only download the zipball url of the tag eg. -z
``` 

### Examples

To download a specific tag (tarball and zipball):

```bash
ghi download-tag -o cloudfoundry -r bosh-bootloader -f v8.4.0 
```

To download a specific tag (tarball only):

```bash
ghi download-tag -o cloudfoundry -r bosh-bootloader -f v8.4.0 -t
```

To download a specific tag (zipball only):

```bash
ghi download-tag -o cloudfoundry -r bosh-bootloader -f v8.4.0 -z
```
