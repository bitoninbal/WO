using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WOCommon.Enums;

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
            MyEmployees   = new ObservableCollection<IPerson>();
            TrackingTasks = new ObservableCollection<MyTask>();

            Task.Run(InitAsync);
        }

        #region Fields
        private ObservableCollection<IPerson> _myEmployees;
        private ObservableCollection<MyTask> _trackingTasks;
        #endregion

        #region Properties
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
        }
        #endregion
    }
}
