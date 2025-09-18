namespace ExakisNeliteTSP.Services.Interface;

public interface IAbsenceService
{
    /// <summary>
    /// Retourne la liste des consultants absents pour la date donnée.
    /// (Vacances, arrêt maladie, RTT, etc.)
    /// </summary>
    Task<IReadOnlyList<string>> GetAbsentConsultantsAsync(DateOnly date);
}
