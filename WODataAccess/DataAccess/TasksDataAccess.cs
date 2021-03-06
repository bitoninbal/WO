﻿using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WODataAccess.Interfaces;
using WODataAccess.Models;

namespace WODataAccess.DataAccess
{
    public class TasksDataAccess: BaseDataAccess, ITasksDataAccess
    {
        public async Task<int> AddTaskDataAccessAsync(int managerId,
                                                      int assignedEmployee,
                                                      DateTime createDate,
                                                      string description,
                                                      DateTime finalDate,
                                                      string priority,
                                                      string subject)
        {
            var cnn   = new SqlConnection(ConnectionString);
            var query = "INSERT INTO Tasks(UserId, ManagerId, Subject, FinalDate, Description, Priority, IsCompleted, CreatedDate) " +
                        "VALUES(@UserId, @ManagerId, @Subject, @FinalDate, @Description, @Priority, @IsCompleted, @CreatedDate); SELECT SCOPE_IDENTITY();";
            var cmd = new SqlCommand(query, cnn);

            cmd.Parameters.AddWithValue("@UserId", assignedEmployee);
            cmd.Parameters.AddWithValue("@ManagerId", managerId);
            cmd.Parameters.AddWithValue("@Subject", subject);
            cmd.Parameters.AddWithValue("@FinalDate", finalDate);
            cmd.Parameters.AddWithValue("@Description", description);
            cmd.Parameters.AddWithValue("@Priority", priority);
            cmd.Parameters.AddWithValue("@IsCompleted", 0);
            cmd.Parameters.AddWithValue("@CreatedDate", createDate);
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
        public async Task DeleteTaskAsync(int taskId)
        {
            var cnn = new SqlConnection(ConnectionString);
            var commentsQuery = "Delete From Comments WHERE TaskId = @TaskId";
            var taskQuery = "Delete From Tasks WHERE Id = @TaskId";
            var commentsCmd = new SqlCommand(commentsQuery, cnn);
            var taskCmd = new SqlCommand(taskQuery, cnn);

            commentsCmd.Parameters.AddWithValue("@TaskId", taskId);
            commentsCmd.CommandType = CommandType.Text;
            taskCmd.Parameters.AddWithValue("@TaskId", taskId);
            taskCmd.CommandType = CommandType.Text;

            try
            {
                await cnn.OpenAsync();
                await commentsCmd.ExecuteNonQueryAsync();
                await taskCmd.ExecuteNonQueryAsync();
            }
            catch (Exception e)
            {
                return;
            }
            finally
            {
                await commentsCmd.DisposeAsync();
                await taskCmd.DisposeAsync();
                await cnn.CloseAsync();
            }
        }
        public async Task<IEnumerable<TaskModel>> GetMyTasksDataAccessAsync(int personId)
        {
            var cnn = new SqlConnection(ConnectionString);
            var query = "SELECT * FROM Tasks WHERE UserId = @UserId";
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
                            TaskId = await reader.GetFieldValueAsync<int>(0),
                            UserId = await reader.GetFieldValueAsync<int>(1),
                            Subject = await reader.GetFieldValueAsync<string>(3),
                            CreatedDate = await reader.GetFieldValueAsync<DateTime>(4),
                            FinalDate = await reader.GetFieldValueAsync<DateTime>(5),
                            Description = await reader.GetFieldValueAsync<string>(6),
                            Priority = await reader.GetFieldValueAsync<string>(7),
                            IsCompleted = await reader.GetFieldValueAsync<bool>(8),
                            IsArchive = await reader.GetFieldValueAsync<bool>(9)
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
            var query = "SELECT * FROM Tasks WHERE ManagerId = @ManagerId";
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
                            TaskId = await reader.GetFieldValueAsync<int>(0),
                            UserId = await reader.GetFieldValueAsync<int>(1),
                            Subject = await reader.GetFieldValueAsync<string>(3),
                            CreatedDate = await reader.GetFieldValueAsync<DateTime>(4),
                            FinalDate = await reader.GetFieldValueAsync<DateTime>(5),
                            Description = await reader.GetFieldValueAsync<string>(6),
                            Priority = await reader.GetFieldValueAsync<string>(7),
                            IsCompleted = await reader.GetFieldValueAsync<bool>(8),
                            IsArchive = await reader.GetFieldValueAsync<bool>(9)
                        });
                    }

                    return tasks;
                }
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                await cmd.DisposeAsync();
                await cnn.CloseAsync();
            }
        }
    }
}
