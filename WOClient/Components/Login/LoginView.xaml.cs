using System.Windows;
using System.Windows.Controls;

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

        #region Private Methods
        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = (LoginViewModel)DataContext;

            await vm.LoginAsync();
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

                var loginVm = (LoginViewModel)DataContext;

                loginVm.Password = PassBox.SecurePassword.Copy();
            }
        }
        #endregion
    }
}
