using System;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using ExakisNeliteTSP.Models;
using ExakisNeliteTSP.Services.Core;
using ExakisNeliteTSP.Services.Interface;

namespace ExakisNeliteTSP.Pages
{
    public partial class TSProjet : ComponentBase
    {
        
        [Inject] private IProjectService Projects { get; set; } = default!;
        [Inject] private NavigationManager Nav { get; set; } = default!;
        [Inject] private IProjectProfilRowsService ProfilRowsSvc { get; set; } = default!;
        [Inject] private IProjectProfileMonthlyService ProfileMonthlySvc { get; set; } = default!;
        [Inject] private IProjectTjmService ProjectTjmSvc { get; set; } = default!;
        [Inject] private IProjectAchatService AchatSvc { get; set; } = default!;
        [Inject] private IProjectAchatPrestataireService PrestasSvc { get; set; } = default!;
        [Inject] private IProjectFraisService FraisSvc { get; set; } = default!;
        [Inject] private IProjectEcheancierService EcheancierSvc { get; set; } = default!;
        [Inject] private IJSRuntime JS { get; set; } = default!;

        [Parameter] public Guid id { get; set; }




}
}
