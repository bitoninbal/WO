using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WODataAccess.Models;

namespace WODataAccess.User
{
    public interface ITasksDataAccess
    {
        Task<int> AddTaskDataAccessAsync(DateTime finalDate, 
                                         int      employeeId, 
                                         int      managerId, 
                                         string   priority, 
                                         string   description, 
                                         string   subject);
        Task<IEnumerable<TaskModel>> GetMyTasksDataAccessAsync(int personId);
        Task<IEnumerable<TaskModel>> GetTrackingTasksDataAccessAsync(int personId);
        Task UpdateTaskFieldAsync(int id, bool newValue, string columnName);
        Task DeleteTaskAsync(int value);
        Task UpdateManagerIdAsync(int oldManagerId, int newManagerId);
    }
}
