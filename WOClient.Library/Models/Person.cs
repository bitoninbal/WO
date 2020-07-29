using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using WOClient.Library.Api;
using WOCommon.Enums;

namespace WOClient.Library.Models
{
    public abstract class Person: IPerson
    {
        protected Person(PermissionsEnum permission, int personId, int managerId, string firstName, string lastName, string email)
        {
            Api = new ClientApi();

            _permission = permission;
            _personId   = personId;
            _managerId  = managerId;
            _firstName  = firstName;
            _lastName   = lastName;
            _email      = email;

            InitMyTasks();
        }

        #region Fields
        private PermissionsEnum _permission;
        private int _personId;
        private int _managerId;
        private string _firstName;
        private string _lastName;
        private string _email;
        #endregion

        #region Properties
        public ObservableCollection<MyTask> MyTasks { get; }
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

                NotifyPropertyChanged("Email");
                Task.Run(() => UpdateFieldInDbAsync(PersonId, value, "Email"));
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
            }
        }

        protected ClientApi Api { get; }
        #endregion

        #region Methods
        private async Task UpdateFieldInDbAsync<T>(int personId, T value, string columnName)
        {
            await Api.UpdateFieldAsync(personId, value, columnName);
        }
        protected void InitMyTasks()
        {

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
