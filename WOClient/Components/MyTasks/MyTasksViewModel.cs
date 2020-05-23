using System;
using System.Collections.ObjectModel;
using WOClient.Components.Base;
using WOClient.Enums;
using WOClient.Models;

namespace WOClient.Components.MyTasks
{
    public class MyTasksViewModel: BaseViewModel, IMyTasksViewModel
    {
        public MyTasksViewModel()
        {
            Tasks = new ObservableCollection<MyTask>();

            var task1 = new MyTask
            {
                TaskName = "Amir's Task",
                FinalDate = DateTime.Now,
                Priority  = PriorityEnum.High,
                Description = "Very good task. Task is very good."
            };

            var task2 = new MyTask
            {
                TaskName = "Inbal's Task",
                FinalDate = DateTime.Now,
                Priority = PriorityEnum.High,
                Description = "Not so Very good task. Task is very good."
            };

            Tasks.Add(task1);
            Tasks.Add(task2);
        }

        #region Properties
        public ObservableCollection<MyTask> Tasks { get; set; }
        #endregion
    }
}
