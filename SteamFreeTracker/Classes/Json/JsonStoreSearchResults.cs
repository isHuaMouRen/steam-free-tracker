using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace SteamFreeTracker.Classes.Json
{
    public class JsonStoreSearchResults
    {
        [JsonProperty("desc")]
        public required string Description { get; set; }

        [JsonProperty("items")]
        public required ResultInfo[] Items { get; set; }

        
        public class ResultInfo
        {
            [JsonProperty("name")]
            public required string Name { get; set; }
            
            [JsonProperty("logo")]
            public required string Logo { get; set; }

            //自动解析Logo字符串内包含的appid
            [JsonIgnore]
            public string? AppId
            {
                get
                {
                    if (string.IsNullOrEmpty(Logo)) return null;
                    var match = Regex.Match(Logo, @"apps/(\d+)");
                    return match.Success ? match.Groups[1].Value : null;
                }
            }
        }
    }
}
