using System;
using System.Linq;
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
                CommandParser.Info(typeof(ListReleaseCommand));
                return -1;
            }

            var releases = GitHubClient
                .GetReleases(args.Owner, args.Repository)
                .OrderByDescending(x => x.CreatedAt)
                .ToList();

            if (releases != null && releases.Count > 0)
            {
                if (!string.IsNullOrEmpty(args.Find))
                {
                    releases = releases.Where(x => x.Name.Contains(args.Find)).ToList();

                    if (releases.Count == 0)
                    {
                        Console.WriteLine("Not found");
                        return -1;
                    }
                }

                if (releases.Count > 1)
                {
                    Console.WriteLine($"Rate limit check failed. Please use the -f option to target a specific release or look at 'list-release' command to find one.");
                    CommandParser.Info(typeof(DownloadReleaseCommand));
                    CommandParser.Info(typeof(ListReleaseCommand));
                    return -1;
                }
                
                if (releases.Count > 1 && args.AssetsOnly)
                {
                    Console.WriteLine($"Rate limit check failed. Please use the -f option to target a specific release or look at 'list-release' command to find one.");
                    CommandParser.Info(typeof(DownloadReleaseCommand));
                    CommandParser.Info(typeof(ListReleaseCommand));
                    return -1;
                }
                
                foreach (var release in releases)
                {
                    if (args.AssetsOnly)
                    {
                        var assets = GitHubClient.GetAssets(release);

                        if (!string.IsNullOrEmpty(args.AssetsFind))
                        {
                            assets = assets.Where(x => x.Name.Contains(args.AssetsFind)).ToList();
                        }

                        if (assets == null || assets.Count == 0)
                        {
                            Console.WriteLine("No assets found");
                        }

                        foreach (var asset in assets)
                        {
                            GitHubClient.Download(asset.DownloadUrl, asset.Name);
                        }
                    }
                    
                    if (args.TarballOnly)
                    {
                        GitHubClient.Download(release.TarBallUrl, $"{release.Name}.tar");
                    }
                
                    if (args.ZipballOnly)
                    {
                        GitHubClient.Download(release.ZipBallUrl, $"{release.Name}.zip");
                    }
                
                    if (!args.AssetsOnly && !args.TarballOnly && !args.ZipballOnly)
                    {
                        GitHubClient.Download(release.TarBallUrl, $"{release.Name}.tar");
                        GitHubClient.Download(release.ZipBallUrl, $"{release.Name}.zip");
                    }
                }
                
                return 0;
            }
            
            return -1;
        }
    }
}