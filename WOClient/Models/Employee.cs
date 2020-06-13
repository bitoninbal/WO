using System.ComponentModel;
using WOClient.Enums;

namespace WOClient.Models
{
    public class Employee: INotifyPropertyChanged
    {
        #region Fields
        private string _firstName;
        private string _lastName;
        private string _email;
        private PremissionsEnum _premission;
        private string _phone;
        #endregion

        #region Properties
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
        public PremissionsEnum Premission
        {
            get => _premission;
            set
            {
                if (_premission == value) return;

                _premission = value;
                NotifyPropertyChanged("Premission");
            }
        }
        public string Phone
        {
            get => _phone;
            set
            {
                if (_phone == value) return;

                _phone = value;
                NotifyPropertyChanged("Phone");
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
