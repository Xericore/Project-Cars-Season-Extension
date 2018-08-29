using System;
using System.Globalization;
using System.Windows.Data;
using ProjectCarsSeasonExtension.Utils;

namespace ProjectCarsSeasonExtension.Converters
{
    public class PositionToPointsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            uint playerPosition = (uint) value;

            return PointsUtil.PositionToPoints(playerPosition);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
