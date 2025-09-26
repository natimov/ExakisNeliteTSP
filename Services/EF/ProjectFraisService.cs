using ExakisNeliteTSP.Data;
using ExakisNeliteTSP.Models;
using ExakisNeliteTSP.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExakisNeliteTSP.Services.EF
{
    public sealed class ProjectFraisService : IProjectFraisService
    {
        private readonly TspDbContext _db;
        public ProjectFraisService(TspDbContext db) => _db = db;

        public async Task<List<FraisItem>> LoadAsync(Guid projectId, CancellationToken ct = default)
            => await _db.FraisItem
                        .Where(f => f.ProjectId == projectId)
                        .OrderBy(f => f.Libelle)
                        .AsNoTracking()
                        .ToListAsync(ct);

        public async Task AddAsync(Guid projectId, FraisItem item, CancellationToken ct = default)
        {
            item.Id = 0;
            item.ProjectId = projectId;
            await _db.FraisItem.AddAsync(new FraisItem
            {
                ProjectId = projectId,
                Libelle = item.Libelle,
                CoutUnitaire = item.CoutUnitaire,
                Quantite = item.Quantite,
                MontantRefacture = item.MontantRefacture,
                Commentaire = item.Commentaire
            }, ct);
            await _db.SaveChangesAsync(ct);
        }

        public async Task SaveAsync(Guid projectId, IEnumerable<FraisItem> items, CancellationToken ct = default)
        {
            var list = (items ?? Enumerable.Empty<FraisItem>()).ToList();
            var existing = await _db.FraisItem
                                    .Where(f => f.ProjectId == projectId)
                                    .ToListAsync(ct);

            foreach (var it in list)
            {
                it.ProjectId = projectId;

                if (it.Id == 0)
                {
                    await _db.FraisItem.AddAsync(new FraisItem
                    {
                        ProjectId = projectId,
                        Libelle = it.Libelle,
                        CoutUnitaire = it.CoutUnitaire,
                        Quantite = it.Quantite,
                        MontantRefacture = it.MontantRefacture,
                        Commentaire = it.Commentaire
                    }, ct);
                }
                else
                {
                    var row = existing.FirstOrDefault(x => x.Id == it.Id);
                    if (row is null)
                    {
                        await _db.FraisItem.AddAsync(new FraisItem
                        {
                            ProjectId = projectId,
                            Libelle = it.Libelle,
                            CoutUnitaire = it.CoutUnitaire,
                            Quantite = it.Quantite,
                            MontantRefacture = it.MontantRefacture,
                            Commentaire = it.Commentaire
                        }, ct);
                    }
                    else
                    {
                        row.Libelle = it.Libelle;
                        row.CoutUnitaire = it.CoutUnitaire;
                        row.Quantite = it.Quantite;
                        row.MontantRefacture = it.MontantRefacture;
                        row.Commentaire = it.Commentaire;
                        _db.FraisItem.Update(row);
                    }
                }
            }

            await _db.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var row = await _db.FraisItem.FirstOrDefaultAsync(x => x.Id == id, ct);
            if (row is null) return;
            _db.FraisItem.Remove(row);
            await _db.SaveChangesAsync(ct);
        }
    }
}
