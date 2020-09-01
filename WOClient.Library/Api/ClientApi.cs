using Grpc.Net.Client;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Security;
using System.Threading.Tasks;
using WOClient.Library.Api.User;
using WOClient.Library.Models;
using WOCommon.Enums;
using WOCommon.Extensions;

namespace WOClient.Library.Api
{
    public class ClientApi: IClientApi
    {
        public ClientApi()
        {
            _commentApi = new CommentApi();
            _tasksApi   = new TasksApi(_commentApi);
            _usersApi   = new UsersApi();
        }

        #region Fields
        private readonly CommentApi _commentApi;
        private readonly TasksApi _tasksApi;
        private readonly UsersApi _usersApi;
        #endregion

        #region Public Methods
        public async Task<int> AddCommentAsync(int taskId,
                                               int senderId,
                                               string comment)
        {
            var channel   = GetChannel();
            var commentId = await _commentApi.AddCommentAsync(channel,
                                                              taskId,
                                                              senderId,
                                                              comment);

            await channel.ShutdownAsync();

            return commentId;
        }
        public async Task<int> AddTaskAsync(DateTime finalDate,
                                            int employeeId,
                                            int managerId,
                                            PriorityEnum priority,
                                            string description,
                                            string subject)
        {
            var channel = GetChannel();
            var taskId  = await _tasksApi.AddTaskAsync(channel, finalDate, employeeId, managerId, priority, description, subject);

            await channel.ShutdownAsync();

            return taskId;
        }
        public async Task DeleteEmployeeAsync(int employeeId)
        {
            var channel = GetChannel();

            await _usersApi.DeleteEmployeeAsync(channel, employeeId);
            await channel.ShutdownAsync();
        }
        public async Task<int> EmployeeRegisterAsync(string firstName,
                                                     string lastName,
                                                     string email,
                                                     SecureString password,
                                                     PermissionsEnum permission,
                                                     int directManager)
        {
            var channel        = GetChannel();
            var hashedPassword = password.HashValue();
            int result;

            if (await _usersApi.IsMailExistAsync(channel, email))
                result = 0;
            else
                result = await _usersApi.EmployeeRegisterAsync(channel, firstName, lastName, email, hashedPassword, permission, directManager);

            await channel.ShutdownAsync();

            return result;
        }
        public async Task<ObservableCollection<Comment>> GetCommentsOfTaskAsync(int taskId)
        {
            var channel = GetChannel();
            var result  = await _commentApi.GetCommentsOfTaskAsync(channel, taskId);

            await channel.ShutdownAsync();

            return result;
        }
        public async Task<ObservableCollection<IPerson>> GetEmployeesAsync(int personId)
        {
            var channel = GetChannel();
            var result = await _usersApi.GetEmployeesAsync(channel, personId);

            await channel.ShutdownAsync();

            return result;
        }
        public async Task<ObservableCollection<MyTask>> GetMyTasksAsync(int personId)
        {
            var channel = GetChannel();
            var result  = await _tasksApi.GetMyTasksAsync(channel, personId);

            await channel.ShutdownAsync();

            return result;
        }
        public async Task<ObservableCollection<MyTask>> GetTrackingTasksAsync(int personId)
        {
            var channel = GetChannel();
            var result = await _tasksApi.GetTrackingTasksAsync(channel, personId);

            await channel.ShutdownAsync();

            return result;
        }
        public async Task LoginAsync(string userName, SecureString password)
        {
            var channel        = GetChannel();
            var hashedPassword = password.HashValue();

            await _usersApi.LoginAsync(channel, userName, hashedPassword);
            await channel.ShutdownAsync();
        }
        public async Task UpdateUserFieldAsync<T>(int personId, T value, string columnName)
        {
            int intValue = 0;
            string stringValue = string.Empty;
            Type type;
            var channel = GetChannel();

            switch (value)
            {
                case int number:
                    type = typeof(int);
                    intValue = number;

                    break;
                case PermissionsEnum permission:
                    type = typeof(string);
                    stringValue = permission.ToString();

                    break;
                case string str:
                    type = typeof(string);
                    stringValue = str;

                    break;
                default:
                    return;
            }

            switch (type.Name)
            {
                case "Int32":
                    await _usersApi.UpdateUserFieldAsync(channel, personId, intValue, columnName);

                    break;
                case "String":
                    await _usersApi.UpdateUserFieldAsync(channel, personId, stringValue, columnName);

                    break;
                default:
                    return;
            }

            await channel.ShutdownAsync();
        }
        public async Task UpdateTaskFieldAsync(int taskId, bool value, string columnName)
        {
            var channel = GetChannel();

            await _tasksApi.UpdateTaskFieldAsync(channel, taskId, value, columnName);
            await channel.ShutdownAsync();
        }
        #endregion

        #region Private Methods
        private GrpcChannel GetChannel()
        {
            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator,
            };
            var httpClient = new HttpClient(httpClientHandler);
            var options = new GrpcChannelOptions { HttpClient = httpClient };

            return GrpcChannel.ForAddress("https://127.0.0.1:5001", options);
        }
        #endregion
    }
}
