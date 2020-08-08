﻿using System;
using System.Globalization;
using System.Windows.Data;
using WOClient.Library.Models;

namespace WOClient.Converters
{
    /// <summary>
    /// 
    /// </summary>
    public class LoggedInToBooleanConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return false;
            if (!(value is int)) return false;

            var id = (int)value;

            if (id == LoggedInUser.Instance.Id)
                return true;
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
