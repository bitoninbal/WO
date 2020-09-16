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
            _commentApi   = new CommentApi();
            _dbUpdatesApi = new DbUpdatesApi();
            _tasksApi     = new TasksApi(_commentApi);
            _usersApi     = new UsersApi();
        }

        #region Fields
        private readonly CommentApi   _commentApi;
        private readonly DbUpdatesApi _dbUpdatesApi;
        private readonly TasksApi     _tasksApi;
        private readonly UsersApi     _usersApi;
        #endregion

        #region Public Methods
        public async Task<int> AddCommentAsync(int taskId,
                                               int senderId,
                                               int userIdToBeUpdated,
                                               string comment)
        {
            var channel   = GetChannel();
            var commentId = await _commentApi.AddCommentAsync(channel,
                                                              taskId,
                                                              senderId,
                                                              userIdToBeUpdated,
                                                              comment);

            await channel.ShutdownAsync();
            return commentId;
        }
        public async Task<int> AddTaskAsync(int managerId,
                                            int assignedEmployee,
                                            DateTime createDate,
                                            string description,
                                            DateTime finalDate,
                                            PriorityEnum priority,
                                            string subject)
        {
            var channel = GetChannel();
            var taskId  = await _tasksApi.AddTaskAsync(channel,
                                                       managerId,
                                                       assignedEmployee,
                                                       createDate,
                                                       description,
                                                       finalDate,
                                                       priority,
                                                       subject);

            await channel.ShutdownAsync();

            return taskId;
        }
        public async Task DeleteEmployeeAsync(int employeeId)
        {
            var channel = GetChannel();

            await _usersApi.DeleteEmployeeAsync(channel, employeeId);
            await channel.ShutdownAsync();
        }
        public async Task DeleteTaskAsync(int taskId, int userIdToBeUpdated)
        {
            var channel = GetChannel();

            await _tasksApi.DeleteTaskAsync(channel, taskId, userIdToBeUpdated);
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
            var result = await _tasksApi.GetMyTasksAsync(channel, personId);

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
        public async Task<bool> RequestUserUpdateAsync(int userId)
        {
            var channel = GetChannel();

            try
            {
                var result = await _usersApi.RequestUserUpdateAsync(channel, userId);

                return result;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                await channel.ShutdownAsync();
            }
        }
        public async Task UpdateTaskDbFiledAsync<T>(int personId, T value, string columnName)
        {
            await UpdateDbFiledAsync(personId, columnName, DbTables.Tasks, value);
        }
        public async Task UpdateUserDbFiledAsync<T>(int personId, T value, string columnName)
        {
            await UpdateDbFiledAsync(personId, columnName, DbTables.Users, value);
        }
        public async Task SendUpdateEventAsync(int userId)
        {
            var channel = GetChannel();

            await _tasksApi.SendUpdateEventAsync(channel, userId);
            await channel.ShutdownAsync();
        }
        #endregion

        #region Private Methods
        private GrpcChannel GetChannel()
        {
            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            var httpClient = new HttpClient(httpClientHandler);
            var options = new GrpcChannelOptions
            {
                HttpClient = httpClient
            };

            return GrpcChannel.ForAddress("https://192.168.1.234:5001", options);
        }
        private async Task UpdateDbFiledAsync<T>(int personId, string columnName, DbTables tableName, T value)
        {
            Type type;
            bool boolValue     = false;
            int intValue       = 0;
            DateTime dateValue = DateTime.Now;
            string stringValue = string.Empty;
            var channel        = GetChannel();

            switch (value)
            {
                case bool boolean:
                    type      = typeof(bool);
                    boolValue = boolean;

                    break;
                case int number:
                    type     = typeof(int);
                    intValue = number;

                    break;
                case DateTime date:
                    type      = typeof(DateTime);
                    dateValue = date.ToUniversalTime();

                    break;
                case PermissionsEnum permission:
                    type        = typeof(string);
                    stringValue = permission.ToString();

                    break;
                case PriorityEnum priority:
                    type        = typeof(string);
                    stringValue = priority.ToString();

                    break;
                case string str:
                    type        = typeof(string);
                    stringValue = str;

                    break;
                default:
                    return;
            }

            switch (type.Name)
            {
                case "Boolean":
                    await _dbUpdatesApi.UpdateFieldAsync(channel, personId, columnName, tableName, boolValue);

                    break;
                case "DateTime":
                    await _dbUpdatesApi.UpdateFieldAsync(channel, personId, columnName, tableName, dateValue);

                    break;
                case "Int32":
                    await _dbUpdatesApi.UpdateFieldAsync(channel, personId, columnName, tableName, intValue);

                    break;
                case "String":
                    await _dbUpdatesApi.UpdateFieldAsync(channel, personId, columnName, tableName, stringValue);

                    break;
                default:
                    return;
            }

            await channel.ShutdownAsync();
        }
        #endregion
    }
}
