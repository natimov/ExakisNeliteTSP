using ExakisNeliteTSP;
using ExakisNeliteTSP.Components;
using ExakisNeliteTSP.Services;

var builder = WebApplication.CreateBuilder(args);

// 1) Services Blazor unifié + interactif serveur
builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

// 2) Tes services applicatifs
builder.Services.AddSingleton<IProjectService, InMemoryProjectService>();
builder.Services.AddSingleton<IConsultantDirectoryService, InMemoryConsultantDirectoryService>();
builder.Services.AddSingleton<IAbsenceService, InMemoryAbsenceService>();

// .cs


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// (si tu ajoutes auth plus tard : app.UseAuthentication(); app.UseAuthorization();)
// >>> AJOUT OBLIGATOIRE .NET 8 (anti-forgery) <<<
app.UseAntiforgery();

// 3) Point d’entrée Blazor (pas de _Host.cshtml)
app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.Run();
