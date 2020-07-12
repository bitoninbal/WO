using System;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Input;
using WOClient.Components.Base;
using WOClient.Components.Main;
using WOClient.Enums;
using WOClient.Library.Api;
using WOClient.Library.Models;
using WOClient.Resources.Commands;

namespace WOClient.Components.Login
{
    public class LoginViewModel: BaseViewModel, ILoginViewModel 
    {
        public LoginViewModel(IClientApi api)
        {
            SwitchToForgetPasswordCommand = new RelayCommand(SwitchToForgetPassword);

            _api = api;
        }

        #region Commands
        public ICommand SwitchToForgetPasswordCommand { get; }
        #endregion

        #region Events
        public event EventHandler<ViewsEnum> SwitchViewRequested;
        public event EventHandler UserLoggedIn;
        #endregion

        #region Fields
        private IClientApi _api;
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

        #region Private Methods
        private void SwitchToForgetPassword()
        {
            OnSwitchToForgerPassword();
        }
        private void OnSwitchToMyTasks()
        {
            SwitchViewRequested?.Invoke(this, ViewsEnum.MyTasks);
        }
        private void OnSwitchToForgerPassword()
        {
            SwitchViewRequested?.Invoke(this, ViewsEnum.ForgetPassword);
        }
        private void OnUserLoggedIn()
        {
            UserLoggedIn?.Invoke(this, new EventArgs());
        }
        #endregion

        #region Public Methods
        public async Task LoginAsync()
        {
            try
            {
                await _api.LoginAsync(UserName, Password.Copy());

                if (UserInfo.Instance.Id == 0)
                {
                    MainWindowViewModel.MessageQueue.Enqueue("User name or password is incorrect.", "OK", (obj) => { }, new object(), false, true, TimeSpan.FromSeconds(6));
                }
                else
                {
                    OnSwitchToMyTasks();
                    OnUserLoggedIn();
                }
            }
            catch (Exception)
            {
                MainWindowViewModel.MessageQueue.Enqueue("Could not connect to server.", "OK", (obj) => { }, new object (), false, true, TimeSpan.FromSeconds(6));
            }
        }
        #endregion
    }
}
