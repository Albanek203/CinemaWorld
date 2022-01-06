using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using BLL.DTO.Enumerations;

namespace UI.Converters {
    public class SeatStatusConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return (int)value switch {
                (int)SeatStatusType.NotTaken => Brushes.White
              , (int)SeatStatusType.Taken    => Brushes.Red
              , (int)SeatStatusType.Reserved => Brushes.Gray
              , _                            => Brushes.White
            };
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null!;
    }
}