using System;
using System.Collections.Generic;

namespace ExakisNeliteTSP.Models
{
    public enum EntiteType { FO, BO, BO_XP, CESSION, ACHAT, ACHAT_BO }
    public enum ZoneTarif { Paris, Region }

    /// <summary>
    /// Une ligne du tableau PROFIL (A→P) de l'Excel.
    /// Les champs "calculés" seront remplis par le service de calcul, pas saisis à la main.
    /// </summary>
    public class ProfilEntry
    {
        // --- Saisie ---
        public string Profil { get; set; } = "";                 // B
        public EntiteType Entite { get; set; }                  // C
        public decimal? Tjm { get; set; }                       // D (€/j)
        public decimal? Cjm { get; set; }                       // E (€/j)
        public decimal? Prcs { get; set; }                      // F (€/j) (BO=440; BO_XP/ACHAT_BO=0.8*TJM; autres: laissé)
        public decimal? FraisDeplacementSiCession { get; set; } // G (€)
        public decimal? TciOverPrcs { get; set; }               // H (€/j) (calculé par logique d'entité)
        public decimal? ChargeJh { get; set; }                  // I (jh)

        // --- Calculs (affichage only) ---
        public decimal? CaEuro { get; set; }                    // J = Charge * TJM * (1 - Remise)
        public decimal? CoutExakisNeliteEuro { get; set; }      // K = CJM * Charge
        public decimal? CoutAgenceEuro { get; set; }            // L = (FO ? CJM : TCI/PRCS) * Charge
        public decimal? PercentJhProfil { get; set; }           // M = I / ΣI
        public decimal? PercentEuroProfil { get; set; }         // N = J / ΣJ
        public decimal? MargeProfilExa { get; set; }            // O = (J - K) / J
        public decimal? MargeProfilAgence { get; set; }         // P = (J - L) / J

        // --- Affichage utile ---
        public decimal? TjmRemises { get; set; }                // A (TJM après remise)
                                                                // --- Planning (UI only : ventilation mensuelle, non persistée en BD pour l'instant) ---
        public int PlanningYear { get; set; } = DateTime.UtcNow.Year;
        public decimal?[] Months { get; set; } = new decimal?[12]; // index 0=Jan ... 11=Déc
        public int? DbRowId { get; set; } // Id de ProjectProfilRow en BD (FK côté ventilation)


    }

    /// <summary>
    /// Table de référence Paris/Région (bas de l'onglet PROFIL).
    /// Sert à pré-remplir TJM/CJM selon la zone.
    /// </summary>
    public class ProfilTarifRef
    {
        public string Profil { get; set; } = "";  // libellé de la ligne de ref
        public decimal? TjmParis { get; set; }
        public decimal? CjmParis { get; set; }
        public decimal? TjmRegion { get; set; }
        public decimal? CjmRegion { get; set; }
    }
}
