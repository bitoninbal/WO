using MaterialDesignThemes.Wpf;
using System.Threading.Tasks;
using WOClient.Components.Base;
using WOClient.Components.Main;
using WOClient.Components.NewEmployee;
using WOClient.Library.Api;
using WOClient.Library.Models;

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
                NotifyPropertyChanged("Employee");
            }
        }
        public INewEmployeeViewModel NewEmployeeVm { get; }
        #endregion

        #region Public Methods
        public async Task DeleteEmployeeAsync()
        {
            var user = (Manager)IMainWindowViewModel.User;

            await _api.DeleteEmployeeAsync(Employee.PersonId);

            user.MyEmployees.Remove(Employee);
        }
        #endregion

        #region Private Methods
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
