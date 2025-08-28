namespace ExakisNeliteTSP.Services;

public interface IConsultantDirectoryService
{
    Task<IReadOnlyList<string>> GetAllConsultantsAsync();
}
