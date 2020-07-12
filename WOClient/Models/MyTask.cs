using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;
using WOCommon.Enums;

namespace WOClient.Models
{
    public class MyTask: INotifyPropertyChanged
    {
        public MyTask()
        {
            Comments = new ObservableCollection<Comment>();
        }

        #region Fields
        private string       _description;
        private Color        _bgColor;
        private DateTime     _finalDate;
        private string       _taskName;
        private PriorityEnum _priority;
        #endregion

        #region Properties
        public Color BgColor
        {
            get => _bgColor;
            set
            {
                if (_bgColor == value) return;

                _bgColor = value;
                NotifyPropertyChanged("BgColor");
            }
        }
        public string Description
        {
            get => _description;
            set
            {
                if (_description == value) return;

                _description = value;
                NotifyPropertyChanged("Description");
            }
        }
        public DateTime FinalDate
        {
            get => _finalDate;
            set
            {
                if (_finalDate == value) return;

                _finalDate = value;
                NotifyPropertyChanged("FinalDate");
            }
        }
        public PriorityEnum Priority
        {
            get => _priority;
            set
            {
                if (_priority == value) return;

                _priority = value;
                NotifyPropertyChanged("Priority");
            }
        }
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
        public ObservableCollection<Comment> Comments { get; }
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
