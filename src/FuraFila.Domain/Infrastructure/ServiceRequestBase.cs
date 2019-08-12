using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FuraFila.Domain.Infrastructure
{
    public abstract class ServiceRequestBase : IRequestContext
    {
        public ClaimsPrincipal User { get; set; }
    }
}
