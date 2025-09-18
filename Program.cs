using ExakisNeliteTSP;
using ExakisNeliteTSP.Components;
using ExakisNeliteTSP.Data;
using ExakisNeliteTSP.Services;
using ExakisNeliteTSP.Services.Core;
using ExakisNeliteTSP.Services.EF;
using ExakisNeliteTSP.Services.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// 1) Services Blazor unifié + interactif serveur
builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

// 2) Tes services applicatifs
builder.Services.AddScoped<IProjectService, EfProjectService>();

builder.Services.AddSingleton<IConsultantDirectoryService, InMemoryConsultantDirectoryService>();
builder.Services.AddSingleton<IAbsenceService, InMemoryAbsenceService>();
builder.Services.AddScoped<IProfilCatalog,
                           ProfilCatalog>();
builder.Services.AddDbContext<TspDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IProjectProfilRowsService, EfProfilRowsService>();
builder.Services.AddServerSideBlazor().AddCircuitOptions(o => o.DetailedErrors = true);
builder.Services.AddScoped<IProjectProfileMonthlyService, EfProjectProfileMonthlyService>();
builder.Services.AddScoped<IProjectTjmService, ProjectTjmService>();


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
// --- TEST TEMPORAIRE DE CONNEXION AZURE SQL ---
var connStr = builder.Configuration.GetConnectionString("DefaultConnection");
try
{
    using var conn = new Microsoft.Data.SqlClient.SqlConnection(connStr);
    conn.Open(); // synchrone
    Console.WriteLine($"? Connexion Azure SQL OK. State={conn.State}");
}
catch (Exception ex)
{
    Console.WriteLine("? Connexion Azure SQL KO : " + ex.Message);
}
// --- FIN TEST ---



app.Run();
