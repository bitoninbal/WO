using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WOCommon.Enums;
using System.Linq;

namespace WOClient.Library.Models
{
    public class Manager: Person
    {
        public Manager(PermissionsEnum permission,
                       int personId,
                       int managerId,
                       string firstName,
                       string lastName,
                       string email):
            base(permission,
                 personId,
                 managerId,
                 firstName,
                 lastName,
                 email)
        {
            _myEmployees = new ObservableCollection<IPerson>();
            _trackingTasks = new ObservableCollection<MyTask>();

            Task.Run(InitAsync);
        }

        #region Fields
        private bool _isAllTrackingTasksArchived;
        private bool _isTrackingTasksArchivedExists;
        private ObservableCollection<IPerson> _myEmployees;
        private ObservableCollection<MyTask> _trackingTasks;
        #endregion

        #region Properties
        public bool IsAllTrackingTasksArchived
        {
            get => _isAllTrackingTasksArchived;
            set
            {
                if (_isAllTrackingTasksArchived == value) return;

                _isAllTrackingTasksArchived = value;

                NotifyPropertyChanged(nameof(IsAllTrackingTasksArchived));
            }
        }
        public bool IsTrackingTasksArchivedExists
        {
            get => _isTrackingTasksArchivedExists;
            set
            {
                if (_isTrackingTasksArchivedExists == value) return;

                _isTrackingTasksArchivedExists = value;

                NotifyPropertyChanged(nameof(IsTrackingTasksArchivedExists));
            }
        }
        public ObservableCollection<IPerson> MyEmployees
        {
            get => _myEmployees;
            set
            {
                if (_myEmployees == value) return;

                _myEmployees = value;
                NotifyPropertyChanged("MyEmployees");
            }
        }
        public ObservableCollection<MyTask> TrackingTasks
        {
            get => _trackingTasks;
            set
            {
                if (_trackingTasks == value) return;

                _trackingTasks = value;
                NotifyPropertyChanged("TrackingTasks");
            }
        }
        #endregion

        #region Public Methods
        public IPerson GetEmplyee(int id)
        {
            foreach (var emplyee in MyEmployees)
            {
                if (emplyee.PersonId != id) continue;

                return emplyee;
            }

            return null;
        }
        public void CheckIfAllTrackingTasksArchived()
        {
            IsAllTrackingTasksArchived = TrackingTasks.All((task) => task.IsArchive);
        }
        public void CheckIfAnyTrackingTasksArchived()
        {
            IsTrackingTasksArchivedExists = TrackingTasks.Any((task) => task.IsArchive);
        }
        #endregion

        #region Private Methods
        private async Task InitAsync()
        {
            await InitMyEmployeesAsync();
            await InitTrackingTasksAsync();
        }
        private async Task InitMyEmployeesAsync()
        {
            var result = await Api.GetEmployeesAsync(PersonId);

            if (result is null) return;

            MyEmployees = result;
        }
        private async Task InitTrackingTasksAsync()
        {
            var result = await Api.GetTrackingTasksAsync(PersonId);

            if (result is null) return;

            TrackingTasks = result;

            IsAllTrackingTasksArchived = CheckIfAllTasksArchived(TrackingTasks);
            IsTrackingTasksArchivedExists = CheckIfAnyTasksArchived(TrackingTasks);
        }
        #endregion
    }
}
