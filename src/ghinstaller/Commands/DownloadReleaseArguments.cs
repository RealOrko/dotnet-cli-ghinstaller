using ghinstaller.Modules.Commands.Options;

namespace ghinstaller.Commands
{
    [Command("download-release")]
    public class DownloadReleaseArguments
    {
        [Argument(ShortName = "-o", LongName = "-owner", EnvVar = "GHI_ORGANISATION", Help = "The name of the GitHub Organisation/Owner eg. -o realorko")]
        public string Owner { get; set; }
        
        [Argument(ShortName = "-r", LongName = "-repository", EnvVar = "GHI_REPOSITORY", Help = "The name of the GitHub Organisation eg. -r dotnet-cli-zip")]
        public string Repository { get; set; }
        
        [Argument(ShortName = "-f", LongName = "-find", Help = "The name of the release to filter on eg. -f v1")]
        public string Find { get; set; }
        
        [Argument(ShortName = "-af", LongName = "-assetfind", Help = "The name of the asset to filter on eg. -af linux")]
        public string AssetsFind { get; set; }

        [Argument(ShortName = "-t", LongName = "-tarball", Help = "Only download the tarball url of the release eg. -t")]
        public bool TarballOnly { get; set; }

        [Argument(ShortName = "-z", LongName = "-zipball", Help = "Only download the zipball url of the release eg. -z")]
        public bool ZipballOnly { get; set; }

        [Argument(ShortName = "-a", LongName = "-assets", Help = "Only download the assets of the release eg. -a")]
        public bool AssetsOnly { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Owner)
                   && !string.IsNullOrEmpty(Repository);
        }
    }
}