using ExakisNeliteTSP;
using ExakisNeliteTSP.Components;
using ExakisNeliteTSP.Data;
using ExakisNeliteTSP.Services;
using ExakisNeliteTSP.Services.Core;
using ExakisNeliteTSP.Services.EF;
using ExakisNeliteTSP.Services.Interface;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Blazor Server
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddScoped<IProjectService, EfProjectService>();
builder.Services.AddSingleton<IConsultantDirectoryService, InMemoryConsultantDirectoryService>();
builder.Services.AddSingleton<IAbsenceService, InMemoryAbsenceService>();
builder.Services.AddScoped<IProfilCatalog, ProfilCatalog>();
builder.Services.AddScoped<IProjectProfilRowsService, EfProfilRowsService>();
builder.Services.AddScoped<IProjectProfileMonthlyService, EfProjectProfileMonthlyService>();
builder.Services.AddScoped<IProjectTjmService, ProjectTjmService>();
builder.Services.AddScoped<IProjectAchatService, ProjectAchatService>();
builder.Services.AddScoped<IProjectAchatPrestataireService, ProjectAchatPrestataireService>();
builder.Services.AddScoped<IProjectFraisService, ProjectFraisService>();
builder.Services.AddScoped<IProjectEcheancierService, EFEcheancierService>();
builder.Services.AddScoped<IWorkloadService, EFWorkloadService>();
builder.Services.AddDbContext<TspDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Ajouter ces 3 lignes :
// ⬇️ AJOUTER ces deux lignes dans la section services :
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ExakisNeliteTSP.Services.Interface.ICurrentUserService, ExakisNeliteTSP.Services.Core.Auth.CurrentUserService>();


builder.Services.AddAuthorizationCore(); // Si pas déjà présent
// ---------------------------
// 🔹 Configuration cookies & proxy (Easy Auth / Azure App Service)
// ---------------------------
builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.Cookie.SameSite = SameSiteMode.None;
    opt.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});
builder.Services.Configure<CookiePolicyOptions>(opt =>
{
    opt.MinimumSameSitePolicy = SameSiteMode.None;
    opt.Secure = CookieSecurePolicy.Always;
});
builder.Services.Configure<ForwardedHeadersOptions>(opt =>
{
    opt.ForwardedHeaders = ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedHost;
    opt.KnownNetworks.Clear();
    opt.KnownProxies.Clear();
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
})
.AddCookie("Cookies");

builder.Services.AddAuthorization(); // côté serveur
// (Tu peux conserver AddAuthorizationCore() si déjà présent pour les composants Blazor)



// ---------------------------
// 🔹 Build app
// ---------------------------
var app = builder.Build();

app.UseForwardedHeaders();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();
app.UseAuthentication();

// === Middleware EasyAuth (lecture X-MS-CLIENT-PRINCIPAL) ===
app.Use(async (ctx, next) =>
{
    if (ctx.Request.Headers.TryGetValue("X-MS-CLIENT-PRINCIPAL", out var header) && header.Count > 0)
    {
        try
        {
            var raw = header![0];
            var json = Encoding.UTF8.GetString(Convert.FromBase64String(raw));

            var principal = JsonSerializer.Deserialize<ClientPrincipal>(
                json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (principal?.claims is { Length: > 0 } || principal?.Claims is { Length: > 0 })
            {
                var all = principal!.claims ?? principal!.Claims!;
                var claims = all
                    .Select(c => new Claim(c.typ ?? c.Typ ?? string.Empty, c.val ?? c.Val ?? string.Empty))
                    .Where(c => !string.IsNullOrEmpty(c.Type));

                var identity = new ClaimsIdentity(claims, "EasyAuth");
                ctx.User = new ClaimsPrincipal(identity);
            }
        }
        catch
        {
            // silencieux
        }
    }

    await next();
});

app.UseAuthorization();
app.UseAntiforgery();


// ---------------------------
// 🔹 Mapping Blazor
// ---------------------------
app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

// ---------------------------
// 🔹 (Optionnel) Test connexion Azure SQL
// ---------------------------
var connStr = builder.Configuration.GetConnectionString("DefaultConnection");
try
{
    using var conn = new SqlConnection(connStr);
    conn.Open();
    Console.WriteLine($"✅ Connexion SQL OK : {conn.State}");
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Connexion SQL KO : {ex.Message}");
}

app.Run();

// ---------------------------
// 🔹 Records Easy Auth
// ---------------------------
// Modèles compatibles avec toutes les variantes de casse d'Easy Auth
// Modèles tolérants à la casse, alignés sur .auth/me (typ/val)
public record ClientPrincipalClaim(string? typ, string? val, string? Typ, string? Val);

public record ClientPrincipal(
    string? auth_typ, string? Auth_typ,
    ClientPrincipalClaim[]? claims, ClientPrincipalClaim[]? Claims,
    string? name_typ, string? Name_typ
);


