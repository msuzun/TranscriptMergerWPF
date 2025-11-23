using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace UdemyTranscriptMergerWPF.Helpers
{
    class LogTypeToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string type = value.ToString().ToUpper();

            return type switch
            {
                "INFO" => new SolidColorBrush(Color.FromRgb(76, 175, 80)),   // Green
                "SYSTEM" => new SolidColorBrush(Color.FromRgb(120, 120, 120)), // Gray
                "EXPORT" => new SolidColorBrush(Color.FromRgb(33, 150, 243)),  // Blue
                "USER" => new SolidColorBrush(Color.FromRgb(0, 172, 193)),   // Teal
                "ERROR" => new SolidColorBrush(Color.FromRgb(244, 67, 54)),   // Red
                _ => Brushes.DarkGray
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}

