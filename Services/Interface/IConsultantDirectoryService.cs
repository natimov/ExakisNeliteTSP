namespace ExakisNeliteTSP.Services.Interface;

public interface IConsultantDirectoryService
{
    Task<IReadOnlyList<string>> GetAllConsultantsAsync();
}
