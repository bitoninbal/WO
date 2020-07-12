using System.Collections.ObjectModel;
using System.ComponentModel;
using WOClient.Library.Api;

namespace WOClient.Models
{
    public abstract class Person: IPerson
    {
        protected Person(int personId, int managerId, string firstName, string lastName, string email)
        {
            Api = new ClientApi();

            _personId  = personId;
            _managerId = managerId;
            _firstName = firstName;
            _lastName  = lastName;
            _email     = email;

            InitMyTasks();
        }

        #region Fields
        private int _personId;
        private int _managerId;
        private string _firstName;
        private string _lastName;
        private string _email;
        #endregion

        #region Properties
        public static IPerson Instance { get; set; }

        public ObservableCollection<MyTask> MyTasks { get; }
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (_firstName == value) return;

                _firstName = value;
                NotifyPropertyChanged("FirstName");
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
