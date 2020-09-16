using System;
using System.Globalization;
using System.Windows.Data;
using WOClient.Components.Main;
using WOClient.Library.Models;

namespace WOClient.Converters
{
    public class EmployeeIdToFullNameConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return string.Empty;

            var id = (int)value;

            if (id == LoggedInUser.Instance.Id)
                return $"{LoggedInUser.Instance.FirstName} {LoggedInUser.Instance.LastName}";
            else
            {
                if (IMainWindowViewModel.User is Manager)
                {
                    var manager = IMainWindowViewModel.User as Manager;
                    var employee = manager.GetEmplyee(id);

                    if (employee is null) return string.Empty;

                    return $"{employee.FirstName} {employee.LastName}";
                }
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
