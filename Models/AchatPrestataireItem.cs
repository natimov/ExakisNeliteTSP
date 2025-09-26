using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExakisNeliteTSP.Models
{
    [Table("AchatPrestataireItem")]
    [Index(nameof(ProjectId), nameof(Societe))]
    public class AchatPrestataireItem
    {
        public int Id { get; set; }
        public Guid ProjectId { get; set; }

        [Required, MaxLength(128)]
        public string Societe { get; set; } = string.Empty;
        public string? Prestation { get; set; }  


        // Saisies
        [Precision(18, 2)] public decimal? CoutJour { get; set; }
        [Precision(18, 2)] public decimal? NbJours { get; set; }
        [Precision(18, 2)] public decimal? Tjm { get; set; }

        // ---- Calculés à la volée (NON mappés) ----
        [NotMapped]
        public decimal? TotalCout => (CoutJour ?? 0m) * (NbJours ?? 0m);

        [NotMapped]
        public decimal? MontantRevente => (Tjm ?? 0m) * (NbJours ?? 0m);

        [NotMapped]
        public decimal? MarkupPercent =>
            (MontantRevente ?? 0m) > 0m
                ? (MontantRevente - TotalCout) / MontantRevente
                : null;

        [MaxLength(512)]
        public string? Commentaire { get; set; }
    }
}
