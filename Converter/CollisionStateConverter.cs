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
    class CollisionStateConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            string state = "???";
            switch ((int)value) {
                case 2:
                    state = "Base";
                    break;
                case 3:
                    state = "Awakening";
                    break;
                case 9:
                    state = "Awakening Model (???)";
                    break;
            }

            return state;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return DependencyProperty.UnsetValue;
        }
    }
}
