using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ghinstaller.Modules.Archiving;
using ghinstaller.Modules.Commands.Options;
using ghinstaller.Modules.Commands.Routing;

namespace ghinstaller.Commands
{
    [Binds(typeof(UnzipArguments))]
    public class UnzipCommand
    {
        public UnzipCommand(CommandContext context)
        {
            Context = context;
        }

        public CommandContext Context { get; }

        public int Execute(UnzipArguments args)
        {
            var zipArchivePaths = new List<string>();
            zipArchivePaths.AddRange(Directory.GetFiles(AppContext.BaseDirectory, "*.zip"));

            if (string.IsNullOrEmpty(args.ZipArhivePath))
            {
                Console.WriteLine("The -z parameter is empty scanning local folder for '*.zip' ... ");
                if (zipArchivePaths.Count == 0)
                {
                    Console.WriteLine($"No zip files found in '{AppContext.BaseDirectory}'");
                    return -1;
                }
                
                if (zipArchivePaths.Count > 1)
                {
                    PrintValidInputArchives(zipArchivePaths);
                    return -1;
                }
            }
            
            if (!string.IsNullOrEmpty(args.ZipArhivePath))
            {
                zipArchivePaths.Clear();
                zipArchivePaths.Add(args.ZipArhivePath);
            }

            if (zipArchivePaths.Count == 1)
            {
                var zipArchivePath = zipArchivePaths[0];
                if (!File.Exists(zipArchivePath))
                {
                    Console.WriteLine($"{zipArchivePath} is not a valid input file");
                    var helpInputFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.zip").ToList();
                    if (helpInputFiles.Count == 0)
                    {
                        return -1;
                    }
                    
                    PrintValidInputArchives(helpInputFiles);
                    return -1;
                }
                
                Console.WriteLine($"Found {zipArchivePath.Replace(AppContext.BaseDirectory, "./")}");
                if (string.IsNullOrEmpty(args.OutputDirectory))
                {
                    var outputDirectory = Path.GetFileNameWithoutExtension(zipArchivePaths[0]);
                    Console.WriteLine($"The -o parameter is empty, assuming folder {outputDirectory}");
                    Archive.ExtractZip(zipArchivePaths[0], outputDirectory);
                    return 0;
                }
                
                Archive.ExtractZip(zipArchivePaths[0], args.OutputDirectory);
                return 0;
            }
                
            return -1;
        }

        private static void PrintValidInputArchives(List<string> zipArchivePaths)
        {
            Console.WriteLine("Found multiple archives, please supply one of the following as parameters:");
            foreach (var file in zipArchivePaths)
            {
                Console.WriteLine($"\t -z {file.Replace(AppContext.BaseDirectory, "./")}");
            }
        }
    }
}