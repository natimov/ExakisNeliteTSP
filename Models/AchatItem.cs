using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExakisNeliteTSP.Models
{
    // Ligne d'achat Matériel / Logiciel rattachée à un projet
    [Index(nameof(ProjectId), nameof(Designation))]
    [Table("AchatItem")]
    public class AchatItem
    {
        public int Id { get; set; }

        // Projet propriétaire
        public Guid ProjectId { get; set; }

        // Col.1 : "Achat Matériel / Logiciel" (désignation)
        [Required, MaxLength(128)]
        public string Designation { get; set; } = string.Empty;

        // Col.2 : "Type d'achat" (libre : ex. Logiciel, Matériel, Abonnement…)
        [MaxLength(64)]
        public PurchaseType PurchaseType { get; set; }

        // Col.3 : Coût unitaire achat (HT)
        [Precision(18, 2)]
        public decimal? UnitCostHt { get; set; }

        // Col.4 : Qté
        [Precision(18, 2)]
        public decimal? Quantity { get; set; }

        // Col.5 : Montant HT achat (si tu veux le stocker — sinon on le calcule dans l’UI)
        [NotMapped]
        public decimal? AmountPurchaseHt { get; set; }

        // Col.6 : Coût unitaire de revente (HT)
        [Precision(18, 2)]
        public decimal? UnitResaleHt { get; set; }

        // Col.7 : Montant revente (HT)
        [NotMapped]
        public decimal? AmountResaleHt { get; set; }

        // Col.8 : Markup (%)  ex: (MontantRevente - MontantAchat) / MontantAchat
        [NotMapped]
        public decimal? MarkupPercent =>
    (AmountResaleHt ?? 0m) > 0m
        ? (AmountResaleHt - AmountPurchaseHt) / AmountResaleHt   // même formule que l’Excel
        : null;

        // Col.9 : Commentaire libre
        [MaxLength(512)]
        public string? Comment { get; set; }

    }
}
