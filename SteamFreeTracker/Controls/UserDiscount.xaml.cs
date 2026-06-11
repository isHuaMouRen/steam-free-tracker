using System.Windows.Controls;

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
