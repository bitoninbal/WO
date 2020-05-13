using System.Security;
using System.Windows.Input;
using WOClient.Components.Base;

namespace WOClient.Components.Login
{
    public interface ILoginViewModel: IBaseViewModel ,IRequestViewChange
    {
        #region Properties
        public string UserName { get; set; }
        public SecureString Password { get; set; }
        #endregion

        #region Commands
        public ICommand SwitchToForgetPasswordCommand { get; }
        #endregion
    }
}
