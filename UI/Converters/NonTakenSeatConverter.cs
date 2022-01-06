using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using BLL.DTO.Enumerations;
using DLL.Models;

namespace UI.Converters {
    public class NonTakenSeatConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            ((List<Seat>)value).Count(x => x.Status == (int)SeatStatusType.NotTaken);
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;
    }
}