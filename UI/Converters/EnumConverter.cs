using System;
using System.Windows.Data;
using UI.Enumeration;

namespace UI.Converters {
    public class EnumConverter : IValueConverter {
        public object Convert(object                           value
                            , Type                             targetType
                            , object                           parameter
                            , System.Globalization.CultureInfo culture) {
            return (GenreType)value;
        }
        public object ConvertBack(object                           value
                                , Type                             targetType
                                , object                           parameter
                                , System.Globalization.CultureInfo culture) {
            return null;
        }
    }
}