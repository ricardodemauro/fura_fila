using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuraFila.Payments.PagSeguro.Configuration
{
    public class PagSeguroOptions
    {
        public string AccessToken { get; set; }

        public bool IsSandbox { get; set; }

        public string CallbackUrl { get; set; }

        public string Email { get; set; }

        public string PaymentUrl { get; set; }

        public string PaymentUrlSandbox { get; set; }
    }
}
