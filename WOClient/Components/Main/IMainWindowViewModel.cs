using MaterialDesignThemes.Wpf;
using WOClient.Components.Base;
using WOClient.Components.Employees;
using WOClient.Components.ForgetPassword;
using WOClient.Components.Login;
using WOClient.Components.MyTasks;
using WOClient.Components.Reports;
using WOClient.Components.TrackingTasks;
using WOClient.Library.Models;

namespace WOClient.Components.Main
{
    public interface IMainWindowViewModel
    {
        #region Properties
        static IPerson User { get; protected set; }
        static SnackbarMessageQueue MessageQueue { get; } = new SnackbarMessageQueue();
        #endregion
    }
}
