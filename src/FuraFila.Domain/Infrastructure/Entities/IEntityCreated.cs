using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuraFila.Domain.Infrastructure.Entities
{
    public interface IEntityCreated
    {
        DateTime Created { get; set; }
    }
}
