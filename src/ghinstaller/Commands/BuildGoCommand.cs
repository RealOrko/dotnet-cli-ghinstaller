using System;
using System.IO;
using ghinstaller.Modules.Commands.Options;
using ghinstaller.Modules.Commands.Routing;
using ghinstaller.Modules.Process;

namespace ghinstaller.Commands
{
    [Binds(typeof(BuildGoArguments))]
    public class BuildGoCommand
    {
        public BuildGoCommand(CommandContext context)
        {
            Context = context;
        }

        public CommandContext Context { get; }

        public int Execute(BuildGoArguments args)
        {
            Console.WriteLine("To install Go, please see: https://golang.org/doc/install");

            if (string.IsNullOrEmpty(args.Binary))
            {
                args.Binary = "go";
            }

            if (string.IsNullOrEmpty(args.Arguments))
            {
                args.Arguments = "build";
            }

            if (string.IsNullOrEmpty(args.WorkingDirectory))
            {
                args.WorkingDirectory = AppContext.BaseDirectory;
            }

            if (string.IsNullOrEmpty(args.GoPath))
            {
                args.GoPath = Path.Combine(AppContext.BaseDirectory, ".go/");
            }

            var process = new Process()
                .SetWorkingDirectory(args.WorkingDirectory)
                .SetTimeout(TimeSpan.FromMinutes(args.TimeoutInMinutes))
                .SetEnvironmentVariable("GOPATH", args.GoPath)
                .SetEnvironmentVariable("GOOS", "linux")
                .SetEnvironmentVariable("GOARCH", "amd64")
                .SetEnvironmentVariable("CGO_ENABLED", "1")
                .SetEnvironmentVariable("PATH", Environment.GetEnvironmentVariable("PATH"));

            ProcessResult result = null;

            if (string.IsNullOrEmpty(args.OutputBinary))
            {
                result = process.Execute(args.Binary, args.Arguments);
            }
            else
            {
                result = process.Execute(args.Binary, args.Arguments, "-o", args.OutputBinary);
            }

            if (result.ExitCode != 0)
            {
                CommandParser.Info(typeof(BuildGoCommand));
                Console.WriteLine(result.ErrorOutput);                
            }
            
            return result.ExitCode;
        }
    }
}