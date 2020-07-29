using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System.Threading.Tasks;
using WODataAccess.User;
using WOServer.Protos;

namespace WOServer.Services
{
    public class TasksService : Tasks.TasksBase
    {
        public TasksService(ITasksDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        #region Fields
        private ITasksDataAccess _dataAccess;
        #endregion

        public override async Task<Int32Value> AddTask(TaskInput request, ServerCallContext context)
        {
            var taskId = await _dataAccess.AddTaskDataAccessAsync(request.FinalDate.ToDateTime(),
                                                     request.EmployeeId,
                                                     request.ManagerId,
                                                     request.Priority,
                                                     request.Description,
                                                     request.Subject);

            return new Int32Value {Value = taskId};
        }
        public override async Task GetTrackingTasks(PersonIdInput request, IServerStreamWriter<TaskInput> responseStream, ServerCallContext context)
        {
            var result = await _dataAccess.GetTrackingTasksDataAccessAsync(request.PersonId);

            if (result is null)
            {
                await responseStream.WriteAsync(new TaskInput());

                return;
            }

            foreach (var task in result)
            {
                var tasks = new TaskInput
                {
                };

                await responseStream.WriteAsync(tasks);
            }
        }
    }
}
