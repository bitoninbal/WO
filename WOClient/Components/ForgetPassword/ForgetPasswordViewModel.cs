﻿using System;
using System.Windows.Input;
using WOClient.Components.Base;
using WOClient.Enums;
using WOClient.Resources.Commands;

namespace WOClient.Components.ForgetPassword
{
    public class ForgetPasswordViewModel: BaseViewModel, IForgetPasswordViewModel
    {
        public ForgetPasswordViewModel()
        {
            SwitchToLoginCommand = new RelayCommand(SwitchToLogin);
        }

        #region Events
        public event EventHandler<ViewsEnum> SwitchViewRequested;
        #endregion

        #region Commands
        public ICommand SwitchToLoginCommand { get; }
        #endregion

        #region Private Methods
        private void SwitchToLogin()
        {
            OnSwitchToLogin();
        }
        private void OnSwitchToLogin()
        {
            SwitchViewRequested?.Invoke(this, ViewsEnum.Login);
        }
        #endregion
    }
}
