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

        private async void EditEmployee_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var vm = (EmployeesViewModel)DataContext;

            await vm.OpenEditEmployeeDialogAsync();
        }

        private async void OpenNewEmployee_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var vm = (EmployeesViewModel)DataContext;

            await vm.OpenNewEmployeeAsync();
        }
        #endregion
    }
}
