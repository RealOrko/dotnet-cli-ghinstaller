using System;
using ghinstaller.Modules.Commands.Options;
using ghinstaller.Modules.Commands.Routing;
using ghinstaller.Modules.Http;

namespace ghinstaller.Commands
{
    [Binds(typeof(DownloadReleaseArguments))]
    public class DownloadReleaseCommand
    {
        public DownloadReleaseCommand(CommandContext context)
        {
            Context = context;
        }

        public CommandContext Context { get; }

        public int Execute(DownloadReleaseArguments args)
        {
            if (!args.IsValid())
            {
                CommandParser.Info(typeof(DownloadReleaseCommand));
                return -1;
            }

            var release = GitHubClient.GetReleases(args.Owner, args.Repository);
            if (release == null)
            {
                return -1;
            }
            
            if (args.TarballOnly)
            {
                GitHubClient.Download(release.TarBallUrl, $"{release.TagName}.tar");
                return 0;
            }
            
            if (args.ZipballOnly)
            {
                GitHubClient.Download(release.ZipBallUrl, $"{release.TagName}.zip");
                return 0;
            }

            if (args.AssetsOnly)
            {
                if (release.Assets == null || release.Assets.Count == 0)
                {
                    Console.WriteLine("Not found");
                    return -1;
                }

                foreach (var asset in release.Assets)
                {
                    GitHubClient.Download(asset.DownloadUrl, $"{asset.Name}");
                }

                return 0;
            }

            if (release.Assets == null || release.Assets.Count == 0)
            {
                GitHubClient.Download(release.TarBallUrl, $"{release.TagName}.tar");
                GitHubClient.Download(release.ZipBallUrl, $"{release.TagName}.zip");
            }
            else
            {
                foreach (var asset in release.Assets)
                {
                    GitHubClient.Download(asset.DownloadUrl, $"{asset.Name}");
                }
            }
            
            return 0;
        }
    }
}