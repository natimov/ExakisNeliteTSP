using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// ==== ALIAS vers TES modèles (ne touche pas ces namespaces si ce sont bien les tiens) ====
using DomainAchatItem  = ExakisNeliteTSP.Models.AchatItem;
using DomainFraisItem  = ExakisNeliteTSP.Models.FraisItem;
using DomainProfilEntry= ExakisNeliteTSP.Models.ProfilEntry;
using DomainProject    = ExakisNeliteTSP.Models.Project;
using DomainWorkload = ExakisNeliteTSP.Models.WorkloadItem; // <-- mets le VRAI nom de ton type

using ExakisNeliteTSP.Services; // contient SyntheseService



namespace ExakisNeliteTSP.Services
{
    public static class SyntheseAdaptater
    {
        public static SyntheseService.SyntheseVm BuildVmFromDomain(
            Models.Project project,
    IEnumerable<DomainWorkload> wlRows, IEnumerable<Models.AchatItem> achats,
            IEnumerable<Models.FraisItem> frais,
            Func<Models.ProfilEntry, decimal> getJhAllYears // ← fonction passée depuis TSProjet
        )
        {
            project ??= new Models.Project();
            achats ??= Enumerable.Empty<Models.AchatItem>();
            frais ??= Enumerable.Empty<Models.FraisItem>();

            var svc = new SyntheseService();

            // 1. Mapper tes profils vers les DTO utilisés par SyntheseService
            IEnumerable<SyntheseService.ProfilEntry> MapProfils()
            {
                foreach (var p in project.Profils ?? Enumerable.Empty<Models.ProfilEntry>())
                {
                    yield return new SyntheseService.ProfilEntry(
                        Categorie: null,                 // si tu as une propriété Catégorie, mets-la ici
                        Entite: p.Entite.ToString(),     // enum → ToString()
                        Tjm: p.Tjm,
                        Prcs: p.Prcs,
                        Cjm: p.Cjm,
                        FraisDeplacementSiCession: p.FraisDeplacementSiCession
                    );
                }
            }

            // 2. Fonction interne : récupère les JH via ta fonction externe
            decimal GetPlannedAllYears(SyntheseService.ProfilEntry e)
            {
                var p = (project.Profils ?? Enumerable.Empty<Models.ProfilEntry>())
                    .FirstOrDefault(x => x.Entite.ToString() == (e.Entite ?? string.Empty));

                return p is null ? 0m : getJhAllYears(p);
            }
            IEnumerable<SyntheseService.WorkloadRow> MapWorkload()
    => (wlRows ?? Enumerable.Empty<DomainWorkload>())
       .Select(w => new SyntheseService.WorkloadRow(
           Phase: w.Phase, // adapte si le nom diffère
           Ca: 0m,         // laisse 0 si tu n'as pas de CA par ligne
           ChargesByProfile: (w.ChargesByProfile ?? new Dictionary<int, decimal>())
                              .ToDictionary(kv => kv.Key.ToString(), kv => kv.Value)
       ));

            // 3. Map Achats
            IEnumerable<SyntheseService.AchatItem> MapAchats()
                => achats.Select(a => new SyntheseService.AchatItem(
                    a.UnitCostHt,
                    a.Quantity,
                    a.AmountPurchaseHt,
                    a.UnitResaleHt,
                    a.AmountResaleHt
                ));

            // 4. Map Frais
            IEnumerable<SyntheseService.FraisItem> MapFrais()
                => frais.Select(f => new SyntheseService.FraisItem(f.Montant));

            // 5. On ignore pour l’instant les workloads (provisions)
            var emptyWorkload = Enumerable.Empty<SyntheseService.WorkloadRow>();

            // 6. Construction finale du ViewModel
            return svc.Build(
            profils: MapProfils(),
            getPlannedJhAllYears: GetPlannedAllYears,
            workloadLeafRows: MapWorkload(),  
            achats: MapAchats(),
            frais: MapFrais(),
            provisionByPhase: true           
        );

        }
    }
}
