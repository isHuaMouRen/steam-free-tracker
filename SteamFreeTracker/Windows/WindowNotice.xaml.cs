using Newtonsoft.Json;
using SteamFreeTracker.Classes;
using SteamFreeTracker.Utils;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace SteamFreeTracker.Windows
{
    /// <summary>
    /// WindowNotice.xaml 的交互逻辑
    /// </summary>
    public partial class WindowNotice : Window
    {
        public BitmapImage? Header { get; set; } = new BitmapImage();
        public string? NoticeTitle { get; set; } = "Title";
        public string? NoticeContent { get; set; } = "Content";
        public string? AppId { get; set; } = "0";

        public int? PricePercent { get; set; } = 100;
        public string? PriceInital { get; set; } = "￥100.00";
        public string? PriceFinal { get; set; } = "￥0.00";


        private CancellationTokenSource _cts = new CancellationTokenSource();

        public WindowNotice()
        {
            InitializeComponent();
            Loaded += ((s, e) =>
            {
                textBlock_Title.Text = NoticeTitle;
                textBlock_Content.Text = NoticeContent;
                image_Header.Source = Header;

                userDiscount.Percent = PricePercent;
                userDiscount.InitalPrice = PriceInital;
                userDiscount.FinalPrice = PriceFinal;
            });
        }

        public async Task ShowNotice()
        {
            var screenWidth = SystemParameters.PrimaryScreenWidth;
            var screenHight = SystemParameters.PrimaryScreenHeight;

            var winPosX = screenWidth - this.Width;
            var winPosY = screenHight - WinAPIHelper.GetTaskbarHeight() - this.Height;

            this.Left = winPosX; this.Top = winPosY;

            win_Translate.X = this.Width;

            this.Show();

            ShowFadeIn();
            try { await Task.Delay(10000, _cts.Token); } catch (TaskCanceledException) { }
            ShowFadeOut();
            await Task.Delay(1000);

            this.Close();

            return;
        }

        private void ShowFadeIn()
        {
            var ani = new DoubleAnimation
            {
                To = 0,
                Duration = TimeSpan.FromMilliseconds(1000),
                EasingFunction = new PowerEase { Power = 5, EasingMode = EasingMode.EaseOut }
            };
            win_Translate.BeginAnimation(TranslateTransform.XProperty, null);
            win_Translate.BeginAnimation(TranslateTransform.XProperty, ani);

            //音效
            var sound = new SoundPlayer(Globals.Resources.NoticeShow);
            sound.Play();
        }

        private void ShowFadeOut()
        {
            var ani = new DoubleAnimation
            {
                To = this.Height,
                Duration = TimeSpan.FromMilliseconds(1000),
                EasingFunction = new PowerEase { Power = 5, EasingMode = EasingMode.EaseOut }
            };
            win_Translate.BeginAnimation(TranslateTransform.YProperty, null);
            win_Translate.BeginAnimation(TranslateTransform.YProperty, ani);
        }

        private void overlay_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //跳转商店页面
            Process.Start(new ProcessStartInfo
            {
                FileName = $"https://store.steampowered.com/app/{AppId}",
                UseShellExecute = true
            });

            Globals.Config.IgnoreGames.Add(AppId!);
            File.WriteAllText(Globals.ConfigPath, JsonConvert.SerializeObject(Globals.Config));

            //取消等待，立即退出
            _cts.Cancel();
        }
    }
}
