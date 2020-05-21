using System;
using System.IO;
using System.IO.Compression;
using SharpCompress.Common;
using SharpCompress.Readers;

namespace ghinstaller.Modules.Archiving
{
    public class Archive
    {
        public static void ExtractZip(string zipArchive, string destinationFolder)
        {
            ZipFile.ExtractToDirectory(zipArchive, destinationFolder, overwriteFiles:true);
        }
        
        public static void ExtractTar(string tarArchive, string destinationFolder)
        {
            using (Stream stream = File.OpenRead(tarArchive))
            {
                var reader = ReaderFactory.Open(stream);
                while (reader.MoveToNextEntry())
                {
                    if (!reader.Entry.IsDirectory)
                    {
                        var opt = new ExtractionOptions {
                            ExtractFullPath = true,
                            Overwrite = true,
                            WriteSymbolicLink = 
                                (sourcePath, targetPath) =>
                                {
                                    Console.WriteLine($"Could not write symlink {sourcePath} -> {targetPath}, for more information please see https://github.com/dotnet/runtime/issues/24271");
                                }
                        };
                        
                        reader.WriteEntryToDirectory(destinationFolder, opt);
                    }
                }
            }
        }    }
}