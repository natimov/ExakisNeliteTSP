using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ExakisNeliteTSP.Data;     // TspDbContext
using ExakisNeliteTSP.Models;   // WorkloadItem, WorkloadAllocation

public sealed class EFWorkloadService : IWorkloadService
{
    private readonly TspDbContext _db;
    public EFWorkloadService(TspDbContext db) => _db = db;

    // Hydrate le dico UI à partir des allocations EF
    private static void HydrateCharges(IEnumerable<WorkloadItem> items)
    {
        foreach (var e in items)
            e.ChargesByProfile = e.Allocations.ToDictionary(a => a.ProfileId, a => a.JoursHomme);
    }

    public async Task<List<WorkloadItem>> LoadAsync(Guid projectId, CancellationToken ct = default)
    {
        var items = await _db.WorkloadItems
            .Where(x => x.ProjectId == projectId)
            .Include(x => x.Allocations)
            .OrderBy(x => x.OrderIndex)
            .ToListAsync(ct);

        // Hydrate le dico pour l’UI (sinon tes inputs sont vides au reload)
        foreach (var e in items)
            e.ChargesByProfile = e.Allocations.ToDictionary(a => a.ProfileId, a => a.JoursHomme);

        return items;
    }


    public async Task UpdateRowAsync(WorkloadItem row, CancellationToken ct = default)
    {
        // 1) Si la ligne (créée dans l’UI) arrive sans Id, on en génère un
        if (row.Id == Guid.Empty)
            row.Id = Guid.NewGuid();

        // 2) On cherche en base
        var e = await _db.WorkloadItems
            .FirstOrDefaultAsync(x => x.Id == row.Id, ct);

        // 3) INSERT si absent (UPSERT)
        if (e is null)
        {
            if (row.ProjectId == Guid.Empty)
                throw new InvalidOperationException("WorkloadItem.ProjectId est vide (nouvelle ligne).");

            e = new WorkloadItem
            {
                Id = row.Id,        // on conserve l’Id de la UI
                ProjectId = row.ProjectId  // indispensable pour l’insert
            };
            _db.WorkloadItems.Add(e);
        }

        // 4) Copier les champs SAISIS (rien de calculé ici)
        e.ParentId = row.ParentId;
        e.OrderIndex = row.OrderIndex;
        e.GroupCode = row.GroupCode ?? "";
        e.Lot = row.Lot ?? "";
        e.Phase = row.Phase ?? "";
        e.Etape = row.Etape ?? "";
        e.Tache = row.Tache ?? "";
        e.Livrable = row.Livrable ?? "";
        e.Commentaire = row.Commentaire ?? "";

        await _db.SaveChangesAsync(ct);
    }



    public async Task UpdateAllocationAsync(Guid rowId, int profileId, decimal jh, CancellationToken ct = default)
    {
        // Si la ligne n'existe pas encore, on ignore pour éviter le FK error
        var exists = await _db.WorkloadItems.AnyAsync(x => x.Id == rowId, ct);
        if (!exists) return;

        var alloc = await _db.WorkloadAllocations
            .FirstOrDefaultAsync(a => a.WorkloadItemId == rowId && a.ProfileId == profileId, ct);

        if (jh <= 0m)
        {
            if (alloc is not null) _db.WorkloadAllocations.Remove(alloc);
        }
        else
        {
            if (alloc is null)
            {
                alloc = new WorkloadAllocation { Id = Guid.NewGuid(), WorkloadItemId = rowId, ProfileId = profileId };
                _db.WorkloadAllocations.Add(alloc);
            }
            alloc.JoursHomme = jh;
        }

        await _db.SaveChangesAsync(ct);
    }

    public async Task<WorkloadItem> CreateParentAsync(Guid projectId, int orderIndex, CancellationToken ct = default)
    {
        var e = new WorkloadItem
        {
            Id = Guid.NewGuid(),
            ProjectId = projectId,
            ParentId = null,       // <- grande étape
            OrderIndex = orderIndex
        };
        _db.WorkloadItems.Add(e);
        await _db.SaveChangesAsync(ct);

        // hydrate les propriétés UI non mappées
        e.Allocations = new List<WorkloadAllocation>();
        e.ChargesByProfile = new Dictionary<int, decimal>();
        return e;
    }
    public async Task DeleteRowsAsync(IEnumerable<Guid> rowIds, CancellationToken ct = default)
    {
        var ids = rowIds?.ToHashSet() ?? new HashSet<Guid>();
        if (ids.Count == 0) return;

        // si un parent est dans la liste, on ajoute aussi tous ses enfants
        var childIds = await _db.WorkloadItems
            .Where(x => x.ParentId != null && ids.Contains(x.ParentId.Value))
            .Select(x => x.Id)
            .ToListAsync(ct);

        foreach (var id in childIds) ids.Add(id);

        // 1) on supprime d’abord les allocations liées (JH)
        var allocs = await _db.WorkloadAllocations
            .Where(a => ids.Contains(a.WorkloadItemId))
            .ToListAsync(ct);
        _db.WorkloadAllocations.RemoveRange(allocs);

        // 2) puis on supprime les lignes workload
        var rows = await _db.WorkloadItems
            .Where(x => ids.Contains(x.Id))
            .ToListAsync(ct);
        _db.WorkloadItems.RemoveRange(rows);

        await _db.SaveChangesAsync(ct);
    }

}
