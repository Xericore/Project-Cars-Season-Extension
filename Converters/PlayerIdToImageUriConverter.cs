using System;
using System.Globalization;
using System.Windows.Data;

namespace ProjectCarsSeasonExtension.Converters
{
    public class PlayerIdToImageUriConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";

            int playerId = (int)value;

            if (playerId < 0)
            {
                return "/Assets/AddPlayer.png";
            }

            return "/Assets/helmet.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}