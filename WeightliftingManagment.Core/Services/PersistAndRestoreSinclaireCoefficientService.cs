using System.IO;

using WeightliftingManagment.Core.Contracts;
using WeightliftingManagment.Core.Models.Config;

namespace WeightliftingManagment.Core.Services
{
    public class PersistAndRestoreSinclaireCoefficientService : IPersistAndRestoreSinclaireCoefficientService
    {
        private readonly IFileService _fileService;
        private readonly AppConfig _appConfig;
        private readonly SinclaireConfig _sinclaireConfig;
        public PersistAndRestoreSinclaireCoefficientService(IFileService fileService, AppConfig appConfig, SinclaireConfig sinclaireConfig)
        {
            _fileService = fileService;
            _appConfig = appConfig;
            _sinclaireConfig = sinclaireConfig;
        }

        public async Task PersistDataAsync() => await _fileService.SaveAsync(GetPathOfConfigurationFolder(), _appConfig.SinclaireConfigFileName, _sinclaireConfig).ConfigureAwait(false);

        public async Task RestoreDataAsync()
        {
            var result = await _fileService.ReadAsync<SinclaireConfig>(GetPathOfConfigurationFolder(), _appConfig.SinclaireConfigFileName).ConfigureAwait(false);
            if (result is not null && !_sinclaireConfig.Equals(result))
            {
                _sinclaireConfig.Men = result.Men;
                _sinclaireConfig.Women = result.Women;
            }
        }

        private string GetPathOfConfigurationFolder() => Path.Combine(_appConfig.ConfigurationsFolder);
    }
}
