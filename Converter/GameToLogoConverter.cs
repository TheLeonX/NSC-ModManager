using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NSC_ModManager.Converter
{
    public class GameToLogoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string game = value as string;
            if (string.IsNullOrEmpty(game))
                game = "NSC";

            string imagePath = $"pack://application:,,,/Resources/Styles/UI/logo/{game}.png";
            try
            {
                ImageBrush brush = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri(imagePath, UriKind.Absolute)),
                    Opacity = 1,
                    Stretch = Stretch.Uniform,
                    AlignmentX = AlignmentX.Left,
                    AlignmentY = AlignmentY.Center,

                };
                return brush;
            } catch
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
