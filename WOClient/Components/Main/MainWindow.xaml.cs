﻿using System.Windows;
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

                    break;
                case "ReportsButton":
                    viewModel.CurrentVm = viewModel.ReportsVm;

                    break;
            }
        } 
        #endregion
    }
}
