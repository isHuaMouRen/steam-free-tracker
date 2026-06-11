using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SteamFreeTracker.Controls
{
    /// <summary>
    /// UserDiscount.xaml 的交互逻辑
    /// </summary>
    public partial class UserDiscount : UserControl
    {
        public int? Percent { get; set; } = 100;
        public string? InitalPrice { get; set; } = "￥100.00";
        public string? FinalPrice { get; set; } = "￥0.00";

        public UserDiscount()
        {
            InitializeComponent();
            Loaded += ((s, e) =>
            {
                textBlock_Percent.Text = $"-{Percent}%";
                textBlock_InitalPrice.Text = InitalPrice;
                textBlock_FinalPrice.Text = FinalPrice;
            });
        }
    }
}
