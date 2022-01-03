namespace WeightliftingManagment.Core.Contracts
{
    public interface IPersistAndRestoreSinclaireCoefficientService
    {
        Task PersistDataAsync();
        Task RestoreDataAsync();
    }
}
