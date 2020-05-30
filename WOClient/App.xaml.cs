﻿using System.Windows;
using WOClient.Components.ForgetPassword;
using WOClient.Components.Login;
using WOClient.Components.Main;
using WOClient.Components.MyTasks;
using WOClient.Components.NewTask;

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
            var myTasksVm        = new MyTasksViewModel();
            var newTaskVm        = new NewTaskViewModel();
            var mainWindowVm     = new MainWindowViewModel(loginVm, forgetPasswodVm, myTasksVm, newTaskVm);

            Current.MainWindow = new MainWindow(mainWindowVm);
        }

    }
}
