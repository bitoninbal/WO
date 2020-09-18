using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using WOClient.Library.Models;

namespace WOClient.Components.MyTaskComponent
{
    public partial class MyTaskDataTemplate: ResourceDictionary
    {
        private void LockToggleButton_Click(object sender, MouseButtonEventArgs e)
        {
            var control = sender as ToggleButton;

            switch (control.IsChecked)
            {
                case true:
                    if (MessageBox.Show("Are you sure you want to open the task?", "Warning", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        control.IsChecked = false;

                        control.Command.Execute(control.CommandParameter);
                    }

                    break;
                case false:
                    if (MessageBox.Show("Are you sure you want to close the task?", "Warning", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        control.IsChecked = true;

                        control.Command.Execute(control.CommandParameter);
                    }

                    break;
                default:
                    break;
            }
        }
    }
}
