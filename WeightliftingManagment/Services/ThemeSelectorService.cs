using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

using ControlzEx.Theming;

using MahApps.Metro.Theming;

using WeightliftingManagment.Core.Contracts;
using WeightliftingManagment.Core.Models;

namespace WeightliftingManagment.Services
{
    public class ThemeSelectorService : IThemeSelectorService
    {
        private const string HcDarkTheme = "pack://application:,,,/Styles/Themes/HC.Dark.Blue.xaml";
        private const string HcLightTheme = "pack://application:,,,/Styles/Themes/HC.Light.Blue.xaml";
        private const string DefaultAccentColor = "#FF0078D7";
        private string _accent;
        private AppTheme _theme;
        public ThemeSelectorService()
        {
            InitializeTheme();
        }
        public void InitializeTheme()
        {
            // TODO WTS: Mahapps.Metro supports syncronization with high contrast but you have to provide custom high contrast themes
            // We've added basic high contrast dictionaries for Dark and Light themes
            // Please complete these themes following the docs on https://mahapps.com/docs/themes/thememanager#creating-custom-themes
            ThemeManager.Current.AddLibraryTheme(new LibraryTheme(new Uri(HcDarkTheme), MahAppsLibraryThemeProvider.DefaultInstance));
            ThemeManager.Current.AddLibraryTheme(new LibraryTheme(new Uri(HcLightTheme), MahAppsLibraryThemeProvider.DefaultInstance));

            ThemeManager.Current.AddTheme(new Theme("Dark.DimGray", "DimGray (Dark)", "Dark", "DimGray", Colors.DimGray, Brushes.DimGray, true, false));
            ThemeManager.Current.AddTheme(new Theme("Light.DimGray", "DimGray (Light)", "Light", "DimGray", Colors.DimGray, Brushes.DimGray, true, false));

            ThemeManager.Current.AddTheme(new Theme("Dark.DodgerBlue", "DodgerBlue (Dark)", "Dark", "DodgerBlue", Colors.DodgerBlue, Brushes.DodgerBlue, true, false));
            ThemeManager.Current.AddTheme(new Theme("Light.DodgerBlue", "DodgerBlue (Light)", "Light", "DodgerBlue", Colors.DodgerBlue, Brushes.DodgerBlue, true, false));

            ThemeManager.Current.AddTheme(new Theme("Dark.SpringGreen", "SpringGreen (Dark)", "Dark", "SpringGreen", Colors.SpringGreen, Brushes.SpringGreen, true, false));
            ThemeManager.Current.AddTheme(new Theme("Light.SpringGreen", "SpringGreen (Light)", "Light", "SpringGreen", Colors.SpringGreen, Brushes.SpringGreen, true, false));

            ThemeManager.Current.AddTheme(new Theme("Dark.SteelBlue", "SteelBlue (Dark)", "Dark", "SteelBlue", Colors.SteelBlue, Brushes.SteelBlue, true, false));
            ThemeManager.Current.AddTheme(new Theme("Light.SteelBlue", "SteelBlue (Light)", "Light", "SteelBlue", Colors.SteelBlue, Brushes.SteelBlue, true, false));

            ThemeManager.Current.AddTheme(new Theme("Dark.Firebrick", "Firebrick (Dark)", "Dark", "Firebrick", Colors.Firebrick, Brushes.Firebrick, true, false));
            ThemeManager.Current.AddTheme(new Theme("Light.Firebrick", "Firebrick (Light)", "Light", "Firebrick", Colors.Firebrick, Brushes.Firebrick, true, false));

            ThemeManager.Current.AddTheme(new Theme("Dark.PaleGreen", "PaleGreen (Dark)", "Dark", "PaleGreen", Colors.PaleGreen, Brushes.PaleGreen, true, false));
            ThemeManager.Current.AddTheme(new Theme("Light.PaleGreen", "PaleGreen (Light)", "Light", "PaleGreen", Colors.PaleGreen, Brushes.PaleGreen, true, false));

            ThemeManager.Current.AddTheme(new Theme("Dark.CadetBlue", "CadetBlue (Dark)", "Dark", "CadetBlue", Colors.CadetBlue, Brushes.CadetBlue, true, false));
            ThemeManager.Current.AddTheme(new Theme("Light.CadetBlue", "CadetBlue (Light)", "Light", "CadetBlue", Colors.CadetBlue, Brushes.CadetBlue, true, false));

            ThemeManager.Current.AddTheme(new Theme("Dark.MidnightBlue", "MidnightBlue (Dark)", "Dark", "MidnightBlue", Colors.MidnightBlue, Brushes.MidnightBlue, true, false));
            ThemeManager.Current.AddTheme(new Theme("Light.MidnightBlue", "MidnightBlue (Light)", "Light", "MidnightBlue", Colors.MidnightBlue, Brushes.MidnightBlue, true, false));


            _theme = GetCurrentTheme();
            _accent = GetCurrentAccent();
            SetTheme(_theme);
        }

        private string GetNameOfAccent(string accentHex)
        {
            var colorHex = (Color)ColorConverter.ConvertFromString(accentHex);
            var colorName = ThemeManager.Current.Themes.Where(item => item.Name.StartsWith("Dark.") && item.PrimaryAccentColor == colorHex).Select(a => a.ColorScheme).FirstOrDefault();
            return colorName;
        }

        public void SetAccent(string accent) => SetThemeAndAccent(_theme, accent);

        public void SetTheme(AppTheme theme) => SetThemeAndAccent(theme, _accent);

        private void SetThemeAndAccent(AppTheme theme, string accent)
        {

            if (theme == AppTheme.Default)
            {
                ThemeManager.Current.ThemeSyncMode = ThemeSyncMode.SyncAll;
                ThemeManager.Current.SyncTheme();
                var acc = ToHex(ThemeManager.Current.DetectTheme(Application.Current)?.PrimaryAccentColor);
                if (GetAccents().Contains(acc))
                    accent = acc;
            }
            else
            {
                ThemeManager.Current.ThemeSyncMode = ThemeSyncMode.SyncWithHighContrast;
                ThemeManager.Current.SyncTheme();
                ThemeManager.Current.ChangeTheme(Application.Current, $"{theme}.{GetNameOfAccent(accent)}", SystemParameters.HighContrast);
            }

            Application.Current.Properties["Theme"] = theme.ToString();
            Application.Current.Properties["Accent"] = accent;
            _theme = theme;
            _accent = accent;
        }

        private string ToHex(Color? color) => new ColorConverter().ConvertToString(color);
        public IEnumerable<string> GetAccents()
        {
            var accents = ThemeManager.Current.Themes.Where(item => item.Name.StartsWith("Dark.")).Select(x => x.PrimaryAccentColor.ToString()).ToList();
            accents.Sort();
            return accents;
        }

        public string GetCurrentAccent()
        {
            if (Application.Current.Properties.Contains("Accent"))
            {
                return Application.Current.Properties["Accent"].ToString();
            }

            return DefaultAccentColor;
        }
        public AppTheme GetCurrentTheme()
        {
            if (Application.Current.Properties.Contains("Theme"))
            {
                var themeName = Application.Current.Properties["Theme"].ToString();
                Enum.TryParse(themeName, out AppTheme theme);
                return theme;
            }

            return AppTheme.Default;
        }
    }
}
