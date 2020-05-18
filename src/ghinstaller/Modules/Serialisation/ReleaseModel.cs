using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ghinstaller.Modules.Serialisation
{
    public class ReleaseModel
    {
        [JsonPropertyName("tag_name")]
        public string TagName { get; set; }
        
        [JsonPropertyName("url")]
        public string Url { get; set; }
        
        [JsonPropertyName("assets_url")]
        public string AssetsUrl { get; set; }
        
        [JsonPropertyName("assets")]
        public List<AssetModel> Assets { get; set; }
        
        [JsonPropertyName("zipball_url")]
        public string ZipBallUrl { get; set; }

        [JsonPropertyName("tarball_url")]
        public string TarBallUrl { get; set; }

        public override string ToString()
        {
            return $"{nameof(TagName)}: {TagName}, {nameof(Url)}: {Url}, {nameof(AssetsUrl)}: {AssetsUrl}";
        }
    }
}