using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace ExakisNeliteTSP.Models;

public class WorkloadItem
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid ProjectId { get; set; }
    public Guid? ParentId { get; set; } // null = grande étape
    public int OrderIndex { get; set; }
    [NotMapped]
    public Dictionary<int, decimal> ChargesByProfile { get; set; } = new();


    // Saisie manuelle uniquement
    public string GroupCode { get; set; } = "";
    public string Lot { get; set; } = "";
    public string Phase { get; set; } = "";
    public string Etape { get; set; } = "";
    public string Tache { get; set; } = "";
    public string Livrable { get; set; } = "";
    public string Commentaire { get; set; } = "";

    public ICollection<WorkloadAllocation> Allocations { get; set; } = new List<WorkloadAllocation>();
}
