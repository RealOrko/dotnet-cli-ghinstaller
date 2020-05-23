# dotnet-cli-ghinstaller

## Downloading Releases

Releases can include build assets which have platform specific binaries, some this is not always available in 
that case we recommend you look at [download-tag](https://github.com/RealOrko/dotnet-cli-ghinstaller/blob/master/docs/download-tag.md).
Please see more about [untarring](https://github.com/RealOrko/dotnet-cli-ghinstaller/blob/master/docs/untar.md) 
and [unzipping](https://github.com/RealOrko/dotnet-cli-ghinstaller/blob/master/docs/unzip.md).

### Synopsis

```
ghi download-release
--------------------

   -owner, -o or GHI_ORGANISATION env var
      The name of the GitHub Organisation/Owner eg. -o realorko

   -repository, -r or GHI_REPOSITORY env var
      The name of the GitHub Organisation eg. -r dotnet-cli-zip

   -find or -f
      The name of the release to filter on eg. -f v1

   -assetfind or -af
      The name of the asset to filter on eg. -af linux

   -tarball or -t
      Only download the tarball url of the release eg. -t

   -zipball or -z
      Only download the zipball url of the release eg. -z

   -assets or -a
      Only download the assets of the release eg. -a
``` 

### Examples

To download a specific release (tarball and zipball):

```bash
ghi download-release -o cloudfoundry -r bosh-bootloader -f v8.4.0 
```

To download a specific release (tarball only):

```bash
ghi download-release -o cloudfoundry -r bosh-bootloader -f v8.4.0 -t
```

To download a specific release (zipball only):

```bash
ghi download-release -o cloudfoundry -r bosh-bootloader -f v8.4.0 -z
```

To get a download of assets for a specific release: 

```bash
ghi download-release -o cloudfoundry -r bosh-bootloader -f v8.4.0 -a
```

To download a specific asset:

```bash
ghi download-release -o cloudfoundry -r bosh-bootloader -f v8.4.0 -a -au -af linux
```
