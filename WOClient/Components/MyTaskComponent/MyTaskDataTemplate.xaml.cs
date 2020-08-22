using System.Windows;
using System.Windows.Controls.Primitives;
using WOClient.Library.Models;

namespace WOClient.Components.MyTaskComponent
{
    public partial class MyTaskDataTemplate: ResourceDictionary
    {
        private void LockToggleButton_Click(object sender, RoutedEventArgs e)
        {
            var control = sender as ToggleButton;

            switch (control.IsChecked)
            {
                case true:
                    if (MessageBox.Show("Are you sure you want to open the task?", "Warning", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        control.IsChecked = false;

                        var task = control.CommandParameter as MyTask;

                        if (task.IsArchive) control.Command.Execute(control.CommandParameter);
                    }

                    break;
                case false:
                    if (MessageBox.Show("Are you sure you want to close the task?", "Warning", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        control.IsChecked = true;
                    }

                    break;
                default:
                    break;
            }
        }
    }
}
