using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExakisNeliteTSP.Models
{
    public class FraisItem
    {
        public int Id { get; set; }

        // Projet auquel le frais est rattaché
        public Guid ProjectId { get; set; }

        // Col.1 : Libellé frais
        public string Libelle { get; set; } = string.Empty;

        // Col.2 : Coût unitaire
        public decimal? CoutUnitaire { get; set; }

        // Col.3 : Quantité
        public decimal? Quantite { get; set; }

        // Col.4 : Montant = CoutUnitaire * Quantite
        [NotMapped]
        public decimal? Montant => (CoutUnitaire ?? 0m) * (Quantite ?? 0m);

        // Col.5 : Montant refacturé (saisi ou calculé selon ta logique)
        public decimal? MontantRefacture { get; set; }

        // Col.6 : Commentaire
        public string? Commentaire { get; set; }
    }
}
