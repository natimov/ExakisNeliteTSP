using System;

namespace ExakisNeliteTSP.Data
{
    /// <summary>
    /// Ventilation mensuelle des charges pour une ligne de PROFIL.
    /// Clé: (ProjectId, ProfilRowId, Year, Month)
    /// </summary>
    public sealed class ProjectProfileMonthly
    {
        public int Id { get; set; }             // PK (IDENTITY)
        public Guid ProjectId { get; set; }     // FK -> Projects.Id
        public int ProfilRowId { get; set; }    // FK -> ProjectProfilRow.Id

        public int Year { get; set; }           // ex: 2025
        public byte Month { get; set; }         // 1..12

        public decimal? ChargeJh { get; set; }  // jours planifiés (jh)
    }
}
