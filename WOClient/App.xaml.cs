using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WOClient.Components.Main;

namespace WOClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ComposeObjects();
            Current.MainWindow.Show();
        }
        private static void ComposeObjects()
        {
            var mainWindowVm = new MainWindowViewModel();
            Current.MainWindow = new MainWindow(mainWindowVm);
        }

    }
}
