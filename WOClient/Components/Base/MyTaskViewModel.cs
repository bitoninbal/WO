using System;
using System.Threading.Tasks;
using System.Windows.Input;
using WOClient.Components.Main;
using WOClient.Library.Api;
using WOClient.Library.Models;
using WOClient.Resources.Commands;

namespace WOClient.Components.Base
{
    public class MyTaskViewModel: BaseViewModel ,IBaseViewModel
    {
        public MyTaskViewModel(IClientApi api)
        {
            OpenCommentDialogCommand = new RelayCommand(OpenCommentDialog);
            SendCommentCommand       = new RelayCommand<int>(SendComment);
            _api                     = api;
        }

        #region Fields
        private IClientApi _api;
        private bool _isCommentDialogOpen;
        private string _comment;
        #endregion

        #region ICommand
        public ICommand OpenCommentDialogCommand { get; }
        public ICommand SendCommentCommand { get; }
        #endregion

        #region Properties
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
        public string Comment
        {
            get => _comment;
            set
            {
                if (_comment == value) return;

                _comment = value;
                NotifyPropertyChanged(nameof(Comment));
            }
        }
        #endregion

        #region Private Methods
        private void OpenCommentDialog()
        {
            if (IsCommentDialogOpen)
                IsCommentDialogOpen = false;
            else
                IsCommentDialogOpen = true;

            Comment = default;
        }
        public async void SendComment(int taskId)
        {
            try
            {
                var commentId = await _api.AddCommentAsync(taskId, IMainWindowViewModel.User.PersonId, Comment);

                if (commentId == 0) return;
                var comment = new Comment
                {
                    CommentId = commentId,
                    Message   = Comment,
                    Sender    = IMainWindowViewModel.User.Permission,
                };
                IsCommentDialogOpen = false;
                AddCommentToCollections(taskId, comment);
                Comment = default;
            }
            catch (Exception)
            {
                IMainWindowViewModel.MessageQueue.Enqueue("Could not connect to server.", "OK", (obj) => { }, new object(), false, true, TimeSpan.FromSeconds(6));
            }
        }
        private void AddCommentToCollections(int taskId, Comment comment)
        {
            if(IMainWindowViewModel.User is Employee)
            {
                foreach (var task in IMainWindowViewModel.User.MyTasks)
                {
                    if (task.TaskId == taskId)
                    {
                        task.Comments.Add(comment);
                        break;
                    }
                }
            }
            else //manager 
            {
                var user = IMainWindowViewModel.User as Manager;

            }
        }
        #endregion
    }
}
