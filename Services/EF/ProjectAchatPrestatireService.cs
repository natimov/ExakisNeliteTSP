using ExakisNeliteTSP.Data;
using ExakisNeliteTSP.Models;
using ExakisNeliteTSP.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExakisNeliteTSP.Services.EF
{
    public sealed class ProjectAchatPrestataireService : IProjectAchatPrestataireService
    {
        private readonly TspDbContext _db;
        public ProjectAchatPrestataireService(TspDbContext db) => _db = db;

        public async Task<List<AchatPrestataireItem>> LoadAsync(Guid projectId, CancellationToken ct = default)
            => await _db.AchatPrestataireItem
                        .Where(x => x.ProjectId == projectId)
                        .OrderBy(x => x.Societe)
                        .AsNoTracking()
                        .ToListAsync(ct);

        public async Task AddAsync(Guid projectId, AchatPrestataireItem item, CancellationToken ct = default)
        {
            item.Id = 0;
            item.ProjectId = projectId;

            await _db.AchatPrestataireItem.AddAsync(new AchatPrestataireItem
            {
                ProjectId = projectId,
                Societe = item.Societe,
                CoutJour = item.CoutJour,
                NbJours = item.NbJours,
                Tjm = item.Tjm,
                Commentaire = item.Commentaire
            }, ct);

            await _db.SaveChangesAsync(ct);
        }

        public async Task SaveAsync(Guid projectId, IEnumerable<AchatPrestataireItem> items, CancellationToken ct = default)
        {
            var list = (items ?? Enumerable.Empty<AchatPrestataireItem>()).ToList();
            var existing = await _db.AchatPrestataireItem
                                    .Where(a => a.ProjectId == projectId)
                                    .ToListAsync(ct);

            foreach (var it in list)
            {
                it.ProjectId = projectId;

                if (it.Id == 0)
                {
                    await _db.AchatPrestataireItem.AddAsync(new AchatPrestataireItem
                    {
                        ProjectId = projectId,
                        Societe = it.Societe,
                        Prestation = it.Prestation,
                        CoutJour = it.CoutJour,
                        NbJours = it.NbJours,
                        Tjm = it.Tjm,
                        Commentaire = it.Commentaire
                    }, ct);
                }
                else
                {
                    var row = existing.FirstOrDefault(x => x.Id == it.Id);
                    if (row is null)
                    {
                        await _db.AchatPrestataireItem.AddAsync(new AchatPrestataireItem
                        {
                            ProjectId = projectId,
                            Societe = it.Societe,
                            Prestation = it.Prestation,
                            CoutJour = it.CoutJour,
                            NbJours = it.NbJours,
                            Tjm = it.Tjm,
                            Commentaire = it.Commentaire
                        }, ct);
                    }
                    else
                    {
                        row.ProjectId = projectId;
                        row.Societe = it.Societe;
                        row.Prestation = it.Prestation;     // <<< NOUVEAU

                        row.CoutJour = it.CoutJour;
                        row.NbJours = it.NbJours;
                        row.Tjm = it.Tjm;
                        row.Commentaire = it.Commentaire;

                        _db.AchatPrestataireItem.Update(row);
                    }
                }
            }

            await _db.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var row = await _db.AchatPrestataireItem.FirstOrDefaultAsync(x => x.Id == id, ct);
            if (row is null) return;

            _db.AchatPrestataireItem.Remove(row);
            await _db.SaveChangesAsync(ct);
        }
    }
}
