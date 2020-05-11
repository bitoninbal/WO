using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WOClient.Components.Login
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
            PassErrTextBlock.Text = " ";
        }

        private void PassBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if(PassBox.SecurePassword.Length == 0)
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
                var loginVm =(LoginViewModel) DataContext;
                loginVm.Password = PassBox.SecurePassword.Copy();
            }

        }
    }
}
