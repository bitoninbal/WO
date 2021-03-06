﻿using System.Windows;
using System.Windows.Controls;

namespace WOClient.Components.EmployeeComponent
{
    public partial class NewEmployeeView : UserControl
    {
        public NewEmployeeView()
        {
            InitializeComponent();

            PassErrTextBlock.Text = " ";
        }

        #region Private Methods
        private async void NewEmplyee_Click(object sender, RoutedEventArgs e)
        {
            var vm = (EmployeeViewModel)DataContext;

            await vm.RegisterAsync();
        }

        private void PassBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PassBox.SecurePassword.Length == 0)
            {
                PassErrTextBlock.Text = " ";
            }
            else if (PassBox.SecurePassword.Length < 6 || PassBox.SecurePassword.Length > 20)
            {
                PassErrTextBlock.Text = "Illegal length for password.";
            }
            else
            {
                PassErrTextBlock.Text = "";

                var newEmplyeeVm = (EmployeeViewModel)DataContext;

                newEmplyeeVm.Password = PassBox.SecurePassword.Copy();
            }
        } 
        #endregion
    }
}
