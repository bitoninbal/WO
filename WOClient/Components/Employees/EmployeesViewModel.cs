using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WOClient.Components.Base;
using WOClient.Components.NewEmployee;
using WOClient.Enums;
using WOClient.Models;
using WOClient.Resources.Commands;

namespace WOClient.Components.Employees
{
    public class EmployeesViewModel: BaseViewModel, IEmplyeesViewModel
    {
        public EmployeesViewModel(INewEmployeeViewModel newEmployeeVm)
        {
            OpenNewEmployeeCommand = new RelayCommand(OpenNewEmployee);
            DeleteEmployeeCommand = new RelayCommand(DeleteEmployee);
            Employees = new ObservableCollection<Employee>();
            _newEmployeeVm = newEmployeeVm;

            var emplyee1 = new Employee
            {
                FirstName = "Inbal",
                LastName = "Biton",
                Email = "iaf106@walla.com",
                Premission = PremissionsEnum.Manager,
                Phone = "+972529876543"
            };

            var emplyee2 = new Employee
            {
                FirstName = "Amir",
                LastName = "Liberzon",
                Email = "iaf105@walla.com",
                Premission = PremissionsEnum.Manager,
                Phone = "+972501234567"
            };

            Employees.Add(emplyee1);
            Employees.Add(emplyee2);
        }

        #region Fields
        private Employee _employee;
        private INewEmployeeViewModel _newEmployeeVm;
        #endregion

        #region ICommands
        public ICommand DeleteEmployeeCommand { get; }
        public ICommand OpenNewEmployeeCommand { get; }
        #endregion

        #region Properties
        public Employee Employee
        {
            get => _employee;
            set
            {
                if (_employee == value) return;

                _employee = value;
                NotifyPropertyChanged("Employee");
            }
        }

        public INewEmployeeViewModel NewEmployeeVm
        {
            get => _newEmployeeVm;
            set
            {
                if (_newEmployeeVm == value) return;

                _newEmployeeVm = value;
                NotifyPropertyChanged("NewEmployeeVm");
            }
        }
        public ObservableCollection<Employee> Employees { get; set; }
        #endregion

        #region Private Methods
        private void DeleteEmployee()
        {
            Employees.Remove(Employee);
        }
        private async void OpenNewEmployee()
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
