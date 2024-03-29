﻿namespace WeightliftingManagment.Core.Contracts
{
    public interface IFileService
    {
        Task<T> ReadAsync<T>(string folderPath, string fileName);

        Task SaveAsync<T>(string folderPath, string fileName, T content);

        Task DeleteAsync(string folderPath, string fileName);
    }
}
