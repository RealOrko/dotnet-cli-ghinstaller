using System;
using System.Linq;
using ghinstaller.Modules.Commands.Options;
using ghinstaller.Modules.Commands.Routing;
using ghinstaller.Modules.Http;

namespace ghinstaller.Commands
{
    [Binds(typeof(DownloadTagArguments))]
    public class DownloadTagCommand
    {
        public DownloadTagCommand(CommandContext context)
        {
            Context = context;
        }

        public CommandContext Context { get; }

        public int Execute(DownloadTagArguments args)
        {
            if (!args.IsValid())
            {
                CommandParser.Info(typeof(ListTagCommand));
                return -1;
            }

            var tags = GitHubClient.GetTags(args.Owner, args.Repository);

            if (tags != null && tags.Count > 0)
            {
                if (!string.IsNullOrEmpty(args.Find))
                {
                    tags = tags.Where(x => x.Name.Contains(args.Find)).ToList();

                    if (tags.Count == 0)
                    {
                        Console.WriteLine("Not found");
                        return -1;
                    }
                }
                
                if (tags.Count > 1)
                {
                    Console.WriteLine($"Rate limit check failed. Please use the -f option to target a specific tag or look at 'list-tag' command to find one.");
                    CommandParser.Info(typeof(DownloadTagCommand));
                    CommandParser.Info(typeof(ListTagCommand));
                    return -1;
                }
                
                foreach (var tag in tags)
                {
                    if (args.TarballOnly)
                    {
                        GitHubClient.Download(tag.TarBallUrl, $"{tag.Name}.tar");
                    }
                
                    if (args.ZipballOnly)
                    {
                        GitHubClient.Download(tag.ZipBallUrl, $"{tag.Name}.zip");
                    }
                
                    if (!args.TarballOnly && !args.ZipballOnly)
                    {
                        GitHubClient.Download(tag.TarBallUrl, $"{tag.Name}.tar");
                        GitHubClient.Download(tag.ZipBallUrl, $"{tag.Name}.zip");
                    }
                }
                
                return 0;
            }
            
            return -1;
        }
    }
}