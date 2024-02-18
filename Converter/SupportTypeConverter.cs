using NSC_ModManager.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace NSC_ModManager.Converter {
    [ValueConversion(typeof(int), typeof(string))]
    class SupportTypeConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {

            int index = 0;
            switch ((int)value) {
                case 0x03:
                    index = 0;
                    break;
                case 0x04:
                    index = 1;
                    break;
                case 0x05:
                    index = 2;
                    break;
                case 0x06:
                    index = 3;
                    break;
                case 0x07:
                    index = 4;
                    break;
                case 0x08:
                    index = 5;
                    break;
                case 0x0B:
                    index = 6;
                    break;
                case 0x0C:
                    index = 7;
                    break;
            }
            return Program.supportTypeList[index].ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return DependencyProperty.UnsetValue;
        }
    }
}
