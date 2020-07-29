using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WODataAccess.Models;

namespace WODataAccess.User
{
    public class TasksDataAccess : BaseDataAccess ,ITasksDataAccess
    {
       public async Task<int> AddTaskDataAccessAsync(DateTime finalDate,
                                                int      employeeId,
                                                int      managerId,
                                                string   priority,
                                                string   description,
                                                string   subject)
        {
            var cnn = new SqlConnection(ConnectionString);
            var query = "INSERT INTO Tasks(UserId, ManagerId, Subject, FinalDate, Description, Priority) " +
                        "VALUES(@UserId, @ManagerId, @Subject, @FinalDate, @Description, @Priority)";
            var cmd = new SqlCommand(query, cnn);

            cmd.Parameters.AddWithValue("@UserId", employeeId);
            cmd.Parameters.AddWithValue("@ManagerId", managerId);
            cmd.Parameters.AddWithValue("@Subject", subject);
            cmd.Parameters.AddWithValue("@FinalDate", finalDate);
            cmd.Parameters.AddWithValue("@Description", description);
            cmd.Parameters.AddWithValue("@Priority", priority);
            cmd.CommandType = CommandType.Text;

            try
            {
                await cnn.OpenAsync();
                var result = await cmd.ExecuteScalarAsync();
                var taskId = Convert.ToInt32(result);
                return taskId;
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                await cmd.DisposeAsync();
                await cnn.CloseAsync();
            }
        }
        public Task<IEnumerable<TaskModel>> GetTrackingTasksDataAccessAsync(int personId)
        {
            return null;
        }

    }
}
