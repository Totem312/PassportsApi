namespace WebApi.Interfases
{
    public interface IUpdater
    {
        Task UpdateAsync();
        Task CreateTriggerAsync();
        Task CreateTempTable();
        Task ValidateData();
    }
}
