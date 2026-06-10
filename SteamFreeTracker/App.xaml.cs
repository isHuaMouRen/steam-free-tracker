using SteamFreeTracker.Windows;
using System.Configuration;
using System.Data;
using System.Windows;

namespace SteamFreeTracker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var win = new WindowMain();
            this.MainWindow = win;
            



        }
    }

}
