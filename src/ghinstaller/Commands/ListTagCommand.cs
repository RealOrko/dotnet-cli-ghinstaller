using System;
using System.Linq;
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
                if (!string.IsNullOrEmpty(args.Find))
                {
                    tags = tags.Where(x => x.Name.Contains(args.Find)).ToList();

                    if (tags.Count == 0)
                    {
                        Console.WriteLine("Not found");
                        return -1;
                    }
                }
                
                foreach (var tag in tags)
                {
                    if (args.NameOnly)
                    {
                        Console.WriteLine($"{tag.Name}");
                    }
                
                    if (args.TarballOnly)
                    {
                        Console.WriteLine($"{tag.TarBallUrl}");
                    }
                
                    if (args.ZipballOnly)
                    {
                        Console.WriteLine($"{tag.ZipBallUrl}");
                    }
                
                    if (!args.NameOnly && !args.TarballOnly && !args.ZipballOnly)
                    {
                        Console.WriteLine($"{tag.Name}");
                    }
                }
                
                return 0;
            }
            
            return -1;
        }
    }
}