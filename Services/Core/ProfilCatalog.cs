using System;
using System.Collections.Generic;
using ExakisNeliteTSP.Models;
using ExakisNeliteTSP.Services.Interface;

namespace ExakisNeliteTSP.Services.Core
{
    public class ProfilCatalog : IProfilCatalog
    {
        private static readonly string[] _profils = new[]
        {
            "Consultant Junior",
            "Consultant",
            "Consultant confirmé",
            "Expert",
            "Expert senior",
            "Architecte",
            "Architecte Senior",
            "Chef de Projet",
            "Chef de Projet Senior",
            "Directeur de projet",
            "Consultant Fonctionnel",
            "Consultant Fonctionnel Senior",
            "Expert fonctionnel",
            "Référent",
            "Manager",
            "Dev Confirmé",
            "Dev Junior",
            "Testeur",
            "CES Infra Tech Support N1",
            "CES Infra Tech Support N2",
            "CES Infra Tech Support N3",
            "CES Infra Tech Support TAM"
        };

        public IReadOnlyList<string> GetProfils() => _profils;
        public IReadOnlyList<EntiteType> GetEntites() => Enum.GetValues<EntiteType>();
    }
}

