using System;
using System.IO;
using ghinstaller.Modules.Commands.Options;
using ghinstaller.Modules.Commands.Routing;
using ghinstaller.Modules.Process;
using OperatingSystem = ghinstaller.Modules.Process.OperatingSystem;

namespace ghinstaller.Commands
{
    [Binds(typeof(InstallArguments))]
    public class InstallCommand
    {
        public InstallCommand(CommandContext context)
        {
            Context = context;
        }

        public CommandContext Context { get; }

        public int Execute(InstallArguments args)
        {
            if (!args.IsValid())
            {
                CommandParser.Info(typeof(InstallCommand));
                return -1;
            }

            if (!File.Exists(args.Binary))
            {
                Console.WriteLine($"Could not find binary {args.Binary}");
                CommandParser.Info(typeof(InstallCommand));
                return -1;
            }

            var installDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".ghi/");
            var installBinary = Path.Combine(installDirectory, Path.GetFileName(args.Binary));

            if (!Directory.Exists(installDirectory))
            {
                Directory.CreateDirectory(installDirectory);
            }

            if (File.Exists(installBinary))
            {
                File.Delete(installBinary);
            }
            
            File.Copy(args.Binary, installBinary);

            Console.WriteLine($"{args.Binary} installed to {installBinary}.");
            Console.WriteLine($"Please be sure to add {installDirectory} to your path.");
            return 0;
        }
    }
}