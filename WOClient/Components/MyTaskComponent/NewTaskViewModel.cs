using MaterialDesignThemes.Wpf;
using System;
using System.Threading.Tasks;
using WOClient.Components.Base;
using WOClient.Components.Main;
using WOClient.Library.Models;
using WOCommon.Enums;

namespace WOClient.Components.MyTaskComponent
{
    public class NewTaskViewModel: BaseViewModel, INewTaskViewModel
    {
        public NewTaskViewModel()
        {
            _description = string.Empty;
        }

        #region Fields
        private string _description;
        private string _subject;
        private DateTime _finalDate = DateTime.Now;
        private IPerson _selectedEmployee;
        private PriorityEnum _priority = PriorityEnum.Low;
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

                var loggedInManager = IMainWindowViewModel.User as Manager;
                var result = await loggedInManager.TryAddTaskAsync(SelectedEmployee.PersonId,
                                                                            Description,
                                                                            FinalDate,
                                                                            Priority,
                                                                            Subject);

                if (!result) throw new Exception();

                SetPropertiesToDefault();
            }
            catch (Exception e)
            {
                IMainWindowViewModel.MessageQueue.Enqueue("Could not connect to server.", "OK", (obj) => { }, new object(), false, true, TimeSpan.FromSeconds(6));
            }
        }
        public void Reset()
        {
            SetPropertiesToDefault();
        }
        #endregion

        #region Private Methods
        private void SetPropertiesToDefault()
        {
            FinalDate = DateTime.Now;
            Priority = PriorityEnum.Low;
            Description = string.Empty;
            Subject = default;
        }
        #endregion
    }
}
