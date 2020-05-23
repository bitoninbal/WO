using WOClient.Components.Base;
using WOClient.Components.ForgetPassword;
using WOClient.Components.Login;
using WOClient.Components.MyTasks;

namespace WOClient.Components.Main
{
    public interface IMainWindowViewModel
    {
        #region Properties
        IBaseViewModel CurrentVm { get; set; }
        IForgetPasswordViewModel ForgetPasswordVm { get; set; }
        ILoginViewModel LoginVm { get; set; }
        IMyTasksViewModel MyTasksVm { get; set; }
        #endregion
    }
}
