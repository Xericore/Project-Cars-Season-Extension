using System;
using System.Windows.Data;
using ProjectCarsSeasonExtension.Utils;

namespace ProjectCarsSeasonExtension.Converters
{
    public class TrackNameToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is string trackName))
                return null;

            var trackImageSourcePath = TrackImageManager.Instance.GetImagePath(trackName);

            return trackImageSourcePath;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
