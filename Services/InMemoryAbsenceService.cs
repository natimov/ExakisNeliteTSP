using ExakisNeliteTSP.Services.Interface;
using System.Collections.Concurrent;

namespace ExakisNeliteTSP.Services;

public sealed class InMemoryAbsenceService : IAbsenceService
{
    // Clé = date, Valeur = liste de noms absents ce jour-là.
    private readonly ConcurrentDictionary<DateOnly, string[]> _absences = new();

    public Task<IReadOnlyList<string>> GetAbsentConsultantsAsync(DateOnly date)
    {
        if (_absences.TryGetValue(date, out var list))
            return Task.FromResult((IReadOnlyList<string>)list);

        // Aucun absent par défaut (tu remplaceras par Talentia plus tard)
        return Task.FromResult((IReadOnlyList<string>)Array.Empty<string>());
    }
}
