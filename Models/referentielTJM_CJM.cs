// Fichier : Models/Tjm.cs
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ExakisNeliteTSP.Models;

/// Référentiel GLOBAL (valeurs par défaut communes à tous)
[Index(nameof(Category), nameof(Profile), nameof(Location), IsUnique = true)]
public class ReferentielTjmItem
{
    public int Id { get; set; }

    [Required, MaxLength(64)]
    public string Category { get; set; } = null!;   // "Agences", "CES Log", "CES Infra"

    [Required, MaxLength(128)]
    public string Profile { get; set; } = null!;    // "Consultant Junior", "Architecte", ...

    [Required, MaxLength(64)]
    public string Location { get; set; } = null!;   // "Paris", "Province", "Région", "CES"

    [Precision(18, 2)]
    public decimal Tjm { get; set; }                // €/jour
}

/// Copie PAR PROJET (pré-remplie depuis le référentiel, modifiable)
[Index(nameof(ProjectId), nameof(Category), nameof(Profile), nameof(Location), IsUnique = true)]
public class ProjectTjmItem
{
    public int Id { get; set; }
    public Guid ProjectId { get; set; }

    [Required, MaxLength(64)]
    public string Category { get; set; } = null!;

    [Required, MaxLength(128)]
    public string Profile { get; set; } = null!;

    [Required, MaxLength(64)]
    public string Location { get; set; } = null!;

    [Precision(18, 2)]
    public decimal Tjm { get; set; }
}
