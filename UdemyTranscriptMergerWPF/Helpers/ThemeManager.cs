using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace UdemyTranscriptMergerWPF.Helpers
{
    public static class ThemeManager
    {
        private static bool _isDark = false;

        public static void ApplyTheme(bool dark)
        {


            _isDark = dark;

            if (_isDark)
            {
                Application.Current.Resources["PrimaryBrush"] = Application.Current.Resources["PrimaryBrush.dark"];
                Application.Current.Resources["BackgroundColorBrush"] = Application.Current.Resources["BackgroundColorBrush.dark"];
                Application.Current.Resources["PanelColorBrush"] = Application.Current.Resources["PanelColorBrush.dark"];
                Application.Current.Resources["BoxColorBrush"] = Application.Current.Resources["BoxColorBrush.dark"];
                Application.Current.Resources["ForegroundColorBrush"] = Application.Current.Resources["ForegroundColorBrush.dark"];
                Application.Current.Resources["OutlineBrush"] = Application.Current.Resources["OutlineBrush.dark"];
            }
            else
            {
                Application.Current.Resources["PrimaryBrush"] = Application.Current.Resources["PrimaryBrush.light"];
                Application.Current.Resources["BackgroundColorBrush"] = Application.Current.Resources["BackgroundColorBrush.light"];
                Application.Current.Resources["PanelColorBrush"] = Application.Current.Resources["PanelColorBrush.light"];
                Application.Current.Resources["BoxColorBrush"] = Application.Current.Resources["BoxColorBrush.light"];
                Application.Current.Resources["ForegroundColorBrush"] = Application.Current.Resources["ForegroundColorBrush.light"];
                Application.Current.Resources["OutlineBrush"] = Application.Current.Resources["OutlineBrush.light"];
            }
        }

        public static void ToggleTheme()
        {
            ApplyTheme(!_isDark);
        }
    }
}
