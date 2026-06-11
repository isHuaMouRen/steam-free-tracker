using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

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
