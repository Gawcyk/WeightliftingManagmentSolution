namespace WeightliftingManagment.Core.Contracts
{
    public interface IPersistAndRestoreThemeService
    {
        Task PersistDataAsync();
        Task RestoreDataAsync();
    }
}
