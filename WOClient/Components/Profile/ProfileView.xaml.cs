using System.Windows;
using System.Windows.Controls;

namespace WOClient.Components.Profile
{
    /// <summary>
    /// Interaction logic for ProfileView.xaml
    /// </summary>
    public partial class ProfileView: UserControl
    {
        public ProfileView()
        {
            InitializeComponent();

            CurrentPassBoxErrTextBlock.Text = " ";
            NewPassBoxErrTextBlock.Text     = " ";
            RepeatPassBoxErrTextBlock.Text  = " ";
        }

        #region Private Methods
        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = (ProfileViewModel)DataContext;

            await vm.SaveAsync();
        }
        private void CurrentPassBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (CurrentPassBox.SecurePassword.Length == 0)
            {
                CurrentPassBoxErrTextBlock.Text = " ";
            }
            else if (CurrentPassBox.SecurePassword.Length < 6 || CurrentPassBox.SecurePassword.Length > 20)
            {
                CurrentPassBoxErrTextBlock.Text = "Illegal length for password.";
            }
            else
            {
                CurrentPassBoxErrTextBlock.Text = "";

                var vm = (ProfileViewModel)DataContext;

                vm.CurrentPassword = CurrentPassBox.SecurePassword.Copy();
            }
        }
        private void NewPassBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (NewPassBox.SecurePassword.Length == 0)
            {
                NewPassBoxErrTextBlock.Text = " ";
            }
            else if (NewPassBox.SecurePassword.Length < 6 || NewPassBox.SecurePassword.Length > 20)
            {
                NewPassBoxErrTextBlock.Text = "Illegal length for password.";
            }
            else
            {
                NewPassBoxErrTextBlock.Text = "";

                var vm = (ProfileViewModel)DataContext;

                vm.NewPassword = NewPassBox.SecurePassword.Copy();
            }
        }
        private void RepeatPassBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (RepeatPassBox.SecurePassword.Length == 0)
            {
                RepeatPassBoxErrTextBlock.Text = " ";
            }
            else if (RepeatPassBox.SecurePassword.Length < 6 || RepeatPassBox.SecurePassword.Length > 20)
            {
                RepeatPassBoxErrTextBlock.Text = "Illegal length for password.";
            }
            else
            {
                RepeatPassBoxErrTextBlock.Text = "";

                var vm = (ProfileViewModel)DataContext;

                vm.RepeatPassword = RepeatPassBox.SecurePassword.Copy();
            }
        }
        #endregion
    }
}
