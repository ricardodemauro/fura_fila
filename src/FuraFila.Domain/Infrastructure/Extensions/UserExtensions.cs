using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FuraFila.Domain.Infrastructure.Extensions
{
    public static class UserExtensions
    {
        public static string GetEmail(this ClaimsPrincipal principal)
        {
            return principal.Identity.Name;
        }

        public static string GetName(this ClaimsPrincipal principal)
        {
            var cl = principal.Claims.FirstOrDefault(c => string.Compare(c.Type, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name") == 0);
            return cl != null ? cl.Value : string.Empty;
        }

        public static string GetSurName(this ClaimsPrincipal principal)
        {
            var cl = principal.Claims.FirstOrDefault(c => string.Compare(c.Type, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname") == 0);
            return cl != null ? cl.Value : string.Empty;
        }
    }
}
