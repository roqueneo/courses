using System.Linq;
using System.Security.Claims;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Security.JsonWebTokens
{
    public class UserSession : IUserSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserSession(IHttpContextAccessor httpContextAccesor)
        {
            _httpContextAccessor = httpContextAccesor;
        }
        public string GetUserName()
            => _httpContextAccessor.HttpContext
                    .User?
                    .Claims?
                    .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?
                    .Value;

    }
}