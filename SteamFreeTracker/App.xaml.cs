using Newtonsoft.Json;
using SteamFreeTracker.Classes;
using SteamFreeTracker.Classes.Json;
using SteamFreeTracker.Utils;
using SteamFreeTracker.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SteamFreeTracker
{
    public partial class App : Application
    {
        private async void Application_Startup(object sender, StartupEventArgs e)
        {
            using var client = new HttpClient();

            JsonStoreSearchResults? searchResult = null;
            try
            {
                // 获取免费游戏列表
                var rawJson = await client.GetStringAsync(Globals.APIs.FreeGamesAPI);
                searchResult = JsonConvert.DeserializeObject<JsonStoreSearchResults>(rawJson);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"初始化游戏列表失败\n{ex}", "启动失败");
                Application.Current.Shutdown(1);
            }

            //无免费游戏，直接退出
            if (searchResult?.Items == null || searchResult.Items.Length == 0)
            {
                Application.Current.Shutdown(0);
                return;
            }

            var tasks = searchResult.Items
                .Select(game => game.AppId)
                .Where(appid => !string.IsNullOrEmpty(appid)) // 过滤掉无效 AppId
                .Select(appid => FetchAndCacheAppInfoAsync(client, appid!));

            await Task.WhenAll(tasks);

            //一切加载完毕，开始显示通知
            foreach (var game in Globals.FreeGames)
            {
                var gameData = game.Value.Data;
                var win = new WindowNotice
                {
                    Header = gameData.Header,
                    NoticeTitle = gameData.Name,
                    NoticeContent = gameData.AboutTheGameNoHTML,

                    PricePercent = gameData.PriceOverview == null ? 100 : gameData.PriceOverview.DiscountPercent,
                    PriceInital = $"￥{(gameData.PriceOverview == null ? "UNKNOWN" : (gameData.PriceOverview.Initial / 100).ToString("F2"))}",
                    PriceFinal = $"￥{(gameData.PriceOverview == null ? "UNKNOWN" : (gameData.PriceOverview.Final / 100).ToString("F2"))}"
                };
                await win.ShowNotice();

                await Task.Delay(500);
            }

            //提示完毕，退出
            Application.Current.Shutdown(0);
        }

        /// <summary>
        /// 独立的、具备高容错的单个游戏数据拉取方法
        /// </summary>
        private async Task FetchAndCacheAppInfoAsync(HttpClient client, string appid)
        {
            try
            {
                //详细信息获取
                string detailJson = await client.GetStringAsync(Globals.APIs.AppInfoAPI + appid);
                var info = JsonConvert.DeserializeObject<Dictionary<string, JsonAppInfo.AppInfo>>(detailJson);

                if (info != null && info.TryGetValue(appid, out var appInfo) && appInfo.Success)
                {
                    //加载Header
                    string imgUrl = string.Format(Globals.APIs.AppHeaderAPI, appid);
                    appInfo.Data.Header = await BitmapLoader.LoadImageFromUrlAsync(imgUrl);

                    lock (Globals.FreeGames)
                    {
                        Globals.FreeGames[appid] = appInfo;
                    }
                }
            }
            catch (Exception)
            {
                //none
            }
        }
    }
}