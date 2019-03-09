using System;
using System.Linq;
using System.Security.Claims;

namespace FuraFila.Identity
{
    public static class UserExtensions
    {
        public static string GetUserPublicId(this ClaimsPrincipal principal)
        {
            var cl = principal.Claims.FirstOrDefault(c => string.Compare(c.Type, FuraFileClaimTypes.UserPublicId) == 0);
            return cl != null ? cl.Value : string.Empty;
        }

        public static string GetEmail(this ClaimsPrincipal principal)
        {
            var cl = principal.Claims.FirstOrDefault(c => string.Compare(c.Type, ClaimTypes.Email) == 0);
            return cl != null ? cl.Value : string.Empty;
        }

        public static string GetName(this ClaimsPrincipal principal)
        {
            var cl = principal.Claims.FirstOrDefault(c => string.Compare(c.Type, ClaimTypes.GivenName) == 0);
            return cl != null ? cl.Value : string.Empty;
        }

        public static string GetSurname(this ClaimsPrincipal principal)
        {
            var cl = principal.Claims.FirstOrDefault(c => string.Compare(c.Type, ClaimTypes.Surname) == 0);
            return cl != null ? cl.Value : string.Empty;
        }

        public static string GetPhone(this ClaimsPrincipal principal)
        {
            var cl = principal.Claims.FirstOrDefault(c => string.Compare(c.Type, ClaimTypes.MobilePhone) == 0);
            return cl != null ? cl.Value : string.Empty;
        }

        public static DateTime? GetDateOfBirth(this ClaimsPrincipal principal)
        {
            Claim cl = principal.Claims.FirstOrDefault(c => string.Compare(c.Type, ClaimTypes.DateOfBirth) == 0);
            return cl != null ? cl.FromClaimsFormat() : DateTime.MinValue;
        }
    }
}
