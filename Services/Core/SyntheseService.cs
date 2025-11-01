using System;
using System.Collections.Generic;
using System.Linq;

namespace ExakisNeliteTSP.Services
{
    // Adapte ces using/namespace à ton projet.
    // Les types Project, ProfilEntry, AchatItem, FraisItem, WorkloadRow sont supposés exister chez toi.

    public sealed class SyntheseService
    {
        // ---------- Entrées attendues ----------
        // project.Profils : liste des profils avec Tjm, Prcs, Cjm, Entite/Categorie...
        // getPlannedJH(p) : fonction pour récupérer les JH (toutes années) d'un profil.
        // workloadLeafRows : lignes feuilles du Workload (pour provisions si tu veux mapper par Phase).
        // achats, frais : listes déjà chargées.

        // ---------- ViewModel de sortie ----------
        public sealed class SyntheseVm
        {
            // Résumé haut
            public decimal JhTotal { get; init; }
            public decimal CaTotal { get; init; }
            public decimal CoutEnTotal { get; init; }
            public decimal CoutAgenceTotal { get; init; } // somme PRCS

            public decimal TjmClient => JhTotal > 0 ? CaTotal / JhTotal : 0m;
            public decimal CjmProd => JhTotal > 0 ? CoutEnTotal / JhTotal : 0m;
            public decimal MargeBruteAgencePct => CaTotal > 0 ? (CaTotal - CoutAgenceTotal) / CaTotal : 0m;
            public decimal MargeBruteEnPct => CaTotal > 0 ? (CaTotal - CoutEnTotal) / CaTotal : 0m;

            // Frais
            public decimal FraisBudget { get; init; } // si pas de budget pour le moment => 0
            public decimal FraisReels { get; init; }
            public decimal EcartFraisParJh => JhTotal > 0 ? (FraisBudget - FraisReels) / JhTotal : 0m;

            // Provisions
            public decimal ProvisionJh { get; init; }
            public decimal ProvisionCa { get; init; }
            public decimal ProvisionPct => CaTotal > 0 ? ProvisionCa / CaTotal : 0m;

            // Achats
            public decimal AchatsBudget { get; init; }
            public decimal AchatsValorise { get; init; }
            public decimal EcartAchats => AchatsValorise - AchatsBudget;
            public decimal TauxEcartAchats => AchatsValorise != 0m ? (AchatsValorise - AchatsBudget) / AchatsValorise : 0m;

            // Montant projet + Marge nette
            public decimal MontantProjet => CaTotal + AchatsValorise + FraisReels;
            public decimal MargeNetteEn => (CaTotal - CoutEnTotal) + EcartAchats + (FraisReels - FraisBudget);
            public decimal MargeNetteEnPct => MontantProjet > 0 ? MargeNetteEn / MontantProjet : 0m;

            // Détails (FO, CESSION, BACK)
            public CatAgg Fo { get; init; } = new();
            public CatAgg Ces { get; init; } = new();
            public BackAgg Back { get; init; } = new();
            public CatAgg Achat { get; init; } = new();

        }

        public sealed class CatAgg
        {
            public decimal Jh { get; init; }
            public decimal Ca { get; init; }
            public decimal CoutEn { get; init; }
            public decimal Prcs { get; init; }

            public decimal Tjm => Jh > 0 ? Ca / Jh : 0m;
            public decimal Cjm => Jh > 0 ? CoutEn / Jh : 0m;
            public decimal MargeEnPct => Ca > 0 ? (Ca - CoutEn) / Ca : 0m;

            // Pour CESSION uniquement
            public decimal TciPrcsParJh => Jh > 0 ? Prcs / Jh : 0m;
            public decimal MargeAgencePct => Ca > 0 ? (Ca - Prcs) / Ca : 0m;
        }

        public sealed class BackAgg
        {
            public CatAgg Bo { get; init; } = new();
            public CatAgg AchatBo { get; init; } = new();

            public decimal JhTotal => Bo.Jh + AchatBo.Jh;
            public decimal CaTotal => Bo.Ca + AchatBo.Ca;
            public decimal CoutEnTotal => Bo.CoutEn + AchatBo.CoutEn;
            public decimal PrcsTotal => Bo.Prcs + AchatBo.Prcs;

            public decimal Tjm => JhTotal > 0 ? CaTotal / JhTotal : 0m;
            public decimal Cjm => JhTotal > 0 ? CoutEnTotal / JhTotal : 0m;
            public decimal MargeEnPct => CaTotal > 0 ? (CaTotal - CoutEnTotal) / CaTotal : 0m;
            public decimal PrcsMoyenParJh => JhTotal > 0 ? PrcsTotal / JhTotal : 0m;

            // Excel D69 — intitulé ambigu; ici: (TJM BACK – PRCS BACK) / TJM BACK
            public decimal MargeFrontBackPct => Tjm > 0 ? (Tjm - PrcsMoyenParJh) / Tjm : 0m;
        }

        // ---------- Types simplifiés attendus ----------
        public sealed record ProfilEntry(
            string? Categorie, // "FO", "CESSION", "BO", "ACHAT BO" (ou similaire)
            string? Entite,    // si tu utilises EntiteType.CESSION etc., adapte la comparaison
            decimal? Tjm, decimal? Prcs, decimal? Cjm, decimal? FraisDeplacementSiCession
        );

