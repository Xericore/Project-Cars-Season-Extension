using System;
using System.Windows.Data;
using System.Windows.Media;
using ProjectCarsSeasonExtension.Models;

namespace ProjectCarsSeasonExtension.Converters
{
    public class DifficultyToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is Difficulty difficulty)) return null;

            switch (difficulty)
            {
                case Difficulty.Easy:
                    return new SolidColorBrush(Color.FromRgb(0,241,0));
                case Difficulty.Medium:
                    return new SolidColorBrush(Color.FromRgb(255, 255, 0));
                case Difficulty.Hard:
                    return new SolidColorBrush(Color.FromRgb(255, 127, 0));
                case Difficulty.Insane:
                    return new SolidColorBrush(Color.FromRgb(241, 26, 26));
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
