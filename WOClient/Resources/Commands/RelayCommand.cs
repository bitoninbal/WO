using System;
using System.Windows.Input;

namespace WOClient.Resources.Commands
{
    public class RelayCommand : ICommand
    {
        #region Fields
        readonly Action _execute;
        #endregion

        #region Events
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        #endregion

        #region Constructor
        public RelayCommand(Action execute)
        {
            if (execute == null) throw new ArgumentNullException("execute");

            _execute = execute;
        }
        #endregion

        #region ICommand Members
        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter) => _execute();
        #endregion
    }
}
