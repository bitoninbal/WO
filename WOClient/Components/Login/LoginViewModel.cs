using System;
using System.Security;
using System.Windows.Input;
using WOClient.Components.Base;
using WOClient.Enums;
using WOClient.Resources.Commands;

namespace WOClient.Components.Login
{
    public class LoginViewModel: BaseViewModel , ILoginViewModel 
    {
        public LoginViewModel()
        {
            SwitchToForgetPasswordCommand = new RelayCommand(SwitchToForgetPassword);
        }

        #region Fields
        private string _userName;
        #endregion

        #region Properties
        public string UserName
        {
            get => _userName;
            set
            {
                if (_userName == value) return;
                _userName = value;
                NotifyPropertyChanged("UserName");
            }
        }
        public SecureString Password { get; set; }
        #endregion

        #region Events
        public event EventHandler<ViewsEnum> SwitchViewRequested;
        #endregion

        #region Commands
        public ICommand SwitchToForgetPasswordCommand { get; }
        #endregion

        #region Private Methods
        private void SwitchToForgetPassword()
        {
            OnSwitchToForgerPassword();
        }
        private void OnSwitchToForgerPassword()
        {
            SwitchViewRequested?.Invoke(this, ViewsEnum.ForgetPassword);
        }
        #endregion
    }
}
