# dotnet-cli-ghinstaller

## Listing Releases

Releases can include build assets which have platform specific binaries, some this is not always available in 
that case we recommend you look at [list-tag](https://github.com/RealOrko/dotnet-cli-ghinstaller/blob/master/docs/list-tag.md).
Please see [downloading releases](https://github.com/RealOrko/dotnet-cli-ghinstaller/blob/master/docs/download-release.md) for more. 

### Synopsis

```
ghi list-release
----------------

   -owner, -o or GHI_ORGANISATION env var
      The name of the GitHub Organisation/Owner eg. -o realorko

   -repository, -r or GHI_REPOSITORY env var
      The name of the GitHub Organisation eg. -r dotnet-cli-zip

   -find or -f
      The name of the release to filter on eg. -f v1

   -assetfind or -af
      The name of the asset to filter on eg. -af linux

   -names or -n
      Only output the name of the release eg. -n

   -tarball or -t
      Only output the tarball url of the release eg. -t

   -zipball or -z
      Only output the zipball url of the release eg. -z

   -assets or -a
      Only output the assets of the release eg. -a

   -assetsname or -an
      Only output the name of the assets of the release eg. -an

   -assetsurl or -au
      Only output the url of the assets of the release eg. -au
```

### Examples

List all releases by name:

```bash
ghi list-release -o cloudfoundry -r bosh-bootloader
```

To get a list of tarball download urls:

```bash
ghi list-release -o cloudfoundry -r bosh-bootloader -t
```

To get a list of zipball download urls:

```bash
ghi list-release -o cloudfoundry -r bosh-bootloader -z
```

To find a specific release:

```bash
ghi list-release -o cloudfoundry -r bosh-bootloader -f v8.4.0 
```

To get a list of asset names for a specific release: 

```bash
ghi list-release -o cloudfoundry -r bosh-bootloader -f v8.4.0 -a
```

To get a list of asset urls: 

```bash
ghi list-release -o cloudfoundry -r bosh-bootloader -f v8.4.0 -a -au
```

To get a specific asset url:

```bash
ghi list-release -o cloudfoundry -r bosh-bootloader -f v8.4.0 -a -au -af linux
```
