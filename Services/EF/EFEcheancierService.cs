using ExakisNeliteTSP.Data;
using ExakisNeliteTSP.Models;
using ExakisNeliteTSP.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExakisNeliteTSP.Services.EF
{
    public sealed class EFEcheancierService : IProjectEcheancierService
    {
        private readonly TspDbContext _db;
        public EFEcheancierService(TspDbContext db) => _db = db;

        public async Task<List<EcheancierItem>> LoadAsync(Guid projectId, CancellationToken ct = default)
            => await _db.EcheancierItem
                        .Where(x => x.ProjectId == projectId)
                        .OrderBy(x => x.BillingYear).ThenBy(x => x.BillingMonth).ThenBy(x => x.Id)
                        .AsNoTracking()
                        .ToListAsync(ct);

        public async Task SaveAsync(Guid projectId, IEnumerable<EcheancierItem> items, CancellationToken ct = default)
        {
            var list = (items ?? Enumerable.Empty<EcheancierItem>()).ToList();
            var existing = await _db.EcheancierItem.Where(x => x.ProjectId == projectId).ToListAsync(ct);

            foreach (var it in list)
            {
                it.ProjectId = projectId;

                if (it.Id == 0)
                {
                    await _db.EcheancierItem.AddAsync(new EcheancierItem
                    {
                        ProjectId = projectId,
                        Label = it.Label,
                        Percent = it.Percent,
                        BillingMonth = it.BillingMonth,
                        BillingYear = it.BillingYear,
                        Comment = it.Comment
                    }, ct);
                }
                else
                {
                    var row = existing.FirstOrDefault(x => x.Id == it.Id);
                    if (row is null)
                    {
                        await _db.EcheancierItem.AddAsync(new EcheancierItem
                        {
                            ProjectId = projectId,
                            Label = it.Label,
                            Percent = it.Percent,
                            BillingMonth = it.BillingMonth,
                            BillingYear = it.BillingYear,
                            Comment = it.Comment
                        }, ct);
                    }
                    else
                    {
                        row.Label = it.Label;
                        row.Percent = it.Percent;
                        row.BillingMonth = it.BillingMonth;
                        row.BillingYear = it.BillingYear;
                        row.Comment = it.Comment;
                        _db.EcheancierItem.Update(row);
                    }
                }
            }

            await _db.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var row = await _db.EcheancierItem.FirstOrDefaultAsync(x => x.Id == id, ct);
            if (row is null) return;
            _db.EcheancierItem.Remove(row);
            await _db.SaveChangesAsync(ct);
        }
    }
}
