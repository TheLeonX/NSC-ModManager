﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace NSC_ModManager.Converter
{
    public class ImageConverter : IValueConverter {
        public object Convert(
            object value, Type targetType, object parameter, CultureInfo culture) {
            return (string)value;
        }

        public object ConvertBack(
            object value, Type targetType, object parameter, CultureInfo culture) {
            return Binding.DoNothing;
        }
    }
}
