using System;
using System.Diagnostics;
using System.Net.Http;
using ghinstaller.Modules.Commands.Options;
using ghinstaller.Modules.Commands.Routing;
using ghinstaller.Modules.Formatters;
using ghinstaller.Modules.Versioning;

namespace ghinstaller
{
    class Program
    {
        private static CommandRouter _router = new CommandRouter();
        
        static int Main(string[] args)
        {
            if (args.Length == 0 || args[0] == "--help" || args[0] == "/?" || args[0] == "?")
            {
                PrintUsage();
                return 1;
            }

            var result = 0;

            try
            {
                var dispatcher = _router.Route(args);
                if (dispatcher == null)
                {
                    PrintUsage();
                    return 1;
                }

                result = dispatcher.Execute();

            }
            catch (Exception err)
            {
                using (new InColour(ConsoleColor.Red, ConsoleColor.Black))
                {
                    Console.WriteLine(err.ToString());
                    Console.WriteLine();
                    Console.WriteLine(err.InnerException?.Message);
                    Console.WriteLine("Exiting with code -1");
                    result = -1;
                }
            }

            return result;
        }


        static void PrintUsage()
        {
            Console.WriteLine($"ghi v{Info.GetVersion()} by realorko \r\n");
            CommandParser.InfoAll();
        }
    }
}
