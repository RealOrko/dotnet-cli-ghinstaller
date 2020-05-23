using System;
using System.IO;
using ghinstaller.Modules.Commands.Options;
using ghinstaller.Modules.Commands.Routing;
using ghinstaller.Modules.Process;

namespace ghinstaller.Commands
{
    [Binds(typeof(BuildDotnetArguments))]
    public class BuildDotnetCommand
    {
        public BuildDotnetCommand(CommandContext context)
        {
            Context = context;
        }

        public CommandContext Context { get; }

        public int Execute(BuildDotnetArguments args)
        {
            Console.WriteLine("To install DOTNET Core, please see: https://dotnet.microsoft.com/download");

            if (string.IsNullOrEmpty(args.Binary))
            {
                args.Binary = "dotnet";
            }

            if (string.IsNullOrEmpty(args.Arguments))
            {
                args.Arguments = "publish";
            }

            if (string.IsNullOrEmpty(args.WorkingDirectory))
            {
                args.WorkingDirectory = AppContext.BaseDirectory;
            }

            var process = new Process()
                .SetWorkingDirectory(args.WorkingDirectory)
                .SetTimeout(TimeSpan.FromMinutes(args.TimeoutInMinutes))
                .SetEnvironmentVariable("PATH", Environment.GetEnvironmentVariable("PATH"));

            var result = ProcessResult.Error(string.Empty);

            if (string.IsNullOrEmpty(args.OutputPath))
            {
                result = process.Execute(args.Binary, args.Arguments);
            }
            else
            {
                result = process.Execute(args.Binary, args.Arguments, "-o", args.OutputPath);
            }

            if (result.ExitCode != 0)
            {
                CommandParser.Info(typeof(BuildDotnetCommand));
                Console.WriteLine(result.ErrorOutput);                
            }
            
            return result.ExitCode;
        }
    }
}