using ghinstaller.Modules.Commands.Options;

namespace ghinstaller.Commands
{
    [Command("build-go")]
    public class BuildGoArguments
    {
        [Argument(ShortName = "-b", LongName = "-binary", EnvVar = "GHI_GO_BINARY", Help = "The path to the go binary or executable eg. -b /usr/bin/go")]
        public string Binary { get; set; }
        
        [Argument(ShortName = "-a", LongName = "-arguments", EnvVar = "GHI_GO_ARGS", Help = "The parameters for the go binary or executable eg. -a build or -a get")]
        public string Arguments { get; set; }

        [Argument(ShortName = "-d", LongName = "-directory", EnvVar = "GHI_GO_DIRECTORY", Help = "The parameters for the working directory containing the go source code eg. -d ./v1.2/")]
        public string WorkingDirectory { get; set; }

        [Argument(ShortName = "-gp", LongName = "-gopath", EnvVar = "GHI_GO_PATH", Help = "The target directory for the go path to be used eg. -gp ./go")]
        public string GoPath { get; set; }

        [Argument(ShortName = "-o", LongName = "-output", Help = "The path for the output binary, relative to -d eg. -o ../my-shiny-new-go-binary")]
        public string OutputBinary { get; set; }

        [Argument(ShortName = "-t", LongName = "-timeout", Help = "The timeout for how long the go build process should take in minutes eg. -t 120")]
        public int TimeoutInMinutes { get; set; } = 120;

    }
}