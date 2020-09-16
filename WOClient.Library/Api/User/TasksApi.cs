using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WOClient.Library.Api.Protos;
using WOClient.Library.Models;
using WOCommon.Enums;

namespace WOClient.Library.Api.User
{
    internal class TasksApi: BaseApi
    {
        public TasksApi(CommentApi commentApi)
        {
            _commentApi = commentApi;
        }

        #region Fields
        private CommentApi _commentApi;
        #endregion

        #region Internal Methods
        internal async Task<int> AddTaskAsync(GrpcChannel channel,
                                              int managerId,
                                              int assignedEmployee,
                                              DateTime createDate,
                                              string description,
                                              DateTime finalDate,
                                              PriorityEnum priority,
                                              string subject)
        {
            var client = new Tasks.TasksClient(channel);
            var input  = new TaskInput
            {
                ManagerId        = managerId,
                AssignedEmployee = assignedEmployee,
                CreateDate       = createDate.Date.ToUniversalTime().ToTimestamp(),
                Description      = description,
                FinalDate        = finalDate.Date.ToUniversalTime().ToTimestamp(),
                Priority         = priority.ToString(),
                Subject          = subject
            };
            var taskId = await client.AddTaskAsync(input);

            await SendUpdateEventAsync(channel, assignedEmployee);

            return taskId.Value;
        }
        internal async Task<ObservableCollection<MyTask>> GetMyTasksAsync(GrpcChannel channel, int personId)
        {
            var tasksClient    = new Tasks.TasksClient(channel);
            var input          = new Int32Value
            {
                Value = personId
            };

            using var result = tasksClient.GetMyTasks(input);
            var tasks = new ObservableCollection<MyTask>();

            while (await result.ResponseStream.MoveNext())
            {
                if (result.ResponseStream.Current.TaskId == 0) return tasks;

                var task = new MyTask(true)
                {
                    TaskId           = result.ResponseStream.Current.TaskId,
                    AssignedEmployee = result.ResponseStream.Current.EmployeeId,
                    Description      = result.ResponseStream.Current.Description,
                    FinalDate        = result.ResponseStream.Current.FinalDate.ToDateTime().ToLocalTime(),
                    CreatedDate      = result.ResponseStream.Current.CreatedDate.ToDateTime().ToLocalTime(),
                    Priority         = ConvertStringToProretyEnum(result.ResponseStream.Current.Priority),
                    Subject          = result.ResponseStream.Current.Subject,
                    IsArchive        = result.ResponseStream.Current.IsArchive,
                    IsCompleted      = result.ResponseStream.Current.IsCompleted
                };

                task.SetInitModeFalse();

                var comments = await _commentApi.GetCommentsOfTaskAsync(channel, task.TaskId);

                task.Comments = comments;

                tasks.Add(task);
            }

            return tasks;
        }
        internal async Task DeleteTaskAsync(GrpcChannel channel, int taskId, int userIdToBeUpdated)
        {
            var client = new Tasks.TasksClient(channel);
            var input  = new Int32Value
            {
                Value = taskId
            };

            await client.DeleteTaskAsync(input);
            await SendUpdateEventAsync(channel, userIdToBeUpdated);
        }
        internal async Task<ObservableCollection<MyTask>> GetTrackingTasksAsync(GrpcChannel channel, int personId)
        {
            var client = new Tasks.TasksClient(channel);
            var input = new Int32Value
            {
                Value = personId
            };

            using var result = client.GetTrackingTasks(input);
            var tasks = new ObservableCollection<MyTask>();

            while (await result.ResponseStream.MoveNext())
            {
                if (result.ResponseStream.Current.TaskId == 0) return tasks;

                var task = new MyTask(true)
                {
                    TaskId           = result.ResponseStream.Current.TaskId,
                    AssignedEmployee = result.ResponseStream.Current.EmployeeId,
                    Description      = result.ResponseStream.Current.Description,
                    FinalDate        = result.ResponseStream.Current.FinalDate.ToDateTime().ToLocalTime(),
                    CreatedDate      = result.ResponseStream.Current.CreatedDate.ToDateTime().ToLocalTime(),
                    Priority         = ConvertStringToProretyEnum(result.ResponseStream.Current.Priority),
                    Subject          = result.ResponseStream.Current.Subject,
                    IsArchive        = result.ResponseStream.Current.IsArchive,
                    IsCompleted      = result.ResponseStream.Current.IsCompleted
                };

                task.SetInitModeFalse();

                var comments = await _commentApi.GetCommentsOfTaskAsync(channel, task.TaskId);

                task.Comments = comments;

                tasks.Add(task);
            }

            return tasks;
        }
        #endregion

        #region Private Methods
        private PriorityEnum ConvertStringToProretyEnum(string priority)
        {
            switch (priority)
            {
                case "Low":
                    return PriorityEnum.Low;
                case "Medium":
                    return PriorityEnum.Medium;
                case "High":
                    return PriorityEnum.High;
                default:
                    return PriorityEnum.Low;
            }
        }
        #endregion
    }
}
