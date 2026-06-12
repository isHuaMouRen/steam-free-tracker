using SteamFreeTracker.Classes.Json;
using System.IO;

namespace SteamFreeTracker.Classes
{
    public static class Globals
    {
        public static readonly string Version = "1.1.0";
        public static readonly string ExecutePath = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly string ConfigPath = Path.Combine(ExecutePath, "config.json");

        public static class APIs
        {
            public const string FreeGamesAPI = "https://store.steampowered.com/search/results/?maxprice=free&specials=1&supportedlang=schinese&json=1";
            public const string AppInfoAPI = "https://store.steampowered.com/api/appdetails?cc=cn&l=zh-cn&appids=";//需要在尾部补充APPID
            public const string AppHeaderAPI = "https://shared.fastly.steamstatic.com/store_item_assets/steam/apps/{0}/header.jpg";
        }

        public static class Resources
        {
            public static readonly string NoticeShow = Path.Combine(ExecutePath, "Resources", "Sounds", "notice_show.wav");
        }

        //免费游戏
        public static Dictionary<string, JsonAppInfo.AppInfo> FreeGames = new Dictionary<string, JsonAppInfo.AppInfo>();

        public static JsonConfig.Root Config = new JsonConfig.Root();
    }
}
