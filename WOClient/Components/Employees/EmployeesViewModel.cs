using System.Collections.ObjectModel;
using System.Windows.Input;
using WOClient.Components.Base;
using WOClient.Enums;
using WOClient.Models;
using WOClient.Resources.Commands;

namespace WOClient.Components.Employees
{
    public class EmployeesViewModel: BaseViewModel, IEmplyeesViewModel
    {
        public EmployeesViewModel()
        {
            DeleteEmployeeCommand = new RelayCommand(DeleteEmployee);
            Employees = new ObservableCollection<Employee>();

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
        #endregion

        #region ICommands
        public ICommand DeleteEmployeeCommand { get; }
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
        public ObservableCollection<Employee> Employees { get; set; }
        #endregion

        #region Private Methods
        private void DeleteEmployee()
        {
            Employees.Remove(Employee);
        }
        #endregion
    }
}
