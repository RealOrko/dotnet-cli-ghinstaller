using ghinstaller.Modules.Commands.Options;

namespace ghinstaller.Commands
{
    [Command("list-release")]
    public class ListReleaseArguments
    {
        [Argument(ShortName = "-o", LongName = "-owner", Help = "The name of the GitHub Organisation/Owner eg. -o realorko")]
        public string Owner { get; set; }
        
        [Argument(ShortName = "-r", LongName = "-repository", Help = "The name of the GitHub Organisation eg. -r dotnet-cli-zip")]
        public string Repository { get; set; }
        
        [Argument(ShortName = "-t", LongName = "-tarball", Help = "Only output the tarball url of the release eg. -t")]
        public bool TarballOnly { get; set; }

        [Argument(ShortName = "-z", LongName = "-zipball", Help = "Only output the zipball url of the release eg. -z")]
        public bool ZipballOnly { get; set; }

        [Argument(ShortName = "-a", LongName = "-assets", Help = "Only output the assets of the release eg. -a")]
        public bool AssetsOnly { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Owner)
                   && !string.IsNullOrEmpty(Repository);
        }
    }
}