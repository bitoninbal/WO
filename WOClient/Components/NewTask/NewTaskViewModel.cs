using MaterialDesignThemes.Wpf;
using System;
using System.Threading.Tasks;
using WOClient.Components.Base;
using WOClient.Components.Main;
using WOClient.Library.Api;
using WOClient.Library.Models;
using WOCommon.Enums;

namespace WOClient.Components.NewTask
{
    public class NewTaskViewModel: BaseViewModel, INewTaskViewModel
    {
        public NewTaskViewModel(IClientApi api)
        {
            _api         = api;
            _description = string.Empty;
        }

        #region Fields
        private string       _description;
        private string       _subject;
        private DateTime     _finalDate = DateTime.Now;
        private IClientApi   _api;
        private IPerson      _selectedEmployee;
        private PriorityEnum _priority  = PriorityEnum.Low;
        #endregion

        #region Properties
        public DateTime FinalDate
        {
            get => _finalDate;
            set
            {
                if (_finalDate == value) return;

                _finalDate = value;
                NotifyPropertyChanged(nameof(FinalDate));
            }
        }
        public IPerson SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                if (_selectedEmployee == value) return;

                _selectedEmployee = value;
                NotifyPropertyChanged(nameof(SelectedEmployee));
            }
        }
        public PriorityEnum Priority
        {
            get => _priority;
            set
            {
                if (_priority == value) return;

                _priority = value;
                NotifyPropertyChanged(nameof(Priority));
            }
        }
        public string Description
        {
            get => _description;
            set
            {
                if (_description == value) return;

                _description = value;
                NotifyPropertyChanged(nameof(Description));
            }
        }
        public string Subject
        {
            get => _subject;
            set
            {
                if (_subject == value) return;

                _subject = value;
                NotifyPropertyChanged(nameof(Subject));
            }
        }
        #endregion

        #region Public Methods
        public async Task SendNewTaskAsync()
        {
            try
            {
                DialogHost.CloseDialogCommand.Execute(null, null);

                var taskId = await _api.AddTaskAsync(FinalDate.Date.ToUniversalTime(),
                                                     SelectedEmployee.PersonId,
                                                     SelectedEmployee.ManagerId,
                                                     Priority,
                                                     Description,
                                                     Subject);
                var newTask = new MyTask(true)
                {
                    Subject     = Subject,
                    Description = Description,
                    FinalDate   = FinalDate,
                    Priority    = Priority,
                    TaskId      = taskId
                };

                newTask.SetInitModeFalse();

                var user = IMainWindowViewModel.User as Manager;

                user.TrackingTasks.Add(newTask);
                AddTaskToEmployee(newTask);
                SetPropertiesToDefault();
            }
            catch (Exception e)
            {
                IMainWindowViewModel.MessageQueue.Enqueue("Could not connect to server.", "OK", (obj) => { }, new object(), false, true, TimeSpan.FromSeconds(6));
            }
        }
        #endregion

        #region Private Methods
        private void AddTaskToEmployee(MyTask newTask)
        {
            var user = IMainWindowViewModel.User as Manager;

            foreach (var employee in user.MyEmployees)
            {
                if (employee.PersonId != SelectedEmployee.PersonId) continue;

                employee.MyTasks.Add(newTask);
            }
        }
        private void SetPropertiesToDefault()
        {
            FinalDate   = DateTime.Now;
            Priority    = PriorityEnum.Low;
            Description = string.Empty;
            Subject     = default;
        }
        #endregion
    }
}
