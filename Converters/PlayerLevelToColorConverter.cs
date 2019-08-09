using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using ProjectCarsSeasonExtension.Models.Player;

namespace ProjectCarsSeasonExtension.Converters
{
    public class PlayerLevelToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            Player player = (Player) value;
            
            var mainWindow = (MainWindow) Application.Current.MainWindow;
            if (mainWindow == null) return null;
            
            if (mainWindow.IsPlayerRookieInCurrentSeason(player.Id))
                return new SolidColorBrush(Color.FromRgb(242, 242, 242));

            return new SolidColorBrush(Color.FromScRgb(0, 255, 255, 255));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
