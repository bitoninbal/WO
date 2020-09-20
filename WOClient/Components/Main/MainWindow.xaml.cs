using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;

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
            var vm     = (MainWindowViewModel)DataContext;
            var button = (Button)sender;

            switch (button.Name)
            {
                case "MyTasksButton":
                    vm.CurrentVm = vm.MyTasksVm;

                    break;
                case "TrackingTasksButton":
                    vm.CurrentVm = vm.TrackingTasksVm;

                    break;
                case "CommentsButton":
                    vm.CurrentVm = vm.CommentsVm;

                    break;
                case "EmployeesButton":
                    vm.CurrentVm = vm.EmplyeesVm;

                    break;
                case "ReportsButton":
                    vm.CurrentVm = vm.ReportsVm;

                    break;
                case "ArchiveButton":
                    vm.CurrentVm = vm.ArchiveVm;

                    break;
                case "ProfileButton":
                    vm.CurrentVm = vm.ProfileVm;

                    break;
                case "LogoutButton":
                    vm.Reset();
                    vm.CurrentVm = vm.LoginVm;

                    DrawerHost.CloseDrawerCommand.Execute(null, null);

                    break;
            }
        } 
        #endregion
    }
}
