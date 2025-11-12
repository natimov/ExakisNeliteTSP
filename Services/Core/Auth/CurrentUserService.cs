using ExakisNeliteTSP.Services.Interface;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ExakisNeliteTSP.Services.Core.Auth
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _http;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _http = httpContextAccessor;
        }

        public string Oid =>
            _http.HttpContext?.User?
                .FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")
                ?.Value ?? string.Empty;

        public string Email =>
            _http.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value
            ?? _http.HttpContext?.User?.FindFirst("preferred_username")?.Value
            ?? _http.HttpContext?.User?.Identity?.Name
            ?? string.Empty;

        public string FullName =>
            _http.HttpContext?.User?.FindFirst("name")?.Value
            ?? Email;
    }
}
