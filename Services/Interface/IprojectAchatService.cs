using ExakisNeliteTSP.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ExakisNeliteTSP.Services.Interface
{
    public interface IProjectAchatService
    {
        Task<List<AchatItem>> LoadAsync(Guid projectId, CancellationToken ct = default);
        Task SaveAsync(Guid projectId, IEnumerable<AchatItem> items, CancellationToken ct = default);
        Task AddAsync(Guid projectId, AchatItem item, CancellationToken ct = default);
        Task DeleteAsync(int achatItemId, CancellationToken ct = default);
    }
}
