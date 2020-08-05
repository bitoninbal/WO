using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using WOClient.Library.Api;
using WOCommon.Enums;

namespace WOClient.Library.Models
{
    public class MyTask: INotifyPropertyChanged
    {
        public MyTask()
        {
            Api      = new ClientApi();
            Comments = new ObservableCollection<Comment>();
        }

        #region Fields
        private bool         _isCompleted;
        private string       _description;
        private Color        _bgColor;
        private DateTime     _finalDate;
        private string       _subject;
        private PriorityEnum _priority;
        private int          _taskId;
        private int          _userId;
        #endregion

        #region Properties
        public int UserId
        {
            get => _userId;
            set
            {
                if (_userId == value) return;

                _userId = value;
                NotifyPropertyChanged("UserId");
            }
        }
        protected ClientApi Api { get; }

        public bool IsCompleted
        {
            get => _isCompleted;
            set
            {
                if (_isCompleted == value) return;

                _isCompleted = value;
                NotifyPropertyChanged("IsCompleted");

                if (TaskId != 0) Task.Run(() => UpdateCompletedTaskFieldAsync(TaskId, value));
            }
        }
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
        public int TaskId
        {
            get => _taskId;
            set
            {
                if (_taskId == value) return;

                _taskId = value;
                NotifyPropertyChanged("TaskId");
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
        public string Subject
        {
            get => _subject;
            set
            {
                if (_subject == value) return;

                _subject = value;
                NotifyPropertyChanged("Subject");
            }
        }
        public ObservableCollection<Comment> Comments { get; }
        #endregion

        #region Private Methods
        private async Task UpdateCompletedTaskFieldAsync(int taskId, bool value)
        {
            await Api.UpdateCompletedTaskFieldAsync(taskId, value);
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
