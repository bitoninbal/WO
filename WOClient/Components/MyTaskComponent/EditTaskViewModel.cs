using MaterialDesignThemes.Wpf;
using System;
using System.Windows.Input;
using WOClient.Components.Base;
using WOClient.Components.Main;
using WOClient.Library.Models;
using WOClient.Resources.Commands;
using WOCommon.Enums;

namespace WOClient.Components.MyTaskComponent
{
    public class EditTaskViewModel: BaseViewModel, IBaseViewModel
    {
        public EditTaskViewModel(MyTask task)
        {
            EditTaskCommand = new RelayCommand(EditTask);
            _task           = task;

            Init();
        }

        #region Fields
        private string _description;
        private string _subject;
        private DateTime _finalDate;
        private PriorityEnum _priority;
        private IPerson _selectedEmployee;
        private MyTask _task;
        #endregion

        #region ICommands
        public ICommand EditTaskCommand { get; }
        #endregion

        #region Properties
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
        #endregion

        #region Private Methods
        private void Init()
        {
            var loggedInManager = IMainWindowViewModel.User as Manager;

            Description      = _task.Description;
            FinalDate        = _task.FinalDate;
            Priority         = _task.Priority;
            SelectedEmployee = loggedInManager.GetEmplyee(_task.AssignedEmployee);
            Subject          = _task.Subject;
        }
        private void EditTask()
        {
            var loggedInManager = IMainWindowViewModel.User as Manager;

            if (!Description.Equals(_task.Description)) _task.Description = Description;
            if (FinalDate.Date != _task.FinalDate.Date) _task.FinalDate = FinalDate;
            if (Priority != _task.Priority) _task.Priority = Priority;
            if (SelectedEmployee.PersonId != _task.AssignedEmployee) _task.AssignedEmployee = SelectedEmployee.PersonId;
            if (!Subject.Equals(_task.Subject)) _task.Subject = Subject;

            DialogHost.CloseDialogCommand.Execute(null, null);
        }
        #endregion
    }
}
