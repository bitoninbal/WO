using System.Windows;
using System.Windows.Input;
using WOClient.Resources.Commands;

namespace WOClient.Components.Base
{
    public class MyTaskViewModel: BaseViewModel ,IBaseViewModel
    {
        public MyTaskViewModel()
        {
            OpenCommentDialogCommand = new RelayCommand(OpenCommentDialog);
        }

        #region Fields
        private bool _isCommentDialogOpen;
        private string _comment;
        #endregion

        #region ICommand
        public ICommand OpenCommentDialogCommand { get; }
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
        #endregion
    }
}
