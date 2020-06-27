using System.ComponentModel;

namespace WOClient.Library.Models
{
    public class UserInfo: INotifyPropertyChanged
    {
        #region Fields
        private int _id;
        private string _email;
        private string _firstName;
        private string _lastName;
        private string _permission;
        private int _directManager;
        #endregion

        #region Properties
        public static UserInfo Instance { get; } = new UserInfo();

        public int Id
        {
            get => _id;
            set
            {
                if (_id == value) return;

                _id = value;
                NotifyPropertyChanged("Id");
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
        public string Permission
        {
            get => _permission;
            set
            {
                if (_permission == value) return;

                _permission = value;
                NotifyPropertyChanged("Permission");
            }
        }
        public int DirectManager
        {
            get => _directManager;
            set
            {
                if (_directManager == value) return;

                _directManager = value;
                NotifyPropertyChanged("DirectManager");
            }
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
