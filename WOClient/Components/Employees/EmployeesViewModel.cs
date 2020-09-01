using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WOClient.Components.Base;
using WOClient.Components.Main;
using WOClient.Components.NewEmployee;
using WOClient.Components.SwitchManager;
using WOClient.Library.Api;
using WOClient.Library.Models;
using WOCommon.Enums;

namespace WOClient.Components.Employees
{
    public class EmployeesViewModel: BaseViewModel, IEmplyeesViewModel
    {
        public EmployeesViewModel(INewEmployeeViewModel newEmployeeVm, IClientApi api)
        {
            _api           = api;
            _newEmployeeVm = newEmployeeVm;
        }

        #region Fields
        private IClientApi _api;
        private INewEmployeeViewModel _newEmployeeVm;
        private IPerson _employee;
        #endregion

        #region Properties
        public IPerson Employee
        {
            get => _employee;
            set
            {
                if (_employee == value) return;

                _employee = value;

                NotifyPropertyChanged(nameof(Employee));
            }
        }
        #endregion

        #region Private Methods
        private async Task OpenSwitchManagerAsync(List<IPerson> collection, Manager deletedManager)
        {
            var view = new SwitchManagerView
            {
                DataContext = new SwitchManagerViewModel(collection, deletedManager)
            };

            await DialogHost.Show(view, "RootDialog");
        }
        #endregion

        #region Public Methods
        public async Task DeleteEmployeeAsync()
        {
            if (Employee is Manager) await HandleDeleteManagerAsync();

            var user = (Manager)IMainWindowViewModel.User;

            await _api.DeleteEmployeeAsync(Employee.PersonId);

            user.MyEmployees.Remove(Employee);
        }
        public async Task HandleDeleteManagerAsync()
        {
            var emplopyee = Employee as Manager;

            if (emplopyee.MyEmployees.Count == 0) return;

            var manager    = IMainWindowViewModel.User as Manager;
            var collection = manager.MyEmployees.Where((employee) => employee.Permission == PermissionsEnum.Manager).ToList();

            collection.Remove(Employee);

            if (collection.Count == 0)
            {
                foreach (var item in emplopyee.MyEmployees) manager.AssignedEmployee(item);

                return;
            }

            await OpenSwitchManagerAsync(collection, emplopyee);
        }
        public async Task OpenNewEmployeeAsync()
        {
            var view = new NewEmployeeView
            {
                DataContext = _newEmployeeVm
            };

            await DialogHost.Show(view, "RootDialog");
        }
        #endregion
    }
}
