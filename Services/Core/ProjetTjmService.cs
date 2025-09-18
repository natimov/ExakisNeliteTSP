using ExakisNeliteTSP.Data;
using ExakisNeliteTSP.Models;
using ExakisNeliteTSP.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExakisNeliteTSP.Services;

public class ProjectTjmService : IProjectTjmService
{
    private readonly TspDbContext _db; // ajuste le nom si ton DbContext s'appelle autrement
    public ProjectTjmService(TspDbContext db) => _db = db;

    public async Task EnsureInitializedAsync(Guid projectId, CancellationToken ct = default)
    {
        // Si le projet a déjà des lignes, on ne fait rien
        if (await _db.ProjectTjmItems.AnyAsync(x => x.ProjectId == projectId, ct)) return;

        // Copie du référentiel global vers le projet
        var refsAll = await _db.ReferentielTjmItems.AsNoTracking().ToListAsync(ct);
        var clones = refsAll.Select(r => new ProjectTjmItem
        {
            ProjectId = projectId,
            Category = r.Category,
            Profile = r.Profile,
            Location = r.Location,
            Tjm = r.Tjm
        });

        await _db.ProjectTjmItems.AddRangeAsync(clones, ct);
        await _db.SaveChangesAsync(ct);
    }

    public Task<List<ProjectTjmItem>> LoadAsync(Guid projectId, CancellationToken ct = default) =>
        _db.ProjectTjmItems
           .Where(x => x.ProjectId == projectId)
           .OrderBy(x => x.Category).ThenBy(x => x.Profile).ThenBy(x => x.Location)
           .ToListAsync(ct);

    public async Task SaveAsync(Guid projectId, IEnumerable<ProjectTjmItem> items, CancellationToken ct = default)
    {
        var list = items.ToList();
        var existing = await _db.ProjectTjmItems.Where(x => x.ProjectId == projectId).ToListAsync(ct);

        // Upsert par (ProjectId, Category, Profile, Location)
        foreach (var item in list)
        {
            var row = existing.FirstOrDefault(x =>
                x.Category == item.Category && x.Profile == item.Profile && x.Location == item.Location);

            if (row is null)
            {
                item.Id = 0;
                item.ProjectId = projectId;
                _db.ProjectTjmItems.Add(item);
            }
            else
            {
                row.Tjm = item.Tjm;
                _db.ProjectTjmItems.Update(row);
            }
        }
        await _db.SaveChangesAsync(ct);
    }
}
