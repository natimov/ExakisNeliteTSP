using ExakisNeliteTSP.Services.Interface;

namespace ExakisNeliteTSP.Services;

public sealed class InMemoryConsultantDirectoryService : IConsultantDirectoryService
{
    // Mets ici la liste de tous les consultants (RH / tenant interne)
    // Tu peux commencer avec quelques noms de test.
    private static readonly string[] All =
    {
        "AS", "BQ", "CR", "DN", "EQ", "FA", "GZ"
    };

    public Task<IReadOnlyList<string>> GetAllConsultantsAsync()
        => Task.FromResult((IReadOnlyList<string>)All);
}
