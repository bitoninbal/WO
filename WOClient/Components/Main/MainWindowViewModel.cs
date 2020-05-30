﻿using WOClient.Components.Base;
using WOClient.Components.ForgetPassword;
using WOClient.Components.Login;
using WOClient.Components.MyTasks;
using WOClient.Components.NewTask;
using WOClient.Components.Reports;
using WOClient.Enums;

namespace WOClient.Components.Main
{
    public class MainWindowViewModel: BaseViewModel, IMainWindowViewModel
    {
        public MainWindowViewModel(ILoginViewModel loginVm ,
                                   IForgetPasswordViewModel forgetPasswordVm,
                                   IMyTasksViewModel myTasksVm,
                                   INewTaskViewModel newTaskVm,
                                   IReportsViewModel reportsVm)
        {
            _currentVm        = loginVm;
            _loginVm          = loginVm;
            _forgetPasswordVm = forgetPasswordVm;
            _myTasksVm        = myTasksVm;
            _newTaskVm        = newTaskVm;
            _reportsVm        = reportsVm;

            SubscribeToSwitchViewRequested();
        }

        #region Fields
        private IForgetPasswordViewModel _forgetPasswordVm;
        private IMyTasksViewModel        _myTasksVm;
        private INewTaskViewModel        _newTaskVm;
        private ILoginViewModel          _loginVm;
        private IReportsViewModel        _reportsVm;       
        private IBaseViewModel           _currentVm;
        #endregion

        #region Properties
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
        public INewTaskViewModel NewTaskVm
        {
            get => _newTaskVm;
            set
            {
                if (_forgetPasswordVm == value) return;

                _newTaskVm = value;
                NotifyPropertyChanged("NewTaskVm");
            }
        }
        public IReportsViewModel ReportsVm
        {
            get => _reportsVm;
            set
            {
                if (_currentVm == value) return;
                _currentVm = value;
                NotifyPropertyChanged("ReportsVm");
            }
        }
        #endregion

        #region Private Methods
        private void SubscribeToSwitchViewRequested()
        {
            LoginVm.SwitchViewRequested += SwitchToView;
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
                case ViewsEnum.NewTask:
                    CurrentVm = NewTaskVm;
                    break;
            }
        } 
        #endregion
    }
}
