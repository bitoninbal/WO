using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WODataAccess.Models;

namespace WODataAccess.Interfaces
{
    public interface ITasksDataAccess
    {
        Task<int> AddTaskDataAccessAsync(DateTime finalDate,
                                         int employeeId,
                                         int managerId,
                                         string priority,
                                         string description,
                                         string subject);
        Task DeleteTaskAsync(int value);
        Task<IEnumerable<TaskModel>> GetMyTasksDataAccessAsync(int personId);
        Task<IEnumerable<TaskModel>> GetTrackingTasksDataAccessAsync(int personId);
    }
}
