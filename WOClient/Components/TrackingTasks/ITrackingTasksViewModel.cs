using WOClient.Components.Base;
using WOClient.Components.NewTask;

namespace WOClient.Components.TrackingTasks
{
    public interface ITrackingTasksViewModel: IBaseViewModel
    {
        INewTaskViewModel NewTaskVm { get; set; }
    }
}
