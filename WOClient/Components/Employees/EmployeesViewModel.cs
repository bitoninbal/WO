﻿using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WOClient.Components.Base;
using WOClient.Components.EmployeeComponent;
using WOClient.Components.Main;
using WOClient.Components.SwitchManager;
using WOClient.Enums;
using WOClient.Library.Models;
using WOCommon.Enums;

namespace WOClient.Components.Employees
{
    public class EmployeesViewModel: BaseViewModel, IEmplyeesViewModel
    {
        public EmployeesViewModel(IEmployeeViewModel employeeVm)
        {
            _employeeVm = employeeVm;
        }

        #region Fields
        private IEmployeeViewModel _employeeVm;
        private IPerson _employee;
        #endregion

        #region Properties
        public IPerson SelectedEmployee
        {
            get => _employee;
            set
            {
                if (_employee == value) return;

                _employee = value;

                NotifyPropertyChanged(nameof(SelectedEmployee));
            }
        }
        #endregion

        #region Private Methods
        private async Task DeleteEmployeeAsync(IPerson employee)
        {
            var loggedInManager = IMainWindowViewModel.User as Manager;
            var result          = await loggedInManager.TryRemoveEmployeeAsync(employee);

            if (result)
            {
                SelectedEmployee = null;

                return;
            }

            IMainWindowViewModel.MessageQueue.Enqueue("You can't delete this employees until his all tasks will be closed or re-assigned.",
                                                      "OK",
                                                      (obj) => { },
                                                      new object(),
                                                      false,
                                                      true,
                                                      TimeSpan.FromSeconds(6));
        }
        private async Task HandleDeleteManagerAsync()
        {
            var managerToDelete = SelectedEmployee as Manager;

            if (managerToDelete.MyEmployees.Count == 0)
            {
                await DeleteEmployeeAsync(managerToDelete);

                return;
            }

            var loggedInManager   = IMainWindowViewModel.User as Manager;
            var potentialManagers = loggedInManager.MyEmployees.Where((employee) => employee.Permission == PermissionsEnum.Manager).ToList();

            potentialManagers.Remove(SelectedEmployee);

            if (potentialManagers.Count == 0)
            {
                await DeleteEmployeeAsync(managerToDelete);

                return;
            }

            await OpenSwitchManagerAsync(potentialManagers, managerToDelete, SwitchingManagerMode.Delete);
        }
        private async Task OpenSwitchManagerAsync(List<IPerson> collection, Manager oldManager, SwitchingManagerMode mode)
        {
            var view = new SwitchManagerView
            {
                DataContext = new SwitchManagerViewModel(collection, oldManager, mode)
            };

            await DialogHost.Show(view, "RootDialog");
        }
        #endregion

        #region Public Methods
        public async Task DeleteEmployeeAsync()
        {
            if (SelectedEmployee is Manager)
            {
                await HandleDeleteManagerAsync();
            }
            else
                await DeleteEmployeeAsync(SelectedEmployee);
        }
        public async Task OpenEditEmployeeDialogAsync()
        {
            _employeeVm.SetProperties(SelectedEmployee);

            var view = new EditEmployeeView(SelectedEmployee.PersonId)
            {
                DataContext = _employeeVm
            };

            await DialogHost.Show(view, "RootDialog");
        }
        public async Task OpenNewEmployeeAsync()
        {
            var view = new NewEmployeeView
            {
                DataContext = _employeeVm
            };

            await DialogHost.Show(view, "RootDialog");
        }
        public void Reset()
        {
            SelectedEmployee = null;

            _employeeVm.Reset();
        }
        #endregion
    }
}
