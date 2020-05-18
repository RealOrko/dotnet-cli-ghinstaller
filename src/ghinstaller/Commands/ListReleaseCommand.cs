using System;
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

            var release = GitHubClient.ListReleases(args.Owner, args.Repository);

            if (release == null)
            {
                return -1;
            }
            
            if (args.TarballOnly)
            {
                Console.WriteLine($"{release.TarBallUrl}");
                return 0;
            }
            
            if (args.ZipballOnly)
            {
                Console.WriteLine($"{release.ZipBallUrl}");
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
                    Console.WriteLine($"{asset.Name}");
                }

                return 0;
            }

            if (release.Assets == null || release.Assets.Count == 0)
            {
                Console.WriteLine($"{release.TarBallUrl}");
                Console.WriteLine($"{release.ZipBallUrl}");
            }
            else
            {
                foreach (var asset in release.Assets)
                {
                    Console.WriteLine($"{asset.Name}");
                }
            }
            
            return 0;
        }
    }
}