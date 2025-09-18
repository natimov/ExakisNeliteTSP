namespace ExakisNeliteTSP.Services.Interface
{
    public interface IProjectProfileMonthlyService
    {
        Task<Dictionary<(int ProfilRowId, int Month), decimal?>> LoadAsync(Guid projectId, int year);
        Task SaveAsync(Guid projectId, int year, IEnumerable<(int ProfilRowId, decimal?[] Months)> items);
    }
}
