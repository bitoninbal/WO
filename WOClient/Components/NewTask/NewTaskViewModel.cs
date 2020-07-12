using System.Threading.Tasks;
using WOClient.Components.Base;

namespace WOClient.Components.NewTask
{
    public class NewTaskViewModel: BaseViewModel, INewTaskViewModel
    {
        #region Public Methods
        public async Task SendNewTaskAsync()
        {
            //try
            //{
            //    await _api.EmployeeRegisterAsync(FirstName, LastName, Email, Password.Copy(), Permission, DirectManager);
            //}
            //catch (Exception)
            //{
            //    MainWindowViewModel.MessageQueue.Enqueue("Could not connect to server.", "OK", (obj) => { }, new object(), false, true, TimeSpan.FromSeconds(6));
            //}
        }
        #endregion
    }
}
