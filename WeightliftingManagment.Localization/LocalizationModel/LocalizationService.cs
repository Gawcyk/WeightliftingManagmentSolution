using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using WeightliftingManagment.Localization.Readers;

namespace WeightliftingManagment.Localization.LocalizationModel
{
    public class LocalizationService : INotifyPropertyChanged
    {

        #region Singleton

        private static LocalizationService _instance;

        public static LocalizationService Instance => _instance ??= new LocalizationService();

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion

        #region Fields

        private readonly Dictionary<CultureInfo, Dictionary<string, LocalizationEntry>> _languageEntries = new();
        private CultureInfo _currentCulture;

        #endregion

        #region Properties

        public CultureInfo CurrentCulture
        {
            get => _currentCulture;
            set {
                if (_currentCulture?.Equals(value) == true)
                    return;

                if (!_languageEntries.ContainsKey(value))
                    return;

                _currentCulture = value;
                RaisePropertyChanged();
            }
        }

        public List<CultureInfo> AvailableCultures => _languageEntries.Keys.ToList();

        #endregion

        #region Public Methods

        public void ChangeCulture(CultureInfo culture)
        {
            if (!_languageEntries.ContainsKey(culture))
            {
                culture = CultureInfo.GetCultureInfo("en-US");
            }
            CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;
        }

        public void RegisterLang(string pathToFile, string fileName = "AppLocalization.csv")
        {
            foreach (var lang in new CsvFileReader(pathToFile, fileName).GetEntries())
            {
                _languageEntries.Add(lang.Key, lang.Value);
            }
        }

        public string GetValue(string key, bool nullWhenUnfound = true)
        {
            if (_languageEntries == null || CurrentCulture == null)
                return key;

            var entries = _languageEntries[CurrentCulture];

            if (key == null || !entries.ContainsKey(key))
                return nullWhenUnfound ? null : key;

            return entries[key].Value;
        }
        #endregion
    }
}