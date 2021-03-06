using System.Text.Json.Serialization;

namespace ghinstaller.Modules.Serialisation
{
    public class ErrorModel
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }
        
        [JsonPropertyName("documentation_url")]
        public string Url { get; set; }
    }
}