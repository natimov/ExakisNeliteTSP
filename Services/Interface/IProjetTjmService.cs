using ExakisNeliteTSP.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ExakisNeliteTSP.Services.Interface
{
    public interface IProjectTjmService
    {
        /// <summary>
        /// Si le projet n’a pas encore ses lignes TJM, copie le référentiel global
        /// dans la table ProjectTjmItems pour ce ProjectId.
        /// </summary>
        Task EnsureInitializedAsync(Guid projectId, CancellationToken ct = default);

        /// <summary>
        /// Charge les lignes TJM du projet (ProjectTjmItems).
        /// </summary>
        Task<List<ProjectTjmItem>> LoadAsync(Guid projectId, CancellationToken ct = default);

        /// <summary>
        /// Sauvegarde/Met à jour les lignes TJM du projet (upsert par Category+Profile+Location).
        /// </summary>
        Task SaveAsync(Guid projectId, IEnumerable<ProjectTjmItem> items, CancellationToken ct = default);
    }
}
