using System;
using System.Collections.Generic;
using System.Linq;
using WOClient.Library.Models.Reports.Data;

namespace WOClient.Library.Models.Reports
{
    public class NumberOfOpenTasksReport: Report
    {
        #region Public Methods
        public override List<object> GenerateReport(List<MyTask> tasks, DateTime fromDate, DateTime toDate)
        {
            var result = new List<object>();
            var filter = tasks.Where((task) => task.CreatedDate.Date >= fromDate.Date && task.CreatedDate.Date <= toDate.Date && !task.IsCompleted)
                              .OrderBy((task) => task.CreatedDate)
                              .ToArray();
            var counter = 0;
            DateTime date;

            if (filter.Length == 0) return result;

            date = filter[0].CreatedDate;

            for (var i = 0; i < filter.Count(); i++)
            {
                if (date.Date != filter[i].CreatedDate.Date)
                {
                    result.Add(new OpenTasksData
                    {
                        CurrentDay = date,
                        NumberOfTasks = counter
                    });

                    counter = 1;
                    date = filter[i].CreatedDate;
                }
                else
                {
                    counter++;
                }
            }

            result.Add(new OpenTasksData
            {
                CurrentDay = date,
                NumberOfTasks = counter
            });

            return result;
        }
        #endregion
    }
}
