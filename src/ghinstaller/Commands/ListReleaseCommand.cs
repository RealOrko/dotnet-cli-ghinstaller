using System;
using System.Linq;
using ghinstaller.Modules.Commands.Options;
using ghinstaller.Modules.Commands.Routing;
using ghinstaller.Modules.Http;

namespace ghinstaller.Commands
{
    [Binds(typeof(ListReleaseArguments))]
    public class ListReleaseCommand
    {
        public ListReleaseCommand(CommandContext context)
        {
            Context = context;
        }

        public CommandContext Context { get; }

        public int Execute(ListReleaseArguments args)
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

                if (releases.Count > 1 && args.AssetsOnly)
                {
                    Console.WriteLine($"Rate limit check failed. Please use the -f option to target a specific release.");
                    CommandParser.Info(typeof(ListReleaseCommand));
                    return -1;
                }
                
                foreach (var release in releases)
                {
                    if (args.NameOnly)
                    {
                        Console.WriteLine($"{release.Name}");
                    }
                
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
                            if (args.AssetsNameOnly)
                            {
                                Console.WriteLine($"{asset.Name}");
                            }

                            if (args.AssetsUrlOnly)
                            {
                                Console.WriteLine($"{asset.DownloadUrl}");
                            }

                            if (!args.AssetsNameOnly && !args.AssetsUrlOnly)
                            {
                                Console.WriteLine($"{asset.Name}");
                            }
                        }
                    }
                    
                    if (args.TarballOnly)
                    {
                        Console.WriteLine($"{release.TarBallUrl}");
                    }
                
                    if (args.ZipballOnly)
                    {
                        Console.WriteLine($"{release.ZipBallUrl}");
                    }
                
                    if (!args.NameOnly && !args.AssetsOnly && !args.TarballOnly && !args.ZipballOnly)
                    {
                        Console.WriteLine($"{release.Name}");
                    }
                }
                
                return 0;
            }
            
            return -1;
        }
    }
}