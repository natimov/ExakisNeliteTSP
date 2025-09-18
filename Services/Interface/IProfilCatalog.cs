using System.Collections.Generic;
using ExakisNeliteTSP.Models;

namespace ExakisNeliteTSP.Services.Interface
{
    public interface IProfilCatalog
    {
        IReadOnlyList<string> GetProfils();
        IReadOnlyList<EntiteType> GetEntites();
    }
}
