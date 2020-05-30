using System;
using System.Globalization;
using System.Windows.Data;

namespace WOClient.Converters
{
    public class StringToIsErrorConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return false;
            if (value.ToString().Equals(string.Empty)) return false;
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
