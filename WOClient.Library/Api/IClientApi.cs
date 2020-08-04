﻿using System;
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
        Task DeleteEmployeeAsync(int employeeId);
        Task<int> EmployeeRegisterAsync(string firstName,
                                        string lastName,
                                        string email,
                                        SecureString password,
                                        PermissionsEnum permission,
                                        int directManager);
        Task<int> AddTaskAsync(DateTime finalDate,
                          int employeeId,
                          int managerId,
                          PriorityEnum priority,
                          string description,
                          string subject);
        Task<ObservableCollection<IPerson>> GetEmployeesAsync(int managerId);
        Task LoginAsync(string email, SecureString password);
        Task UpdateUserFieldAsync<T>(int personId, T value, string columnName);
        Task<ObservableCollection<MyTask>> GetMyTasksAsync(int personId);
        Task<ObservableCollection<MyTask>> GetTrackingTasksAsync(int personId);
        Task UpdateCompletedTaskFieldAsync(int taskId, bool newValue);
        Task<int> AddCommentAsync(int taskId, int personId, string comment);
        #endregion
    }
}