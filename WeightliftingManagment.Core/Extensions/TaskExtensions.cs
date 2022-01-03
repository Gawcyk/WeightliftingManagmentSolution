namespace WeightliftingManagment.Core.Extensions
{
    public static class TaskExtensions
    {
        public static async void Await(this Task task) => await task.ConfigureAwait(false);
    }
}
