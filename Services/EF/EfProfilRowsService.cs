using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ExakisNeliteTSP.Data;
using ExakisNeliteTSP.Models;
using ExakisNeliteTSP.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExakisNeliteTSP.Services.EF
{
    public sealed class EfProfilRowsService : IProjectProfilRowsService
    {
        private readonly TspDbContext _db;
        public EfProfilRowsService(TspDbContext db) => _db = db;

        // DB -> UI (ProfilEntry)
        public async Task<List<ProfilEntry>> LoadProfilEntriesAsync(Guid projectId, CancellationToken ct = default)
        {
            var rows = await _db.ProjectProfilRows
                                .Where(r => r.ProjectId == projectId)
                                .OrderBy(r => r.Order)
                                .ToListAsync(ct);

            return rows.Select(r => new ProfilEntry
            {
                DbRowId = r.Id,                             // ← IMPORTANT : ID BD de la ligne profil
                Profil = r.Profil,
                Entite = r.Entite,
                Tjm = r.Tjm,
                Cjm = r.Cjm,
                Prcs = r.Prcs,
                FraisDeplacementSiCession = r.FraisDeplacementSiCession,
                TciOverPrcs = r.TciOverPrcs,
                ChargeJh = r.ChargeJh,

                // buffers UI planning (si pas déjà faits ailleurs)
                PlanningYear = DateTime.UtcNow.Year,
                Months = new decimal?[12]
            }).ToList();

        }

        // UI (ProfilEntry) -> DB
        public async Task SaveProfilEntriesAsync(Guid projectId, List<ProfilEntry> profils, CancellationToken ct = default)
        {
            // 1) On supprime l’existant
            var existing = await _db.ProjectProfilRows
                                    .Where(r => r.ProjectId == projectId)
                                    .ToListAsync(ct);
            _db.ProjectProfilRows.RemoveRange(existing);

            // 2) On prépare les nouvelles entités et on les garde pour récupérer leurs IDs
            var newRows = new List<ProjectProfilRow>(profils.Count);
            int order = 0;
            foreach (var p in profils)
            {
                var entity = new ProjectProfilRow
                {
                    ProjectId = projectId,
                    Order = order++,
                    Profil = p.Profil,
                    Entite = p.Entite,
                    Tjm = p.Tjm,
                    Cjm = p.Cjm,
                    Prcs = p.Prcs,
                    FraisDeplacementSiCession = p.FraisDeplacementSiCession,
                    TciOverPrcs = p.TciOverPrcs,
                    ChargeJh = p.ChargeJh
                };
                newRows.Add(entity);
                _db.ProjectProfilRows.Add(entity);
            }

            // 3) Sauvegarde => EF génère les IDs
            await _db.SaveChangesAsync(ct);

            // 4) Propager les IDs dans la liste en mémoire (même ordre)
            for (int i = 0; i < profils.Count; i++)
            {
                profils[i].DbRowId = newRows[i].Id;   // ← maintenant la page a les bons IDs
            }
        }

    }
}
