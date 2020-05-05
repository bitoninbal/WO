using System;
using System.Collections.Generic;
using System.Text;
using WOClient.Components.Base;

namespace WOClient.Components.Main
{
    public class MainWindowViewModel : BaseViewModel, IMainWindowViewModel
    {
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
    }
}
