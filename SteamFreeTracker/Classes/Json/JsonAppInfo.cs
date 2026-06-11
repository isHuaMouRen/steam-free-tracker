using Newtonsoft.Json;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Media.Imaging;

namespace SteamFreeTracker.Classes.Json
{
    public class JsonAppInfo
    {
        public class AppInfo
        {
            [JsonProperty("success")]
            public required bool Success { get; set; }

            [JsonProperty("data")]
            public required AppData Data { get; set; }
        }

        public class AppData
        {
            [JsonProperty("type")]
            public required string Type { get; set; }

            [JsonProperty("name")]
            public required string Name { get; set; }

            [JsonProperty("steam_appid")]
            public required long SteamAppid { get; set; }

            [JsonProperty("is_free")]
            public required bool IsFree { get; set; }

            [JsonProperty("about_the_game")]
            public required string AboutTheGame { get; set; }

            [JsonIgnore]
            public string? AboutTheGameNoHTML
            {
                get
                {
                    if (string.IsNullOrEmpty(AboutTheGame)) return null;
                    var nohtml = Regex.Replace(AboutTheGame, "<[^>]*>", string.Empty);
                    return WebUtility.HtmlDecode(nohtml);
                }
            }

            [JsonProperty("price_overview")]
            public PriceInfo? PriceOverview { get; set; }

            [JsonIgnore]
            public BitmapImage? Header { get; set; }//需要手动获取并填充
        }

        public class PriceInfo
        {
            [JsonProperty("initial")]
            public required int Initial { get; set; }

            [JsonProperty("final")]
            public required int Final { get; set; }

            [JsonProperty("discount_percent")]
            public required int DiscountPercent { get; set; }
        }
    }
}