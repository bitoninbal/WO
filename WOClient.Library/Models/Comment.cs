using System.ComponentModel;

namespace WOClient.Library.Models
{
    public class Comment: INotifyPropertyChanged
    {
        #region Fields
        private int    _commentId;
        private int    _senderId;
        private string _message;
        private string _senderFirstName;
        private string _senderLastName;
        #endregion

        #region Properties
        public int CommentId
        {
            get => _commentId;
            set
            {
                if (_commentId == value) return;

                _commentId = value;
                NotifyPropertyChanged(nameof(CommentId));
            }
        }
        public int SenderId
        {
            get => _senderId;
            set
            {
                if (_senderId == value) return;

                _senderId = value;
                NotifyPropertyChanged(nameof(SenderId));
            }
        }
        public string Message
        {
            get => _message;
            set
            {
                if (_message == value) return;

                _message = value;
                NotifyPropertyChanged(nameof(Message));
            }
        }
        public string SenderFirstName
        {
            get => _senderFirstName;
            set
            {
                if (_senderFirstName == value) return;

                _senderFirstName = value;
                NotifyPropertyChanged(nameof(SenderFirstName));
            }
        }
        public string SenderLastName
        {
            get => _senderLastName;
            set
            {
                if (_senderLastName == value) return;

                _senderLastName = value;
                NotifyPropertyChanged(nameof(SenderLastName));
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
