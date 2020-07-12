﻿using System.ComponentModel;
using WOCommon.Enums;

namespace WOClient.Models
{
    public class Comment: INotifyPropertyChanged
    {
        #region Fields
        private PermissionsEnum _sender;
        private string _message;
        #endregion

        #region Properties
        public PermissionsEnum Sender
        {
            get => _sender;
            set
            {
                if (_sender == value) return;

                _sender = value;
                NotifyPropertyChanged(nameof(Sender));
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
