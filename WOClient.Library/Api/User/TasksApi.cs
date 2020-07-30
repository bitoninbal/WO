﻿using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WOClient.Library.Api.Protos;
using WOClient.Library.Models;
using WOCommon.Enums;

namespace WOClient.Library.Api.User
{
    public class TasksApi
    {
        #region Internal Methods
        internal async Task<int> AddTaskAsync(GrpcChannel channel,
                                      DateTime finalDate,
                                      int employeeId,
                                      int managerId,
                                      PriorityEnum priority,
                                      string description,
                                      string subject)
        {
            var client = new Tasks.TasksClient(channel);

            var input = new TaskInput
            {
                FinalDate = finalDate.ToTimestamp(),
                EmployeeId = employeeId,
                ManagerId = managerId,
                Priority = priority.ToString(),
                Description = description,
                Subject = subject
            };
            var taskId = await client.AddTaskAsync(input);

            return taskId.Value;
        }
        internal async Task<ObservableCollection<MyTask>> GetMyTasksAsync(GrpcChannel channel, int personId)
        {
            var client = new Tasks.TasksClient(channel);
            var input = new Int32Value
            {
                Value = personId
            };

            using var result = client.GetMyTasks(input);
            var tasks = new ObservableCollection<MyTask>();

            while (await result.ResponseStream.MoveNext())
            {
                if (result.ResponseStream.Current.TaskId == 0) return tasks;

                var task = new MyTask
                {
                    Description = result.ResponseStream.Current.Description,
                    FinalDate   = result.ResponseStream.Current.FinalDate.ToDateTime().ToLocalTime(),
                    Priority    = ConvertStringToProretyEnum(result.ResponseStream.Current.Priority),
                    Subject     = result.ResponseStream.Current.Subject
                };

                tasks.Add(task);
            }

            return tasks;
        }
        internal async Task<ObservableCollection<MyTask>> GetTrackingTasksAsync(GrpcChannel channel, int personId)
        {
            var client = new Tasks.TasksClient(channel);
            var input = new Int32Value
            {
                Value = personId
            };

            using var result = client.GetTrackingTasks(input);
            var trackingTasks = new ObservableCollection<MyTask>();

            while (await result.ResponseStream.MoveNext())
            {
                if (result.ResponseStream.Current.TaskId == 0) return trackingTasks;

                var currTask = new MyTask
                {
                    Description = result.ResponseStream.Current.Description,
                    FinalDate   = result.ResponseStream.Current.FinalDate.ToDateTime().ToLocalTime(),
                    Priority    = ConvertStringToProretyEnum(result.ResponseStream.Current.Priority),
                    Subject     = result.ResponseStream.Current.Subject
                };

                trackingTasks.Add(currTask);
            }

            return trackingTasks;
        }
        #endregion

        #region Private Methods
        private PriorityEnum ConvertStringToProretyEnum(string priority)
        {
            switch (priority)
            {
                case "Low":
                    return PriorityEnum.Low;
                case "Medium":
                    return PriorityEnum.Medium;
                case "High":
                    return PriorityEnum.High;
                default:
                    return PriorityEnum.Low;
            }
        } 
        #endregion
    }
}