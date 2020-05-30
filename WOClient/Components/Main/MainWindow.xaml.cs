using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
                    break;
                case "CreateNewTaskButton":
                    viewModel.CurrentVm = viewModel.NewTaskVm;
                    break;
                case "CommentsButton":
                    break;
                case "AddNewAccountButton":
                    break;
                case "ReportsButton":
                    break;
            }
        }
    }
}
