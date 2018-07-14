using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using ProjectCarsSeasonExtension.Utils;

namespace ProjectCarsSeasonExtension.Converters
{
    public class RowToPointsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var rowIndex = 0;
            if (value is DataGridRow row)
                rowIndex = row.GetIndex();

            return PointsUtil.PositionToPoints(rowIndex + 1);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
