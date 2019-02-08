using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuraFila.Payments.MercadoPago.Configuration
{
    public class MercadoPagoOptions
    {
        public string AccessToken { get; set; }

        public bool IsSandbox { get; set; }

        public string CallbackUrl { get; set; }
    }
}
