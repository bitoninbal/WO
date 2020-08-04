using System.Collections.ObjectModel;
using WOClient.Components.Base;
using WOClient.Library.Api;
using WOClient.Library.Models;

namespace WOClient.Components.MyTasks
{
    public class MyTasksViewModel: MyTaskViewModel, IMyTasksViewModel
    {
        public MyTasksViewModel(IClientApi api) : base(api)
        {
            Tasks = new ObservableCollection<MyTask>();
        }

        #region Properties
        public ObservableCollection<MyTask> Tasks { get; set; }
        #endregion
    }
}
