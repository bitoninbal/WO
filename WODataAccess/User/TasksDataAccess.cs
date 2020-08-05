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
            var query = "INSERT INTO Tasks(UserId, ManagerId, Subject, FinalDate, Description, Priority, IsCompleted) " +
                        "VALUES(@UserId, @ManagerId, @Subject, @FinalDate, @Description, @Priority, @IsCompleted); SELECT SCOPE_IDENTITY();";
            var cmd = new SqlCommand(query, cnn);

            cmd.Parameters.AddWithValue("@UserId", employeeId);
            cmd.Parameters.AddWithValue("@ManagerId", managerId);
            cmd.Parameters.AddWithValue("@Subject", subject);
            cmd.Parameters.AddWithValue("@FinalDate", finalDate);
            cmd.Parameters.AddWithValue("@Description", description);
            cmd.Parameters.AddWithValue("@Priority", priority);
            cmd.Parameters.AddWithValue("@IsCompleted", 0);
            cmd.CommandType = CommandType.Text;

            try
            {
                await cnn.OpenAsync();

                var result = await cmd.ExecuteScalarAsync();
                var taskId = Convert.ToInt32(result);

                return taskId;
            }
            catch (Exception e)
            {
                return 0;
            }
            finally
            {
                await cmd.DisposeAsync();
                await cnn.CloseAsync();
            }
        }
        public async Task<IEnumerable<TaskModel>> GetMyTasksDataAccessAsync(int personId)
        {
            var cnn = new SqlConnection(ConnectionString);
            var query = "SELECT Id, Subject, FinalDate, Description, Priority, IsCompleted, UserId FROM Tasks WHERE UserId = @UserId";
            var cmd = new SqlCommand(query, cnn);

            cmd.Parameters.AddWithValue("@UserId", personId);
            cmd.CommandType = CommandType.Text;

            try
            {
                await cnn.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (!reader.HasRows) return null;

                    var tasks = new List<TaskModel>();

                    while (await reader.ReadAsync())
                    {
                        tasks.Add(new TaskModel
                        {
                            TaskId      = await reader.GetFieldValueAsync<int>(0),
                            Subject     = await reader.GetFieldValueAsync<string>(1),
                            FinalDate   = await reader.GetFieldValueAsync<DateTime>(2),
                            Description = await reader.GetFieldValueAsync<string>(3),
                            Priority    = await reader.GetFieldValueAsync<string>(4),
                            IsCompleted = await reader.GetFieldValueAsync<bool>(5),
                            UserId      = await reader.GetFieldValueAsync<int>(6)
                        });
                    }

                    return tasks;
                }
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                await cmd.DisposeAsync();
                await cnn.CloseAsync();
            }
        }
        public async Task<IEnumerable<TaskModel>> GetTrackingTasksDataAccessAsync(int personId)
        {
            var cnn = new SqlConnection(ConnectionString);
            var query = "SELECT Id, Subject, FinalDate, Description, Priority, IsCompleted UserId FROM Tasks WHERE ManagerId = @ManagerId";
            var cmd = new SqlCommand(query, cnn);

            cmd.Parameters.AddWithValue("@ManagerId", personId);
            cmd.CommandType = CommandType.Text;

            try
            {
                await cnn.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (!reader.HasRows) return null;

                    var tasks = new List<TaskModel>();

                    while (await reader.ReadAsync())
                    {
                        tasks.Add(new TaskModel
                        {
                            TaskId      = await reader.GetFieldValueAsync<int>(0),
                            Subject     = await reader.GetFieldValueAsync<string>(1),
                            FinalDate   = await reader.GetFieldValueAsync<DateTime>(2),
                            Description = await reader.GetFieldValueAsync<string>(3),
                            Priority    = await reader.GetFieldValueAsync<string>(4),
                            IsCompleted = await reader.GetFieldValueAsync<bool>(5),
                            UserId      = await reader.GetFieldValueAsync<int>(6)
                        });
                    }

                    return tasks;
                }
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                await cmd.DisposeAsync();
                await cnn.CloseAsync();
            }
        }
        public async Task UpdateCompletedTaskFieldAsync(int id, bool newValue)
        {
            var cnn = new SqlConnection(ConnectionString);
            var query = $"UPDATE Tasks SET IsCompleted = @NewValue WHERE Id = @Id";
            var cmd = new SqlCommand(query, cnn);

            cmd.Parameters.AddWithValue("@NewValue", newValue);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.CommandType = CommandType.Text;

            try
            {
                await cnn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {
                return;
            }
            finally
            {
                await cmd.DisposeAsync();
                await cnn.CloseAsync();
            }
        }
    }
}
