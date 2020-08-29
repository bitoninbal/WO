using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WOClient.Library.Models.Reports
{
    public abstract class Report: INotifyPropertyChanged
    {
        #region Public Methods
        public abstract List<object> GenerateReport(List<MyTask> tasks, DateTime fromDate, DateTime toDate);
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
