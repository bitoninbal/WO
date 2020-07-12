using System.Windows.Controls;

namespace WOClient.Components.NewTask
{
    /// <summary>
    /// Interaction logic for NewTaskView.xaml
    /// </summary>
    public partial class NewTaskView : UserControl
    {
        public NewTaskView()
        {
            InitializeComponent();
        }

        private async void SendNewTask_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var vm = (NewTaskViewModel)DataContext;

            await vm.SendNewTaskAsync();
        }
    }
}
