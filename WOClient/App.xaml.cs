using System.Windows;
using WOClient.Components.Comments;
using WOClient.Components.Employees;
using WOClient.Components.ForgetPassword;
using WOClient.Components.Login;
using WOClient.Components.Main;
using WOClient.Components.MyTasks;
using WOClient.Components.NewEmployee;
using WOClient.Components.NewTask;
using WOClient.Components.Reports;
using WOClient.Components.TrackingTasks;

namespace WOClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App: Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ComposeObjects();
            Current.MainWindow.Show();
        }
        private static void ComposeObjects()
        {
            var commentsVm      = new CommentsViewModel();
            var newEmployeeVm   = new NewEmployeeViewModel();
            var employyesVm     = new EmployeesViewModel(newEmployeeVm);
            var loginVm         = new LoginViewModel();
            var forgetPasswodVm = new ForgetPasswordViewModel();
            var myTasksVm       = new MyTasksViewModel();
            var newTaskVm       = new NewTaskViewModel();
            var trackingTasksVm = new TrackingTasksViewModel(newTaskVm);
            var reportsVm       = new ReportsViewModel();
            var mainWindowVm    = new MainWindowViewModel(commentsVm,
                                                          employyesVm,
                                                          loginVm,
                                                          forgetPasswodVm,
                                                          myTasksVm,
                                                          reportsVm,
                                                          trackingTasksVm);

            Current.MainWindow = new MainWindow(mainWindowVm);
        }

    }
}
