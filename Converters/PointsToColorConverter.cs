using System;
using System.Windows.Data;
using System.Windows.Media;
using ProjectCarsSeasonExtension.Utils;

namespace ProjectCarsSeasonExtension.Converters
{
    public class PointsToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            var points = (int)value;

            if (points == PointsUtil.PositionToPoints(1))
            {
                return new SolidColorBrush(Color.FromRgb(222, 196, 50));
            }

            if (points == PointsUtil.PositionToPoints(2))
            {
                return new SolidColorBrush(Color.FromRgb(230, 232, 250));
            }

            if (points == PointsUtil.PositionToPoints(3))
            {
                return new SolidColorBrush(Color.FromRgb(140, 120, 83));
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
