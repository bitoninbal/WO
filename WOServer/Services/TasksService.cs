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

        #region Public Methods
        public override async Task<Int32Value> AddTask(TaskInput request, ServerCallContext context)
        {
            var taskId = await _dataAccess.AddTaskDataAccessAsync(request.FinalDate.ToDateTime(),
                                                                  request.EmployeeId,
                                                                  request.ManagerId,
                                                                  request.Priority,
                                                                  request.Description,
                                                                  request.Subject);

            return new Int32Value { Value = taskId };
        }
        public override async Task<Empty> DeleteTask(Int32Value request, ServerCallContext context)
        {
            await _dataAccess.DeleteTaskAsync(request.Value);

            return new Empty();
        }
        public override async Task GetMyTasks(Int32Value request, IServerStreamWriter<TaskOutput> responseStream, ServerCallContext context)
        {
            var result = await _dataAccess.GetMyTasksDataAccessAsync(request.Value);

            if (result is null)
            {
                await responseStream.WriteAsync(new TaskOutput());

                return;
            }

            foreach (var taskModel in result)
            {
                var task = new TaskOutput
                {
                    TaskId      = taskModel.TaskId,
                    FinalDate   = taskModel.FinalDate.ToUniversalTime().ToTimestamp(),
                    Subject     = taskModel.Subject,
                    Description = taskModel.Description,
                    Priority    = taskModel.Priority,
                    IsArchive   = taskModel.IsArchive,
                    IsCompleted = taskModel.IsCompleted,
                    EmployeeId  = taskModel.UserId,
                    CreatedDate = taskModel.CreatedDate.ToUniversalTime().ToTimestamp()
                };

                await responseStream.WriteAsync(task);
            }
        }
        public override async Task GetTrackingTasks(Int32Value request, IServerStreamWriter<TaskOutput> responseStream, ServerCallContext context)
        {
            var result = await _dataAccess.GetTrackingTasksDataAccessAsync(request.Value);

            if (result is null)
            {
                await responseStream.WriteAsync(new TaskOutput());

                return;
            }

            foreach (var taskModel in result)
            {
                var task = new TaskOutput
                {
                    TaskId      = taskModel.TaskId,
                    FinalDate   = taskModel.FinalDate.ToUniversalTime().ToTimestamp(),
                    Subject     = taskModel.Subject,
                    Description = taskModel.Description,
                    Priority    = taskModel.Priority,
                    IsArchive   = taskModel.IsArchive,
                    IsCompleted = taskModel.IsCompleted,
                    EmployeeId  = taskModel.UserId,
                    CreatedDate   = taskModel.CreatedDate.ToUniversalTime().ToTimestamp()
                };

                await responseStream.WriteAsync(task);
            }
        }
        public override async Task<Empty> UpdateField(UpdateTaskFieldInput request, ServerCallContext context)
        {
            await _dataAccess.UpdateTaskFieldAsync(request.TaskId, request.NewValue, request.ColumnName);

            return new Empty();
        }
        #endregion
    }
}
