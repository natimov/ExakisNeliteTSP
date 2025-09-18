using ExakisNeliteTSP.Data;
using ExakisNeliteTSP.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExakisNeliteTSP.Services.EF
{
    public sealed class EfProjectProfileMonthlyService : IProjectProfileMonthlyService
    {
        private readonly TspDbContext _db;
        public EfProjectProfileMonthlyService(TspDbContext db) => _db = db;

        public async Task<Dictionary<(int ProfilRowId, int Month), decimal?>> LoadAsync(Guid projectId, int year)
        {
            var rows = await _db.ProjectProfileMonthlies
                .Where(x => x.ProjectId == projectId && x.Year == year)
                .ToListAsync();

            var dict = new Dictionary<(int, int), decimal?>(rows.Count);
            foreach (var r in rows)
                dict[(r.ProfilRowId, r.Month)] = r.ChargeJh;
            return dict;
        }

        public async Task SaveAsync(Guid projectId, int year, IEnumerable<(int ProfilRowId, decimal?[] Months)> items)
        {
            // DIAG: vérifier la base cible
            var dbName = _db.Database.GetDbConnection().Database;
            Console.WriteLine($"[MonthlySvc] DB={dbName}, project={projectId}, year={year}");

            var profilIds = items.Select(i => i.ProfilRowId).Distinct().ToList();
            Console.WriteLine($"[MonthlySvc] profilIds= {string.Join(",", profilIds)}");

            var existing = await _db.ProjectProfileMonthlies
                .Where(x => x.ProjectId == projectId && x.Year == year && profilIds.Contains(x.ProfilRowId))
                .ToListAsync();

            var byKey = existing.ToDictionary(x => (x.ProfilRowId, x.Month)); // Month = byte

            int addCount = 0, updCount = 0;
            var toDelete = new List<ProjectProfileMonthly>();

            foreach (var (profilRowId, months) in items)
            {
                for (byte m = 1; m <= 12; m++)
                {
                    var val = months[m - 1];
                    var key = (profilRowId, m);

                    if (byKey.TryGetValue(key, out var row))
                    {
                        // update
                        if (row.ChargeJh != val)
                        {
                            row.ChargeJh = val;
                            updCount++;
                        }
                        // si val == null, on supprime
                        if (val is null) toDelete.Add(row);
                    }
                    else
                    {
                        if (val is not null)
                        {
                            _db.ProjectProfileMonthlies.Add(new ProjectProfileMonthly
                            {
                                ProjectId = projectId,
                                ProfilRowId = profilRowId,
                                Year = year,
                                Month = m,
                                ChargeJh = val
                            });
                            addCount++;
                        }
                    }
                }
            }

            if (toDelete.Count > 0)
                _db.ProjectProfileMonthlies.RemoveRange(toDelete);

            var affected = await _db.SaveChangesAsync();
            Console.WriteLine($"[MonthlySvc] add={addCount}, upd={updCount}, del={toDelete.Count}, saved={affected}");
        }


    }
}
