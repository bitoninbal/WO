using System;
using System.Collections.Generic;
using System.Text;
using WOClient.Components.Base;
using WOClient.Components.Login;

namespace WOClient.Components.Main
{
    public class MainWindowViewModel : BaseViewModel, IMainWindowViewModel
    {
        public MainWindowViewModel(ILoginViewModel loginVm)
        {
            _currentVm = loginVm;
            _loginVm   = loginVm;
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
        public ILoginViewModel LoginVm
        {
            get => _loginVm;
            set
            {
                if (_loginVm == value) return;
                _loginVm = value;
                NotifyPropertyChanged("LoginVm");
            }
        }



    }
}
