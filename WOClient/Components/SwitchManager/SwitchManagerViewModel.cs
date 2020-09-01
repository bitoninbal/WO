using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using WOClient.Components.Base;
using WOClient.Components.Main;
using WOClient.Library.Models;
using WOClient.Resources.Commands;

namespace WOClient.Components.SwitchManager
{
    public class SwitchManagerViewModel: BaseViewModel, IBaseViewModel
    {
        public SwitchManagerViewModel(List<IPerson> employees, Manager deletedManager)
        {
            SwitchManagerValueCommand = new RelayCommand(SwitchManagerValue);

            _deletedManager = deletedManager;
            Employees       = employees;
        }

        #region Fields
        private bool _isAssignedToMe;
        private Manager _deletedManager;
        private IPerson _selectedManager;
        #endregion

        #region ICommands
        public ICommand SwitchManagerValueCommand { get; }
        #endregion

        #region Properties
        public List<IPerson> Employees { get; set; }

        public bool IsAssignedToMe
        {
            get => _isAssignedToMe;
            set
            {
                if (_isAssignedToMe == value) return;

                _isAssignedToMe = value;

                NotifyPropertyChanged(nameof(IsAssignedToMe));
            }
        }
        public IPerson SelectedManager
        {
            get => _selectedManager;
            set
            {
                if (_selectedManager == value) return;

                _selectedManager = value;

                NotifyPropertyChanged(nameof(SelectedManager));
            }
        }
        #endregion

        #region Private Methods
        private void SwitchManagerValue()
        {
            if (SelectedManager is null && !IsAssignedToMe)
            {
                MessageBox.Show("You must choose a manager.");

                return;
            }

            if (IsAssignedToMe)
            {
                var manager = IMainWindowViewModel.User as Manager;

                foreach (var item in _deletedManager.MyEmployees) manager.AssignedEmployee(item);

                DialogHost.CloseDialogCommand.Execute(null, null);

                return;
            }

            var selectedManager = SelectedManager as Manager;

            foreach (var item in _deletedManager.MyEmployees) selectedManager.AssignedEmployee(item);

            DialogHost.CloseDialogCommand.Execute(null, null);
        }
        #endregion
    }
}
