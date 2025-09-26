using ExakisNeliteTSP.Models;

namespace ExakisNeliteTSP.Services.Interface
{
    public interface IProjectFraisService
    {
        Task<List<FraisItem>> LoadAsync(Guid projectId, CancellationToken ct = default);
        Task SaveAsync(Guid projectId, IEnumerable<FraisItem> items, CancellationToken ct = default);
        Task AddAsync(Guid projectId, FraisItem item, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
    }
}
