using System;
using System.Globalization;
using System.Windows.Data;
using WOCommon.Enums;

namespace WOClient.Converters
{
    public class PermissionEnumToStringConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return "Employee";
            if (!(value is PermissionsEnum)) return "Employee";

            if (value is PermissionsEnum.Manager)
                return "Manager";
            else
                return "Employee";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
