using ExakisNeliteTSP.Data;               // <-- ajuste le namespace si ton DbContext est ailleurs
using ExakisNeliteTSP.Models;
using ExakisNeliteTSP.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExakisNeliteTSP.Services.EF;

public class ProjectAchatService : IProjectAchatService
{
    private readonly TspDbContext _db;     // <-- ajuste le nom si ton DbContext s'appelle autrement
    public ProjectAchatService(TspDbContext db) => _db = db;

    public async Task<List<AchatItem>> LoadAsync(Guid projectId, CancellationToken ct = default)
    {
        return await _db.AchatItem
            .Where(a => a.ProjectId == projectId)
            .OrderBy(a => a.Designation)
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task AddAsync(Guid projectId, AchatItem item, CancellationToken ct = default)
    {
        item.Id = 0;
        item.ProjectId = projectId;

        // Plus besoin de Recompute(item)

        await _db.AchatItem.AddAsync(new AchatItem
        {
            ProjectId = projectId,
            Designation = item.Designation,
            PurchaseType = item.PurchaseType,
            UnitCostHt = item.UnitCostHt,
            Quantity = item.Quantity,
            UnitResaleHt = item.UnitResaleHt,
            Comment = item.Comment
        }, ct);

        await _db.SaveChangesAsync(ct);
    }


    public async Task SaveAsync(Guid projectId, IEnumerable<AchatItem> items, CancellationToken ct = default)
    {
        var list = (items ?? Enumerable.Empty<AchatItem>()).ToList();

        // Charger les lignes déjà persistées pour ce projet
        var existing = await _db.AchatItem
            .Where(a => a.ProjectId == projectId)
            .ToListAsync(ct);

        foreach (var it in list)
        {
            it.ProjectId = projectId; // sécurise le ProjectId

            if (it.Id == 0)
            {
                // INSERT : uniquement les colonnes persistées
                await _db.AchatItem.AddAsync(new AchatItem
                {
                    ProjectId = projectId,
                    Designation = it.Designation,
                    PurchaseType = it.PurchaseType,
                    UnitCostHt = it.UnitCostHt,
                    Quantity = it.Quantity,
                    UnitResaleHt = it.UnitResaleHt,
                    Comment = it.Comment
                }, ct);
            }
            else
            {
                // UPDATE
                var row = existing.FirstOrDefault(x => x.Id == it.Id);
                if (row is null)
                {
                    // Sécurité : si l'Id n'existe plus en base, on réinsère
                    await _db.AchatItem.AddAsync(new AchatItem
                    {
                        ProjectId = projectId,
                        Designation = it.Designation,
                        PurchaseType = it.PurchaseType,
                        UnitCostHt = it.UnitCostHt,
                        Quantity = it.Quantity,
                        UnitResaleHt = it.UnitResaleHt,
                        Comment = it.Comment
                    }, ct);
                }
                else
                {
                    // MAJ : ne JAMAIS toucher aux propriétés calculées (readonly)
                    row.ProjectId = projectId;
                    row.Designation = it.Designation;
                    row.PurchaseType = it.PurchaseType;
                    row.UnitCostHt = it.UnitCostHt;
                    row.Quantity = it.Quantity;
                    row.UnitResaleHt = it.UnitResaleHt;
                    row.Comment = it.Comment;

                    _db.AchatItem.Update(row);
                }
            }
        }

        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(int achatItemId, CancellationToken ct = default)
    {
        var row = await _db.AchatItem.FirstOrDefaultAsync(x => x.Id == achatItemId, ct);
        if (row is null) return;
        _db.AchatItem.Remove(row);
        await _db.SaveChangesAsync(ct);
    }

    
}
