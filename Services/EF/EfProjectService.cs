// Services/EfProjectService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExakisNeliteTSP.Data;
using ExakisNeliteTSP.Models;
using ExakisNeliteTSP.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExakisNeliteTSP.Services.EF
{
    public sealed class EfProjectService : IProjectService
    {
        private readonly TspDbContext _db;
        public EfProjectService(TspDbContext db) => _db = db;

        // --- Helpers ---
        private static List<string> EmptyListIfNull(List<string>? list)
            => list is null ? new List<string>() : list;

        private async Task HydrateConsultantsAsync(Project p)
        {
            var names = await _db.ProjectConsultants
                                 .Where(pc => pc.ProjectId == p.Id)
                                 .Select(pc => pc.Name)
                                 .ToListAsync();
            p.Consultants = names;
        }

        private async Task ReplaceConsultantsAsync(Project p)
        {
            // Remplace intégralement les consultants liés au projet (stratégie simple & sûre)
            var existing = await _db.ProjectConsultants
                                    .Where(pc => pc.ProjectId == p.Id)
                                    .ToListAsync();
            _db.ProjectConsultants.RemoveRange(existing);

            var names = EmptyListIfNull(p.Consultants)
                        .Where(n => !string.IsNullOrWhiteSpace(n))
                        .Select(n => n.Trim())
                        .Distinct(StringComparer.OrdinalIgnoreCase);

            foreach (var name in names)
                _db.ProjectConsultants.Add(new ProjectConsultant { ProjectId = p.Id, Name = name });
        }

        // --- IProjectService ---

        public async Task<List<Project>> GetAllAsync()
        {
            var list = await _db.Projects.AsNoTracking()
                                         .OrderByDescending(p => p.LastUpdated)
                                         .ToListAsync();

            // batch-load consultants pour éviter N requêtes
            var ids = list.Select(p => p.Id).ToList();
            var map = await _db.ProjectConsultants
                               .Where(pc => ids.Contains(pc.ProjectId))
                               .GroupBy(pc => pc.ProjectId)
                               .ToDictionaryAsync(g => g.Key, g => g.Select(x => x.Name).Distinct().ToList());

            foreach (var p in list)
                p.Consultants = map.TryGetValue(p.Id, out var names) ? names : new List<string>();

            return list;
        }

        public async Task<Project?> GetByIdAsync(Guid id)
        {
            var p = await _db.Projects.FindAsync(id);
            if (p is null) return null;
            await HydrateConsultantsAsync(p);
            return p;
        }

        public async Task<Project> CreateAsync(Project p)
        {
            // NE PAS toucher à p.Id (init-only; déjà Guid.NewGuid() par défaut dans Project)
            p.LastUpdated = DateTime.UtcNow;

            var consultants = p.Consultants ?? new List<string>();
            p.Consultants = new List<string>(); // on laisse EF traquer seulement les scalaires

            _db.Projects.Add(p);
            await _db.SaveChangesAsync();

            if (consultants.Count > 0)
            {
                foreach (var name in consultants.Where(n => !string.IsNullOrWhiteSpace(n))
                                                .Select(n => n.Trim())
                                                .Distinct(StringComparer.OrdinalIgnoreCase))
                {
                    _db.ProjectConsultants.Add(new ProjectConsultant
                    {
                        ProjectId = p.Id,
                        Name = name
                    });
                }
                await _db.SaveChangesAsync();
            }

            p.Consultants = consultants; // cohérence pour l’appelant
            return p;
        }

        public async Task UpdateAsync(Project p)
        {
            p.LastUpdated = DateTime.UtcNow;

            // détache les collections non mappées ; EF suit uniquement les scalaires du Project
            var consultants = EmptyListIfNull(p.Consultants);
            p.Consultants = new List<string>();

            _db.Projects.Update(p);
            await _db.SaveChangesAsync();

            await ReplaceConsultantsAsync(p);
            await _db.SaveChangesAsync();

            p.Consultants = consultants; // cohérence pour l’appelant
        }

        public Task<int> CountByStatusAsync(ProjectStatus status)
            => _db.Projects.CountAsync(pr => pr.Status == status);

        public Task<int> CountActiveConsultantsAsync()
            => _db.ProjectConsultants
                  .Select(pc => pc.Name)
                  .Distinct()
                  .CountAsync();
    }
}
