using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WOClient.Components.Base;
using WOClient.Components.Main;
using WOClient.Library.Api;
using WOClient.Library.Models;
using WOClient.Resources.Commands;

namespace WOClient.Components.MyTaskComponent
{
    public class MyTaskViewModel: BaseViewModel, IBaseViewModel
    {
        public MyTaskViewModel()
        {
            CloseCommentDialogCommand = new RelayCommand<MyTask>(CloseCommentDialog);
            CommentDialogCommand      = new RelayCommand<MyTask>(CommentDialog);
            MoveFromArchiveCommand    = new RelayCommand<MyTask>(MoveFromArchive);
            MoveToArchiveCommand      = new RelayCommand<MyTask>(MoveToArchive);
            SendCommentCommand        = new RelayCommand<MyTask>(SendCommentAsync);
            _api                      = new ClientApi();
        }

        #region Fields
        private readonly IClientApi _api;
        #endregion

        #region ICommand
        public ICommand CloseCommentDialogCommand { get; }
        public ICommand CommentDialogCommand { get; }
        public ICommand MoveFromArchiveCommand { get; }
        public ICommand MoveToArchiveCommand { get; }
        public ICommand SendCommentCommand { get; }
        #endregion

        #region Private Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tasks"></param>
        /// <param name="taskId"></param>
        /// <param name="comment"></param>
        private void AddComment(ObservableCollection<MyTask> tasks, int taskId, Comment comment)
        {
            foreach (var item in tasks)
            {
                if (item.TaskId != taskId) continue;

                item.Comments.Add(comment);

                break;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        /// <param name="comment"></param>
        private void AddCommentToCollections(MyTask task, Comment comment)
        {
            if (IMainWindowViewModel.User is Employee)
            {
                AddComment(IMainWindowViewModel.User.MyTasks, task.TaskId, comment);
            }
            else 
            {
                var user = IMainWindowViewModel.User as Manager;

                if (task.AssignedEmployee == user.PersonId)
                {
                    AddComment(user.MyTasks, task.TaskId, comment);
                }
                else
                {
                    var myEmployee = user.GetEmplyee(task.AssignedEmployee);

                    if (myEmployee is null) return;

                    AddComment(user.TrackingTasks, task.TaskId, comment);
                    AddComment(myEmployee.MyTasks, task.TaskId, comment);
                }
            }
        }
        private void CloseCommentDialog(MyTask task)
        {
            task.IsCommentDialogOpen = false;
        }
        private void CommentDialog(MyTask task)
        {
            if (task.IsCommentDialogOpen)
                task.IsCommentDialogOpen = false;
            else
                task.IsCommentDialogOpen = true;
        }
        private void MoveFromArchive(MyTask task)
        {
            task.IsArchive = false;

            if (IMainWindowViewModel.User.MyTasks.Contains(task))
            {
                IMainWindowViewModel.User.IsAllMyTasksArchived = false;

                IMainWindowViewModel.User.CheckIfAnyMyTasksArchived();
            }
            else
            {
                var user = IMainWindowViewModel.User as Manager;

                user.IsAllTrackingTasksArchived = false;

                user.CheckIfAnyTrackingTasksArchived();
            }  
        }
        private void MoveToArchive(MyTask task)
        {
            task.IsArchive = true;

            if (IMainWindowViewModel.User.MyTasks.Contains(task))
            {
                IMainWindowViewModel.User.IsAllMyTasksArchived = true;

                IMainWindowViewModel.User.CheckIfAnyMyTasksArchived();
            }
            else
            {
                var user = IMainWindowViewModel.User as Manager;

                user.CheckIfAllTrackingTasksArchived();
                user.CheckIfAnyTrackingTasksArchived();
            }
        }
        private async void SendCommentAsync(MyTask task)
        {
            try
            {
                var userToBeUpdated = task.AssignedEmployee;

                if (IMainWindowViewModel.User.PersonId == task.AssignedEmployee)
                {
                    userToBeUpdated = IMainWindowViewModel.User.ManagerId;
                }

                var commentId = await _api.AddCommentAsync(task.TaskId, IMainWindowViewModel.User.PersonId, userToBeUpdated, task.CommentMessage);

                if (commentId == 0) return;

                var comment = new Comment
                {
                    CommentId       = commentId,
                    Message         = task.CommentMessage,
                    SenderId        = IMainWindowViewModel.User.PersonId,
                    SenderFirstName = IMainWindowViewModel.User.FirstName,
                    SenderLastName  = IMainWindowViewModel.User.LastName
                };

                task.CommentMessage      = string.Empty;
                task.IsCommentDialogOpen = false;

                AddCommentToCollections(task, comment);
            }
            catch (Exception)
            {
                IMainWindowViewModel.MessageQueue.Enqueue("Could not connect to server.", "OK", (obj) => { }, new object(), false, true, TimeSpan.FromSeconds(6));
            }
        }
        #endregion
    }
}
