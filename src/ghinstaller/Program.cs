using System;

namespace ghinstaller
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        static void PrintUsage()
        {
            Console.WriteLine("dotnet cli ghinstaller");
            Console.WriteLine("----------------------");
            Console.WriteLine("Usage:");
            Console.WriteLine("   > ghi github.com cloundfoundry bosh-bootloader");
        }
    }
}
