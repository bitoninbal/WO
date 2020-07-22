using System.Windows.Controls;
using WOCommon.Enums;

namespace WOClient.Components.Employees
{
    /// <summary>
    /// Interaction logic for EmployeesView.xaml
    /// </summary>
    public partial class EmployeesView: UserControl
    {
        public EmployeesView()
        {
            InitializeComponent();
        }

        #region Private Methods
        private async void DeleteEmployee_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var vm = (EmployeesViewModel)DataContext;

            await vm.DeleteEmployeeAsync();
        }

        private async void OpenNewEmployee_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var vm = (EmployeesViewModel)DataContext;

            await vm.OpenNewEmployeeAsync();
        }

        private void PermissionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            var vm       = (EmployeesViewModel)DataContext;

            if (vm.Employee is null) return;

            var value = (PermissionsEnum)comboBox.SelectedValue;

            switch (value)
            {
                case PermissionsEnum.Employee:
                    vm.Employee.Permission = PermissionsEnum.Employee;

                    break;
                case PermissionsEnum.Manager:
                    vm.Employee.Permission = PermissionsEnum.Manager;

                    break;
                default:
                    vm.Employee.Permission = PermissionsEnum.Employee;

                    break;
            }
        }
        #endregion
    }
}
