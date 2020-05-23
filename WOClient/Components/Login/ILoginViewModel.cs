using System.Security;
using System.Windows.Input;
using WOClient.Components.Base;

namespace WOClient.Components.Login
{
    public interface ILoginViewModel: IBaseViewModel ,IRequestViewChange
    {
        #region Properties
        string UserName { get; set; }
        SecureString Password { get; set; }
        #endregion

        #region Commands
        ICommand LoginCommand { get; }
        ICommand SwitchToForgetPasswordCommand { get; }
        #endregion
    }
}
