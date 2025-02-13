﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace NSC_ModManager.Converter {
    public class IndexConverter : IValueConverter {
        public object Convert(object value, Type TargetType, object parameter, CultureInfo culture) {
            ListBoxItem item = (ListBoxItem)value;
            ListBox listBox = ItemsControl.ItemsControlFromItemContainer(item) as ListBox;
            int index = listBox.ItemContainerGenerator.IndexFromContainer(item);
            return index.ToString("X2") + " - ";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
