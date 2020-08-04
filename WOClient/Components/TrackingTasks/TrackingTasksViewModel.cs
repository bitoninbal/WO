using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WOClient.Components.Base;
using WOClient.Components.NewTask;
using WOClient.Library.Api;
using WOClient.Library.Models;
using WOClient.Resources.Commands;

namespace WOClient.Components.TrackingTasks
{
    public class TrackingTasksViewModel: MyTaskViewModel, ITrackingTasksViewModel
    {
        public TrackingTasksViewModel(INewTaskViewModel newTaskVm, IClientApi api) : base(api)
        {
            OpenNewTaskCommand = new RelayCommand(OpenNewTask);
            Tasks              = new ObservableCollection<MyTask>();
            _newTaskVm         = newTaskVm;
        }

        #region ICommands
        public ICommand OpenNewTaskCommand { get; }
        #endregion

        #region Fields
        private INewTaskViewModel _newTaskVm;
        #endregion

        #region Properties
        public INewTaskViewModel NewTaskVm
        {
            get => _newTaskVm;
            set
            {
                if (_newTaskVm == value) return;

                _newTaskVm = value;
                NotifyPropertyChanged("NewTaskVm");
            }
        }

        public ObservableCollection<MyTask> Tasks { get; set; }
        #endregion

        #region Private Methods
        private async void OpenNewTask()
        {
            var view = new NewTaskView
            {
                DataContext = _newTaskVm
            };

            await DialogHost.Show(view, "RootDialog");
        }
        #endregion
    }
}
