using System;
using ghinstaller.Modules.Commands.Options;
using ghinstaller.Modules.Commands.Routing;
using ghinstaller.Modules.Http;

namespace ghinstaller.Commands
{
    [Binds(typeof(ListTagArguments))]
    public class ListTagCommand
    {
        public ListTagCommand(CommandContext context)
        {
            Context = context;
        }

        public CommandContext Context { get; }

        public int Execute(ListTagArguments args)
        {
            if (!args.IsValid())
            {
                CommandParser.Info(typeof(ListTagCommand));
                return -1;
            }

            var tags = GitHubClient.GetTags(args.Owner, args.Repository);

            if (tags != null && tags.Count > 0)
            {
                if (args.TarballOnly)
                {
                    Console.WriteLine($"{tags[0].TarBallUrl}");
                    return 0;
                }
                    
                if (args.ZipballOnly)
                {
                    Console.WriteLine($"{tags[0].ZipBallUrl}");
                    return 0;
                }
                    
                Console.WriteLine($"{tags[0].TarBallUrl}");
                Console.WriteLine($"{tags[0].ZipBallUrl}");
                return 0;
            }
            
            return -1;
        }
    }
}