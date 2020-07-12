using System;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Input;
using WOClient.Components.Base;

namespace WOClient.Components.Login
{
    public interface ILoginViewModel: IBaseViewModel, IRequestViewChange
    {
        #region ICommands
        ICommand SwitchToForgetPasswordCommand { get; }
        #endregion

        #region Events
        event EventHandler UserLoggedIn;
        #endregion

        #region Properties
        string UserName { get; set; }
        SecureString Password { get; set; }
        #endregion

        #region Methods
        Task LoginAsync(); 
        #endregion
    }
}
