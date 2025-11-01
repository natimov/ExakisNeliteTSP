namespace ExakisNeliteTSP.Models;

public enum ProjectStatus { Draft, InReview, ValidationPending, InProgress, Done, OnHold }
public enum Complexity { Simple, Medium, Complex }

public sealed class Project
{
    
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Title { get; set; } = "";
    public string? Description { get; set; }
    public string? Client { get; set; }
    public ProjectStatus Status { get; set; } = ProjectStatus.Draft;
    public Complexity Complexity { get; set; } = Complexity.Simple;
    public string? ArchitectCds { get; set; }
    public string? ArchitectInternal { get; set; }
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    public List<string> Consultants { get; set; } = new();
    public string? ProjectLead { get; set; }   // Chef de projet
    public DateTime? StartDate { get; set; }   // Date de début (JJ/MM/AAAA)
    public DateTime? DueDate { get; set; }   // Date de fin prévue (JJ/MM/AAAA)

    // Phase détaillant "En cours" (uniquement pertinente si Status == InProgress)
    public ProjectPhase? Phase { get; set; } = null;
    // --- Fiche projet (infos commerciales / organisation) ---
    public string? ProposalRef { get; set; }            // Référence propale
    public string? ClientContact { get; set; }          // Contact client

    public string? Agency { get; set; }                 // Agence
    public string? ServiceLine { get; set; }            // Service line
    public string? AgencyDirector { get; set; }         // Directeur d’agence
    public string? ServiceLineManager { get; set; }     // Responsable SL
    public string? SalesRep { get; set; }               // Commercial
    public string? BackProjectManager { get; set; }     // Chef de projet back (Unit4)

    public string? ContractType { get; set; }           // Type de contrat
    public decimal? ContractAmount { get; set; }        // Montant du contrat (total)
    public decimal? ContractCaBack { get; set; }        // dont CA back
    public decimal? ContractBackAmount { get; set; }    // Montant du contrat back
    public decimal? RemiseTjmPercent { get; set; } = 0m; // % remise appliquée au TJM (équiv. J17 Excel)
    public List<ProfilEntry> Profils { get; set; } = new(); // lignes du tableau PROFIL
    public List<ProfilTarifRef> ProfilTarifs { get; set; } = new(); // table de ref Paris/Région



}
// ↑ Place ceci près de tes autres enums
public enum ProjectPhase
{
    None,          // valeur "vide" quand le projet n'est pas "En cours"
    Pilotage,
    Conception,
    Construction,
    Validation,
    Finalisation,
    Garantie,
    TMA,
    Provision
}

public enum ProjectEtape
{
    None,
    PilotageProjet,
    AnalyseDeLExistant,
    ConceptionGenerale,
    ConceptionTechniqueDetaillee,
    ConceptionFonctionnelleDetaillee,
    EgornomieGraphisme,
    CahierDeRecette,
    DeveloppementTestsUnitaires,
    TestsIntegrationsARecetteInterne,
    LivraisonAInstallationAMiseEnOeuvre,
    Pilote,
    Generalisation,
    Support,
    RepriseDeContenu,
    AssistanceRecette,
    Formation,
    Garantie,
    ProvisionPourRisque,
    HorsAbaques
}

public enum ServiceLine
{
    None,
    AID,
    MD,
    MW,
    Operations,
    HR
}