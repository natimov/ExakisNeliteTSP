// Data/ProjectProfilRow.cs
using System;
using ExakisNeliteTSP.Models;

namespace ExakisNeliteTSP.Data
{
    /// <summary>
    /// Entité EF pour une ligne de l'onglet "Ressources & Charges (Profil)".
    /// On ne stocke que les champs SAISIS (entrées). Les colonnes calculées
    /// resteront calculées à l'affichage via ProfilComputeService.
    /// </summary>
    public class ProjectProfilRow
    {
        public int Id { get; set; }                 // PK (IDENTITY)
        public Guid ProjectId { get; set; }         // FK vers Projects.Id
        public Project Project { get; set; } = null!;

        // --- Champs saisis (entrées) ---
        public string Profil { get; set; } = "";
        public EntiteType Entite { get; set; }      // FO, BO, ...
        public decimal? Tjm { get; set; }
        public decimal? Cjm { get; set; }
        public decimal? Prcs { get; set; }
        public decimal? FraisDeplacementSiCession { get; set; }
        public decimal? TciOverPrcs { get; set; }
        public decimal? ChargeJh { get; set; }

        // Optionnel : pour garder l'ordre d'affichage des lignes dans le tableau
        public int Order { get; set; }
    }
}
