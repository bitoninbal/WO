using WOClient.Components.Base;
using WOClient.Components.ForgetPassword;
using WOClient.Components.Login;

namespace WOClient.Components.Main
{
    public interface IMainWindowViewModel
    {
        #region Properties
        IBaseViewModel CurrentVm { get; set; }
        IForgetPasswordViewModel ForgetPasswordVm { get; set; }
        ILoginViewModel LoginVm { get; set; }
        #endregion
    }
}
