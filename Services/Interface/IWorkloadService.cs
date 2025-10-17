using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ExakisNeliteTSP.Models; // <- TON namespace Models (même que WorkloadItem)

public interface IWorkloadService
{
    Task<List<WorkloadItem>> LoadAsync(Guid projectId, CancellationToken ct = default);
    Task UpdateRowAsync(WorkloadItem row, CancellationToken ct = default);
    Task UpdateAllocationAsync(Guid rowId, int profileId, decimal jh, CancellationToken ct = default);
    Task<WorkloadItem> CreateParentAsync(Guid projectId, int orderIndex, CancellationToken ct = default);
    Task DeleteRowsAsync(IEnumerable<Guid> rowIds, CancellationToken ct = default);

}
