using ExakisNeliteTSP.Models;

namespace ExakisNeliteTSP.Services;

public sealed class InMemoryProjectService : IProjectService
{
    private readonly List<Project> _projects = new()
{
    new Project
    {
        Title        = "Refonte portail Extranet",
        Client       = "Total Energie",
        Status       = ProjectStatus.InProgress,
        Complexity   = Complexity.Complex,
        ProjectLead  = "Jean-Jean Doe",
        StartDate    = new DateTime(2025, 06, 02),
        DueDate      = new DateTime(2025, 09, 30),
        LastUpdated  = DateTime.UtcNow.AddDays(-1),
        Consultants  = new() { "Dupont", "Li" }
    },
    new Project
    {
        Title        = "Modernisation Intranet",
        Client       = "Contoso",
        Status       = ProjectStatus.InReview,
        Complexity   = Complexity.Medium,
        ProjectLead  = "Jean-Jules Jux",
        StartDate    = new DateTime(2025, 05, 12),
        DueDate      = new DateTime(2025, 08, 15),
        LastUpdated  = DateTime.UtcNow.AddDays(-2),
        Consultants  = new() { "Tran", "Martin" }
    },
    new Project
    {
        Title        = "Migration vers Azure",
        Client       = "Northwind",
        Status       = ProjectStatus.Done,
        Complexity   = Complexity.Simple,
        ProjectLead  = "Jean-Tom Oppenheimer",
        StartDate    = new DateTime(2025, 03, 01),
        DueDate      = new DateTime(2025, 05, 31),
        LastUpdated  = DateTime.UtcNow.AddDays(-5),
        Consultants  = new() { "Leroy" }
    },
    new Project
    {
        Title        = "Migration vers Azure",
        Client       = "Northwind",
        Status       = ProjectStatus.Done,
        Complexity   = Complexity.Simple,
        ProjectLead  = "Myriam HGF",
        StartDate    = new DateTime(2025, 03, 01),
        DueDate      = new DateTime(2025, 05, 31),
        LastUpdated  = DateTime.UtcNow.AddDays(-5),
        Consultants  = new() { "Leroy" }
    },
    new Project
    {
        Title        = "App Agent Conversationnel",
        Client       = "Apple",
        Status       = ProjectStatus.ValidationPending,
        Complexity   = Complexity.Simple,
        ProjectLead  = "Brandon Hui",
        StartDate    = new DateTime(2025, 03, 01),
        DueDate      = new DateTime(2025, 05, 31),
        LastUpdated  = DateTime.UtcNow.AddDays(-5),
        Consultants  = new() { "Leroy" }
    },
    new Project
    {
        Title        = "App Web CV blazor",
        Client       = "Exakis Nelite",
        Status       = ProjectStatus.Draft,
        Complexity   = Complexity.Simple,
        ProjectLead  = "Tim JJ Nenin. A",
        StartDate    = new DateTime(2025, 03, 01),
        DueDate      = new DateTime(2025, 05, 31),
        LastUpdated  = DateTime.UtcNow.AddDays(-5),
        Consultants  = new() { "Leroy" }
    },
    new Project
    {
        Title        = "App Web CV blazor",
        Client       = "Exakis Nelite",
        Status       = ProjectStatus.Draft,
        Complexity   = Complexity.Simple,
        ProjectLead  = "Tim JJ Nenin. A",
        StartDate    = new DateTime(2025, 03, 01),
        DueDate      = new DateTime(2025, 05, 31),
        LastUpdated  = DateTime.UtcNow.AddDays(-5),
        Consultants  = new() { "Leroy" }
    },

    new Project
    {
        Title        = "Refonte portail Extranet",
        Client       = "Total Energie",
        Status       = ProjectStatus.InProgress,
        Complexity   = Complexity.Complex,
        ProjectLead  = "Jean-Jean Doe",
        StartDate    = new DateTime(2025, 06, 02),
        DueDate      = new DateTime(2025, 09, 30),
        LastUpdated  = DateTime.UtcNow.AddDays(-1),
        Consultants  = new() { "Dupont", "Li" }
    },

    new Project
    {
        Title        = "Refonte portail Extranet",
        Client       = "Total Energie",
        Status       = ProjectStatus.InProgress,
        Complexity   = Complexity.Complex,
        ProjectLead  = "Jean-Jean Doe",
        StartDate    = new DateTime(2025, 06, 02),
        DueDate      = new DateTime(2025, 09, 30),
        LastUpdated  = DateTime.UtcNow.AddDays(-1),
        Consultants  = new() { "Dupont", "Li" }
    },

    new Project
    {
        Title        = "Refonte portail Extranet",
        Client       = "Total Energie",
        Status       = ProjectStatus.InProgress,
        Complexity   = Complexity.Complex,
        ProjectLead  = "Jean-Jean Doe",
        StartDate    = new DateTime(2025, 06, 02),
        DueDate      = new DateTime(2025, 09, 30),
        LastUpdated  = DateTime.UtcNow.AddDays(-1),
        Consultants  = new() { "Dupont", "Li" }
    },

    new Project
    {
        Title        = "Refonte portail Extranet",
        Client       = "Total Energie",
        Status       = ProjectStatus.InProgress,
        Complexity   = Complexity.Complex,
        ProjectLead  = "Jean-Jean Doe",
        StartDate    = new DateTime(2025, 06, 02),
        DueDate      = new DateTime(2025, 09, 30),
        LastUpdated  = DateTime.UtcNow.AddDays(-1),
        Consultants  = new() { "Dupont", "Li" }
    },

};


    public Task<List<Project>> GetAllAsync()
        => Task.FromResult(_projects.OrderByDescending(p => p.LastUpdated).ToList());

    public Task<Project?> GetByIdAsync(Guid id)
        => Task.FromResult(_projects.FirstOrDefault(p => p.Id == id));

    public Task<Project> CreateAsync(Project p)
    {
        p.LastUpdated = DateTime.UtcNow;
        _projects.Add(p);
        return Task.FromResult(p);
    }

    public Task UpdateAsync(Project p)
    {
        var i = _projects.FindIndex(x => x.Id == p.Id);
        if (i >= 0) _projects[i] = p;
        return Task.CompletedTask;
    }

    public Task<int> CountByStatusAsync(ProjectStatus s)
        => Task.FromResult(_projects.Count(p => p.Status == s));

    public Task<int> CountActiveConsultantsAsync()
        => Task.FromResult(_projects.SelectMany(p => p.Consultants).Distinct().Count());
}
