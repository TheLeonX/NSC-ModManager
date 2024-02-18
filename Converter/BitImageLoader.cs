using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using System.Windows.Media.Imaging;
using System.IO;

namespace NSC_ModManager.Converter
{
    [ValueConversion(typeof(string), typeof(Uri))]
    class BitImageLoader : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {

            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit(); // загружаю картинку
            if (File.Exists((string)value))
                bmp.UriSource = new Uri((string)value);
            else
                bmp.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\Resources\\TemplateImages\\template_icon.png");
            bmp.DecodePixelWidth = 100;
            bmp.EndInit();

            return bmp;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return DependencyProperty.UnsetValue;
        }
    }
}
