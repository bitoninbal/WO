using WOClient.Components.Base;
using WOClient.Components.Comments;
using WOClient.Components.ForgetPassword;
using WOClient.Components.Login;
using WOClient.Components.MyTasks;
using WOClient.Components.Reports;
using WOClient.Components.TrackingTasks;

namespace WOClient.Components.Main
{
    public interface IMainWindowViewModel
    {
        #region Properties
        IBaseViewModel CurrentVm { get; set; }
        ICommentsViewModel CommentsVm { get; set; }
        IForgetPasswordViewModel ForgetPasswordVm { get; set; }
        ILoginViewModel LoginVm { get; set; }
        IMyTasksViewModel MyTasksVm { get; set; }
        ITrackingTasksViewModel TrackingTasksVm { get; set; }
        IReportsViewModel ReportsVm { get; set; }
        #endregion
    }
}
