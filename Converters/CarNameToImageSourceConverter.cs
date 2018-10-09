using System;
using System.Windows.Data;
using ProjectCarsSeasonExtension.Utils;

namespace ProjectCarsSeasonExtension.Converters
{
    public class CarNameToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is string carName))
                return null;

            var carImageSourcePath = CarImageManager.Instance.GetImagePath(carName);

            return carImageSourcePath;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
