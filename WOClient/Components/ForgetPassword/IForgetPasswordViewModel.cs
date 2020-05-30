using System.Windows.Input;
using WOClient.Components.Base;

namespace WOClient.Components.ForgetPassword
{
    public interface IForgetPasswordViewModel: IBaseViewModel, IRequestViewChange
    {
        #region Commands
        ICommand SendEmailCommand { get; }
        ICommand SwitchToLoginCommand { get; }
        #endregion

        #region Properties
        string Email { get; set; }
        #endregion
    }
}
