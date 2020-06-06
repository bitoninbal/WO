using System.Collections.ObjectModel;
using WOClient.Components.Base;
using WOClient.Components.NewTask;
using WOClient.Models;

namespace WOClient.Components.TrackingTasks
{
    public interface ITrackingTasksViewModel: IBaseViewModel
    {
        #region Properties
        INewTaskViewModel NewTaskVm { get; set; }
        ObservableCollection<MyTask> Tasks { get; set; }
        #endregion
    }
}
