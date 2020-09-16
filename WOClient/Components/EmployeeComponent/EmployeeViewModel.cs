using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using System.Windows;
using WOClient.Components.Base;
using WOClient.Components.Main;
using WOClient.Components.SwitchManager;
using WOClient.Enums;
using WOClient.Library.Models;
using WOCommon.Enums;

namespace WOClient.Components.EmployeeComponent
{
    public class EmployeeViewModel: BaseViewModel, IEmployeeViewModel
    {
        public EmployeeViewModel()
        {
            Permission = PermissionsEnum.Employee;
        }

        #region Fields
        private string _email;
        private string _firstName;
        private string _lastName;
        private PermissionsEnum _permission;
        #endregion

        #region Properties
        public string Email
        {
            get => _email;
            set
            {
                if (_email == value) return;

                _email = value;
                NotifyPropertyChanged("Email");
            }
        }
        public SecureString Password { get; set; }
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (_firstName == value) return;

                _firstName = value;
                NotifyPropertyChanged("FirstName");
            }
        }
        public string LastName
        {
            get => _lastName;
            set
            {
                if (_lastName == value) return;

                _lastName = value;
                NotifyPropertyChanged("LastName");
            }
        }
        public PermissionsEnum Permission
        {
            get => _permission;
            set
            {
                if (_permission == value) return;

                _permission = value;
                NotifyPropertyChanged("Permission");
            }
        }
        #endregion

        #region Public Methods
        public async Task EditEmployeeAsync(int id)
        {
            var loggedInManager = IMainWindowViewModel.User as Manager;
            var selectedUser = loggedInManager.MyEmployees.Single(user => user.PersonId == id);

            if (!Email.Equals(selectedUser.Email)) selectedUser.Email = Email;
            if (!FirstName.Equals(selectedUser.FirstName)) selectedUser.FirstName = FirstName;
            if (!LastName.Equals(selectedUser.LastName)) selectedUser.LastName = LastName;
            if (Permission != selectedUser.Permission)
            {
                if (Permission == PermissionsEnum.Employee)
                    await HandleSwitchingManagerAsync(selectedUser);
                else
                    selectedUser.Permission = Permission;
            }

            SetPropertiesToDefault();
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
        public async Task RegisterAsync()
        {
            try
            {
                var loggedInManager = IMainWindowViewModel.User as Manager;
                var result = await loggedInManager.TryAddEmployeeAsync(FirstName,
                                                                                LastName,
                                                                                Email,
                                                                                Password.Copy(),
                                                                                Permission);

                if (!result)
                {
                    MessageBox.Show("User already exist.");

                    return;
                }

                SetPropertiesToDefault();
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
            catch (Exception)
            {
                IMainWindowViewModel.MessageQueue.Enqueue("Could not connect to server.", "OK", (obj) => { }, new object(), false, true, TimeSpan.FromSeconds(6));
            }
        }
        public void SetProperties(IPerson employee)
        {
            Email = employee.Email;
            FirstName = employee.FirstName;
            LastName = employee.LastName;
            Permission = employee.Permission;
        }
        public void Reset()
        {
            Email = null;
            Password = null;
            FirstName = null;
            LastName = null;
            Permission = PermissionsEnum.Employee;
        }
        #endregion

        #region Private Methods
        private async Task HandleSwitchingManagerAsync(IPerson selectedUser)
        {
            var managerToDowngrade = selectedUser as Manager;

            if (managerToDowngrade.MyEmployees.Count == 0)
            {
                managerToDowngrade.Downgrade();

                return;
            }

            var loggedInManager = IMainWindowViewModel.User as Manager;
            var potentialManagers = loggedInManager.MyEmployees.Where((employee) => employee.Permission == PermissionsEnum.Manager).ToList();

            potentialManagers.Remove(selectedUser);

            if (potentialManagers.Count == 0)
            {
                foreach (var employee in managerToDowngrade.MyEmployees) await loggedInManager.AssignedEmployee(employee);

                managerToDowngrade.Downgrade();

                return;
            }

            DialogHost.CloseDialogCommand.Execute(null, null);

            await OpenSwitchManagerAsync(potentialManagers, managerToDowngrade, SwitchingManagerMode.Edit);
        }
        private async Task OpenSwitchManagerAsync(List<IPerson> potentialManagers, Manager deletedManager, SwitchingManagerMode mode)
        {
            var view = new SwitchManagerView
            {
                DataContext = new SwitchManagerViewModel(potentialManagers, deletedManager, mode)
            };

            await DialogHost.Show(view, "RootDialog");
        }
        private void SetPropertiesToDefault()
        {
            Email = default;
            FirstName = default;
            LastName = default;
        }
        #endregion
    }
}
