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
builder.Services.AddScoped<IProjectAchatService, ProjectAchatService>();
builder.Services.AddScoped<IProjectAchatPrestataireService, ProjectAchatPrestataireService>();
builder.Services.AddScoped<IProjectFraisService, ProjectFraisService>();
builder.Services.AddScoped<IProjectEcheancierService, EFEcheancierService>();





var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.UseAntiforgery();


app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

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




app.Run();
