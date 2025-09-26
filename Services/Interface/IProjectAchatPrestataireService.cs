using ExakisNeliteTSP.Models;

namespace ExakisNeliteTSP.Services.Interface
{
    public interface IProjectAchatPrestataireService
    {
        Task<List<AchatPrestataireItem>> LoadAsync(Guid projectId, CancellationToken ct = default);
        Task SaveAsync(Guid projectId, IEnumerable<AchatPrestataireItem> items, CancellationToken ct = default);
        Task AddAsync(Guid projectId, AchatPrestataireItem item, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
    }
}
