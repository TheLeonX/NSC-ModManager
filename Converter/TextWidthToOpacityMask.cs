using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace NSC_ModManager.Converter
{
    public class TextWidthToOpacityMaskConverter : IMultiValueConverter
    {
        // values[0] = text ActualWidth, values[1] = container ActualWidth
        // fadePx — ширина размытия по краям в пикселях (можно менять)
        private const double fadePx = 8.0;

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 2) return DependencyProperty.UnsetValue;
            if (!(values[0] is double textW) || !(values[1] is double totalW)) return DependencyProperty.UnsetValue;
            if (totalW <= 0) totalW = 1.0;

            double left = 0.0; // прямоугольник привязан слева к тексту
            double start = left / totalW;
            double end = Math.Min(1.0, (left + textW) / totalW);
            double fade = Math.Min(0.5, fadePx / totalW);

            double g0 = Math.Max(0.0, start - fade);
            double g1 = Math.Max(0.0, start);
            double g2 = Math.Min(1.0, end);
            double g3 = Math.Min(1.0, end + fade);

            var brush = new LinearGradientBrush { StartPoint = new System.Windows.Point(0, 0.5), EndPoint = new System.Windows.Point(1, 0.5) };
            brush.GradientStops.Add(new GradientStop(System.Windows.Media.Color.FromArgb(0, 255, 255, 255), g0));
            brush.GradientStops.Add(new GradientStop(System.Windows.Media.Color.FromArgb(255, 255, 255, 255), g1));
            brush.GradientStops.Add(new GradientStop(System.Windows.Media.Color.FromArgb(255, 255, 255, 255), g2));
            brush.GradientStops.Add(new GradientStop(System.Windows.Media.Color.FromArgb(0, 255, 255, 255), g3));

            return brush;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}
