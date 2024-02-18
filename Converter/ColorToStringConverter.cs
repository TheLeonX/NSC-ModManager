using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using System.Drawing;

namespace NSC_ModManager.Converter
{
    [ValueConversion(typeof(Color), typeof(string))]
    class ColorToStringConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {

            string color = "#" + ((Color)value).A.ToString("X2") + ((Color)value).B.ToString("X2") + ((Color)value).G.ToString("X2") + ((Color)value).R.ToString("X2");


            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {

            System.Windows.Media.Color col = (System.Windows.Media.Color)value;

            Color conv_color = System.Drawing.Color.FromArgb(col.A, col.B, col.G, col.R);

            return conv_color;
        }
    }
    class ColorToStringConverter2 : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {

            string color = "#" + ((Color)value).A.ToString("X2") + ((Color)value).R.ToString("X2") + ((Color)value).G.ToString("X2") + ((Color)value).B.ToString("X2");


            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {

            System.Windows.Media.Color col = (System.Windows.Media.Color)value;

            Color conv_color = System.Drawing.Color.FromArgb(col.A, col.R, col.G, col.B);

            return conv_color;
        }
    }

}
