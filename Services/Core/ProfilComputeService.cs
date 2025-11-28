using System;
using System.Linq;
using ExakisNeliteTSP.Models;

namespace ExakisNeliteTSP.Services.Core
{
    public static class ProfilComputeService
    {
        // --- Helpers ---
        private static decimal Ratio(decimal? percent) => percent.HasValue ? percent.Value / 100m : 0m;
        private static decimal Safe(decimal? v) => v ?? 0m;
        private static decimal Div(decimal num, decimal den) => den == 0m ? 0m : num / den;




        /// <summary>
        /// Reproduit les formules Excel pour une ligne de PROFIL.
        /// N'altère pas les champs "saisie" (Profil, Entite, TJM, CJM, Charge, Frais...) ; calcule le reste.
        /// </summary>
        /// 
        public static void ComputeRow(Project project, ProfilEntry r)
        {
            if (project is null || r is null) return;

            var remise = Ratio(project.RemiseTjmPercent); // ex: 10% => 0.10
            var tjm = Safe(r.Tjm);
            var cjm = Safe(r.Cjm);
            var chg = Safe(r.ChargeJh);
            var frais = Safe(r.FraisDeplacementSiCession);

            // --- PRCS (col F) ---
            // BO => 440
            // BO_XP ou ACHAT_BO => 0.8 * TJM
            // autres entités : on ne force pas (on conserve ce qui a été saisi/laisser vide)
            if (r.Entite == EntiteType.BO)
                r.Prcs = 440m;
            else if (r.Entite == EntiteType.BO_XP || r.Entite == EntiteType.ACHAT_BO)
                r.Prcs = tjm * 0.8m;
            // FO / CESSION / ACHAT : r.Prcs reste tel quel

            var prcs = Safe(r.Prcs);

            // --- TCI/PRCS (col H) ---
            // FO      -> CJM
            // CESSION -> ((TJM - CJM)/2 + CJM) + (frais/2)/Charge
            // ACHAT   -> CJM
            // BO / BO_XP / ACHAT_BO -> PRCS + (frais / Charge)
            decimal tci;
            if (r.Entite == EntiteType.CESSION)
            {
                var part = (tjm - cjm) / 2m + cjm;
                var add = chg != 0m ? frais / 2m / chg : 0m;
                tci = part + add;
            }
            else if (r.Entite == EntiteType.ACHAT)
            {
                tci = cjm;
            }
            else if (r.Entite == EntiteType.BO || r.Entite == EntiteType.BO_XP || r.Entite == EntiteType.ACHAT_BO)
            {
                var add = chg != 0m ? frais / chg : 0m;
                tci = prcs + add;
            }
            else // FO
            {
                tci = cjm;
            }
            r.TciOverPrcs = tci;

            // --- TJM remisés (affichage, col A dans ton mapping) ---
            r.TjmRemises = tjm * (1 - remise);

            // --- CA (€) (col J) = I * D * (1 - remise) ---
            r.CaEuro = chg * tjm * (1 - remise);

            // --- COUT Exakis Nelite (€) (col K) = CJM * Charge ---
            r.CoutExakisNeliteEuro = cjm * chg;

            // --- COUT Agence (€) (col L) = (FO ? CJM : TCI/PRCS) * Charge ---
            r.CoutAgenceEuro = (r.Entite == EntiteType.FO ? cjm : tci) * chg;
        }

        /// <summary>
        /// Calcule la totalité des lignes (CA, coûts...) puis applique % et marges (nécessitent les totaux).
        /// </summary>
        public static void ComputeAll(Project project)
        {
            if (project is null || project.Profils is null) return;

            // 1) Calculs par ligne
            foreach (var r in project.Profils)
                ComputeRow(project, r);

            // 2) Totaux pour % et marges
            var totalJh = project.Profils.Sum(x => Safe(x.ChargeJh));
            var totalCa = project.Profils.Sum(x => Safe(x.CaEuro));

            // 3) % et marges
            foreach (var r in project.Profils)
            {
                var chg = Safe(r.ChargeJh);
                var ca = Safe(r.CaEuro);
                var k = Safe(r.CoutExakisNeliteEuro);
                var l = Safe(r.CoutAgenceEuro);

                r.PercentJhProfil = totalJh == 0m ? null : Div(chg, totalJh);
                r.PercentEuroProfil = totalCa == 0m ? null : Div(ca, totalCa);

                r.MargeProfilExa = ca == 0m ? null : Div(ca - k, ca);
                r.MargeProfilAgence = ca == 0m ? null : Div(ca - l, ca);
            }
        }

        /// <summary>
        /// Pré-remplit TJM/CJM d'une ligne à partir de la grille Paris/Région (bas de l'onglet).
        /// </summary>
        public static void ApplyTarif(Project project, ProfilEntry row, ZoneTarif zone)
        {
            if (project is null || row is null) return;
            if (project.ProfilTarifs is null || project.ProfilTarifs.Count == 0) return;

            var tref = project.ProfilTarifs.FirstOrDefault(x => string.Equals(x.Profil, row.Profil, StringComparison.OrdinalIgnoreCase));
            if (tref is null) return;

            if (zone == ZoneTarif.Paris)
            {
                row.Tjm = tref.TjmParis;
                row.Cjm = tref.CjmParis;
            }
            else
            {
                row.Tjm = tref.TjmRegion;
                row.Cjm = tref.CjmRegion;
            }
        }
    }
}
