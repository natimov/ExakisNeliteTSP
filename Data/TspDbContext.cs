using ExakisNeliteTSP.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace ExakisNeliteTSP.Data
{
    public class TspDbContext : DbContext
    {
        public TspDbContext(DbContextOptions<TspDbContext> options) : base(options) { }

        public DbSet<Project> Projects => Set<Project>();
        public DbSet<ProjectProfilRow> ProjectProfilRows => Set<ProjectProfilRow>();

        public DbSet<ProjectConsultant> ProjectConsultants => Set<ProjectConsultant>();
        public DbSet<ProjectProfileMonthly> ProjectProfileMonthlies => Set<ProjectProfileMonthly>();
        public DbSet<ReferentielTjmItem> ReferentielTjmItems => Set<ReferentielTjmItem>();
        public DbSet<ProjectTjmItem> ProjectTjmItems => Set<ProjectTjmItem>();
        public DbSet<AchatItem> AchatItem => Set<AchatItem>();
        public DbSet<AchatPrestataireItem> AchatPrestataireItem => Set<AchatPrestataireItem>();

        public DbSet<FraisItem> FraisItem => Set<FraisItem>();
        public DbSet<EcheancierItem> EcheancierItem => Set<EcheancierItem>();

        public DbSet<WorkloadItem> WorkloadItems => Set<WorkloadItem>();
        public DbSet<WorkloadAllocation> WorkloadAllocations => Set<WorkloadAllocation>();





        protected override void OnModelCreating(ModelBuilder b)
        {
            b.Entity<Project>(e =>
            {
                e.HasKey(p => p.Id);
                e.Property(p => p.Title).HasMaxLength(200);
                e.Property(p => p.Client).HasMaxLength(200);

                e.Ignore(p => p.Consultants);
                e.Ignore(p => p.Profils);
                e.Ignore(p => p.ProfilTarifs);
                e.Property(p => p.ContractAmount).HasPrecision(18, 2);
                e.Property(p => p.ContractBackAmount).HasPrecision(18, 2);
                e.Property(p => p.ContractCaBack).HasPrecision(18, 2);
                e.Property(p => p.RemiseTjmPercent).HasPrecision(9, 4); 

            });

            b.Entity<ProjectConsultant>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Name).HasMaxLength(120).IsRequired();
                e.HasIndex(x => new { x.ProjectId, x.Name }).IsUnique();
                e.HasOne(x => x.Project)
                 .WithMany()
                 .HasForeignKey(x => x.ProjectId)
                 .OnDelete(DeleteBehavior.Cascade);
            });
            b.Entity<ProjectProfilRow>(e =>
            {
                e.HasKey(x => x.Id);  // PK

                // FK -> Projects.Id
                e.HasOne(x => x.Project)
                 .WithMany() // pour l'instant Project n'a pas de nav. collection
                 .HasForeignKey(x => x.ProjectId)
                 .OnDelete(DeleteBehavior.Cascade);

                // Champs simples
                e.Property(x => x.Profil).HasMaxLength(120);
                e.Property(x => x.Entite).HasConversion<string>().HasMaxLength(20);

                // Décimaux (pour SQL Server / Azure SQL)
                e.Property(x => x.Tjm).HasColumnType("decimal(18,2)");
                e.Property(x => x.Cjm).HasColumnType("decimal(18,2)");
                e.Property(x => x.Prcs).HasColumnType("decimal(18,2)");
                e.Property(x => x.FraisDeplacementSiCession).HasColumnType("decimal(18,2)");
                e.Property(x => x.TciOverPrcs).HasColumnType("decimal(18,2)");
                e.Property(x => x.ChargeJh).HasColumnType("decimal(18,2)");

                // Pour garder l'ordre des lignes
                e.Property(x => x.Order);
            });
            b.Entity<ProjectProfileMonthly>(e =>
            {
                e.HasKey(x => x.Id);

                e.Property(x => x.Year).IsRequired();
                e.Property(x => x.Month).IsRequired();
                e.Property(x => x.ChargeJh).HasPrecision(18, 2);   // précision €

                // Unicité Profil/Mois
                e.HasIndex(x => new { x.ProjectId, x.ProfilRowId, x.Year, x.Month }).IsUnique();

                // FK -> Projects  ❌ PAS de cascade (pour éviter multiple cascade paths)
                e.HasOne<Project>()
                 .WithMany()
                 .HasForeignKey(x => x.ProjectId)
                 .OnDelete(DeleteBehavior.NoAction);

                // FK -> ProjectProfilRow  ✅ on garde la cascade (si une ligne profil est supprimée)
                e.HasOne<ProjectProfilRow>()
                 .WithMany()
                 .HasForeignKey(x => x.ProfilRowId)
                 .OnDelete(DeleteBehavior.Cascade);

            });


            // --- Seed des valeurs par défaut (tableaux jaunes) ---
            // --- RESET complet : référentiels TJM/CJM/PRCS identiques à l'Excel ---
            b.Entity<ReferentielTjmItem>().HasData(
                // ========= Agences (TJM/CJM) =========
                // Consultant Junior
                new() { Id = 1000, Category = "Agences", Profile = "Consultant Junior", Location = "TJM Paris", Tjm = 500m },
                new() { Id = 1001, Category = "Agences", Profile = "Consultant Junior", Location = "CJM Paris", Tjm = 300m },
                new() { Id = 1002, Category = "Agences", Profile = "Consultant Junior", Location = "TJM Région", Tjm = 435m },
                new() { Id = 1003, Category = "Agences", Profile = "Consultant Junior", Location = "CJM Région", Tjm = 261m },

                // Consultant
                new() { Id = 1010, Category = "Agences", Profile = "Consultant", Location = "TJM Paris", Tjm = 583m },
                new() { Id = 1011, Category = "Agences", Profile = "Consultant", Location = "CJM Paris", Tjm = 350m },
                new() { Id = 1012, Category = "Agences", Profile = "Consultant", Location = "TJM Région", Tjm = 482m },
                new() { Id = 1013, Category = "Agences", Profile = "Consultant", Location = "CJM Région", Tjm = 289m },

                // Consultant confirmé
                new() { Id = 1020, Category = "Agences", Profile = "Consultant confirmé", Location = "TJM Paris", Tjm = 717m },
                new() { Id = 1021, Category = "Agences", Profile = "Consultant confirmé", Location = "CJM Paris", Tjm = 430m },
                new() { Id = 1022, Category = "Agences", Profile = "Consultant confirmé", Location = "TJM Région", Tjm = 567m },
                new() { Id = 1023, Category = "Agences", Profile = "Consultant confirmé", Location = "CJM Région", Tjm = 340m },

                // Expert
                new() { Id = 1030, Category = "Agences", Profile = "Expert", Location = "TJM Paris", Tjm = 792m },
                new() { Id = 1031, Category = "Agences", Profile = "Expert", Location = "CJM Paris", Tjm = 475m },
                new() { Id = 1032, Category = "Agences", Profile = "Expert", Location = "TJM Région", Tjm = 667m },
                new() { Id = 1033, Category = "Agences", Profile = "Expert", Location = "CJM Région", Tjm = 400m },

                // Expert senior
                new() { Id = 1040, Category = "Agences", Profile = "Expert senior", Location = "TJM Paris", Tjm = 842m },
                new() { Id = 1041, Category = "Agences", Profile = "Expert senior", Location = "CJM Paris", Tjm = 505m },
                new() { Id = 1042, Category = "Agences", Profile = "Expert senior", Location = "TJM Région", Tjm = 767m },
                new() { Id = 1043, Category = "Agences", Profile = "Expert senior", Location = "CJM Région", Tjm = 460m },

                // Architecte
                new() { Id = 1050, Category = "Agences", Profile = "Architecte", Location = "TJM Paris", Tjm = 842m },
                new() { Id = 1051, Category = "Agences", Profile = "Architecte", Location = "CJM Paris", Tjm = 505m },
                new() { Id = 1052, Category = "Agences", Profile = "Architecte", Location = "TJM Région", Tjm = 697m },
                new() { Id = 1053, Category = "Agences", Profile = "Architecte", Location = "CJM Région", Tjm = 418m },

                // Architecte Senior
                new() { Id = 1060, Category = "Agences", Profile = "Architecte Senior", Location = "TJM Paris", Tjm = 917m },
                new() { Id = 1061, Category = "Agences", Profile = "Architecte Senior", Location = "CJM Paris", Tjm = 550m },
                new() { Id = 1062, Category = "Agences", Profile = "Architecte Senior", Location = "TJM Région", Tjm = 833m },
                new() { Id = 1063, Category = "Agences", Profile = "Architecte Senior", Location = "CJM Région", Tjm = 500m },

                // Chef de Projet
                new() { Id = 1070, Category = "Agences", Profile = "Chef de Projet", Location = "TJM Paris", Tjm = 755m },
                new() { Id = 1071, Category = "Agences", Profile = "Chef de Projet", Location = "CJM Paris", Tjm = 453m },
                new() { Id = 1072, Category = "Agences", Profile = "Chef de Projet", Location = "TJM Région", Tjm = 625m },
                new() { Id = 1073, Category = "Agences", Profile = "Chef de Projet", Location = "CJM Région", Tjm = 375m },

                // Chef de Projet Senior
                new() { Id = 1080, Category = "Agences", Profile = "Chef de Projet Senior", Location = "TJM Paris", Tjm = 883m },
                new() { Id = 1081, Category = "Agences", Profile = "Chef de Projet Senior", Location = "CJM Paris", Tjm = 530m },
                new() { Id = 1082, Category = "Agences", Profile = "Chef de Projet Senior", Location = "TJM Région", Tjm = 718m },
                new() { Id = 1083, Category = "Agences", Profile = "Chef de Projet Senior", Location = "CJM Région", Tjm = 431m },

                // Directeur de projet
                new() { Id = 1090, Category = "Agences", Profile = "Directeur de projet", Location = "TJM Paris", Tjm = 983m },
                new() { Id = 1091, Category = "Agences", Profile = "Directeur de projet", Location = "CJM Paris", Tjm = 590m },
                new() { Id = 1092, Category = "Agences", Profile = "Directeur de projet", Location = "TJM Région", Tjm = 867m },
                new() { Id = 1093, Category = "Agences", Profile = "Directeur de projet", Location = "CJM Région", Tjm = 520m },

                // Consultant Fonctionnel
                new() { Id = 1100, Category = "Agences", Profile = "Consultant Fonctionnel", Location = "TJM Paris", Tjm = 648m },
                new() { Id = 1101, Category = "Agences", Profile = "Consultant Fonctionnel", Location = "CJM Paris", Tjm = 389m },
                new() { Id = 1102, Category = "Agences", Profile = "Consultant Fonctionnel", Location = "TJM Région", Tjm = 505m },
                new() { Id = 1103, Category = "Agences", Profile = "Consultant Fonctionnel", Location = "CJM Région", Tjm = 303m },

                // Consultant Fonctionnel Senior
                new() { Id = 1110, Category = "Agences", Profile = "Consultant Fonctionnel Senior", Location = "TJM Paris", Tjm = 700m },
                new() { Id = 1111, Category = "Agences", Profile = "Consultant Fonctionnel Senior", Location = "CJM Paris", Tjm = 420m },
                new() { Id = 1112, Category = "Agences", Profile = "Consultant Fonctionnel Senior", Location = "TJM Région", Tjm = 600m },
                new() { Id = 1113, Category = "Agences", Profile = "Consultant Fonctionnel Senior", Location = "CJM Région", Tjm = 360m },

                // Expert Fonctionnel
                new() { Id = 1120, Category = "Agences", Profile = "Expert Fonctionnel", Location = "TJM Paris", Tjm = 775m },
                new() { Id = 1121, Category = "Agences", Profile = "Expert Fonctionnel", Location = "CJM Paris", Tjm = 465m },
                new() { Id = 1122, Category = "Agences", Profile = "Expert Fonctionnel", Location = "TJM Région", Tjm = 683m },
                new() { Id = 1123, Category = "Agences", Profile = "Expert Fonctionnel", Location = "CJM Région", Tjm = 410m },

                // Référent
                new() { Id = 1130, Category = "Agences", Profile = "Référent", Location = "TJM Paris", Tjm = 1000m },
                new() { Id = 1131, Category = "Agences", Profile = "Référent", Location = "CJM Paris", Tjm = 600m },
                new() { Id = 1132, Category = "Agences", Profile = "Référent", Location = "TJM Région", Tjm = 767m },
                new() { Id = 1133, Category = "Agences", Profile = "Référent", Location = "CJM Région", Tjm = 460m },

                // Manager
                new() { Id = 1140, Category = "Agences", Profile = "Manager", Location = "TJM Paris", Tjm = 1050m },
                new() { Id = 1141, Category = "Agences", Profile = "Manager", Location = "CJM Paris", Tjm = 630m },
                new() { Id = 1142, Category = "Agences", Profile = "Manager", Location = "TJM Région", Tjm = 833m },
                new() { Id = 1143, Category = "Agences", Profile = "Manager", Location = "CJM Région", Tjm = 500m },

                // ========= CES Log (TJM/CJM/PRCS) =========
                new() { Id = 2000, Category = "CES Log", Profile = "Architecte", Location = "TJM CES", Tjm = 652m },
                new() { Id = 2001, Category = "CES Log", Profile = "Architecte", Location = "CJM CES", Tjm = 391m },
                new() { Id = 2002, Category = "CES Log", Profile = "Architecte", Location = "PRCS CES", Tjm = 555m },

                new() { Id = 2010, Category = "CES Log", Profile = "Chef de Projet", Location = "TJM CES", Tjm = 603m },
                new() { Id = 2011, Category = "CES Log", Profile = "Chef de Projet", Location = "CJM CES", Tjm = 362m },
                new() { Id = 2012, Category = "CES Log", Profile = "Chef de Projet", Location = "PRCS CES", Tjm = 514m },

                new() { Id = 2020, Category = "CES Log", Profile = "Dev confirmé", Location = "TJM CES", Tjm = 570m },
                new() { Id = 2021, Category = "CES Log", Profile = "Dev confirmé", Location = "CJM CES", Tjm = 342m },
                new() { Id = 2022, Category = "CES Log", Profile = "Dev confirmé", Location = "PRCS CES", Tjm = 486m },

                new() { Id = 2030, Category = "CES Log", Profile = "Dev junior", Location = "TJM CES", Tjm = 417m },
                new() { Id = 2031, Category = "CES Log", Profile = "Dev junior", Location = "CJM CES", Tjm = 250m },
                new() { Id = 2032, Category = "CES Log", Profile = "Dev junior", Location = "PRCS CES", Tjm = 355m },

                new() { Id = 2040, Category = "CES Log", Profile = "Manager", Location = "TJM CES", Tjm = 915m },
                new() { Id = 2041, Category = "CES Log", Profile = "Manager", Location = "CJM CES", Tjm = 549m },
                new() { Id = 2042, Category = "CES Log", Profile = "Manager", Location = "PRCS CES", Tjm = 780m },

                // ========= CES Infra (TJM/CJM) =========
                new() { Id = 3000, Category = "CES Infra", Profile = "Technicien Support N1", Location = "TJM CES", Tjm = 180m },
                new() { Id = 3001, Category = "CES Infra", Profile = "Technicien Support N1", Location = "CJM CES", Tjm = 180m },

                new() { Id = 3010, Category = "CES Infra", Profile = "Technicien Support N2", Location = "TJM CES", Tjm = 235m },
                new() { Id = 3011, Category = "CES Infra", Profile = "Technicien Support N2", Location = "CJM CES", Tjm = 235m },

                new() { Id = 3020, Category = "CES Infra", Profile = "Ingénieur Support N3", Location = "TJM CES", Tjm = 325m },
                new() { Id = 3021, Category = "CES Infra", Profile = "Ingénieur Support N3", Location = "CJM CES", Tjm = 325m },

                new() { Id = 3030, Category = "CES Infra", Profile = "TAM", Location = "TJM CES", Tjm = 360m },
                new() { Id = 3031, Category = "CES Infra", Profile = "TAM", Location = "CJM CES", Tjm = 360m }
            );
            // === AchatItem ===
            b.Entity<AchatItem>(e =>
            {
                e.HasKey(x => x.Id);

                e.Property(x => x.Designation)
                    .HasMaxLength(128)
                    .IsRequired();

                e.Property(x => x.PurchaseType)
                    .HasConversion<string>()
                    .HasMaxLength(64);

                e.Property(x => x.UnitCostHt).HasPrecision(18, 2);
                e.Property(x => x.Quantity).HasPrecision(18, 2);
                e.Property(x => x.UnitResaleHt).HasPrecision(18, 2);
                e.Property(x => x.Comment).HasMaxLength(512);

                // ⛔️ NE PAS mapper :
                // e.Property(x => x.AmountPurchaseHt)...
                // e.Property(x => x.AmountResaleHt)...
                // e.Property(x => x.MarkupPercent)...
            });



            // === AchatPrestataireItem ===
            b.Entity<AchatPrestataireItem>(e =>
            {
                e.HasKey(x => x.Id);

                e.Property(x => x.Societe).HasMaxLength(128).IsRequired();
                e.Property(x => x.Prestation).HasMaxLength(128);
                e.Property(x => x.CoutJour).HasPrecision(18, 2);
                e.Property(x => x.NbJours).HasPrecision(18, 2);
                e.Property(x => x.Tjm).HasPrecision(18, 2);
                e.Property(x => x.Commentaire).HasMaxLength(512);

            });
            b.Entity<FraisItem>(e =>
            {
                e.ToTable("FraisItem");
                e.HasKey(x => x.Id);

                // Index sur ProjectId pour faciliter les requêtes
                e.HasIndex(x => x.ProjectId);

                // Col.1 : Libellé frais
                e.Property(x => x.Libelle)
                    .HasMaxLength(128)
                    .IsRequired();

                // Col.2 : Coût unitaire
                e.Property(x => x.CoutUnitaire).HasPrecision(18, 2);

                // Col.3 : Quantité
                e.Property(x => x.Quantite).HasPrecision(18, 2);

                // Col.5 : Montant refacturé
                e.Property(x => x.MontantRefacture).HasPrecision(18, 2);

                // Col.6 : Commentaire (nullable, texte libre)
                e.Property(x => x.Commentaire);

                // Col.4 : Montant est calculé côté C#, pas stocké en base
                e.Ignore(x => x.Montant);
            });
            b.Entity<EcheancierItem>(e =>
            {
                e.ToTable("EcheancierItem");
                e.HasKey(x => x.Id);

                e.HasIndex(x => new { x.ProjectId, x.BillingYear, x.BillingMonth });

                e.Property(x => x.Label)
                    .HasMaxLength(128)
                    .IsRequired();

                e.Property(x => x.Percent).HasPrecision(9, 4);

                // Non mappés (calculés côté app)
                e.Ignore(x => x.AmountFacture);
                e.Ignore(x => x.CumulFacture);
                e.Ignore(x => x.CoutEngageCumul);
                e.Ignore(x => x.FAE);
            });

            b.Entity<WorkloadItem>(e =>
            {
                e.HasKey(x => x.Id);

                e.Property(x => x.Lot).HasMaxLength(200);
                e.Property(x => x.Phase).HasMaxLength(100);
                e.Property(x => x.Etape).HasMaxLength(150);
                e.Property(x => x.Tache).HasMaxLength(1000);
                e.Property(x => x.Livrable).HasMaxLength(1000);
                e.Property(x => x.Commentaire).HasMaxLength(2000);

                e.HasIndex(x => new { x.ProjectId, x.OrderIndex });

                // Hiérarchie parent -> enfants
                e.HasOne<WorkloadItem>()
                 .WithMany()
                 .HasForeignKey(x => x.ParentId)
                 .OnDelete(DeleteBehavior.Restrict); // garde le contrôle côté code pour supprimer le bloc
            });

            b.Entity<WorkloadAllocation>(e =>
            {
                e.HasKey(x => x.Id);

                // Une allocation par (ligne, profil)
                e.HasIndex(x => new { x.WorkloadItemId, x.ProfileId }).IsUnique();

                e.Property(x => x.JoursHomme).HasColumnType("decimal(10,2)");

                e.HasOne(x => x.WorkloadItem)
                 .WithMany(p => p.Allocations)
                 .HasForeignKey(x => x.WorkloadItemId)
                 .OnDelete(DeleteBehavior.Cascade);
            });





        }
    }

    public class ProjectConsultant
    {
        public int Id { get; set; }
        public Guid ProjectId { get; set; }
        public string Name { get; set; } = "";
        public Project Project { get; set; } = null!;
    }
}
