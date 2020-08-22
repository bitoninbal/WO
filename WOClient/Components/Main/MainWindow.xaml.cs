using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using WOClient.Library.Models;

namespace WOClient.Components.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IMainWindowViewModel mainWindowVm)
        {
            InitializeComponent();

            DataContext = mainWindowVm;
        }

        #region Private Methods
        private void SwitchViewClick(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainWindowViewModel)DataContext;
            var button = (Button)sender;

            switch (button.Name)
            {
                case "MyTasksButton":
                    viewModel.CurrentVm = viewModel.MyTasksVm;

                    break;
                case "TrackingTasksButton":
                    viewModel.CurrentVm = viewModel.TrackingTasksVm;

                    break;
                case "CommentsButton":
                    viewModel.CurrentVm = viewModel.CommentsVm;

                    break;
                case "EmployeesButton":
                    viewModel.CurrentVm = viewModel.EmplyeesVm;

                    break;
                case "ReportsButton":
                    viewModel.CurrentVm = viewModel.ReportsVm;

                    break;
                case "ArchiveButton":
                    viewModel.CurrentVm = viewModel.ArchiveVm;

                    break;
                case "LogoutButton":
                    LoggedInUser.Instance.Reset();
                    DrawerHost.CloseDrawerCommand.Execute(null, null);

                    viewModel.CurrentVm = viewModel.LoginVm;

                    break;
            }
        } 
        #endregion
    }
}
