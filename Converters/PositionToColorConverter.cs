using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ProjectCarsSeasonExtension.Converters
{
    public class PositionToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            uint playerPosition = (uint) value;

            switch (playerPosition)
            {
                case 1:
                    return new SolidColorBrush(Color.FromRgb(222,196,50));
                case 2:
                    return new SolidColorBrush(Color.FromRgb(230, 232, 250));
                case 3:
                    return new SolidColorBrush(Color.FromRgb(140, 120, 83));
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
