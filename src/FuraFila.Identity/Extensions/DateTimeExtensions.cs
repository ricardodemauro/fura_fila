using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FuraFila.Identity
{
    public static class DateTimeExtensions
    {
        internal static DateTime FromClaimsFormat(this Claim claim)
        {
            return DateTime.ParseExact(claim.Value, Constants.DATE_FORMAT, CultureInfo.InvariantCulture);
        }

        internal static Claim ToClaimsFormat(this DateTime dt, string claimType)
        {
            var clDateBirth = new Claim(type: claimType, value: dt.ToString(Constants.DATE_FORMAT, CultureInfo.InvariantCulture));
            return clDateBirth;
        }
    }
}
