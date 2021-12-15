using System.IO;
using System.Text;

using System.Text.Json;

using WeightliftingManagment.Core.Contracts;

namespace WeightliftingManagment.Core.Services
{
    public class FileService : IFileService
    {
        public async Task DeleteAsync(string folderPath, string fileName)
        {
            if (fileName != null && File.Exists(Path.Combine(folderPath, fileName)))
            {
                File.Delete(Path.Combine(folderPath, fileName));
            }
            await Task.CompletedTask;
        }

        public async Task<T> ReadAsync<T>(string folderPath, string fileName)
        {
            var path = Path.Combine(folderPath, fileName);
            if (File.Exists(path))
            {
                var json = await File.ReadAllTextAsync(path);
                return JsonSerializer.Deserialize<T>(json);
            }

            return default;
        }

        public async Task SaveAsync<T>(string folderPath, string fileName, T content)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var fileContent = JsonSerializer.Serialize(content);
            await File.WriteAllTextAsync(Path.Combine(folderPath, fileName), fileContent, Encoding.UTF8);
        }
    }
}
