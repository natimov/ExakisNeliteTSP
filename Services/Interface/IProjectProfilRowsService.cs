using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ExakisNeliteTSP.Models;

namespace ExakisNeliteTSP.Services.Interface
{
    public interface IProjectProfilRowsService
    {
        Task<List<ProfilEntry>> LoadProfilEntriesAsync(Guid projectId, CancellationToken ct = default);
        Task SaveProfilEntriesAsync(Guid projectId, List<ProfilEntry> profils, CancellationToken ct = default);
    }
}
