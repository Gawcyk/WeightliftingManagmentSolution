using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;

using Prism.Commands;

using WeightliftingManagment.Core.Contracts;
using WeightliftingManagment.Core.FlyoutManager;
using WeightliftingManagment.Core.Models;
using WeightliftingManagment.Core.MvvmSupport.ViewModelsTypeBase;
using WeightliftingManagment.Localization.LocalizationModel;

namespace WeightliftingManagment.Moduls.Settings.ViewModels
{
    public class ThemeSettingsViewModel : FlyoutViewModelBase
    {
        private readonly IThemeSelectorService _themeSelectorService;
        private readonly LocalizationService _localizationService;
        private AppTheme _appTheme;
        private ICommand _setThemeCommand;
        private ICommand _setAccentHexCommand;
        private string _accent;
        private ObservableCollection<string> _listOfAccentColorHexString = new();

        public AppTheme AppTheme
        {
            get => _appTheme;
            set => SetProperty(ref _appTheme, value);
        }
        public string Accent
        {
            get => _accent;
            set => SetProperty(ref _accent, value);
        }

        public ObservableCollection<string> ListOfAccentColorHexString
        {
            get => _listOfAccentColorHexString;
            set => SetProperty(ref _listOfAccentColorHexString, value);
        }

        public ICommand SetThemeCommand => _setThemeCommand ??= new DelegateCommand<string>(OnSetTheme);
        public ICommand SetAccentHexCommand => _setAccentHexCommand ??= new DelegateCommand<string>(OnSetAccent);

        public ThemeSettingsViewModel(IThemeSelectorService themeSelectorService, LocalizationService localizationService)
        {
            _themeSelectorService = themeSelectorService;
            _localizationService = localizationService;
            ListOfAccentColorHexString = new ObservableCollection<string>(_themeSelectorService.GetAccents());
            AppTheme = _themeSelectorService.GetCurrentTheme();
            Accent = _themeSelectorService.GetCurrentAccent();
        }

        protected override void OnOpening(FlyoutParameters flyoutParameters)
        {
            base.OnOpening(flyoutParameters);
            AvailableCultures = _localizationService.AvailableCultures;
            SelectedLang = _localizationService.CurrentCulture;
        }

        private List<CultureInfo> _availableCultures;
        public List<CultureInfo> AvailableCultures
        {
            get => _availableCultures;
            set=>SetProperty(ref _availableCultures, value);
        }

        private void OnSetTheme(string themeName)
        {
            var theme = (AppTheme)Enum.Parse(typeof(AppTheme), themeName);
            _themeSelectorService.SetTheme(theme);
        }

        private void OnSetAccent(string hex) => _themeSelectorService.SetAccent(hex);

        private CultureInfo _selectedLang;

        public CultureInfo SelectedLang
        {
            get => _selectedLang;
            set => SetProperty(ref _selectedLang, value, SelectedLangAction);
        }

        private void SelectedLangAction() => _localizationService.ChangeCulture(SelectedLang);
    }
}
