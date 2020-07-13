using System.Windows.Controls;

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

        private async void DelteEmployee_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var vm = (EmployeesViewModel)DataContext;

            await vm.DeleteEmployeeAsync();
        }

        private async void OpenNewEmployee_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var vm = (EmployeesViewModel)DataContext;

            await vm.OpenNewEmployeeAsync();
        }
    }
}
