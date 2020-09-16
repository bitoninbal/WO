using System.Windows;
using System.Windows.Controls;

namespace WOClient.Components.NewEmployee
{
    /// <summary>
    /// Interaction logic for EditEmployeeView.xaml
    /// </summary>
    public partial class EditEmployeeView: UserControl
    {
        public EditEmployeeView(int id)
        {
            InitializeComponent();

            EmployeeId = id;
        }

        #region Properties
        public int EmployeeId { get; } 
        #endregion

        #region Private Methods
        private async void EditEmplyee_Click(object sender, RoutedEventArgs e)
        {
            var vm = (EmployeeViewModel)DataContext;

            await vm.EditEmployeeAsync(EmployeeId);
        }
        #endregion
    }
}
