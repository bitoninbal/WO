using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using WOCommon.Enums;

namespace WOClient.Converters
{
    public class PremissionToVisibilityConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return Visibility.Collapsed;
            if (!(value is PermissionsEnum)) return Visibility.Collapsed;

            var permissions = (PermissionsEnum)value;

            if (permissions == PermissionsEnum.Employee) return Visibility.Collapsed;
            if (permissions == PermissionsEnum.Manager) return Visibility.Visible;

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
