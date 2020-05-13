using System.Windows;
using WOClient.Components.ForgetPassword;
using WOClient.Components.Login;
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
            var loginVm          = new LoginViewModel();
            var forgetPasswodVm  = new ForgetPasswordViewModel();
            var mainWindowVm     = new MainWindowViewModel(loginVm, forgetPasswodVm);

            Current.MainWindow = new MainWindow(mainWindowVm);
        }

    }
}
