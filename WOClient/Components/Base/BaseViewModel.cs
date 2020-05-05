using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace WOClient.Components.Base
{
    public abstract class BaseViewModel : IBaseViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected void NotifyPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
