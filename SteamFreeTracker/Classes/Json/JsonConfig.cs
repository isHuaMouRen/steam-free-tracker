using Newtonsoft.Json;

namespace SteamFreeTracker.Classes.Json
{
    public class JsonConfig
    {
        public class Root
        {
            [JsonProperty("ignore-games")]
            public List<string> IgnoreGames { get; set; } = new List<string>();
        }
    }
}
