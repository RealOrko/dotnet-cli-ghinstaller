using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ghinstaller
{
    class Program
    {
        const int OrgOrdinal = 0;
        private const string GitHubUri = "https://api.github.com";
        
        private static HttpClient client = new HttpClient();
        
        static void Main(string[] args)
        {
            if (args.Length == 0 || args[0] == "--help" || args[0] == "/?" || args[0] == "?" || args.Length < 1)
            {
                PrintUsage();
                return;
            }

            client.BaseAddress = new Uri(GitHubUri);
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("ghinstaller", "0.0.1"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.inertia-preview+json"));
            
            var orgValue = args[OrgOrdinal];

            if (args.Length == 1)
            {
                ListProjectsForOrganisation(orgValue);
            }
            
        }

        public static void ListProjectsForOrganisation(string organisation)
        {
            if (organisation == null) throw new ArgumentNullException(nameof(organisation));

            var response = client
                .GetAsync($"/orgs/{organisation}/projects/")
                .GetAwaiter()
                .GetResult();
            
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
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
