using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using WOClient.Library.Api;
using WOCommon.Enums;

namespace WOClient.Library.Models
{
    public abstract class Person: IPerson
    {
        protected Person(PermissionsEnum permission, int personId, int managerId, string firstName, string lastName, string email)
        {
            Api      = new ClientApi();
            _myTasks = new ObservableCollection<MyTask>();

            _permission = permission;
            _personId   = personId;
            _managerId  = managerId;
            _firstName  = firstName;
            _lastName   = lastName;
            _email      = email;
        }

        #region Fields
        private ObservableCollection<MyTask> _myTasks;
        private PermissionsEnum _permission;
        private bool _isAllTasksArchived;
        private bool _isMyTasksArchivedExists;
        private int _personId;
        private int _managerId;
        private string _firstName;
        private string _lastName;
        private string _email;
        #endregion

        #region Properties
        public ObservableCollection<MyTask> MyTasks
        {
            get => _myTasks;
            set
            {
                if (_myTasks == value) return;

                _myTasks = value;

                NotifyPropertyChanged(nameof(MyTasks));
            }
        }
        public PermissionsEnum Permission
        {
            get => _permission;
            set
            {
                if (_permission == value) return;

                _permission = value;

                NotifyPropertyChanged(nameof(Permission));
                Task.Run(() => UpdateFieldInDbAsync(PersonId, value, "Permission"));
            }
        }
        public bool IsMyTasksArchivedExists
        {
            get => _isMyTasksArchivedExists;
            set
            {
                if (_isMyTasksArchivedExists == value) return;

                _isMyTasksArchivedExists = value;

                NotifyPropertyChanged(nameof(IsMyTasksArchivedExists));
            }
        }
        public bool IsAllMyTasksArchived
        {
            get => _isAllTasksArchived;
            set
            {
                if (_isAllTasksArchived == value) return;

                _isAllTasksArchived = value;

                NotifyPropertyChanged(nameof(IsAllMyTasksArchived));
            }
        }
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (_firstName == value) return;

                _firstName = value;

                NotifyPropertyChanged("FirstName");
                Task.Run(() => UpdateFieldInDbAsync(PersonId, value, "FirstName"));
            }
        }
        public string LastName
        {
            get => _lastName;
            set
            {
                if (_lastName == value) return;

                _lastName = value;

                NotifyPropertyChanged("LastName");
                Task.Run(() => UpdateFieldInDbAsync(PersonId, value, "LastName"));
            }
        }
        public string Email
        {
            get => _email;
            set
            {
                if (_email == value) return;

                _email = value;

                NotifyPropertyChanged(nameof(Email));
                Task.Run(() => UpdateFieldInDbAsync(PersonId, value, nameof(Email)));
            }
        }
        public int PersonId
        {
            get => _personId;
            set
            {
                if (_personId == value) return;

                _personId = value;
                NotifyPropertyChanged("PersonId");
            }
        }
        public int ManagerId
        {
            get => _managerId;
            set
            {
                if (_managerId == value) return;

                _managerId = value;

                NotifyPropertyChanged(nameof(ManagerId));
                Task.Run(() => UpdateFieldInDbAsync(PersonId, value, "DirectManager"));
            }
        }

        protected ClientApi Api { get; }
        #endregion

        #region Private Methods
        private async Task UpdateFieldInDbAsync<T>(int personId, T value, string columnName)
        {
            await Api.UpdateUserFieldAsync(personId, value, columnName);
        }
        #endregion

        #region Protected Methods
        protected bool CheckIfAllTasksArchived(ObservableCollection<MyTask> tasks)
        {
            return tasks.All((task) => task.IsArchive);
        }
        protected bool CheckIfAnyTasksArchived(ObservableCollection<MyTask> tasks)
        {
            return tasks.Any((task) => task.IsArchive);
        }
        protected abstract Task InitAsync();
        protected async Task InitMyTasksAsync()
        {
            var result = await Api.GetMyTasksAsync(PersonId);

            if (result is null) return;

            MyTasks = result;

            IsMyTasksArchivedExists = CheckIfAnyTasksArchived(MyTasks);
            IsAllMyTasksArchived    = CheckIfAllTasksArchived(MyTasks);
        }
        #endregion

        #region Public Methods
        public void CheckIfAllMyTasksArchived()
        {
            IsAllMyTasksArchived = MyTasks.All((task) => task.IsArchive);
        }
        public void CheckIfAnyMyTasksArchived()
        {
            IsMyTasksArchivedExists = MyTasks.Any((task) => task.IsArchive);
        }
        public virtual void Reset()
        {
            MyTasks.Clear();
        }
        public async Task UpdateAsync()
        {
            var result = await Api.RequestUserUpdateAsync(PersonId);

            if (result) await InitAsync();
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
