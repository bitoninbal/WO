using WOClient.Components.Base;
using WOClient.Components.ForgetPassword;
using WOClient.Components.Login;
using WOClient.Enums;

namespace WOClient.Components.Main
{
    public class MainWindowViewModel : BaseViewModel, IMainWindowViewModel
    {
        public MainWindowViewModel(ILoginViewModel loginVm ,IForgetPasswordViewModel forgetPasswordVm)
        {
            _currentVm        = loginVm;
            _loginVm          = loginVm;
            _forgetPasswordVm = forgetPasswordVm;

            SubscribeToSwitchViewRequested();
        }

        private IBaseViewModel _currentVm;
        public IBaseViewModel CurrentVm
        {
            get => _currentVm;
            set
            {
                if (_currentVm == value) return;
                _currentVm = value;
                NotifyPropertyChanged("CurrentVm");
            }
        }
        private ILoginViewModel _loginVm;
        public ILoginViewModel   LoginVm
        {
            get => _loginVm;
            set
            {
                if (_loginVm == value) return;
                _loginVm = value;
                NotifyPropertyChanged("LoginVm");
            }
        }

        private IForgetPasswordViewModel _forgetPasswordVm;
        public IForgetPasswordViewModel ForgetPasswordVm
        {
            get => _forgetPasswordVm;
            set
            {
                if (_forgetPasswordVm == value) return;
                _forgetPasswordVm = value;
                NotifyPropertyChanged("ForgetPasswordVm");
            }
        }

        private void SubscribeToSwitchViewRequested()
        {
            LoginVm.SwitchViewRequested          += SwitchToView;
            ForgetPasswordVm.SwitchViewRequested += SwitchToView;
        }

        private void SwitchToView(object sender, ViewsEnum args)
        {
            switch (args)
            {
                case ViewsEnum.ForgetPassword:
                    CurrentVm = ForgetPasswordVm;
                    break;
                case ViewsEnum.Login:
                    CurrentVm = LoginVm;
                    break;
            }
        }
    }
}
