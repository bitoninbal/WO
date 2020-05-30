using WOClient.Components.Base;
using WOClient.Components.ForgetPassword;
using WOClient.Components.Login;
using WOClient.Components.MyTasks;
using WOClient.Components.NewTask;
using WOClient.Components.Reports;

namespace WOClient.Components.Main
{
    public interface IMainWindowViewModel
    {
        #region Properties
        IBaseViewModel CurrentVm { get; set; }
        IForgetPasswordViewModel ForgetPasswordVm { get; set; }
        ILoginViewModel LoginVm { get; set; }
        IMyTasksViewModel MyTasksVm { get; set; }
        INewTaskViewModel NewTaskVm { get; set; }
        IReportsViewModel ReportsVm { get; set; }
        #endregion
    }
}
