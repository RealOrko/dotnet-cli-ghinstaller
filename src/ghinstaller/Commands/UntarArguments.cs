using ghinstaller.Modules.Commands.Options;

namespace ghinstaller.Commands
{
    [Command("untar")]
    public class UntarArguments
    {
        [Argument(ShortName = "-t", LongName = "-tar", Help = "The path to the tar file you would to extract eg. -t myrelease.tar")]
        public string TarArhivePath { get; set; }
        
        [Argument(ShortName = "-o", LongName = "-output", Help = "The output folder that needs to be created eg. -o myrelease/")]
        public string OutputDirectory { get; set; }
    }
}