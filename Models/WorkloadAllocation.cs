using System;

namespace ExakisNeliteTSP.Models;

public class WorkloadAllocation
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid WorkloadItemId { get; set; } // FK -> WorkloadItem
    public int ProfileId { get; set; }       // correspond à tes profs[i].ProfileId
    public decimal JoursHomme { get; set; }  // 0.5, 1, 2, ...

    public WorkloadItem? WorkloadItem { get; set; }
}
