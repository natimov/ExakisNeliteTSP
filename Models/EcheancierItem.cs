using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExakisNeliteTSP.Models
{
    public class EcheancierItem
    {
        public int Id { get; set; }
        public Guid ProjectId { get; set; }

        // Libellé du jalon (ex: "Commande", "Livraison", "Fin garantie")
        public string Label { get; set; } = string.Empty;

        // Pourcentage du montant total (0..1) - on saisit 0.40 pour 40 %
        public decimal Percent { get; set; }

        // Mois/Année de facturation prévus
        public int BillingMonth { get; set; }   // 1..12
        public int BillingYear { get; set; }   // ex: 2025

        // Champ libre
        public string? Comment { get; set; }

        // Champs calculés (pas mappés en base)
        [NotMapped] public decimal? AmountFacture { get; set; }      // = Percent * ContractAmount
        [NotMapped] public decimal? CumulFacture { get; set; }
        [NotMapped] public decimal? CoutEngageCumul { get; set; }    // Planning -> cumul jusqu'au mois du jalon
        [NotMapped] public decimal? FAE => (CoutEngageCumul ?? 0m) - (CumulFacture ?? 0m);
    }
}
