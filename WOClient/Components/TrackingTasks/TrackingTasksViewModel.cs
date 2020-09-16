using MaterialDesignThemes.Wpf;
using System.Windows.Input;
using WOClient.Components.Base;
using WOClient.Components.NewTask;
using WOClient.Resources.Commands;

namespace WOClient.Components.TrackingTasks
{
    public class TrackingTasksViewModel: BaseViewModel, ITrackingTasksViewModel
    {
        public TrackingTasksViewModel(INewTaskViewModel newTaskVm)
        {
            OpenNewTaskCommand = new RelayCommand(OpenNewTask);

            _newTaskVm         = newTaskVm;
        }

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

                NotifyPropertyChanged(nameof(NewTaskVm));
            }
        }
        #endregion

        #region ICommands
        public ICommand OpenNewTaskCommand { get; }
        #endregion

        #region Public Methods
        public void Reset()
        {
            NewTaskVm.Reset();
        }
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
