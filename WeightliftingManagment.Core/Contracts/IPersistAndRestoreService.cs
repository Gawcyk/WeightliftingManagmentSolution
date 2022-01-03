namespace WeightliftingManagment.Core.Contracts
{
    public interface IPersistAndRestoreService
    {
        Task PersistDataAsync();
        Task RestoreDataAsync();
    }

}
