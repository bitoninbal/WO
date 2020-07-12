using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using WOClient.Models;

namespace WOClient.Converters
{
    public class PersonTypeToVisibilityConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return Visibility.Collapsed;
            if (!(value is Person)) return Visibility.Collapsed;

            var person = (Person)value;

            if (person.GetType() == typeof(Employee)) return Visibility.Collapsed;
            if (person.GetType() == typeof(Manager)) return Visibility.Visible;

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
