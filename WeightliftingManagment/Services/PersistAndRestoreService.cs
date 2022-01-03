using System.Collections;
using System.IO;
using System.Windows;

using WeightliftingManagment.Core.Contracts;
using WeightliftingManagment.Core.Models.Config;

namespace WeightliftingManagment.Services
{
    public class PersistAndRestoreService : IPersistAndRestoreService
    {
        private readonly IFileService _fileService;
        private readonly AppConfig _appConfig;

        public PersistAndRestoreService(IFileService fileService, AppConfig appConfig)
        {
            _fileService = fileService;
            _appConfig = appConfig;
        }

        public async Task PersistDataAsync()
        {
            if (Application.Current.Properties != null)
            {
                await _fileService.SaveAsync(folderPath: GetPathOfConfigurationFolder(), fileName: GetFileName(), content: Application.Current.Properties).ConfigureAwait(false);
            }
        }

        public async Task RestoreDataAsync()
        {
            var properties = await _fileService.ReadAsync<IDictionary>(folderPath: GetPathOfConfigurationFolder(), fileName: GetFileName()).ConfigureAwait(false);
            if (properties != null)
            {
                foreach (DictionaryEntry property in properties)
                {
                    Application.Current.Properties.Add(property.Key, property.Value);
                }
            }
        }

        private string GetFileName() => _appConfig.AppConfigFileName;
        private string GetPathOfConfigurationFolder() => Path.Combine(_appConfig.ConfigurationsFolder);
    }
}
