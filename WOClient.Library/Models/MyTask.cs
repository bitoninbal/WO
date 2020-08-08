﻿using System;
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
            Api             = new ClientApi();
            _comments       = new ObservableCollection<Comment>();
            _commentMessage = string.Empty;
        }

        #region Fields
        private bool _isCommentDialogOpen;
        private bool _isCompleted;
        private int _taskId;
        private int _assignedEmployee;
        private string _commentMessage;
        private string _description;
        private string _subject;
        private Color _bgColor;
        private DateTime _finalDate;
        private ObservableCollection<Comment> _comments;
        private PriorityEnum _priority;
        #endregion

        #region Properties
        protected ClientApi Api { get; }

        public ObservableCollection<Comment> Comments
        {
            get => _comments;
            set
            {
                if (_comments == value) return;

                _comments = value;
                NotifyPropertyChanged(nameof(Comments));
            }
        }
        public bool IsCommentDialogOpen
        {
            get => _isCommentDialogOpen;
            set
            {
                if (_isCommentDialogOpen == value) return;

                _isCommentDialogOpen = value;
                NotifyPropertyChanged(nameof(IsCommentDialogOpen));
            }
        }
        public bool IsCompleted
        {
            get => _isCompleted;
            set
            {
                if (_isCompleted == value) return;

                _isCompleted = value;
                NotifyPropertyChanged(nameof(IsCompleted));

                if (TaskId != 0) Task.Run(() => UpdateCompletedTaskFieldAsync(TaskId, value));
            }
        }
        public int TaskId
        {
            get => _taskId;
            set
            {
                if (_taskId == value) return;

                _taskId = value;
                NotifyPropertyChanged(nameof(TaskId));
            }
        }
        public int AssignedEmployee
        {
            get => _assignedEmployee;
            set
            {
                if (_assignedEmployee == value) return;

                _assignedEmployee = value;
                NotifyPropertyChanged(nameof(AssignedEmployee));
            }
        }
        public string CommentMessage
        {
            get => _commentMessage;
            set
            {
                if (_commentMessage == value) return;

                _commentMessage = value;
                NotifyPropertyChanged(nameof(CommentMessage));
            }
        }
        public string Description
        {
            get => _description;
            set
            {
                if (_description == value) return;

                _description = value;
                NotifyPropertyChanged(nameof(Description));
            }
        }
        public string Subject
        {
            get => _subject;
            set
            {
                if (_subject == value) return;

                _subject = value;
                NotifyPropertyChanged(nameof(Subject));
            }
        }
        public DateTime FinalDate
        {
            get => _finalDate;
            set
            {
                if (_finalDate == value) return;

                _finalDate = value;
                NotifyPropertyChanged(nameof(FinalDate));
            }
        }
        public Color BgColor
        {
            get => _bgColor;
            set
            {
                if (_bgColor == value) return;

                _bgColor = value;
                NotifyPropertyChanged(nameof(BgColor));
            }
        }
        public PriorityEnum Priority
        {
            get => _priority;
            set
            {
                if (_priority == value) return;

                _priority = value;
                NotifyPropertyChanged(nameof(Priority));
            }
        }
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
