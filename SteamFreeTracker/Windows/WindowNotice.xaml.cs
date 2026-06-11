using SteamFreeTracker.Classes;
using SteamFreeTracker.Utils;
using System;
using System.Collections.Generic;
using System.Media;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

        public int? PricePercent { get; set; } = 100;
        public string? PriceInital { get; set; } = "￥100.00";
        public string? PriceFinal { get; set; } = "￥0.00";

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
            await Task.Delay(10000);
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
    }
}
