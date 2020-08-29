using System;
using System.Collections.Generic;
using WOClient.Library.Models.Reports.Data;

namespace WOClient.Library.Models.Reports
{
    public class AllTasksReport: Report
    {
        public override List<object> GenerateReport(List<MyTask> tasks, DateTime fromDate, DateTime toDate)
        {
            var result = new List<object>();

            foreach (var task in tasks)
            {
                result.Add(new TaskData
                {
                    IsArchive = task.IsArchive,
                    CreatedDate = task.CreatedDate,
                    IsCompleted = task.IsCompleted,
                    FinalDate = task.FinalDate,
                    Priority = task.Priority,
                    Subject = task.Subject
                });
            }

            return result;
        }
    }
}
