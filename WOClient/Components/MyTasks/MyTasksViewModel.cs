using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using WOClient.Components.Base;
using WOClient.Library.Models;
using WOClient.Resources.Commands;

namespace WOClient.Components.MyTasks
{
    public class MyTasksViewModel: BaseViewModel, IMyTasksViewModel
    {
        public MyTasksViewModel()
        {
            AddCommentCommand = new RelayCommand(AddComment);
            Tasks             = new ObservableCollection<MyTask>();
        }

        #region ICommand
        public ICommand AddCommentCommand { get; }
        #endregion

        #region Properties
        public ObservableCollection<MyTask> Tasks { get; set; }
        #endregion

        #region Private Methods
        private void AddComment()
        {
            MessageBox.Show("Hello");
        }
        #endregion
    }
}
