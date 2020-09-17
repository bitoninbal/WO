using MaterialDesignThemes.Wpf;
using System;
using System.Windows.Input;
using WOClient.Components.Base;
using WOClient.Components.Main;
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
            DeleteTaskCommand         = new RelayCommand<MyTask>(DeleteTaskAsync);
            MoveFromArchiveCommand    = new RelayCommand<MyTask>(MoveFromArchive);
            MoveToArchiveCommand      = new RelayCommand<MyTask>(MoveToArchive);
            OpenEditTaskDialogCommand = new RelayCommand<MyTask>(OpenEditTaskDialogAsync);
            SendCommentCommand        = new RelayCommand<MyTask>(SendCommentAsync);
        }

        #region ICommand
        public ICommand CloseCommentDialogCommand { get; }
        public ICommand CommentDialogCommand { get; }
        public ICommand DeleteTaskCommand { get; }
        public ICommand MoveFromArchiveCommand { get; }
        public ICommand MoveToArchiveCommand { get; }
        public ICommand OpenEditTaskDialogCommand { get; }
        public ICommand SendCommentCommand { get; }
        #endregion

        #region Private Methods
        private void CloseCommentDialog(MyTask task)
        {
            task.IsCommentDialogOpen = false;
        }
        private void CommentDialog(MyTask task)
        {
            task.IsCommentDialogOpen = !task.IsCommentDialogOpen;
        }
        private async void DeleteTaskAsync(MyTask task)
        {
            var loggedInManager = IMainWindowViewModel.User as Manager;

            await loggedInManager.RemoveTaskAsync(task);
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
                IMainWindowViewModel.User.CheckIfAllMyTasksArchived();
                IMainWindowViewModel.User.CheckIfAnyMyTasksArchived();
            }
            else
            {
                var user = IMainWindowViewModel.User as Manager;

                user.CheckIfAllTrackingTasksArchived();
                user.CheckIfAnyTrackingTasksArchived();
            }
        }
        private async void OpenEditTaskDialogAsync(MyTask task)
        {
            var view = new EditTaskView()
            {
                DataContext = new EditTaskViewModel(task)
            };

            await DialogHost.Show(view, "RootDialog");
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

                var comment = await task.TryAddCommentAsync(task.CommentMessage, task.TaskId, userToBeUpdated);

                if (comment is null) throw new Exception();

                /* Checks if the user is manager or not because if it is manager,
                   it must insert the comment also its employee. */
                if (IMainWindowViewModel.User is Employee) return;

                var loggedInManager = IMainWindowViewModel.User as Manager;

                loggedInManager.AddCommentToEmployeeTask(task.AssignedEmployee, task.TaskId, comment);
            }
            catch (Exception)
            {
                IMainWindowViewModel.MessageQueue.Enqueue("Could not connect to server.", "OK", (obj) => { }, new object(), false, true, TimeSpan.FromSeconds(6));
            }
        }
        #endregion
    }
}
