using System.ComponentModel;

namespace WOClient.Models
{
    public class Comment: INotifyPropertyChanged
    {
        #region Fields
        private string _taskName;
        private string _employeeName;
        private string _employeeComment;
        private string _reply;
        #endregion

        #region Properties
        public string TaskName
        {
            get => _taskName;
            set
            {
                if (_taskName == value) return;

                _taskName = value;
                NotifyPropertyChanged("TaskName");
            }
        }
        public string EmployeeName
        {
            get => _employeeName;
            set
            {
                if (_employeeName == value) return;

                _employeeName = value;
                NotifyPropertyChanged("EmployeeName");
            }
        }
        public string EmployeeComment
        {
            get => _employeeComment;
            set
            {
                if (_employeeComment == value) return;

                _employeeComment = value;
                NotifyPropertyChanged("EmployeeComment");
            }
        }
        public string Reply
        {
            get => _reply;
            set
            {
                if (_reply == value) return;

                _reply = value;
                NotifyPropertyChanged("Reply");
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
