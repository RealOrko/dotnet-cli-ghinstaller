using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using ghinstaller.Modules.Serialisation;

namespace ghinstaller.Modules.Http
{
    public class GitHubClient
    {
        private static HttpClient client;
        private static HttpClientHandler handler;

        static GitHubClient()
        {
            var args = CreateNewClient();
            client = args.Item2;
            handler = args.Item1;

            client.BaseAddress = new Uri("https://api.github.com");
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("ghinstaller", "0.0.1"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.inertia-preview+json"));
        }

        public static ReleaseModel GetReleases(string owner, string repository)
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
        
        public static List<TagModel> GetTags(string owner, string repository)
        {
            if (owner == null) throw new ArgumentNullException(nameof(owner));
            if (repository == null) throw new ArgumentNullException(nameof(repository));
            
            var uri = $"repos/{owner}/{repository}/tags";

            var response = client
                .GetAsync(uri)
                .GetAwaiter()
                .GetResult();

            return Deserialize<List<TagModel>>(response, true);
        }
        
        public static string Download(string url, string targetFileName)
        {
            Console.WriteLine($"{url}:");

            var clientArgs = CreateNewClient((h) => h.ClientCertificateOptions = ClientCertificateOption.Automatic);
            using var httpClient = clientArgs.Item2;
            httpClient.DefaultRequestHeaders.ExpectContinue = true;
            
            var fileInfo = new FileInfo($"{targetFileName}");
            using var file = File.Create(fileInfo.FullName);
            Console.Write($"\r{targetFileName}: 0%");

            var response = clientArgs.Item2.GetAsync($"{url}", HttpCompletionOption.ResponseHeadersRead).GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();

            int totalBytesReadSoFar = 0;
            const int maxBytesReadableForBuffer = 8192;
            byte[] buffer = new byte[maxBytesReadableForBuffer];
            var totalBytes = response.Content.Headers.ContentLength;
            using var stream = response.Content.ReadAsStreamAsync().GetAwaiter().GetResult();

            while (true)
            {
                var bytesRead = stream.Read(buffer, 0, maxBytesReadableForBuffer);
                if (bytesRead == 0)
                {
                    break;
                }
                
                file.Write(buffer, 0, bytesRead);
                totalBytesReadSoFar += bytesRead;
                
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write($"\r{targetFileName}: {(totalBytesReadSoFar/totalBytes) * 100}%");
            }
            
            Console.WriteLine();
            
            return fileInfo.FullName;
        }
        
        private static (HttpClientHandler, HttpClient) CreateNewClient(Action<HttpClientHandler> handlerCallback = null)
        {
            var handler = new HttpClientHandler();
            handlerCallback?.Invoke(handler);

            var client = new HttpClient(handler);
            return (handler, client);
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