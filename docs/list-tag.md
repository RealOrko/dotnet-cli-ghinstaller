# dotnet-cli-ghinstaller

## Listing Tags

Here is an example of [listing tags](https://github.com/cloudfoundry/bosh-bootloader/tags) for the `bosh-bootloader` 
project in the `cloudfoundry` organisation.
Please see [downloading tags](https://github.com/RealOrko/dotnet-cli-ghinstaller/blob/master/docs/download-tag.md) for more. 

### Synopsis

```
ghi list-tag
------------

   -owner, -o or GHI_ORGANISATION env var
      The name of the GitHub Organisation/Owner eg. -o realorko

   -repository, -r or GHI_REPOSITORY env var
      The name of the GitHub Organisation eg. -r dotnet-cli-zip

   -find or -f
      The name of the tag to filter on eg. -f v1

   -names or -n
      Only output the name of the tag eg. -n

   -tarball or -t
      Only output the tarball url of the tag eg. -t

   -zipball or -z
      Only output the zipball url of the tag eg. -z
```

### Examples 

List all tags by name:

```bash
ghi list-tag -o cloudfoundry -r bosh-bootloader
```

To get a list of tarball download urls:

```bash
ghi list-tag -o cloudfoundry -r bosh-bootloader -t
```

To get a list of zipball download urls:

```bash
ghi list-tag -o cloudfoundry -r bosh-bootloader -z
```

To find a specific tag:

```bash
ghi list-tag -o cloudfoundry -r bosh-bootloader -f v8.4.0 
```

To find a specific tag with tarball url:

```bash
ghi list-tag -o cloudfoundry -r bosh-bootloader -f v8.4.0 -t
```

To find a specific tag with zipball url:

```bash
ghi list-tag -o cloudfoundry -r bosh-bootloader -f v8.4.0 -z
```
