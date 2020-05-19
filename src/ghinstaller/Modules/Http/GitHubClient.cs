using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using ghinstaller.Modules.Serialisation;

namespace ghinstaller.Modules.Http
{
    public class GitHubClient
    {
        private static HttpClient client = new HttpClient();

        static GitHubClient()
        {
            client.BaseAddress = new Uri("https://api.github.com");
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("ghinstaller", "0.0.1"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.inertia-preview+json"));
        }

        public static ReleaseModel ListReleases(string owner, string repository)
        {
            if (owner == null) throw new ArgumentNullException(nameof(owner));
            if (repository == null) throw new ArgumentNullException(nameof(repository));
            
            var uri = $"repos/{owner}/{repository}/releases/latest";

            var response = client
                .GetAsync(uri)
                .GetAwaiter()
                .GetResult();

            return Deserialize<ReleaseModel>(response);
        }
        
        public static List<TagModel> ListTags(string owner, string repository)
        {
            if (owner == null) throw new ArgumentNullException(nameof(owner));
            if (repository == null) throw new ArgumentNullException(nameof(repository));
            
            var uri = $"repos/{owner}/{repository}/tags";

            var response = client
                .GetAsync(uri)
                .GetAwaiter()
                .GetResult();

            return Deserialize<List<TagModel>>(response);
        }
        
        private static T Deserialize<T>(HttpResponseMessage response, bool dumpContent = false)
        {
            var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            if (dumpContent)
            {
                Console.WriteLine(content);
            }

            if (!response.IsSuccessStatusCode)
            {
                var error = JsonSerializer.Deserialize<ErrorModel>(content);
                Console.WriteLine($"{error.Message}, please see {error.Url}");
                return default(T);
            }
            
            return JsonSerializer.Deserialize<T>(content);
        }
    }
}