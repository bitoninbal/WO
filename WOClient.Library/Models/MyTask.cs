﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using WOClient.Library.Api;
using WOCommon.Enums;

namespace WOClient.Library.Models
{
    public class MyTask: INotifyPropertyChanged, ICloneable
    {
        public MyTask(bool isInitMode = false)
        {
            Api             = new ClientApi();
            _comments       = new ObservableCollection<Comment>();
            _commentMessage = string.Empty;
            _isInitMode     = isInitMode;
        }

        #region Fields
        private bool _isArchive;
        private bool _isCommentDialogOpen;
        private bool _isCompleted;
        private bool _isInitMode;
        private int _taskId;
        private int _assignedEmployee;
        private string _commentMessage;
        private string _description;
        private string _subject;
        private DateTime _createdDate;
        private DateTime _finalDate;
        private ObservableCollection<Comment> _comments;
        private PriorityEnum _priority;
        #endregion

        #region Properties
        protected ClientApi Api { get; }

        public bool IsArchive
        {
            get => _isArchive;
            set
            {
                if (_isArchive == value) return;

                _isArchive = value;

                NotifyPropertyChanged(nameof(IsArchive));

                if (TaskId != 0 && !_isInitMode) Task.Run(() => UpdateFieldDbAsync(this, value, nameof(IsArchive)));
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

                if (TaskId != 0 && !_isInitMode) Task.Run(() => UpdateFieldDbAsync(this, value, nameof(IsCompleted)));
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

                if (TaskId != 0 && !_isInitMode) Task.Run(() => UpdateFieldDbAsync(this, value, "UserId"));
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

                if (TaskId != 0 && !_isInitMode) Task.Run(() => UpdateFieldDbAsync(this, value, "Description"));
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

                if (TaskId != 0 && !_isInitMode) Task.Run(() => UpdateFieldDbAsync(this, value, "Subject"));
            }
        }
        public DateTime CreatedDate
        {
            get => _createdDate;
            set
            {
                if (_createdDate == value) return;

                _createdDate = value;

                NotifyPropertyChanged(nameof(CreatedDate));
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

                if (TaskId != 0 && !_isInitMode) Task.Run(() => UpdateFieldDbAsync(this, value, "FinalDate"));
            }
        }
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
        public PriorityEnum Priority
        {
            get => _priority;
            set
            {
                if (_priority == value) return;

                _priority = value;

                NotifyPropertyChanged(nameof(Priority));

                if (TaskId != 0 && !_isInitMode) Task.Run(() => UpdateFieldDbAsync(this, value, "Priority"));
            }
        }
        #endregion

        #region Public Methods
        public object Clone()
        {
            var task = new MyTask(true)
            {
                AssignedEmployee    = this.AssignedEmployee,
                CommentMessage      = this.CommentMessage,
                CreatedDate         = this.CreatedDate,
                Description         = this.Description,
                FinalDate           = this.FinalDate,
                IsArchive           = this.IsArchive,
                IsCommentDialogOpen = this.IsCommentDialogOpen,
                IsCompleted         = this.IsCompleted,
                Priority            = this.Priority,
                Subject             = this.Subject,
                TaskId              = this.TaskId
            };

            task.SetInitModeFalse();

            foreach (var comment in Comments) task.Comments.Add(comment);

            return task;
        }
        public void RemoveAllCommentsByEmployeeId(int employeeId)
        {
            foreach (var comment in Comments.ToList())
            {
                if (comment.SenderId != employeeId) continue;

                Comments.Remove(comment);
            }
        }
        public void SetInitModeFalse()
        {
            _isInitMode = false;
        }
        public async Task<Comment> TryAddCommentAsync(string message, int taskId, int userToBeUpdated)
        {
            var commentId = await Api.AddCommentAsync(taskId, LoggedInUser.Instance.Id, userToBeUpdated, message);

            if (commentId == 0) return null;

            var comment = new Comment
            {
                CommentId       = commentId,
                Message         = message,
                SenderId        = LoggedInUser.Instance.Id,
                SenderFirstName = LoggedInUser.Instance.FirstName,
                SenderLastName  = LoggedInUser.Instance.LastName
            };

            Comments.Add(comment);

            CommentMessage      = string.Empty;
            IsCommentDialogOpen = false;

            return comment;
        }
        public async Task UpdateTaskCreaterIdAsync(int id)
        {
            await UpdateFieldDbAsync(this, id, "ManagerId");
        }
        #endregion

        #region Private Methods
        private async Task UpdateFieldDbAsync<T>(MyTask task, T value, string columnName)
        {
            var userToBeUpdated = task.AssignedEmployee;

            if (LoggedInUser.Instance.Id == task.AssignedEmployee)
            {
                userToBeUpdated = LoggedInUser.Instance.DirectManager;
            }

            await Api.UpdateTaskDbFiledAsync(task.TaskId, value, columnName);
            await Api.SendUpdateEventAsync(userToBeUpdated);
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
