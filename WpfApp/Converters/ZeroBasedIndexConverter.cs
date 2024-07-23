    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Data;

    namespace WpfApp.Converters
    {
        public class ZeroBasedIndexConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value is int intValue)
                {
                    return intValue - 1; // Convert 1-based index to 0-based index
                }
                return value;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value is int intValue)
                {
                    return intValue + 1; // Convert back to 1-based index
                }
                return value;
            }
        }
    }
