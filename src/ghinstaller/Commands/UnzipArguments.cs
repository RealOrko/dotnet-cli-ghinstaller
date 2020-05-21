using ghinstaller.Modules.Commands.Options;

namespace ghinstaller.Commands
{
    [Command("unzip")]
    public class UnzipArguments
    {
        [Argument(ShortName = "-z", LongName = "-zip", Help = "The path to the zip file you would to extract eg. -z myrelease.zip")]
        public string ZipArhivePath { get; set; }
        
        [Argument(ShortName = "-o", LongName = "-output", Help = "The output folder that needs to be created eg. -o myrelease/")]
        public string OutputDirectory { get; set; }
    }
}