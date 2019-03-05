using FuraFila.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FuraFila.Identity
{
    public class AppClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        private readonly IFormatProvider formatProvider = CultureInfo.InvariantCulture;

        public AppClaimsPrincipalFactory(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        {
        }

        public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            ClaimsPrincipal principal = await base.CreateAsync(user);

            var identity = (ClaimsIdentity)principal.Identity;

            if (!string.IsNullOrEmpty(user.Name))
            {
                var clName = new Claim(type: ClaimTypes.GivenName, value: user.Name);
                identity.AddClaims(new[] { clName });
            }
            if (!string.IsNullOrEmpty(user.SurName))
            {
                var clSurName = new Claim(type: ClaimTypes.Surname, value: user.SurName);
                identity.AddClaims(new[] { clSurName });
            }
            if (!string.IsNullOrEmpty(user.Email))
            {
                var clEmail = new Claim(type: ClaimTypes.Email, value: user.Email);
                identity.AddClaims(new[] { clEmail });
            }
            if (!string.IsNullOrEmpty(user.PhoneNumber))
            {
                var clPhone = new Claim(type: ClaimTypes.MobilePhone, value: user.PhoneNumber);
                identity.AddClaims(new[] { clPhone });
            }
            if (user.DateOfBirth.HasValue)
            {
                var clDateBirth = user.DateOfBirth.Value.ToClaimsFormat(ClaimTypes.DateOfBirth);
                identity.AddClaims(new[] { clDateBirth });
            }
            return principal;
        }
    }
}
