using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace WOClient.Models
{
    public class Manager: Person
    {
        public Manager(int personId, int managerId, string firstName, string lastName, string email) : base(personId, managerId, firstName, lastName, email)
        {
            Task.Run(Init);
        }

        #region Fields
        private ObservableCollection<IPerson> _myEmployees;
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
        public ObservableCollection<MyTask> TrackingTasks { get; }
        #endregion

        #region Private Methods
        private async void Init()
        {
            await InitMyEmployees();
            await InitTrackingTasks();
        }
        private async Task InitMyEmployees()
        {
            var result = await Api.GetEmployeesAsync(PersonId);

            if (result is null) return;

            MyEmployees = new ObservableCollection<IPerson>();

            foreach (var user in result)
            {
                switch (user.Permission)
                {
                    case "Manager":
                        MyEmployees.Add(new Manager(user.Id,
                                                    user.DirectManager,
                                                    user.FirstName,
                                                    user.LastName,
                                                    user.Email));
                        break;
                    case "Employee":
                        MyEmployees.Add(new Employee(user.Id,
                                                     user.DirectManager,
                                                     user.FirstName,
                                                     user.LastName,
                                                     user.Email));
                        break;
                }
            }
        }
        private async Task InitTrackingTasks()
        {

        }
        #endregion
    }
}
