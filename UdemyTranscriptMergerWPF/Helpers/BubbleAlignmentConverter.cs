using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace UdemyTranscriptMergerWPF.Helpers
{
    class BubbleAlignmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string type = value.ToString().ToUpper();

            return type switch
            {
                "INFO" => HorizontalAlignment.Left,
                "SYSTEM" => HorizontalAlignment.Left,
                "EXPORT" => HorizontalAlignment.Right,
                "USER" => HorizontalAlignment.Right,
                "ERROR" => HorizontalAlignment.Right,
                _ => HorizontalAlignment.Left
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
           => throw new NotImplementedException();
    }
}
