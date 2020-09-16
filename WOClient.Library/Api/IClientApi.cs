using System;
using System.Collections.ObjectModel;
using System.Security;
using System.Threading.Tasks;
using WOClient.Library.Models;
using WOCommon.Enums;

namespace WOClient.Library.Api
{
    public interface IClientApi
    {
        #region Methods
        Task<int> AddCommentAsync(int taskId,
                                  int senderd,
                                  int userIdToBeUpdated,
                                  string comment);
        Task<int> AddTaskAsync(DateTime finalDate,
                               int employeeId,
                               int managerId,
                               PriorityEnum priority,
                               string description,
                               string subject);
        Task DeleteEmployeeAsync(int employeeId);
        Task DeleteTaskAsync(int taskId, int userIdToBeUpdated);
        Task<int> EmployeeRegisterAsync(string firstName,
                                        string lastName,
                                        string email,
                                        SecureString password,
                                        PermissionsEnum permission,
                                        int directManager);
        Task<ObservableCollection<IPerson>> GetEmployeesAsync(int managerId);
        Task<ObservableCollection<MyTask>> GetMyTasksAsync(int personId);
        Task<ObservableCollection<MyTask>> GetTrackingTasksAsync(int personId);
        Task LoginAsync(string email, SecureString password);
        Task<bool> RequestUserUpdateAsync(int userId);
        Task UpdateTaskDbFiledAsync<T>(int taskId, T value, string columnName);
        Task UpdateUserDbFiledAsync<T>(int userId, T value, string columnName);
        Task SendUpdateEventAsync(int userId);
        #endregion
    }
}