using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using ghinstaller.Modules.Serialisation;
using ghinstaller.Modules.Serialisation.Interfaces;
using ghinstaller.Modules.Versioning;

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
        }

        public static List<ReleaseModel> GetReleases(string owner, string repository)
        {
            if (owner == null) throw new ArgumentNullException(nameof(owner));
            if (repository == null) throw new ArgumentNullException(nameof(repository));
            
            var uri = $"repos/{owner}/{repository}/releases";

            var response = client
                .GetAsync(uri)
                .GetAwaiter()
                .GetResult();

            return Deserialize<List<ReleaseModel>>(response);
        }
        
        public static ReleaseModel GetLatestRelease(string owner, string repository)
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

            return Deserialize<List<TagModel>>(response);
        }
        
        public static List<AssetModel> GetAssets(IHaveAssetsUrl assetsUrl)
        {
            if (assetsUrl == null) throw new ArgumentNullException(nameof(assetsUrl));
            
            var response = client
                .GetAsync(assetsUrl.AssetsUrl)
                .GetAwaiter()
                .GetResult();

            return Deserialize<List<AssetModel>>(response);
        }
        
        public static string Download(string url, string targetFileName)
        {
            var clientArgs = CreateNewClient((h) => h.ClientCertificateOptions = ClientCertificateOption.Automatic);
            using var httpClient = clientArgs.Item2;
            httpClient.DefaultRequestHeaders.ExpectContinue = true;

            var fileInfo = new FileInfo($"{targetFileName}");
            using var file = File.Create(fileInfo.FullName);

            var response = clientArgs.Item2.GetAsync($"{url}", HttpCompletionOption.ResponseHeadersRead).GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();

            int counter = 0;
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
                counter++;

                if (counter % 200 == 0)
                {
                    Console.SetCursorPosition(0, Console.CursorTop);
                    Console.Write($"{targetFileName}: {totalBytesReadSoFar/1024}/{totalBytes/1024} KB");
                }
            }

            Console.SetCursorPosition(0, Console.CursorTop);
            Console.WriteLine($"{targetFileName}: {totalBytesReadSoFar/1024}/{totalBytes/1024} KB");
            
            return fileInfo.FullName;
        }

        private static string DownloadSimple(string url, string targetFileName)
        {
            var clientArgs = CreateNewClient();
            using var httpClient = clientArgs.Item2;

            var fileInfo = new FileInfo($"{targetFileName}");
            var response = httpClient.GetAsync($"{url}").GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();
            
            using var ms = response.Content.ReadAsStreamAsync().GetAwaiter().GetResult();
            using var fs = File.Create(fileInfo.FullName);
            ms.Seek(0, SeekOrigin.Begin);
            ms.CopyTo(fs);

            return fileInfo.FullName;
        }
        
        private static (HttpClientHandler, HttpClient) CreateNewClient(Action<HttpClientHandler> handlerCallback = null)
        {
            var httpHandler = new HttpClientHandler();
            handlerCallback?.Invoke(httpHandler);

            var httpClient = new HttpClient(httpHandler);
            httpClient.BaseAddress = new Uri("https://api.github.com");
            httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("ghinstaller", Info.GetVersion()));
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.inertia-preview+json"));
            return (httpHandler, httpClient);
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