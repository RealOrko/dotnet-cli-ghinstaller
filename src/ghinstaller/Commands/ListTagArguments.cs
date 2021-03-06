using ghinstaller.Modules.Commands.Options;

namespace ghinstaller.Commands
{
    [Command("list-tag")]
    public class ListTagArguments
    {
        [Argument(ShortName = "-o", LongName = "-owner", EnvVar = "GHI_ORGANISATION", Help = "The name of the GitHub Organisation/Owner eg. -o realorko")]
        public string Owner { get; set; }
        
        [Argument(ShortName = "-r", LongName = "-repository", EnvVar = "GHI_REPOSITORY", Help = "The name of the GitHub Organisation eg. -r dotnet-cli-zip")]
        public string Repository { get; set; }
        
        [Argument(ShortName = "-f", LongName = "-find", Help = "The name of the tag to filter on eg. -f v1")]
        public string Find { get; set; }
        
        [Argument(ShortName = "-n", LongName = "-names", Help = "Only output the name of the tag eg. -n")]
        public bool NameOnly { get; set; }

        [Argument(ShortName = "-t", LongName = "-tarball", Help = "Only output the tarball url of the tag eg. -t")]
        public bool TarballOnly { get; set; }

        [Argument(ShortName = "-z", LongName = "-zipball", Help = "Only output the zipball url of the tag eg. -z")]
        public bool ZipballOnly { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Owner)
                   && !string.IsNullOrEmpty(Repository);
        }
    }
}