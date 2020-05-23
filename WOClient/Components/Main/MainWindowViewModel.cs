using WOClient.Components.Base;
using WOClient.Components.ForgetPassword;
using WOClient.Components.Login;
using WOClient.Components.MyTasks;
using WOClient.Enums;

namespace WOClient.Components.Main
{
    public class MainWindowViewModel : BaseViewModel, IMainWindowViewModel
    {
        public MainWindowViewModel(ILoginViewModel loginVm ,IForgetPasswordViewModel forgetPasswordVm, IMyTasksViewModel myTasksVm)
        {
            _currentVm        = loginVm;
            _loginVm          = loginVm;
            _forgetPasswordVm = forgetPasswordVm;
            _myTasksVm        = myTasksVm;

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

        private IMyTasksViewModel _myTasksVm;
        public IMyTasksViewModel MyTasksVm
        {
            get => _myTasksVm;
            set
            {
                if (_forgetPasswordVm == value) return;

                _myTasksVm = value;
                NotifyPropertyChanged("MyTasksVm");
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
                case ViewsEnum.MyTasks:
                    CurrentVm = MyTasksVm;
                    break;
            }
        }
    }
}
