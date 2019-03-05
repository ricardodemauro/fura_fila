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

            var clName = new Claim(type: ClaimTypes.GivenName, value: user.Name);
            var clSurName = new Claim(type: ClaimTypes.Surname, value: user.SurName);
            var clEmail = new Claim(type: ClaimTypes.Email, value: user.Email);
            var clDateBirth = new Claim(type: ClaimTypes.DateOfBirth, value: user.DateOfBirth?.ToString("yyyy-MM-ddTHH:mm:ssZ"));
            identity.AddClaims(new[]
            {
                clName,
                clSurName,
                clEmail,
                clDateBirth
            });

            return principal;
        }
    }
}
