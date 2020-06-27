﻿using System.Security;
using System.Threading.Tasks;
using System.Windows.Input;
using WOClient.Components.Base;

namespace WOClient.Components.Login
{
    public interface ILoginViewModel: IBaseViewModel, IRequestViewChange
    {
        #region Properties
        string UserName { get; set; }
        SecureString Password { get; set; }
        #endregion

        Task LoginAsync();

        #region Commands
        ICommand SwitchToForgetPasswordCommand { get; }
        #endregion
    }
}
