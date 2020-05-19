using System;
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
                CommandParser.Info(typeof(DownloadTagCommand));
                return -1;
            }

            var tags = GitHubClient.GetTags(args.Owner, args.Repository);

            if (tags != null && tags.Count > 0)
            {
                if (args.TarballOnly)
                {
                    GitHubClient.Download($"{tags[0].TarBallUrl}", $"{tags[0].Name}.tar");
                    return 0;
                }
                    
                if (args.ZipballOnly)
                {
                    GitHubClient.Download($"{tags[0].ZipBallUrl}", $"{tags[0].Name}.zip");
                    return 0;
                }
                    
                GitHubClient.Download($"{tags[0].TarBallUrl}", $"{tags[0].Name}.tar");
                GitHubClient.Download($"{tags[0].ZipBallUrl}", $"{tags[0].Name}.zip");
                return 0;
            }
            
            return -1;
        }
    }
}