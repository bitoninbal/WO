using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using WOClient.Components.Base;
using WOClient.Components.Main;
using WOClient.Enums;
using WOClient.Library.Models;
using WOClient.Resources.Commands;

namespace WOClient.Components.SwitchManager
{
    public class SwitchManagerViewModel: BaseViewModel, IBaseViewModel
    {
        public SwitchManagerViewModel(List<IPerson> employees, Manager oldManager, SwitchingManagerMode mode)
        {
            SwitchManagerValueCommand = new RelayCommand(SwitchManagerValue);

            Employees   = employees;
            _mode       = mode;
            _oldManager = oldManager;
        }

        #region Fields
        private bool _isAssignedToMe;
        private Manager _oldManager;
        private IPerson _selectedManager;
        private SwitchingManagerMode _mode;
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
        private async void SwitchManagerValue()
        {
            if (SelectedManager is null && !IsAssignedToMe)
            {
                MessageBox.Show("You must choose a manager.");

                return;
            }

            var loggedInManager = IMainWindowViewModel.User as Manager;
            var selectedManager = IMainWindowViewModel.User as Manager;

            if (!IsAssignedToMe) selectedManager = SelectedManager as Manager;

            foreach (var employee in _oldManager.MyEmployees) await selectedManager.AssignedEmployeeAsync(employee);

            switch (_mode)
            {
                case SwitchingManagerMode.Delete:
                    await loggedInManager.RemoveEmployeeAsync(_oldManager.PersonId);

                    break;
                case SwitchingManagerMode.Edit:
                    loggedInManager.Downgrade(_oldManager);

                    break;
            }

            DialogHost.CloseDialogCommand.Execute(null, null);
        }
        #endregion
    }
}
