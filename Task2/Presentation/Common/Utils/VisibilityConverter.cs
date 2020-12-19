﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Presentation.Common
{
    // this is to control the visibility of the object,
    // so that they are visible if some property in a view model is set to true

    // example in the 

    public class VisibilityConverter : IValueConverter
    {
        public const string Invert = "Invert";

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(Visibility))
                throw new InvalidOperationException("The target must be a Visibility.");

            bool? bValue = (bool?)value;

            if (parameter != null && parameter as string == Invert)
                bValue = !bValue;

            return bValue.HasValue && bValue.Value ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
        #endregion
    }
}
