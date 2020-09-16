using System;
using System.Timers;
using WOClient.Components.Archive;
using WOClient.Components.Base;
using WOClient.Components.Comments;
using WOClient.Components.Employees;
using WOClient.Components.ForgetPassword;
using WOClient.Components.Login;
using WOClient.Components.MyTasks;
using WOClient.Components.Reports;
using WOClient.Components.TrackingTasks;
using WOClient.Enums;
using WOClient.Library.Models;

namespace WOClient.Components.Main
{
    public class MainWindowViewModel: BaseViewModel, IMainWindowViewModel
    {
        public MainWindowViewModel(IArchiveViewModel archiveVm,
                                   ICommentsViewModel commentsVm,
                                   IEmplyeesViewModel emplyeesVm,
                                   ILoginViewModel loginVm,
                                   IForgetPasswordViewModel forgetPasswordVm,
                                   IMyTasksViewModel myTasksVm,
                                   IReportsViewModel reportsVm,
                                   ITrackingTasksViewModel trackingTasksVm)
        {
            _archiveVm        = archiveVm;
            _currentVm        = loginVm;
            _commentsVm       = commentsVm;
            _emplyeesVm       = emplyeesVm;
            _loginVm          = loginVm;
            _forgetPasswordVm = forgetPasswordVm;
            _myTasksVm        = myTasksVm;
            _reportsVm        = reportsVm;
            _trackingTasksVm  = trackingTasksVm;

            SubscribeToSwitchViewRequested();
        }

        #region Fields
        private static Timer             _updateTimer;
        private IArchiveViewModel        _archiveVm;
        private ICommentsViewModel       _commentsVm;
        private IEmplyeesViewModel       _emplyeesVm;
        private IForgetPasswordViewModel _forgetPasswordVm;
        private IMyTasksViewModel        _myTasksVm;
        private ILoginViewModel          _loginVm;
        private IReportsViewModel        _reportsVm;
        private ITrackingTasksViewModel  _trackingTasksVm;
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
        public IArchiveViewModel ArchiveVm
        {
            get => _archiveVm;
            set
            {
                if (_archiveVm == value) return;

                _archiveVm = value;

                NotifyPropertyChanged(nameof(ArchiveVm));
            }
        }
        public ICommentsViewModel CommentsVm
        {
            get => _commentsVm;
            set
            {
                if (_commentsVm == value) return;

                _commentsVm = value;
                NotifyPropertyChanged("CommentsVm");
            }
        }
        public IEmplyeesViewModel EmplyeesVm
        {
            get => _emplyeesVm;
            set
            {
                if (_emplyeesVm == value) return;

                _emplyeesVm = value;
                NotifyPropertyChanged("EmplyeesVm");
            }
        }
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
                if (_myTasksVm == value) return;

                _myTasksVm = value;
                NotifyPropertyChanged("MyTasksVm");
            }
        }
        public IReportsViewModel ReportsVm
        {
            get => _reportsVm;
            set
            {
                if (_reportsVm == value) return;

                _reportsVm = value;
                NotifyPropertyChanged("ReportsVm");
            }
        }
        public ITrackingTasksViewModel TrackingTasksVm
        {
            get => _trackingTasksVm;
            set
            {
                if (_trackingTasksVm == value) return;

                _trackingTasksVm = value;
                NotifyPropertyChanged("TrackingTasksVm");
            }
        }
        #endregion

        #region Public Methods
        public void Reset()
        {
            _emplyeesVm.Reset();
            _trackingTasksVm.Reset();
            _reportsVm.Reset();

            LoggedInUser.Instance.Reset();
            IMainWindowViewModel.User.Reset();
        }
        #endregion

        #region Private Methods
        private void SubscribeToSwitchViewRequested()
        {
            LoginVm.SwitchViewRequested          += SwitchToView;
            LoginVm.UserLoggedIn                 += InitUser;
            ForgetPasswordVm.SwitchViewRequested += SwitchToView;
        }
        private void InitUser(object sender, EventArgs args)
        {
            switch (LoggedInUser.Instance.Permission)
            {
                case WOCommon.Enums.PermissionsEnum.Manager:
                    IMainWindowViewModel.User = new Manager(LoggedInUser.Instance.Permission,
                                                            LoggedInUser.Instance.Id,
                                                            LoggedInUser.Instance.DirectManager,
                                                            LoggedInUser.Instance.FirstName,
                                                            LoggedInUser.Instance.LastName,
                                                            LoggedInUser.Instance.Email);
                    break;
                case WOCommon.Enums.PermissionsEnum.Employee:
                    IMainWindowViewModel.User = new Employee(LoggedInUser.Instance.Permission, 
                                                             LoggedInUser.Instance.Id,
                                                             LoggedInUser.Instance.DirectManager,
                                                             LoggedInUser.Instance.FirstName,
                                                             LoggedInUser.Instance.LastName,
                                                             LoggedInUser.Instance.Email);
                    break;
            }

            //SetTimer();
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
        private void SetTimer()
        {
            _updateTimer           = new Timer(2000);
            _updateTimer.Elapsed  += _updateTimer_Elapsed;
            _updateTimer.AutoReset = true;
            _updateTimer.Enabled   = true;
        }

        private async void _updateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            await IMainWindowViewModel.User.UpdateAsync();
        }
        #endregion
    }
}
