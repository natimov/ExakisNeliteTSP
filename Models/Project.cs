namespace ExakisNeliteTSP.Models;

public enum ProjectStatus { Draft, InReview, ValidationPending, InProgress, Done, OnHold }
public enum Complexity { Simple, Medium, Complex }

public sealed class Project
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Title { get; set; } = "";
    public string? Description { get; set; }
    public string? Client { get; set; }
    public ProjectStatus Status { get; set; } = ProjectStatus.Draft;
    public Complexity Complexity { get; set; } = Complexity.Simple;
    public string? ArchitectCds { get; set; }
    public string? ArchitectInternal { get; set; }
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    public List<string> Consultants { get; set; } = new();
    public string? ProjectLead { get; set; }   // Chef de projet
    public DateTime? StartDate { get; set; }   // Date de début (JJ/MM/AAAA)
    public DateTime? DueDate { get; set; }   // Date de fin prévue (JJ/MM/AAAA)


}
