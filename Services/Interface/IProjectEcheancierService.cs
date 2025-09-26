using ExakisNeliteTSP.Models;

namespace ExakisNeliteTSP.Services.Interface
{
    public interface IProjectEcheancierService
    {
        Task<List<EcheancierItem>> LoadAsync(Guid projectId, CancellationToken ct = default);
        Task SaveAsync(Guid projectId, IEnumerable<EcheancierItem> items, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
    }
}
