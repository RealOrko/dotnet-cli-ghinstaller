using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ghinstaller.Modules.Archiving;
using ghinstaller.Modules.Commands.Options;
using ghinstaller.Modules.Commands.Routing;

namespace ghinstaller.Commands
{
    [Binds(typeof(UntarArguments))]
    public class UntarCommand
    {
        public UntarCommand(CommandContext context)
        {
            Context = context;
        }

        public CommandContext Context { get; }

        public int Execute(UntarArguments args)
        {
            var tarArchivePaths = new List<string>();
            tarArchivePaths.AddRange(Directory.GetFiles(AppContext.BaseDirectory, "*.tar"));

            if (string.IsNullOrEmpty(args.TarArhivePath))
            {
                Console.WriteLine("The -t parameter is empty scanning local folder for '*.tar' ... ");
                if (tarArchivePaths.Count == 0)
                {
                    Console.WriteLine($"No tar files found in '{AppContext.BaseDirectory}'");
                    CommandParser.Info(typeof(UntarCommand));
                    return -1;
                }
                
                if (tarArchivePaths.Count > 1)
                {
                    PrintValidInputArchives(tarArchivePaths);
                    CommandParser.Info(typeof(UntarCommand));
                    return -1;
                }
            }
            
            if (!string.IsNullOrEmpty(args.TarArhivePath))
            {
                tarArchivePaths.Clear();
                tarArchivePaths.Add(args.TarArhivePath);
            }

            if (tarArchivePaths.Count == 1)
            {
                var tarArchivePath = tarArchivePaths[0];
                if (!File.Exists(tarArchivePath))
                {
                    Console.WriteLine($"{tarArchivePath} is not a valid input file");
                    var helpInputFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.tar").ToList();
                    if (helpInputFiles.Count == 0)
                    {
                        CommandParser.Info(typeof(UntarCommand));
                        return -1;
                    }
                    
                    PrintValidInputArchives(helpInputFiles);
                    CommandParser.Info(typeof(UntarCommand));
                    return -1;
                }
                
                Console.WriteLine($"Found {tarArchivePath.Replace(AppContext.BaseDirectory, "./")}");
                if (string.IsNullOrEmpty(args.OutputDirectory))
                {
                    var outputDirectory = Path.GetFileNameWithoutExtension(tarArchivePaths[0]);
                    Console.WriteLine($"The -o parameter is empty, assuming folder {outputDirectory}");
                    Archive.ExtractTar(tarArchivePaths[0], outputDirectory);
                    return 0;
                }
                
                Archive.ExtractTar(tarArchivePaths[0], args.OutputDirectory);
                return 0;
            }
                
            CommandParser.Info(typeof(UntarCommand));
            return -1;
        }

        private static void PrintValidInputArchives(List<string> tarArchivePaths)
        {
            Console.WriteLine("Found multiple archives, please supply one of the following as parameters:");
            foreach (var file in tarArchivePaths)
            {
                Console.WriteLine($"\t -t {file.Replace(AppContext.BaseDirectory, "./")}");
            }
        }
    }
}