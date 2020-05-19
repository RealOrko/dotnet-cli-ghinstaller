using System.IO;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Tar;
using ICSharpCode.SharpZipLib.Zip;

namespace ghinstaller.Modules.Archiving
{
    public class Archive
    {
        public static void ExtractTar(string tarArchive, string destinationFolder)
        {
            using var inputStream = File.OpenRead(tarArchive);

            using var archive = TarArchive.CreateInputTarArchive(inputStream);
            archive.ExtractContents(destinationFolder);
            archive.Close();

            inputStream.Close();
        }
        
        public static void ExtractZip(string zipArchive, string destinationFolder)
        {
            using var inputStream = File.OpenRead(zipArchive);
            using var archive = new ZipInputStream(inputStream);

            while (archive.GetNextEntry() is ZipEntry zipEntry)
            {
                var buffer = new byte[4096];

                var fullUnzippedPath = Path.Combine(destinationFolder, zipEntry.Name);
                var fullUnzippedDirectory = Path.GetDirectoryName(fullUnzippedPath);
                if (fullUnzippedDirectory.Length > 0)
                {
                    Directory.CreateDirectory(fullUnzippedDirectory);
                }

                if (Path.GetFileName(fullUnzippedPath).Length == 0)
                {
                    continue;
                }

                using (var streamWriter = File.Create(fullUnzippedPath))
                {
                    StreamUtils.Copy(inputStream, streamWriter, buffer);
                }
            }
            
            archive.Close();
            inputStream.Close();
        }
    }
}