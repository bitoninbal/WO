using System;
using System.ComponentModel;
using System.Windows.Media;
using WOClient.Enums;

namespace WOClient.Models
{
    public class MyTask: INotifyPropertyChanged
    {
        #region Fields
        private string       _taskName;
        private DateTime     _finalDate;
        private PriorityEnum _priority;
        private string       _description;
        private Color        _bgColor;
        #endregion

        #region Properties
        public string TaskName
        {
            get => _taskName;
            set
            {
                if (value == _taskName) return;

                _taskName = value;
                NotifyPropertyChanged("TaskName");
            }
        }
        public DateTime FinalDate
        {
            get => _finalDate;
            set
            {
                if (value == _finalDate) return;

                _finalDate = value;
                NotifyPropertyChanged("FinalDate");
            }
        }
        public PriorityEnum Priority
        {
            get => _priority;
            set
            {
                if (value == _priority) return;

                _priority = value;
                NotifyPropertyChanged("Priority");
            }
        }
        public string Description
        {
            get => _description;
            set
            {
                if (value == _description) return;

                _description = value;
                NotifyPropertyChanged("Description");
            }
        }
        public Color BgColor
        {
            get => _bgColor;
            set
            {
                if (value == _bgColor) return;

                _bgColor = value;
                NotifyPropertyChanged("BgColor");
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
