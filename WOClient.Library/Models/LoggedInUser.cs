using System.ComponentModel;
using WOCommon.Enums;

namespace WOClient.Library.Models
{
    public class LoggedInUser: INotifyPropertyChanged
    {
        #region Fields
        private int _id;
        private int _directManager;
        private string _email;
        private string _firstName;
        private string _lastName;
        private PermissionsEnum _permission;
        #endregion

        #region Properties
        public static LoggedInUser Instance { get; } = new LoggedInUser();

        public int Id
        {
            get => _id;
            set
            {
                if (_id == value) return;

                _id = value;
                NotifyPropertyChanged(nameof(Id));
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
            }
        }
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (_firstName == value) return;

                _firstName = value;
                NotifyPropertyChanged(nameof(FirstName));
            }
        }
        public string LastName
        {
            get => _lastName;
            set
            {
                if (_lastName == value) return;

                _lastName = value;
                NotifyPropertyChanged(nameof(LastName));
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
            }
        }
        public int DirectManager
        {
            get => _directManager;
            set
            {
                if (_directManager == value) return;

                _directManager = value;
                NotifyPropertyChanged(nameof(DirectManager));
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
