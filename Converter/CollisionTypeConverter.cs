using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace NSC_ModManager.Converter
{
    [ValueConversion(typeof(Int16), typeof(string))]
    class CollisionTypeConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            string state = "???";
            switch ((int)value) {
                case 0:
                    state = "Collision";
                    break;
                case 1:
                    state = "Hurtbox";
                    break;
                case 2:
                    state = "Tracker";
                    break;
            }

            return state;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return DependencyProperty.UnsetValue;
        }
    }
}
