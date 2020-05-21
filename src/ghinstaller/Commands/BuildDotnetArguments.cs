using ghinstaller.Modules.Commands.Options;

namespace ghinstaller.Commands
{
    [Command("build-dotnet")]
    public class BuildDotnetArguments
    {
        [Argument(ShortName = "-b", LongName = "-binary", EnvVar = "GHI_DOTNET_BINARY", Help = "The path to the dotnet binary or executable eg. -b /usr/bin/dotnet")]
        public string Binary { get; set; }
        
        [Argument(ShortName = "-a", LongName = "-arguments", EnvVar = "GHI_DOTNET_ARGS", Help = "The parameters for the dotnet binary or executable eg. -a build or -a restore or -a publish")]
        public string Arguments { get; set; }

        [Argument(ShortName = "-d", LongName = "-directory", EnvVar = "GHI_DOTNET_DIRECTORY", Help = "The parameters for the working directory containing the dotnet source code eg. -d ./v1.2/")]
        public string WorkingDirectory { get; set; }
        
        [Argument(ShortName = "-o", LongName = "-output", Help = "The path for the binary output, relative to -d eg. -o ../my-shiny-new-binary-output-drectory/")]
        public string OutputPath { get; set; }
        
        [Argument(ShortName = "-t", LongName = "-timeout", Help = "The timeout for how long the go build process should take in minutes eg. -t 120")]
        public int TimeoutInMinutes { get; set; } = 120;
    }
}