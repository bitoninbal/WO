using WOClient.Components.Base;
using WOClient.Components.NewTask;

namespace WOClient.Components.TrackingTasks
{
    public interface ITrackingTasksViewModel: IBaseViewModel
    {
        #region Properties
        INewTaskViewModel NewTaskVm { get; set; }
        #endregion

        void Reset();
    }
}
