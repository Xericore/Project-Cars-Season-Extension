using System;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Color = System.Drawing.Color;

namespace ProjectCarsSeasonExtension.Converters
{
    public class RowToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var rowIndex = 0;
            if (value is DataGridRow row)
                rowIndex = row.GetIndex();

            switch (rowIndex)
            {
                case 0:
                    return new SolidColorBrush(System.Windows.Media.Color.FromRgb(222,196,50));
                case 1:
                    return new SolidColorBrush(System.Windows.Media.Color.FromRgb(230, 232, 250));
                case 2:
                    return new SolidColorBrush(System.Windows.Media.Color.FromRgb(140, 120, 83));
            }

            return Color.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
