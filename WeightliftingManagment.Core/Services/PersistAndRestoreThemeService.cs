using System.IO;

using WeightliftingManagment.Core.Contracts;
using WeightliftingManagment.Core.Models.Config;

namespace WeightliftingManagment.Core.Services
{
    public class PersistAndRestoreThemeService : IPersistAndRestoreThemeService
    {
        private readonly IFileService _fileService;
        private readonly AppConfig _appConfig;
        private readonly ThemeConfig _themeConfig;
        public PersistAndRestoreThemeService(IFileService fileService, AppConfig appConfig, ThemeConfig themeConfig)
        {
            _fileService = fileService;
            _appConfig = appConfig;
            _themeConfig = themeConfig;
        }

        public async Task PersistDataAsync() => await _fileService.SaveAsync(GetPathOfConfigurationFolder(), _appConfig.ThemeConfigFileName, _themeConfig).ConfigureAwait(false);

        public async Task RestoreDataAsync()
        {
            var result = await _fileService.ReadAsync<ThemeConfig>(GetPathOfConfigurationFolder(), _appConfig.ThemeConfigFileName).ConfigureAwait(false);
            if (result is not null && !_themeConfig.Equals(result))
            {
                _themeConfig.AccentHex = result.AccentHex;
                _themeConfig.Theme = result.Theme;
            }
        }

        private string GetPathOfConfigurationFolder() => Path.Combine( _appConfig.ConfigurationsFolder);
    }
}
