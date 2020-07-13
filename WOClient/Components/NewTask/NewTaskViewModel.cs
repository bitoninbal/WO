using System;
using System.Threading.Tasks;
using WOClient.Components.Base;
using WOClient.Models;
using WOCommon.Enums;

namespace WOClient.Components.NewTask
{
    public class NewTaskViewModel: BaseViewModel, INewTaskViewModel
    {
        #region Fields
        private DateTime     _finalDate = DateTime.Now;
        private IPerson      _selectedEmployee;
        private PriorityEnum _priority  = PriorityEnum.Low;
        private string       _description;
        private string       _subject;
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
            //try
            //{
            //    await _api.EmployeeRegisterAsync(FirstName, LastName, Email, Password.Copy(), Permission, DirectManager);
            //}
            //catch (Exception)
            //{
            //    MainWindowViewModel.MessageQueue.Enqueue("Could not connect to server.", "OK", (obj) => { }, new object(), false, true, TimeSpan.FromSeconds(6));
            //}
        }
        #endregion
    }
}
