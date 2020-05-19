using ghinstaller.Modules.Commands.Options;

namespace ghinstaller.Commands
{
    [Command("list-release")]
    public class ListReleaseArguments
    {
        [Argument(ShortName = "-o", LongName = "-owner", EnvVar = "GHI_ORGANISATION", Help = "The name of the GitHub Organisation/Owner eg. -o realorko")]
        public string Owner { get; set; }
        
        [Argument(ShortName = "-r", LongName = "-repository", EnvVar = "GHI_REPOSITORY", Help = "The name of the GitHub Organisation eg. -r dotnet-cli-zip")]
        public string Repository { get; set; }
        
        [Argument(ShortName = "-f", LongName = "-find", Help = "The name of the release to filter on eg. -f v1")]
        public string Find { get; set; }
        
        [Argument(ShortName = "-af", LongName = "-assetfind", Help = "The name of the asset to filter on eg. -af linux")]
        public string AssetsFind { get; set; }

        [Argument(ShortName = "-n", LongName = "-names", Help = "Only output the name of the release eg. -n")]
        public bool NameOnly { get; set; }

        [Argument(ShortName = "-t", LongName = "-tarball", Help = "Only output the tarball url of the release eg. -t")]
        public bool TarballOnly { get; set; }

        [Argument(ShortName = "-z", LongName = "-zipball", Help = "Only output the zipball url of the release eg. -z")]
        public bool ZipballOnly { get; set; }

        [Argument(ShortName = "-a", LongName = "-assets", Help = "Only output the assets of the release eg. -a")]
        public bool AssetsOnly { get; set; }

        [Argument(ShortName = "-an", LongName = "-assetsname", Help = "Only output the name of the assets of the release eg. -an")]
        public bool AssetsNameOnly { get; set; }

        [Argument(ShortName = "-au", LongName = "-assetsurl", Help = "Only output the url of the assets of the release eg. -au")]
        public bool AssetsUrlOnly { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Owner)
                   && !string.IsNullOrEmpty(Repository);
        }
    }
}