        public sealed record AchatItem(decimal? UnitCostHt, decimal? Quantity, decimal? AmountPurchaseHt,
                                       decimal? UnitResaleHt, decimal? AmountResaleHt);
        public sealed record FraisItem(decimal? Montant /*, ...*/);

        public sealed record WorkloadRow(string? Phase, decimal Ca /*si tu as le CA ligne*/, Dictionary<string, decimal> ChargesByProfile);

        // ---------- API principale ----------
        public SyntheseVm Build(
            IEnumerable<ProfilEntry> profils,
            Func<ProfilEntry, decimal> getPlannedJhAllYears,
            IEnumerable<WorkloadRow> workloadLeafRows,
            IEnumerable<AchatItem> achats,
            IEnumerable<FraisItem> frais,
            bool provisionByPhase = true // map Provision via Phase == "Provision"
        )
        {
            profils ??= Array.Empty<ProfilEntry>();
            workloadLeafRows ??= Array.Empty<WorkloadRow>();
            achats ??= Array.Empty<AchatItem>();
            frais ??= Array.Empty<FraisItem>();

            // 1) Totaux globaux (CA, coûts, PRCS)
            decimal jhTotal = 0m, caTotal = 0m, coutEnTotal = 0m, prcsTotal = 0m;

            foreach (var p in profils)
            {
                var jh = getPlannedJhAllYears(p);
                var tjm = p.Tjm ?? (p.Prcs ?? p.Cjm ?? 0m);
                var coutJour = (p.Prcs ?? p.Cjm ?? 0m) + (IsCession(p) ? (p.FraisDeplacementSiCession ?? 0m) : 0m);
                var prcs = p.Prcs ?? 0m;

                jhTotal += jh;
                caTotal += jh * tjm;
                coutEnTotal += jh * coutJour;
                prcsTotal += jh * prcs;
            }

            // 2) Frais
            var fraisReels = frais.Sum(x => x.Montant ?? 0m);
            var fraisBudget = 0m; // si pas de saisie "budget" pour l’instant, laisse 0

            // 3) Achats
            decimal AchatsBudget() => achats.Sum(x => x.AmountPurchaseHt ?? (x.UnitCostHt ?? 0m) * (x.Quantity ?? 0m));
            decimal AchatsValorise() => achats.Sum(x => x.AmountResaleHt ?? (x.UnitResaleHt ?? 0m) * (x.Quantity ?? 0m));
            var achatsBudget = AchatsBudget();
            var achatsValorise = AchatsValorise();

            // 4) Provisions (via Phase == "Provision" ou autre règle)
            decimal provisionJh = 0m, provisionCa = 0m;
            if (provisionByPhase)
            {
                foreach (var r in workloadLeafRows.Where(r => string.Equals(r.Phase, "Provision", StringComparison.OrdinalIgnoreCase)))
                {
                    provisionJh += r.ChargesByProfile?.Values.Sum() ?? 0m;
                    // Si tu n'as pas le CA par ligne de workload, tu peux approximer en CA = somme (JH* Tjm du profil concerné).
                    provisionCa += r.Ca; // si r.Ca est à 0, remplace par un calcul via profils (optionnel)
                }
            }

            // 5) Détails par catégorie
            var achatFront = AggFor(profils, getPlannedJhAllYears, "ACHAT");

            var fo = AggFor(profils, getPlannedJhAllYears, "FO");
            var ces = AggFor(profils, getPlannedJhAllYears, "CESSION");
            var bo = AggFor(profils, getPlannedJhAllYears, "BO");
            var achat = AggFor(profils, getPlannedJhAllYears, "ACHAT BO");

            var back = new BackAgg { Bo = bo, AchatBo = achat };

            // 6) VM final
            return new SyntheseVm
            {
                JhTotal = jhTotal,
                CaTotal = caTotal,
                CoutEnTotal = coutEnTotal,
                CoutAgenceTotal = prcsTotal,
                FraisBudget = fraisBudget,
                FraisReels = fraisReels,
                ProvisionJh = provisionJh,
                ProvisionCa = provisionCa,
                AchatsBudget = achatsBudget,
                AchatsValorise = achatsValorise,
                Fo = fo,
                Ces = ces,
                Back = back
            };
        }

        // ---------- Helpers ----------
        private static bool IsCession(ProfilEntry p)
            => string.Equals(p.Entite, "CESSION", StringComparison.OrdinalIgnoreCase)
            || string.Equals(p.Categorie, "CESSION", StringComparison.OrdinalIgnoreCase);

        private static CatAgg AggFor(IEnumerable<ProfilEntry> profils, Func<ProfilEntry, decimal> getPlannedJh, string cat)
        {
            decimal jh = 0m, ca = 0m, cout = 0m, prcs = 0m;

            foreach (var p in profils)
            {
                var isCat = string.Equals(p.Categorie, cat, StringComparison.OrdinalIgnoreCase)
                         || string.Equals(p.Entite, cat, StringComparison.OrdinalIgnoreCase);
                if (!isCat) continue;

                var j = getPlannedJh(p);
                var tjm = p.Tjm ?? (p.Prcs ?? p.Cjm ?? 0m);
                var cost = (p.Prcs ?? p.Cjm ?? 0m) + (IsCession(p) ? (p.FraisDeplacementSiCession ?? 0m) : 0m);
                var pr = p.Prcs ?? 0m;

                jh += j;
                ca += j * tjm;
                cout += j * cost;
                prcs += j * pr;
            }

            return new CatAgg { Jh = jh, Ca = ca, CoutEn = cout, Prcs = prcs };
        }
    }
}
