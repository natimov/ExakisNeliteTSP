using ExakisNeliteTSP.Models;

namespace ExakisNeliteTSP.Services.Interface;

public interface IProjectService
{
    Task<List<Project>> GetAllAsync();
    Task<Project?> GetByIdAsync(Guid id);
    Task<Project> CreateAsync(Project p);
    Task UpdateAsync(Project p);
    Task<int> CountByStatusAsync(ProjectStatus status);
    Task<int> CountActiveConsultantsAsync();
}
