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
        Task<int> EmployeeRegisterAsync(string firstName,
                                        string lastName,
                                        string email,
                                        SecureString password,
                                        PermissionsEnum permission,
                                        int directManager);
        Task<ObservableCollection<Comment>> GetCommentsOfTaskAsync(int taskId);
        Task<ObservableCollection<IPerson>> GetEmployeesAsync(int managerId);
        Task<ObservableCollection<MyTask>> GetMyTasksAsync(int personId);
        Task<ObservableCollection<MyTask>> GetTrackingTasksAsync(int personId);
        Task LoginAsync(string email, SecureString password);
        Task<bool> RequestUserUpdateAsync(int userId);
        Task UpdateTaskFieldAsync(int taskId, bool value, string columnName);
        Task UpdateUserFieldAsync<T>(int personId, T value, string columnName);
        #endregion
    }
}