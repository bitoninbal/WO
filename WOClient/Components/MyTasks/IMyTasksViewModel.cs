using System.Collections.ObjectModel;
using WOClient.Components.Base;
using WOClient.Models;

namespace WOClient.Components.MyTasks
{
    public interface IMyTasksViewModel: IBaseViewModel
    {
        #region Properties
        ObservableCollection<MyTask> Tasks { get; set; }
        #endregion
    }
}
